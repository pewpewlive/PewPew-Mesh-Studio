using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using PewPewMeshStudio.LuaUtils;
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.UI;
using PewPewMeshStudio.ExtraUtils;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Serilog;
using static OpenTK.Graphics.OpenGL.GL;
using OpenTK.Compute.OpenCL;
using PewPewMeshStudio.LuaAPI;
using System.Threading;
using System.IO;

namespace PewPewMeshStudio.Core;

public class Window : GameWindow
{
    private const int WINDOW_WIDTH = 800;
    private const int WINDOW_HEIGHT = 600;

    ImGuiController UIController;
    UIHandler uiHandler = new UIHandler();

    //public string lastAction = "Last Action: Not Applicable";

    GCHandle FontPtr = GCHandle.Alloc(Properties.Resources.Font, GCHandleType.Pinned);

    public Renderable Mesh { set; get; }
    private Thread meshThread;
    
    // Try to find a better solution for importing meshes from LuaAPI
    public static bool isMeshChangeRequest { set; private get; } = false;
    public static string requestedMeshPath { set; private get; }
    public static int requestedMeshIndex { set; private get; }

    Camera MeshCamera = new Camera();
    InputSystem track = new InputSystem();
    private bool MouseHeld = false;
    public Window() : base(GameWindowSettings.Default, new NativeWindowSettings()
    {
        Size = new Vector2i(WINDOW_WIDTH, WINDOW_HEIGHT),
        APIVersion = new Version(4, 1),
        Title = "PewPew Mesh Studio",
        NumberOfSamples = 8,
    })
    {
        VSync = VSyncMode.On;
        UIController = new ImGuiController(WINDOW_WIDTH, WINDOW_HEIGHT, FontPtr.AddrOfPinnedObject());
        Icon = new WindowIcon(new OpenTK.Windowing.Common.Input.Image(64, 64, Properties.Resources.Logo));
        //WindowState = WindowState.Maximized;

        Mesh = new Renderable(Array.Empty<MeshVertex>(), Array.Empty<uint[]>());

        //Mesh = MeshParser.ParseMeshFile("s.lua", 1);
        meshThread = new Thread(new ThreadStart(RunMesh));
        meshThread.Name = "MeshThread";

        //meshThread.Start();
    }
    private void RunMesh()
    {
        Mesh = MeshParser.ParseMeshFile("s.lua", 1);
    }
    protected override void OnUnload()
    {
        base.OnUnload();
        Mesh.Destroy();
        FontPtr.Free();
        Log.Information("(Window @ OnUnload) <{thread}> Closed GUI, closing application...", Thread.CurrentThread.Name);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        
        Log.Information("(Window @ OnLoad) <{thread}> GUI loaded successfully.", Thread.CurrentThread.Name);
        Interpreter.Run("plugins\\test.lua");
        //UIHandler.openModals = UIHandler.OpenModals.SplashScreen;
    }

    protected override void OnResize(ResizeEventArgs Event)
    {
        base.OnResize(Event);
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        UIController.WindowResized(ClientSize.X, ClientSize.Y);
    }

    protected override void OnRenderFrame(FrameEventArgs Event)
    {
        base.OnRenderFrame(Event);

        UIController.Update(this, (float)Event.Time);

        GL.ClearColor(new Color4(0, 0, 0, 255));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

        //RangeAccessor<System.Numerics.Vector4> colors = style.Colors;
        //colors[0] = ColorUtil.Vec4IntToFloat(new System.Numerics.Vector4(255, 0, 255, 255));

        if (isMeshChangeRequest)
        {
            Mesh = MeshParser.ParseMeshFile(requestedMeshPath, requestedMeshIndex);
            isMeshChangeRequest = false;
        }

        Mesh.Render((Vector2)ClientSize, MeshCamera);
        track.Track();

        uiHandler.InitUI();

        if (!ImGui.IsAnyItemHovered())
            CursorSetter.ResetCursor();

        UIController.Render();

        ImGuiController.CheckGLError("End of frame");

        SwapBuffers();
    }

    protected override void OnTextInput(TextInputEventArgs Event)
    {
        base.OnTextInput(Event);
        UIController.PressChar((char)Event.Unicode);
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs Event)
    {
        base.OnKeyDown(Event);
        if (Event.Key == Keys.F11 && (WindowState == WindowState.Normal || WindowState == WindowState.Maximized))
        {
            WindowState = WindowState.Fullscreen;
            VSync = VSyncMode.On;
        }
        else if (Event.Key == Keys.F11 && WindowState == WindowState.Fullscreen)
        {
            WindowState = WindowState.Normal;
        }
    }

    protected override void OnMouseWheel(MouseWheelEventArgs Event)
    {
        base.OnMouseWheel(Event);
        UIController.MouseScroll(Event.Offset);
        MeshCamera.ZoomBy(Event.OffsetY < 0.0f ? 10.0f * MathF.Abs(Event.OffsetY) : -10.0f * MathF.Abs(Event.OffsetY));
        MeshCamera.Update();
    }

    protected override void OnMouseDown(MouseButtonEventArgs Event)
    {
        base.OnMouseDown(Event);
        if (Event.Button == MouseButton.Middle)
            MouseHeld = true;
    }

    protected override void OnMouseUp(MouseButtonEventArgs Event)
    {
        base.OnMouseUp(Event);
        if (Event.Button == MouseButton.Middle)
            MouseHeld = false;
    }

    protected override void OnMouseMove(MouseMoveEventArgs Event)
    {
        base.OnMouseMove(Event);
        if (MouseHeld && ImGui.GetIO().KeysDown[(char)Keys.LeftShift]) 
        {
            MeshCamera.PanBy(Event.Delta * 0.75f);
            MeshCamera.Update();
        }
        else if (MouseHeld)
        {
            MeshCamera.RotateBy(Event.Delta * 0.25f); 
            MeshCamera.Update();
        }
    }

    protected override void OnClosing(CancelEventArgs Event)
    {
        base.OnClosing(Event);
        if (!UI.Modals.UnsavedChangesModal.dontShowThisAgain)
        {
            Event.Cancel = true;
            UIHandler.openModals = UIHandler.OpenModals.UnsavedChanges;
        }
    }

    protected override void OnFileDrop(FileDropEventArgs Event)
    {
        base.OnFileDrop(Event);

        Mesh = MeshParser.ParseMeshFile(Event.FileNames[0], 1);
        Log.Verbose("(Window @ OnFileDrop) <{Thread}> Drag & Dropped following files (1st file tried to import):\n{@file_names}", Thread.CurrentThread.Name, Event.FileNames);
    }

}

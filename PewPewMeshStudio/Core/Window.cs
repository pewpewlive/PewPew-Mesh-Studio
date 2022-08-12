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
using PewPewMeshStudio.UI.Modals;
using PewPewMeshStudio.UI.Popups;
using PewPewMeshStudio.UI.Windows;
using PewPewMeshStudio.ExtraUtils;
using System.Runtime.InteropServices;
using Serilog;

namespace PewPewMeshStudio.Core;

public class Window : GameWindow
{   
    private const int WINDOW_WIDTH = 800;
    private const int WINDOW_HEIGHT = 600;

    ImGuiController UIController;

    GlobalDockspace globalDockspace = new GlobalDockspace();
    InspectorWindow inspectorWindow = new InspectorWindow();
    ToolsWindow toolsWindow = new ToolsWindow();
    FileDialogModal fileDialogModal = new FileDialogModal();
    GlobalMenu globalMenu = new GlobalMenu();
    ContextMenu contextMenu = new ContextMenu();
    ErrorModal errorModal = new ErrorModal();
    AboutModal aboutModal = new AboutModal();
    UnsavedChangesModal unsavedChangesModal = new UnsavedChangesModal();
    PreferencesModal preferencesModal = new PreferencesModal();
    public string lastAction = "Last Action: Not Applicable";

    GCHandle FontPtr = GCHandle.Alloc(Properties.Resources.Font, GCHandleType.Pinned);

    Renderable Mesh;
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

        Mesh = MeshParser.ParseMeshFile("mesh.lua", 1);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        Mesh.Destroy();
        FontPtr.Free();
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        Log.Information("(Window) GUI loaded successfully.");
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

        ImGuiStylePtr style = ImGui.GetStyle();
        style.FrameRounding = 3;
        style.WindowRounding = 3;
        style.ChildRounding = 3;
        style.ScrollbarRounding = 12;
        style.TabRounding = 3;
        style.GrabRounding = 3;
        style.PopupRounding = 3;

        //RangeAccessor<System.Numerics.Vector4> colors = style.Colors;
        //colors[0] = ColorUtil.Vec4IntToFloat(new System.Numerics.Vector4(255, 0, 255, 255));

        globalDockspace.Initialize();
        Mesh.Render((Vector2)ClientSize, MeshCamera);

        ImGui.ShowDemoWindow();

        track.Track();

        globalMenu.Initialize();
        contextMenu.Initialize();

        inspectorWindow.Initialize();
        toolsWindow.Initialize();

        //uchangesPopup.Initialize();

        if (globalMenu.OpenErrorDialog)
        {
            errorModal.open = true;
            errorModal.Initialize(ref globalMenu.OpenErrorDialog);
        }
        if (globalMenu.OpenFileDialog)
        {
            fileDialogModal.open = true;
            fileDialogModal.Initialize(ref globalMenu.OpenFileDialog, globalMenu.fileDialogType);
        }
        if (globalMenu.OpenAboutDialog)
        {
            aboutModal.open = true;
            aboutModal.Initialize(ref globalMenu.OpenAboutDialog);
        }
        if (globalMenu.OpenPrefsDialog)
        {
            preferencesModal.open = true;
            preferencesModal.Initialize(ref globalMenu.OpenPrefsDialog);
        }
        /* Black Background Toggle (Unused)
        if (prefsPopup.blackBg)
        {
            GL.ClearColor(new Color4(0, 0, 0, 235));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
        }
        */
        UIController.Render();

        ImGuiController.CheckGLError("End of frame");

        SwapBuffers();
    }

    protected override void OnTextInput(TextInputEventArgs Event)
    {
        base.OnTextInput(Event);
        UIController.PressChar((char)Event.Unicode);
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
}

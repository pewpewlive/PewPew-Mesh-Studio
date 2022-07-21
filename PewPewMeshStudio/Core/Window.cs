using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using PewPewMeshStudio.UI;

namespace PewPewMeshStudio.Core;

public class Window : GameWindow
{   
    private const int WINDOW_WIDTH = 800;
    private const int WINDOW_HEIGHT = 600;

    ImGuiController UIController;

    InspectorTAB inspectorTAB = new InspectorTAB();
    ToolsTAB toolsTAB = new ToolsTAB();
    FileDialogTAB filedTab = new FileDialogTAB();
    ProgramMenu progMenu = new ProgramMenu();
    ProgramPOPUPS progPops = new ProgramPOPUPS();

    public Window() : base(GameWindowSettings.Default, new NativeWindowSettings()
    {
        Size = new Vector2i(WINDOW_WIDTH, WINDOW_HEIGHT),
        APIVersion = new Version(3, 3),
        Title = "PewPew Mesh Studio"
    })
    {
        VSync = VSyncMode.On;
        UIController = new(WINDOW_WIDTH, WINDOW_HEIGHT);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        Console.WriteLine("Welcome to PewPewMeshStudio!");
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

        GL.ClearColor(new Color4(0, 32, 90, 235));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

        ImGui.ShowDemoWindow();

        progMenu.Initialize();
        progPops.Initialize();

        inspectorTAB.Initialize();
        toolsTAB.Initialize();

        if (progMenu.openFileDialog)
        {
            filedTab.open = true;
            filedTab.Initialize(ref progMenu.openFileDialog);
        }

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
    }
}

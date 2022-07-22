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

    InspectorTab inspectorTab = new InspectorTab();
    ToolsTab toolsTab = new ToolsTab();
    FileDialogTab filedTab = new FileDialogTab();
    ProgramMenu progMenu = new ProgramMenu();
    ContextMenu ctxMenu = new ContextMenu();
    ErrorPopup errorPopup = new ErrorPopup();
    AboutPopup aboutPopup = new AboutPopup();

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
        ctxMenu.Initialize();

        inspectorTab.Initialize();
        toolsTab.Initialize();

        if (progMenu.OpenErrorDialog)
        {
            errorPopup.open = true;
            errorPopup.Initialize(ref progMenu.OpenErrorDialog);
        }
        if (progMenu.OpenFileDialog)
        {
            filedTab.open = true;
            filedTab.Initialize(ref progMenu.OpenFileDialog);
        }
        if (progMenu.OpenAboutDialog)
        {
            aboutPopup.open = true;
            aboutPopup.Initialize(ref progMenu.OpenAboutDialog);
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

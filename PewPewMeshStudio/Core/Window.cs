using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
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
    UnsavedChangesPopup uchangesPopup = new UnsavedChangesPopup();
    PreferencesPopup prefsPopup = new PreferencesPopup();

    public string lastAction = "Last Action: Not Applicable";

    public Window() : base(GameWindowSettings.Default, new NativeWindowSettings()
    {
        Size = new Vector2i(WINDOW_WIDTH, WINDOW_HEIGHT),
        APIVersion = new Version(4, 1),
        Title = "PewPew Mesh Studio"
    })
    {
        VSync = VSyncMode.On;
        UIController = new(WINDOW_WIDTH, WINDOW_HEIGHT);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("[Info]: Window -> GUI has loaded successfully.");
        Console.ResetColor();
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

        ImGuiStylePtr style = ImGui.GetStyle();
        style.FrameRounding = 2;

        //RangeAccessor<System.Numerics.Vector4> colors = style.Colors;
        //colors[0] = ColorUtil.Vec4IntToFloat(new System.Numerics.Vector4(255, 0, 255, 255));

        ImGui.ShowDemoWindow();

        progMenu.Initialize();
        ctxMenu.Initialize();

        inspectorTab.Initialize();
        toolsTab.Initialize();

        //uchangesPopup.Initialize();

        if (progMenu.OpenErrorDialog)
        {
            errorPopup.open = true;
            errorPopup.Initialize(ref progMenu.OpenErrorDialog);
        }
        if (progMenu.OpenFileDialog)
        {
            filedTab.open = true;
            filedTab.Initialize(ref progMenu.OpenFileDialog, progMenu.fileDialogType);
        }
        if (progMenu.OpenAboutDialog)
        {
            aboutPopup.open = true;
            aboutPopup.Initialize(ref progMenu.OpenAboutDialog);
        }
        if (progMenu.OpenPrefsDialog)
        {
            prefsPopup.open = true;
            prefsPopup.Initialize(ref progMenu.OpenPrefsDialog);
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
    }
}

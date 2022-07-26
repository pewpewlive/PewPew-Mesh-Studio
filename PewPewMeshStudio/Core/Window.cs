using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using PewPewMeshStudio.UI;
using PewPewMeshStudio.UI.Modals;
using PewPewMeshStudio.UI.Popups;
using PewPewMeshStudio.UI.Windows;

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
    ProgramMenu programMenu = new ProgramMenu();
    ContextMenu contextMenu = new ContextMenu();
    ErrorModal errorModal = new ErrorModal();
    AboutModal aboutModal = new AboutModal();
    UnsavedChangesModal unsavedChangesModal = new UnsavedChangesModal();
    PreferencesModal preferencesModal = new PreferencesModal();
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
        style.WindowRounding = 2;

        //RangeAccessor<System.Numerics.Vector4> colors = style.Colors;
        //colors[0] = ColorUtil.Vec4IntToFloat(new System.Numerics.Vector4(255, 0, 255, 255));

        globalDockspace.Initialize();

        ImGui.ShowDemoWindow();

        programMenu.Initialize();
        contextMenu.Initialize();

        inspectorWindow.Initialize();
        toolsWindow.Initialize();

        //uchangesPopup.Initialize();

        if (programMenu.OpenErrorDialog)
        {
            errorModal.open = true;
            errorModal.Initialize(ref programMenu.OpenErrorDialog);
        }
        if (programMenu.OpenFileDialog)
        {
            fileDialogModal.open = true;
            fileDialogModal.Initialize(ref programMenu.OpenFileDialog, programMenu.fileDialogType);
        }
        if (programMenu.OpenAboutDialog)
        {
            aboutModal.open = true;
            aboutModal.Initialize(ref programMenu.OpenAboutDialog);
        }
        if (programMenu.OpenPrefsDialog)
        {
            preferencesModal.open = true;
            preferencesModal.Initialize(ref programMenu.OpenPrefsDialog);
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

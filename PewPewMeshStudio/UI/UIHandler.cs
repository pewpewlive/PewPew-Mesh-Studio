using ImGuiNET;
using PewPewMeshStudio.UI.Globals;
using PewPewMeshStudio.UI.Modals;
using PewPewMeshStudio.UI.Popups;
using PewPewMeshStudio.UI.Windows;

namespace PewPewMeshStudio.UI;

public class UIHandler
{
    // Globals
    GlobalDockspace globalDockspace = new GlobalDockspace();
    GlobalMenu globalMenu = new GlobalMenu();

    // Modals
    public enum OpenModals
    {
        None,
        About,
        Error,
        FileDialog,
        Preferences,
        UnsavedChanges
    }
    public static OpenModals openModals = OpenModals.None;

    AboutModal aboutModal = new AboutModal();
    ErrorModal errorModal = new ErrorModal();
    FileDialogModal fileDialogModal = new FileDialogModal();
    PreferencesModal preferencesModal = new PreferencesModal();
    UnsavedChangesModal unsavedChangesModal = new UnsavedChangesModal();

    // Popups
    ContextMenu contextMenu = new ContextMenu();

    // Windows
    InspectorWindow inspectorWindow = new InspectorWindow();
    ToolsWindow toolsWindow = new ToolsWindow();

    // Other
    

    //public static Action OnGlobalInit;
    //public static Action OnModalInit;
    //public static Action OnPopupInit;
    //public static Action OnWindowInit;
    //OnGlobalInit?.Invoke();
    //if (openModals != OpenModals.None)
    //    OnModalInit?.Invoke();
    //OnPopupInit?.Invoke();
    //OnWindowInit?.Invoke();

    public void InitUI()
    {
        ImGuiStylePtr style = ImGui.GetStyle();
        style.FrameRounding = 3;
        style.WindowRounding = 3;
        style.ChildRounding = 3;
        style.ScrollbarRounding = 12;
        style.TabRounding = 3;
        style.GrabRounding = 3;
        style.PopupRounding = 3;

        globalDockspace.Initialize();

        ImGui.ShowDemoWindow();
        ImGui.ShowMetricsWindow();

        globalMenu.Initialize();

        if (openModals == OpenModals.About) aboutModal.Initialize();
        else if (openModals == OpenModals.Error) errorModal.Initialize();
        else if (openModals == OpenModals.FileDialog)  fileDialogModal.Initialize(globalMenu.fileDialogType);
        else if (openModals == OpenModals.Preferences) preferencesModal.Initialize();
        else if (openModals == OpenModals.UnsavedChanges) unsavedChangesModal.Initialize(); 

        contextMenu.Initialize();

        inspectorWindow.Initialize();
        toolsWindow.Initialize();
    }
}

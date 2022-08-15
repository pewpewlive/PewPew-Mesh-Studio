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

    //public static Action OnGlobalInit;
    //public static Action OnModalInit;
    //public static Action OnPopupInit;
    //public static Action OnWindowInit;

    public void InitUI()
    {
        //OnGlobalInit?.Invoke();
        //if (openModals != OpenModals.None)
        //    OnModalInit?.Invoke();
        //OnPopupInit?.Invoke();
        //OnWindowInit?.Invoke();
        ImGui.ShowDemoWindow();
        ImGui.ShowMetricsWindow();

        globalDockspace.Initialize();
        globalMenu.Initialize();

        contextMenu.Initialize();

        inspectorWindow.Initialize();
        toolsWindow.Initialize();

        if (openModals == OpenModals.About) aboutModal.Initialize();
        else if (openModals == OpenModals.Error) errorModal.Initialize();
        else if (openModals == OpenModals.FileDialog)  fileDialogModal.Initialize(globalMenu.fileDialogType);
        else if (openModals == OpenModals.Preferences) preferencesModal.Initialize();
        else if (openModals == OpenModals.UnsavedChanges) unsavedChangesModal.Initialize(); 
    }
}

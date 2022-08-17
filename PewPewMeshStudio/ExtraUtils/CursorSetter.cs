using OpenTK.Windowing.GraphicsLibraryFramework;
//using PewPewMeshStudio.Core;

namespace PewPewMeshStudio.ExtraUtils;

public static unsafe class CursorSetter
{
    public static Window* WindowPointer { private get; set; }
    public static void SetCursorNoProtection(CursorShape shape)
    {
        GLFW.SetCursor(WindowPointer, GLFW.CreateStandardCursor(shape));
    }
    public static void ResetCursorNoProtection(CursorShape shape)
    {
        GLFW.SetCursor(WindowPointer, GLFW.CreateStandardCursor(CursorShape.Arrow));
    }
    // == Incomplete ==
    private static bool locked = false;
    public static void SetCursor(CursorShape shape)
    {
        if (!locked)
        {
            GLFW.SetCursor(WindowPointer, GLFW.CreateStandardCursor(shape));
            locked = true;
        }
    }
    public static void ResetCursor()
    {
        GLFW.SetCursor(WindowPointer, GLFW.CreateStandardCursor(CursorShape.Arrow));
        locked = false;
    }
}

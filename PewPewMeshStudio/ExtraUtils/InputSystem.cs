using ImGuiNET;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;

namespace PewPewMeshStudio.ExtraUtils;

public class InputSystem
{
    public static List<int> inputOrder { get; private set; } = new List<int>();

    public void Track()
    {
        GetOrder();
    }

    private void GetOrder()
    {
        ImGuiIOPtr IO = ImGui.GetIO();

        for (int i = 0; i < IO.KeysDown.Count; i++)
        {
            if (IO.KeysDown[i] == true && !inputOrder.Contains(i))
                inputOrder.Add(i);

            else if(IO.KeysDown[i] == false && inputOrder.Contains(i))
                inputOrder.Remove(i);
        }

        //foreach (int key in inputOrder)
        //{
        //    Console.WriteLine((Keys)key);
        //}
    }

    public static bool KeyPressed(Keys key)
    {
        if (ImGui.GetIO().KeysDown[(int)key] && inputOrder.Count < 2)
            return true;

        return false;
    }

    public static bool HotkeyPressed(Keys[] keys)
    {
        ImGuiIOPtr IO = ImGui.GetIO();

        foreach (Keys key in keys)
        {
            if (IO.KeysDown[(int)key])
                continue;
            else
                return false;
        }

        if (CheckOrder(keys))
            return true;

        return false;
    }

    private static bool CheckOrder(Keys[] keys)
    {
        ImGuiIOPtr IO = ImGui.GetIO();

        for (int i = 0; i < keys.Length; i++)
        {
            if (inputOrder[i] == (int)keys[i])
                continue;
            else
                return false;
        }

        return true;
    }
}
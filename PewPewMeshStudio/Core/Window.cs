﻿using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace PewPewMeshStudio.Core
{
    public class Window : GameWindow
    {   
        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        ImGuiController UIController;

        #pragma warning disable CS8618
        public Window() : base(GameWindowSettings.Default, new NativeWindowSettings()
        #pragma warning restore CS8618
        {
            Size = new Vector2i(WIDTH, HEIGHT),
            APIVersion = new Version(3, 3),
            Title = "PewPew Mesh Studio"
        })
        { }

        protected override void OnLoad()
        {
            base.OnLoad();
            Console.WriteLine("Welcome to PewPewMeshStudio!");
            UIController = new(WIDTH, HEIGHT);
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

            //ImGui.ShowDemoWindow();
            
            ImGui.Begin("Testing");
            if (ImGui.Button("Press me!"))
                Console.WriteLine("I was pressed!");
            ImGui.End();
            
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
}
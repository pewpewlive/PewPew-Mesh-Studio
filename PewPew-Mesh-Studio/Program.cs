using SFML.Graphics;
using SFML.Window;
using SFML.System;

Console.WriteLine("Hello, JF!");

using(RenderWindow RendererWindow = new RenderWindow(new VideoMode(800, 600), "Mesh renderer", Styles.Titlebar | Styles.Close))
{
    RendererWindow.SetFramerateLimit(60);
    RendererWindow.Closed += (s,e) => RendererWindow.Close();

    while (RendererWindow.IsOpen) 
    {
        RendererWindow.DispatchEvents();
        RendererWindow.Clear(new Color(0x1e1e1eff));
        RendererWindow.Draw(new CircleShape(45.0f));
        RendererWindow.Display();
    }
}
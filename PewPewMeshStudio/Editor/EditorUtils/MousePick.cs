using OpenTK.Mathematics;
using OpenTK.Windowing.Common.Input;
using PewPewMeshStudio.Renderer;

namespace PewPewMeshStudio.Editor.EditorUtils;

class MousePick
{
    private Vector3 currentRay;

    private Matrix4 projectionMatrix;
    private Matrix4 viewMatrix;
    private Camera camera;

    private float mouseX, mouseY;
    private float windowX, windowY;

    public MousePick(Camera cam, Matrix4 projection)
    {
        camera = cam;
        projectionMatrix = projection;
    }

    public Vector3 GetCurrentRay() => currentRay;

    public void Update(Vector2 mousePos, Vector2 windowWidth)
    {
        mouseX = mousePos.X;
        mouseY = mousePos.Y;

        windowX = windowWidth.X;
        windowY = windowWidth.Y;

        viewMatrix = camera.GetCameraView().ClearProjection();
        currentRay = CalculateMouseRay(mousePos);

        //Console.WriteLine($"{currentRay.X}, {currentRay.Y}, {currentRay.Z}");
    }

    private Vector3 CalculateMouseRay(Vector2 mousePos)
    {
        //Console.WriteLine($"{mouseX}, {mouseY}");

        Vector2 normalizedCoords = GetNormalizedDeviceCoords();
        Vector4 clipCoords = new Vector4(normalizedCoords.X, normalizedCoords.Y, -1f, 1f);
        Vector4 eyeCoords = ToEyeCoords(clipCoords);
        Vector3 worldRay = ToWorldCoords(eyeCoords);

        return worldRay;
    }

    private Vector4 ToEyeCoords(Vector4 clipCoords)
    {
        Vector4 eyeCoords = clipCoords * projectionMatrix.Inverted();
        return new Vector4(eyeCoords.X, eyeCoords.Y, -1f, 1f);
    }

    private Vector3 ToWorldCoords(Vector4 eyeCoords)
    {
        Vector4 rayWorld = eyeCoords * viewMatrix.Inverted();
        Vector3 mouseRay = new Vector3(rayWorld.X, rayWorld.Y, rayWorld.Z);
        return mouseRay.Normalized();
    } //return (eyeCoord * viewMatrix.Inverted()).Xyz.Normalized();

    private Vector2 GetNormalizedDeviceCoords()
    {
        float x = (2f * mouseX) / windowX - 1f;
        float y = 1f - (2f * mouseY) / windowY;

        //Console.WriteLine($"{x}, {-y}");

        return new Vector2(x, y);
    }
}

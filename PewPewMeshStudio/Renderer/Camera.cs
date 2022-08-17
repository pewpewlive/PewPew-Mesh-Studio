using OpenTK.Mathematics;
using PewPewMeshStudio.LuaUtils;

namespace PewPewMeshStudio.Renderer;

public class Camera
{
    private Vector3 CameraPosition;
    private Vector3 LookPosition;
    private Vector3 CameraAngle;
    private Matrix4 CameraView;
    private float CameraZoomRadius;

    public Camera()
    {
        CameraZoomRadius = 250.0f;
        CameraAngle = new Vector3(MathHelper.DegreesToRadians(90.0f), MathHelper.DegreesToRadians(89.0f), 0.0f);
        CameraPosition = new Vector3(MathF.Cos(CameraAngle.X) * MathF.Cos(CameraAngle.Y) * CameraZoomRadius,
                                     MathF.Sin(CameraAngle.Y) * CameraZoomRadius,
                                     MathF.Sin(CameraAngle.X) * MathF.Cos(CameraAngle.Y) * CameraZoomRadius);
        LookPosition = Vector3.Zero;
        CameraView = Matrix4.LookAt(CameraPosition, LookPosition, Vector3.UnitY);
    }

    public void Reset()
    {
        CameraZoomRadius = 250.0f;
        CameraAngle = new Vector3(MathHelper.DegreesToRadians(90.0f), MathHelper.DegreesToRadians(89.0f), 0.0f);
        CameraPosition = new Vector3(MathF.Cos(CameraAngle.X) * MathF.Cos(CameraAngle.Y) * CameraZoomRadius,
                                     MathF.Sin(CameraAngle.Y) * CameraZoomRadius,
                                     MathF.Sin(CameraAngle.X) * MathF.Cos(CameraAngle.Y) * CameraZoomRadius);
        LookPosition = Vector3.Zero;
        CameraView = Matrix4.LookAt(CameraPosition, LookPosition, Vector3.UnitY);
    }

    public void Update()
    {
        CameraPosition = LookPosition + new Vector3(MathF.Cos(CameraAngle.X) * MathF.Cos(CameraAngle.Y) * CameraZoomRadius,
                                                    MathF.Sin(CameraAngle.Y) * CameraZoomRadius,
                                                    MathF.Sin(CameraAngle.X) * MathF.Cos(CameraAngle.Y) * CameraZoomRadius);
        CameraView = Matrix4.LookAt(CameraPosition, LookPosition, Vector3.UnitY);
    }

    public Matrix4 GetCameraView() => CameraView;

    public Vector3 GetViewVector() => Vector3.Normalize(LookPosition - CameraPosition);

    public void RotateBy(Vector2 Offset)
    {
        CameraAngle += new Vector3(MathHelper.DegreesToRadians(Offset.X), MathHelper.DegreesToRadians(Offset.Y), 0.0f);
        if (MathHelper.RadiansToDegrees(CameraAngle.Y) > 89.0f)
            CameraAngle.Y = MathHelper.DegreesToRadians(89.0f);
        if (MathHelper.RadiansToDegrees(CameraAngle.Y) < -89.0f)
            CameraAngle.Y = MathHelper.DegreesToRadians(-89.0f);
    }

    public void ZoomBy(float Offset)
    {
        if (CameraZoomRadius + Offset > 10.0f && CameraZoomRadius + Offset < 7000.0f)
            CameraZoomRadius += Offset;
    }

    public void PanBy(Vector2 Offset)
    {
        Vector3 CameraOffsetX = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, Vector3.Normalize(CameraPosition - LookPosition)));
        Vector3 CameraOffsetY = Vector3.Cross(Vector3.Normalize(CameraPosition - LookPosition), CameraOffsetX);
        LookPosition += CameraOffsetX * -Offset.X + CameraOffsetY * Offset.Y;
        CameraPosition += CameraOffsetX * -Offset.X + CameraOffsetY * Offset.Y;
    }
}
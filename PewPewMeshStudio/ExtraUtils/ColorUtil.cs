using System.Numerics;

namespace PewPewMeshStudio.ExtraUtils;

public class ColorUtil
{
    public static Vector4 Vec4ByteToFloat(Vector4 Vec4Int)
    {
        return new Vector4(Vec4Int.X / 255.0f, Vec4Int.Y / 255.0f, Vec4Int.Z / 255.0f, Vec4Int.W / 255.0f);
    }
    public static Vector4 LongToVector4(long Color)
    {
        return new Vector4(
            (Color >> 24) & 255,
            (Color >> 16) & 255,
            (Color >> 8) & 255,
             Color & 255
        );
    }
}

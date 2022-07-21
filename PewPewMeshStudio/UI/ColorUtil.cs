using System.Numerics;

namespace PewPewMeshStudio.UI
{
    public class ColorUtil
    {
        public static Vector4 Vec4IntToFloat(Vector4 Vec4Int)
        {
            return new Vector4(Vec4Int.X / 255.0f, Vec4Int.Y / 255.0f, Vec4Int.Z / 255.0f, Vec4Int.W / 255.0f);
        }
    }
}

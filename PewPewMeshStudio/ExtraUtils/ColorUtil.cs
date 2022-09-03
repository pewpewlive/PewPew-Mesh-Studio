﻿using System.Numerics;

namespace PewPewMeshStudio.ExtraUtils;

public static class ColorUtil
{
    /// <summary>
    /// Converts Vector4 containing bytes to Vector4 containing floats between 0 and 1.
    /// </summary>
    /// <param name="Vec4Int">Vector4 containing bytes.</param>
    public static Vector4 Vec4ByteToFloat(Vector4 vec4Int)
        => new Vector4(vec4Int.X / 255.0f, vec4Int.Y / 255.0f, vec4Int.Z / 255.0f, vec4Int.W / 255.0f);
    public static Vector4 FloatToVec4Byte(Vector4 vec4Float)
        => new Vector4(vec4Float.X * 255.0f, vec4Float.Y * 255.0f, vec4Float.Z * 255.0f, vec4Float.W * 255.0f);

    /// <summary>
    /// Converts longs (or hex numbers, like 0xff0ff0ff) to Vector4 containing bytes.
    /// </summary>
    /// <param name="Color">Longs (or hex numbers, like 0xff0ff0ff).</param>
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

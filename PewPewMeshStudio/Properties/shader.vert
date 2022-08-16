#version 410 core

layout (location = 0) in vec3 lPrevPos;
layout (location = 1) in vec3 lCurrPos;
layout (location = 2) in vec3 lNextPos;
layout (location = 3) in vec4 lVertCol; 
layout (location = 4) in float lVertSg;
  
out vec4 VertexColor;

uniform mat4 uMVP;
uniform vec2 uScreenSize;

void main()
{
    vec4 PrevPosProj = vec4(lPrevPos, 1.0) * uMVP;
    vec4 CurrPosProj = vec4(lCurrPos, 1.0) * uMVP;
    vec4 NextPosProj = vec4(lNextPos, 1.0) * uMVP;

    vec2 PrevPosScrn = (PrevPosProj.xy / PrevPosProj.w) * uScreenSize;
    vec2 CurrPosScrn = (CurrPosProj.xy / CurrPosProj.w) * uScreenSize;
    vec2 NextPosScrn = (NextPosProj.xy / NextPosProj.w) * uScreenSize;

    vec2 AB = normalize(CurrPosScrn - PrevPosScrn);
    vec2 BC = normalize(NextPosScrn - CurrPosScrn);

    vec2 Tangent = normalize(AB + BC);
    vec2 Miter = vec2(-Tangent.y, Tangent.x);
    vec2 Normal = vec2(-AB.y, AB.x);

    float Length = 5.0 / dot(Miter, Normal);

    gl_Position = vec4((CurrPosScrn + (Miter * Length * lVertSg)) / uScreenSize, CurrPosProj.z / CurrPosProj.w, 1.0);
    VertexColor = lVertCol;
}
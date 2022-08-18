#version 410 core

layout (location = 0) in vec3 lPrevPos;
layout (location = 1) in vec3 lCurrPos;
layout (location = 2) in vec3 lNextPos;
layout (location = 3) in vec4 lVertCol; 
layout (location = 4) in float lMitLen;
  
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

    vec2 Miter = vec2(-normalize(AB + BC).y, normalize(AB + BC).x);

    gl_Position = vec4((CurrPosScrn + (Miter * lMitLen * 5.0)) / uScreenSize, CurrPosProj.z / CurrPosProj.w, 1.0);
    VertexColor = lVertCol;
}

//#version 410 core
//
//layout (location = 0) in vec3 lPrevPos;
//layout (location = 1) in vec3 lCurrPos;
//layout (location = 2) in vec3 lNextPos;
//layout (location = 3) in vec4 lVertCol; 
//layout (location = 4) in float lVertSg;
//  
//out vec4 VertexColor;
//
//uniform mat4 uMVP;
//uniform mat3 uW;
//uniform mat3 uWSquared;
//uniform vec2 uScreenSize;
//uniform vec3 uViewVector;
//
//float tau = 6.283185307179586;
//float pi = 3.14159265359;
//
//void main()
//{
//    vec3 start = normalize(lNextPos - lCurrPos);
//    float angle = acos(dot(normalize(lPrevPos - lCurrPos), normalize(lNextPos - lCurrPos))) / 2;
//
//    if (lVertSg == 1.0) {
//        angle = angle - pi;
//    }
//    
//    mat3 rotation_matrix = mat3(1.0) + (sin(angle) * uW) + (2.0 * pow(sin(angle / 2.0), 2.0) * uWSquared);
//
//    vec4 pos = vec4(start * rotation_matrix + lCurrPos, 1.0) * uMVP;
//
//    gl_Position = vec4(pos);
//    VertexColor = lVertCol;
//}
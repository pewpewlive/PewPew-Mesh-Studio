#version 410 core

layout (location = 0) in vec3 lCurrPos;
layout (location = 1) in vec4 lVertCol; 
  
out vec4 VertexColor;

uniform mat4 uMVP;
uniform vec2 uScreenSize;

void main()
{
    vec4 CurrPosProj = vec4(lCurrPos, 1.0) * uMVP;
    gl_Position = CurrPosProj;
    VertexColor = lVertCol;
}
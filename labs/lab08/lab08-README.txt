B581/Fall 2022		Lab 08		Duozhao Wang		duozwang


Note: Since lab 08 is a part of problem set 03, I will upload my work in the problem-set directory.
The link for this lab task is: https://github.iu.edu/B481-B581-Fall2022/B581duozwang/tree/main/problem-sets/ps03/Problem-Set-03-duozwang

For this lab task, I followed the lab's instructions, and finished Task A step 1 and step 2, as required by the lab task.
I did not copy the original problem set (problem set 02). Instead, I started a new project, and do the same steps as what I did in problem set 02. 
However, I made changes in problem set 03, as required by the requirements of problem set 03.



Q&A for today's lab:
1. What kind of data structure is used by Unity to pass information to a vertex shader about the individual vertex to be processed by the vertex shader?
What is the name of that data structure, what information does that data represent, and what data type(s) does it use?
Answer:
Unity uses a struct called appdata to pass information to a vertex shader about the individual vertex to be processed by the vertex shader.
There are two information which the data contains, one is vertex position, the other one is first texture coordinate.
For the vertex position, it uses a float4 vector. It uses float values.
For the first texture coordinate, it uses a float2 vector. It uses float values.



2. When both a vertex shader and a fragment shader are used in Unity, how does information get passed from the vertex shader's output, as input to the 
fragment shader: is there a data structure involved?
What is the name of that data structure, what information does that data represent, and what data type(s) may it use?
Answer:
When information get passed from the vertex shader's output as input to the fragment shader, the data will be interpolated. In HLSL, normally they wil be 
labeled with texture coordinate. There is also a struct called v2f, which is used during this procedure.
In this data structure, there are two kinds of information. One is called world reflection, it uses a half3 vector, and it uses half values. The other one 
is position, it uses a float4 vector, and it uses float values.
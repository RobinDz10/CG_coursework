B581/Fall 2022	Lab 01	Duozhao Wang	duozwang

The organization of my IU GitHub repository for B581:
In my repository, I have two dictionaries, one is called B481-B581-students-repo, the other one is my personal dictionary called B581duozwang.
In the dictionary of B581duozwang, there are basically three subdictionaries, one is homework (hw), one is labs, and one is problem-sets.


For the Shadertoy task, according to the instructions, I modified the default code of fragment shader:
For the normalization step and output step, I didn't make any change on them.
For the second part, I delete iTime, so that pixel color will not be changed as time varying. 
If I want to set the entire graphics context to black by assigning the appropriate vec4 value to the fragColor output parameter:
vec3 col = vec3(0, 0, 0);
If I want to set the entire graphics context to white:
vec3 col = vec3(1, 1, 1);
If I want to set the entire graphics context to blue:
vec3 col = vec3(0, 0, 1);
If I want to set the entire graphics context to green:
vec3 col = vec3(0, 1, 0);
If I want to set the entire graphics context to red:
vec3 col = vec3(1, 0, 0);
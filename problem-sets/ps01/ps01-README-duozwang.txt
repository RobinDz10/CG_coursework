B581/Fall 2022	Problem Set 01	Duozhao Wang	duozwang


In case the project under this folder not work correctly, here is the pwd of the original project of this problem set:
https://github.iu.edu/B481-B581-Fall2022/B581duozwang/tree/main/labs/lab05


Details:

The unity version is: 2020.3.16f1. It is the required by this course.

Scripts:
In my work, I used four C# scripts: SingleSegmentPositionLine, DragObject, LineUtility, PolygonLines.
The first and fourth script is used for initializing transforms and renderers. The first and fourth script are really similar, except I used 
an array for rendering several lines instead of a single line.
The second script is used for drag spirit object.
The Third script is the most important script in this project. It is used for computing point-line distances.

In all these four scripts, I did not modify DragObject and SingleSegmentPositionLines. I implemented my own implementation in LineUtility. I 
also create a new script named PolygonLines in order to finish Task B.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS01 {

    public static class LineUtility {
    
        // DirectionNormal() --- returns the normal to a given direction vector:
        public static Vector2 DirectionNormal(Vector2 direction) {
            // TODO: compute 
            Vector2 normal = new Vector2(-direction[1], direction[0]);
            return normal;
        } // end of DirectionNormal() 

        // LineSegmentNormal() --- returns the normal to a line segment:
        public static Vector2 LineSegmentNormal(Vector2 start, Vector2 end) {
            // TODO: compute 
            Vector2 normal = new Vector2(-(end[1] - start[1]), (end[0] - start[0]));
            return normal;
        } // end of LineSegmentNormal()


        // ClosestPointOnLine() --- returns the closest point on a line to a given query point:
        public static Vector2 ClosestPointOnLine(Vector2 pointOnLine, Vector2 direction, Vector2 point) {
            // TODO: compute 
            //
            // ERRATA CORRIGE:
            //  Vector2 localX = ...  <- incorrect, it should be a float
            //  float localX = ...    <- correct type definition
            Vector2 lineDirectionNormalized = direction;
            lineDirectionNormalized.Normalize(); // v

            Vector2 pointDirection = DirectionNormal(direction);
            Vector2 pointDirectionNormalized = pointDirection;
            pointDirectionNormalized.Normalize(); // n

            float h = pointDirectionNormalized[0] * (point[0] - pointOnLine[0]) + pointDirectionNormalized[1] * (point[1] - pointOnLine[1]);
            float l = lineDirectionNormalized[0] * (point[0] - pointOnLine[0]) + lineDirectionNormalized[1] * (point[1] - pointOnLine[1]);

            Vector2 closestPoint = pointOnLine + l * lineDirectionNormalized;

            return closestPoint;
            //  return ...
            
            //  you may find useful the 2D Point-Line Geometry expressions shown at lecture time
            //  
            
        } // end of ClosestPointOnLine()

        // ClosestPointOnSegment() --- returns the closest point (on a line segment)
        //                             to a given subject point:
        public static Vector2 ClosestPointOnSegment(Vector2 start, Vector2 end, Vector2 point) {
            // TODO: 
            //  you may find the above methods useful, once you complete them...
            Vector2 lineDirection = new Vector2(end[0] - start[0], end[1] - start[1]);
            Vector2 closestPoint = ClosestPointOnLine(start, lineDirection, point);

            Vector2 lineDirectionNormalized = lineDirection;
            lineDirectionNormalized.Normalize(); // v

            Vector2 pointDirection = LineSegmentNormal(start, end);
            Vector2 pointDirectionNormalized = pointDirection;
            pointDirectionNormalized.Normalize(); // n

            float h = pointDirectionNormalized[0] * (point[0] - start[0]) + pointDirectionNormalized[1] * (point[1] - start[1]);
            float l = lineDirectionNormalized[0] * (point[0] - start[0]) + lineDirectionNormalized[1] * (point[1] - start[1]);
            
            float length = Mathf.Sqrt((end[1] - start[1]) * (end[1] - start[1]) + (end[0] - start[0]) * (end[0] - start[0]));

            if(l < 0) return start;
            else if(l > length) return end;
            else return closestPoint;
            
        } // end of ClosestPointOnSegment()

        
/*

        // NOTE: look at the two method implementations below, and 
        //       decide whether you prefer to use:
        //       either the ClosestPointOnPolygon() method using Vector2[] polygonPoints
        //       or the ClosestPointOnPolygon() method using Transform[] polygonVertices,
        //       but not both!

*/

        // ClosestPointOnPolygon() --- returns the closest point (on a polygon)
        //                             to a given subject point.
        //  Note:
        //      the polygon is given as array of transforms
        //      with vertex[n-1] connecting back to vertex[0]
        //
        public static Vector2 ClosestPointOnPolygon(Transform[] polygonVertices, Vector2 point) {
        
            Vector2 result = Vector2.zero;
            float minSqrDistance = float.PositiveInfinity;
            for (int i = 0; i < polygonVertices.Length; i++) {
                int j = (i + 1) % polygonVertices.Length;
                Vector2 side = polygonVertices[j].position - polygonVertices[i].position;
                float sideLength = side.magnitude;
                Vector2 sideDirection = side / sideLength;
                // Vector2 pointToSideDirectionNormal = LineSegmentNormal(polygonVertices[i].position, polygonVertices[j].position);


            // TODO: 
            //  you may find useful the utility methods at the top of this file, once you complete them...

                Vector2 pointOnPolygon = ClosestPointOnSegment(polygonVertices[i].position, polygonVertices[j].position, point);

            //    if (localX < 0) {
            //        pointOnPolygon = ...
            //    } else if (localX > sideLength) {
            //        pointOnPolygon = ...
            //    } else {
            //        pointOnPolygon = ...
            //    }

            // TODO:
            //  the following code works, as long as you computed pointOnPolygon correctly.
            //  It will be useful to understand what the following lines do:
                Vector2 delta = point - pointOnPolygon;
                float sqrDistance = delta.sqrMagnitude;

                if (sqrDistance < minSqrDistance) {
                    result = pointOnPolygon;
                    minSqrDistance = sqrDistance;
                }
            }
            return result;
        } // end of ClosestPointOnPolygon()


    } // end of static class LineUtility

} // end of namespace PS01
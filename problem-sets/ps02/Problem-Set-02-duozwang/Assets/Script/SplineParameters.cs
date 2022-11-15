using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PS02 {

    public static class SplineParameters    {
    
        public enum SplineType { Bezier, CatmullRom, Bspline }

        public static Matrix4x4 GetMatrix(SplineType type) {
        
            switch (type) {
                // TODO: generate Bezier spline matrix,
                //   with constants as per Lecture 11 notes:
                case SplineType.Bezier:
                    return new Matrix4x4( // COLUMN MAJOR!
                        new Vector4( -1, 3, -3, 1 ),
                        new Vector4( 3, -6, 3, 0 ),
                        new Vector4( -3, 3, 0, 0 ),
                        new Vector4( 1, 0, 0, 0 )
                    );
                // TODO: generate Catmull-Rom spline matrix,
                //   with constants as per Lecture 11 notes:
                case SplineType.CatmullRom:
                    float scale1 = 0.5F;
                    return new Matrix4x4( // COLUMN MAJOR!
                        scale1 * new Vector4( -1, 3, -3, 1 ),
                        scale1 * new Vector4( 2, -5, 4, -1 ),
                        scale1 * new Vector4( -1, 0, 1, 0 ),
                        scale1 * new Vector4( 0, 2, 0, 0 )
                    );
                // TODO: generate B-spline matrix,
                //   with constants as per Lecture 11 notes:
                case SplineType.Bspline:
                    float scale2 = 0.16667F;
                    return new Matrix4x4( // COLUMN MAJOR!
                        scale2 * new Vector4( -1, 3, -3, 1 ),
                        scale2 * new Vector4( 3, -6, 3, 0 ),
                        scale2 * new Vector4( -3, 0, 3, 0 ),
                        scale2 * new Vector4( 1, 4, 1, 0 )
                    );
                default:
                    return Matrix4x4.identity;
            }
        } // end of GetMatrix()

        // this could be useful for multi-segment spline curves:
        public static bool UsesConnectedEndpoints(SplineType type) {
            switch (type) {
                case SplineType.Bezier: return true;
                case SplineType.CatmullRom: return false;
                case SplineType.Bspline: return false;
                default: return false;
            }
        } // end of UsesConnectedEndpoints()
        
    } // end of class SplineParameters
    
} // end of namespace PS02
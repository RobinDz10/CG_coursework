using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PS02 {

    public class SplineSegmentCPUCompute : MonoBehaviour {

        // control points for a single spline segment curve:
        [SerializeField] private Transform control0, control1, control2, control3;

        // choice of spline type:
        [SerializeField] private SplineParameters.SplineType splineType;

        // the two line renderers: the control polyline, and the spline curve itself:
        [SerializeField] private LineRenderer controlPolyline, splineCurve;

		// how many points on the spline curve?
		//   (the more points you set, the smoother the curve will be)
        [Range(8, 512)] [SerializeField] private int curvePoints = 16;

        public void SetType(SplineParameters.SplineType type) {
            splineType = type;
        }

        public void UseBezier() => SetType(SplineParameters.SplineType.Bezier);

        public void UseCatmullRom() => SetType(SplineParameters.SplineType.CatmullRom);

        public void UseB() => SetType(SplineParameters.SplineType.Bspline);

        private void Update() {

            // update number of points on spline curve:
            if (splineCurve.positionCount != curvePoints) {
                splineCurve.positionCount = curvePoints;
            }

			// get the correct matrix of spline parameters, from the SplineParameters script:
            // (e.g. for the Bezier spline matrix, as from Lecture 11 notes )
            //
            Matrix4x4 splineMatrix = SplineParameters.GetMatrix(splineType);

			// and now compute the spline curve, point by point:
            for (int i = 0; i < curvePoints; i++) {
                float u = (float)i / (float)(curvePoints - 1);
                
                // you have to define the u vector, a 4-element vector...
                //      (4-element vector defined as in Lecture 11 notes,
                //       albeit in Lecture 11 the parameter is named 't',
                //       and here we're naming the parameter 'u',
                //       following the nomenclature used in the textbook)
                // ...here, but always keep in mind that:
                // Unity stores matrices in the Matrix4x4 data type
                //    (as stated in the online Unity scripting manual)
                // "Matrices in unity are column major."
                // so ...
                // 
                Vector4 uRow = new Vector4(Mathf.Pow(u, 3), Mathf.Pow(u, 2), u, 1);
                 
                Matrix4x4 controlMatrix = new Matrix4x4(
                   new Vector4(control0.position[0], control0.position[1], 1, 1),
                   new Vector4(control1.position[0], control1.position[1], 1, 1),
                   new Vector4(control2.position[0], control2.position[1], 1, 1),
                   new Vector4(control3.position[0], control3.position[1], 1, 1)
                );
                
                // finally, compute the splinePointPosition as from the spline Matrix Form
                //      (defined as in Lecture 11 notes,
                //       albeit in Lecture 11 the parameter is named 't',
                //       and here we're naming the parameter 'u',
                //       following the nomenclature used in the textbook)
                // i.e.
                //  the form p(u) = u * M * p
                //  really should be computed as:  p(u) = p * MB * u
                //  as per assignment instructions!
                //
                Vector4 splinePointPosition = controlMatrix * splineMatrix * uRow;
                
                // once the splinePointPosition is computed,
                //   assign it to the related Position property in the splineCurve object:
                splineCurve.SetPosition(i, (Vector2)splinePointPosition);
                
                // we placed the following line here, so that this script may compile,
                //  but you have to replace the following line
                //  with all that's commented out above!
                // And then remove the following line:
                // splineCurve.SetPosition(i, Vector2.one);
            }

            // set vertices for control polyline:
            controlPolyline.SetPosition(0, control0.position);
            controlPolyline.SetPosition(1, control1.position);
            controlPolyline.SetPosition(2, control2.position);
            controlPolyline.SetPosition(3, control3.position);
        }
    }
} // end of namespace PS02
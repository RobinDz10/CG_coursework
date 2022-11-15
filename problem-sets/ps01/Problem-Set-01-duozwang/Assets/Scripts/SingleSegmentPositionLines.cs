using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS01 {

    public class SingleSegmentPositionLines : MonoBehaviour {

        // fields to connect to Unity objects:
        [SerializeField] private Transform subjectLineStartTransform, subjectLineEndTransform, subjectPointTransform;
        [SerializeField] private LineRenderer subjectLineRenderer, connectingLineRenderer;


        // Update() is called once per frame:
        private void Update() {

            // set positions for subject line vertices:
            subjectLineRenderer.SetPosition(0, subjectLineStartTransform.position);
            subjectLineRenderer.SetPosition(1, subjectLineEndTransform.position);

            // if debug is necessary, uncomment these lines:
            // Debug.Log("subjectLineStartTransform.position = " + subjectLineStartTransform.position);
            // Debug.Log("subjectLineEndTransform.position = " + subjectLineEndTransform.position);
            // Debug.Log("subjectLineRenderer.GetPosition(0) = " + subjectLineRenderer.GetPosition(0));
            // Debug.Log("subjectLineRenderer.GetPosition(1) = " + subjectLineRenderer.GetPosition(1));

            // set positions for connecting line vertices:

            // TODO - uncomment when .ClosestPointOnSegment is implemented:
            Vector2 lClosestPoint = LineUtility.ClosestPointOnSegment(
                subjectLineStartTransform.position,
                subjectLineEndTransform.position,
                subjectPointTransform.position);

            // TODO: remove next line when .ClosestPointOnSegment is implemented and lClosestPoint can be computed:
            // Vector2 lClosestPoint = Vector2.one; // temporarily set to Vector2.one, until we compute lClosestPoint correctly!
            
            connectingLineRenderer.SetPosition(0, subjectPointTransform.position);
            connectingLineRenderer.SetPosition(1, lClosestPoint);
        } // end of Update()

    } // end of class SingleSegmentPositionLines

} // end of namespace PS01
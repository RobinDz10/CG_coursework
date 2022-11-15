using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS01{
    public class PolygonLines : MonoBehaviour
    {
        [SerializeField] private Transform[] points = new Transform[5];
        [SerializeField] private Transform subjectPointTransform;
        [SerializeField] private LineRenderer[] subjectLineRenderer = new LineRenderer[5];
        [SerializeField] private LineRenderer connectingLineRenderer;

    // Update is called once per frame
        void Update()
        {
            for(int i = 0; i < points.Length; i++){
                int j = (i + 1) % points.Length;
                subjectLineRenderer[i].SetPosition(0, points[i].position);
                subjectLineRenderer[i].SetPosition(1, points[j].position);
            }

            Vector2 lClosestPoint = LineUtility.ClosestPointOnPolygon(points, subjectPointTransform.position);
            
            connectingLineRenderer.SetPosition(0, subjectPointTransform.position);
            connectingLineRenderer.SetPosition(1, lClosestPoint);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] Transform pointPrefab;
    [SerializeField, Range(10, 100)] int resolution = 10;
    Transform[] points;
    
    void cubeScale(Transform pointParent, float step){
        Transform point = pointParent.GetChild(0);
        Vector3 rotateAngle1 = new Vector3(0, 0, 45);
        Vector3 rotateAngle2 = new Vector3(54, 0, 0);
        point.Rotate(rotateAngle1);
        point.Rotate(rotateAngle2);
        var scale = new Vector3(step, 3 * step, step);
        pointParent.localScale = scale;
    }

    void Awake(){
        float step = 2f / resolution;
        var position = Vector3.zero;
        // var scale = Vector3.one * step;
        points = new Transform[resolution];
        for(int i = 0; i < points.Length; i++){
            Transform point = points[i] = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            position.y = position.x * position.x * position.x;
            point.localPosition = position;
            // point.localScale = scale;
            cubeScale(point, step);
            point.SetParent(transform, false);
        }
    }
    
    void cubeRotation(Transform point, int i){
        float turnSpeed = 50;
        float posX = point.position.x;
        float posY = point.position.y;
        float posZ = point.position.z;
        point.eulerAngles = new Vector3(10 * i, (posY % 360) * 100, i + 30);
        if(i % 2 == 0)
            point.Rotate(point.eulerAngles, turnSpeed * (i + 5) * Time.deltaTime);
        else
            point.Rotate(-1 * point.eulerAngles, turnSpeed * (i + 10) * Time.deltaTime);
    }

    void Update(){
        float time = Time.time;
        for(int i = 0; i < points.Length; i++){
            Transform point = points[i];
            Vector3 position = point.localPosition;
            // position.y = position.x * position.x * position.x;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            point.localPosition = position;
            
            cubeRotation(point, i);
        }
    }
}

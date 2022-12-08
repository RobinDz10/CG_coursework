using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS04{
    public class ObjectMovement : MonoBehaviour {
        public MeshFilter meshFilter;

        // Start is called before the first frame update
        void Start() {
            meshFilter.mesh = new Mesh();
            Vector3[] vertex = {
                new Vector3(0, 0, 0), // 0
                new Vector3(0, 0, 1), // 1
                new Vector3(1, 0, 1), // 2
                new Vector3(1, 0, 0), // 3

                new Vector3(0, 0, 0),
                new Vector3(0, 0, 1),
                new Vector3(0, 1, 1),
                new Vector3(0, 1, 0),

                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(1, 1, 0),
                new Vector3(1, 0, 0),

                new Vector3(0, 0, 1),
                new Vector3(0, 1, 1),
                new Vector3(1, 1, 1),
                new Vector3(1, 1, 0),

                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1),
                new Vector3(1, 1, 1),
                new Vector3(1, 1, 0),

                new Vector3(0, 0, 1),
                new Vector3(0, 1, 1),
                new Vector3(1, 1, 1),
                new Vector3(1, 1, 0)
            };
            meshFilter.mesh.vertices = vertex;
            int[] nums = {0, 2, 1, 0, 3, 2};
            // Color[] color = {
            //     new Color(255f / 255f, 255f / 255f, 0f / 255f),
            //     new Color(255f / 255f, 0f / 255f, 0f / 255f),
            //     new Color(0f / 255f, 255f / 255f, 255f / 255f),
            //     new Color(255f / 255f, 255f / 255f, 0f / 255f)
            // };
            meshFilter.mesh.triangles = nums;
            // meshFilter.mesh.colors = color;
        }

    // Update is called once per frame
        void Update() {
        
        }
    }
}

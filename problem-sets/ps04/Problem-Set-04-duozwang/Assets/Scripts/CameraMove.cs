using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS04
{
    public class CameraMove : MonoBehaviour
    {
        private Camera mainCamera;

        public Transform cube;
        private Vector3 cameraPos;
        private Vector3 cameraTarget;
        private Vector3 up;
        private Vector3 cameraDirection;
        public Material x_color;
        public Material y_color;
        public Material z_color;
        public Material faceColor;
        public MeshFilter[] meshes;

        public MeshFilter test;

        private Matrix4x4 viewMatrix(Vector3 cameraPos, Vector3 up, Vector3 cameraDirection)
        {
            Vector3 right = Vector3.Cross(up, cameraDirection).normalized;
            Matrix4x4 view = new Matrix4x4(
                new Vector4(right.x, up.x, cameraDirection.x, 0),
                new Vector4(right.y, up.y, cameraDirection.y, 0),
                new Vector4(right.z, up.z, cameraDirection.z, 0),
                new Vector4(-cameraPos.x, -cameraPos.y, -cameraPos.z, 1)
            );
            return view;
        }

        // Start is called before the first frame update
        void Start()
        {
            cameraPos = new Vector3(0.0f, 0.0f, 0.0f);
            cameraTarget = new Vector3(0.0f, 0.0f, 1.0f);
            up = new Vector3(0.0f, 1.0f, 0.0f);
            cameraDirection = Vector3.Normalize(cameraTarget - cameraPos);
            Matrix4x4 view = viewMatrix(cameraPos, up, cameraDirection);
            x_color.SetMatrix("viewMatrix", view);
            y_color.SetMatrix("viewMatrix", view);
            z_color.SetMatrix("viewMatrix", view);
            faceColor.SetMatrix("viewMatrix", view);
            Color[] color = {
                new Color(255f / 255f, 255f / 255f, 0f / 255f),
                new Color(255f / 255f, 255f / 255f, 0f / 255f),
                new Color(255f / 255f, 255f / 255f, 0f / 255f),
                new Color(255f / 255f, 255f / 255f, 0f / 255f),

        
                new Color(255f / 255f, 0f / 255f, 0f / 255f),
                new Color(255f / 255f, 0f / 255f, 0f / 255f),
                new Color(0f / 255f, 255f / 255f, 0f / 255f),
                new Color(0f / 255f, 255f / 255f, 0f / 255f),
                

                new Color(255f / 255f, 0f / 255f, 0f / 255f),
                new Color(255f / 255f, 0f / 255f, 0f / 255f),
                new Color(0f / 255f, 255f / 255f, 0f / 255f),
                new Color(0f / 255f, 255f / 255f, 0f / 255f),
                

                new Color(0f / 255f, 0f / 255f, 255f / 255f),
                new Color(0f / 255f, 0f / 255f, 255f / 255f),
                new Color(0f / 255f, 0f / 255f, 255f / 255f),
                new Color(0f / 255f, 0f / 255f, 255f / 255f),
                new Color(0f / 255f, 255f / 255f, 255f / 255f),
                new Color(0f / 255f, 255f / 255f, 255f / 255f),
                new Color(0f / 255f, 255f / 255f, 255f / 255f),
                new Color(0f / 255f, 255f / 255f, 255f / 255f),
                new Color(255f / 255f, 0f / 255f, 255f / 255f),
                new Color(255f / 255f, 0f / 255f, 255f / 255f),
                new Color(255f / 255f, 0f / 255f, 255f / 255f),
                new Color(255f / 255f, 0f / 255f, 255f / 255f)
            };
            test.mesh.colors = color;
            Matrix4x4 model = Matrix4x4.Translate(new Vector3(0.0f, 0.0f, 10.0f));
            x_color.SetMatrix("modelMatrix", model);
            y_color.SetMatrix("modelMatrix", model);
            z_color.SetMatrix("modelMatrix", model);

            faceColor.SetMatrix("TransMatrix", Matrix4x4.identity);
            faceColor.SetMatrix("rotateMatrix", Matrix4x4.identity);
            faceColor.SetMatrix("scaleMatrix", Matrix4x4.identity);
            // Debug.Log(model);
            // Debug.Log(x_color.GetMatrix("modelMatrix"));
            Debug.Log(test.mesh.vertices.Length);
        }

        public void setMode(int m)
        {
            mode = m;
            // Debug.Log(mode);
        }

        public void setObject(int oBJ)
        {
            obj = oBJ;
        }

        public int obj = 0;
        public int mode = 0;
        
        private Matrix4x4 rotateMatrix = Matrix4x4.identity;
        private Matrix4x4 scaleMatrix = Matrix4x4.identity;
        public void UpdateCamera(Vector2 d)
        {
            if (obj == 0)
            { // Coordinate System
                switch (mode)
                {
                    case 0:
                        cameraPos = new Vector3(cameraPos.x - d.x * 0.1f, cameraPos.y - d.y * 0.1f, cameraPos.z);
                        break;
                    case 1:
                        cameraPos = new Vector3(cameraPos.x - d.x * 0.1f, cameraPos.y, cameraPos.z - d.y * 0.1f);
                        break;
                }
                cameraPos = new Vector3(cameraPos.x - d.x * 0.1f, cameraPos.y - d.y * 0.1f, cameraPos.z);
                Matrix4x4 view = viewMatrix(cameraPos, up, cameraDirection);
                x_color.SetMatrix("viewMatrix", view);
                y_color.SetMatrix("viewMatrix", view);
                z_color.SetMatrix("viewMatrix", view);
                faceColor.SetMatrix("viewMatrix", view);
                // Debug.Log(view);
            }
            else
            {
                Vector4 trans;
                switch (mode)
                {
                    
                    case 0:
                        trans = faceColor.GetMatrix("TransMatrix").GetColumn(3); 
                        faceColor.SetMatrix("TransMatrix", Matrix4x4.Translate(new Vector3(trans.x + d.x * 0.1f, trans.y + d.y * 0.1f, trans.z)));
                        break;
                    case 1:
                        trans = faceColor.GetMatrix("TransMatrix").GetColumn(3); 
                        faceColor.SetMatrix("TransMatrix", Matrix4x4.Translate(new Vector3(trans.x + d.x * 0.1f, trans.y, trans.z + d.y * 0.1f)));
                        break;
                    case 2:
                        Vector2 n1 = new Vector2(-d.y, d.x);
                        float dr = Mathf.Sqrt(d.y * d.y + d.x * d.x);
                        if (dr <= 0)
                            break;
                        Vector3 n2 = new Vector3(-d.y / dr, d.x / dr, 0);
                        float proj1 = Mathf.Cos(-dr * Mathf.Deg2Rad);
                        float proj2 = Mathf.Sin(-dr * Mathf.Deg2Rad);
             
                        Matrix4x4 newRotate = new Matrix4x4(
                            new Vector4(proj1 + n2.x * n2.x * (1 - proj1), n2.y * n2.x * (1 - proj1) + n2.z * proj2, n2.z * n2.x * (1 - proj1) - n2.y * proj2, 0),
                            new Vector4(n2.x * n2.y * (1 - proj1) - n2.z * proj2, proj1 + n2.y * n2.y * (1 - proj1), n2.z * n2.y * (1 - proj1) + n2.x * proj2, 0),
                            new Vector4(n2.x * n2.z * (1 - proj1) + n2.y * proj2, n2.y * n2.z * (1 - proj1) - n2.x * proj2, proj1 + n2.z * n2.x * (1 - proj1), 0),
                            new Vector4(0, 0, 0, 1)

                            // new Vector4(proj1 + n2.x * n2.x * (1 - proj1), n2.x * n2.y * (1 - proj1) - n2.z * proj2, n2.x * n2.z * (1 - proj1) + n2.y * proj2, 0),
                            // new Vector4(n2.y * n2.x * (1 - proj1) + n2.z * proj2, proj1 + n2.y * n2.y * (1 - proj1), n2.y * n2.z * (1 - proj1) - n2.x * proj2, 0),
                            // new Vector4(n2.z * n2.x * (1 - proj1) - n2.y * proj2, n2.z * n2.y * (1 - proj1) + n2.x * proj2, proj1 + n2.z * n2.x * (1 - proj1), 0),
                            // new Vector4(0, 0, 0, 1)
                        );

                        rotateMatrix = newRotate * rotateMatrix;
                        //Debug.Log(newRotate);
                        //Debug.Log(rotate);
                        faceColor.SetMatrix("rotateMatrix", rotateMatrix);
                        break;
                    case 3:
                        Matrix4x4 newScale =Matrix4x4.Scale(new Vector3(1 + 0.1f*d.x, 1 + 0.1f*d.y, 1));
                        scaleMatrix = newScale * scaleMatrix;
                        faceColor.SetMatrix("scaleMatrix", scaleMatrix);
                        break;
                }
            }
        }

        // Update is called once per frame

    }
} // End of PS04
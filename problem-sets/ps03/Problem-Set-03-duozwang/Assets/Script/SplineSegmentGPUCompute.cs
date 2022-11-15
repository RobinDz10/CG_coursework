using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS03 {

    public class SplineSegmentGPUCompute : MonoBehaviour {

        // specify the name of the Vertex Shader to be used:
        private const string shaderName = "SplineVertexShader";

        // control points for a single Spline Curve segment:
        [SerializeField] private Transform control0, control1, control2, control3;
        // choice of Spline Curve type:
        [SerializeField] private SplineParameters.SplineType splineType;
        // only one line renderer: the control polyline:
        [SerializeField] private LineRenderer controlPolyLine;
        [SerializeField] private LineRenderer firstDerivativeLine0, firstDerivativeLine1, secondDerivativeLine0, secondDerivativeLine1;

        // what color should the Spline Curve be?
        [SerializeField] private Color splineColor = new Color(255f / 255f, 255f / 255f, 0f / 255f);
        [SerializeField] private Color derivative1Color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
        [SerializeField] private Color derivative2Color = new Color(255f / 255f, 0f / 255f, 255f / 255f);

        // how wide should the Spline Curve be?
        [SerializeField] private float splineWidth = 0.1f;

        // how many vertices on the spline curve?
        //   (the more vertices you set, the smoother the curve will be)
        [Range(8, 512)] [SerializeField] private int verticesOnCurve = 64;


        // the Spline Curve will be rendered by a MeshRenderer,
        //   (and the vertices for the Mesh Renderer
        //   will be computed in our Vertex Shader)
        private MeshRenderer meshRenderer;

        // The Mesh Filter is meant to take a mesh from assets
        //    and pass it to the Mesh Renderer for rendering on the screen.
        // However, we create the mesh in this script,
        //    before the Mesh Filter passes it to the Mesh Renderer:
        private MeshFilter meshFilter;

        // the Vertex Shader will be considered a "Material" for rendering purposes:
        private Material material;

        // the Mesh to be rendered:
        private Mesh mesh;

        public void SetType(SplineParameters.SplineType type) {
            splineType = type;
        }

        public void UseBezier() => SetType(SplineParameters.SplineType.Bezier);

        public void UseCatmullRom() => SetType(SplineParameters.SplineType.CatmullRom);

        public void UseB() => SetType(SplineParameters.SplineType.Bspline);

        public void drawBezierFirstDerivative(Transform control0, Transform control1, Transform control2, Transform control3){
            float x0 = control0.position[0], x1 = control1.position[0], x2 = control2.position[0], x3 = control3.position[0];
            float y0 = control0.position[1], y1 = control1.position[1], y2 = control2.position[1], y3 = control3.position[1];
            Vector2 direction0 = new Vector2(3 * (x1 - x0), 3 * (y1 - y0));
            Vector2 direction1 = new Vector2(3 * (x3 - x2), 3 * (y3 - y2));
            Vector2 point0 = new Vector2(control0.position[0], control0.position[1]); //x(0)
            Vector2 point1 = new Vector2(control3.position[0], control3.position[1]); //x(1)
            firstDerivativeLine0.SetPosition(0, point0);
            firstDerivativeLine0.SetPosition(1, point0 + direction0);
            firstDerivativeLine1.SetPosition(0, point1);
            firstDerivativeLine1.SetPosition(1, point1 + direction1);
        }

        public void drawBezierSecondDerivative(Transform control0, Transform control1, Transform control2, Transform control3){
            float x0 = control0.position[0], x1 = control1.position[0], x2 = control2.position[0], x3 = control3.position[0];
            float y0 = control0.position[1], y1 = control1.position[1], y2 = control2.position[1], y3 = control3.position[1];
            Vector2 direction0 = new Vector2(6 * (x0 - 2 * x1 + x2), 6 * (y0 - 2 * y1 + y2));
            Vector2 direction1 = new Vector2(6 * (x1 - 2 * x2 + x3), 6 * (y1 - 2 * y2 + y3));
            Vector2 point0 = new Vector2(control0.position[0], control0.position[1]);
            Vector2 point1 = new Vector2(control3.position[0], control3.position[1]);
            secondDerivativeLine0.SetPosition(0, point0);
            secondDerivativeLine0.SetPosition(1, point0 + direction0);
            secondDerivativeLine1.SetPosition(0, point1);
            secondDerivativeLine1.SetPosition(1, point1 + direction1);
        }

        public void drawCRFirstDerivative(Transform control0, Transform control1, Transform control2, Transform control3){
            float x0 = control0.position[0], x1 = control1.position[0], x2 = control2.position[0], x3 = control3.position[0];
            float y0 = control0.position[1], y1 = control1.position[1], y2 = control2.position[1], y3 = control3.position[1];
            Vector2 direction0 = new Vector2(0.5f * (x2 - x0), 0.5f * (y2 - y0));
            Vector2 direction1 = new Vector2(0.5f * (x3 - x1), 0.5f * (y3 - y1));
            Vector2 point0 = new Vector2(control1.position[0], control1.position[1]);
            Vector2 point1 = new Vector2(control2.position[0], control2.position[1]);
            firstDerivativeLine0.SetPosition(0, point0);
            firstDerivativeLine0.SetPosition(1, point0 + direction0);
            firstDerivativeLine1.SetPosition(0, point1);
            firstDerivativeLine1.SetPosition(1, point1 + direction1);
        }

        public void drawCRSecondDerivative(Transform control0, Transform control1, Transform control2, Transform control3){
            float x0 = control0.position[0], x1 = control1.position[0], x2 = control2.position[0], x3 = control3.position[0];
            float y0 = control0.position[1], y1 = control1.position[1], y2 = control2.position[1], y3 = control3.position[1];
            Vector2 direction0 = new Vector2(2.0f * x0 - 5.0f * x1 + 4.0f * x2 - x3, 2.0f * y0 - 5.0f * y1 + 4.0f * y2 - y3);
            Vector2 direction1 = new Vector2(-1.0f * x0 + 4.0f * x1 - 5.0f * x2 + 2.0f * x3, -1.0f * y0 + 4.0f * y1 - 5.0f * y2 + 2.0f * y3);
            Vector2 point0 = new Vector2(control1.position[0], control1.position[1]);
            Vector2 point1 = new Vector2(control2.position[0], control2.position[1]);
            secondDerivativeLine0.SetPosition(0, point0);
            secondDerivativeLine0.SetPosition(1, point0 + direction0);
            secondDerivativeLine1.SetPosition(0, point1);
            secondDerivativeLine1.SetPosition(1, point1 + direction1);
        }

        public void drawBSplineFirstDerivative(Transform control0, Transform control1, Transform control2, Transform control3){
            float x0 = control0.position[0], x1 = control1.position[0], x2 = control2.position[0], x3 = control3.position[0];
            float y0 = control0.position[1], y1 = control1.position[1], y2 = control2.position[1], y3 = control3.position[1];
            Vector2 direction0 = new Vector2(0.5f * (x2 - x0), 0.5f * (y2 - y0));
            Vector2 direction1 = new Vector2(0.5f * (x3 - x1), 0.5f * (y3 - y1));
            Vector2 point0 = new Vector2(0.167f * (x0 + 4 * x1 + x2), 0.167f * (y0 + 4 * y1 + y2));
            Vector2 point1 = new Vector2(0.167f * (x1 + 4 * x2 + x3), 0.167f * (y1 + 4 * y2 + y3));
            firstDerivativeLine0.SetPosition(0, point0);
            firstDerivativeLine0.SetPosition(1, point0 + direction0);
            firstDerivativeLine1.SetPosition(0, point1);
            firstDerivativeLine1.SetPosition(1, point1 + direction1);
        }

        public void drawBSplineSecondDerivative(Transform control0, Transform control1, Transform control2, Transform control3){
            float x0 = control0.position[0], x1 = control1.position[0], x2 = control2.position[0], x3 = control3.position[0];
            float y0 = control0.position[1], y1 = control1.position[1], y2 = control2.position[1], y3 = control3.position[1];
            Vector2 direction0 = new Vector2(x0 - 2.0f * x1 + x2, y0 - 2.0f * y1 + y2);
            Vector2 direction1 = new Vector2(x1 - 2.0f * x2 + x3, y1 - 2.0f * y2 + y3);
            Vector2 point0 = new Vector2(0.167f * (x0 + 4 * x1 + x2), 0.167f * (y0 + 4 * y1 + y2));
            Vector2 point1 = new Vector2(0.167f * (x1 + 4 * x2 + x3), 0.167f * (y1 + 4 * y2 + y3));
            secondDerivativeLine0.SetPosition(0, point0);
            secondDerivativeLine0.SetPosition(1, point0 + direction0);
            secondDerivativeLine1.SetPosition(0, point1);
            secondDerivativeLine1.SetPosition(1, point1 + direction1);
        }

        // ---------------------------------------------------------
        // set up the renderer, the first time this object is instantiated in the scene:
        private void Awake() {

            // obtain Mesh Renderer and Mesh Filter components from Unity scene:
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();

            // find the vertex shader that will compute Spline curve vertices:
            material = new Material(Shader.Find(shaderName));

            // connect our MeshRenderer to the Vertex Shader:
            meshRenderer.material = material;

            // instantiate required vertices and triangles for the Mesh:
            Vector3[] vertices = new Vector3[verticesOnCurve * 2];
            int[] triangles = new int[verticesOnCurve * 6 - 6];

            for (int i = 0; i < verticesOnCurve; i++) {


                // parameter for vertices on "base spline curve":
                float t1 = (float)i / (float)(verticesOnCurve - 1);

                // parameter for vertices on "offset spline curve":
                float t2 = (float)i / (float)(verticesOnCurve - 1);

                // the "trick" is to pass the parameters t1 and t2
                //   (for Spline Curve computation in the Vertex Shader)
                // as .x components in the vertices.

                // we also use the .y components to pass another value
                //   used to compute the "offset spline curve" vertices (see below)

                // the Vertex Shader will receive the t1, t2 parameters
                // and use t1, t2 values to compute the position of each
                // vertex on the Spline Curve.

                // vertices on "base spline curve":
                vertices[2 * i].x = t1;
                vertices[2 * i].y = 0;

                // vertices on "offset spline curve":
                vertices[2 * i + 1].x = t2;
                vertices[2 * i + 1].y = splineWidth;

                if (i < verticesOnCurve - 1) {

                    // triangle with last side on "base spline curve"
                    // i.e. vertex 2 to vertex 0:
                    triangles[6 * i] = 2 * i;
                    triangles[6 * i + 1] = 2 * i + 1;
                    triangles[6 * i + 2] = 2 * i + 2;

                    // triangle with one side on "offset spline curve"
                    // i.e. vertex 1 to vertex to vertex 3:
                    triangles[6 * i + 3] = 2 * i + 1;
                    triangles[6 * i + 4] = 2 * i + 3;
                    triangles[6 * i + 5] = 2 * i + 2;
                }
            }
            mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            meshFilter.mesh = mesh;
            meshRenderer.sortingLayerName = "Default";
            meshRenderer.sortingOrder = 1;
            
        } // end of Awake()

        // ---------------------------------------------------------
        private void Update() {
            
            Matrix4x4 splineMatrix = SplineParameters.GetMatrix(splineType);

            // pass all necessary variables to the Vertex Shader:
            // (note: in GLSL these would be considered "uniform" variables)
            //
            // spline matrix in Hermite form:
            material.SetMatrix("_SplineMatrix", splineMatrix);
            // control points for spline curve rendering:
            material.SetVector("_Control0", control0.position);
            material.SetVector("_Control1", control1.position);
            material.SetVector("_Control2", control2.position);
            material.SetVector("_Control3", control3.position);
            // step between subsequent t parameter values for curve:
            float step = (float)1.0 / (float)(verticesOnCurve - 1);
            material.SetFloat("_Step", step);
            // color to be passed to the Fragment Shader:
            material.SetColor("_Color", splineColor);


            // to draw the enclosing polyLine, set control line points:
            //
            controlPolyLine.SetPosition(0, control0.position);
            controlPolyLine.SetPosition(1, control1.position);
            controlPolyLine.SetPosition(2, control2.position);
            controlPolyLine.SetPosition(3, control3.position);

            if(splineType == SplineParameters.SplineType.Bezier){
                drawBezierFirstDerivative(control0, control1, control2, control3);
                drawBezierSecondDerivative(control0, control1, control2, control3);
            }
            else if(splineType == SplineParameters.SplineType.CatmullRom){
                drawCRFirstDerivative(control0, control1, control2, control3);
                drawCRSecondDerivative(control0, control1, control2, control3);
            }
            else if(splineType == SplineParameters.SplineType.Bspline){
                drawBSplineFirstDerivative(control0, control1, control2, control3);
                drawBSplineSecondDerivative(control0, control1, control2, control3);
            }

        } // end of Update()

    } // end of SplineSegmentGPUCompute

} // end of PS03
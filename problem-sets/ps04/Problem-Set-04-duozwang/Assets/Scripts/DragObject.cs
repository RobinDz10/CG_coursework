using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PS04 {

    public class DragObject : MonoBehaviour {


        public CameraMove CameraController;
        private Camera myMainCamera;
    
        private Vector2 myMouseStartPosition;
        
        private Transform _transform;
        new public Transform transform {
            get {

                // GetComponent<type>()
                //    returns the component with name type
                //        if the game object has one attached,
                //    null if it doesn't.
                return _transform ?? (_transform = GetComponent<Transform>());
                // Note: in C# the "null coalescing" operator does the following:
                //    x ?? y   Evaluates to y if x is null, to x otherwise.
            } // end of get
        } // end of public Transform transform


        // Awake is called when the script instance is being loaded.
        //   It is used to initialize any variables or state before the game starts.
        //   Awake is called after all objects are initialized,
        //   so it can be used to communicate with other objects.
        private void Awake() {
            // obtain the main Camera used in the scene:
            myMainCamera = Camera.main;
        }

        // OnMouseDown() is an event handler, it is called when
        //   the mouse button is clicked while over any GUIElement or Collider object in the scene
        private void OnMouseDown() {

            myMouseStartPosition = Input.mousePosition;
            // if debug is necessary, uncomment these lines:
            // Debug.Log("OnMouseDown() lMousePosition = " + lMousePosition);
            // Debug.Log("OnMouseDown() myMouseStartWorldPosition = " + myMouseStartWorldPosition);
            //Debug.Log("OnMouseDown() myObjectStartPosition = " + myMouseStartPosition);
        }

        // OnMouseDrag() is an event handler, it is called when
        //   the mouse button is "dragging" any GUIElement or Collider object in the scene
        private void OnMouseDrag() {
            Vector2 d = (Vector2)Input.mousePosition - myMouseStartPosition;
            myMouseStartPosition = Input.mousePosition;
            // Debug.Log("OnMouseDown() lMousePosition = " + lMousePosition);
            // Debug.Log("OnMouseDrag() lMouseCurrentWorldPosition = " + lMouseCurrentWorldPosition);
            CameraController.UpdateCamera(d);
            // Debug.Log("OnMouseDrag() transform.position = " + d);
        }

    } // end of class DragObject

}
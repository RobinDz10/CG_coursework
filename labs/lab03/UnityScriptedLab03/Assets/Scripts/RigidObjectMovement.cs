using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidObjectMovement : MonoBehaviour
{

    public float moveSpeed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
    }
}

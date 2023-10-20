using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
// Makes the camera follow the balls until the balls reaches the end of the lane
// See gameController.cs and pinController.cs for collision stuff.
// 

public class followBall : MonoBehaviour
{
    // Difference in initial x/y/z position from balls to camera
    float xi = 0.0f;
    float yi = 0.0f;
    float zi = 0.0f;
    Quaternion ri;
    Transform ballpos;
    GameObject ball;
    
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("ball");
        ballpos = ball.GetComponent<Transform>();

        // Grab initial difference between balls and camera
        xi = transform.position.x - ballpos.position.x;
        yi = transform.position.y - ballpos.position.y;
        zi = transform.position.z - ballpos.position.z;
        ri = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Update current balls position, and update camera position.
        if(ball.GetComponent<ballController>().mode == 1){ // If we are in rotation mode
            transform.position = ballpos.position + 0.4f*(ballpos.forward) + new Vector3(0.0f, 0.25f, 0.0f); // Change the view angle
            transform.rotation = ballpos.rotation;
            transform.RotateAround(transform.position, transform.up, 180); // Because axis differences
        }else{ // Otherwise track balls normally
            transform.rotation = ri;
            transform.position = new Vector3(ballpos.position.x + xi, ballpos.position.y + yi, ballpos.position.z + zi);
        }
    }
}

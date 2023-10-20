using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Used for the directional arrow to track balls rotation
// so that things are aligned in a semi-accurate representation of direction
//

public class trackRotation : MonoBehaviour
{
    private GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        // Find the balls object
        ball = GameObject.Find("ball");
    }

    // Update is called once per frame
    void Update()
    {
        // Get current balls transform
        Transform ballstuff = ball.GetComponent<Transform>();

        // Adjust position and rotation equal to that of the balls, but offset.
        transform.position = ballstuff.position + -0.4f*(ballstuff.forward);
        transform.rotation = ballstuff.rotation;
        transform.RotateAround(transform.position, transform.up, 180);
    }
}

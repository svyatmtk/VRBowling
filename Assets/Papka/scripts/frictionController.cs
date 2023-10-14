using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
// Changes how much friction the lane object has when a trigger happens.
// Only intended for a trigger box ~halfway down the lane.
//

public class frictionController : MonoBehaviour
{
    public PhysicMaterial lb; // This gets set in Unity. Sort of a weird setup, but it works. lb stands for "lane back" which has more dynamic friction.
    private GameObject lane;

    private void OnTriggerEnter(Collider collider) {
        // When the ball enters the area
        if(collider.name == "ball"){
            // Change material of the lane
            lane.GetComponent<BoxCollider>().material = lb;
        }
    }

    void Start()
    {
        // Obtain lane object
        lane = GameObject.Find("lane");
    }

    void Update()
    {}
}

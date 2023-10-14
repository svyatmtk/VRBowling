using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
   [SerializeField] GameObject ball;

    public void SpawnBall(GameObject transform) {
        if (ball!=null)
        {
           GameObject ballObj = Instantiate(ball, transform.transform.position, transform.transform.rotation);
            
        }
    }

}

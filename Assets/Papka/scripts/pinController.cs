using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Pin gravity-enable. Otherwise they like to shift around.
// Pins are placed slightly above the ground so that they don't contact floor (because physics is annoying)
// Pins play collision sound when colliding with another pin or the ball
//

public class pinController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Rigidbody rBody = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;

        // If the rigidbody exists (which it should, this should only be applied to pins)
        if (rBody != null){
            
            // Turn gravity on (gravity was causing our objects to slide across one another)
            rBody.useGravity = true;

            // If the colliding object is the ball or another pin, play a sound
            if(other.collider.name == "ball" || other.collider.name.Contains("pin")){
                float collisionForce = (other.impulse / Time.fixedDeltaTime).magnitude/350.0f;
                // print(collisionForce);
                gameObject.GetComponent<AudioSource>().volume = Mathf.Min(collisionForce, 0.6f);
                gameObject.GetComponent<AudioSource>().mute = false;
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }

    // Start is called before the first frame update
    void Start(){
        // print(gameObject);
        // GameObject.Find("bucket_boundary").GetComponent<gameController>().pins.Add((gameObject, gameObject.transform));
    }

    // Update is called once per frame
    void Update(){}
}

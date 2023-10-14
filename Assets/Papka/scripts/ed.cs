using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Enables/Disables a component when the B key is pressed. 
// Only intended to go on our bumpers, hence the B key.
//

public class ed : MonoBehaviour
{
    bool mode = true;
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){

        // Bumper Toggle on key B
        if(Input.GetKeyDown(KeyCode.B)){
            mode = !mode;

            // Enable/Disable bumper collider and mesh
            this.GetComponent<MeshRenderer>().enabled = mode;
            this.GetComponent<MeshCollider>().enabled = mode;
        }
    }
}

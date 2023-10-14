using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// Move initial ball position/rotation left/right
// using A/D keys on the keyboard & shift to swap modes.
// Space to launch ball.
//

public class ballController : MonoBehaviour
{
    public Texture2D[] texts;
    public Vector3 ballStartPos = new Vector3(0.0f, 0.5f, 1.0f);
    private float range = 0.5f;
    private float angleRange = 30.0f;
    public float speed = -10.0f;
    public int mode = 0;
    Rigidbody rBody;
    private GameObject arrow;
    private MeshRenderer arrowMesh;
    private Vector3 direction = new Vector3(0.0f, 0.0f, -1.0f);
    private Transform originalTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Obtain rigidbody so we don't have to do it every frame
        rBody = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
        originalTransform = transform;
        arrow = GameObject.Find("arrow");
        arrowMesh = arrow.GetComponent<MeshRenderer>();
    }

    public void ResetPosition(){
        transform.position = originalTransform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        mode = 0;
        arrowMesh.enabled = true;
        GameObject temp = GameObject.Find("Canvas");
        Slider[] s = temp.GetComponentsInChildren<Slider>(false);
        s[1].value = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float lr = Input.GetAxisRaw("Horizontal");
        float moveSpeed = 0.004f;
        float rotateSpeed = 0.07f;
        float modifier = 1.0f;

        if(Input.GetKey(KeyCode.LeftControl)){
            modifier = 0.5f;
        }

        // If we are in left/right movement mode
        if(mode == 0){
            rBody.useGravity = false;
            // Transform ball left/right
            transform.position = new Vector3(transform.position.x - (lr * moveSpeed * modifier), ballStartPos.y, ballStartPos.z);

            // Maintain ball within bounds of the lane (initially)
            if (transform.position.x > range) {
                transform.position = new Vector3(range, ballStartPos.y, ballStartPos.z);
            } else if (transform.position.x < -range) {
                transform.position = new Vector3(-range, ballStartPos.y, ballStartPos.z);
            }
            
            // Switch to rotation mode
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                mode = 1;
            }

        }
        // Rotation mode!
        else if (mode == 1){
            rBody.useGravity = false;
            // Rotate around current position
            transform.RotateAround(transform.position, Vector3.up, (lr * rotateSpeed * modifier));

            // Stay within rotation bounds (initially)
            if (Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.up) < -angleRange) {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, angleRange, transform.eulerAngles.z);
            } else if (Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.up) > angleRange) {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, -angleRange, transform.eulerAngles.z);
            }

            // Switch to position mode
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                mode = 0;
            }
        }

        // Launch ball down the lane
        if(Input.GetKeyUp(KeyCode.Space) && mode != 2){
            mode = 2;
            rBody.useGravity = true;

            GameObject temp = GameObject.Find("Canvas");
            Slider[] s = temp.GetComponentsInChildren<Slider>(false);

            rBody.useGravity = true;
            rBody.velocity = (speed - (2.0f * -s[0].value)) * transform.forward;

            rBody.maxAngularVelocity = 40.0f;
            rBody.AddTorque(new Vector3(5.0f * -(s[0].value + 1.0f / 2.0f) - 5.0f, 0.0f, 7.5f * s[1].value), ForceMode.Impulse);

            arrowMesh.enabled = false;
        }
        
        checkColorSwap();
    }

    private void checkColorSwap(){
        // Change ball color
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = texts[0];
        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = texts[1];
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = texts[2];
        }else if(Input.GetKeyDown(KeyCode.Alpha4)){
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = texts[3];
        }else if(Input.GetKeyDown(KeyCode.Alpha5)){
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = texts[4];
        }else if(Input.GetKeyDown(KeyCode.Alpha6)){
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = texts[5];
        }else if(Input.GetKeyDown(KeyCode.Alpha7)){
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = texts[6];
        }
    }
}

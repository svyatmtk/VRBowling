using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
// Houses the majority of our game systems. Camera movement, pin placement, and reset
// 

public class gameController : MonoBehaviour
{
    public GameObject pinPrefab; // This gets set in the unity interface. Sort of a weird setup, but it works.
    public Vector3 pinOrigin; // This gets set in the unity interface. Sort of a weird setup, but it works.
    public PhysicMaterial slippery; // This gets set in the unity interface. Sort of a weird setup, but it works.
    public (GameObject obj, Vector3 originalPosition)[] pins;
    private GameObject ball;
    private GameObject lane;
    private GameObject cam;
    private GameObject scoreText;
    private bool fullReset = false;
    private int score;

    private void OnTriggerEnter(Collider collider) {
        //stop camera once ball reaches pin collector
        if(collider.name == "ball"){
            (cam.GetComponent<followBall>()).enabled = false;
        }
    }

    private void InitPins(){
        //place pins programmatically
        // pin_origin = new Vector3(0.0f, 1.0f, -25.0f);
        pins = new (GameObject obj, Vector3 originalPosition)[10];
        float a = 0.3048f/2.0f; //lateral distance between pins in a row
        float b = a * Mathf.Tan(Mathf.PI / 3); //distance between each row of pins

        int pindex = 0; //pin index

        for(int i = 0; i <= 3; i++){ //for each row

            for(int j = 0; j <= i; j++){ //place i+1 pins

                Vector3 pinLocation = new Vector3(
                    pinOrigin.x + (i * a) - ((2 * a) * j), //each pin in a row spaced 2a apart starting at offset of i*a
                    pinOrigin.y, //all same height
                    pinOrigin.z - (i * b) //each row is i units of b length from start
                );

                GameObject pin = Instantiate(pinPrefab, pinLocation, Quaternion.Euler(-90, 0, 0)); //instantiate pin object and grab reference
                pin.name = "pin " + (pindex + 1); //give a unique name
                pins[pindex] = (obj: pin, originalPosition: pinLocation); //store reference in tuple array
                pindex++; //increment pin counter
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Obtain camera, ball, lane, and UI objects
        cam = GameObject.Find("Main Camera");
        ball = GameObject.Find("ball");
        lane = GameObject.Find("lane");
        scoreText = GameObject.Find("ScoreText");
        //set initial game conditions
        fullReset = false;
        score = 0;
        InitPins();
    }

    void ResetPins(bool fullReset) //set up pins again
    {
        int shotScore = 0;
        for(int i = 0; i < pins.Length; i++){

            //get original position and rotation for each pin
            Vector3 position = pins[i].originalPosition;
            Quaternion rotation = Quaternion.Euler(-90, 0, 0);

            if((pins[i].obj.transform.position - position).magnitude > 0.1f){ //if displacement is greater than 10 cm
                shotScore++; //raise shot score
                pins[i].obj.GetComponent<MeshRenderer>().enabled = false; //disable visibility
                pins[i].obj.GetComponent<MeshCollider>().enabled = false; //disable collision
            }

            if(fullReset){ //place all pins
                pins[i].obj.GetComponent<MeshRenderer>().enabled = true; //enable visibility
                pins[i].obj.GetComponent<MeshCollider>().enabled = true; //enable collision
            }

            pins[i].obj.transform.position = position; //restore position to initial
            pins[i].obj.transform.rotation = rotation; //restor rotation to initial
            pins[i].obj.GetComponent<Rigidbody>().velocity = Vector3.zero; //keep stable
            pins[i].obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; //don't rotate
            pins[i].obj.GetComponent<Rigidbody>().useGravity = false; //suspend in air just above lane until collision
        }

        score += shotScore; //add up points to total score
        if (shotScore == 10){ //reset pins on strike (or 0-10 spare)
            fullReset = true; //set to true so it becomes false after inversion following function call
            ResetPins(true); //put pins back even if first bowl in frame
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){ //Reset condition. make smarter later
            
            ResetPins(fullReset); //reset all pins every other throw
                        
            lane.GetComponent<BoxCollider>().material = slippery; //reset lane material back to normal

            ball.GetComponent<ballController>().ResetPosition(); //reset ball to be thrown again

            cam.GetComponent<followBall>().enabled = true; //re-enable camera ball-tracking

            fullReset = !fullReset; //only reset all pins every other throw

            scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score; //update total score to UI
        }
    }
}

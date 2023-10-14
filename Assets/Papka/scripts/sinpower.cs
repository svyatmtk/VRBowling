using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 
// Grants user the ability to set the value of a slider based on the mathematical sin function when holding the space key
// 

public class sinpower : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start(){
        slider = gameObject.GetComponent(typeof(Slider)) as Slider;
    }

    // Update is called once per frame
    void Update()
    {
        // Spacebar (Other sinpower is Arrow Down)
        if(Input.GetKey(KeyCode.Space)){
            slider.value = Mathf.Sin(5.0f * Time.time);
        }
    }
}

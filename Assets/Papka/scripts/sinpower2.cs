using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 
// Grants user the ability to set the value of a slider based on the mathematical sin function when holding the downwards arrow key
// 

public class sinpower2 : MonoBehaviour
{
    public Slider slider;
    private bool first = true;
    // Start is called before the first frame update
    void Start(){
        slider = gameObject.GetComponent(typeof(Slider)) as Slider;
    }

    // Update is called once per frame
    void Update()
    {
        // Arrow Down (Other sinpower is spacebar)
        if(Input.GetKey(KeyCode.DownArrow) && first){
            slider.value = Mathf.Sin(5.0f* Time.time);
        }
    }
}

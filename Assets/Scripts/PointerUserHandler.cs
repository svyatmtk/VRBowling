using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;

public class PointerUserHandler : SteamVR_LaserPointer
{
    private Color defaultColor = Color.white;
    public GameObject player;
    public override void OnPointerClick(PointerEventArgs e)
    {
        base.OnPointerClick(e);
        if (e.target.CompareTag("buttonUI"))
        {
            Debug.Log("restart");
            e.target.GetComponent<Button>().image.color = Color.yellow;
            e.target.GetComponent<Button>().onClick.Invoke();
            e.target.LookAt(player.transform);
        }
    }
    public override void OnPointerOut(PointerEventArgs e)
    {
        base.OnPointerOut(e);
        if (e.target.CompareTag("buttonUI"))
        {
            Debug.Log("out");
            e.target.GetComponent<Button>().image.color = defaultColor;
        }
    }
    public override void OnPointerIn(PointerEventArgs e)
    {   
        base.OnPointerIn(e);
        if (e.target.CompareTag("buttonUI"))
        {
            Debug.Log("in");
            e.target.GetComponent<Button>().image.color = Color.red;
        }
    }
}


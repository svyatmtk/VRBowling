using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

public class PointerUserHandler : SteamVR_LaserPointer
{
    private Color defaultColor = Color.white;
    public override void OnPointerClick(PointerEventArgs e)
    {
        base.OnPointerClick(e);
        if (e.target.CompareTag("buttonUI"))
        {                     
              e.target.GetComponent<Button>().image.color = Color.yellow;
              e.target.GetComponent<Button>().onClick.Invoke();           
        }
    }
    public override void OnPointerOut(PointerEventArgs e)
    {
        base.OnPointerOut(e);
        if (e.target.CompareTag("buttonUI"))
        {
            e.target.GetComponent<Button>().image.color = defaultColor;
        }
    }
    public override void OnPointerIn(PointerEventArgs e)
    {   
        base.OnPointerIn(e);
        if (e.target.CompareTag("buttonUI"))
        {
            e.target.GetComponent<Button>().image.color = Color.red;
        }
    }
}


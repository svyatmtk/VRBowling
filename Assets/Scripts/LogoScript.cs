using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.Newtonsoft.Json.Bson;

public class LogoScript : MonoBehaviour
{
    private float logoAppearanceDuration = 1.0f;
    private float logoDisappearanceDuration = 4.0f;
    private string menuScene = "Menu";

    private Color startColor;
    private Color targetColorFirst;

    public TextMeshProUGUI studioName;

    private void Start()
    {
        startColor = gameObject.GetComponent<Image>().color;       
        targetColorFirst = new Color(startColor.r, startColor.g, startColor.b, 1f);

        StartCoroutine(LogoShadingOn());
    }

    public IEnumerator LogoShadingOn()
    {
        float time = 0f;

        while (time < logoAppearanceDuration)
        {         
            gameObject.GetComponent<Image>().color = Color.Lerp(startColor, targetColorFirst, time / logoAppearanceDuration);
            Debug.Log(gameObject.GetComponent<Image>().color);
            studioName.color = Color.Lerp(startColor, targetColorFirst, time / logoAppearanceDuration);
            time += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(LogoShadingOff());
    }
    public IEnumerator LogoShadingOff()
    {
        float time = 0f;

        while (time < logoDisappearanceDuration) 
        {
            gameObject.GetComponent<Image>().color = Color.Lerp(targetColorFirst, startColor, time / logoDisappearanceDuration);
            Debug.Log(gameObject.GetComponent<Image>().color);
            studioName.color = Color.Lerp(targetColorFirst, startColor, time / logoDisappearanceDuration);
            time += Time.deltaTime;
            yield return null;
        }
        LoadMainMenu();
    }

    public void LoadMainMenu() => SceneManager.LoadScene(menuScene);
}

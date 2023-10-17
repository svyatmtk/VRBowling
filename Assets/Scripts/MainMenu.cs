using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject authorsList;
    public void MainSceneLoad() => SceneManager.LoadScene("Main");
    public void CloseGame() => Application.Quit();
    public void ShowAuthors() => authorsList.SetActive(true);
    public void CloseAuthors() => authorsList.SetActive(false);
}

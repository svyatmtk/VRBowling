using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject authorsList;
    public GameObject mainMenuCanvas;
    public GameObject player;
    public void MainSceneLoad() => SceneManager.LoadScene("Main");
    public void CloseGame() => Application.Quit();
    public void ShowAuthors() => authorsList.SetActive(true);
    public void CloseAuthors() => authorsList.SetActive(false);

    public void RestartMainScene()
    {
        Destroy(player);
        string mainName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(mainName);
    }

    public void CloseMenu() => mainMenuCanvas.SetActive(false);
}

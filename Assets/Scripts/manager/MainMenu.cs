using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

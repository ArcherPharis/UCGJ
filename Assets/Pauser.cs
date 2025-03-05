using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    [SerializeField] Button MenuButton;
    [SerializeField] Button QuitButton;

    public void OnMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;

    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    public void ShowPauseMenu()
    {
        gameObject.SetActive(true);
    }

    public void HidePauseMenu()
    {
        gameObject.SetActive(false);
    }
}

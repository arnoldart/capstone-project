using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class pauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionMenu;

    private void Start()
    {
        _pauseMenu.SetActive(false);
        _optionMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void pauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void option()
    {
        _optionMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}

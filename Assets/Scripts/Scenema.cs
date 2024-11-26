using UnityEngine.SceneManagement;
using UnityEngine;

public class Scenema : MonoBehaviour
{
    [SerializeField] private GameObject _optionMenu;
    public void playbutton()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Options()
    {
        _optionMenu.SetActive(true);
    }
}

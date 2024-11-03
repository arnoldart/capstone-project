using UnityEngine.SceneManagement;
using UnityEngine;

public class Scenema : MonoBehaviour
{
    public void playbutton()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

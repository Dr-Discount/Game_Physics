using CGL.Events;
using CGL.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    [SerializeField]
    PauseScreen pauseScreen;

    [SerializeField]
    GameObject panel;

    [SerializeField]
    private EventSO onQuitGameEvent;

    [SerializeField]
    GameManager gameManager;

    public void Resume()
    {
        pauseScreen.canPause = true;
        Time.timeScale = 1.0f;
        panel.SetActive(false);
    }

    public void Restart()
    {
        gameManager.Restart();
    }

    public void Quit()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
        onQuitGameEvent?.RaiseEvent();
    }
}

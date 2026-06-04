using CGL.Data;
using CGL.Events;
using CGL.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    IntDataSO score;

    [SerializeField]
    private EventSO onUpdateEvent;

    [SerializeField]
    GameObject winScreen;
    
    [SerializeField]
    GameObject GameOverPanel;

    [SerializeField]
    PauseScreen pauseScreen;

    float timer = 1;
    bool check = true;
    bool gameWon = false;

    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindFirstObjectByType<GameManager>();
            return instance;
        }
    }

    void Start()
    {
        score.value = 0;
        pauseScreen.canPause = true;
    }

    private void FixedUpdate()
    {
        if (check && Time.timeScale > 0)
        {
            check = false;
            StartCoroutine(Score());
        }
    }

    public void OnGameStart()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            score.value += 1;
            if (score.value > 256)
                onGameWin();
            timer = 1;
        }
    }

    public void onGameWin()
    {
        winScreen.SetActive(true);
        pauseScreen.canPause = false;
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");

    }

    IEnumerator Score()
    {
        while (Time.timeScale > 0)
        {
            yield return new WaitForSecondsRealtime(timer);

            score.value += 1;
            onUpdateEvent?.RaiseEvent();

            if (!gameWon && score.value > 256)
            {
                gameWon = true;
                onGameWin();
            }
        }

        check = true;
    }
}
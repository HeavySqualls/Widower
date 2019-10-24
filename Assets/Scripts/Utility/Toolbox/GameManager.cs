using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public float countInTime = 3;
    public bool isGameOver = false;
    public bool isGameStart = false;
    public int currentLevel;

    private TimerCanvas tCan;
    private Player_Manager p1_Manager;
    private Player_Manager p2_Manager;
    private TimeManager timeManager;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGameManager();
    }

    public void ResetGameManager()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = 1f;
        isGameStart = false;

        timeManager = Toolbox.GetInstance().GetTimeManager();
        timeManager.ResetTime();

        p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
        p1_Manager.ResetPlayerManager();

        p2_Manager = Toolbox.GetInstance().GetPlayer_2_Manager();
        p2_Manager.ResetPlayerManager();

        if (currentLevel != 0)
        {
            tCan = GameObject.FindGameObjectWithTag("TimerCanvas").GetComponent<TimerCanvas>();
        }
    }

    private void Update()
    {
        StartGame();
        ResetAndChangeLevel();
    }

    public void changeLevel(int selectedLevel)
    {
        ResetPlayerPickUps();
        SceneManager.LoadScene(selectedLevel);
    }

    private void StartGame()
    {
        if (!isGameStart && p1_Manager.isReady && p2_Manager.isReady && currentLevel != 0)
        {

            tCan.StartCountDownTimer();
            isGameStart = true;
        }
    }

    public void EndRound()
    {
        if (currentLevel != 0)
        {
            Time.timeScale = 0.25f;

            print("Game Over");
            isGameOver = true;
            tCan.gameOverPanel.SetActive(true);

            p1_Manager.DisplayScore();
            p1_Manager.pCon.processInputs = false;

            p2_Manager.DisplayScore();
            p2_Manager.pCon.processInputs = false;
        }
    }


    public void ResetAndChangeLevel()
    {
        if (p1_Manager.isRestart && p2_Manager.isRestart/* && isGameOver == true*/)
        {
            ResetPlayerPickUps();
            isGameOver = false;
            SceneManager.LoadScene("gym_WidowPredatorPathfinding"); // resets current level for now
        }
    }

    public void ResetPlayerPickUps()
    {
        p1_Manager.ResetPickups();
        p2_Manager.ResetPickups();
    }
}

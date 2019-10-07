using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public float countInTime = 3;

    public bool isGameOver = false;
    public bool isGameStart = false;

    public int gameDeathCount; // not used at the moment
    public int numberOfPlayers; //for knowing if it is multi player
    public int currentLevel;

    private Player_1_Manager p1_Manager;
    private Player_2_Manager p2_Manager;
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
        //Toolbox.GetInstance().GetTimer().ResetTimer();
        //Toolbox.GetInstance().GetPlayer_1_Manager().ResetPlayerManager();
        ResetGameManager();
    }

    private void Start() // references to scene objects must also be made in ResetGameManager() below
    {
        p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
        p2_Manager = Toolbox.GetInstance().GetPlayer_2_Manager();
        timeManager = Toolbox.GetInstance().GetTimeManager();

        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void ResetGameManager() // is this needed?
    {
        p1_Manager = Toolbox.GetInstance().GetPlayer_1_Manager();
        p1_Manager.ResetPlayerManager1();

        p2_Manager = Toolbox.GetInstance().GetPlayer_2_Manager();
        p2_Manager.ResetPlayerManager2();

        timeManager = Toolbox.GetInstance().GetTimeManager();
        timeManager.ResetTimer();

        currentLevel = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = 1f;
        isGameStart = false;
    }

    private void Update()
    {
        StartGame();
        ResetAndChangeLevel();
    }

    public void changeLevel(int selectedLevel)
    {
        SceneManager.LoadScene(selectedLevel);
    }

    private void StartGame()
    {
        if (!isGameStart && p1_Manager.isReady && p2_Manager.isReady)
        {
            Toolbox.GetInstance().GetTimeManager().StartCountDownTimer(countInTime);
            isGameStart = true;
        }
    }

    public void EndRound()
    {
        Time.timeScale = 0.25f;
        print("Game Over");
        isGameOver = true;
        timeManager.SetGameOverPanel();

        p1_Manager.DisplayScore();
        p1_Manager.pController.processInputs = false;

        p2_Manager.DisplayScore();
        p2_Manager.pController.processInputs = false;

    }


    public void ResetAndChangeLevel()
    {
        if (p1_Manager.isRestart && p2_Manager.isRestart && isGameOver == true)
        {
            p1_Manager.ResetPickups();
            p2_Manager.ResetPickups();

            SceneManager.LoadScene("gym_WidowPredatorPathfinding"); // resets current level for now

            isGameOver = false;
        }
    }
}

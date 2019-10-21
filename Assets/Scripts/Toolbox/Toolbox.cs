using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    private static Toolbox _instance;

    public static Toolbox GetInstance()
    {
        if (_instance == null)
        {
            var go = new GameObject("Toolbox");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<Toolbox>();
        }
        return _instance;
    }

    private TimeManager timeManager;
    private GameManager gameManager;

    private Player_Manager p1_playerManager;
    private Player_Manager p2_playerManager;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        timeManager = gameObject.AddComponent<TimeManager>();
        gameManager = gameObject.AddComponent<GameManager>();

        p1_playerManager = gameObject.AddComponent<Player_Manager>();
        p1_playerManager.playerID = 1;

        p2_playerManager = gameObject.AddComponent<Player_Manager>();
        p2_playerManager.playerID = 2;


    }

    public TimeManager GetTimeManager()
    {
        return timeManager;
    }

    public GameManager GetGameManager()
    {
        return gameManager;
    }

    public Player_Manager GetPlayer_1_Manager()
    {
        return p1_playerManager;
    }

    public Player_Manager GetPlayer_2_Manager()
    {
        return p2_playerManager;
    }
}

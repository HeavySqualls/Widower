using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int gameScore,
        gameDeathCount,
        gameBugEaten,
        numberOfplayers;//for knowing if it is multi player
    
    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            var go = new GameObject("GameManager");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<GameManager>();
        }
        return _instance;
    }


    //private SceneController sceneManager;
    //private PlayerManager playerManager;
    private Timer timer;

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

        //var go = new GameObject("SceneManager");
        //this.sceneManager = go.AddComponent<SceneController>();

        //var go2 = new GameObject("PlayerManager");
        //this.playerManager = go2.AddComponent<PlayerManager>();

        var go3 = new GameObject("Timer");
        timer = go3.AddComponent<Timer>();
    }

    //public SceneController GetSceneManager()
    //{
    //    return this.sceneManager;
    //}

    //public PlayerManager GetPlayerManager()
    //{
    //    return this.playerManager;
    //}

    public Timer GetTimer()
    {
        return timer;
    }
}

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
    }

    public TimeManager GetTimer()
    {
        return timeManager;
    }

    public GameManager GetGameManager()
    {
        return gameManager;
    }
}

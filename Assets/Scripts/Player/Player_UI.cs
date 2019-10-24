using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [Space]
    [Header("Player Ready/Controls:")]
    public GameObject readyPanel;
    public Canvas controlSelectionCanvas;
    public Button controler;
    public Button keyMouse;
    public GameObject winnerPanel;

    [Space]
    [Header("Player Score:")]
    public bool statPanelActive = false;
    public GameObject statusPanel;
    public Text level;
    public Text death;
    public Text points;
    public Text eatSpeed;
    public Text moveSpeed;
    public Button respawnButton;
    public GameObject respawnButtonObject;
    public Button restartButton;
    public GameObject restartButtonObject;

    //TODO: Link up players current pick ups and stats to display
    [Space]
    [Header("Player UI:")]
    public bool runWindDown = false;
    public Canvas playerUICanvas;
    public Image staminaBar;
    public Text eatSpCurrent;
    public Text eatSpNext;
    public Text moveSpCurrent;
    public Text moveSpNext;
    public Text pointsCurrent;
    public Text pointsNext;

    [Space]
    [Header("UI Refs:")]
    private Player_Controller pCon;
    private Player_Manager pMan;
    private GameManager gMan;

    private void Start()
    {
        pCon = GetComponentInParent<Player_Controller>();
        pMan = pCon.playerManager;
        gMan = Toolbox.GetInstance().GetGameManager();

        restartButton = restartButtonObject.GetComponent<Button>();
        restartButton.onClick.AddListener(OKToRestart);
        restartButtonObject.SetActive(false);
        winnerPanel.SetActive(false);
        DisableStaminaBar();

        respawnButton = respawnButtonObject.GetComponent<Button>();
        respawnButton.onClick.AddListener(pCon.PlayerRespawn);

        controler.onClick.AddListener(Control_Gamepad);
        keyMouse.onClick.AddListener(Control_keyboardMouse);

        if (gMan.currentLevel != 2)
        {
            UpdateUI();
        }
    }

    void Update()
    {
        RunWindDown();

        if (pCon.playerManager.isWinner)
        {
            winnerPanel.SetActive(true);
        }
    }


    // ---- CONTROLLS ---- //

    public void Control_Gamepad()
    {
        pCon.isGamePad = true;
        controlSelectionCanvas.enabled = false;
    }
    public void Control_keyboardMouse()
    {
        pCon.isGamePad = false;
        controlSelectionCanvas.enabled = false;
    }


    // ---- STAT PANEL ---- //

    public void DisplayStatsPanel()
    {
        EnableStatPanel();

        if (gMan.isGameOver)
        {
            //eatSpNext.text = 0.ToString();
            //moveSpNext.text = 0.ToString();
            //pointsNext.text = 0.ToString();

            respawnButtonObject.SetActive(false);
            restartButtonObject.SetActive(true);
        }

        level.text = pMan.playerLevel.ToString();
        death.text = pMan.deathCause;
        points.text = pMan.points + " + " + pMan.pointsToAdd;
        eatSpeed.text = pMan.eatSpeed + " + " + pMan.eatSpeedToAdd;
        moveSpeed.text = pMan.moveSpeed + " + " + pMan.moveSpeedToAdd;
    }

    public void OKToRestart()
    {
        pCon.playerManager.isRestart = true;
    }

    public void EnableStatPanel()
    {
        statusPanel.SetActive(true);

        if (gMan.currentLevel != 2)
        {
            UpdateCurrentStats();
        }

        statPanelActive = true;
    }

    public void DisableStatPanel()
    {
        if (respawnButton.gameObject.activeSelf == false)
        {
            respawnButton.gameObject.SetActive(true);
        }

        statusPanel.SetActive(false);
        statPanelActive = false;
    }


    // ---- PLAYER UI ---- //

    public void UpdateCurrentStats()
    {
        eatSpCurrent.text = pMan.eatSpeed.ToString();
        moveSpCurrent.text = pMan.moveSpeed.ToString();
        pointsCurrent.text = pMan.points.ToString();
    }

    public void UpdateNextStats()
    {
        eatSpNext.text = pMan.eatPickups.ToString();
        moveSpNext.text = pMan.movePickups.ToString();
        pointsNext.text = pMan.pointPickups.ToString();
    }

    public void UpdateUI()
    {
        if (gMan.currentLevel != 2)
        {
            UpdateCurrentStats();
            UpdateNextStats();
        }
    }

    public void EnableStaminaBar()
    {
        staminaBar.enabled = true;
        staminaBar.fillAmount = (pCon.runTime / 2);
    }

    public void DisableStaminaBar()
    {
        runWindDown = false;
        pCon.runTime = 0;
        staminaBar.fillAmount = 0f;
        staminaBar.enabled = false;
    }

    // SPRINT WIND DOWN CALL & COROUTINE

    void RunWindDown()
    {
        if (pCon.runTime > 0 && runWindDown)
        {
            pCon.runTime -= 1 * Time.deltaTime;
            staminaBar.fillAmount = (pCon.runTime / 2);

            if (pCon.runTime <= 0 || pCon.isRunning)
            {
                staminaBar.enabled = false;
                runWindDown = false;
            }

            if (pCon.runTime <= 0)
            {
                DisableStaminaBar();
            }
        }
    }

    public void PlayerRunningCoolDown()
    {
        staminaBar.GetComponent<Image>().color = Color.white;
        StartCoroutine(IRunningCooldown());
    }

    public IEnumerator IRunningCooldown()
    {
        pCon.canRun = false;
        yield return new WaitForSeconds(pCon.playerManager.runCooldownSeconds);
        pCon.canRun = true;
        staminaBar.GetComponent<Image>().color = Color.yellow;
        staminaBar.enabled = false;
        Debug.Log("Running Ready!");
    }
}

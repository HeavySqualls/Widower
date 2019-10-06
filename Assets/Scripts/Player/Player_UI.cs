using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
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

    [Space]
    [Header("Player UI:")]
    public bool runWindDown = false;
    public Canvas playerUICanvas;
    public Image staminaBar;

    private Player_Controller pCon;

    private void Start()
    {
        pCon = GetComponentInParent<Player_Controller>();
        //DisableStatPanel();
        respawnButton.onClick.AddListener(pCon.PlayerRespawn);
    }

    void Update()
    {
        RunWindDown();
    }

    public void EnableStatPanel()
    {
        statusPanel.SetActive(true);
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


    // AFTER PLAYER LETS GO OF SPRINT BUTTON, 
    // THIS DECREASES THE STAMINA ON THE BAR UNTIL IT REACHES 0.

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


    // SPRINT WIND DOWN CALL & COROUTINE

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

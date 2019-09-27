using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidowController : MonoBehaviour
{
    public bool isEating = false;
    private bool isCoolDown = false;
    private float cooldownTime = 2f;

    private void Update()
    {
        if (isEating && !isCoolDown)
        {
            StartCoroutine(WidowEatCooldown());
            isCoolDown = true;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        OnPlayerHitWidow playerThatHitUs = other.gameObject.GetComponent<OnPlayerHitWidow>();

        if (playerThatHitUs != null)
        {
            playerThatHitUs.OnWidowHit(other, this);
        }
    }

    private IEnumerator WidowEatCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);

        isEating = false;
        isCoolDown = false;
    }
}

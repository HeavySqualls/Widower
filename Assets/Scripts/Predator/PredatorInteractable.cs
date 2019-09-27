using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PredatorInteractable : MonoBehaviour
{
    public abstract void OnPredatorHit(Collision hit, PredatorController predator);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WidowInteractable : MonoBehaviour
{
    public abstract void OnWidowHit(Collision hit, WidowController widow);
}

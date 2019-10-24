using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeSwitch : MonoBehaviour
{
    void Awake()
    {
        Toolbox.GetInstance().GetGameManager().isPrototype = true;
    }
}

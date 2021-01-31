using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [System.NonSerialized]
    public bool hasHiddenPart = false;


    public void AddSpaceshipPart()
    {
        hasHiddenPart = true;
    }

    public void RemoveSpaceshipPart()
    {
        hasHiddenPart = false;
    }
}

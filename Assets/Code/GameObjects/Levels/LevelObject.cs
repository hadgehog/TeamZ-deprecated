using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public float HP = 100.0f;
    public float HitImpuls = 0.5f;
    public bool IsDestructible = false;

    void OnTriggerEnter(Collider other)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public int Strength = 100;
	public int Weight = 100;
	public float HitImpuls = 0.5f;
    public bool IsDestructible = false;

	void OnTriggerEnter(Collider other)
    {

    }
}

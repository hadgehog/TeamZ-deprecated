using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
	public int Weight = 100;
	public int Strength = 100;
    public bool IsDestructible = false;

	private float hitImpuls = 0.5f;

	void OnTriggerEnter(Collider other)
    {

    }
}

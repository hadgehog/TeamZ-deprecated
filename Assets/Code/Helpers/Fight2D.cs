﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight2D : MonoBehaviour
{
	static GameObject GetNearTarget(Vector3 position, Collider2D[] array)
	{
		Collider2D current = null;
		float dist = Mathf.Infinity;

		foreach (Collider2D coll in array)
		{
			float curDist = Vector3.Distance(position, coll.transform.position);

			if (curDist < dist)
			{
				current = coll;
				dist = curDist;
			}
		}

		return (current != null) ? current.gameObject : null;
	}

	// bool allTargets - set true for Tail Stroke
	public static void Action(Vector2 point, float radius, int[] layers, bool allTargets, int damage, int impulse)
	{
		int finalLayerMask = 8;

		foreach(int layer in layers)
		{
			int layerMask = 1 << layer;
			finalLayerMask |= layerMask;
		}

		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, finalLayerMask);

		if (allTargets)		// hit all targets in radius
		{
			foreach (Collider2D hit in colliders)
			{
				if (hit != null)
				{
					if (hit.GetComponent<Enemy>() != null)
					{
						hit.GetComponent<Enemy>().TakeDamage(damage, impulse);
					}
					else if (hit.GetComponent<LevelObject>() != null)
					{
						hit.GetComponent<LevelObject>().TakeDamage(damage, impulse);
					}
				}
			}			
		}
		else	// hit concrete target
		{
			GameObject obj = GetNearTarget(point, colliders);

			if (obj != null)
			{
				if (obj.GetComponent<IDamageable>() != null)
				{
					obj.GetComponent<IDamageable>().TakeDamage(damage, impulse);
				}
			}
		}
	}
}

using System.Collections;
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
	public static void Action(Vector2 point, float radius, int[] layers, int damage, bool allTargets)
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
			Debug.Log("Fight2D::Action() - hit all targets in radius");

			foreach (Collider2D hit in colliders)
			{
				if (hit != null && hit.GetComponent<Enemy>() != null)
				{
					hit.GetComponent<Enemy>().TakeDamage(damage);
					Debug.Log(hit.name);
				}
			}			
		}
		else	// hit concrete target
		{
			Debug.Log("Fight2D::Action() - hit concrete target");

			GameObject obj = GetNearTarget(point, colliders);

			if (obj != null)
			{
				if (obj.GetComponent<Enemy>() != null)
				{
					obj.GetComponent<Enemy>().TakeDamage(damage);
				}
				else if (obj.GetComponent<LevelObject>() != null)
				{

				}

				Debug.Log("Fight2D::Action() - object founded");
				Debug.Log(obj.name);
			}
			else
			{
				Debug.Log("Fight2D::Action() - poshel nahui");
			}
		}
	}
}

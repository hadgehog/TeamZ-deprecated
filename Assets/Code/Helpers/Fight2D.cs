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
	public static void Action(Vector2 point, float radius, int layerMask, float damage, bool allTargets)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);

		// hit concrete target
		if (!allTargets)
		{
			GameObject obj = GetNearTarget(point, colliders);

			if(obj != null)
			{
				Debug.Log("Fight2D::Action() - Aim object founded and punched!!!");
			}
			else
			{
				Debug.Log("Fight2D::Action() - poshel nahui");
			}

		//	if (obj != null && obj.GetComponent<EnemyHP>())
		//	{
		//		obj.GetComponent<EnemyHP>().HP -= damage;
		//	}
			return;
		}

		// hit all targets in radius
		//foreach (Collider2D hit in colliders)
		//{
		//	if (hit.GetComponent<EnemyHP>())
		//	{
		//		hit.GetComponent<EnemyHP>().HP -= damage;
		//	}
		//}
	}
}

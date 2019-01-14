using UnityEngine;

public class Fight2D : MonoBehaviour
{
    private static GameObject GetNearTarget(Vector3 position, Collider2D[] array)
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

        foreach (int layer in layers)
        {
            int layerMask = 1 << layer;
            finalLayerMask |= layerMask;
        }

        var colliders = Physics2D.OverlapCircleAll(point, radius, finalLayerMask);

        if (allTargets)     // hit all targets in radius
        {
            foreach (Collider2D hit in colliders)
            {
                    if (hit?.GetComponent<IDamageable>() is IDamageable damageable)
                    {
                        damageable.TakeDamage(damage, impulse);
                    }
            }
        }
        else    // hit concrete target
        {
            var target = GetNearTarget(point, colliders);
            if (target?.GetComponent<IDamageable>() is IDamageable damageable)
            {
                damageable.TakeDamage(damage, impulse);
            }
        }
    }
}

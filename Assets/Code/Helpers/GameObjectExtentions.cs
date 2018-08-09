using UnityEngine;

public static class GameObjectExtentions
{
    public static void Activate(this MonoBehaviour monoBeahviour)
    {
        monoBeahviour.gameObject.SetActive(true);
    }

    public static void Deactivate(this MonoBehaviour monoBeahviour)
    {
        monoBeahviour.gameObject.SetActive(false);
    }
}
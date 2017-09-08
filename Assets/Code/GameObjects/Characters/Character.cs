using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public float Health;
    public float Armor;
    public float Damage;

    public void TakeDamage(float value)
    {
        float blockedDamage = this.Armor - value;

        if (blockedDamage >= 0)
        {
            this.Armor = blockedDamage;
        }
        else
        {
            this.Armor = 0;
            this.Health += blockedDamage;
        }

        if (this.Health <= 0)
        {
            this.Health = 0;

            Debug.Log("You are die!");
            SceneManager.LoadScene("Laboratory");
        }
    }

    public float MakeDamage()
    {
        return this.Damage;
    }

    public void TakeHealth(float value)
    {
        this.Health += value;

        if (this.Health > 100)
        {
            this.Health = 100.0f;
        }
    }

    public void TakeArmor(float value)
    {
        this.Armor += value;
    }
}
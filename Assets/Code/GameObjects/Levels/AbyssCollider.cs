using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssCollider : MonoBehaviour
{
    public int AbyssDepth;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var character = col.gameObject.GetComponent<Character>();

        if (character == null)
        {
            Debug.Log("Character not founded!!!");
            return;
        }

        character.TakeDamage(this.AbyssDepth);

        Debug.Log("Damage is taken! -500 damage. now you have " + character.Armor + " armor and " + character.Health + " health");
    }
}

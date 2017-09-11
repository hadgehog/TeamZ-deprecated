using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorKit : MonoBehaviour
{
    public int ArmorKitCapacity;

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

        character.TakeArmor(this.ArmorKitCapacity);

        Debug.Log("Armor is taken! +50 armor. now you have " + character.Armor + " armor");
    }
}

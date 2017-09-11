using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    public int FirstAidKitCapacity;

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

        character.TakeHealth(this.FirstAidKitCapacity);

        Debug.Log("First aid kit is taken! +80 health. now you have " + character.Health + " health");
    }
}

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
        var character = col.gameObject.GetComponentInParent<ICharacter>();

        if (character == null)
        {
            return;
        }

        character.TakeArmor(this.ArmorKitCapacity);
    }
}

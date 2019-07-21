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
        var character = col.gameObject.GetComponentInParent<ICharacter>();

        if (character == null)
        {
            Debug.Log("Character not founded!!!");
            return;
        }

        character.TakeHealth(this.FirstAidKitCapacity);
    }
}

using UnityEngine;

public class MutagenCapsule : MonoBehaviour
{
    public int MutagenDuration;

    // Start is called before the first frame update
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

        character.ApplyMutagen(this.MutagenDuration);
    }
}

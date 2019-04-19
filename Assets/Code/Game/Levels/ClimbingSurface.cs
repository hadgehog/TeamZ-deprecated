using UnityEngine;

public class ClimbingSurface : MonoBehaviour
{
	// TODO: add more types
	public enum ClimbingSurfaceType
	{
		Stairway,	// Lizard, Hedgehog
		Fence		// Lizard
	}

	public ClimbingSurfaceType Type;

	public Vector3 Position
	{
		get { return this.position.position; }
	}

	public Vector2 Size
	{
		get { return this.box.size; }
	}

	private Transform position;
	private BoxCollider2D box;

	// Start is called before the first frame update
	void Start()
    {
		this.position = this.GetComponent<Transform>();
		this.box = this.GetComponent<BoxCollider2D>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}

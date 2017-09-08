using UnityEngine;

public class DirectionalCamera : MonoBehaviour
{
    public float dampTime = 0.3f;
    public Transform target;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update ()
    {
        if (this.target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(new Vector3(this.target.position.x, this.target.position.y + 0.5f, this.target.position.z));
            Vector3 delta = new Vector3(this.target.position.x, this.target.position.y + 0.5f, this.target.position.z) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = this.transform.position + delta;

            this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
        }
    }
}
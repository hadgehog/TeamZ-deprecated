using System.Linq;
using Assets.Code.Helpers;
using GameSaving;
using UniRx;
using UnityEngine;

public class DirectionalCamera : MonoBehaviour
{
	public float dampTime = 0.3f;
	public Transform target;
	public UnityDependency<Main> Main;

	private Vector3 velocity = Vector3.zero;

	private void Start()
	{
		this.Main.Value.GameController.Loaded.Subscribe(_ => this.Loaded());
	}

	private void Loaded()
	{
		this.target = EntitiesStorage.Instance.Entities.Values.
			Where(o => o.GetComponent<Lizard>()).FirstOrDefault()?.transform;
	}

	private void Update()
	{
		if (this.target)
		{
			Vector3 point = this.GetComponent<Camera>().WorldToViewportPoint(new Vector3(this.target.position.x, this.target.position.y + 1.5f, this.target.position.z));
			Vector3 delta = new Vector3(this.target.position.x, this.target.position.y + 1.5f, this.target.position.z) - this.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = this.transform.position + delta;

			this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref this.velocity, this.dampTime);
		}
	}
}
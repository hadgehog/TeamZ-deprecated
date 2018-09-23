using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float rotationSpeed = 1;
	public bool useRandomBoost = true;
	public float randomBoostValue = 1;
	public int randomBoostSeed = 0;

	private Vector3 xAxis;
	private Transform currentTransform;
	private System.Random random;
	private Action update;

	private void Start()
	{
		this.xAxis = new Vector3(1, 0, 0);
		this.currentTransform = this.transform;

		if (this.useRandomBoost)
		{
			this.update = () => this.currentTransform.Rotate(this.xAxis, Math.Max(this.rotationSpeed, this.rotationSpeed + Mathf.Sin(Time.time) * this.randomBoostValue));
		}
		else
		{
			this.update = () => this.currentTransform.Rotate(this.xAxis, this.rotationSpeed);
		}

		this.random = new System.Random(this.randomBoostSeed);
	}

	private void Update()
	{
		this.update();
	}
}

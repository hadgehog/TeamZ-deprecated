using GameSaving;
using GameSaving.States;
using UnityEngine;
using UniRx;
using GameSaving.MonoBehaviours;
using System.Linq;
using System;
using System.Collections;

public class DirectionalCamera : MonoBehaviour
{
    public float dampTime = 0.3f;
    public Transform target;
    public Main Main;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        this.Main.GameController.Loaded.Subscribe(_ => Loaded());
        this.Loaded();
    }

    private void Loaded()
    {
        this.target = EntitiesStorage.Instance.Entities.Values.
            Where(o => o.GetComponent<Lizard>()).FirstOrDefault().transform;
    }

    void Update()
    {
        if (this.target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(new Vector3(this.target.position.x, this.target.position.y + 1.5f, this.target.position.z));
            Vector3 delta = new Vector3(this.target.position.x, this.target.position.y + 1.5f, this.target.position.z) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = this.transform.position + delta;

            this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
        }
    }
}
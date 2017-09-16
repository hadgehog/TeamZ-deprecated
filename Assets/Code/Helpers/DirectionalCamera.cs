using GameSaving;
using GameSaving.States;
using UnityEngine;
using UniRx;
using GameSaving.MonoBehaviours;
using System.Linq;
using System;

public class DirectionalCamera : MonoBehaviourWithState<CameraState>
{
    public float dampTime = 0.3f;
    public Transform target;

    private GameController<GameState> gameController;
    private Guid playerId;
    private Vector3 velocity = Vector3.zero;

    public override CameraState GetState()
    {
        this.playerId = this.target.GetComponent<Entity>().Id;

        return new CameraState
        {
            PlayerId = this.playerId,
            Position = this.transform.localPosition
        };
    }

    public override void Loaded()
    {
        var player = EntitiesStorage.Instance.Entities[this.playerId];
        this.target = player.gameObject.transform;
    }

    public override void SetState(CameraState state)
    {
        this.playerId = state.PlayerId;
        this.transform.localPosition = state.Position;
    }

    void Update()
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
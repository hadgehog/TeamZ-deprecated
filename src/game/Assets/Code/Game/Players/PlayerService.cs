using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Code.Helpers;
using GameSaving;
using GameSaving.MonoBehaviours;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.Code.Game.Characters;
using TeamZ.Assets.Code.Game.Levels;
using TeamZ.Assets.Code.Game.Messages.GameSaving;
using TeamZ.Assets.Code.Game.UserInput;
using TeamZ.Assets.GameSaving.Interfaces;
using UniRx;
using UnityEngine;
using ZeroFormatter;

namespace TeamZ.Assets.Code.Game.Players
{
    [ZeroFormattable]
    public class PlayerServiceState : State
    {
        public override StateKind Kind => StateKind.PlayerService;

        [Index(0)]
        public virtual Guid? FirstPlayerEntityId { get; set; }

        [Index(1)]
        public virtual Guid? SecondPlayerEntityId { get; set; }
    }


    public class PlayerService : StateProvider<PlayerServiceState>
    {

        UnityDependency<FirstPlayer> FirstPlayer;
        UnityDependency<SecondPlayer> SecondPlayer;
        UnityDependency<DirectionalCamera> Camera;

        Dependency<EntitiesStorage> EntitiesStorage;
        Dependency<UserInputMapper> UserInputMapper;

        public PlayerService()
        {
            this.UserInputMapper.Value.UserInputProviders[KeyMapping.CombinedFirst].Start
                .True()
                .HoldFor(TimeSpan.FromSeconds(3))
                .Subscribe(_ => this.HandlePlayerActivation(this.FirstPlayer.Value, this.SecondPlayer.Value));

            this.UserInputMapper.Value.UserInputProviders[KeyMapping.CombinedSecond].Start
                .True()
                .HoldFor(TimeSpan.FromSeconds(3))
                .Subscribe(_ => this.HandlePlayerActivation(this.SecondPlayer.Value, this.FirstPlayer.Value));
        }

        private void HandlePlayerActivation(Player activatedPlayer, Player anotherPlayer)
        {
            if (!activatedPlayer)
            {
                var characterController = anotherPlayer.GetComponent<CharacterControllerScript>();
                var newCharacterDescriptor = characterController is LizardController ?
                    Characters.Characters.Hedgehog :
                    Characters.Characters.Lizard;

                this.AddPlayer(newCharacterDescriptor, anotherPlayer.transform.localPosition);
                return;
            }

            if (activatedPlayer)
            {
                this.RemovePlayer(activatedPlayer);
            }
        }

        private void RemovePlayer(Player player)
        {
            GameObject.Destroy(player.gameObject);
        }

        public void AddPlayer(CharacterDescriptor characterDescriptor, Vector3 localPosition)
        {
            var root = this.EntitiesStorage.Value.Root.transform;
            var characterTemplate = Resources.Load<GameObject>(characterDescriptor.Path);

            var player = characterTemplate.Spawn(localPosition, root);
            if (!this.FirstPlayer)
            {
                player.gameObject.AddComponent<FirstPlayer>();
            }
            else
            {
                player.gameObject.AddComponent<SecondPlayer>();
            }

            this.UserInputMapper.Value.Bootstrap();
        }

        public override PlayerServiceState GetState() => new PlayerServiceState
        {
            FirstPlayerEntityId = this.FirstPlayer.Value?.GetComponent<Entity>()?.Id,
            SecondPlayerEntityId = this.SecondPlayer.Value?.GetComponent<Entity>()?.Id,
        };

        public override void SetState(PlayerServiceState state)
        {
            if (state.FirstPlayerEntityId.HasValue &&
                this.EntitiesStorage.Value.Entities.TryGetValue(state.FirstPlayerEntityId.Value, out var firstPlayer))
            {
                firstPlayer.gameObject.AddComponent<FirstPlayer>();
            }

            if (state.SecondPlayerEntityId.HasValue &&
                this.EntitiesStorage.Value.Entities.TryGetValue(state.SecondPlayerEntityId.Value, out var secondPlayer))
            {
                secondPlayer.gameObject.AddComponent<SecondPlayer>();
            }

            this.UserInputMapper.Value.Bootstrap();
        }
    }
}

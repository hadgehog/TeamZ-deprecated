using GameSaving;
using GameSaving.Interfaces;
using GameSaving.MonoBehaviours;
using GameSaving.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.GameSaving.Interfaces;
using UnityEngine;
using ZeroFormatter;

namespace TeamZ.Assets.Code.Game.UserInput
{
    [ZeroFormattable]
    public class UserInputMapperState : State
    {
        public override StateKind Kind
            => StateKind.UserInputMapper;

        [Index(0)]
        public virtual Guid FirstPlayer { get; set; }

        [Index(1)]
        public virtual Guid SecondPlayer { get; set; }
    }


    public class UserInputMapper : StateProvider<UserInputMapperState>
    {
        public enum KeyMapping
        {
            KeyboardFirst,
            KeyboardSecond,
            JoystickFirst,
            JoystickSecond,
            CombinedFirst,
            CombinedSecond
        }

        public CharacterControllerScript FirstPlayer { get; private set; }

        public CharacterControllerScript SecondPlayer { get; private set; }

        public Dictionary<KeyMapping, IUserInputProvider> UserInputProviders { get; }
            = new Dictionary<KeyMapping, IUserInputProvider>()
            {
                { KeyMapping.KeyboardFirst,  new UserInputProvider("Horizontal1", "Vertical1", "Jump1", "Punch1", "Kick1", "Activate1", "Start1", "Cancel1") },
                { KeyMapping.KeyboardSecond,  new UserInputProvider("Horizontal2", "Vertical2", "Jump2", "Punch2", "Kick2", "Activate2", "Start2", "Cancel2") },

                { KeyMapping.JoystickFirst,  new UserInputProvider("HorizontalJoystic1", "VerticalJoystic1", "JumpJoystic1", "PunchJoystic1", "KickJoystic1", "ActivateJoystic1", "StartJoystic1", "CancelJoystic1") },
                { KeyMapping.JoystickSecond,  new UserInputProvider("HorizontalJoystic2", "VerticalJoystic2", "JumpJoystic2", "PunchJoystic2", "KickJoystic2", "ActivateJoystic2", "StartJoystic2","CancelJoystic2") },
            };

        public UserInputMapper()
        {
            this.UserInputProviders[KeyMapping.CombinedFirst] = new CombinedUserInputProvider(
                this.UserInputProviders[KeyMapping.KeyboardFirst],
                this.UserInputProviders[KeyMapping.JoystickFirst]);

            this.UserInputProviders[KeyMapping.CombinedSecond] = new CombinedUserInputProvider(
                this.UserInputProviders[KeyMapping.KeyboardSecond],
                this.UserInputProviders[KeyMapping.JoystickSecond]);
        }

        public void Bootstrap()
        {
            var characters = GameObject.FindObjectsOfType<CharacterControllerScript>();
            if (!characters.Any())
            {
                return;
            }
            
            this.FirstPlayer = characters.First();
            if (characters.Length > 1)
            {
                this.SecondPlayer = characters.Last();
            }

            this.Map();
        }

        private void Map()
        {
            var userInputProvider = this.UserInputProviders[KeyMapping.CombinedFirst];
            userInputProvider.StartMonitoring();

            this.FirstPlayer.UserInputProvider.Value = userInputProvider;

            userInputProvider = this.UserInputProviders[KeyMapping.CombinedSecond];
            userInputProvider.StartMonitoring();
            this.SecondPlayer.UserInputProvider.Value = userInputProvider;
        }

        public override UserInputMapperState GetState()
        {
            return new UserInputMapperState
            {
                FirstPlayer = this.FirstPlayer.GetComponent<Entity>().Id,
                SecondPlayer = this.SecondPlayer?.GetComponent<Entity>().Id ?? Guid.Empty,
            };
        }

        public override void SetState(UserInputMapperState state)
        {
            var entityStorage = Dependency<EntitiesStorage>.Resolve();
            this.FirstPlayer = entityStorage.Entities[state.FirstPlayer].GetComponent<CharacterControllerScript>();

            if (state.SecondPlayer != Guid.Empty)
            {
                this.SecondPlayer = entityStorage.Entities[state.SecondPlayer].GetComponent<CharacterControllerScript>();
            }

            this.Map();
        }
    }
}

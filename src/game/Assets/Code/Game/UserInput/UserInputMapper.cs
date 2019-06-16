using Assets.Code.Helpers;
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
using TeamZ.Assets.Code.Game.Characters;
using TeamZ.Assets.Code.Game.Messages.GameSaving;
using TeamZ.Assets.Code.Game.Players;
using TeamZ.Assets.GameSaving.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using ZeroFormatter;

namespace TeamZ.Assets.Code.Game.UserInput
{
    public class UserInputMapper : IDisposable
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

        public UnityDependency<FirstPlayer> FirstPlayer { get; }
        public UnityDependency<SecondPlayer> SecondPlayer { get; }

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
            var combinedUserInputProvider = new CombinedUserInputProvider(
                this.UserInputProviders[KeyMapping.KeyboardFirst],
                this.UserInputProviders[KeyMapping.JoystickFirst]);
            combinedUserInputProvider.StartMonitoring();

            this.UserInputProviders[KeyMapping.CombinedFirst] = combinedUserInputProvider;

            combinedUserInputProvider = new CombinedUserInputProvider(
                this.UserInputProviders[KeyMapping.KeyboardSecond],
                this.UserInputProviders[KeyMapping.JoystickSecond]);
            combinedUserInputProvider.StartMonitoring();

            this.UserInputProviders[KeyMapping.CombinedSecond] = combinedUserInputProvider;
        }

        public void Bootstrap()
        {
            this.Map();
        }

        private void Map()
        {
            if (this.FirstPlayer)
            {
                var userInputProvider = this.UserInputProviders[KeyMapping.CombinedFirst];
                this.FirstPlayer.Value.GetComponent<CharacterControllerScript>().UserInputProvider.Value = userInputProvider;
            }

            if (this.SecondPlayer)
            {
                var userInputProvider = this.UserInputProviders[KeyMapping.CombinedSecond];
                this.SecondPlayer.Value.GetComponent<CharacterControllerScript>().UserInputProvider.Value = userInputProvider;
            }
        }

        public void Dispose()
        {
            foreach (var provider in this.UserInputProviders.Values)
            {
                provider.Dispose();
            }
        }
    }
}

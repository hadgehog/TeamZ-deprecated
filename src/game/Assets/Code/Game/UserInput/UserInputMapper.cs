using GameSaving.Interfaces;
using GameSaving.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.UserInput
{
    public class UserInputMapper
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

            var userInputProvider = this.UserInputProviders[KeyMapping.CombinedFirst];
            userInputProvider.StartMonitoring();

            characters.First().UserInputProvider.Value = userInputProvider;

            if (characters.Length < 2)
            {
                return;
            }

            userInputProvider = this.UserInputProviders[KeyMapping.CombinedSecond];
            userInputProvider.StartMonitoring();
            characters.Last().UserInputProvider.Value = userInputProvider;
        }
    }
}

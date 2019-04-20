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
            First,
            Second
        }

        public static Dictionary<KeyMapping, IUserInputProvider> UserInputProviders
            = new Dictionary<KeyMapping, IUserInputProvider>()
            {
                { KeyMapping.First,  new UserInputProvider("HorizontalJoystic1", "VerticalJoystic1", "JumpJoystic1", "PunchJoystic1", "KickJoystic1", "SubmitJoystic1", "CancelJoystic1") },
                { KeyMapping.Second,  new UserInputProvider("Horizontal2", "Vertical2", "Jump2", "Punch2", "Kick2", "Submit2", "Cancel2") }
            };


        public void Bootstrap()
        {
            var characters = GameObject.FindObjectsOfType<CharacterControllerScript>();
            if (!characters.Any())
            {
                return;
            }

            var userInputProvider = UserInputProviders[KeyMapping.First];
            userInputProvider.Activate();

            characters.First().UserInputProvider.Value = userInputProvider;

            if (characters.Length < 2)
            {
                return;
            }

            userInputProvider = UserInputProviders[KeyMapping.Second];
            userInputProvider.Activate();
            characters.Last().UserInputProvider.Value = userInputProvider;
        }
    }
}

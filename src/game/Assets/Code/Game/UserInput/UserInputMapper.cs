﻿using GameSaving.Interfaces;
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
            JoystickSecond
        }

        public static Dictionary<KeyMapping, IUserInputProvider> UserInputProviders
            = new Dictionary<KeyMapping, IUserInputProvider>()
            {
                { KeyMapping.KeyboardFirst,  new UserInputProvider("Horizontal1", "Vertical1", "Jump1", "Punch1", "Kick1", "Submit1", "Cancel1") },
                { KeyMapping.KeyboardSecond,  new UserInputProvider("Horizontal2", "Vertical2", "Jump2", "Punch2", "Kick2", "Submit2", "Cancel2") },

                { KeyMapping.JoystickFirst,  new UserInputProvider("HorizontalJoystic1", "VerticalJoystic1", "JumpJoystic1", "PunchJoystic1", "KickJoystic1", "SubmitJoystic1", "CancelJoystic1") },
                { KeyMapping.JoystickSecond,  new UserInputProvider("HorizontalJoystic2", "VerticalJoystic2", "JumpJoystic2", "PunchJoystic2", "KickJoystic2", "SubmitJoystic2", "CancelJoystic2") },
            };


        public void Bootstrap()
        {
            var characters = GameObject.FindObjectsOfType<CharacterControllerScript>();
            if (!characters.Any())
            {
                return;
            }

            var userInputProvider = UserInputProviders[KeyMapping.KeyboardFirst];
            userInputProvider.Activate();

            characters.First().UserInputProvider.Value = userInputProvider;

            if (characters.Length < 2)
            {
                return;
            }

            userInputProvider = UserInputProviders[KeyMapping.JoystickFirst];
            userInputProvider.Activate();
            characters.Last().UserInputProvider.Value = userInputProvider;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamZ.Assets.Code.Game.Characters
{
    public static class Characters
    {
        public class CharacterDescriptor
        {
            public string Path { get; set; }
            public Type ControllerType { get; set; }
        }

        public static CharacterDescriptor Lizard { get; }
            = new CharacterDescriptor
            {
                Path = @"Prefabs\Characters\Lizard",
                ControllerType = typeof(LizardController)
            };

        public static CharacterDescriptor Hedgehog { get; }
            = new CharacterDescriptor
            {
                Path = @"Prefabs\Characters\Hedgehog",
                ControllerType = typeof(HedgehogController)
            };
    }
}

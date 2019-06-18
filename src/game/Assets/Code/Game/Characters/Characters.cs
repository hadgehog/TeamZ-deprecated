using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamZ.Assets.Code.Game.Characters
{
    public class CharacterDescriptor
    {
        public string Path { get; set; }
        public Type ControllerType { get; set; }
        public string Name { get; set; }
    }

    public static class Characters
    {
        public static CharacterDescriptor Lizard { get; }
            = new CharacterDescriptor
            {
                Name = nameof(Lizard),
                Path = @"Prefabs\Characters\Lizard",
                ControllerType = typeof(LizardController)
            };

        public static CharacterDescriptor Hedgehog { get; }
            = new CharacterDescriptor
            {
                Name = nameof(Hedgehog),
                Path = @"Prefabs\Characters\Hedgehog",
                ControllerType = typeof(HedgehogController)
            };

        public static CharacterDescriptor[] Descriptors { get; }
            = new []
            {
                Lizard,
                Hedgehog
            };
    }
}

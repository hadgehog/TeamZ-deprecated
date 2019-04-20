using GameSaving.MonoBehaviours;
using GameSaving.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFormatter;

namespace TeamZ.Assets.GameSaving.States
{
	[ZeroFormattable]
    public class CharacterControllerState : MonoBehaviourState
	{
		[Obsolete]
		[Index(0)]
		public virtual CharacterControllerScript.Direction CurrentDirection { get; set; }

		[Index(1)]
        public virtual bool IsClimbed { get; set; }

		[Obsolete]
        [Index(2)]
        public virtual bool IsKeyUpWasPressed { get; set; }

		public override MonoBehaviourStateKind Type { get; } = MonoBehaviourStateKind.CharacterController;
    }
}

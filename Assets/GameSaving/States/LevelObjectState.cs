using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSaving.States;
using ZeroFormatter;

namespace TeamZ.Assets.GameSaving.States
{
	[ZeroFormattable]
	public class LevelObjectState : MonoBehaviourState
	{
		public override MonoBehaviourStateKind Type => MonoBehaviourStateKind.LevelObject;

		[Index(0)]
		public virtual int Strength { get; set; }

		[Index(1)]
		public virtual bool IsDestructible { get; set; }
	}
}

using System;
using System.Collections.Generic;
using TeamZ.Assets.Code.Game.Players;
using TeamZ.Assets.Code.Game.UserInput;
using ZeroFormatter;

namespace GameSaving.States
{
    [ZeroFormattable]
    public class GameState
    {
        public GameState()
        {
        }

        [Index(0)]
        public virtual Guid LevelId
        {
            get;
            set;
        }

        [Index(1)]
        public virtual IEnumerable<GameObjectState> GameObjectsStates
        {
            get;
            set;
        }

        [Index(2)]
        public virtual HashSet<Guid> VisitedLevels
        {
            get;
            set;
        } = new HashSet<Guid>();


        [Index(3)]
        public virtual PlayerServiceState PlayerServiceState
        {
            get;
            set;
        }
    }
}
using System.Collections.Generic;
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
        public virtual IEnumerable<GameObjectState> GameObjectsStates
        {
            get;
            set;
        }
    }
}
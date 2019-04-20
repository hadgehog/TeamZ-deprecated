using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSaving;

namespace TeamZ.Assets.Code.Game.Messages.GameSaving
{
    public class GameSaved
    {
    }

    public class GameLoaded
    {
    }

    public class LoadGameRequest
    {
        public string SlotName { get; set; }
    }
}

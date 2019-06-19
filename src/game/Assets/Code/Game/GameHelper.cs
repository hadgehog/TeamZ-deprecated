using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TeamZ.Assets.Code.Game
{
    public static class GameHelper
    {
        public static bool IsPaused => Time.timeScale < 0.1;
        public static bool IsActive => !IsPaused;
    }
}

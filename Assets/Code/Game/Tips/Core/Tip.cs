using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.Tips.Core
{
    public abstract class Tip : MonoBehaviour
    {
        public abstract void Activate();
        public abstract void Deactivate();
    }
}

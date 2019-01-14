using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamZ.Assets.Code.GameObjects.Levels
{
    public class DestorableLevelObject : LevelObject
    {
        public override void StrengthTooLow()
        {
            Destroy(this.gameObject);
        }
    }
}

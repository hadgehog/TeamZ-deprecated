using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ZeroFormatter;

namespace GameSaving.States
{
    [ZeroFormattable]
    public class CameraState : MonoBehaviourState
    {
        [Index(0)]
        public virtual Guid PlayerId { get; set; }

        [Index(1)]
        public virtual Vector3 Position { get; set; }

        public override MonoBehaviourStateKind Type
        {
            get
            {
                return MonoBehaviourStateKind.Camera;
            }
        }
    }
}

using System;
using ZeroFormatter;

namespace GameSaving.States
{
    [ZeroFormattable]
    public class EntityState : MonoBehaviourState
    {
        [Index(0)]
        public virtual Guid Id { get; set; }

        [Index(1)]
        public virtual string Path { get; set; }

        [Index(2)]
        public virtual UnityEngine.Vector3 Scale { get; set; }

        [Index(3)]
        public virtual UnityEngine.Quaternion Rotation { get; set; }

        [Index(4)]
        public virtual UnityEngine.Vector3 Position { get; set; }

        public override MonoBehaviourStateKind Type
        {
            get
            {
                return MonoBehaviourStateKind.Entity;
            }
        }
    }
}
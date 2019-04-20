using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TeamZ.Assets.Effects
{
    public class Jumper : MonoBehaviour
    {
        private Vector3 startLocationPosition;
        private float randomShift;
        public float InitialOffset;
        public float JumpHeight;
        public float Speed;

        private void Start()
        {
            this.startLocationPosition = this.transform.localPosition;
            this.randomShift = UnityEngine.Random.value;
        }

        private void Update()
        {
            var localPosition = this.startLocationPosition;
            localPosition.y = localPosition.y + this.InitialOffset + Mathf.Cos((Time.time + this.randomShift)* this.Speed) * this.JumpHeight;

            this.transform.localPosition = localPosition;
        }
    }
}

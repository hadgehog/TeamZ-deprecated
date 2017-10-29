using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameObjects.Activation.Core
{
    public class DebugActivation : MonoBehaviour, IActivable
    {
        public void Activate()
        {
            Debug.Log("yo");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamZ.Assets.Code.Game.Tips.Core;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.Tips
{
    public class TipsBroker : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Tip>() is Tip tip)
            {
                tip.Activate();
            }    
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<Tip>() is Tip tip)
            {
                tip.Deactivate();
            }
        }
    }
}

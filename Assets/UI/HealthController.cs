using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSaving;
using GameSaving.States;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using GameSaving.MonoBehaviours;
using Assets.Code.Helpers;

public class HealthController : MonoBehaviour
{
    public Text Text;
    UnityDependency<Lizard> character;

    void Update()
    {
        if (this.character)
        {
            this.Text.text = $"Health: {this.character.Value.Health.ToString()} Armor: {this.character.Value.Armor.ToString()}";
        }
    }
}

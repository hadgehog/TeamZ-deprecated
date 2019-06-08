using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.UI;
using GameSaving;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.Code.Game.Characters;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionView : View
{
    Dependency<GameController> GameController;

    private void OnEnable()
    {
        Observable.NextFrame().Subscribe(_ => Selectable.allSelectables.First().Select());
    }

    public async void Lizard()
    {
        await this.GameController.Value.StartNewGameAsync(Characters.Lizard);
    }

    public async void Hedgehog()
    {
        await this.GameController.Value.StartNewGameAsync(Characters.Hedgehog);
    }
}

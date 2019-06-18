using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Helpers;
using Assets.UI;
using GameSaving;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.Code.Game.Characters;
using TeamZ.Assets.Code.Game.Notifications;
using TeamZ.Assets.Code.Game.UserInput;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionView : View
{
    Dependency<GameController> GameController;
    Dependency<UserInputMapper> UserInputMapper;
    UnityDependency<NotificationService> NotificationService;

    public CharacterDescriptor firstUserSelection;
    public CharacterDescriptor secondUserSelection;

    private void OnEnable()
    {
        Observable.NextFrame().Subscribe(_ => Selectable.allSelectables.First().Select());

        this.UserInputMapper.Value.FirstUserInputProvider.Horizontal
            .Skip(0)
            .Where(o => o < -0.5f || o > 0.5f)
            .Select(o => o < 0 ? Characters.Lizard : Characters.Hedgehog)
            .Subscribe(o =>
            {
                this.firstUserSelection = o;
                this.NotificationService.Value.ShowShortMessage($"First player selectecs {o.Name}");
            })
            .AddTo(this);

        this.UserInputMapper.Value.SecondUserInputProvider.Horizontal
            .Skip(0)
            .Where(o => o < -0.5f || o > 0.5f)
            .Select(o => o < 0 ? Characters.Lizard : Characters.Hedgehog)
            .Subscribe(o =>
            {
                this.secondUserSelection = o;
                this.NotificationService.Value.ShowShortMessage($"Second player selectecs {o.Name}");
            })
            .AddTo(this);

        this.UserInputMapper.Value.FirstUserInputProvider.Start
            .Concat(this.UserInputMapper.Value.SecondUserInputProvider.Start)
            .HoldFor(TimeSpan.FromSeconds(3))
            .Subscribe(async _ =>
            {
                if (this.firstUserSelection is null && this.secondUserSelection is null)
                {
                    this.NotificationService.Value.ShowShortMessage("Selecte some character");
                    return;
                }

                if (this.firstUserSelection == this.secondUserSelection)
                {
                    this.NotificationService.Value.ShowShortMessage("Same character are not allowed");
                    return;
                }

                var selectedCharacters = new[] { this.firstUserSelection, this.secondUserSelection }.Where(o => o != null).ToArray();
                await this.GameController.Value.StartNewGameAsync(selectedCharacters);
            })
            .AddTo(this);

    }

    public async void Lizard()
    {
        //await this.GameController.Value.StartNewGameAsync(Characters.Lizard);
    }

    public async void Hedgehog()
    {
        //await this.GameController.Value.StartNewGameAsync(Characters.Hedgehog);
    }
}

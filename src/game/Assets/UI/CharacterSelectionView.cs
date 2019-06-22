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

    public CharacterDescriptor firstUserSelection = null;
    public CharacterDescriptor secondUserSelection = null;

    private CharacterDescriptor[] selectedCharacters;

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
            .Merge(this.UserInputMapper.Value.SecondUserInputProvider.Start)
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

                this.selectedCharacters = new[] { this.firstUserSelection, this.secondUserSelection }.Where(o => o != null).ToArray();
                await this.GameController.Value.StartNewGameAsync(this.selectedCharacters);
            })
            .AddTo(this);

    }

    public void SelectLizard(bool isActive)
    {
        if (isActive)
        {
            if (this.firstUserSelection == null)
            {
                this.firstUserSelection = Characters.Lizard;
                this.NotificationService.Value.ShowShortMessage($"First player selectecs {this.firstUserSelection.Name}");
            }
            else if (this.secondUserSelection == null)
            {
                this.secondUserSelection = Characters.Lizard;
                this.NotificationService.Value.ShowShortMessage($"Second player selectecs {this.secondUserSelection.Name}");
            }
        }
        else
        {
            if (this.firstUserSelection == Characters.Lizard)
            {
                this.firstUserSelection = null;
            }

            if (this.secondUserSelection == Characters.Lizard)
            {
                this.secondUserSelection = null;
            }
        }
    }

    public void SelectHedgehog(bool isActive)
    {
        if (isActive)
        {
            if (this.firstUserSelection == null)
            {
                this.firstUserSelection = Characters.Hedgehog;
                this.NotificationService.Value.ShowShortMessage($"First player selectecs {this.firstUserSelection.Name}");
            }
            else if (this.secondUserSelection == null)
            {
                this.secondUserSelection = Characters.Hedgehog;
                this.NotificationService.Value.ShowShortMessage($"Second player selectecs {this.secondUserSelection.Name}");
            }
        }
        else
        {
            if (this.firstUserSelection == Characters.Hedgehog)
            {
                this.firstUserSelection = null;
            }

            if (this.secondUserSelection == Characters.Hedgehog)
            {
                this.secondUserSelection = null;
            }
        }
    }

    public async void StartGame()
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

        this.selectedCharacters = new[] { this.firstUserSelection, this.secondUserSelection }.Where(o => o != null).ToArray();

        await this.GameController.Value.StartNewGameAsync(this.selectedCharacters);
    }
}

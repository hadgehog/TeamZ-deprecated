using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSaving;
using GameSaving.States;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using GameSaving.MonoBehaviours;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Code.Helpers;
using Assets.UI;

public class HUDController : MonoBehaviour
{
	public Slider HealthSlider;
	public Slider ArmorSlider;

	private Dependency<Lizard> character;

    void Update()
    {
        if (this.character != null)
        {
			this.HealthSlider.value = this.character.Value.Health;
			this.ArmorSlider.value = this.character.Value.Armor;
		}
    }
}

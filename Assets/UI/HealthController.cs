using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSaving;
using GameSaving.States;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using GameSaving.MonoBehaviours;

public class HealthController : MonoBehaviour
{
    public Main Main;
    public Text Text;
    private Lizard character;

    void Start()
    {
        this.Main.GameController.Loaded.Subscribe(_ => Loaded());
        this.Loaded();
    }

    public void Loaded()
    {
        this.character = EntitiesStorage.Instance.Entities.Values.Where(o => o.GetComponent<Lizard>() != null).FirstOrDefault()?.GetComponent<Lizard>();
    }

    void Update()
    {
        if (this.character)
        {
            this.Text.text = $"Health: {this.character.Health.ToString()} Armor: {this.character.Armor.ToString()}";
        }
    }
}

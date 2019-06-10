using Assets.Code.Helpers;
using Assets.UI;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : View
{
    public Slider HealthSliderPlayer1;
    public Slider ArmorSliderPlayer1;

    public Slider HealthSliderPlayer2;
    public Slider ArmorSliderPlayer2;

    private UnityDependency<Lizard> lizardCharacter;
    private UnityDependency<Hedgehog> hedgehogCharacter;

    private GameObject hudInfoBarPlayer1;
    private GameObject hudInfoBarPlayer2;

    private void Start()
    {
        this.hudInfoBarPlayer1 = GameObject.Find("HUDInfoBarPlayer1");
        this.hudInfoBarPlayer2 = GameObject.Find("HUDInfoBarPlayer2");
    }

    private void Update()
    {
        if (this.lizardCharacter)
        {
            if (!this.hudInfoBarPlayer1.activeSelf)
            {
                this.hudInfoBarPlayer1.SetActive(true);
            }

            this.HealthSliderPlayer1.value = this.lizardCharacter.Value.Health;
            this.ArmorSliderPlayer1.value = this.lizardCharacter.Value.Armor;
        }
        else if (this.hudInfoBarPlayer1.activeSelf)
        {
            this.hudInfoBarPlayer1.SetActive(false);
        }

        if (this.hedgehogCharacter)
        {
            if (!this.hudInfoBarPlayer2.activeSelf)
            {
                this.hudInfoBarPlayer2.SetActive(true);
            }

            this.HealthSliderPlayer2.value = this.hedgehogCharacter.Value.Health;
            this.ArmorSliderPlayer2.value = this.hedgehogCharacter.Value.Armor;
        }
        else if (this.hudInfoBarPlayer2.activeSelf)
        {
            this.hudInfoBarPlayer2.SetActive(false);
        }
    }
}

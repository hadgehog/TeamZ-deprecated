using Assets.Code.Helpers;
using Assets.UI;
using UnityEngine.UI;

public class HUDController : View
{
	public Slider HealthSliderPlayer1;
	public Slider ArmorSliderPlayer1;

    public Slider HealthSliderPlayer2;
    public Slider ArmorSliderPlayer2;

    private UnityDependency<Lizard> lizardCharacter;
    private UnityDependency<Hedgehog> hedgehogCharacter;

    void Update()
	{
		if (this.lizardCharacter)
		{
			this.HealthSliderPlayer1.value = this.lizardCharacter.Value.Health;
			this.ArmorSliderPlayer1.value = this.lizardCharacter.Value.Armor;
		}
        
        if (this.hedgehogCharacter)
        {
            this.HealthSliderPlayer2.value = this.hedgehogCharacter.Value.Health;
            this.ArmorSliderPlayer2.value = this.hedgehogCharacter.Value.Armor;
        }
    }
}

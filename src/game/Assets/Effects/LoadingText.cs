using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
	public Text Text;

	public void DisplayNewText(string text)
	{
		this.Text.text = text;

		var color = this.Text.color;
		color.a = 1.0f;
		this.Text.color = color;
	}

	public void HideText()
	{
		var color = this.Text.color;
		color.a = 0.0f;
		this.Text.color = color;
	}
}

using UnityEngine;
using UnityEngine.UI;

public class UnityFontBug : MonoBehaviour 
{
	private const float CHANGE_FONT_SECONDS = 3.0f;

	private string[] fontNames = null;
	private int index = 0;
	private float timer = 0.0f;
	private Text text;

	void Start()
	{
		text = GetComponent<Text>();
		if (Random.value < 0.5f)
		{
			Debug.Log("Calling GetOSInstalledFontNames immediately, which will work on iOS");
			text.color = new Color32(0, 255, 0, 255);
			CacheFontNames();
		}
		else
		{
			Debug.Log("Delaying call to GetOSInstalledFontNames, which won't work on iOS");
			text.color = new Color32(255, 0, 0, 255);
			Invoke("CacheFontNames", 5);
		}
	}

	void Update()
	{
		if (fontNames != null)
		{
			timer += Time.deltaTime;
			if (timer > CHANGE_FONT_SECONDS)
			{
				timer = 0.0f;
				if (index < fontNames.Length)
				{
					text.font = Font.CreateDynamicFontFromOSFont(fontNames[index++], text.fontSize);
					Debug.Log("text.font.fontNames[0] = " + text.font.fontNames[0]);
				}
				else
				{
					index = 0; // Covers case where fontNames is empty
				}
			}
		}
	}
	
	void CacheFontNames()
	{
		fontNames = Font.GetOSInstalledFontNames();
		Debug.Log("fontNames.Length = " + fontNames.Length);
	}
}

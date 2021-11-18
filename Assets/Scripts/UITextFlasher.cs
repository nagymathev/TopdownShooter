using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextFlasher : MonoBehaviour
{
	public Text text;
	public Color baseColour = Color.red;
	public Color flashColour = Color.white;
	public float flashTime = 0.5f;
	public float flashTimer;

	public string lastText;

	//monitors the Text component and if the text has changed, flashes it up

	void Start()
    {
		if (!text)
			text = GetComponent<Text>();

		baseColour = text.color;
		lastText = text.text;
    }

    void Update()
    {
        if (lastText != text.text)
		{
			lastText = text.text;
			flashTimer = flashTime;
		}

		if (flashTimer > 0)
		{
			flashTimer -= Time.deltaTime;
			text.color = Color.Lerp(baseColour, flashColour, flashTimer / flashTime);
		}
    }
}

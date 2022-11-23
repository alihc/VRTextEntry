using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class InputFieldFocusUtil : MonoBehaviour
{
	public InputField mainInputField;
	public GameObject keyboard;
	bool hasEnabled = false;
	void Update()
	{
		//If the input field is focused, change its color to green.
		if (mainInputField.isFocused == true && !hasEnabled)
		{
			hasEnabled = true;
			mainInputField.text = " ";
			mainInputField.GetComponent<Image>().color = Color.green;
			keyboard.SetActive(true);
		}
	}
}
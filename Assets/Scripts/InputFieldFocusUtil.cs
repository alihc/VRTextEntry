using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class InputFieldFocusUtil : MonoBehaviour
{
	public InputField mainInputField;
	public GameObject keyboard;

	void Update()
	{
		//If the input field is focused, change its color to green.
		if (mainInputField.isFocused == true)
		{
			mainInputField.text = " ";
			mainInputField.GetComponent<Image>().color = Color.green;
			keyboard.SetActive(true);
		}
	}
}
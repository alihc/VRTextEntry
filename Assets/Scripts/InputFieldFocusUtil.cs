using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class InputFieldFocusUtil : MonoBehaviour
{
	public GameObject mainInputField;
	public GameObject keyboard;

	void Update()
	{
		//If the input field is focused, change its color to green.
		if (mainInputField.GetComponent<InputField>().isFocused == true)
		{
			mainInputField.GetComponent<Image>().color = Color.green;
			keyboard.SetActive(true);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
	[Header("Welcome Screen")]
	public InputField initialsInputField;
	public InputField ageInputField;
	public Button maleButton, femaleButton;
	public GameObject maleButtonSelected, femaleButtonSelected;
	public Button vrExpYesButton, vrExpNoButton;
	public GameObject vrExpYesButtonSelected, vrExpNoButtonSelected;
	public KeyBoard keyBoardIn, keyBoardNum;
	[Header("Input Screen")]
	public Text referenceText;


	public void Start()
	{
		
		initialsInputField.onEndEdit.AddListener(delegate { OnInitialsDone(initialsInputField); });
		ageInputField.onEndEdit.AddListener(delegate { OnAgeDone(ageInputField); });
        maleButton.onClick.AddListener(delegate { OnSex(true); });
		femaleButton.onClick.AddListener(delegate { OnSex(false); });
		vrExpYesButton.onClick.AddListener(delegate { OnVrExpierience(true); });
		vrExpNoButton.onClick.AddListener(delegate { OnVrExpierience(false); });
	}

	
	void OnInitialsDone(InputField input)
	{
		string _text= input.text;
        ReferenceManager.Instance._dataManager.UserData.initials = _text;
	}

	void OnAgeDone(InputField input)
	{
		string _text = input.text;
		ReferenceManager.Instance._dataManager.UserData.age = _text;
	}

	void OnSex(bool isMale)
    {
		
		maleButtonSelected.SetActive(isMale);
		femaleButtonSelected.SetActive(!isMale);
		ReferenceManager.Instance._dataManager.UserData.sex = isMale ? "Male" : "Female";
	}

	void OnVrExpierience(bool isYes)
	{

		vrExpYesButtonSelected.SetActive(isYes);
		vrExpNoButtonSelected.SetActive(!isYes);
		ReferenceManager.Instance._dataManager.UserData.previousVRExp = isYes ? "Yes" : "No";
	}


}

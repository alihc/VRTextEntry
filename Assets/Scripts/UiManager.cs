using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
	[Header("Welcome Screen")]
	public bool isWelcomeScene;
	public InputField initialsInputField;
	public InputField ageInputField;
	public Button maleButton, femaleButton;
	public GameObject maleButtonSelected, femaleButtonSelected;
	public Button vrExpYesButton, vrExpNoButton;
	public GameObject vrExpYesButtonSelected, vrExpNoButtonSelected;
	public KeyBoard keyBoardIn, keyBoardNum;
	public Button proceeedButon;
	[Header("Trail Screen")]
	public GameObject startButton;
	public GameObject referenceTextParent;
	public Text referenceText;
	public ResultScreen resultsScreen;
	public Text debugText;


	private bool isInitialDone, isAgeDone, isSexDone, isExpDone;
	public void Start()
	{
        if (isWelcomeScene)
        {
			
			maleButton.onClick.AddListener(delegate { OnSex(true); });
			femaleButton.onClick.AddListener(delegate { OnSex(false); });
			vrExpYesButton.onClick.AddListener(delegate { OnVrExpierience(true); });
			vrExpNoButton.onClick.AddListener(delegate { OnVrExpierience(false); });
			proceeedButon.gameObject.SetActive(false);
			
			
		}
		else
		{
			ReferenceManager.Instance._uiManager = this;
			Invoke("OnResultsOk", 2f);
		}


		

       
		
	}

	
	public void OnInitialsDone(string input)
	{
		string _text= input;
        ReferenceManager.Instance._dataManager.UserData.initials = _text;
		if(string.IsNullOrEmpty(_text))
        {
			isInitialDone = false;
		}
		else
		{
			isInitialDone = true;
		}
		//isInitialDone = true;
		CheckProceesButton();
	}

	public void OnAgeDone(string input)
	{
		string _text = input;
		ReferenceManager.Instance._dataManager.UserData.age = _text;
		if (string.IsNullOrEmpty(_text))
		{
			isAgeDone = false;

        }
        else
        {
			isAgeDone = true;
        }
		//isAgeDone = true;
		CheckProceesButton();
	}

	void OnSex(bool isMale)
    {
		
		maleButtonSelected.SetActive(isMale);
		femaleButtonSelected.SetActive(!isMale);
		ReferenceManager.Instance._dataManager.UserData.sex = isMale ? "Male" : "Female";
		isSexDone = true;
		CheckProceesButton();
	}

	void OnVrExpierience(bool isYes)
	{

		vrExpYesButtonSelected.SetActive(isYes);
		vrExpNoButtonSelected.SetActive(!isYes);
		ReferenceManager.Instance._dataManager.UserData.previousVRExp = isYes ? "Yes" : "No";
		isExpDone = true;
		CheckProceesButton();
	}

	void CheckProceesButton()
    {
		if(isInitialDone && isAgeDone && isSexDone && isExpDone)
        {
			proceeedButon.gameObject.SetActive(true);
		}
        else
        {
			proceeedButon.gameObject.SetActive(false);
		}
    }

	public void OnProceedButton()
    {
		string path= ReferenceManager.Instance.fileManager.OnInfoSave();
		ReferenceManager.Instance._dataManager.path = path;
		SceneManager.LoadScene(1);

	}


	

	public void OnResultsOk()
	{
		startButton.SetActive(true);
		referenceTextParent.gameObject.SetActive(true);
		ReferenceManager.Instance._textEntryTrialManager.SelectNewPhrase();
		

	}
	public void OnPhraseDone()
	{
		referenceTextParent.gameObject.SetActive(false);



	}
	public void OnStartInput()
    {
		startButton.SetActive(false);
		ReferenceManager.Instance._textEntryTrialManager.OnStartPhraseEntry();

	}




}

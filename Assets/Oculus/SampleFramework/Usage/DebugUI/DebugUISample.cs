using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

// Show off all the Debug UI components.
public class DebugUISample : MonoBehaviour
{
    bool inMenu;
    private Text sliderText;
    public FileManager fileManager;

	void Start ()
    {
        //DebugUIBuilder.instance.AddLabel("Recent hardware advancements in Virtual Reality (VR) have fueled the demand and adoption of VR headsets. Consumer-ready headsets are currently being used for a variety of life-saving procedures as well as entertainment experiences. Text input is one of the crucial components of these experiences. The development of recent VR applications have created a demand for the quick and effective text entry systems. To this end numerous text input methods have been developed and investigated since the invention of virtual reality in order to improve the experience of typing while wearing Head-Mounted Displays (HMDs) and using handheld controllers");
        DebugUIBuilder.instance.AddButton("Save To File", LogButtonPressed);
        var sliderPrefab = DebugUIBuilder.instance.AddSlider("Slider", 1.0f, 10.0f, SliderPressed, true);
        var textElementsInSlider = sliderPrefab.GetComponentsInChildren<Text>();
        Assert.AreEqual(textElementsInSlider.Length, 2, "Slider prefab format requires 2 text components (label + value)");
        sliderText = textElementsInSlider[1];
        Assert.IsNotNull(sliderText, "No text component on slider prefab");
        sliderText.text = sliderPrefab.GetComponentInChildren<Slider>().value.ToString();
        DebugUIBuilder.instance.AddDivider();
        DebugUIBuilder.instance.AddToggle("Toggle", TogglePressed);
        DebugUIBuilder.instance.AddRadio("Radio1", "group", delegate(Toggle t) { RadioPressed("Radio1", "group", t); }) ;
        DebugUIBuilder.instance.AddRadio("Radio2", "group", delegate(Toggle t) { RadioPressed("Radio2", "group", t); }) ;
        DebugUIBuilder.instance.AddLabel("Secondary Tab", 1);
		DebugUIBuilder.instance.AddDivider(1);
        DebugUIBuilder.instance.AddRadio("Side Radio 1", "group2", delegate(Toggle t) { RadioPressed("Side Radio 1", "group2", t); }, DebugUIBuilder.DEBUG_PANE_RIGHT);
        DebugUIBuilder.instance.AddRadio("Side Radio 2", "group2", delegate(Toggle t) { RadioPressed("Side Radio 2", "group2", t); }, DebugUIBuilder.DEBUG_PANE_RIGHT);

        DebugUIBuilder.instance.Show();
        inMenu = true;
	}

    public void TogglePressed(Toggle t)
    {
        Debug.Log("Toggle pressed. Is on? "+t.isOn);
    }
    public void RadioPressed(string radioLabel, string group, Toggle t)
    {
        Debug.Log("Radio value changed: "+radioLabel+", from group "+group+". New value: "+t.isOn);
    }

    public void SliderPressed(float f)
    {
        Debug.Log("Slider: " + f);
        sliderText.text = f.ToString();
    }

    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (inMenu) DebugUIBuilder.instance.Hide();
            else DebugUIBuilder.instance.Show();
            inMenu = !inMenu;
        }
    }

    void LogButtonPressed()
    {
        
    }
}

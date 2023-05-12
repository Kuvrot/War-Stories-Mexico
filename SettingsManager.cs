using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public Toggle fullScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MediumQuality() {


        QualitySettings.SetQualityLevel(3);
    
    }

    public void HighQuality()
    {


        QualitySettings.SetQualityLevel(5);

    }

    public void res720p() {

        Screen.SetResolution(1366 , 768 , fullScreen.isOn);
    
    }

    public void FullHD()
    {

        Screen.SetResolution(1920, 1080, fullScreen.isOn);

    }
}

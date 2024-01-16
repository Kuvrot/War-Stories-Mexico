using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public int ID;

    [TextArea(10, 10)]
    public string[] details;

    public Text detail_text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (ID) {

            default: detail_text.text = details[ID];break;
        
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetBattleID(int id) {

        ID = id;
    
    }

    public void StartBattle()
    {
        PlayerPrefs.SetInt("map", ID);
        SceneManager.LoadScene("LoadingScreen");
        
    }

    public void DemoBattle () {

        StartCoroutine(timer());
        

    }

    IEnumerator timer ()
    {

        yield return new WaitForSeconds(0.2f);
        PlayerPrefs.SetInt("map", ID);
        SceneManager.LoadScene("LoadingScreen");
        Debug.Log("ID is " + ID);
        StopAllCoroutines();


    }

}

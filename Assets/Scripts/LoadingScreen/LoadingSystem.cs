using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSystem : MonoBehaviour
{
    public Slider progressBar;
    public Text progressText;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SampleScene");
       

    }
}

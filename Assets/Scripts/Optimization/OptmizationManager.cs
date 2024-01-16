using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptmizationManager : MonoBehaviour
{
    //Meshes

    [Header("Low detail meshes")]
    public Mesh Low_Top;
    public Mesh Low_Bottom;
    public Mesh Low_Hat;
    public Mesh Low_Horse;

    [Header("High detail meshes")]
    public Mesh High_Top;
    public Mesh High_Bottom;
    public Mesh High_Hat;
    public Mesh High_Horse;

    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(
          // Mathf.CeilToInt(Screen.currentResolution.width * 0.5f),
           //Mathf.CeilToInt(Screen.currentResolution.height * 0.5f),
           //true);
    }

    // Update is called once per frame
    void Update()
    {

       

    }
}

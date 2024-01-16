using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    //This script manages the scenerios that are gonna be used

    //0 = battalla nombre de las cruces
    int battleID = 0;

    public GameObject[] map;
    public GameObject[] allies;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        battleID = PlayerPrefs.GetInt("map");

        map[battleID].SetActive(true);
        allies[battleID].SetActive(true);
        enemies[battleID].SetActive(true);

    }
}

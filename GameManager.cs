using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameObject player;
    public GameObject _player;
    private CameraController cameraController;

    public GameObject curUnitSelected;

    public static GameManager gameManager;
    public GameManager gm;

    [Header("Troops")]
    public List<GameObject> allies;
    public List<GameObject> enemies;


    public GameObject shootingParticle;
    public GameObject explosionParticle;

    // Start is called before the first frame update
    void Awake()
    {
        player = Camera.main.gameObject;
        _player = player;

        gm = this;
        gameManager = gm;

       // cameraController = player.GetComponent<CameraController>();

    }

    // Update is called once per frame
    void Update()
    {

        // Victory System

        if (allies.Count <= 0 || enemies.Count <= 0) {

            SceneManager.LoadScene("MainMenu");
        
        }

       // curUnitSelected = cameraController.formationSelected;

    }


}

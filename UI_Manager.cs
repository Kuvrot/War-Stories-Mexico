using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    Language_Manager lm;
    GameManager gm;

    public Text combatMode;
    public Button distance, melee;

    public bool isMelee;
    Formation f;

    // Start is called before the first frame update
    void Start()
    {

        GameObject o = GameObject.FindGameObjectWithTag("GameManager");

        gm = o.GetComponent<GameManager>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSelection(GameObject formation)
    {

        f = formation.GetComponent<Formation>();



        if (f.ID == 0)
        {

            //f.StopCoroutine(f.Shooting());

            if (f.shootTarget != null)
            {

                if (f.Distance > f.meleeRange)
                {
                    distance.gameObject.SetActive(true);
                    melee.gameObject.SetActive(false);


                }
                else
                {

                    f.ID = 1;
                    Debug.Log("No se puede cambiar a modo distancia");


                }
            }
            else
            {
                distance.gameObject.SetActive(true);
                melee.gameObject.SetActive(false);

            }



        }
        else if (f.ID == 1)
        {

            distance.gameObject.SetActive(false);
            melee.gameObject.SetActive(true);

        }
        else if (f.ID == 3) {

            distance.gameObject.SetActive(false);
            melee.gameObject.SetActive(true);
        }

    }

    public void Melee ()
    {

        isMelee = false;

        //f.StopAllCoroutines();

        if (f.ID != 3)
        {



            if (f.Distance > f.meleeRange)
            {
                distance.gameObject.SetActive(true);
                melee.gameObject.SetActive(false);
                f.ID = 0;

            }
            else
            {

                f.ID = 1;
                f.StopCoroutine(f.Shooting());


            }

        }

    }

    public void Distance()
    {
       // Formation f = gm.curUnitSelected.GetComponent<Formation>();
        isMelee = true;

        

        if (f.ID != 3)
        {
            f.StopCoroutine(f.Melee()); 
            f.ID = 1;

            //f.StopCoroutine(f.Shooting());
            melee.gameObject.SetActive(true);
            distance.gameObject.SetActive(false);
            
        }

        
        

    }

}

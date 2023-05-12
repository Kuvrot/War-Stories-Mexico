using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour
{
    //This bool is set true when the allied unit is blocking the line of fire of the unit
    public bool crossfire;
    public Formation f;

    // Start is called before the first frame update
    void Start()
    {
        f = GetComponentInParent<Formation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (f) {

            if (!f.isAI)
            {

                if (other.tag == "Allied")
                {

                    crossfire = true;

                }

            }
            else
            {

                if (other.tag == "Enemy")
                {

                    crossfire = true;

                }

            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (f) {

            if (!f.isAI)
            {

                if (other.tag == "Allied")
                {

                    crossfire = false;

                }

            }
            else
            {

                if (other.tag == "Enemy")
                {

                    crossfire = false;

                }

            }



        }
    }


}

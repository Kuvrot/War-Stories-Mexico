using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour
{
    //This bool is set true when the allied unit is blocking the line of fire of the unit
    public bool crossfire;
    public Formation f;

    // Start is called before the first frame update
    void Awake()
    {
        f = GetComponentInParent<Formation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag1 = other.GetComponent<Collider>().tag;
        
        if (!f.isAI)
        {
            

            if (tag1 == "Allied")
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

    private void OnTriggerExit(Collider other)
    {
        string tag1 = other.tag;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SoldierController : MonoBehaviour
{

    Animator anim;
    Formation formation;
    bool shoot = false;
    public bool isDeath;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        formation = GetComponentInParent<Formation>();
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetInteger("ID", formation.ID);

       if (formation.ID == 0)
        {
            if (!formation.artillery)
            {

                switch (formation.state)
                {
                    default: anim.SetInteger("Walk", 0); break;
                    case 1: anim.SetInteger("Walk", 1); break;
                    case 2: anim.SetTrigger("Shoot"); anim.SetInteger("Walk", 0); break;
                        //case 3: anim.SetTrigger("Reloading"); shoot = false; break;

                }

            }
            else {

                if (formation.state == 2) {

                    anim.SetTrigger("Shoot");

                }
            
            
            }

        }else
        {
            switch (formation.state)
            {
                default: anim.SetInteger("Walk", 0); anim.SetBool("Melee", false); break;
                case 1: anim.SetInteger("Walk", 1); anim.SetBool("Melee", false); break;
                case 2: anim.SetBool("Melee" , true); anim.SetInteger("Walk", 0); break;
                    //case 3: anim.SetTrigger("Reloading"); shoot = false; break;

            }
        }

        //anim.SetBool("Reloading", formation.reloading);
    }

    public void Death()
    {
        anim.SetTrigger("Death");
        isDeath = true;
        //transform.position -= new Vector3(0 , 0.7f , 0);
        anim.applyRootMotion = true;
        transform.parent = null;
    }

}

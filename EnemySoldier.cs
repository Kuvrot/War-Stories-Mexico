using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : MonoBehaviour
{

    Animator anim;
    EnemyUnit enemyUnit;

    public bool isDeath;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        enemyUnit = GetComponentInParent<EnemyUnit>();
    }

    // Update is called once per frame
    void Update()
    {
       

      
        if (enemyUnit.ID == 0)
        {
                if (!enemyUnit.melee)
                {

                    anim.SetBool("Melee", true);

                    switch (enemyUnit.state)
                    {
                        default: anim.SetInteger("Walk", 0); break;
                        case 1: anim.SetInteger("Walk", 1); break;
                        case 2: anim.SetTrigger("Shooting"); break;

                    }
                }
                else
                {
                 anim.SetBool("Melee", true);
                }
        }
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

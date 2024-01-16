using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    Formation formation;
    [HideInInspector]
    public bool shoot = false;
    public bool isDeath;
    [HideInInspector]
    public bool init;
    // Start is called before the first frame update
    void Awake()
    {
       formation = GetComponentInParent<Formation>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        init = false; anim.enabled = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            if (formation.state != 0)
            {
                if (!anim.enabled)
                {
                    anim.enabled = true;
                }
            }

            init = true;

        }


        if (formation.state != 0)
        {
            shoot = true;
            anim.enabled = true;
        }
        else
        {
            shoot = false;
        }

        if (formation.ID != 2)
        {
            anim.SetInteger("ID", formation.ID);
        }else
        {
            anim.SetInteger("ID", 0);
        }

       if (formation.ID == 0 || formation.ID == 2)
        {
            switch (formation.state)
            {
                default: anim.SetInteger("Walk", 0); break;
                case 1: anim.SetInteger("Walk", 1); break;
                case 2: anim.SetTrigger("Shoot"); anim.SetInteger("Walk", 0); break;
                    //case 3: anim.SetTrigger("Reloading"); shoot = false; break;
            }

        }
        else
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
        StartCoroutine(DisableAnimator());
    }

    IEnumerator DisableAnimator()
    {
        if (!anim.enabled)
        {
            anim.enabled = true;
        }
        yield return new WaitForSeconds(3);
        anim.enabled = false;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    bool canShoot = true;

    Animator anim;
    Formation formation;

    // Start is called before the first frame update
    void Start()
    {
        formation = GetComponentInParent<Formation>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (formation.state == 2)
        {
           if (canShoot)
            {
                anim.enabled = true;
                anim.SetTrigger("Shoot");
                StartCoroutine(timer());
                canShoot = false;
            }

        }
        
    }

    IEnumerator timer()
    {

        yield return new WaitForSeconds(5);
        canShoot = true;
        StopAllCoroutines();

    }

}

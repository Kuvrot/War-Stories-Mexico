using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{


    public float reloadingTime_distance = 20f;  //Reloading time will be used as shooting rate as well
    public float shootingRange_distance = 73.152f;
    public int damage_distance = 8;

    public float reloadingTime_melee = 20f;  //Reloading time will be used as shooting rate as well
    public float shootingRange_melee = 5;
    public int damage_melee = 8;


//    public float curReloadingMelee, curReloadingDistance;


    Formation f;

    // Start is called before the first frame update
    void Start()
    {
        f = GetComponent<Formation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (f.ID == 0)
        {

            //f.reloadingTime = reloadingTime_distance;
            f.shootingRange = shootingRange_distance;
            f.damage = damage_distance;

        }
        else if (f.ID == 1) {

            //f.reloadingTime = reloadingTime_melee;
            f.shootingRange = shootingRange_melee;
            f.damage = damage_melee;

        }
    }
}

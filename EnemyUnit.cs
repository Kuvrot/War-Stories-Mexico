using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    //Identification
    [Header("Identificiation")]
    //ID 0 = normal infantry, 1 = Melee, 2 = artillery, 3 = Cavalry
    public int ID = 0, formationType = 0;

    [Header("Stats")]
    public int Health = 100;
    public float reloadingTime = 20f;  //Reloading time will be used as shooting rate as well
    public float shootingRange = 73.152f;
    public int damage = 8;

    bool canShoot = true;
    float timer;
    [HideInInspector]
    public bool reloading;


    public int state;
    public bool melee;

    public List<EnemySoldier> men;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        men.AddRange(GetComponentsInChildren<EnemySoldier>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(int amount)
    {
        Health -= amount;
        DeathSelection(amount);
    }

    public void DeathSelection(int amount)
    {
        for (int i = 0; i <= amount; i++)
        {
            int r = Random.Range(0, men.Count);

            if (!men[r].isDeath)
            {
                men[r].Death();
                men.Remove(men[r]);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour
{

    Vector3 dir;


    // Update is called once per frame
    void Update()
    {
        dir = (GameManager.player.transform.position - transform.position).normalized;
        Quaternion lr = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = lr;


    }
}

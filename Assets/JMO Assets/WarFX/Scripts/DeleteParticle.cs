using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticle : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject() {


        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    
    }
}

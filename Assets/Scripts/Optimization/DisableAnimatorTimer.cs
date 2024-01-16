using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnimatorTimer : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    private void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
        anim.enabled = true;
        StartCoroutine(DisableAnimator());

    }

    IEnumerator DisableAnimator()
    {
        

        yield return new WaitForSeconds(1);
        anim.enabled = false;
        StopAllCoroutines();
    }

}

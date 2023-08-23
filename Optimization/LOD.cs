using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOD : MonoBehaviour
{

    bool far = false;
    bool render = false;
    bool isRendering = true;
    private bool is_lod_active;
    bool temp = false;
    bool characterInit = false; //Checks if the character has been initialized

    float maxDistance = 65; //if the distance is superior something will happend
    float noRenderDistance = 100; //if the distance is superior the men will stop being rendered
    float curDistance = 0;
    Formation formation;
    GameObject shoes;
    OptmizationManager om;

    // Start is called before the first frame update
    void Start()
    {
        formation = GetComponent<Formation>();
        om = GameManager.gameManager.gameObject.GetComponent<OptmizationManager>();

    }

    // Update is called once per frame
    void Update()
    {
        curDistance = Vector3.Distance(GameManager.player.transform.position, transform.position);

        if (formation.state == 0)
        {
            if (formation.men[0].anim.enabled)
            {
                if (!temp)
                {
                    StartCoroutine(DisableAnimator());
                    temp = true;
                }
            }
        }else
        {
            temp = false;
        }

        if (curDistance > maxDistance)
        {

            far = true;

        } else
        {
            far = false;

        }

        if (curDistance > noRenderDistance)
        {

            render = false;

        } else
        {
            render = true;
        }


    }

    private void LateUpdate()
    {

     
        if (!render)
        {

             if (isRendering)
            {
                
                foreach (SoldierController n in formation.men)
                {

                    n.gameObject.SetActive(false);

                }
                
                isRendering = false;

            }

        }else
        {

            if (!isRendering)
            {
                foreach (SoldierController n in formation.men)
                {

                    n.gameObject.SetActive(true);

                }

                isRendering = false;


            }

        }

        if (far)
        {

            if (!is_lod_active)
            {

                for (int i = 0; i < formation.men.Count; i++)
                {


                    if (formation.men[i].GetComponentInChildren<Top>())
                    {

                        formation.men[i].GetComponentInChildren<Top>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.Low_Top;
                        formation.men[i].GetComponentInChildren<Bottom>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.Low_Bottom;
                        formation.men[i].GetComponentInChildren<Hat>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.Low_Hat;
                        formation.men[i].GetComponentInChildren<cross>().gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;

                    }

                    if (formation.men[i].GetComponentInChildren<Horse>())
                    {
                        formation.men[i].GetComponentInChildren<Horse>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.Low_Horse;
                    }


                }

                if (formation.deadMen.Count > 0)
                {

                    foreach (SoldierController i in formation.deadMen)
                    {
                        if (i.GetComponentInChildren<Top>())
                        {

                            i.GetComponentInChildren<Top>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.Low_Top;
                            i.GetComponentInChildren<Bottom>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.Low_Bottom;
                            i.GetComponentInChildren<Hat>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.Low_Hat;
                            i.GetComponentInChildren<cross>().gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
                            i.GetComponentInChildren<Shoes>().gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
                        }
                    }
                }

                is_lod_active = true;

            }
        }
        else
        {

            if (is_lod_active)
            {

                for (int i = 0; i < formation.men.Count; i++)
                {

                   // formation.men[i].GetComponentInChildren<Animator>().enabled = true;

                    if (formation.men[i].GetComponentInChildren<Top>())
                    {

                        formation.men[i].GetComponentInChildren<Top>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.High_Top;
                        formation.men[i].GetComponentInChildren<Bottom>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.High_Bottom;
                        formation.men[i].GetComponentInChildren<Hat>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.High_Hat;
                        formation.men[i].GetComponentInChildren<cross>().gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                        formation.men[i].GetComponentInChildren<Shoes>().gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                    }

                    if (formation.men[i].GetComponentInChildren<Horse>())
                    {
                        formation.men[i].GetComponentInChildren<Horse>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.High_Horse;
                    }
                    

                }

                if (formation.deadMen.Count > 0)
                {

                    foreach (SoldierController i in formation.deadMen)
                    {
                        if (i.GetComponentInChildren<Top>())
                        {

                            i.GetComponentInChildren<Top>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.High_Top;
                            i.GetComponentInChildren<Bottom>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.High_Bottom;
                            i.GetComponentInChildren<Hat>().gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = om.High_Hat;
                            i.GetComponentInChildren<cross>().gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                            i.GetComponentInChildren<Shoes>().gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                        }
                    }
                }



                is_lod_active = false;

            }

        }

    }

    IEnumerator DisableAnimator()
    {
       
        yield return new WaitForSeconds(1.5f);
        foreach (SoldierController i in formation.men)
        {

            if (i.anim)
            {
                i.anim.enabled = false;
            }

            temp = false;

        }
        StopCoroutine(DisableAnimator());
    }
}

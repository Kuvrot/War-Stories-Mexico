using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierUniformManager : MonoBehaviour
{

    public int uniformType; //0 = mexican in 1810, 1 = royalist 1810, 2 = mexican mid XIX century , 3 = Saint Patrick, 4  = USA ,  5 = French;

    //public string skinColor = "body", top = "top", bottom = "bottom" , cross = "Circle.001";

    //skin 0 = brown, skin 1 = white, 
    //top 0 = light blue, 1 = dark blue, 2 = white, 3 = red;
    //bottom 0 = light blue, 1 = dark blue, 2 = white, 3 = red;
    //cross 0 = white, 1 = red
    public Material[] skin, top_color, bottom_color , cross_color;

    Formation formation;

    public Image currentFlag;

    public Sprite[] flags; // 0 = mexico, 1 = spain

    // Start is called before the first frame update
    void Start()
    {

        formation = GetComponent<Formation>();

        bottom_color = top_color;

        if (uniformType == 0) {

            formation.flag.sprite = flags[0];

            for (int i = 0; i < formation.men.Count; i++)
            {

                if (formation.men[i].GetComponentInChildren<Body>()) {

                    formation.men[i].GetComponentInChildren<Body>().gameObject.GetComponent<Renderer>().material = skin[0];
                    formation.men[i].GetComponentInChildren<Top>().gameObject.GetComponent<Renderer>().material = top_color[0];
                    formation.men[i].GetComponentInChildren<Bottom>().gameObject.GetComponent<Renderer>().material = bottom_color[2];
                    formation.men[i].GetComponentInChildren<cross>().gameObject.GetComponent<Renderer>().material = cross_color[0];
                }

            }

        }
        else if (uniformType == 1)
        {

            formation.flag.sprite = flags[1];

            for (int i = 0; i < formation.men.Count; i++)
            {
                if (formation.men[i].GetComponentInChildren<Body>())
                {
                    formation.men[i].GetComponentInChildren<Body>().gameObject.GetComponent<Renderer>().material = skin[1];
                    formation.men[i].GetComponentInChildren<Top>().gameObject.GetComponent<Renderer>().material = top_color[1];
                    formation.men[i].GetComponentInChildren<Bottom>().gameObject.GetComponent<Renderer>().material = bottom_color[1];
                    formation.men[i].GetComponentInChildren<cross>().gameObject.GetComponent<Renderer>().material = cross_color[1];
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

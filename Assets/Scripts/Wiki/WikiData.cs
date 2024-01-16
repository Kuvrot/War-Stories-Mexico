using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WikiData : MonoBehaviour
{

    [TextArea(10, 10)]
    public string data;
    public Sprite image;
    WikiManager wm;
    Button btn;
    public bool selected;


    // Start is called before the first frame update
    void Start()
    {
        wm = GetComponentInParent<WikiManager>();
        btn = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PresentData()
    {
        wm.image.sprite = image;
        wm.data.text = data;
    }

    
}

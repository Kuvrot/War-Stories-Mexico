using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    public float speed = 20;
    public float mouseSensitivity = 50;
    CharacterController characterCon;

    public LayerMask UI_layerMask = ~(1 << 5);

    public List<GameObject> formations;
    public GameObject formationSelected;
    public Transform target;
    UI_Manager ui_manager;


    // Start is called before the first frame update
    void Start()
    {
        characterCon = GetComponent <CharacterController>();
        ui_manager = GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {

        //Using old unity input system

        float h = Input.GetAxis("Horizontal") * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * Time.deltaTime;

        float m_x = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float m_y = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;


        transform.position += Quaternion.Euler (transform.eulerAngles.x , transform.eulerAngles.y , 0) * new Vector3(h, 0f, v) * speed;

        if (Input.GetButton("Fire3"))
        {
            transform.localEulerAngles += new Vector3(-m_y, m_x, 0);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetButtonDown("Fire1"))
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Si el raycast no golpeó un objeto 3D pero el puntero está sobre un objeto de la UI, haz algo
                Debug.Log("Objeto de la UI detectado");
            }else
            { 
                RaycastHit hit;
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.GetComponent<Formation>() )
                    {


                        if (!hit.transform.GetComponent<Formation>().isAI)
                        {
                            if (formationSelected != null)
                            {

                                formationSelected.GetComponent<Formation>().selected = false;

                            }

                            hit.transform.GetComponent<Formation>().selected = true;
                            hit.transform.GetComponent<Formation>().shootTarget = null;
                            hit.transform.GetComponent<Formation>().moveTo = null;
                            formationSelected = hit.transform.gameObject;

                            ui_manager.ChangeSelection(formationSelected);

                        }
                        else {

                            if (formationSelected != null)
                            {
                                formationSelected.GetComponent<Formation>().shootTarget = hit.transform;
                                formationSelected.GetComponent<Formation>().selected = false;
                                //formationSelected.GetComponent<AudioSource>().PlayOneShot(formationSelected.GetComponent<FormationSounds>().TrumpetMarch);
                                formationSelected = null;
                            }


                        }
                    }
                    else
                    {

                        if (formationSelected != null)
                        {
                            Transform move = target;
                            target.position = hit.point;
                            formationSelected.GetComponent<Formation>().moveTo = move;
                            formationSelected.GetComponent<Formation>().moveToCoords = move.position;
                            formationSelected.GetComponent<Formation>().selected = false;
                            formationSelected = null;
                        }
                    }

                    //Debug.Log(hit.transform.name);

                }

            }

            
        }

        if (Input.GetButton("Jump"))
        {
            Time.timeScale = 2.5f;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//The levels of the infantry formation are
//Reloading time 90 sec , 60 sec and 20 sec
//Also the shooting distance will change depending of the level of the unit

public class Formation : MonoBehaviour
{
    [Header("AI settings")]
    public bool isAI = false;
    public bool Defensor; // if defensor is true, the AI will hold positions;

    public bool selected;

    public UnitVision unitVision;

    public bool gettingAttack = false;

    //Identification
    [Header("Identificiation")]
    //ID 0 = normal infantry, 1 = Melee, 2 = artillery, 3 = Cavalry
    public int ID = 0, formationType = 0;

    [Header("Stats")]
    public int Health = 100;
    public float reloadingTime = 20f , attackRate = 10;  //Reloading time will be used as shooting rate as well. AttackTime is for melee attackRate
    public float shootingRange = 73.152f, meleeRange = 5;
    public int damage = 8;
    public bool artillery = false;

   // [HideInInspector]
    public bool canShoot = true , shooting = false , canMelee = true;
    float timer;
    [HideInInspector]
    public bool reloading;

    [Header("States")]
    //state 0 = idle  , state 1 = moving , state 2 = shooting, state 3 = reloading
    public int state;

    [Header("Targets")]
    public Transform shootTarget , moveTo;
    public Vector3 moveToCoords;


    [Header("UI components")]
    public Image canShootImage;
    public Image flag;
    public Slider healthBar;

    public List<SoldierController> men;

    public List<SoldierController> deadMen;

    //components
    NavMeshAgent _nva;
    AudioSource audioSource;
    FormationSounds formationSounds;
    Formation targetFormation;
    GameManager gm;

    [HideInInspector]
    public float Distance , waypointDistance;


    float distanceCounter, meleeCounter;


    // Start is called before the first frame update
    void Awake()
    {
        _nva = GetComponent<NavMeshAgent>();
        _nva.stoppingDistance = shootingRange;
        audioSource = GetComponent<AudioSource>();
        formationSounds = GetComponent<FormationSounds>();
        canShootImage.fillAmount = 1;
       


        men.AddRange(GetComponentsInChildren<SoldierController>());
        healthBar.maxValue = Health;
        healthBar.value = Health;

        if (ID == 0)
        {
            unitVision = GetComponentInChildren<UnitVision>();
           
            timer = reloadingTime;
        }
        else {

            timer = attackRate;

        }

        gm = GameManager.gameManager;

    }


    private void OnEnable()
    {
        if (!isAI) {

            gm.allies.Add(this.gameObject);
        }
        else 
        { 
            gm.enemies.Add(gameObject); 
        }

        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = Health;

        if (!isAI) { 
            
            playerController();

            if (Health <= 0)
            {
                gm.allies.Remove(gameObject);
                Destroy(gameObject);
            }

        } else { 

            AI_Controller();

            if (Health <= 0)
            {
                gm.enemies.Remove(gameObject);
                Destroy(gameObject);
            }
        } 
    }

    void AI_Controller() {

        if (shootTarget == null) { SelectNewTarget(); }

        Distance = Vector3.Distance(shootTarget.position, transform.position);
        _nva.stoppingDistance = shootingRange;

        if (!Defensor) {

            if (shootTarget == null)
            {
                int ran = Random.Range(0, gm.allies.Count);
                shootTarget = gm.allies[ran].transform;
            }
            else {

                if (ID != 3)
                {
                    if (faceTarget(shootTarget.position))
                    {
                        if (Distance > shootingRange)
                        {
                            _nva.SetDestination(shootTarget.position);
                            _nva.isStopped = false;
                            state = 1;
                        }
                        else
                        {
                            if (Distance > meleeRange)
                            {
                                ID = 0;

                                if (canShoot)
                                {
                                    Shoot(Distance);
                                }

                            }
                            else
                            {
                                ID = 1;
                                state = 2;

                                if (canMelee)
                                {
                                    Shoot(Distance);
                                }

                            }

                        }

                    }else
                    {
                        state = 1;
                    }
                }
                else {

                    if (Distance <= meleeRange + 2) {

                        if (canMelee) { Shoot(Distance); }
                        state = 2;

                    }
                    else
                    {
                        _nva.SetDestination(shootTarget.position);
                        state = 1;
                        _nva.isStopped = false;

                    }

                 }
            }
        }
        else
        {

            float allTargets_distance;
            for (int i = 0; i < gm.allies.Count; i++)
            {

                allTargets_distance = Vector3.Distance(transform.position, gm.allies[i].transform.position);

                if (allTargets_distance <= shootingRange)
                {

                    Defensor = false;

                }
            }
        }

        canShootImage.fillAmount = timer / (reloadingTime + 2.5f);
        timer += 1 * Time.deltaTime;


        if (ID == 0)
        {
            if (timer >= reloadingTime)
            {

                timer = reloadingTime + 2.5f;

            }
        }
        /* else if (ID == 1)
         {

             if (timer <= 10)
             {

                 timer = 10 + 2.5f;

             }
         }
         else if (ID == 3) {

            if (timer <= 10)
             {

                 timer = 10 + 2.5f;

             }
         }*/

        if (targetFormation.Health <= 0) {

            shootTarget = null;
        
        }
    }

    void playerController()
    {

        
        if (shootTarget != null)
        {
            Distance = Vector3.Distance(shootTarget.position, transform.position);
            _nva.stoppingDistance = shootingRange;

            if (ID != 3)
            {

                if (faceTarget(shootTarget.position))
                {

                    _nva.SetDestination(shootTarget.position);
                    _nva.isStopped = false;

                }
            
            }
            else {

                _nva.SetDestination(shootTarget.position);
                _nva.isStopped = true;

            }

            if (moveTo != null)
            {
                shootTarget = null;
                _nva.isStopped = false;
            }

            moveTo = null;
            moveToCoords = Vector3.zero;

            if (Distance <= shootingRange)
            {
                if (ID == 0)
                {
                    if (canShoot)
                    {
                        state = 2;
                    }
                }
                else {

                    if (ID == 1 || ID == 3)
                    {

                        if (Distance <= meleeRange)
                        {

                            state = 2;

                            if (canMelee)
                            {

                                Shoot(Distance);
                                timer = 0;

                            }
                        }
                    }
                }

                _nva.isStopped = true;
            }
            else if (Distance >= shootingRange)
            {
                state = 1;
                _nva.isStopped = false;
            }

        }
        else if (moveTo != null)
        {

            waypointDistance = Vector3.Distance(moveToCoords, transform.position);

            if (ID != 3) {

                if (faceTarget(moveToCoords))
                {

                    _nva.SetDestination(moveToCoords);
                    _nva.isStopped = false;

                }
                else
                {
                    _nva.isStopped = true;
                }
            }
            else
            {

                _nva.SetDestination(moveToCoords);

            }

            _nva.stoppingDistance = 0;

            if (waypointDistance <= 2)
            {
                moveTo = null;

            }
            else
            {
                state = 1;
                _nva.isStopped = false;
            }

            if (shootTarget != null)
            {
                moveTo = null;
            }

            shootTarget = null;

           // if (!audioSource.isPlaying)
            //{
            //    audioSource.PlayOneShot(formationSounds.DrumMarch);
            //}

        }
        else if (moveTo == null && shootTarget == null)
        {
            state = 0;
            _nva.isStopped = true;
            _nva.SetDestination(transform.position);
            selected = false;
        }
         
            if (state == 3)
            {
                state = 4;
            }

            timer += 1 * Time.deltaTime;


        if (ID == 0)
        {

            if (canShoot)
            {
                if (shootTarget != null) {

                    if (faceTarget(shootTarget.position)) {

                        if (state == 2)
                        {
                            Shoot(Distance);
                        }

                    }else
                    {
                        state = 1;
                    }
                }        
            }

            canShootImage.fillAmount = timer / (reloadingTime + 2.5f);

            if (timer >= reloadingTime)
            {

                timer = reloadingTime + 2.5f;

            }

        }
       /* else
        {

            canShootImage.fillAmount = timer / (attackRate + 2.5f);

            if (timer >= attackRate)
            {

                timer = attackRate + 2.5f;

            }

        }*/
    }

    //if is facing the target then will return true
    bool faceTarget (Vector3 target) {

        bool isFacingTarget;

        Vector3 direccionObjetivo = target - transform.position;
        direccionObjetivo.y = 0f; // aseguramos que el agente no rote en la dirección de la altura del objetivo
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionObjetivo);
        

        if (Quaternion.Angle(transform.rotation, rotacionObjetivo) < 5f)
        { // si la rotación es suficiente, avanzamos hacia el objetivo
            isFacingTarget = true;
        }
        else {

            isFacingTarget = false;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, _nva.angularSpeed * Time.deltaTime);
        }
        

        return isFacingTarget;

    }
    private void OnMouseOver()
    {
        if (!Input.GetButton("Fire3")) {

            GetComponent<MeshRenderer>().enabled = true;

        }
    }

    private void OnMouseExit()
    {
        if (!Input.GetButton("Fire3"))
        {

            GetComponent<MeshRenderer>().enabled = false;

        }
    }

    void Shoot(float distance)
    {
        
       
        //Debug.Log("shoot");

        if (ID == 0)
        {
            if (canShoot)
            {
                state = 2;

                if (unitVision != null)
                {
                    if (!unitVision.crossfire)
                    {
                        audioSource.PlayOneShot(formationSounds.Shooting);
                        Instantiate(gm.shootingParticle, transform.position, transform.rotation);

                        canShoot = false;
                        StartCoroutine(Shooting());
                        distanceCounter = 0;

                        if (!artillery)
                        {


                            if (shootTarget.GetComponent<Formation>().ID == 0)
                            {
                                if (distance <= 25)
                                {
                                    shootTarget.GetComponent<Formation>().GetDamage(((Health * damage) / men.Count) * 4);
                                }
                                else
                                {
                                    shootTarget.GetComponent<Formation>().GetDamage((Health * damage) / men.Count);
                                }
                            }
                            else if (shootTarget.GetComponent<Formation>().ID == 3)
                            {
                                if (distance >= 25)
                                {
                                    shootTarget.GetComponent<Formation>().GetDamage(Random.Range(0,3));
                                }
                                else
                                {
                                    shootTarget.GetComponent<Formation>().GetDamage(((Health * damage) / men.Count) * 4);
                                }
                            }

                            
                        }
                        else {

                            if (Random.Range(0, 100) < 33)
                            {

                                shootTarget.GetComponent<Formation>().GetDamage(damage * 3);
                                Instantiate(gm.explosionParticle, shootTarget.position, shootTarget.rotation);

                            }
                            else {
                                int ran = Random.Range(0 , 20);
                                Instantiate(gm.explosionParticle, new Vector3 (shootTarget.position.x + ran, shootTarget.position.y , shootTarget.position.z + ran), shootTarget.rotation);

                            }

                        }

                        timer = 0;
                    }
                   // //Debug.Log("Shooting");
                }
                else
                {

                    state = 0;

                }
            }  
            
        }
        else if (ID == 1)
        {
            if (canMelee) {

                Formation target_formation = shootTarget.GetComponent<Formation>();

                if (men.Count != 0)
                {

                    target_formation.GetDamage((Health * damage) / men.Count);

                }

                StartCoroutine(Melee());
                if (!shootTarget.GetComponent<Formation>().gettingAttack && shootTarget.GetComponent<Formation>().isAI)
                {
                    target_formation.gettingAttack = true;
                    target_formation.shootTarget = transform;
                    target_formation._nva.SetDestination(transform.position);
                    target_formation._nva.isStopped = true;
                    target_formation.ID = 1;
                }
                meleeCounter = 0;
                canMelee = false;
            }

            timer = 0;

        }
         else if (ID == 3)
        {
            if (canMelee)
            {

                state = 2;

                if (shootTarget.GetComponent<Formation>().ID != 3)
                {
                    shootTarget.GetComponent<Formation>().GetDamage(Random.Range(damage / 2 , damage + 1) / men.Count);
                    //Debug.Log("Cavalry_Attack");
                    //canMelee = false;
                    if (!shootTarget.GetComponent<Formation>().gettingAttack && shootTarget.GetComponent<Formation>().isAI)
                    {
                        shootTarget.GetComponent<Formation>().gettingAttack = true;
                        shootTarget.GetComponent<Formation>().shootTarget = transform;
                        shootTarget.GetComponent<Formation>().ID = 1;
                    }
                    StartCoroutine(Melee());


                }
                else
                {
                    shootTarget.GetComponent<Formation>().GetDamage(damage / 4);

                    //Debug.Log("Cavalry_Attack");
                    //canMelee = false;
                    if (!shootTarget.GetComponent<Formation>().gettingAttack && shootTarget.GetComponent<Formation>().isAI)
                    {
                        shootTarget.GetComponent<Formation>().gettingAttack = true;
                        shootTarget.GetComponent<Formation>().shootTarget = transform;
                        shootTarget.GetComponent<NavMeshAgent>().SetDestination(transform.position);
                    }
                    StartCoroutine(Melee());

                }

                meleeCounter = 0;
                timer = 0;
                canMelee = false;
            }
        }
    }

    public IEnumerator Shooting()
    {

        int curID = ID;

        reloading = true;
        //Debug.Log("Reloading");
        yield return new WaitForSeconds(2.5f);
        state = 3;

        yield return new WaitForSeconds(reloadingTime);

        canShoot = true;
        reloading = false;
        state = 0;


        StopCoroutine(Shooting());
    }

    public IEnumerator Melee()
    {

        int curID = ID;

        reloading = true;
        //Debug.Log("Reloading");
        yield return new WaitForSeconds(2.5f);
        state = 3;

        yield return new WaitForSeconds(attackRate);

        canMelee = true;
        reloading = false;
        state = 0;


        StopCoroutine(Melee());
    }

    //This function is called when a formation needs to get damage
    public void GetDamage(int amount)
    {
        Health -= amount;
        DeathSelection(amount);
    }
    
    //This method is only called by the AI when there is not current target to attack
    void SelectNewTarget () {

        int ran = Random.Range(0 , gm.allies.Count);

        shootTarget = gm.allies[ran].transform;

        targetFormation = shootTarget.GetComponent<Formation>();
    }

    //This function selects who dies in the formation
    public void DeathSelection (int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int r = Random.Range(0 , men.Count);

            if (men.Count > 0) {

                if (!men[r].isDeath)
                {
                    men[r].Death();

                    deadMen.Add(men[r]);
                    men.Remove(men[r]);
                    
                }

            }
        } 
    }

}

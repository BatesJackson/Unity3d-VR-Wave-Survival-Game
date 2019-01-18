using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject powerCell;
    public GameObject target;
    public GameObject laserSpawn;
    public GameObject bulletList;

    //Enemy bullet pool
   // public List<GameObject> enemyBulletPool;
    public int bulletAmount;
    public SpawnManager sm;
    public GameObject spawnManager;

    public float moveSpeed;
    public bool inRange;
    public PowerCell pc;

    //Shooting Mechanics
    public float rifleFireRate = 1.0f;
    public float pistolFireRate = 1.3f;
    public float canFire = 0.0f;
    public float pistolCanFire = 0.5f;


    public int health;
	// Use this for initialization
	void Start () {
        moveSpeed = 3f;
        health = 20;
        spawnManager = GameObject.FindGameObjectWithTag("Spawn_Manager");
        sm = spawnManager.GetComponent<SpawnManager>();
        inRange = false;
        pistolFireRate = 1.405f;
        sm.bulletAmount = 200;
    }
	
	// Update is called once per frame
	void Update () {
        
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        laserSpawn.transform.LookAt(powerCell.transform);
        if(health <= 0)
        {
            gameObject.SetActive(false);
            health = 20;
            moveSpeed = 3.0f;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player_Laser"))
        {
            health -= 20;
            col.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (gameObject.name.Contains("Rifle_Enemy"))
        {
            if (col.CompareTag("Rifle_Shoot_Range"))
            {
                //sm.enemyBulletPool[].SetActive(true);
                if (Time.time > canFire)
                {
                    transform.LookAt(target.transform.position);
                    laserSpawn.transform.LookAt(powerCell.transform);
                    moveSpeed = 0.0f;

                    gameObject.GetComponentInChildren<Animation>().Play("Fire_Rifle_Single");
                    for (int i = 0; i < sm.bulletAmount; i++)
                    {
                        if (sm.enemyBulletPool[i].activeSelf == false)
                        {
                            
                            sm.enemyBulletPool[i].transform.position = laserSpawn.transform.position;
                            sm.enemyBulletPool[i].transform.LookAt(powerCell.transform);
                            sm.enemyBulletPool[i].SetActive(true);

                            break;
                        }
                    }
                    canFire = Time.time + rifleFireRate;
                }
            }
        }
        else if(gameObject.name.Contains("Pistol_Enemy"))
        {
            if (col.CompareTag("Pistol_Shoot_Range"))
            {
                transform.LookAt(target.transform.position);
                laserSpawn.transform.LookAt(powerCell.transform);
                moveSpeed = 0.0f;
                StartCoroutine(PistolWait());               
                gameObject.GetComponentInChildren<Animation>().Play("Fire_Pistol_Single");
                if(inRange)
                {
                    
                    if (Time.time > canFire)
                    {
                        
                        for (int i = 0; i < sm.bulletAmount; i++)
                        {
                            if (sm.enemyBulletPool[i].activeSelf == false)
                            {
                                
                                sm.enemyBulletPool[i].SetActive(true);
                                sm.enemyBulletPool[i].transform.position = laserSpawn.transform.position;
                                sm.enemyBulletPool[i].transform.LookAt(powerCell.transform);
                               
                                break;
                            }
                        }

                        canFire = Time.time + pistolFireRate;
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Rifle_Shoot_Range"))
        {
            if (gameObject.name.Contains("Rifle_Enemy"))
            {
                gameObject.GetComponentInChildren<Animation>().Play("Run_Rifle_Forward");
            }
            moveSpeed = 1.0f;
        }
        else if (col.CompareTag("Pistol_Shoot_Range"))
        {
            if (gameObject.name.Contains("Pistol_Enemy"))
            {
                gameObject.GetComponentInChildren<Animation>().Play("Run_Pistol_Forward");
            }
            moveSpeed = 1.0f;
        }
        /*private IEnumerator Shoot()
        {
            while (true)
            {

                yield return new WaitForSeconds(1.0f);
            }
        }*/
    }
    private IEnumerator PistolWait()
    {             
        yield return new WaitForSeconds(0.67f);
        inRange = true;         
    }
}

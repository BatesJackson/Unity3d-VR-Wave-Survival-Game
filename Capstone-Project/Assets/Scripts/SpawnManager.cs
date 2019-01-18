using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //play bool determines state of the game
    public bool play;
    //Pistol Enemy
    public List<GameObject> pistolEnemyList;
    public float pistolSpawnRate;
    public float pistolSpawnUpper;
    public float pistolSpawnLower;
    [SerializeField]
    private GameObject _pistolEnemyPrefab;

    //Rifle Enemy
    public List<GameObject> rifleEnemyList;
    public float rifleSpawnRate;
    public float rifleSpawnUpper;
    public float rifleSpawnLower;
    [SerializeField]
    private GameObject _rifleEnemyPrefab;

    //Heart
    public float heartSpawnRate;
    public float heartSpawnUpper;
    public float heartSpawnLower;
    [SerializeField]
    private GameObject _heartPrefab;

    //bullet pool
    //Enemy bullet pool
    public List<GameObject> enemyBulletPool;
    public int bulletAmount;
    public GameObject _laserPrefab;

    //player bullet pool
    public List<GameObject> playerBulletPool;
    public int playerLaserAmount;
    public GameObject _playerLaserPrefab;

    private int pistolEnemyAmount;
    private int rifleEnemyAmount;

    Quaternion startRotation;


    // Use this for initialization
    void Start()
    {
        startRotation = _pistolEnemyPrefab.transform.rotation;
        play = true;
        pistolSpawnLower = 5.0f;
        pistolSpawnUpper = 8.0f;
        rifleSpawnLower = 7.0f;
        rifleSpawnUpper = 10.0f;
        heartSpawnLower = 10.0f;
        heartSpawnUpper = 20.0f;
        pistolSpawnRate = Random.Range(pistolSpawnLower, pistolSpawnUpper);
        rifleSpawnRate = Random.Range(rifleSpawnLower, rifleSpawnUpper);
        heartSpawnRate = Random.Range(heartSpawnLower, heartSpawnUpper);
        bulletAmount = 100;
        playerLaserAmount = 200;
        pistolEnemyAmount = 20;
        rifleEnemyAmount = 20;
        for (int i = 0; i < pistolEnemyAmount; i++)
        {
            GameObject enemy = Instantiate(_pistolEnemyPrefab);
            enemy.SetActive(false);
            pistolEnemyList.Add(enemy);
        }
        for (int i = 0; i < rifleEnemyAmount; i++)
        {
            GameObject enemy = Instantiate(_rifleEnemyPrefab);
            enemy.SetActive(false);
            rifleEnemyList.Add(enemy);
        }
        //bullet pool
        bulletAmount = 100;
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject laser = Instantiate(_laserPrefab);
            laser.SetActive(false);
            enemyBulletPool.Add(laser);
        }
        //player bullet pool
        for (int i = 0; i < playerLaserAmount; i++)
        {
            GameObject laser = Instantiate(_playerLaserPrefab);
            laser.gameObject.SetActive(false);
            playerBulletPool.Add(laser);
        }
        
    }
    //y pos always 42
    //x pos between -10 and 10
    public IEnumerator SpawnPistolEnemy()
    {
        while (play)
        {

            for (int i = 0; i < pistolEnemyAmount; i++)
            {
                if (!play)
                {
                    break;
                }
                if (pistolEnemyList[i].activeSelf == false)
                {
                    yield return new WaitForSeconds(pistolSpawnRate);
                    pistolEnemyList[i].transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 1.41f, 38);
                    pistolEnemyList[i].transform.rotation = _pistolEnemyPrefab.transform.rotation;
                    pistolEnemyList[i].SetActive(true);
                    pistolSpawnRate = Random.Range(pistolSpawnLower, pistolSpawnUpper);
                }
               // break;
            }

            
        }
    }
    public IEnumerator SpawnRifleEnemy()
    {
        while (play)
        {


            for (int i = 0; i < rifleEnemyAmount; i++)
            {
                if (!play)
                {
                    break;
                }
                if (pistolEnemyList[i].activeSelf == false)
                {
                    yield return new WaitForSeconds(rifleSpawnRate);
                    rifleEnemyList[i].transform.rotation = _rifleEnemyPrefab.transform.rotation;
                    rifleEnemyList[i].transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 1.41f, 38);
                    rifleEnemyList[i].SetActive(true);
                    rifleSpawnRate = Random.Range(rifleSpawnLower, rifleSpawnUpper);
                }
               // break;
            }

            
        }
    }
    public IEnumerator SpawnRateUpdate()
    {
        while (pistolSpawnLower > 1.3f)
        {

            yield return new WaitForSeconds(10.0f);

            pistolSpawnLower *= 0.80f;
            pistolSpawnUpper *= 0.80f;
            rifleSpawnLower *= 0.80f;
            rifleSpawnUpper *= 0.80f;

        }
    }

    public IEnumerator SpawnHeart()
    {
        while (play)
        {

            if (!play)
            {
                break;
            }
            yield return new WaitForSeconds(heartSpawnRate);
            if (_heartPrefab.activeSelf == false)
            {
                _heartPrefab.transform.position = new Vector3(Random.Range(-5.4f, 5.4f), 3.2f, Random.Range(24.0f, 37.0f));
                _heartPrefab.SetActive(true);
            }

        }
    }
    public void StopCoroutines()
    {
        StopAllCoroutines();
    }

}

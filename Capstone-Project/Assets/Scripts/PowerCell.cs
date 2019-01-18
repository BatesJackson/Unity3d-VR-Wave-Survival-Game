using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCell : MonoBehaviour {
    public float health;
    private float _healthMultiplier;

    public GameObject cameraUICanvas;
    public GameObject player;
    private Vector3 playerStart;
    SpawnManager sm;
    GameObject spawnManager;
    // Use this for initialization
    void Start () {
        health = 100;
        playerStart = player.transform.position;
        spawnManager = GameObject.FindGameObjectWithTag("Spawn_Manager");
        sm = spawnManager.GetComponent<SpawnManager>();
    }
    private void Update()
    {
        if(health == 0)
        {
            health = -1;
            EndGame(); 
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Laser"))
        {
            if (health >= 0)
            {
                health -= 1;
            }
            //Destroy(col.gameObject);
            col.gameObject.SetActive(false);
        }
    }
    private void EndGame()
    {
        sm.play = false;
        player.transform.position = playerStart;
        //health = 100;
        cameraUICanvas.SetActive(true);
    }
}

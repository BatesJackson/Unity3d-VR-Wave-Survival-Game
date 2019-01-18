using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    public Text timeText;
    public Text healthText;
    public Text endTime;

    private float startTime;
    private float time;

    PowerCell pc;
    GameObject powerCell;

    SpawnManager sm;
    GameObject spawnManager;


    public GameObject cameraStartUICanvas;
    public GameObject cameraEndUICanvas;

    bool started;

    string min;
    string seconds;
	// Use this for initialization
	void Start () {
        started = false;
        powerCell = GameObject.FindGameObjectWithTag("Power_Cell");
        pc = powerCell.GetComponent<PowerCell>();

        spawnManager = GameObject.FindGameObjectWithTag("Spawn_Manager");
        sm = spawnManager.GetComponent<SpawnManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //Time text
        if (sm.play)
        {
            if (!started)
            {
                timeText.text = "00:00";
            }
            else
            {
                time = Time.time - startTime;
                min = ((int)time / 60).ToString("00");
                seconds = (time % 60).ToString("00");
                timeText.text = string.Format("{0:00}:{1:00}", min, seconds);
                endTime.text = "Survival Time: " + min + ":" + seconds;
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.One) && cameraStartUICanvas.activeSelf == true)
        {
            startGame();
        } else if (OVRInput.GetDown(OVRInput.Button.One) && cameraEndUICanvas.activeSelf == true)
        {
            RestartGame();
        }
        //Health Text
        string health = pc.health.ToString();
        healthText.text = "Health: " + health;
	}

    private void startGame()
    {
        startTime = Time.time;
        started = true;
        sm.play = true;
        StartCoroutine(sm.SpawnPistolEnemy());
        StartCoroutine(sm.SpawnRifleEnemy());
        StartCoroutine(sm.SpawnRateUpdate());
        StartCoroutine(sm.SpawnHeart());
        cameraStartUICanvas.SetActive(false);
    }
    private void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    PowerCell pc;
    GameObject powerCell;

    public AudioClip heartAudioClip;
    //AudioSource audioSource;

    // Use this for initialization
    void Start () {
        powerCell = GameObject.FindGameObjectWithTag("Power_Cell");
        pc = powerCell.GetComponent<PowerCell>();

        //audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * 0.75f * Time.deltaTime);
        if(gameObject.transform.position.y < -1.68)
        {
            gameObject.SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player_Laser"))
        {
            Vector3 pos = gameObject.transform.position;
            pc.health += 25;
            AudioSource.PlayClipAtPoint(heartAudioClip, pos);
            if (pc.health > 100)
            {
                pc.health = 100;
            }
            col.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

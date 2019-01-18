using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool leftHand;

    public GameObject rightGun;
    private float rightCanFire;
    private float leftCanFire;
    private float fireRate;
    public GameObject leftGun;
    public Rigidbody laser;

    public SpawnManager sm;
    public GameObject spawnManager;

    public Vector3 leftGunPos;
    public Vector3 rightGunPos;


    public AudioClip laserAudioClip;
    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        fireRate = 0.3f;
        rightCanFire = 0.0f;
        leftCanFire = 0.0f;
        spawnManager = GameObject.FindGameObjectWithTag("Spawn_Manager");
        sm = spawnManager.GetComponent<SpawnManager>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        if (sm.play)
        {
            if (!leftHand)
            {

                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    if (Time.time > rightCanFire)
                    {

                        rightGunPos = new Vector3(rightGun.transform.position.x, rightGun.transform.position.y, rightGun.transform.position.z);
                        for (int i = 0; i < sm.playerLaserAmount; i++)
                        {
                            if (sm.playerBulletPool[i].gameObject.activeSelf == false)
                            {
                                sm.playerBulletPool[i].transform.position = rightGunPos;
                                sm.playerBulletPool[i].gameObject.SetActive(true);
                                sm.playerBulletPool[i].GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 40);
                                audioSource.PlayOneShot(laserAudioClip);
                                break;
                            }

                        }
                        rightCanFire = Time.time + fireRate;
                    }
                }
            }
            else
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    if (Time.time > leftCanFire)
                    {
                        leftGunPos = new Vector3(leftGun.transform.position.x, leftGun.transform.position.y, leftGun.transform.position.z);
                        for (int i = 0; i < sm.playerLaserAmount; i++)
                        {
                            if (sm.playerBulletPool[i].gameObject.activeSelf == false)
                            {
                                sm.playerBulletPool[i].transform.position = leftGunPos;
                                sm.playerBulletPool[i].gameObject.SetActive(true);
                                sm.playerBulletPool[i].GetComponent<Rigidbody>().velocity = leftGun.transform.TransformDirection(Vector3.forward * 40);
                                audioSource.PlayOneShot(laserAudioClip);
                                break;

                            }
                        }
                        leftCanFire = Time.time + fireRate;
                    }
                }
            }
        }
    }

}




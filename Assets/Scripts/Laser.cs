using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public  GameObject powerCell;
    public float laserSpeed;

    // Use this for initialization
    void Start () {
        transform.LookAt(powerCell.transform);
        laserSpeed = 15.0f;

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * laserSpeed * Time.deltaTime);
	}
}

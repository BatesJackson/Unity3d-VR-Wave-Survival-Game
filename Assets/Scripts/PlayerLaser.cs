using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(LaserDeactivate());
	}

    public IEnumerator LaserDeactivate()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
    }
}

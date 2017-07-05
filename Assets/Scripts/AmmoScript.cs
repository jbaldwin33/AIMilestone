using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour {

    public int ammo;
	// Use this for initialization
	void Start () {
        ammo = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump"))
        {
            ammo++;
        }
        if (Input.GetKeyDown("m"))
        {
            ammo--;
        }
	}
}

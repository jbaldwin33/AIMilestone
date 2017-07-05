using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : MonoBehaviour {

    HingeJoint j;
    JointMotor m;
    GameObject cyl;
	// Use this for initialization
	void Start ()
    {
        cyl = GameObject.Find("Cylinder1");
        j = cyl.GetComponent<HingeJoint>();
        m = new JointMotor();
        
        m.force = 5000;
        m.targetVelocity = 100f;
        j.motor = m;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (cyl.transform.rotation.z > 0.4f && cyl.transform.rotation.z < 0.5f)
        {
            m.targetVelocity = -100f;
            j.motor = m;
        }
        if (cyl.transform.rotation.z < -0.4f && cyl.transform.rotation.z > -0.5)
        {
            m.targetVelocity = 100f;
            j.motor = m;
        }
	}
}

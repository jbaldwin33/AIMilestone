using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RagdollScript : MonoBehaviour {

    Animator anim;
    public float time;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Cylinder" || col.gameObject.name == "axe_s")
        {
            Debug.Log(Time.fixedTime.ToString());
            anim.enabled = false;
            time = Time.fixedTime;
        }
    }
    void Update()
    {
        if ((!anim.enabled) && Time.fixedTime > time + 2.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

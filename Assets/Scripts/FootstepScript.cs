using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]

public class FootstepScript : MonoBehaviour
{
    public AudioClip[] dirt;
    private CharacterController con;
    private AudioSource aud;
    public Animator anim;

    void Awake()
    {
        con = GetComponent<CharacterController>();
        aud = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    /*IEnumerator Start()
    {
        while (true)
        {
            float vel = con.velocity.magnitude;
            RaycastHit hit = new RaycastHit();
            string floortag;
            if (con.isGrounded == true && vel > 0.2f)
            {
                if (Physics.Raycast(transform.position, Vector3.down, out hit))
                {
                    floortag = hit.collider.gameObject.tag;
                    if (floortag == "Plane")
                    {
                        aud.clip = dirt[Random.Range(0, dirt.Length)];
                    }
                }
                Debug.Log("Sound");
                aud.PlayOneShot(aud.clip);
                float interval = aud.clip.length;
                yield return new WaitForSeconds(interval);
            }
            else
            {
                yield return 0;
            }
        }
    }*/
}

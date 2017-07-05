using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListenerScript : MonoBehaviour {

    public string lastMesgRecvd = "";
    public Material[] mats;

    private UnityAction<string> simpleEventListener;
    private ParticleSystem particle;
    private ParticleSystem particle2;

    void Awake()
    {
        simpleEventListener = new UnityAction<string>(SomeFunction);
        particle = GameObject.Find("FootParticle").GetComponent<ParticleSystem>();
        particle2 = GameObject.Find("FootParticleTwo").GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        EventManager.StartListening<SimpleEvent, string>(simpleEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<SimpleEvent, string>(simpleEventListener);
    }

    void SomeFunction(string s)
    {
        lastMesgRecvd = s;

    }
}

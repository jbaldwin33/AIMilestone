using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(AINavSteeringController))]
public class AIDemoController : MonoBehaviour {


    public Transform [] waypointSetA; 

    public Transform [] waypointSetB; 

    public Transform [] waypointSetC; 

	public Transform waypointE;

    public Transform[] waypointSetF;

    private bool gettingAmmo;

    private GameObject guards;


    public enum State {

        A,B,C,D,E,GetAmmo,Stop

    }


    public State state = State.A;

    public float waitTime = 5f;

    protected float beginWaitTime;


    AINavSteeringController aiSteer;


	// Use this for initialization
	void Start () {

        aiSteer = GetComponent<AINavSteeringController>();

        aiSteer.Init();

        aiSteer.waypointLoop = false;
        aiSteer.stopAtNextWaypoint = false;

        gettingAmmo = false;

        guards = GameObject.Find("AllGuards");

        transitionToStateA();
		
	}
	

    void transitionToStateA() {

        print("Transition to state A");

        state = State.A;

        aiSteer.setWayPoints(waypointSetA);

        aiSteer.useNavMeshPathPlanning = true;


    }


    void transitionToStateB() {

        print("Transition to state B");

        state = State.B;

        aiSteer.setWayPoints(waypointSetB);

        aiSteer.useNavMeshPathPlanning = true;
    }


    void transitionToStateC() {

        print("Transition to state C");

        state = State.C;

        aiSteer.setWayPoints(waypointSetC);

        aiSteer.useNavMeshPathPlanning = false;

    }

    void transitionToStateD() {

        print("Transition to state D");

        state = State.D;

        beginWaitTime = Time.timeSinceLevelLoad;

		aiSteer.clearWaypoints ();

		aiSteer.useNavMeshPathPlanning = true;

    }


	void transitionToStateE() {

		print("Transition to state E");

		state = State.E;
	
		aiSteer.setWayPoint (waypointE);

		aiSteer.useNavMeshPathPlanning = true;

	}

    void transitionToStateGetAmmo() {

        print("Transition to state Get Ammo");

        state = State.GetAmmo;

        aiSteer.setWayPoints(waypointSetF);

        aiSteer.useNavMeshPathPlanning = true;
    }

    void transitionToStop() {
        print("Transition to state Stop");

        state = State.Stop;

        beginWaitTime = Time.timeSinceLevelLoad;

        aiSteer.useNavMeshPathPlanning = true;
    }

	// Update is called once per frame
	void Update () {
		
        if (this.gameObject.GetComponent<AmmoScript>().ammo == 0 && !gettingAmmo)
        {
            gettingAmmo = true;
            transitionToStateGetAmmo();
        }

        if (GameObject.Find("Guard").GetComponent<AIGuard>().state == AIGuard.State.B)
        {
            transitionToStop();
        }


        switch (state)
        {
            case State.A:
                
                if (aiSteer.waypointsComplete())
                    transitionToStateB();
                break;

            case State.B:
                if (aiSteer.waypointsComplete())
                    transitionToStateA();
                break;

            case State.C:
                if (aiSteer.waypointsComplete())
                    transitionToStateD();
                break;

            case State.D:
                if (Time.timeSinceLevelLoad - beginWaitTime > waitTime)
                    transitionToStateE();
                break;

		    case State.E:
				if (aiSteer.waypointsComplete())
					transitionToStateA();
			    break;

            case State.GetAmmo:
                if (this.gameObject.GetComponent<AmmoScript>().ammo > 0)
                {
                    gettingAmmo = false;
                    transitionToStateA();
                }
                if (aiSteer.waypointsComplete())
                {
                    this.gameObject.GetComponent<AmmoScript>().ammo = 5;
                    gettingAmmo = false;
                    transitionToStateA();
                }
                break;

            case State.Stop:
                //foreach (Transform child in guards.transform)
                //{
                //if (child.GetComponent<AIGuard>().state == AIGuard.State.B)
                if (Time.timeSinceLevelLoad - beginWaitTime > waitTime)
                {
                    if (GameObject.Find("Guard").GetComponent<AIGuard>().state == AIGuard.State.A)
                    {
                        transitionToStateA();
                    }
                }
                break;

            default:

                print("Weird?");
                break;
        }


	}
}

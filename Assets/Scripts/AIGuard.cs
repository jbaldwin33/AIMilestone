using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AINavSteeringController))]
public class AIGuard : MonoBehaviour
{


    public Transform waypointA;

    public Transform waypointB;




    public enum State
    {

        A, B, C

    }


    public State state = State.A;
    private State previousState;

    public float waitTime = 5f;

    protected float beginWaitTime;


    AINavSteeringController aiSteer;


    // Use this for initialization
    void Start()
    {

        aiSteer = GetComponent<AINavSteeringController>();

        aiSteer.Init();

        aiSteer.waypointLoop = false;
        aiSteer.stopAtNextWaypoint = false;

        transitionToStateA();

    }


    void transitionToStateA()
    {

        print("Transition to state A");

        state = State.A;

        aiSteer.setWayPoint(waypointA);

        aiSteer.useNavMeshPathPlanning = true;


    }


    void transitionToStateB()
    {

        print("Transition to state B");

        state = State.B;

        aiSteer.setWayPoint(waypointB);

        aiSteer.useNavMeshPathPlanning = true;
    }


    void transitionToStateC()
    {

        print("Transition to state C");
        
        state = State.C;

        beginWaitTime = Time.timeSinceLevelLoad;

        aiSteer.clearWaypoints();

        aiSteer.useNavMeshPathPlanning = true;

    }


    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.A:

                if (aiSteer.waypointsComplete())
                {
                    previousState = State.A;
                    transitionToStateC();
                }
                break;

            case State.B:
                if (aiSteer.waypointsComplete())
                {
                    previousState = State.B;
                    transitionToStateC();
                }
                break;

            case State.C:
                if (Time.timeSinceLevelLoad - beginWaitTime > waitTime)
                {
                    if (previousState == State.A)
                    {
                        previousState = State.C;
                        transitionToStateB();
                    }
                    else if (previousState == State.B)
                    {
                        previousState = State.C;
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
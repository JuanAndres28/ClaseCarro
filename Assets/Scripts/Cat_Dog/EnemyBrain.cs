using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private string initState; //initial state of the enemy (Chase State)
    [SerializeField] private FSMState[] states; 

    public FSMState CurrentState {get; set;}
    public Transform CarPlayer {get; set;}

    private void Start()
    {
        ChangeState(initState); //start the game with this state
    }

    private void Update ()
    {
        //if (CurrentState == null) return; this is the same as adding ?
        CurrentState?.UpdateState(this); //EnemyBrain //Update if its not null
    }

    public void ChangeState(string newStateID)
    {
        FSMState newState = GetState(newStateID); //If we find a State with this ID we are saving that state reference
        if (newState == null) return; //if this is not true/fount the state
        CurrentState = newState; 
    }

    private FSMState GetState(string newStateID)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].ID == newStateID)
            {
                return states [i];
            } //checking if the ID of all the states is equal to the new StateID, find the state inside the array
        }

        return null; //If we dont find the newStateID
    }

}

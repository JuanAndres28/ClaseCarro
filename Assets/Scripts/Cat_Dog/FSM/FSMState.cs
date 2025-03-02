using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FSMState
{
    //Safe the logic of each state, true or false
    public string ID; 
    public FSMAction[] Actions; //ChaseAction, AttackAcion, etc. 
    public FSMTransition [] Transitions; 

    public void UpdateState(EnemyBrain enemyBrain)
    {
        ExecuteActions();
        ExecuteTransitions(enemyBrain);
    }

    private void ExecuteActions()
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }

    private void ExecuteTransitions(EnemyBrain enemyBrain)
    {
        if (Transitions == null || Transitions.Length <=0) return; //means that we have transitions to execute
        for (int i = 0; i < Transitions.Length;i++) 
        {
            bool value = Transitions[i]. Decision.Decide(); //Is this equal to true or false?
            if (value)
        {
            enemyBrain.ChangeState(Transitions[i].TrueState);
        }
        else
        {
            enemyBrain.ChangeState(Transitions[i].FalseState);
        }
        }
        
    }
}
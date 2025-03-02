using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FSMTransition 
{
   public FSMDecision Decision; //PlayerInRange of attack, return true or false with decise method
   public string TrueState; //Want to transition to CurrentState to Atack
   public string FalseState; //CurrentState to Chase state
}

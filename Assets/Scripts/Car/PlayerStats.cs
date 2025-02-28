using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")] 
public class PlayerStats : ScriptableObject
{
    

  [Header("Health")]
  public float Health;
  public float MiniToys; 

  //[Header("HUD")]
  //public float Speed;
  //public float maxBrake;
  //public float maxAngle;

   public void ResetPlayer()
  {
    Health = 1;
    MiniToys=00;
  }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : FSMAction
{
    [Header("Config")]    
    [SerializeField] private float damage; 
    [SerializeField] private float timeBtwAttack;

    private EnemyBrain enemyBrain;
    private float timer;

    private void Awake() 
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
   public override void Act()
   {
    AttackPlayer();
   }

   private void AttackPlayer()
   {
    if (enemyBrain.CarPlayer == null) return;
    timer -= Time.deltaTime;
    if (timer <= 0f )
    {
        IDamageable carPlayer =  
        enemyBrain.CarPlayer.GetComponent<IDamageable>(); //Implements player health
        CarHealth carHealth = enemyBrain.CarPlayer.GetComponent<CarHealth>();
         if (carHealth != null) // Asegúrate de que carHealth no sea nulo
        {
            carHealth.TakeDamage(damage); // Llama a TakeDamage en la instancia de CarHealth
        }
        timer = timeBtwAttack;
    }
   }
}
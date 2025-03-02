using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : FSMAction
{
   [Header("Config")]
    [SerializeField] private float chaseSpeed;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override void Act()
    {
        ChasePlayer(); 
    }

    private void ChasePlayer()
    {
        if (enemyBrain.CarPlayer == null) return; // El jugador no está en el rango del enemigo

        // Calcula la dirección hacia el jugador
        Vector3 dirToPlayer = enemyBrain.CarPlayer.position - transform.position;

        // Verifica si el enemigo está lo suficientemente lejos del jugador
        if (dirToPlayer.magnitude >= 1.3f)
        {
            // Mueve al enemigo hacia el jugador de manera suave
            Vector3 newPosition = Vector3.MoveTowards(transform.position, enemyBrain.CarPlayer.position, chaseSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
    }
}
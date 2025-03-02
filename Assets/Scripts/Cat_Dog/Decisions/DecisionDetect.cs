using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionDetect : FSMDecision

{
     [Header("Config")]
    [SerializeField] private float range; 
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return DetectPlayer();
    }

    private bool DetectPlayer()
    {
         Collider[] playerColliders = Physics.OverlapSphere(enemy.transform.position, range, playerMask);
        
        if (playerColliders.Length > 0) // Verificamos si hay colisionadores
        {
            enemy.CarPlayer = playerColliders[0].transform; // Asignamos el primer colisionador encontrado
            Debug.Log("detected player");
            return true;
        }

        enemy.CarPlayer = null; // Si no hay colisionadores, aseguramos que CarPlayer sea nulo
        return false;
    }

    private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red; 
            Gizmos.DrawWireSphere(transform.position, range);
        }
}

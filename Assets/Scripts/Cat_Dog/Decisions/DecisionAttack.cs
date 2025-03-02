using UnityEngine;

public class DecisionAttack : FSMDecision
{
    [Header("Config")]
    [SerializeField] private float attackRange; 
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return PlayerInAttackRange();
    }

    private bool PlayerInAttackRange()
    {
        if(enemy.CarPlayer == null) return false; //We dont have player reference
        Collider[] playerCollider = Physics.OverlapSphere(enemy.transform.position, attackRange, playerMask);
        if(playerCollider != null)
        {
            Debug.Log("attack player");
            return true; //Player is in atack range
        }

        return false;
    }

    private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

}

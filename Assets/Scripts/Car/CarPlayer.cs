using UnityEngine;

public class CarPlayer : MonoBehaviour
{
    // Almacenar xp, objetos, etc., del jugador
    [Header("Config")]
    [SerializeField] private PlayerStats stats; 

    public CarHealth CarHealth { get; private set;}

    // Propiedad para acceder a la variable privada 'stats'
    public PlayerStats Stats => stats;

    private void Awake()
    {
        CarHealth = GetComponent<CarHealth>();
    }

    public void ResetPlayer()
    {
        stats.ResetPlayer();
    }
}

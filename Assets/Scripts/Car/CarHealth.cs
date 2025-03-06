using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarHealth : MonoBehaviour, IDamageable
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Transform spawnCarPosition;

    private Vector3 initialPosition;

    private void Update()
    {
        if (stats.Health <= 0f)
        {
            PlayerDead();
        }

        if (Input.GetKeyDown(KeyCode.K)) // Testeo daño
        {
            TakeDamage(1); // Reduce 10 de vida al presionar la tecla
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            AddHealth(1);
        }
    }

    public void TakeDamage(float amount)
    {
        if (stats.Health <= 0f) return;
        stats.Health -= amount; // Reducir la salud
        Debug.Log("vida" + stats.Health);
        if (stats.Health <= 0f) // Verificar si el jugador sigue vivo
        {
            stats.Health = 0f;
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        Debug.Log("el jugador murio");

        // Obtener el Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();

        // Detener la velocidad
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Mostrar una img, audio, etc.
        // StartCoroutine(RespawnCoroutine());
    }

    public void AddHealth(float amount)
    {
        stats.MiniToys += amount;
        stats.Health += amount;
        Debug.Log("vida" + stats.Health);
        Debug.Log("MiniToys" + stats.MiniToys);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("MiniToys"))
        {
            AddHealth(1);
            Destroy(collision.gameObject); // Destruir el miniToy una vez recogido
        }
    }
}

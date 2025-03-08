using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarHealth : MonoBehaviour, IDamageable
{
    [Header("Config")]
    public List<GameObject> miniToys;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Transform spawnCarPosition;

    private Vector3 initialPosition;
    private List<MiniToys> collectedMiniToys = new List<MiniToys>();

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
      if (collectedMiniToys.Count > 0)
        {
            int lastIndex = collectedMiniToys.Count - 1; // Obtener el índice del último MiniToy
            MiniToys miniToyToDestroy = collectedMiniToys[lastIndex]; // Obtener el último MiniToy
            collectedMiniToys.RemoveAt(lastIndex); // Eliminarlo de la lista
            Destroy(miniToyToDestroy.gameObject); // Destruir el objeto
            stats.MiniToys--; // Decrementar el contador de MiniToys
        }
    }

    private void PlayerDead()
    {
        Debug.Log("el jugador murio");
        StartCoroutine(RespawnCoroutine());

        // Mostrar una img, audio, etc.
    }

    private IEnumerator RespawnCoroutine()
    {
        // Opcional: Esperar antes de reaparecer
        yield return new WaitForSeconds(2f);

        // Reaparecer al jugador en el punto de reaparición
        if (spawnCarPosition != null)
        {
            transform.position = spawnCarPosition.position;
        }
        else
        {
            transform.position = initialPosition; // Reaparecer en la posición inicial si no hay punto de reaparición definido
        }

        stats.Health = stats.MaxHealth;
        stats.MiniToys = 00; 

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
             MiniToys miniToy = collision.gameObject.GetComponent<MiniToys>();
        if (miniToy != null)
       {
            int index = collectedMiniToys.Count; // Obtener el índice actual
            miniToy.Collect(index); // Llama al método Collect del MiniToy con el índice
            collectedMiniToys.Add(miniToy); // Agregar el MiniToy a la lista
        }

        
            AddHealth(1);
            
        }
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats; 
    [SerializeField] private Transform spawnCarPosition;
    //public GameObject miniToyPrefab; // Prefab del objeto que seguirá al jugador
    //private GameObject currentMiniToy; // Referencia al objeto recogido


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


    public void TakeDamage(int amount)  
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
    //quitar movimiento
    //mostrar una img? audio?, etc.
    //StartCoroutine(RespawnCoroutine());
}

public void AddHealth(float amount)
    {

        stats.MiniToys += amount;
        stats.Health += amount;
        Debug.Log("vida" + stats.Health);
        Debug.Log("MiniToys" + stats.MiniToys);  
        /*if (stats.Health > stats.MaxHealth)
        {
            stats.Health = stats.MaxHealth;
        } */
    }

private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("MiniToys"))
        {
            // Solo recoger si no hay un mini toy ya recogido
            //if (currentMiniToy == null){
                AddHealth(1);
                Destroy(collision.gameObject); // Destruir el miniToy una vez recogido
                //CollectMiniToy();}
        }
    }


/*private void CollectMiniToy()
{
    if (currentMiniToy == null)
        {
            // Instanciar el objeto recogido
            currentMiniToy = Instantiate(miniToyPrefab, transform.position, Quaternion.identity);
            currentMiniToy.GetComponent<MiniToys>().SetTarget(transform); // Pasar la referencia del jugador
        }
}*/ 
/*private IEnumerator RespawnCoroutine()
    {
        // Opcional: Esperar antes de reaparecer
        yield return new WaitForSeconds(2f);

        // Reaparecer al jugador en el punto de reaparición
        if (spawnCarPosition;!= null)
        {
            transform.position = spawnCarPosition.position;
        }
        else
        {
            transform.position = initialPosition; // Reaparecer en la posición inicial si no hay punto de reaparición definido
        }

        stats.Health = 1;

    }*/

}

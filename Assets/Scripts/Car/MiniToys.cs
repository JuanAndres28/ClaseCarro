using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniToys : MonoBehaviour
{ 
    public float followDistance = 2.0f;
    public float followSpeed = 5.0f;
    public Transform car; 
    [SerializeField] private PlayerStats stats; 
    //public Cars car;


    /*void Update()
    {
        if (car != null && stats.Health != 0)
        {
            // Calcular cuántos objetos deben seguir al jugador
            float followCount = stats.Health; // Número de vidas determina cuántos objetos siguen

            // Asegurarse de que el número de objetos no exceda un límite
            followCount = Mathf.Clamp(followCount, 0, 3); // Limitar a un máximo de 3 objetos

            // Calcular la posición objetivo
            Vector3 targetPosition = car.position - car.forward * followDistance * followCount;
            targetPosition.y = transform.position.y; // Mantener la altura del objeto

            // Mover el objeto hacia la posición objetivo
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform carTarget)
    {
        car = carTarget; // Asignar el jugador como objetivo
    }

    /*public void InstantiateMiniToys()
{
    // Obtener el número de vidas del jugador
    int numberOfMiniToys = stats.Health; // Asumiendo que tienes una referencia a PlayerHealth

    for (int i = 0; i < numberOfMiniToys; i++)
    {
        // Calcular la posición para cada MiniToy
        Vector3 position = car.position + new Vector3(i * 1.5f, 0, 0); // Espaciado en el eje X

        // Instanciar el MiniToy
        Instantiate(miniToyPrefab, position, Quaternion.identity);
    }
}*/
}

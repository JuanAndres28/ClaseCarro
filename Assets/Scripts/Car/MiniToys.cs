using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniToys : MonoBehaviour
{ 
    public static MiniToys instance;
    public Transform target;
    // Variable que almacena un vector 3 con la diferencia que se quiere entre la c�mara y el carro.
    [SerializeField] private Vector3 offset;

    // Almacena la velocidad a la que la c�mara sigue al carro.
    [SerializeField] private float followSpeed;

    // Almacena la velocidad a la que la c�mara rota con el carro.
    [SerializeField] private float rotationSpeed;
 
    [SerializeField] private PlayerStats stats; 

    private bool shouldFollow = false; 
    private Vector3 currentOffset;

    private void Awake()
    {
        if(instance == null) 
        { 
            instance = this;
        }

        shouldFollow = false; 
    }

     void FixedUpdate()
    {
        // Se inicializan los m�todos.
        if (shouldFollow)
        {
            FollowTarget();
            RotationTarget();
        }
    }

      public void Collect(int index)
    {
       // Cambia el offset basado en el índice del MiniToy recogido
        currentOffset = offset + new Vector3(0, -index, 0); // Ajusta el valor según sea necesario
        shouldFollow = true; // Iniciar el seguimiento

        // Colocar el MiniToy en la posición inicial con el nuevo offset
        transform.position = target.position + currentOffset; // Aplicar el nuevo offset inmediatamente
    }

    // M�todo que permite seguir al carro.
    private void FollowTarget()
    {
        if(target != null)
        {
            // Usa el currentOffset en lugar de offset
            var targetPos = target.TransformPoint(currentOffset);
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.fixedDeltaTime);
        }
       
    }

    // M�todo que permite rotar junto al carro
    private void RotationTarget()
    {
        // Si el target tiene alg�n elemento, sucede lo de adentro.
        if (target != null) 
        {
            // A una variable local se le asigna la direcci�n, la cual es la diferencia entre la posici�n del objetivo y la posici�n del objeto.
            var direction = target.position - transform.position;

            // A una variable local se le asigna la rotaci�n final.
            var rotation = Quaternion.LookRotation(direction, Vector3.up);

            // Permite una rotaci�n suave, dada por la velocidad de rotaci�n.
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);

        }
        
    }





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

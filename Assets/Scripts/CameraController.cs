using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Se crea un singleton para referenciar este script desde otros.
    public static CameraController instance;

    // Variable que almacena el objetivo al que la c�mara va a seguir.
    [SerializeField] private Transform target;

    // Variable que almacena un vector 3 con la diferencia que se quiere entre la c�mara y el carro.
    [SerializeField] private Vector3 offset;

    // Almacena la velocidad a la que la c�mara sigue al carro.
    [SerializeField] private float followSpeed;

    // Almacena la velocidad a la que la c�mara rota con el carro.
    [SerializeField] private float rotationSpeed;

    // Se finaliza de configurar el singleton.
    private void Awake()
    {
        if(instance == null) 
        { 
            instance = this;
        }
    }

   void FixedUpdate()
    {
        // Se inicializan los m�todos.
        FollowTarget();
        RotationTarget();
    }

    // M�todo que permite seguir al carro.
    private void FollowTarget()
    {
        // Si el target tiene alg�n elemento, sucede lo de adentro.
        if(target != null)
        {
            // A una variable local llamada targetPos se toma la posici�n del objetivo con el offset creado anteriormente.
            var targetPos = target.TransformPoint(offset);

            // Permite un seguimiento suave, dado por la velocidad de seguimiento.
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
}

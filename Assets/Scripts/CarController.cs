using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    // Variable para guardar la informaci�n del input del usuario.
    private Vector2 inputM;

    // Almacena el componente que tiene el player del input.
    private PlayerInput playerInput;

    public Cars car;
    // Almacena el componente de Rigidbody que tiene el player.
    private Rigidbody rb;

    // Variable para almacenar el float horizontal y vertical.
    private float horizontalInput;
    private float verticalInput;

    // Variable para almacenar el �ngulo de las ruedas.
    private float steering;

    // Variables que almacenan los Wheel Colliders de todas las ruedas.
    [Header("Wheel Data")]
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider backRightCollider;
    [SerializeField] private WheelCollider backLeftCollider;

    // Variables que almacenan los objetos f�sicos de las ruedas.
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform backRightTransform;
    [SerializeField] private Transform backLeftTransform;

    [Header("Values")]

    // Varaiable que almacena la velocidad del objeto.
    private float motorForce;

   

    // Start is called before the first frame update
    void Start()
    {
        // Se le asigna el componente real al player input y al Rigidbody.
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        motorForce = car.MotorForce;
        // Permite modificar el centro de gravedad del objeto, en este caso se baj� para que el carro no se volteara.
        //rb.centerOfMass = new Vector3(0f, -0.5f, 0f);

    }

    private void FixedUpdate()
    {
        // Se inicializan los m�todos
        GetInput();
        Motor();
        Steering();
        UpdateWheels();
        
    }

    // M�todo ue almacena el input del jugador-
    private void GetInput()
    {
        // Se almacena en la variable el input.
        inputM = playerInput.actions["Move"].ReadValue<Vector2>();

        // El input se divide en dos, el vector x en la variable horizontal y el y en la vertical.
        horizontalInput = inputM.x;
        verticalInput = inputM.y;
    }

    // M�todo que permite agregarle fuerza al motor.
    private void Motor()
    {
        
        // Con la opci�n motorToruqe de collisionador de las ruedas frontales, se le puede agregar la velocidad y se multiplica por el input vertical.
        frontLeftCollider.motorTorque = verticalInput * motorForce;
        frontRightCollider.motorTorque = verticalInput * motorForce;
    }

    // M�todo que permite inicializar los frenos
    public void Break(InputAction.CallbackContext context)
    {
        // Cuando el evento se encuentra en ejecuci�n, y, usando la opci�n brakeToqrque de los colisionadores de las ruedas, se le asigna una fuerza de frenado.
        if (context.performed) 
        {
            frontLeftCollider.brakeTorque = car.BrakeForce;
            frontRightCollider.brakeTorque = car.BrakeForce;
            backLeftCollider.brakeTorque = car.BrakeForce;
            backRightCollider.brakeTorque = car.BrakeForce;
        }

        // Cuando el evento termina o cancela, la fuerza de frenado se devuelve a 0 para que pueda volver a moverse.
        if (context.canceled) 
        {
            frontLeftCollider.brakeTorque = 0;
            frontRightCollider.brakeTorque = 0;
            backLeftCollider.brakeTorque = 0;
            backRightCollider.brakeTorque = 0;

        }

    }

    // M�todo que permite asignarle a las ruedas frontales el giro.
    private void Steering()
    {
        // La variable steering es igual al �ngulo m�ximo por el input horizontal, para que verifique si se gira a la derecha o a la izquierda.
        steering = car.MaxSteeringAngle * horizontalInput;
        frontLeftCollider.steerAngle = steering;
        frontRightCollider.steerAngle = steering;
    }

    // M�todo que permite actualizar el movimiento visual de las ruedas con el de los colisionadores.
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontRightCollider, frontRightTransform);
        UpdateSingleWheel(frontLeftCollider, frontLeftTransform);
        UpdateSingleWheel(backRightCollider, backRightTransform);
        UpdateSingleWheel(backLeftCollider, backLeftTransform);
    }

    // M�todo que configura la actualizaci�n anterior.
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        // Se crean dos variables locales, una de posici�n y otra de rotaci�n.
        Vector3 pos;
        Quaternion quat;

        // Se obtiene la posici�n y rotaci�n actual de los colisionadores.
        wheelCollider.GetWorldPose(out pos, out quat);

        // Se le asigna a las ruedas visuales la posici�n y rotaci�n obtenida.
        wheelTransform.position = pos;
        wheelTransform.rotation = quat;
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    // Variable para guardar la información del input del usuario.
    private Vector2 inputM;

    // Almacena el componente que tiene el player del input.
    private PlayerInput playerInput;

    // Almacena el componente de Rigidbody que tiene el player.
    private Rigidbody rb;

    // Variable para almacenar el float horizontal y vertical.
    private float horizontalInput;
    private float verticalInput;

    // Variable para almacenar el ángulo de las ruedas.
    private float steering;

    [Header("Car")]
    [SerializeField] private Cars car;

    // Variables que almacenan los Wheel Colliders de todas las ruedas.
    [Header("Wheel Data")]
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider backRightCollider;
    [SerializeField] private WheelCollider backLeftCollider;

    // Variables que almacenan los objetos físicos de las ruedas.
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform backRightTransform;
    [SerializeField] private Transform backLeftTransform;

    [Header("Values")]

    // Variable que almacena la velocidad del objeto.
    [SerializeField] private float motorForce;

    // Variable que almacena la fuerza de los frenos del objeto.
    [SerializeField] private float brakeForce;

    // Variable que almacena el ángulo máximo a darle a las ruedas en el giro.
    [SerializeField] private float maxSteeringAngle;

    // Variables para el turbo
    [Header("Turbo Settings")]
    [SerializeField] private float turboSpeedMultiplier = 2.0f; // Multiplicador de velocidad durante el turbo
    [SerializeField] private float turboDuration = 5.0f; // Duración del turbo en segundos
    [SerializeField] private KeyCode turboKey = KeyCode.LeftShift; // Tecla para activar el turbo

    private bool isTurboActive = false;
    private bool hasTurbo = false; // Indica si el jugador tiene el turbo disponible
    private float turboTimer = 0f;
    private float originalMotorForce; // Almacena la fuerza original del motor

    // Referencia al Slider de la UI para la barra de turbo
    [Header("UI")]
    [SerializeField] private Slider turboBar;

    // Start is called before the first frame update
    void Start()
    {
        // Se le asigna el componente real al player input y al Rigidbody.
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        // Permite modificar el centro de gravedad del objeto, en este caso se bajó para que el carro no se volteara.
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);

        // Guarda la fuerza original del motor
        originalMotorForce = motorForce;

        // Configura el Slider de turbo
        if (turboBar != null)
        {
            turboBar.maxValue = turboDuration;
            turboBar.value = 0f; // Inicia vacío
            turboBar.gameObject.SetActive(false); // Oculta la barra al inicio
        }
    }

    private void FixedUpdate()
    {
        // Se inicializan los métodos
        GetInput();
        Motor();
        Steering();
        UpdateWheels();

        // Manejar el turbo
        HandleTurbo();
    }

    // Método que maneja la lógica del turbo
    private void HandleTurbo()
    {
        // Activar el turbo cuando se presione la tecla asignada y el jugador tenga el turbo disponible
        if (Input.GetKeyDown(turboKey) && hasTurbo && !isTurboActive)
        {
            ActivateTurbo();
        }

        // Si el turbo está activo, contar el tiempo
        if (isTurboActive)
        {
            turboTimer += Time.fixedDeltaTime;

            // Actualizar la barra de turbo
            if (turboBar != null)
            {
                turboBar.value = turboDuration - turboTimer;
            }

            // Desactivar el turbo después de la duración especificada
            if (turboTimer >= turboDuration)
            {
                DeactivateTurbo();
            }
        }
    }

    // Método para activar el turbo
    private void ActivateTurbo()
    {
        motorForce *= turboSpeedMultiplier; // Aumenta la fuerza del motor
        isTurboActive = true;
        hasTurbo = false; // El turbo se consume al usarlo
        turboTimer = 0f; // Reinicia el temporizador
        Debug.Log("Turbo activado!");

        // Activar la barra de turbo
        if (turboBar != null)
        {
            turboBar.gameObject.SetActive(true);
            turboBar.value = turboDuration; // Llenar la barra al activar
        }
    }

    // Método para desactivar el turbo
    private void DeactivateTurbo()
    {
        motorForce = originalMotorForce; // Restaura la fuerza original del motor
        isTurboActive = false;
        Debug.Log("Turbo desactivado.");

        // Desactivar la barra de turbo
        if (turboBar != null)
        {
            turboBar.gameObject.SetActive(false);
        }
    }

    // Método que detecta colisiones con objetos recolectables
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto tiene el tag "TurboItem"
        if (other.CompareTag("TurboItem"))
        {
            CollectTurboItem(other.gameObject);
        }
    }

    // Método para recolectar el ítem de turbo
    private void CollectTurboItem(GameObject turboItem)
    {
        hasTurbo = true; // El jugador ahora tiene turbo disponible
        Destroy(turboItem); // Destruye el objeto recolectable
        Debug.Log("¡Ítem de turbo recolectado!");
    }

    // Método que almacena el input del jugador.
    private void GetInput()
    {
        // Se almacena en la variable el input.
        inputM = playerInput.actions["Move"].ReadValue<Vector2>();

        // El input se divide en dos, el vector x en la variable horizontal y el y en la vertical.
        horizontalInput = inputM.x;
        verticalInput = inputM.y;
    }

    // Método que permite agregarle fuerza al motor.
    private void Motor()
    {
        // Con la opción motorTorque de collisionador de las ruedas frontales, se le puede agregar la velocidad y se multiplica por el input vertical.
        frontLeftCollider.motorTorque = verticalInput * motorForce;
        frontRightCollider.motorTorque = verticalInput * motorForce;
    }

    // Método que permite inicializar los frenos
    public void Break(InputAction.CallbackContext context)
    {
        // Cuando el evento se encuentra en ejecución, y, usando la opción brakeTorque de los colisionadores de las ruedas, se le asigna una fuerza de frenado.
        if (context.performed)
        {
            frontLeftCollider.brakeTorque = brakeForce;
            frontRightCollider.brakeTorque = brakeForce;
            backLeftCollider.brakeTorque = brakeForce;
            backRightCollider.brakeTorque = brakeForce;
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

    // Método que permite asignarle a las ruedas frontales el giro.
    private void Steering()
    {
        // La variable steering es igual al ángulo máximo por el input horizontal, para que verifique si se gira a la derecha o a la izquierda.
        steering = maxSteeringAngle * horizontalInput;
        frontLeftCollider.steerAngle = steering;
        frontRightCollider.steerAngle = steering;
    }

    // Método que permite actualizar el movimiento visual de las ruedas con el de los colisionadores.
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontRightCollider, frontRightTransform);
        UpdateSingleWheel(frontLeftCollider, frontLeftTransform);
        UpdateSingleWheel(backRightCollider, backRightTransform);
        UpdateSingleWheel(backLeftCollider, backLeftTransform);
    }

    // Método que configura la actualización anterior.
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        // Se crean dos variables locales, una de posición y otra de rotación.
        Vector3 pos;
        Quaternion quat;

        // Se obtiene la posición y rotación actual de los colisionadores.
        wheelCollider.GetWorldPose(out pos, out quat);

        // Se le asigna a las ruedas visuales la posición y rotación obtenida.
        wheelTransform.position = pos;
        wheelTransform.rotation = quat;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    // Variables que almacenan los datos para las barras de valores.
    [Header("Car Values")]
    public Scrollbar speedSB;
    public Scrollbar brakeSB;
    public Scrollbar angleSB;
    public float maxSpeed;
    public float maxBrake;
    public float maxAngle;

    // Lista en donde se almacenan los carros que se van a manejar.
    public Cars[] carList = new Cars[3];

    // Variable que almacena cual es el carro seleccionado.
    private Cars selectedCar;

    // Variable que almacena una posición en el espacio para que el carro aparezca.
    public Transform spawnCarPosition;

    // Variables de la interfaz gráfica para que varien dependiendo del carro elegido.
    [SerializeField] private Image carImage;
    [SerializeField] private TextMeshProUGUI carName;

    // Índice para recorrer el arreglo.
    private int carIndex;

    private void Awake()
    {
        // El índice se inicializa en 0.
        carIndex = 0;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // El carro seleccionado es el primer carro del arreglo.
        selectedCar = carList[carIndex];

        // Se llama el método para actualizar los datos de la pantalla.
        UpdateSelection();
    }

    public void UpdateSelection()
    {
        // Cuando se efectúa el método, la imagen de la interfaz se actualiza con la del carro seleccionado.
        carImage.sprite = selectedCar.CarImage;

        // Cuando se efectúa el método, el texto de la interfaz se actualiza con el del carro seleccionado.
        carName.text = selectedCar.CarName;

        // Se llama el método que actualiza las barras con los datos.
        SetScrollBars();
    }

    // Método que se llama con el botón.
    public void ChosenCar()
    {
        // Se instancia el prefab del carro seleccionado y se almacena en una variable.
       var chosenCar =  Instantiate(selectedCar.Car, spawnCarPosition.position, Quaternion.identity);
        
        // Se le indica a la cámara que el objetivo es el carro seleccionado.
       CameraController.instance.target = chosenCar.transform;

        // Se guarda la instancia dentro de una ubicación en el inspector.
       chosenCar.transform.parent = spawnCarPosition.transform;
    }

    // Método para cambiar el carro seleccionado.
    public void ChangeCarRight()
    {
        // Valida que si el indice es menor al máximo de carros, que suba, y con ello sube el carro de la lista.
        // luego el carro seleccionadon cambia y se aplican los cambios en la interfaz.
        if (carIndex < carList.Length - 1) 
        {
            carIndex++;
            selectedCar = carList[carIndex];
            UpdateSelection();
        }
        else
        {
            // Si se supera el tamaño del arreglo, el índice vuelve a cero, se actualiza el carro seleccionado y
            // los cambios en la interfaz.
            carIndex = 0;
            selectedCar = carList[carIndex];
            UpdateSelection();
        }

    }

    // Método que actualiza los valores de las barras.
    void SetScrollBars()
    {
        // A cada barra se le da un valor de 0 - 1, dividiento los valores de cada carro con su máximo.
        speedSB.size = Mathf.Clamp01(selectedCar.MotorForce / maxSpeed);   
        brakeSB.size = Mathf.Clamp01(selectedCar.BrakeForce / maxBrake);   
        angleSB.size = Mathf.Clamp01(selectedCar.MaxSteeringAngle / maxAngle);   
        
    }
}

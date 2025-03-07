using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


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

    // Variable que almacena cuál es el carro seleccionado.
    private Cars selectedCar;

    // Variables de la interfaz gráfica para que varíen dependiendo del carro elegido.
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

    // Método que se llama con el botón "Seleccionar".
    public void ChosenCar()
    {
        // Obtener el nivel seleccionado.
        string selectedLevel = PlayerPrefs.GetString("SelectedLevel", "Nivel1"); // Por defecto Nivel1

        // Cargar la escena del nivel seleccionado.
        SceneManager.LoadScene(selectedLevel);

        // Instanciar el carro en la escena del nivel.
        StartCoroutine(InstantiateCarAfterSceneLoad(selectedLevel));
    }

    // Corrutina para instanciar el carro después de que la escena se haya cargado.
    private IEnumerator InstantiateCarAfterSceneLoad(string level)
    {
        // Esperar a que la escena se cargue completamente.
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == level);

        // Buscar la posición de spawn en la escena cargada.
        GameObject spawnPoint = GameObject.Find(level + "SpawnPoint"); // Busca un objeto llamado "Nivel1SpawnPoint" o "Nivel2SpawnPoint"
        if (spawnPoint == null)
        {
            Debug.LogError("No se encontró el punto de spawn en la escena.");
            yield break;
        }

        // Instanciar el carro en la posición de spawn.
        var chosenCar = Instantiate(selectedCar.Car, spawnPoint.transform.position, spawnPoint.transform.rotation);

        // Indicar a la cámara que el objetivo es el carro seleccionado.
        CameraController.instance.target = chosenCar.transform;
    }

    // Método para cambiar el carro seleccionado.
    public void ChangeCarRight()
    {
        // Valida que si el índice es menor al máximo de carros, que suba, y con ello sube el carro de la lista.
        // Luego el carro seleccionado cambia y se aplican los cambios en la interfaz.
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
        // A cada barra se le da un valor de 0 - 1, dividiendo los valores de cada carro con su máximo.
        speedSB.size = Mathf.Clamp01(selectedCar.MotorForce / maxSpeed);
        brakeSB.size = Mathf.Clamp01(selectedCar.BrakeForce / maxBrake);
        angleSB.size = Mathf.Clamp01(selectedCar.MaxSteeringAngle / maxAngle);
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Importar SceneManagement

public class LevelSelection : MonoBehaviour
{
    // Lista de nombres de niveles disponibles.
    public string[] niveles = { "Nivel1", "Nivel2" };

    // Índice del nivel actualmente seleccionado.
    private int nivelIndex = 0;

    // Texto para mostrar el nombre del nivel seleccionado.
    public TextMeshProUGUI textoNivel;

    // Imagen para mostrar el nivel seleccionado.
    public Image imagenNivel;

    // Lista de imágenes correspondientes a cada nivel.
    public Sprite[] imagenesNiveles;

    // Nombre de la escena de selección de carros.
    public string nombreEscenaSeleccionCarro = "SeleccionCarro";

    private void Start()
    {
        // Inicializa el texto y la imagen con el primer nivel.
        ActualizarNivel();
    }

    // Método para cambiar al siguiente nivel.
    public void SiguienteNivel()
    {
        nivelIndex = (nivelIndex + 1) % niveles.Length; // Cicla entre los niveles.
        ActualizarNivel();
    }

    // Método para seleccionar el nivel y cambiar a la escena de selección de carros.
    public void SeleccionarNivel()
    {
        // Guarda el nivel seleccionado en PlayerPrefs.
        PlayerPrefs.SetString("SelectedLevel", niveles[nivelIndex]);
        PlayerPrefs.Save();

        // Carga la escena de selección de carros.
        SceneManager.LoadScene(nombreEscenaSeleccionCarro);
    }

    // Actualiza el texto y la imagen del nivel seleccionado.
    private void ActualizarNivel()
    {
        textoNivel.text = niveles[nivelIndex];
        if (imagenesNiveles.Length > nivelIndex)
        {
            imagenNivel.sprite = imagenesNiveles[nivelIndex];
        }
    }
}
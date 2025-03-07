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

    // Nombre de la escena de selección de carros.
    public string nombreEscenaSeleccionCarro = "SeleccionCarro";

    private void Start()
    {
        // Inicializa el texto con el primer nivel.
        ActualizarTextoNivel();
    }

    // Método para cambiar al siguiente nivel.
    public void SiguienteNivel()
    {
        nivelIndex = (nivelIndex + 1) % niveles.Length; // Cicla entre los niveles.
        ActualizarTextoNivel();
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

    // Actualiza el texto del nivel seleccionado.
    private void ActualizarTextoNivel()
    {
        textoNivel.text = niveles[nivelIndex];
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permite crear los carros directamente en la ventana de proyecto.
[CreateAssetMenu (fileName = "New Car", menuName = "Car/New Car")]
public class Cars : ScriptableObject
{
    // Variables que se necesiran en el carro.
    [SerializeField] private AudioClip honk;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject miniToy;
    [SerializeField] private Sprite carImage;
    [SerializeField] private string carName;
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteeringAngle;

    // Getters y Setters para poder ver o modificar los valores de las variables.
    public AudioClip Honk { get => honk; set => honk = value; }
    public GameObject Car { get => car; set => car = value; }
    public GameObject MiniToy { get => miniToy; set => miniToy = value; }
    public Sprite CarImage { get => carImage; set => carImage = value; }
    public string CarName { get => carName; set => carName = value; }
    public float MotorForce { get => motorForce; set => motorForce = value; }
    public float BrakeForce { get => brakeForce; set => brakeForce = value; }
    public float MaxSteeringAngle { get => maxSteeringAngle; set => maxSteeringAngle = value; }
}

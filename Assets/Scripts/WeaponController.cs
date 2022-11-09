using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bolaPrefab;
    public Transform puntoSalida;
    public int municionActual;
    public int municionMax;
    public bool municionInfinita;
    public float bolaVida;

    public float velocidadBola;

    public float frecuenciaDisparo;
    public float ultimoTimepoDisparo;
    private bool esJugador;

    private ObjectPool bolaPool;


    private void Awake()
    {
        // soy el jugador
        if (GetComponent<PlayerController>())
        {
            esJugador = true;
        }
        bolaPool = GetComponent<ObjectPool>();
    }
    public bool PuedeDisparar()
    {
        if (Time.time - ultimoTimepoDisparo > frecuenciaDisparo)
        {
            if (municionActual > 0 || municionInfinita)
            {
                return true;
            }
        }
        return false;
    }
    public void Disparar()
    {
        ultimoTimepoDisparo = Time.time;
        municionActual--;

        GameObject bola = bolaPool.GetObjeto();
        bola.transform.position = puntoSalida.position;
        bola.transform.rotation = puntoSalida.rotation;

        bola.GetComponent<Rigidbody>().velocity = puntoSalida.forward * velocidadBola;
    }
}

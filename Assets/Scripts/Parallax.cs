using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 direccion;
    [SerializeField] private float anchoImagen;
    private Vector3 posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
           //Resto: cuanto me queda de recorrido para alcanzar un nuevo ciclo.
        float espacio = velocidad * Time.time;
        float resto = espacio % anchoImagen;

        //Mi posisión se va refrescando desde la incial SUMANDO tsanto como resto me quede en la dirección deseada.
        transform.position = posicionInicial + resto * direccion;
    }
}

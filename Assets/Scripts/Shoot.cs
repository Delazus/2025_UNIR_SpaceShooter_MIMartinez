using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 direccion;

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

}

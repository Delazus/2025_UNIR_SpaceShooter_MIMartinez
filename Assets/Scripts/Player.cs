using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float ratioDisparo;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private GameObject spawnPoint;
    private float temporizador = 0.5f;
    private float vidas = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        DelimitarMovimiento();
        Disparar();
    }

    void Movimiento ()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputH, inputV).normalized * velocidad * Time.deltaTime);
    }
    void DelimitarMovimiento()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -8.1f, 8.1f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.41f, 4.41f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }
    void Disparar()
    {
        temporizador += 1 * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && temporizador > ratioDisparo)
        {
            Instantiate(shootPrefab, spawnPoint.transform.position, Quaternion.identity);
            temporizador = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("ShootEnemy") || elOtro.gameObject.CompareTag("Enemy"))
        {
            vidas -= 20;
            Destroy(elOtro.gameObject);
            if(vidas <= 0) 
            { 
                Destroy(this.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject shootEnemy;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private AudioSource shootAudioSource;

    [Header("Score")]
    [SerializeField] private int puntosPorMuerte = 1;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {      
        StartCoroutine(SpawnearShoots());
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidad * Time.deltaTime);
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
    
    IEnumerator SpawnearShoots()
    {
        while (true)
        {
            Instantiate(shootEnemy, spawnPoint.transform.position, Quaternion.identity);
            // Reproducir el sonido
            if (shootAudioSource != null)
            {
                shootAudioSource.Play();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("ShootPlayer"))
        {
            Destroy(elOtro.gameObject);
            Destroy(this.gameObject);
            if (gameManager != null)
            {
                gameManager.SumarPuntos(puntosPorMuerte);
            }
        }
    }
}

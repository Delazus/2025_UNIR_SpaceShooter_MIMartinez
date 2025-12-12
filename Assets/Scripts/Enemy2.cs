using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Vector3 linearVelocity = Vector3.left;
    private SpriteRenderer sr;

    [Header("Score")]
    [SerializeField] private int puntosPorMuerte = 1;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        transform.Translate(linearVelocity * speed * Time.deltaTime);
        if (transform.position.x < -8.1f)
        {
            linearVelocity = Vector3.right;
            sr.flipX = true;
        }
        if (transform.position.x > 10)
        {
            Destroy(gameObject);
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

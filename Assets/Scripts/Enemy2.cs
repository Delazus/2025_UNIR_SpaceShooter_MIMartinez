using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Vector3 linearVelocity = Vector3.left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(linearVelocity * speed * Time.deltaTime);
        if (transform.position.x < -8.1f)
        {
            linearVelocity = Vector3.right;
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
        }
    }
}

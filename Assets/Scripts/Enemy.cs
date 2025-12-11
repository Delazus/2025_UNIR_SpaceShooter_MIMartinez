using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject shootEnemy;
    [SerializeField] private GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {      
        StartCoroutine(SpawnearShoots());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidad * Time.deltaTime);
    }
    
    IEnumerator SpawnearShoots()
    {
        while (true)
        {
            Instantiate(shootEnemy, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
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

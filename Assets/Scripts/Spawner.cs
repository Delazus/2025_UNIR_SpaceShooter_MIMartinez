using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TextMeshProUGUI textNiveles;
    // Start is called before the first frame update
    void Start()
    {
        //Random.Range(0f, 4f);//2.86f, 2.87f, 4.0000000f
        //Random.Range(0, 4);//0, 1, 2, 3
        StartCoroutine(SpawnearEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnearEnemies()
    {
        for (int i = 0; i < 5; i++) //Niveles
        { 
            for (int j = 0; j < 3; j++) //Oladeas
            {
                textNiveles.text = "Nivel " + (i + 1) + "-" + "Oledada " + (j + 1);
                yield return new WaitForSeconds(1f);
                textNiveles.text = "";
                for (int k = 0; k < 10; k++) //0, 1, 2, 3, 4 // Enemigos
                {
                    Vector3 puntoAleatorio = new Vector3(transform.position.x, Random.Range(-4.41f, 4.41f), 0);
                    Instantiate(enemyPrefab, puntoAleatorio, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}

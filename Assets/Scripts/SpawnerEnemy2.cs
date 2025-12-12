using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy2 : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnLineTop;
    [SerializeField] Transform spawnLineBottom;
    void Start()
    {
        StartCoroutine(LineSpawning());

    }

    IEnumerator LineSpawning()
    {
        Vector3 posTop = spawnLineTop.position;
        Vector3 posBottom = spawnLineBottom.position;

        for (int i = 0; i < 5; i++) //Niveles
        {
            for (int j = 0; j < 2; j++) //Oladeas
            {
                for (int k = 0; k < 2; k++) //0, 1, 2, 3, 4 // Enemigos
                {
                    float t = Random.Range(0f, 1f);
                    Vector3 startPosition = Vector3.Lerp(posTop, posBottom, t);
                    Instantiate(enemyPrefab, startPosition, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(5f);
        }
    }

    void Update()
    {
        
    }
       
}

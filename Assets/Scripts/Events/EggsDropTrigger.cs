using UnityEngine;

public class EggsDropTrigger : MonoBehaviour
{
    [SerializeField] private GameObject eggPrefab, shellsPrefab;  
    [SerializeField] private Transform[] spawnPoints;

    public void SpawnObjects()
    {
        Instantiate(eggPrefab, spawnPoints[0].position, Quaternion.identity);
        Instantiate(shellsPrefab, spawnPoints[1].position, Quaternion.identity);
        Instantiate(shellsPrefab, spawnPoints[2].position, Quaternion.identity);
    }
}
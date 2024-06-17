using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnAnimals;
    private int spawnHeight = 15;
    private float spawnX;
    private float spawnZ;
    private int spawnDelay = 3;
    private Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimal", 0, spawnDelay);
    }
    public Vector3 GenerateSpawnPosition()
    {
        spawnZ = Random.Range(-8, 12);
        spawnX = Random.Range(-12, 7);
        Vector3 spawnPos = new Vector3(spawnX, spawnHeight, spawnZ);
        return spawnPos;
    }

    public GameObject GenerateSpawnAnimal()
    {
       GameObject spawnAnimal = spawnAnimals[Random.Range(0, spawnAnimals.Length)];       
        return spawnAnimal;
    }

    public void SpawnAnimal()
    {
        Instantiate(GenerateSpawnAnimal(), GenerateSpawnPosition(), spawnRotation);
    }
}

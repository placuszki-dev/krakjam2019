using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject gameObjectToSpawn;
    public float timeBetweenSpawns = 1f;
    public float buttonMoveSpeed = 1f;
    public int itemsNumberToSpawn = 10;

    private float timeLeftToNextSpawn;

    void Start()
    {
        timeLeftToNextSpawn = timeBetweenSpawns;
    }

    void Update()
    {
        timeLeftToNextSpawn -= Time.deltaTime;
        if(timeLeftToNextSpawn <= 0)
        {
            GameObject instance = Instantiate(gameObjectToSpawn, transform.parent);
            instance.transform.position = transform.position;
            instance.GetComponent<LadderMiniGameButton>().SetSpeed(buttonMoveSpeed);
            timeLeftToNextSpawn = timeBetweenSpawns;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawn : MonoBehaviour
{
  [SerializeField] float spawnMaxY; //Currently hardcoded. Should be calculated by 
  [SerializeField] float spawnMinY;

  [SerializeField] private float spawnFrequency;
  [SerializeField] private float increaseSpawnTime;
  [SerializeField] private float minSpawnFrequency;
  private float timeSinceLastIncrease;
  private float nextSpawnTime;
  private float timeSinceLastSpawn; 
  private int totalSpawnChance;

  [SerializeField] List<Bug> bugs;

  void Start()
  {
    DetermineTotalSpawnChance();
  }

  void Update()
  {
      timeSinceLastSpawn += Time.deltaTime;

      if(timeSinceLastSpawn > nextSpawnTime)
      {
        timeSinceLastSpawn = 0;
        SpawnBug();
        CalculateNextSpawn();
      }

      timeSinceLastIncrease += Time.deltaTime;
      if(timeSinceLastIncrease > increaseSpawnTime)
      {
        timeSinceLastIncrease = 0;
        IncreaseSpawnFrequency();
      }
  }

  /*This will determine the maximum spawn chance of all bugs. Currently only called in start but bugs may be added or removed during the game in the future.*/
  private void DetermineTotalSpawnChance()
  {
    totalSpawnChance = 0;

    foreach(Bug b in bugs)
    {
      totalSpawnChance += b.So.SpawnChance;
    }    
  }

  /*Calculate which bug should be spawned then create the prefab and reset the timer.*/
  private void SpawnBug()
  {
    /*Calculate a number between all of the spawn chances combined.*/
    int spawnValue = Random.Range(0, totalSpawnChance);

    /*Loop each bug until the number is lower than the spawn chance of the bug.*/
    for(int x = 0; x < bugs.Count; ++x)
    {
      if(spawnValue < bugs[x].So.SpawnChance && (GameController.Instance.BugCount < 50))
      {
        /*Create the bug object.*/
        Instantiate(bugs[x], new Vector2(transform.position.x, Random.Range(spawnMinY, spawnMaxY)), Quaternion.identity);
        GameController.Instance.IncreaseBugCount();
        break;
      }
      spawnValue -= bugs[x].So.SpawnChance;
    }
  }

  /*Determine the next point a bug will spawn. This is a randomly generated number slightly lower or higher than the spawn frequency.*/
  private void CalculateNextSpawn()
  {
    float minSpawn = (spawnFrequency / 0.75f);
    float maxSpawn = (spawnFrequency * 1.25f);

    nextSpawnTime = Random.Range((int)minSpawn, (int)maxSpawn);
  }

  /*This function will be called once the player reaches certain point values.*/
  public void IncreaseSpawnFrequency()
  {
    if(spawnFrequency > minSpawnFrequency)
    {
      spawnFrequency -= ((float)Random.Range(0, 20) / 100);
    }
  }
}

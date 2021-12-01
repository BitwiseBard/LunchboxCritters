using UnityEngine;

[CreateAssetMenu(fileName = "Bug", menuName = "ScriptableObjects/Bug", order = 1)]
public class BugSO : ScriptableObject
{
  public Sprite sprite;

  public int speed;
  public int strength;
  public int maxHealth;
  public int spawnChance;

  public int pointValue;

  public bool flying;
  
  public int Speed => speed;
  public int Strength => strength;
  public int SpawnChance => spawnChance;

}

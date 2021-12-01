using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemSO : ScriptableObject
{
  public ItemEffect effect; //Potentially should be list in future.
  public int damage;
  public float effectDuration;
  public float onMapDuration;
  public float useTime;

  // public float offsetX;
  // public float offsetY;

  public Texture2D cursor;

  public int cost;
}

public enum ItemEffect { Damage, Stun, Slow }
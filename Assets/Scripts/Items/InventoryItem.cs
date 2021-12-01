using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
  [SerializeField] Image inventoryImage;
  [SerializeField] Image backgroundImage;
  public Image BackgroundImage => backgroundImage;

  [SerializeField] Sprite mainSprite;
  [SerializeField] Sprite usingSprite;

  public Color currentColor;

  public Item item; 
  public bool usable;
  private float resetTimer;

  void Update()
  {
    if(!usable)
    {
      resetTimer -= Time.deltaTime;
      if(resetTimer <= 0)
      {
        usable = true;
        inventoryImage.sprite = mainSprite;
        backgroundImage.color = currentColor;
      }
    }
  }

  public void UseItem()
  {
    usable = false;
    inventoryImage.sprite = usingSprite;
    resetTimer = item.So.useTime;

    backgroundImage.color = new Color(0.0f, 0.0f, 0.0f, 0.8f);

    Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    GameObject itemClone = Instantiate(item.gameObject, spawnPosition, Quaternion.identity);

    itemClone.GetComponent<AudioSource>().Play();
  }
}

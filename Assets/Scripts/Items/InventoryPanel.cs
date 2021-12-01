using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
  private Inventory inventory;
  private List<Image> images;

  void Start()
  {
    inventory = FindObjectOfType<Inventory>();

    images = new List<Image>();

    Image[] childrenImages = GetComponentsInChildren<Image>();

    foreach(Image childImage in childrenImages)
    {
      images.Add(childImage);
    }
  }

  public void MouseEnterEvent()
  {
    foreach(Image img in images)
    {
      img.color = new Color(img.color.r, img.color.r, img.color.r, 0.1f);
    }
  } 

  public void MouseExitEvent()
  {
    foreach(Image img in images)
    {
      img.color = new Color(img.color.r, img.color.r, img.color.r, 1.0f);
    }
  } 
}

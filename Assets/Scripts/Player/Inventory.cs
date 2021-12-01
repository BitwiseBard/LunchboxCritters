using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
  [SerializeField] private List<InventoryItem> items;
  private int currentItem;

  private float useTime;
  private bool usingItem = false;

  private bool gameStopped = false;

  private Color selectedItemBackgroundColor = new Color(0.6f, 0.8f, 0.2f);
  private Color defaultItemBackgroundColor = new Color(0.75f, 0.75f, 0.75f);

  //This potentially should be moved to a player class. Since this class currently isn't needed it is here for now.
  void Update()
  { 
    if(!gameStopped)
    {
      /*Use item on mouse click*/
      if(items[currentItem].usable)
      {
        if(Input.GetMouseButtonDown(0))
        {
          UseItem();
        }
      }

      /*Switch items*/
      if(Input.GetKeyDown(KeyCode.Alpha1))
      {
        SelectItem(0);
      }
      else if(Input.GetKeyDown(KeyCode.Alpha2))
      {
        SelectItem(1);
      }
      else if(Input.GetKeyDown(KeyCode.Alpha3))
      {
        SelectItem(2);
      }
      else if(Input.GetKeyDown(KeyCode.Alpha4))
      {
        SelectItem(3);
      }
      else if(Input.GetKeyDown(KeyCode.Alpha5))
      {
        SelectItem(4);
      } 
    }
    gameStopped = GameController.Instance.GameStopped;
  }

  public void SelectItem(int index)
  {
    currentItem = index;

    for(int i = 0; i < items.Count; ++i)
    {
      items[i].currentColor = ((i == index) ? selectedItemBackgroundColor : defaultItemBackgroundColor); //This will be used when the item switches back to usable.
      if(items[i].usable)
      {
        items[i].BackgroundImage.color = items[i].currentColor;
      }
    }
  }

  private void UseItem()
  {
    items[currentItem].UseItem();
  }
}
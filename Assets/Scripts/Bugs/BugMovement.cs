using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMovement : MonoBehaviour
{
  [SerializeField] private Bug bug;

  private Animator anim;

  private bool reachedBase = false;
  private bool previousReachedBase = false;

  private bool isStunned;
  private float stunTime;

  private bool isSlow;
  private float slowTime;

  void Start() 
  {
    bug = GetComponent<Bug>();
    bug.CurrentSpeed = bug.So.Speed;
    anim = GetComponent<Animator>();
  }

  void Update()
  {
    if(bug.CurrentHealth > 0)
    {
      /*If stunned do not move.*/
      if(isStunned)
      {
        stunTime -= Time.deltaTime;
        if(stunTime <= 0)
        {
          isStunned = false;
        }
        return; 
      }

      if(isSlow)
      {
        slowTime -= Time.deltaTime;
        if(slowTime <= 0)
        {
          isSlow = false;
          bug.CurrentSpeed = bug.So.Speed;
        }      
      }

      /*Switch animation if reached base has changed.*/
      if(previousReachedBase != reachedBase)
      {
        anim.SetBool("DraggingBase", reachedBase);
        previousReachedBase = reachedBase;
      }
    
      if(!reachedBase)
      {
        transform.position -= new Vector3(bug.CurrentSpeed, 0, 0) * Time.deltaTime; //This should potentially be changed to simply move towards the player base object.
      } 
      else 
      {
        transform.position += GameController.Instance.GetPlayerBaseLocation()  * Time.deltaTime; //This should potentially be changed to move towards the spawn point.
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if(!reachedBase && other.tag == "Base")
    {
      reachedBase = true;
      GameController.Instance.AddToBugsPulling(bug);
    }
  }  

  public void SetStun(float duration)
  {
    isStunned = true;
    stunTime = duration;
  }

  public void SetSlow(float duration)
  {
    isSlow = true;
    slowTime = duration; 
    bug.CurrentSpeed /= 5;
  }
}

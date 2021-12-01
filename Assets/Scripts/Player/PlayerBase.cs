using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
  [SerializeField] private int defence;
  private Vector3 pulledDistance;

  [SerializeField] private List<Bug> bugsPulling;

  public Vector3 PulledDistance => pulledDistance;
  public List<Bug> BugsPulling => bugsPulling;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if(other.tag == "BugSpawn")
    {
      GameController.Instance.GameOver();
    }
  }  

  void Start()
  {
    bugsPulling = new List<Bug>();
  }

  void FixedUpdate()
  {
    int totalBugStrength = 0;
    float maxBugSpeed = -1;

    maxBugSpeed = -1;
    pulledDistance = Vector3.zero;

    /*Sum all the strength of the bugs. Also get the speed of the slowest bug.*/
    foreach(Bug b in bugsPulling)
    {
      totalBugStrength += b.So.Strength;

      if(maxBugSpeed < 0 || maxBugSpeed < b.CurrentSpeed)
      {
        maxBugSpeed = b.CurrentSpeed;
      }
    }

    /*If total strength is greater than defence create a new vector 3 between the defence and the strength above.*/
    if(totalBugStrength > defence)
    {
      float pullSpeed = (totalBugStrength - defence + 1);

      /*Do not let the base move faster than the slowest bug.*/
      if(pullSpeed > maxBugSpeed)
      {
        pullSpeed = maxBugSpeed;
      }

      pulledDistance = new Vector3(Random.Range(0, pullSpeed), 0, 0);
      transform.position += pulledDistance  * Time.deltaTime;
    }

    /*Increase music */
    //TODO: Should be calculated not hard coded.
    if(transform.position.x > 30)
    {
      GameController.Instance.SetMusicSpeed(2f);
    }
    else if(transform.position.x > 20)
    {
      GameController.Instance.SetMusicSpeed(1.8f);
    }
    else if(transform.position.x > 10)
    {
      GameController.Instance.SetMusicSpeed(1.6f);
    }
    else if(transform.position.x > 0)
    {
      GameController.Instance.SetMusicSpeed(1.2f);
    }
  }
}

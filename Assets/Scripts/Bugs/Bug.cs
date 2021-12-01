using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
  [SerializeField] private BugSO so;

  private Animator anim;

  [SerializeField] float deathAnimationTime;

  private int currentHealth;
  public int CurrentHealth => currentHealth;

  public BugSO So => so;

  public float CurrentSpeed { get; set; }

  private PlayerBase playerBase;

  void Start()
  {
    currentHealth = so.maxHealth;
    anim = GetComponent<Animator>();
    playerBase = FindObjectOfType<PlayerBase>();
  }

  void Update()
  {
    if(playerBase.transform.position.x > transform.position.x)
    {
      StartCoroutine(Death(false));
    }
  }

  public void TakeDamage(int damage)
  {
    //Currently kill the bugs instantly until more items are made.
    //All health is set to 1.
    currentHealth -= damage;
    if(currentHealth <= 0)
    {
      GameController.Instance.RemoveBugsPulling(this);
      StartCoroutine(Death(true));
    }
  }

  private IEnumerator Death(bool score)
  {
    anim.SetTrigger("IsHurt");

    //TODO: Should check for animation complete instead of setting time.
    yield return new WaitForSeconds(deathAnimationTime);

    if(score)
    {
      PointsSystem.Instance.IncreasePoints(so.pointValue);
    }
    GameController.Instance.DecreaseBugCount();

    Destroy(gameObject);
  }
}

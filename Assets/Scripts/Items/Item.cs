using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: This should be redone. Currently we have item selected by user, the instantiated item and the background.
//      These should all be split into separate classes under one main class.
public class Item : MonoBehaviour
{
  [SerializeField] ItemSO so;

  public ItemSO So => so;
  private float currentMapDuration;

  public bool OnMap { get; set; }

  public int InventoryIndex { get; set; }

  private Animator anim;

  [SerializeField] private AudioSource sound;

  private bool playingSound = false;
  private float playSoundMaxTime = 1.0f;
  private float playSoundTimer;

  void Start()
  {
    currentMapDuration = so.onMapDuration;

    anim = GetComponent<Animator>();
  }

  void Update()
  {
    currentMapDuration -= Time.deltaTime;
    if(currentMapDuration <= 0)
    {
      this.gameObject.SetActive(false);
      Destroy(this.gameObject, 1.5f);
    }

    if(playingSound)
    {
      playSoundTimer -= Time.deltaTime;

      if(playSoundTimer <= 0)
      {
        playingSound = false;
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DoEffect(other);
  }  

  void DoEffect(Collider2D bugCollider)
  {
    if(bugCollider.tag == "Bug")
    {
      PlaySound();

      if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Action"))
      {
        anim.Play("Action");
      }

      /*Perform action depending on item.*/
      switch(so.effect)
      {
        case ItemEffect.Damage:
          bugCollider.gameObject.GetComponent<Bug>().TakeDamage(so.damage);
          break;      
        case ItemEffect.Stun:
          bugCollider.gameObject.GetComponent<BugMovement>().SetStun(so.effectDuration);
          break;      
        case ItemEffect.Slow:
          bugCollider.gameObject.GetComponent<BugMovement>().SetSlow(so.effectDuration);
          break;
        default:
          break;
      }
    }
  }

  public void PlaySound()
  {
    if(sound != null)
    {
      if(!playingSound)
      {
        playingSound = true;
        playSoundTimer = playSoundMaxTime;

        sound.Play();
      }
    }
  }
}

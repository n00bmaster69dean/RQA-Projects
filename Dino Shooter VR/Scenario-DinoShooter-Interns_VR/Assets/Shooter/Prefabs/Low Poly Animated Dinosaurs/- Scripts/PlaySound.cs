using UnityEngine;

namespace LowPolyAnimalPack
{
  public class PlaySound : MonoBehaviour
  {
    [SerializeField]
    private AudioClip animalSound = null;
    [SerializeField]
    private AudioClip walking = null;
    [SerializeField]
    private AudioClip eating = null;
    [SerializeField]
    private AudioClip running = null;
    [SerializeField]
    private AudioClip attacking = null;
    [SerializeField]
    private AudioClip death = null;
    [SerializeField]
    private AudioClip sleeping = null;

    void AnimalSound()
    {
      if (animalSound)
      {
        AudioManager.PlaySound(animalSound, transform.position);
      }
    }

    void Walking()
    {
      if (walking)
      {
        AudioManager.PlaySound(walking, transform.position);
      }
    }

    void Eating()
    {
      if (eating)
      {
        AudioManager.PlaySound(eating, transform.position);
      }
    }

    void Running()
    {
      if (running)
      {
        AudioManager.PlaySound(running, transform.position);
      }
    }

    void Attacking()
    {
      if (attacking)
      {
        AudioManager.PlaySound(attacking, transform.position);
      }
    }

    void Death()
    {
      if (death)
      {
        AudioManager.PlaySound(death, transform.position);
      }
    }

    void Sleeping()
    {
      if (sleeping)
      {
        AudioManager.PlaySound(sleeping, transform.position);
      }
    }
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    public Sound TakeDamage;
    public Sound Jump;
    public AudioManager audioManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        PlaySound_TakeDamage();
            
    }
    public void PlaySound_TakeDamage()

    {
        audioManager.Play(TakeDamage.SoundName);

    }
    public void PlaySound_Jump()
    {
        audioManager.Play(Jump.SoundName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip main;
    public AudioClip level_1;
    public AudioClip level_2;
    public AudioClip level_3;
    public bool isPlaying;
    private bool isDisabled;

    void updateClip(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void disable()
    {
        audioSource.Stop();
        isPlaying = false;
        isDisabled = true;
    }

    public void enable()
    {
        audioSource.Play();
        isDisabled = false;
        isPlaying = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = main;
        audioSource.Play();
        isPlaying = true;
        isDisabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Lockout when disabled
        if (isDisabled)
        {
            return;
        }

        //Pause Music when game is paused
        if (isPlaying)
        {
            if (PauseMenu.isPaused)
            {
                audioSource.Pause();
                isPlaying = false;
            }
        }
        else
        {
            if (!PauseMenu.isPaused)
            {
                audioSource.UnPause();
                isPlaying = true;
            }
        }

        //Song Select
        if (TitleScreen.titleScreenActive || LevelSelect.levelSelectActive)
        {
            updateClip(main);
        }
        else
        {
            switch (LevelSelect.levelSelected)
            {
                case 1:
                    updateClip(level_1);
                    break;
                case 2:
                    updateClip(level_2);
                    break;
                case 3:
                    updateClip(level_3);
                    break;
                default:
                    updateClip(main);
                    break;
            }
        }
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SoundType { Jump , Shuriken, PickUp, Damage, LevelFinish}

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle bgToggle;

    [Header("Audio clips")]
    [SerializeField] private AudioClip playerJump;
    [SerializeField] private AudioClip shurikenSound;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip levelFinishSound;

    private void Start()
    {
        if(PlayerPrefs.HasKey("BgMusic") && PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            bgToggle.isOn = PlayerPrefs.GetInt("BgMusic") == 1;

            source.volume = PlayerPrefs.GetFloat("Volume");

            if (backgroundMusicSource != null)
                backgroundMusicSource.volume = PlayerPrefs.GetFloat("Volume");
        }

        if (backgroundMusicSource != null)
        {
            if (bgToggle.isOn)
                backgroundMusicSource.Play();
            else
                backgroundMusicSource.Stop();
        }
    }

    public void ChangeVolume(Slider slider)
    {
        source.volume = slider.value;
        if (backgroundMusicSource != null)
            backgroundMusicSource.volume = slider.value;
        PlayerPrefs.SetFloat("Volume", slider.value);
    }

    public void BackgroundMusic(Toggle toggle)
    {
        PlayerPrefs.SetInt("BgMusic", toggle.isOn?1:0);
        
        if(backgroundMusicSource != null)
        {
            if (bgToggle.isOn)
                backgroundMusicSource.Play();
            else
                backgroundMusicSource.Stop();
        }
    }

    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.Jump:
                source.PlayOneShot(playerJump);
                break;
            case SoundType.PickUp:
                source.PlayOneShot(pickupSound);
                break;
            case SoundType.Shuriken:
                source.PlayOneShot(shurikenSound);
                break;
            case SoundType.Damage:
                source.PlayOneShot(damageSound);
                break;
            case SoundType.LevelFinish:
                source.PlayOneShot(levelFinishSound);
                break;
        }
    }

}

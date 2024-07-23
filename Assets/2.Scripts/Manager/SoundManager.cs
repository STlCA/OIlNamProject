using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Manager
{
    [Header("AudioSource")]
    public AudioSource BGMSource;
    public AudioSource EffectSource;

    [Header("VolumeSlider")]
    public Slider BGMSlider;
    public Slider EffectSlider;

    [Header("BGMClipList")]
    public List<AudioClip> bgmList;

    [Header("EffectAudioClip")]
    public List<AudioClip> effectAudioList;

/*    private float bgmVolume = 1;
    private float effectVolume = 1;

    public void BGMSoundSlider(Image image)
    {
        if (BGMSource.volume == 0)
            image.color = Color.gray;
        else
            image.color = Color.white;
    }
    public void EffectSoundSlider(Image image)
    {
        if (EffectSource.volume == 0)
            image.color = Color.gray;
        else
            image.color = Color.white;
    }

    public void BGMSoundOnOff(Image image)
    {
        if (BGMSource.volume == 0)
        {
            image.color = Color.white;
            BGMSource.volume = bgmVolume;
            BGMSlider.value = bgmVolume;
        }
        else
        {
            image.color = Color.gray;
            bgmVolume = BGMSource.volume;
            BGMSource.volume = 0;
            BGMSlider.value = 0;
        }
    }

    public void EffectSoundOnOff(Image image)
    {
        if (EffectSource.volume == 0)
        {
            image.color = Color.white;
            EffectSource.volume = effectVolume;
            EffectSlider.value = effectVolume;
        }
        else
        {
            image.color = Color.gray;
            effectVolume = EffectSource.volume;
            EffectSource.volume = 0;
            EffectSlider.value = 0;
        }
    }*/

    public void SourceSet(AudioSource bgm, AudioSource effect)
    {
        BGMSource = bgm;
        EffectSource = effect;
    }

    public void EffectAudioClipPlay(int index)
    {
        EffectSource.PlayOneShot(effectAudioList[index], 1);
    }
}

using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Manager
{
    [Header("AudioSource")]
    public AudioSource BGMSource;
    public AudioSource EffectSource;
    public AudioSource GameSource;

    [Header("VolumeSlider")]
    public Slider BGMSlider;
    public Slider EffectSlider;

    [Header("BGMClipList")]
    public List<AudioClip> bgmList;

    [Header("EffectAudioClip")]
    public List<AudioClip> effectAudioList;

    [Header("GameAudioClip")]
    public List<AudioClip> gameAudioList;

    [Header("Volume")]
    public float bgm;
    public float effect;
    public float game;


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

    public override void Init(GameManager gm)
    {
        base.Init(gm);

        bgm = -1f;
        effect = -1f;
        game = -1f;
    }

    public void VolumeSave(float bgm, float effect, float game)
    {
        this.bgm = bgm;
        this.effect = effect;
        this.game = game;
    }

    public void SourceSet(AudioSource bgm, AudioSource effect, AudioSource gameSource, Slider bgmSd =null, Slider effectSd = null)
    {
        BGMSource = bgm;
        EffectSource = effect;
        GameSource = gameSource;

        BGMSlider = bgmSd;
        EffectSlider = effectSd;

        VoulumeSetting();
    }

    private void VoulumeSetting()
    {
        if(bgm >= 0)
        {
            BGMSource.volume = bgm;
            EffectSource.volume = effect;
            GameSource.volume = game;
        }

        if (BGMSlider != null)
        {
            BGMSlider.value = BGMSource.volume;
            EffectSlider.value = EffectSource.volume;
        }
    }

    public void BGMCheck(int newIndex)
    {
        if (BGMSource.clip == bgmList[newIndex])
            return;
        else
            BGMChange(newIndex);

    }

    public void BGMChange(int index)
    {
        BGMSource.Stop();
        BGMSource.clip = bgmList[index];
        BGMSource.Play();
    }
    public void EffectAudioClipPlay(int index)
    {
        EffectSource.PlayOneShot(effectAudioList[index], 1);
    }

    public void EffectAudioClipPlay(int index,float volum = 1)
    {
        EffectSource.PlayOneShot(effectAudioList[index], volum);
    }
    public void EffectAudioClipPlay(EffectList index)
    {
        EffectSource.PlayOneShot(effectAudioList[(int)index], 1);
    }

    public void GameAudioClipPlay(int index)
    {
/*        if (GameSource.isPlaying)
            return;
        else
        {
            GameSource.Stop();
            GameSource.clip = gameAudioList[index];
            GameSource.Play();
        }*/

        GameSource.PlayOneShot(gameAudioList[index],0.1f);
    }

}

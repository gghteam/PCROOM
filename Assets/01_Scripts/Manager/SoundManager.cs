using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private AudioClip[] BackgroundSoundclip;

        [SerializeField]
    private AudioClip[] EfectClip;

    private AudioSource bgm;
    private AudioSource efectsound;
    private void Awake()
    {
        bgm = GetComponent<AudioSource>();
        efectsound = transform.GetChild(0).GetComponent<AudioSource>();
    }
    public void SetEffectSoundClip(int num)
    {
        efectsound.Stop();
        efectsound.clip = EfectClip[num];
        efectsound.Play();
    }

    public void SetBackgroundSoundclip(int num)
    {
        bgm.Stop();
        bgm.clip = BackgroundSoundclip[num];
        bgm.Play();
    }
    public void bgmSetVolume(bool a)
    {
        if(a)
        {
            bgm.volume = 1;
            GameManager.Instance.uiManager.bksimage.sprite = GameManager.Instance.uiManager.soundsprites[0];
        }
        else
        {
            bgm.volume = 0;
            GameManager.Instance.uiManager.bksimage.sprite = GameManager.Instance.uiManager.soundsprites[1];
        }
    }
    public void efectSetVolume(bool a)
    {
        if(a)
        {
            GameManager.Instance.uiManager.efimage.sprite = GameManager.Instance.uiManager.soundsprites[2];
        }
        else
        {
            efectsound.volume = 0;
            GameManager.Instance.uiManager.efimage.sprite = GameManager.Instance.uiManager.soundsprites[3];
        }
    }
}

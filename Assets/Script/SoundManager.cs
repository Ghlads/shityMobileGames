using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static List<AudioSource> musiques = new List<AudioSource>();
    public static List<AudioSource> sfx = new List<AudioSource>();

    public static float volumeSFX = 1f;
    public static float volumeMusic = 1f;

    public GameObject muteSFXSprite, muteMusicSprite;

    public Slider Smusic, Ssfx;

    private bool asStartTo = false;

    private void SetVolume(List<AudioSource> sounds, float newVolume)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].volume = newVolume;
        }
    }

    public void SaveSound()
    {
        ScoreHandler.instance.dataToSave[7].var = volumeSFX;
        ScoreHandler.instance.dataToSave[6].var = volumeMusic;
    }

    public void MuteSFX()
    {
        ScoreHandler.instance.dataToSave[9].var = (ScoreHandler.instance.dataToSave[9].var == 1) ? 0 : 1;
        muteSFXSprite.SetActive(!muteSFXSprite.activeSelf);
        MuteAudio(sfx);
        ScoreHandler.instance.SaveAllData();
    }
    public void MuteMusic()
    {
        ScoreHandler.instance.dataToSave[8].var = (ScoreHandler.instance.dataToSave[8].var == 1) ? 0 : 1;
        muteMusicSprite.SetActive(!muteMusicSprite.activeSelf);
        MuteAudio(musiques);
        ScoreHandler.instance.SaveAllData();
    }

    private void MuteAudio(List<AudioSource> sounds)
    {
        for(int i = 0; i < sounds.Count; i++)
        {
            sounds[i].mute = !sounds[i].mute;
        }
    }

    private void LateStart()
    {
        if (ScoreHandler.instance.dataToSave[9].var == 1)
        {
            muteSFXSprite.SetActive(!muteSFXSprite.activeSelf);
            MuteAudio(sfx);
        }
        if(ScoreHandler.instance.dataToSave[8].var == 1)
        {
            muteMusicSprite.SetActive(!muteMusicSprite.activeSelf);
            MuteAudio(musiques);
        }
        Smusic.value = ScoreHandler.instance.dataToSave[6].var;
        Ssfx.value = ScoreHandler.instance.dataToSave[7].var;
        ScoreHandler.instance.asStart = false;
        asStartTo = true;
    }

    private void Update()
    {
        if (ScoreHandler.instance.asStart)
        {
            LateStart();
        }
        if (asStartTo)
        {
            volumeMusic = Smusic.value;
            volumeSFX = Ssfx.value;
            SetVolume(musiques, volumeMusic);
            SetVolume(sfx, volumeSFX);
        }
    }
}

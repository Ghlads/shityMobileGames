using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAudioSource : MonoBehaviour
{
    [SerializeField]
    bool isSFX;

    void Start()
    {
        if (this.gameObject.GetComponent<AudioSource>() != null) 
        {
            if (isSFX)
            {
                SoundManager.sfx.Add(this.gameObject.GetComponent<AudioSource>());
            }
            else
            {
                SoundManager.musiques.Add(this.gameObject.GetComponent<AudioSource>());
            }
        }
    }

    private void OnDisable()
    {
        if (this.gameObject.GetComponent<AudioSource>() != null)
        {
            if (isSFX)
            {
                SoundManager.sfx.Remove(this.gameObject.GetComponent<AudioSource>());
            }
            else
            {
                SoundManager.musiques.Remove(this.gameObject.GetComponent<AudioSource>());
            }
        }
    }


}

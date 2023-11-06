using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    private void Awake()
    {
        int numSoundsManager = FindObjectsOfType<SoundsManager>().Length;
        if (numSoundsManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log(volume);
    }

}

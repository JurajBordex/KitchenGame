using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    //Audio Sources
    [SerializeField] private AudioSource testAudioSource;

    public void PlayTestAudio()
    {
        testAudioSource.Play();
    }





}//END OF CLASS

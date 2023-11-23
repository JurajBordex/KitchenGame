using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicList : MonoBehaviour
{
    [SerializeField] AudioClip[] musicList;
    [SerializeField] int CurrentMusicIndex = 0;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource =  GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayMusic()
    {
        audioSource.clip = musicList[CurrentMusicIndex];
        audioSource.Play();
    }

    public void LoadNextMusic()
    {
        CurrentMusicIndex++;
        PlayMusic();
    }
    // Update is called once per frame
    void Update()
    {

    }
}

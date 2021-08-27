using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackController : MonoBehaviour
{
    public SoundtrackList SoundtracksListObj;
    public AudioSource SoundtrackAudioSource;
    public static SoundtrackController Instance;
    public static string LastPlayedSoundtrack;

    // Private variables.
    private bool stopped;

    private void Awake()
    {
        Instance = this;
        if (Instance != this && Instance != null) Destroy(Instance);
    }

    private void Start()
    {
        if (LastPlayedSoundtrack != "")
        {
            ChangeSoundtrack(LastPlayedSoundtrack);
        }
    }

    public void ChangeSoundtrack(string SoundtrackName)
    {
        StopSoundtrack();
        stopped = false;

        for (int i = 0; i < SoundtracksListObj.Soundtracks.Length; i++)
        {
            if(SoundtracksListObj.Soundtracks[i].Name == SoundtrackName)
            {
                SoundtrackAudioSource.PlayOneShot(SoundtracksListObj.Soundtracks[i].SoundtrackClip);
                LastPlayedSoundtrack = SoundtracksListObj.Soundtracks[i].Name;
            }
        }
    }

    public void StopSoundtrack()
    {
        SoundtrackAudioSource.Stop();
        stopped = true;
    }

    private void Update()
    {
        if(!SoundtrackAudioSource.isPlaying && !stopped)
        {
            ChangeSoundtrack(SoundtracksListObj.Soundtracks[Random.Range(0, SoundtracksListObj.Soundtracks.Length)].Name);
        }
    }
}

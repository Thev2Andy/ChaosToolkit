using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoundtrackList", menuName = "ScriptableObjects/Soundtrack List")]
public class SoundtrackList : ScriptableObject
{
    public SoundtrackDefinition[] Soundtracks;
}

[System.Serializable]
public class SoundtrackDefinition
{
    public string Name;
    public AudioClip SoundtrackClip;
}

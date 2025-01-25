using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class StandardAudioEntry
{
    public string clipName;
    public AudioClip clipFile;
    [Range(0f,1f)]
    public float volume = 1f;
}

[System.Serializable]
public class AudioEntryGroup
{
    public string groupName;
    public StandardAudioEntry[] groupEntries; 
}

[CreateAssetMenu]
public class AudioData : ScriptableObject
{
    private AudioClip clipToUse;
    private float currentClipVolume;
    public AudioMixerGroup masterMixer;

    // change the names of these arrays for proper category sorting
    public StandardAudioEntry[] myPlayerAudio;
    public StandardAudioEntry[] myGameAudio;
    public AudioEntryGroup[] myUIAudio;

    // standard is just a normal array
    public AudioSource AddNewAudioSourceFromStandard(string audioTag, GameObject objectToUse, string clipName)
    {
        currentClipVolume = 1f;
        clipToUse = null;
        switch (audioTag)
        {
            case "Player":
                return CheckStandardAudioEntry(myPlayerAudio, objectToUse , clipName);
            case "GameManager":
                return CheckStandardAudioEntry(myGameAudio, objectToUse ,clipName);
            default:
                return null;
        }
    }


    // group is just a sub array from a category (enemies is the group, crawlers are the sub array)
    public AudioSource AddNewAudioSourceFromGroup(string audioTag, string groupTag , GameObject objectToUse, string clipName)
    {
        clipToUse = null;
        switch (audioTag)
        {
            case "UIManager":
                return CheckGroupAudioEntry(myUIAudio, groupTag, objectToUse, clipName);
            default:
                return null;
        }
    }
    public AudioSource CheckStandardAudioEntry(StandardAudioEntry[] myEntry, GameObject objectToUse, string clipName)
    {
        foreach (var entry in myEntry)
        {
            if (clipName == entry.clipName)
            {
                currentClipVolume = entry.volume;
                clipToUse = entry.clipFile;
                return AddSourceComponent(clipToUse, objectToUse, currentClipVolume);
            }
        }
        return null;
    }
    public AudioSource CheckGroupAudioEntry(AudioEntryGroup[] myEntry, string entryGroupName , GameObject objectToUse, string clipName)
    {
        foreach (var group in myEntry)
        {
            if (group.groupName == entryGroupName)
            {
                foreach (var entry in group.groupEntries)
                {
                    if (entry.clipName == clipName)
                    {
                        clipToUse = entry.clipFile;
                        currentClipVolume = entry.volume;
                        return AddSourceComponent(clipToUse, objectToUse, currentClipVolume);
                    }
                }
            }
        }
        return null;
    }
    public AudioSource AddSourceComponent(AudioClip clipToAdd, GameObject objectToUse, float clipVolume)
    {
        var sourceControl = objectToUse.AddComponent<AudioSource>();
        sourceControl.clip = clipToAdd;
        sourceControl.outputAudioMixerGroup = masterMixer;
        sourceControl.maxDistance = 100000f;
        sourceControl.volume = clipVolume;
        //Other Variables if need
        return sourceControl;
    }
    public void AddAudioStandardSources(List<string> audioClipList, Dictionary<string, AudioSource> audioDict, string objectTag, GameObject objectToAdd)
    {
        foreach (var clip in audioClipList)
        {
            AudioSource clipSource = AddNewAudioSourceFromStandard(objectTag, objectToAdd, clip);
            audioDict.Add(clip, clipSource);
        }
    }
    public void AddAudioGroupSources(List<string> audioClipList, Dictionary<string, AudioSource> audioDict, string objectTag, string groupName, GameObject objectToAdd)
    {
        foreach (var clip in audioClipList)
        {
            AudioSource clipSource = AddNewAudioSourceFromGroup(objectTag, groupName, objectToAdd, clip);
            audioDict.Add(clip, clipSource);
        }
    }
}

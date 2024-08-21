using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Slider soundSlider;
    public Slider musicSlider;
    public Slider voiceSlider;

    public Text soundValueText;
    public Text musicValueText;
    public Text voiceValueText;

    private List<AudioTypeAssigner> audioSources = new List<AudioTypeAssigner>();

    private float soundVolume = 1f;
    private float musicVolume = 1f;
    private float voiceVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        voiceSlider.onValueChanged.AddListener(SetVoiceVolume);

        SetSoundVolume(soundSlider.value);
        SetMusicVolume(musicSlider.value);
        SetVoiceVolume(voiceSlider.value);
    }

    public void RegisterAudioSource(AudioTypeAssigner audioSourceAssigner)
    {
        audioSources.Add(audioSourceAssigner);
        UpdateVolume(audioSourceAssigner);
    }

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
        soundValueText.text = $"Sound Volume: {(int)(volume * 100)}%";
        UpdateVolumesByType(AudioTypeAssigner.AudioType.Sound, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicValueText.text = $"Music Volume: {(int)(volume * 100)}%";
        UpdateVolumesByType(AudioTypeAssigner.AudioType.Music, volume);
    }

    public void SetVoiceVolume(float volume)
    {
        voiceVolume = volume;
        voiceValueText.text = $"Voice Volume: {(int)(volume * 100)}%";
        UpdateVolumesByType(AudioTypeAssigner.AudioType.Voice, volume);
    }

    private void UpdateVolumesByType(AudioTypeAssigner.AudioType type, float volume)
    {
        foreach (var audioSourceAssigner in audioSources)
        {
            if (audioSourceAssigner.GetAudioType() == type)
            {
                audioSourceAssigner.GetAudioSource().volume = volume;
            }
        }
    }

    private void UpdateVolume(AudioTypeAssigner audioSourceAssigner)
    {
        switch (audioSourceAssigner.GetAudioType())
        {
            case AudioTypeAssigner.AudioType.Sound:
                audioSourceAssigner.GetAudioSource().volume = soundVolume;
                break;
            case AudioTypeAssigner.AudioType.Music:
                audioSourceAssigner.GetAudioSource().volume = musicVolume;
                break;
            case AudioTypeAssigner.AudioType.Voice:
                audioSourceAssigner.GetAudioSource().volume = voiceVolume;
                break;
        }
    }
}

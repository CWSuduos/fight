using UnityEngine;

public class AudioTypeAssigner : MonoBehaviour
{
    public enum AudioType { Sound, Music, Voice }
    public AudioType audioType;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Подключение аудиофайла к AudioManager
        AudioManager.Instance.RegisterAudioSource(this);
    }

    // Получаем текущий тип аудио
    public AudioType GetAudioType()
    {
        return audioType;
    }

    // Получаем AudioSource для управления громкостью
    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
}

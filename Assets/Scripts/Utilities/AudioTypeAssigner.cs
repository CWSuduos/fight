using UnityEngine;

public class AudioTypeAssigner : MonoBehaviour
{
    public enum AudioType { Sound, Music, Voice }
    public AudioType audioType;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // ����������� ���������� � AudioManager
        AudioManager.Instance.RegisterAudioSource(this);
    }

    // �������� ������� ��� �����
    public AudioType GetAudioType()
    {
        return audioType;
    }

    // �������� AudioSource ��� ���������� ����������
    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
}

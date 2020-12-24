using Audio;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSourcePool _audioSourcePool;
    [SerializeField] private AudioDatabase _audioDatabase;
    [SerializeField] private AudioClipDatabase _audioClipDatabase;
    private IAudioManager _audioManager;

    private void Awake()
    {
        _audioSourcePool.Init(_audioClipDatabase.AudioClipDic);
        _audioManager = new AudioManager(_audioDatabase, _audioSourcePool);
    }

    public void Play(string audioSound)
    {
        var audioPlayer = _audioManager.Play(audioSound, new PlaySetting{AudioPriorityType = AudioPriorityType.Important, ObjectToAttach = transform});
    }
}
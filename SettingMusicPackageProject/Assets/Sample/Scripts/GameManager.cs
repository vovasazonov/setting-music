using System.Collections.Generic;
using Audio;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSourcePool _audioSourcePool;
    [SerializeField] private AudioDatabase _audioDatabase;
    [SerializeField] private AudioClipDatabase _audioClipDatabase;
    private IAudioManager _audioManager;
    private Queue<IAudioPlayer> _audioPlayers = new Queue<IAudioPlayer>();

    private void Awake()
    {
        _audioSourcePool.Init(_audioClipDatabase.AudioClipDic);
        _audioManager = new AudioManager(_audioDatabase, _audioSourcePool);
    }

    public void Play(string audioSound)
    {
        _audioPlayers.Enqueue(_audioManager.Play(audioSound, new PlaySetting {AudioPriorityType = AudioPriorityType.Important, ObjectToAttach = transform}));
    }

    public void DisposeRandomAudio()
    {
        if (_audioPlayers.Count > 0)
        {
            _audioPlayers.Dequeue().Dispose();
        }
    }
}
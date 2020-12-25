using Audio;
using Sample.Scripts;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSourcePool _audioSourcePool;
    [SerializeField] private AudioDatabase _audioDatabase;
    [SerializeField] private AudioClipDatabase _audioClipDatabase;
    [SerializeField] private ButtonView _zombieButtonView;

    private IAudioManager _audioManager;

    private void Awake()
    {
        _audioSourcePool.Init(_audioClipDatabase.AudioClipDic);
        _audioManager = new AudioManager(_audioDatabase, _audioSourcePool);

        var zombieModel = new ZombieModel(_audioManager, "zombie_audio_player", "general");
        var zombiePresenter = new CharacterPresenter(_zombieButtonView, zombieModel);
    }
}
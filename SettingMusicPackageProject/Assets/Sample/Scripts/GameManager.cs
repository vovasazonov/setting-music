using Audio;
using Sample.Scripts;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSourcePool _audioSourcePool;
    [SerializeField] private AudioDatabase _audioDatabase;
    [SerializeField] private AudioClipDatabase _audioClipDatabase;
    [SerializeField] private ButtonView _playAudioButtonView;
    [SerializeField] private ButtonView _increaseVolumeAudioCollectionButtonView;
    [SerializeField] private ButtonView _decreaseVolumeAudioCollectionButtonView;
    [SerializeField] private ButtonView _muteAudioCollectionButtonView;
    [SerializeField] private ButtonView _stopAudioCollectionButtonView;

    private IAudioManager _audioManager;

    private void Awake()
    {
        _audioSourcePool.Init(_audioClipDatabase.AudioClipDic);
        _audioManager = new AudioManager(_audioDatabase, _audioSourcePool);

        var playSetting = new PlaySetting(followTransform: new UnityTransform(_playAudioButtonView.transform));
        var zombieModel = new ZombieModel(_audioManager, "zombie_audio_player", "general",playSetting);
        var collectionVolumeModel = new AudioCollectionModel(_audioManager.AudioCollections["general"], 0.1f);
        
        var zombiePresenter = new CharacterPresenter(_playAudioButtonView, zombieModel);
        var increaseAudioCollectionVolumePresenter = new IncreaseVolumeAudioCollectionPresenter(_increaseVolumeAudioCollectionButtonView, collectionVolumeModel);
        var decreaseAudioCollectionVolumePresenter = new DecreaseVolumeAudioCollectionPresenter(_decreaseVolumeAudioCollectionButtonView, collectionVolumeModel);
        var muteAudioCollectionPresenter = new MuteAudioCollectionPresenter(_muteAudioCollectionButtonView, collectionVolumeModel);
        var stopAudioCollectionPresenter = new StopAudioCollectionPresenter(_stopAudioCollectionButtonView, collectionVolumeModel);
    }
}
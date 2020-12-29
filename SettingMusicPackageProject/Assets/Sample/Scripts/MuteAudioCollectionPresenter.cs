namespace Sample.Scripts
{
    public class MuteAudioCollectionPresenter
    {
        private readonly IButtonView _view;
        private readonly IAudioCollectionModel _model;
        
        public MuteAudioCollectionPresenter(IButtonView view, IAudioCollectionModel model)
        {
            _view = view;
            _model = model;

            AddViewListeners();
        }

        private void AddViewListeners()
        {
            _view.Click += OnClick;
        }
        
        private void RemoveViewListeners()
        {
            _view.Click -= OnClick;
        }

        private void OnClick()
        {
            _model.Mute();
        }
    }
}
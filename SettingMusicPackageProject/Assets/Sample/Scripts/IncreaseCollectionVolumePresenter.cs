namespace Sample.Scripts
{
    public class IncreaseCollectionVolumePresenter
    {
        private readonly IButtonView _view;
        private readonly IVolumeModel _model;
        
        public IncreaseCollectionVolumePresenter(IButtonView view, IVolumeModel model)
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
            _model.Increase();
        }
    }
}
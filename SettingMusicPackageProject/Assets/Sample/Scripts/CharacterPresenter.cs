namespace Sample.Scripts
{
    public class CharacterPresenter
    {
        private readonly IButtonView _view;
        private readonly ICharacterModel _model;
        
        public CharacterPresenter(IButtonView view, ICharacterModel model)
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
            _model.HitMe();
        }
    }
}
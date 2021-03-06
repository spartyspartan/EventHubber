using EventHubber.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

namespace EventHubber.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private IDialogService _dialogService;

        public EventHubViewModel EventHub { get; set; }

        public RelayCommand<MessageViewModel> ShowMessage { get; set; }

        public MainViewModel(IDialogService dialogService)
        {
            this.EventHub = SimpleIoc.Default.GetInstance<EventHubViewModel>();
            _dialogService = dialogService;
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            this.ShowMessage = new RelayCommand<MessageViewModel>((msg) =>
            {
                _dialogService.ShowDialog("Message", msg);
            });
        }
    }
}
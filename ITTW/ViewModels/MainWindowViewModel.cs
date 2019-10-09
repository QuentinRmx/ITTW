using System;
using System.ComponentModel.DataAnnotations;
using System.Timers;
using System.Windows.Controls.Ribbon;
using ITTW.Classes;
using Prism.Commands;
using Prism.Mvvm;

namespace ITTW.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "It's time to work'";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string _textTimer;
        public string TextTimer
        {
            get => "Remaining time";
            set => _textTimer = value;
        }

        public DelegateCommand StartTimerCommand => new DelegateCommand(StartTimer);

        private void StartTimer()
        {
        }

        public MainWindowViewModel()
        {
        }
    }
}

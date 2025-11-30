using Lights.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lights.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _lightsJsonPath = "";
        private int _pollIntervalMs = 1000;

        public ObservableCollection<LightViewModel> Lights { get; } = new();

        public string LightsJsonPath
        {
            get => _lightsJsonPath;
            set
            {
                if (_lightsJsonPath != value)
                {
                    _lightsJsonPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public int PollIntervalMs
        {
            get => _pollIntervalMs;
            set
            {
                if (_pollIntervalMs != value)
                {
                    _pollIntervalMs = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ApplyConfig(AppConfig cfg)
        {
            LightsJsonPath = cfg.LightsJsonPath;
            PollIntervalMs = cfg.PollIntervalMs;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}

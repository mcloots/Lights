using Lights.Models;
using Lights.ViewModels;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lights
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm = new();
        private readonly DispatcherTimer _timer = new();

        public MainWindow()
        {
            InitializeComponent();

            // ViewModel koppelen
            DataContext = _vm;

            // 1) Config inladen
            LoadConfig();

            // 2) Eerste keer JSON lezen en lampen aanmaken
            LoadLights();

            // 3) Timer instellen o.b.v. config
            _timer.Tick += Timer_Tick;
            _timer.Interval = TimeSpan.FromMilliseconds(_vm.PollIntervalMs);
            _timer.Start();
        }

        private void LoadConfig()
        {
            try
            {
                // config.json ligt naast de .exe (dankzij Copy to Output Directory)
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string configPath = System.IO.Path.Combine(baseDir, "config.json");

                if (!File.Exists(configPath))
                {
                    MessageBox.Show($"config.json niet gevonden op {configPath}");
                    return;
                }

                string json = File.ReadAllText(configPath);

                var cfg = JsonSerializer.Deserialize<AppConfig>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (cfg == null)
                {
                    MessageBox.Show("config.json kon niet gede-serialiseerd worden.");
                    return;
                }

                _vm.ApplyConfig(cfg);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij lezen van config.json: {ex.Message}");
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            LoadLights();
        }

        private void LoadLights()
        {
            if (string.IsNullOrWhiteSpace(_vm.LightsJsonPath))
                return;

            if (!File.Exists(_vm.LightsJsonPath))
                return;

            try
            {
                string json = File.ReadAllText(_vm.LightsJsonPath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<LightStateDto>? latestStates =
                    JsonSerializer.Deserialize<List<LightStateDto>>(json, options);

                if (latestStates == null)
                    return;

                // 1) Updaten of aanmaken
                foreach (var state in latestStates)
                {
                    // Positie zoeken voor deze kamer
                    if (!RoomLayout.Positions.TryGetValue(state.Name, out var pos))
                        continue; // onbekende kamer → overslaan

                    var vmLight = _vm.Lights.FirstOrDefault(l => l.Id == state.Id);

                    if (vmLight == null)
                    {
                        // NIEUWE lamp → toevoegen
                        _vm.Lights.Add(new LightViewModel
                        {
                            Id = state.Id,
                            Name = state.Name,
                            IsOn = state.IsOn,
                            X = pos.X,
                            Y = pos.Y
                        });
                    }
                    else
                    {
                        // bestaande lamp → status (en evt. positie) updaten
                        vmLight.IsOn = state.IsOn;
                        vmLight.X = pos.X;
                        vmLight.Y = pos.Y;
                    }
                }

                // 2) (optioneel) lampen verwijderen die niet meer in JSON staan
                for (int i = _vm.Lights.Count - 1; i >= 0; i--)
                {
                    var id = _vm.Lights[i].Id;
                    if (!latestStates.Any(s => s.Id == id))
                    {
                        _vm.Lights.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void FloorPlanImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(FloorPlanImage);
            System.Diagnostics.Debug.WriteLine($"Klik: X={p.X}, Y={p.Y}");
        }
    }
}
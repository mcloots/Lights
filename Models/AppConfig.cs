using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lights.Models
{
    public class AppConfig
    {
        [JsonPropertyName("lightsJsonPath")]
        public string LightsJsonPath { get; set; } = "";

        [JsonPropertyName("pollIntervalMs")]
        public int PollIntervalMs { get; set; } = 1000;
    }
}

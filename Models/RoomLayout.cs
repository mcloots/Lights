using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lights.Models
{
    public static class RoomLayout
    {
        public static readonly Dictionary<string, (double X, double Y)> Positions =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["woonkamer"] = (415, 225),
            ["eetkamer"] = (423, 415),
            ["keuken"] = (400, 610),
            ["toilet"] = (525, 640),

            ["slaapkamer 1"] = (192, 652),
            ["slaapkamer 2"] = (235, 150),
            ["studeerkamer"] = (665, 260),

            ["badkamer 1"] = (127, 460),
            ["badkamer 2"] = (88, 170),
            ["badkamer 3"] = (777, 135),

            ["veranda"] = (658, 550),

            ["terras"] = (640, 670),
            ["tuin"] = (875, 190)
        };
    }

}

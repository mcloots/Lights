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
            ["woonkamer"] = (665, 541),
            ["keukentafel"] = (433, 423),
            ["keuken"] = (406, 620),

            ["slaapkamer 1"] = (202, 652),
            ["slaapkamer 2"] = (245, 150),
            ["slaapkamer 3"] = (734, 369),

            ["badkamer 1"] = (134, 470),
            ["badkamer 2"] = (101, 199),
            ["badkamer 3"] = (784, 140),

            ["studeerkamer"] = (684, 262),
            ["inkomhal"] = (668, 555),

            ["terras"] = (653, 653),
            ["tuin"] = (858, 184)
        };
    }

}

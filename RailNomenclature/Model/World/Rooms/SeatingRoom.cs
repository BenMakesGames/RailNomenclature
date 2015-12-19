using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class SeatingRoom: Room
    {
        public SeatingRoom(World w)
            : base(w, 960, 540, "Seating Room")
        {
            new Wall(this, Width / 2, 80, Width, 80, RGBA.DarkRed);
            
            new RailMap(this, 300, 80);

            // horizontal wall
            new RectangleStructure(this, 200 + 630 / 2, 200, 630, 40, 20, RGBA.DarkRed, RGBA.Black);

            var whiteDoor = new RectangleStructure(this, 830 + 90 / 2, 200, 90, 40, 20, RGBA.White, RGBA.Gray);
            whiteDoor.AttachSwitch(new NotSwitch(new ToggleSwitch(this, 220, 280, RGBA.White)));
            
            new RectangleStructure(this, 920 + 40 / 2, 200, 40, 40, 20, RGBA.DarkRed, RGBA.Black);

            // left vertical wall
            new RectangleStructure(this, 180 + 20 / 2, 80 + 250, 20, 40, 250, RGBA.DarkRed, RGBA.Black);

            // right vertical wall
            new RectangleStructure(this, 770 + 20 / 2, 330, 20, 40, 130, RGBA.DarkRed, RGBA.Black);
        }
    }
}

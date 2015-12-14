using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class RailLinePuzzleRoom: Room
    {
        public RailLinePuzzleRoom(World world)
            : base(world, 2000, 700, "Rail Line")
        {
            SetBackgroundColor(new RGBA(214, 194, 158));

            ToggleSwitch redToggle = new ToggleSwitch(this, 380, 650, new RGBA(255, 0, 0));

            PressurePlate bluePressurePlate = new PressurePlate(this, 430, 460, 30, 30, new RGBA(51, 153, 204));
            PressurePlate orangePressurePlate = new PressurePlate(this, 850, 140, 30, 30, new RGBA(255, 153, 0));

            // top wall
            new Wall(this, Width / 2, 80, Width, 80, RGBA.White);
            // middle-bottom block
            new RectangleStructure(this, 490 + 530 / 2, Height, 530, 40, 190, RGBA.Black, RGBA.Black);

            // bottom-left wall
            new RectangleStructure(this, 0 + 290 / 2, 530, 290, 40, 20, RGBA.White, RGBA.Black);
            RectangleStructure redDoor = new RectangleStructure(this, 290 + 90 / 2, 530, 90, 40, 20, new RGBA(255, 0, 0), new RGBA(128, 0, 0));
            new RectangleStructure(this, 380 + 110 / 2, 530, 110, 40, 20, RGBA.White, RGBA.Black);

            // middle horizontal wall
            new RectangleStructure(this, 0 + 290 / 2, 300, 290, 40, 20, RGBA.White, RGBA.Black);
            RectangleStructure blueDoor = new RectangleStructure(this, 290 + 90 / 2, 300, 90, 40, 20, new RGBA(51, 153, 204), new RGBA(26, 77, 102));
            new RectangleStructure(this, 380 + 620 / 2, 300, 620, 40, 20, RGBA.White, RGBA.Black);

            // middle vertical wall
            new RectangleStructure(this, 490 + 20 / 2, 370, 20, 40, 70, RGBA.White, RGBA.Black);
            RectangleStructure orangeDoor = new RectangleStructure(this, 490 + 20 / 2, 440, 20, 40, 70, new RGBA(255, 153, 0), new RGBA(255, 153, 0));
            new RectangleStructure(this, 490 + 20 / 2, 510, 20, 40, 70, RGBA.White, RGBA.Black);

            // right wall
            new RectangleStructure(this, 1000 + 20 / 2, 370, 20, 40, 310, RGBA.White, RGBA.Black);
            new RectangleStructure(this, 1000 + 20 / 2, 510, 20, 40, 70, RGBA.White, RGBA.Black);

            redDoor.AttachSwitch(new NotSwitch(redToggle));
            blueDoor.AttachSwitch(new NotSwitch(bluePressurePlate));
            orangeDoor.AttachSwitch(new NotSwitch(orangePressurePlate));
        }

        public override void Step()
        {
            base.Step();

            if (World.GetQuestValue(World.QUEST_EXPLORE_STATION) == 1)
            {
                ThePlayer player = (ThePlayer)World.FindCharacter(typeof(ThePlayer));
                ProfessorRed professor = (ProfessorRed)World.FindCharacter(typeof(ProfessorRed));

                if (player != null && professor != null)
                {
                    if (player.Location == this)
                    {
                        World.SetQuestValue(World.QUEST_EXPLORE_STATION, 2);

                        World.PlayingState.QueueState(new GameStateCutSceneProfessorJoins(player, professor));
                    }
                }
            }
        }
    }
}

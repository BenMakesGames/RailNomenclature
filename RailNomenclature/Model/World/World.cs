﻿using System;
using System.Collections.Generic;

namespace RailNomenclature
{
    [Serializable]
    public class World
    {
        public List<Room> Locations { get; protected set; }
        public List<Thing> Things { get; protected set; }

        public Character ActiveCharacter { get; set; }
        public List<Character> Characters  { get; protected set; }
        public Camera Camera { get; protected set; }

        public Thing ActionableThingUnderCursor { get; protected set; }

        private List<FlashMessage> _flash_messages = new List<FlashMessage>();
        private List<TeleportStation> _teleporters = new List<TeleportStation>();

        public GameStatePlaying PlayingState { get; set; }

        public const int QUEST_PLAYER_SAW_RAIL_MAP = 0;
        public const int QUEST_RAIL_MAP_QUIZ_SCORE = 0;
        
        public Dictionary<int, int> QuestVariables = new Dictionary<int, int>();

        public World()
        {
            Locations = new List<Room>();
            Things = new List<Thing>();
            Characters = new List<Character>();

            Room baseCamp = BuildBaseCamp();
            
            Room caveEntrance = new Room(this, 1400, 800, "Cave Entrance");
            caveEntrance.SetBackgroundColor(new RGBA(153));

            /*LinkLocationsWithDoor(
                baseCamp, baseCamp.Width * 4 / 5, 181 + 48,
                caveEntrance, 200, 200
            );*/
            Door d1 = new Door(baseCamp, baseCamp.Width * 4 / 5, 181 + 48);
            Door d2 = new Door(caveEntrance, 200, 200);

            d1.PairWith(d2);

            new RailMap(caveEntrance, 400, 160);

            // add characters and attach camera
            ProfessorRed professorRed = new ProfessorRed(baseCamp, 200, TheGame.HEIGHT / 2);
            ActiveCharacter = new Character(baseCamp, 50, TheGame.HEIGHT - 32, "You", "It's just me.");

            new HomeBase(baseCamp, 400, 250, professorRed);

            Camera = new Camera(ActiveCharacter);

            d1.IsLocked = delegate(Door d, Thing t)
            {
                if (professorRed.HasGivenIntroductionTalk()) return false;

                t.Notify(professorRed.Name(), "Eh? Who's that? Come say hello!");
                return true;
            };
        }

        private Room BuildBaseCamp()
        {
            Room baseCamp = new Room(this, 1000, TheGame.HEIGHT, "Base Camp");
            baseCamp.SetBackgroundColor(new RGBA(89, 135, 3));

            baseCamp.MinCharacterY = 182;

            //LinkLocationsWithDoor(Street, 800, store, 10);

            new Tree(baseCamp, 100, 500);
            new Tree(baseCamp, 350, 200);
            new Tree(baseCamp, 750, 400);
            new Tree(baseCamp, 900, 250);

            new Wall(baseCamp, baseCamp.Width / 2, 181, baseCamp.Width, 181, new RGBA(100, 84, 55));

            new RectangleStructure(baseCamp, baseCamp.Width * 4 / 5, 181 + 47, 283, 94, 47, new RGBA(102), new RGBA(153));

            return baseCamp;
        }

        protected void LinkLocationsWithDoor(Room l1, int x1, int y1, Room l2, int x2, int y2)
        {
            Door d1 = new Door(l1, x1, y1);
            Door d2 = new Door(l2, x2, y2);

            d1.PairWith(d2);
        }

        private int _step_count = 0;

        public void Step()
        {
            for (int i = _flash_messages.Count - 1; i >= 0; i--)
            {
                if (_flash_messages[i].Life == 1)
                    _flash_messages.RemoveAt(i);
                else
                    _flash_messages[i].Life--;
            }

            // Locations don't update their Things, because if they did, then a Thing
            // might get updated twice when it moves from one Location to another...
            foreach (Room l in Locations)
                l.Step();

            // ... so instead, Things update themselves after Locations do whatever they
            // want to do (ex: weather change, which might affect what a Thing wants to
            // do!)
            foreach (Thing t in Things)
            {
                if (t is Character)
                    (t as Character).HandleInput();

                t.PreStep();
                t.Step();
                t.PostStep();
            }

            for (int i = Things.Count - 1; i >= 0; i--)
            {
                if (Things[i].FlaggedForDestruction)
                    RemoveFromWorld(Things[i]);
            }

            Camera.Step();

            _step_count++;

            if (_step_count == TheGame.FPS * 1440)
                _step_count = 0;

            FindActionableThingAtCursor();
        }

        public void HandleInput()
        {
            if (TheGame.Instance.IsMouseOnUIElement()) return;

            if(MouseHandler.Instance.IsLeftClicking(true) && ActionableThingUnderCursor != null)
            {
                if (ActionableThingUnderCursor.WithinDistance(ActiveCharacter, ActiveCharacter.ActionReach() + ActionableThingUnderCursor.MaximumPrimaryActionDistance()))
                    ActionableThingUnderCursor.DoPrimaryAction(ActiveCharacter);
                else
                    AddFlashMessage(ActionableThingUnderCursor.Its().UppercaseFirst() + " too far away.");
            }
            else if (MouseHandler.Instance.IsRightClicking(true) && ActionableThingUnderCursor != null)
            {
                if (ActionableThingUnderCursor.WithinDistance(ActiveCharacter, ActiveCharacter.ActionReach() + ActionableThingUnderCursor.MaximumSecondaryActionDistance()))
                    ActionableThingUnderCursor.DoSecondaryAction(ActiveCharacter);
                else
                    AddFlashMessage(ActionableThingUnderCursor.Its().UppercaseFirst() + " too far away.");
            }
            else if (MouseHandler.Instance.IsLeftClicking())
            {
                if (ActionableThingUnderCursor == null)
                    ActiveCharacter.SetPath((int)(MouseHandler.Instance.X() + Camera.X), (int)(MouseHandler.Instance.Y() + Camera.Y));
            }
        }

        public void AddFlashMessage(string message)
        {
            _flash_messages.Insert(0, new FlashMessage(message, TheGame.FPS * 2));
        }

        public void Draw()
        {
            ActiveCharacter.Location.Draw();

            if (ActionableThingUnderCursor != null)
                ActionableThingUnderCursor.DrawInstructions(Camera);


            int y = 16;

            foreach (FlashMessage m in _flash_messages)
            {
                Assets.Fonts[FontID.Consolas16].WriteText((TheGame.WIDTH - (m.Message.Width() * 9)) / 2, y, m.Message, ActiveCharacter.Location.InstructionTextColor);
                y += Assets.Fonts[FontID.Consolas16].LineHeight + 2;
            }
        }

        private void RemoveFromWorld(Thing t)
        {
            foreach (Room r in Locations)
                r.RemoveThing(t);

            foreach (Character c in Characters)
                c.RemoveInventory(t);

            Things.Remove(t);
        }

        public void FindActionableThingAtCursor()
        {
            if (ActiveCharacter == null || ActiveCharacter.Location == null)
                ActionableThingUnderCursor = null;
            else
                ActionableThingUnderCursor = ActiveCharacter.Location.FindActionableThingAtCursor(Camera);
        }

        public int GetQuestValue(int key)
        {
            if (QuestVariables.ContainsKey(key))
                return QuestVariables[key];
            else
                return 0;
        }

        public void SetQuestValue(int key, int value)
        {
            QuestVariables[key] = value;
        }

        public void IncrementQuestValue(int key, int value = 1)
        {
            QuestVariables[key] = GetQuestValue(key) + value;
        }

        public void DecrementQuestValue(int key, int value = 1)
        {
            QuestVariables[key] = GetQuestValue(key) - value;
        }

        public TeleportStation NextTeleporter(TeleportStation t)
        {
            int i = _teleporters.IndexOf(t);

            if (i == -1)
                return null;
            else
                return _teleporters[(i + 1) % _teleporters.Count];
        }

        public void AddTeleporter(TeleportStation t)
        {
            if(!_teleporters.Contains(t))
                _teleporters.Add(t);
        }

        public void RemoveTeleporter(TeleportStation t)
        {
            _teleporters.Remove(t);
        }
    }
}
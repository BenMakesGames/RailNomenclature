using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class ProfessorRed: Character
    {
        public const string DESCRIPTION = "Professor Red. An eccentric character, easily excited by ancient American city planning.";

        public const int STATE_NEEDS_INTRODUCTION = 0;
        public const int STATE_WAITING_FOR_INFORMATION = 1;
        public const int STATE_WAITING_FOR_INFORMATION_AGAIN = 2;
        public const int STATE_WAITING_FOR_PLAYER_TO_EXPLORE = 3;

        private int _current_state = STATE_NEEDS_INTRODUCTION;

        public ProfessorRed(Room r, float x, float y): base(r, x, y, "Professor Red", DESCRIPTION)
        {
            // @TODO: DEBUGGING; REMOVE FOR RELEASE:
            /*_current_state = STATE_WAITING_FOR_INFORMATION;
            Location.World.SetQuestValue(World.QUEST_PLAYER_SAW_RAIL_MAP, 1);*/
        }

        public bool HasGivenIntroductionTalk()
        {
            return _current_state != STATE_NEEDS_INTRODUCTION;
        }

        public override void DoSecondaryAction(Thing a)
        {
            switch (_current_state)
            {
                case STATE_NEEDS_INTRODUCTION:
                    a.Notify(Name(), new List<string>() {
                        "Oh, hello! You must be the new grad student!",
                        "I'm Professor Red! Sorry, I guess you already knew that.",
                        "I'm so excited to have someone else to help me out! I've just stumbled upon this old building. It was a train station in ancient America!",
                        "I have some theories about how these were organized. Maybe we could put one to the test! Care to help me out?",
                        "Of course you would!",
                        "Go take a look in the building over there. I'm sure there will be SOMETHING telling. I'm interested in knowing how their rail system was arranged.",
                        "Oh, and take this portable Teleport Station, in case you get lost!",
                        "I'll be here, going over my notes. I'm trying to figure out which American city this must have been..."
                    });

                    if (a is Character)
                        (a as Character).TeleportStations++;

                    _current_state = STATE_WAITING_FOR_INFORMATION;
                    
                    break;

                case STATE_WAITING_FOR_INFORMATION:

                    if (Location.World.GetQuestValue(World.QUEST_PLAYER_SAW_RAIL_MAP) == 0)
                    {
                        a.Notify(Name(), "Were you able to find anything? There must be something! Look around a little more!");
                    }
                    else
                    {
                        a.Notify(Name(), new List<string>() {
                            "Ah, you're back! I take it you found something?",
                            "A map?! Amazing!",
                            "Was it as I suspected? Are there more-- wait, no! I don't want you to be biased!",
                            "Before I tell you my theory, tell me what you found! On the map, were there more Heavy Rail, or more Light Rail systems?"
                        });

                        QuizAboutRailMapQuestion1(a);
                    }

                    break;

                case STATE_WAITING_FOR_INFORMATION_AGAIN:
                    if (Location.World.GetQuestValue(World.QUEST_PLAYER_SAW_RAIL_MAP) == 0)
                    {
                        a.Notify(Name(), "Can you double-check the map you found? Just to make sure?");
                    }
                    else
                    {
                        a.Notify(Name(), "Ah, you're back! What did you find?");

                        QuizAboutRailMapQuestion1(a);
                    }
                    break;

                case STATE_WAITING_FOR_PLAYER_TO_EXPLORE:
                    a.Notify(Name(), "Why don't you go see what else you can find? I'll join you in a couple minutes, as soon as I finish up these notes.");
                    break;
            }
        }

        private void QuizAboutRailMapQuestion1(Thing a)
        {
            Location.World.SetQuestValue(World.QUEST_RAIL_MAP_QUIZ_SCORE, 0);

            Location.World.PlayingState.QueueState(new GameStateMultipleChoiceModal(
                a,
                "Are there more Heavy Rail, or Light Rail systems?",
                new List<string>() {
                    "More Heavy",
                    "More Light"
                },
                new List<GameStateMultipleChoiceModal.ChoiceDelegate>()
                {
                    // more heavy:
                    (GameStateMultipleChoiceModal.ChoiceDelegate)delegate(Thing t) {
                        QuizAboutRailMapQuestion2(a);
                    },
                    // more light - correct:
                    (GameStateMultipleChoiceModal.ChoiceDelegate)delegate(Thing t) {
                        Location.World.IncrementQuestValue(World.QUEST_RAIL_MAP_QUIZ_SCORE, 1);

                        QuizAboutRailMapQuestion2(a);
                    }

                }
            ));
        }

        private void QuizAboutRailMapQuestion2(Thing a)
        {
            a.Notify(Name(), "And where were most of the Heavy Rail systems found? In large cities, or small ones?");

            Location.World.PlayingState.QueueState(new GameStateMultipleChoiceModal(
                a,
                "Where are most Heavy Rail systems found?",
                new List<string>() {
                    "Large cities",
                    "Small cities"
                },
                new List<GameStateMultipleChoiceModal.ChoiceDelegate>()
                {
                    // large cities - correct:
                    (GameStateMultipleChoiceModal.ChoiceDelegate)delegate(Thing t) {
                        Location.World.IncrementQuestValue(World.QUEST_RAIL_MAP_QUIZ_SCORE, 1);

                        QuizAboutRailMapConclusion(a);
                    },
                    // small cities:
                    (GameStateMultipleChoiceModal.ChoiceDelegate)delegate(Thing t) {
                        QuizAboutRailMapConclusion(a);
                    }
                }
            ));
        }

        private void QuizAboutRailMapConclusion(Thing a)
        {
            if (Location.World.GetQuestValue(World.QUEST_RAIL_MAP_QUIZ_SCORE) < 2)
            {
                a.Notify(Name(), new List<string>() {
                    "Hm... strange...",
                    "I mean, I suppose it's possible that this area is unique, but...",
                    "Can you double-check the map you found? Just to make sure?"
                });

                Location.World.SetQuestValue(World.QUEST_PLAYER_SAW_RAIL_MAP, 0);
                _current_state = STATE_WAITING_FOR_INFORMATION_AGAIN;
            }
            else
            {
                a.Notify(Name(), new List<string>() {
                    "Amazing! Wonderful!",
                    "It's just as I suspected!",
                    "I need to write this down, but why don't you go see what else you can find in there? I'll join you in a couple minutes."
                });

                _current_state = STATE_WAITING_FOR_PLAYER_TO_EXPLORE;
            }
        }
    }
}

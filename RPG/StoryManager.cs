using System;
using System.Collections.Generic;
using System.Threading;
using StringTheory;

namespace RPG
{
    public enum StorySequence
    {
        Introduction,
        SlimeCave,
        SlimeCaveWin,
        VillageIntroduction,
        VillageCamp,
        VillageCampIncomplete,
        VillageCampComplete,
        VillageArmourer,
        VillageArmourerComplete,
        VillageArmourerIncomplete,
        VillageWander,
        VillageIncomplete,
        VillageClimax,
        VillageThiefWin,
        JourneyThiefBossWin,
        SoloCastleExplore,
        SoloCastleHealingChamber,
        SoloCastleHealingChamberWater,
        SoloCastleHealingChamberNoWater,
        SoloCastleTop,
        BossFightWin,
        BossFightPart1Lose,
        BossFightPart2,
        DeathGeneric,
        DevTest
    }
    public enum StoryEvent
    {
        VillageVisitedCamp,
        VillageVisitedArmourer,
        VillageClimax
    }
    public static class StoryManager
    {
        public static Sequence GetStorySequence(StorySequence enumerator)
        {
            Sequence seq = new Sequence();
            STFunc funky = delegate () { };
            STFunc[] funkyChoose;

            switch (enumerator)
            {
                case StorySequence.Introduction:
                    {
                        seq.AddEvent(new DynamicString(". . . ", clearScreen: true, delay: 500, delayPerChar: true));
                        seq.AddEvent(new DynamicString("Welcome . . ", delay: 45, delayPerChar: true));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("We've been waiting for you.", clearScreen: true, delay: 250));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("You're here to play a game.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString(" So let's play.", delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("First.", clearScreen: true, newLine: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString(" Tell me of you and your two friends.", delayPerChar: true, delay: 45)); ;
                        seq.AddEvent(new Pause(500));
                        funky = delegate()
                        {
                            GameManager.getInstance().playerTeam = new Team(3);
                        };
                        seq.AddEvent(new Function(funky));

                        funky = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.SlimeCave));
                        };
                        seq.AddEvent(new Function(funky));

                        break;
                    }
                case StorySequence.SlimeCave:
                    {
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("What an unsightly bunch.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString("Your party finds themselves mid-journey through the deep, dark caves.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("The caves are filled with Slimes.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString("Light shines from the distance. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("The exit is near.", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("Suddenly. . .", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new PressContinue());
                        //Slight Fight
                        funky = delegate ()
                        {
                            // Create slimes
                            List<Character> slimes = new List<Character>();
                            slimes.Add(new Character("Slime", 20, 0, new Weapon("goopy body", 7, 0)));
                            slimes.Add(new Character("Slime", 20, 0, new Weapon("acidic body", 6, 0)));
                            slimes.Add(new Character("Slime", 20, 0, new Weapon("goopy body", 7, 0)));
                            slimes.Add(new Character("Slime", 20, 0, new Weapon("acidic body", 5, 0)));
                            Team slimeTeam = new Team(slimes, false);

                            // Initiate battle
                            Battle battle = new Battle(GameManager.getInstance().playerTeam, slimeTeam);

                            // Post Slime Fight
                            if (battle.DoBattle()) seq = StoryManager.GetStorySequence(StorySequence.SlimeCaveWin);
                            else seq = StoryManager.GetStorySequence(StorySequence.DeathGeneric);
                            GameManager.getInstance().mainSequence.AddSequence(seq);
                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
                case StorySequence.SlimeCaveWin:
                    {
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Well done.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString("With the Slimes cleared, you advance forward to the crack of light.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Emerging from the caves you find yourselves in a lush green forest.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("A broken sign on the ground reads: ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"DO NOT ENTER. ", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("OVERRUN WITH SLIMES. ", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("YOU WILL NOT RETURN\".", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You hear the echo of voices and clatter of busy work up ahead.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Following the sounds, your party steps foot in to a small village.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));

                        funky = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.VillageIntroduction));
                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
                case StorySequence.VillageIntroduction:
                    {
                        // Villager
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"Your blades.. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("dripping of slime. . ", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You've cleared the cave?\"", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString(" a villager questions.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"We owe you a great deal.\"", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"Please, let us help you with what little we have.\"", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"You can rest as long as you need at the camp.\"", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"Our blacksmith can help with your weapons.\"", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        Sequence[] choices = new Sequence[] { GetStorySequence(StorySequence.VillageCamp), GetStorySequence(StorySequence.VillageArmourer), GetStorySequence(StorySequence.VillageWander) };
                        seq.AddEvent(new Choose(new string[] { "Rest at the camp.", "Visit the blacksmith.", "Wander the village" }, choices, ref GameManager.getInstance().mainSequence));
                        break;
                    }
                case StorySequence.VillageCamp:
                    {
                        funky = delegate ()
                        {
                            if(!GameManager.getInstance().IsEventCompleted(StoryEvent.VillageVisitedCamp))
                            {
                                GameManager.getInstance().mainSequence.InsertSequence(StoryManager.GetStorySequence(StorySequence.VillageCampIncomplete));
                            }
                            else
                            {
                                GameManager.getInstance().mainSequence.InsertSequence(StoryManager.GetStorySequence(StorySequence.VillageCampComplete));
                            }
                        };
                        seq.AddEvent(new Function(funky));

                        funkyChoose = new STFunc[2];
                        funkyChoose[0] = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.VillageArmourer));
                        };

                        funkyChoose[1] = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.VillageWander));
                        };

                        seq.AddEvent(new Choose(new string[] { "Visit the blacksmith.", "Wander the village." }, funkyChoose));
                        break;
                    }
                case StorySequence.VillageCampIncomplete:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("You spend the night at the villages campsite.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("All characters HP has been restored.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));

                        // Healing event
                        funky = delegate ()
                        {
                            foreach (Character person in GameManager.getInstance().playerTeam)
                            {
                                person.RefreshStatus();
                            }
                            GameManager.getInstance().AddCompletedEvent(StoryEvent.VillageVisitedCamp);
                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
                case StorySequence.VillageCampComplete:
                    {
                        seq.AddEvent(new DynamicString("Your party is well rested and does not have time to sleep all day.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        break;
                    }
                case StorySequence.VillageArmourer:
                    {
                        funky = delegate ()
                        {
                            if (!GameManager.getInstance().IsEventCompleted(StoryEvent.VillageVisitedArmourer))
                            {
                                GameManager.getInstance().mainSequence.InsertSequence(StoryManager.GetStorySequence(StorySequence.VillageArmourerIncomplete));
                            }
                            else
                            {
                                GameManager.getInstance().mainSequence.InsertSequence(StoryManager.GetStorySequence(StorySequence.VillageArmourerComplete));
                            }
                        };
                        seq.AddEvent(new Function(funky));
                        Sequence[] choices = new Sequence[] { GetStorySequence(StorySequence.VillageCamp), GetStorySequence(StorySequence.VillageWander) };
                        seq.AddEvent(new Choose(new string[] { "Rest at the camp.", "Wander the village" }, choices, ref GameManager.getInstance().mainSequence));
                        break;
                    }
                case StorySequence.VillageArmourerIncomplete:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("\"Hello! ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("I heard what you did for us.\"", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"With the caves cleared we'll be able to start mining the Iron again.\"", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"Thanks for that. My services are yours. let me take a look at your gear.\"", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        funky = delegate ()
                        {
                            Sequence armourUp = new Sequence();
                            foreach(Character ch in GameManager.getInstance().playerTeam)
                            {
                                //Upgrade weapons
                                switch(ch.Weapon)
                                {
                                    default:
                                        armourUp.AddEvent(new Pause(500));
                                        armourUp.AddEvent(new DynamicString($"\"{ch.Name}, ",
                                            newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                                        armourUp.AddEvent(new Pause(500));
                                        armourUp.AddEvent(new DynamicString($"Your {ch.Weapon.Name} is in terrible shape",
                                            newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                                        armourUp.AddEvent(new Pause(500));
                                        armourUp.AddEvent(new DynamicString($", let me fix it up for you.\"",
                                            newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                                        ch.Weapon.Upgrade();
                                        break;
                                }

                                // Multiply defense
                                ch.Defence *= 3;
                            }
                            armourUp.AddEvent(new DynamicString("We also have some armour, please take it.",
                                            newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                            GameManager.getInstance().mainSequence.InsertSequence(armourUp);
                            GameManager.getInstance().AddCompletedEvent(StoryEvent.VillageVisitedArmourer);
                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
                case StorySequence.VillageArmourerComplete:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("The armourer looks at you with confusion.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"Is there an issue with my work?\"", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        break;
                    }
                case StorySequence.VillageWander:
                    {
                        // Check village exploration status
                        funky = delegate ()
                        {
                            // Gatekeep - Check if other locations are completed first.
                            if (!(GameManager.getInstance().IsEventCompleted(StoryEvent.VillageVisitedCamp)
                            &&
                            GameManager.getInstance().IsEventCompleted(StoryEvent.VillageVisitedArmourer)))
                            {
                                GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.VillageIncomplete));
                            }
                            else
                            {
                                GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.VillageClimax));
                            }
                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
                case StorySequence.VillageIncomplete:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("You wander the village aimlessly.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Something tells you this game isn't very good and you should visit the other locations first.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        funkyChoose = new STFunc[2];
                        funkyChoose[0] = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.VillageCamp));
                        };

                        funkyChoose[1] = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.VillageArmourer));
                        };

                        seq.AddEvent(new Choose(new string[] { "Rest at the camp.", "Visit the Blacksmith." }, funkyChoose));
                        break;
                    }
                case StorySequence.VillageClimax:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("After gracefully accepting the help of the villagers, you now feel prepared for anything.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"STOP! ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Theives!!\"", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Two cloaked figures are running from the armourers tent towards your party.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new PressContinue());

                        // Theif fight
                        funky = delegate ()
                        {
                            List<Character> thieves = new List<Character>();
                            thieves.Add(new Character("Handsome Thief", 55, 0, new Weapon()));
                            thieves.Add(new Character("Not-Handsome Thief", 65, 0, new Weapon()));
                            Team thiefTeam = new Team(thieves, false);

                            Battle battle = new Battle(GameManager.getInstance().playerTeam, thiefTeam);

                            // Post Thief Fight
                            if (battle.DoBattle())
                            {
                                seq = StoryManager.GetStorySequence(StorySequence.VillageThiefWin);
                                GameManager.getInstance().AddCompletedEvent(StoryEvent.VillageClimax);
                            }
                            else seq = StoryManager.GetStorySequence(StorySequence.DeathGeneric);
                            GameManager.getInstance().mainSequence.AddSequence(seq);


                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
                case StorySequence.VillageThiefWin:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("\"Good job on killing those thieves. You are our saviour.\"", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"Please, rest another night at the camp.\"", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddSequence(GetStorySequence(StorySequence.VillageCampIncomplete));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("During the night you had strange dreams. A voice calling you North.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Images of a decrepit castle.  A shadowy figure.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Sounds like your next boss fight.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You venture out of the village Northwards, following a windy path.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You've no idea how far your destination is. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Thick tree branches block the horizon.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You do, ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("however, ", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("notice the lack of noise.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("The forest animals are nowhere to be heard.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Your party pauses to listen.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("THWAK. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("An arrow screeches past your ear, lodging itself in the tree behind you.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("\"AWWWweeee sheeit. Idiot, you missed!\"", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new PressContinue());

                        funky = delegate ()
                        {
                            // Create Thief party
                            List<Character> thieves = new List<Character>();
                            thieves.Add(new Character("Leader Thief", 150, 0, new Weapon("Sharp Sword", 15, 0)));
                            thieves.Add(new Character("Idiot Archer", 70, 0, new Weapon("Flimsy Bow", 10, 0)));
                            Team thiefTeam = new Team(thieves, false);

                            // Initiate battle
                            Battle battle = new Battle(GameManager.getInstance().playerTeam, thiefTeam);

                            // Post Slime Fight
                            if (battle.DoBattle()) seq = StoryManager.GetStorySequence(StorySequence.JourneyThiefBossWin);
                            else seq = StoryManager.GetStorySequence(StorySequence.DeathGeneric);
                            GameManager.getInstance().mainSequence.AddSequence(seq);
                        };
                        seq.AddEvent(new Function(funky));

                        break;
                    }
                case StorySequence.JourneyThiefBossWin:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("With the thieves dispatched. You continue on your journey.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("The horizon begins to clear, slowly revealing a tower.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("A tower attached to a castle.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You've arrived.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString("You follow a winding set of stairs up to the entrance.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Searching for the door, you find it ripped and torn across the entrance hall.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You step in. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("An unwelcome foul stench permeates the air.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("The place is damp and mouldy. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("You feel it could collapse at any moment.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("As if on queue, the floor falls out from beneath you.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Your party tumbles down through countless levels. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString("You have become separated.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString("Choose.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));

                        // Choose character
                        funkyChoose = new STFunc[3];
                        funkyChoose[0] = delegate ()
                        {
                            List<Character> solo = new List<Character>();
                            solo.Add(GameManager.getInstance().playerTeam.Member(0));
                            GameManager.getInstance().soloTeam = new Team(solo, true);
                        };

                        funkyChoose[1] = delegate ()
                        {
                            List<Character> solo = new List<Character>();
                            solo.Add(GameManager.getInstance().playerTeam.Member(1));
                            GameManager.getInstance().soloTeam = new Team(solo, true);
                        };

                        funkyChoose[2] = delegate ()
                        {
                            List<Character> solo = new List<Character>();
                            solo.Add(GameManager.getInstance().playerTeam.Member(2));
                            GameManager.getInstance().soloTeam = new Team(solo, true);
                        };
                        string[] characters = GameManager.getInstance().playerTeam.Names();
                        seq.AddEvent(new Choose(characters, funkyChoose));

                        funky = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.SoloCastleExplore));
                        };
                        seq.AddEvent(new Function(funky));

                        break;
                    }
                case StorySequence.SoloCastleExplore:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("You muster the energy and get back up on your feet.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Separated from your party, the eerie silence is chilling to the bone.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Looking around, you see you are surrounded by iron bars.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You are in the castles dungeon.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("It's dark. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You can't see much. ", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("But you can feel the musty air pulling down a dark hallway.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));

                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You begin to step forward to follow the path, but-", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new PressContinue());
                        funky = delegate ()
                        {
                            List<Character> skellies = new List<Character>();
                            skellies.Add(new Character("SpOoKy sKelLiNgToN", 15, 5, new Weapon("Bone Sword", 10, 1)));
                            skellies.Add(new Character("SpOoKy sKelLiNgToN", 13, 6, new Weapon("Bone Sword", 10, 2)));
                            skellies.Add(new Character("SpOoKy sKelLiNgToN", 19, 4, new Weapon("Bone Sword", 10, 3)));
                            Team skellieTeam = new Team(skellies, false);

                            Battle battle = new Battle(GameManager.getInstance().soloTeam, skellieTeam);

                            // Post skelly Fight
                            if (battle.DoBattle())
                            {
                                seq = StoryManager.GetStorySequence(StorySequence.SoloCastleHealingChamber);
                            }
                            else seq = StoryManager.GetStorySequence(StorySequence.DeathGeneric);
                            GameManager.getInstance().mainSequence.AddSequence(seq);

                        };
                        seq.AddEvent(new Function(funky));

                        break;
                    }
                case StorySequence.SoloCastleHealingChamber:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("With the Skeletons dispatched, you continue on your path down the hallway.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Another room appears on the left. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("A blue glow emanates from within.", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Peering in. You see the source of the glow.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("A pool of water illuminates the room. ", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("In here, you can't smell the musty air.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));



                        funkyChoose = new STFunc[2];
                        funkyChoose[0] = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.SoloCastleHealingChamberWater));
                        };
                        funkyChoose[1] = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.SoloCastleHealingChamberNoWater));
                        };
                        seq.AddEvent(new Choose(new string[2] { "Dip in to the pool.", "Don't touch the water."}, funkyChoose));
                        break;
                    }
                case StorySequence.SoloCastleHealingChamberWater:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("You step in to the pool of water. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("It's warm.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("HP has been restored.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));

                        // Healing event
                        funky = delegate ()
                        {
                            foreach (Character person in GameManager.getInstance().playerTeam)
                            {
                                person.RefreshStatus();
                            }
                        };
                        seq.AddEvent(new Function(funky));

                        // next path
                        funky = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.SoloCastleTop));
                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
                case StorySequence.SoloCastleHealingChamberNoWater:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("You avoid the water.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("Looking back. It does seem kind of inviting.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));

                        funkyChoose = new STFunc[2];
                        funkyChoose[0] = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.SoloCastleHealingChamberWater));
                        };
                        funkyChoose[1] = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.SoloCastleHealingChamberNoWater));
                        };
                        seq.AddEvent(new Choose(new string[2] { "OK, Sure. Dip in to the pool.", "Don't touch the water." }, funkyChoose));
                        break;
                    }
                case StorySequence.SoloCastleTop:
                    {
                        seq.AddEvent(new Pause(250));
                        seq.AddEvent(new DynamicString("Feeling refreshed and ready for anything, you continue on.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("The hall terminates to a stairwell. From here, it only goes up.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You ascend the stairs, finding entrances to each level you fell through when the floor collapsed.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You don't have time to explore every level", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString(", so you continue until you reach the top.", newLine: true, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("The stairs finish and reveal a grand opening to the an outside area of the castles roof.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("On the other side stands a shadowy figure.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString("You recognise the silhouette from your dream.", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString(". . . ", clearScreen: true, delay: 500, delayPerChar: true));
                        seq.AddEvent(new DynamicString("\"Welcome . . \"", delay: 45, delayPerChar: true));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString("\"I've been waiting for you.\"", clearScreen: true, delay: 350));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString("\"You're here to play a game.", newLine: false, clearScreen: true, delayPerChar: true, delay: 65));
                        seq.AddEvent(new Pause(750));
                        seq.AddEvent(new DynamicString(" So let's play.\"", newLine: true, delayPerChar: true, delay: 65));
                        seq.AddEvent(new PressContinue());

                        // Boss fight
                        funky = delegate ()
                        {
                            List<Character> narratorBoss = new List<Character>();
                            narratorBoss.Add(new Character("Shadowy Figure", 250, 10, new Weapon("Moon Blade", 30, 0)));
                            Team bossTeam = new Team(narratorBoss, false);
                            GameManager.getInstance().boss = bossTeam;
                            Battle battle = new Battle(GameManager.getInstance().soloTeam, bossTeam);

                            // Post Boss Fight
                            if (battle.DoBattle())
                            {
                                seq = StoryManager.GetStorySequence(StorySequence.BossFightWin);
                            }
                            else seq = StoryManager.GetStorySequence(StorySequence.BossFightPart1Lose);
                            GameManager.getInstance().mainSequence.AddSequence(seq);
                        };
                        seq.AddEvent(new Function(funky));

                        break;
                    }
                case StorySequence.BossFightPart1Lose:
                    {
                        seq.InsertSequence(StoryManager.GetStorySequence(StorySequence.DeathGeneric));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString(". . .", newLine: true, clearScreen: false, delayPerChar: true, delay: 100));
                        seq.AddEvent(new Pause(1000));
                        seq.AddEvent(new DynamicString(". . .", newLine: true, clearScreen: false, delayPerChar: true, delay: 100));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("<< This isn't game over. >>", newLine: true, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(800));
                        funky = delegate ()
                        {
                            Console.Title = "RPG.debug.LiveEdit()";
                            Console.CursorVisible = true;
                        };
                        seq.AddEvent(new Function(funky));
                        seq.AddEvent(new DynamicString("GameManager.getInstance().activeTeam = playerTeam;", newLine: true, clearScreen: true, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(800));
                        seq.AddEvent(new DynamicString("foreach (Character ch in playerTeam)", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(800));
                        seq.AddEvent(new DynamicString("{", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(100));
                        seq.AddEvent(new DynamicString("\tch.RefreshStatus();", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(100));
                        seq.AddEvent(new DynamicString("}", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(400));
                        seq.AddEvent(new DynamicString("", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(400));
                        seq.AddEvent(new DynamicString("bossTeam.ShowRealNames();", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(400));
                        seq.AddEvent(new DynamicString("bossTeam.Member(0).Defence = 0.0f;", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(400));
                        seq.AddEvent(new DynamicString("bossTeam.Member(0).Weapon.Name = \"Wet Noodle\";", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(400));
                        seq.AddEvent(new DynamicString("bossTeam.Member(0).Weapon.AttackRating = 0.0f;", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(800));
                        seq.AddEvent(new DynamicString("Battle battle = new Battle(GameManager.getInstance().activeTeam, bossTeam);", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new Pause(800));
                        seq.AddEvent(new DynamicString("battle.DoBattle();", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        seq.AddEvent(new DynamicString("", newLine: true, clearScreen: false, delayPerChar: true, delay: 15));
                        funky = delegate ()
                        {
                            Console.CursorVisible = false;
                        };
                        seq.AddEvent(new Function(funky));
                        seq.AddEvent(new Pause(1000));
                        funkyChoose = new STFunc[1];
                        funkyChoose[0] = delegate ()
                        {
                            Console.Title = "RPG.modified<";
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.BossFightPart2));
                        };
                        seq.AddEvent(new Choose(new string[1] { "Execute" +
                            "" }, funkyChoose));
                        break;
                    }
                case StorySequence.BossFightPart2:
                    {
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("What.. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 65));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString(" What are you doing??", newLine: false, clearScreen: false, delayPerChar: true, delay: 65));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("You can't do tha-", newLine: false, clearScreen: true, delayPerChar: true, delay: 65));
                        seq.AddEvent(new Pause(500));
                        funky = delegate ()
                        {
                            GameManager.getInstance().soloTeam.Member(0).RefreshStatus();

                            Team bossTeam = GameManager.getInstance().boss;
                            bossTeam.Member(0).Name = "The Narrator";
                            bossTeam.Member(0).Defence = 0.0f;
                            bossTeam.Member(0).Weapon.Name = "Wet Noodle";
                            bossTeam.Member(0).Weapon.AttackRating = 0.0f;


                            Battle battle = new Battle(GameManager.getInstance().playerTeam, bossTeam);

                            // Post Boss Fight
                            if (battle.DoBattle())
                            {
                                seq = StoryManager.GetStorySequence(StorySequence.BossFightWin);
                            }
                            else seq = StoryManager.GetStorySequence(StorySequence.BossFightPart1Lose);
                            GameManager.getInstance().mainSequence.AddSequence(seq);
                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
                case StorySequence.BossFightWin:
                    {
                        seq.AddEvent(new Pause(1000));
                        funky = delegate ()
                        {
                            int time = 2000;
                            while(!(Console.CursorTop == 0))
                            {
                                Thread.Sleep(time);
                                if (time > 500) time -= 500;
                                Console.MoveBufferArea(0, 1, Console.BufferWidth, Console.CursorTop, 0, 0);
                                Console.CursorTop -= 1;
                            }
                        };
                        seq.AddEvent(new Function(funky));
                        funky = delegate ()
                        {
                            Thread.Sleep(5000);
                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
                case StorySequence.DeathGeneric:
                    {
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("You've failed. ", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(300));
                        seq.AddEvent(new DynamicString("Too bad.", newLine: false, clearScreen: false, delayPerChar: true, delay: 45));
                        seq.AddEvent(new Pause(500));
                        seq.AddEvent(new DynamicString("Perhaps the next lot will be worthy.", newLine: false, clearScreen: true, delayPerChar: true, delay: 45));
                        break;
                    }
                case StorySequence.DevTest:
                    {
                        // Make Team
                        funky = delegate ()
                        {
                            GameManager.getInstance().playerTeam = new Team(3);
                        };
                        seq.AddEvent(new Function(funky));

                        funky = delegate ()
                        {
                            Sequence armourUp = new Sequence();
                            foreach (Character ch in GameManager.getInstance().playerTeam)
                            {
                                ch.Weapon.Upgrade();

                                // Multiply defense
                                ch.Defence *= 3;
                            }
                            GameManager.getInstance().mainSequence.InsertSequence(armourUp);
                        };
                        seq.AddEvent(new Function(funky));

                        // Choose Solo character
                        funkyChoose = new STFunc[3];
                        funkyChoose[0] = delegate ()
                        {
                            List<Character> solo = new List<Character>();
                            solo.Add(GameManager.getInstance().playerTeam.Member(0));
                            GameManager.getInstance().soloTeam = new Team(solo, true);
                        };

                        funkyChoose[1] = delegate ()
                        {
                            List<Character> solo = new List<Character>();
                            solo.Add(GameManager.getInstance().playerTeam.Member(1));
                            GameManager.getInstance().soloTeam = new Team(solo, true);
                        };

                        funkyChoose[2] = delegate ()
                        {
                            List<Character> solo = new List<Character>();
                            solo.Add(GameManager.getInstance().playerTeam.Member(2));
                            GameManager.getInstance().soloTeam = new Team(solo, true);
                        };
                        // string[] characters = GameManager.getInstance().playerTeam.Names();
                        seq.AddEvent(new Choose(new string[3] {"Char 1", "Char 2", "Char 3"}, funkyChoose));

                        // Start Sequence
                        funky = delegate ()
                        {
                            GameManager.getInstance().mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.SoloCastleTop));
                        };
                        seq.AddEvent(new Function(funky));
                        break;
                    }
            }
            return seq;
        }
    }

    
}

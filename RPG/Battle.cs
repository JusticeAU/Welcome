using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using StringTheory;

namespace RPG
{
    public class Battle
    {
        private Team activeTeam;
        private Team inactiveTeam;
        private BattleData activeTeamData;
        private BattleData inactiveTeamData;

        int textDelay = 15;


        public Battle(Team playerTeam, Team enemyTeam)
        {
            this.activeTeam = playerTeam;
            this.inactiveTeam = enemyTeam;

            this.activeTeamData = new BattleData(this.activeTeam);
            this.inactiveTeamData = new BattleData(this.inactiveTeam);
        }

        public bool DoBattle()
        {
            bool gameover = false;
            Introduction(inactiveTeam);

            while (!gameover)
            {
                Console.Clear();
                // Announce who's turn it is.
                Sequence sec = new Sequence();

                if (!activeTeam.IsAI)   sec.AddEvent(new DynamicString("Your turn.", delay: 45, delayPerChar: true));
                else sec.AddEvent(new DynamicString("My turn.", delay: 45, delayPerChar: true));

                sec.AddEvent(new Pause(500));
                sec.StartSequence();

                // Get actions for each team member
                int[] targets = new int[activeTeam.Size];
                for (int i = 0; i < activeTeam.Size; i++)
                {
                    Character currentChararacter = activeTeam.Member(i);
                    if (currentChararacter.IsAlive)
                    {
                        if (activeTeamData.CanAttack(i))
                        {
                            new DynamicString($"{currentChararacter.Name} is ready.", delay: textDelay, delayPerChar: true).Run();
                            new Pause(150).Run();
                            if (!activeTeam.IsAI)
                            {
                                targets[i] = UI.Choice($"Who will {currentChararacter.Name} attack?", inactiveTeam.GetChoicesTargetable());
                                Character targetCharacter = inactiveTeam.Member(targets[i]);
                                new DynamicString($"{currentChararacter.Name} turns to face {targetCharacter.Name}.", delay: textDelay, delayPerChar: true).Run();
                                new Pause(500).Run();
                            }
                            else
                            {
                                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                                targets[i] = rnd.Next(inactiveTeam.Size);
                                Character targetCharacter = inactiveTeam.Member(targets[i]);
                                new DynamicString($"{currentChararacter.Name} turns to face {targetCharacter.Name}.", delay: textDelay, delayPerChar: true).Run();
                                new Pause(500, skippable: true).Run();

                                //Pause on last one.
                                //if (i == activeTeam.Size-1) new PressContinue().Run();
                            }

                        }
                        else
                        {
                            new DynamicString($"{currentChararacter.Name} is recovering.", delay: textDelay, delayPerChar: true).Run();
                            new Pause(500, skippable:true).Run();
                            activeTeamData.Rest(i);
                            targets[i] = -1;
                        }
                    }
                }
                new PressContinue().Run();

                // Have each team member perform actions
                for (int i = 0; i < activeTeam.Size; i++)
                {
                    Character currentChararacter = activeTeam.Member(i);
                    if (currentChararacter.IsAlive)
                    {
                        if(targets[i] != -1)
                        { 
                            Character targetCharacter = inactiveTeam.Member(targets[i]);
                            PerformAttack(currentChararacter, targetCharacter);

                            // apply recovery data TODO move in to apply damage
                            activeTeamData.SetRecovery(i, currentChararacter.Weapon.Recovery);
                        }
                    }
                }
                if (!(inactiveTeam.Member(0).Name == "The Narrator" && !inactiveTeam.Member(0).IsAlive))
                    new PressContinue().Run();

                if (inactiveTeam.IsAlive)
                {
                    EndRoundCleanup();
                }
                else
                {
                    if(activeTeam.IsAI)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        private void PerformAttack(Character attacker, Character defender)
        {
            if (defender.IsAlive)
            {
                // Apply damage
                float damage = attacker.ComputeBaseDamage();
                new DynamicString($"{attacker.Name} used their {attacker.Weapon.name} to inflict {damage} damage on {defender.Name}.", delay: textDelay, delayPerChar: true).Run();
                new Pause(150, skippable: true).Run();
                defender.ApplyBaseDamage(damage);

                // Check status
                if (!defender.IsAlive)
                {
                    if(defender.Name == "The Narrator") new DynamicString($"{defender.Name} collap-", delay: textDelay, delayPerChar: true).Run();
                    else new DynamicString($"{defender.Name} collapsed.", delay: textDelay, delayPerChar: true).Run();
                    new Pause(250, skippable: true).Run();
                }
                else
                {
                    if(damage > 0)
                    {
                        new DynamicString($"{defender.Name}s defence absorbed {defender.Defence} damage.", delay: textDelay, delayPerChar: true).Run();
                        new Pause(250, skippable: true).Run();
                    }
                    
                    new DynamicString($"{defender.Name} has {defender.HP} HP remaining.", delay: textDelay, delayPerChar: true).Run();
                    new Pause(250, skippable: true).Run();
                }
            }
            else
            {
                if (defender.Name != "The Narrator")
                    new DynamicString($"{attacker.Name} kicks the lifeless body of {defender.Name}.", delay: textDelay, delayPerChar: true).Run();
            }

            // Pause
            new Pause(450).Run();
        }

        private void Introduction(Team enemyTeam)
        {
            Console.Clear();
            new Pause(500).Run();
            new DynamicString("You are stopped.", delay: 45, delayPerChar: true).Run();
            new Pause(500).Run();
            new DynamicString("Standing before you:", delay: 45, delayPerChar: true).Run();
            new Pause(500).Run();
            string[] people = enemyTeam.Names(true);
            for (int i = 0; i < people.Length; i++)
            {
                new DynamicString($"\t{people[i]}.", delay: textDelay, delayPerChar: true).Run();
                new Pause(100).Run();
            }
            Console.WriteLine("");
            new Pause(500).Run();
            new PressContinue().Run();
        }

        private void EndRoundCleanup()
        {
            // Tick battle data down one
            //activeTeamData.Update();
            // Swap teams
            Team temp = activeTeam;
            activeTeam = inactiveTeam;
            inactiveTeam = temp;

            // Swap battle data
            BattleData tempData = activeTeamData;
            activeTeamData = inactiveTeamData;
            inactiveTeamData = tempData;
        }
    }
}

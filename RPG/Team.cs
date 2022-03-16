using System;
using System.Collections;
using System.Collections.Generic;


namespace RPG
{
    public class Team : IEnumerable<Character>
    {
        private List<Character> members = new List<Character>();
        private bool isAI = true;

        public Team(int size, bool playerTeam = true)
        {
            // Clear input buffer
            while (Console.KeyAvailable)
                Console.ReadKey(true);

            for (int i = 0; i < size; i++)
            {
                if (playerTeam)
                {
                    Console.WriteLine("Create Character " + (i+1));
                    Console.Write("Name: ");
                    members.Add(new Character(Console.ReadLine()));
                }
                else
                {
                    members.Add(new Character());
                }
            }

            if (playerTeam) isAI = false;
        }

        public Team(List<Character> members, bool playerTeam = true)
        {
            this.members = members;
            this.isAI = !playerTeam;
        }

        public bool IsAlive
        {
            get
            {
                foreach (Character member in members)
                {
                    if (member.IsAlive) return true;
                }
                return false;
            }
        }

        public bool IsAI
        {
            get
            {
                return isAI;
            }
        }

        public int Size
        {
            get
            {
                return members.Count;
            }
        }

        public Character Member(int index)
        {
            return members[index];
        }

        public string[] Names(bool appendHP = false)
        {
            string[] names = new string[this.Size];
            for (int i = 0; i < this.Size; i++)
            {
                if (appendHP)
                {
                    names[i] = $"{members[i].Name}, with {members[i].HP} HP";
                }
                else
                {
                    names[i] = members[i].Name;
                }
            }
            return names;
        }

        public ChoiceData GetChoicesTargetable()
        {
            int size = members.Count;
            string[] options = new string[size];
            bool[] selectable = new bool[size];

            for (int i = 0; i < this.Size; i++)
            {
                options[i] = $"{members[i].Name}, with {members[i].HP} HP";
                selectable[i] = members[i].IsAlive;
            }

            return new ChoiceData(options, selectable);
        }

        // Basic Enumerator implementation.
        public IEnumerator<Character> GetEnumerator()
        {
            foreach (Character person in members)
            {
                yield return person;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

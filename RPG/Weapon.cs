using System;

namespace RPG
{
    public class Weapon
    {
        public string name;
        private float baseAttackRating;
        private int recovery;
        public string Name
        { 
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public float AttackRating
        {
            get
            {
                return baseAttackRating;
            }
            set
            {
                baseAttackRating = value;
            }
        }
        public int Recovery
        {
            get
            {
                return recovery;
            }
        }
        public Weapon()
        {
            string[] names =
            {
                "Great Sword",
                "Long Sword",
                "Knife",
                "Flimsy Stick",
                "Non-Copywrite Infringing Laser-like Sword"
            };

            int[] damageValues =
            {
                15,
                12,
                10,
                5,
                15
            };

            int[] recoveryTurns =
            {
                3,
                2,
                1,
                0,
                3
            };

            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int weaponIndex = rnd.Next(names.Length);
            name = names[weaponIndex];
            baseAttackRating = damageValues[weaponIndex];
            recovery = recoveryTurns[weaponIndex];
        }

        public Weapon(string name, float damage, int recovery)
        {
            this.name = name;
            this.baseAttackRating = damage;
            this.recovery = recovery;
        }

        public void Upgrade()
        {

            if (recovery > 0)
            {
                recovery -= 1;
                baseAttackRating *= 2;
            }
            else
            {
                baseAttackRating *= 3;
            }
        }
    }
}

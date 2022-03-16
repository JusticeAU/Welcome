using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Character
    {
        private string name;
        private float hitPoints;
        private float maxHitPoints;
        private float defenseRating;
        private Weapon weapon;

        public Character()
        {
            // This method of constructions is no longer used in the game. All names are specified.
            string[] names =
            {
                "Rogue",
                "Thief",
                "Generally unpleasant person"
            };

            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            name = names[rnd.Next(names.Length)];
            InitiliseBaseStats();
        }

        public Character(string name)
        {
            this.name = name;
            InitiliseBaseStats();
        }

        public Character(string name, float hp, float def, Weapon weapon)
        {
            this.name = name;
            this.maxHitPoints = hp;
            hitPoints = maxHitPoints;
            this.defenseRating = def;
            this.weapon = weapon;
        }

        private void InitiliseBaseStats()
        {
            // This method is called by most of the constructors and provide some default values.
            this.maxHitPoints = 50;
            this.hitPoints = maxHitPoints;
            this.defenseRating = 3f;
            this.weapon = new Weapon();
        }

        public float ComputeBaseDamage()
        {
            if (weapon == null) return 0;
            else return weapon.AttackRating;
        }

        public void ApplyBaseDamage(float dmg)
        {
            hitPoints -= Math.Max((dmg - defenseRating), 0);
            hitPoints = Math.Max(hitPoints, 0);
        }
        public void RefreshStatus()
        {
            hitPoints = maxHitPoints;
        }

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

        public float HP
        {
            get
            {
                return hitPoints;
            }
        }

        public float Defence
        {
            get
            {
                return defenseRating;
            }
            set
            {
                defenseRating = value;
            }
        }

        public Weapon Weapon
        {
            get
            {
                return weapon;
            }
        }

        public bool IsAlive
        {
            get
            {
                return hitPoints > 0;
            }
        }
    }
}

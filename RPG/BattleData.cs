using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class BattleData
    {
        private int[] recoveryTurns;

        public BattleData(Team team)
        {
            this.recoveryTurns = new int[team.Size];
        }

        public void Update()
        {
            // tick all recovery turns down
            for (int i = 0; i < recoveryTurns.Length; i++)
            {
                
            }
        }

        public bool CanAttack(int memberNumber)
        {
            return (recoveryTurns[memberNumber] == 0);
        }

        public void SetRecovery(int memberNumber, int turns)
        {
            recoveryTurns[memberNumber] = turns;
        }

        public void Rest(int memberNumber)
        {
            recoveryTurns[memberNumber] -= 1;
        }
    }
}

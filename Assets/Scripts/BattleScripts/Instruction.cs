
using System.Collections.Generic;
using BattleScripts.Actions;

namespace BattleScripts
{
    public class Instruction
    {
        public List<IAction> actions { get; set; }

        public Instruction()
        {
            actions = new List<IAction>();
        }
    }
}

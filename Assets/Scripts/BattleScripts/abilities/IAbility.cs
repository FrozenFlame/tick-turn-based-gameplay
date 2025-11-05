using BattleScripts;
using BattleScripts.Actions;

namespace BattleScripts.Abilities
{
    public interface IAbility
    {
        // stubs
        Instruction BuildInstruction(Character source, TargetingContext context);
        string name { get; }
        string display_name { get; }
    }
}

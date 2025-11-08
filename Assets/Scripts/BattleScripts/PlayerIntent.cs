
using UnityEngine;
using BattleScripts.Abilities;

namespace BattleScripts
{
    public class PlayerIntent
    {
        BattleContext battle_context;
        private IAbility selected_ability_;
        private Character selected_target_;

        public PlayerIntent(BattleContext context)
        {
            battle_context = context;
            selected_ability_ = null;
            selected_target_ = null;
        }

        public void SetSelectedAbility(IAbility ability)
        {
            selected_ability_ = ability;
        }
        public void SetSelectedTarget(Character target)
        {
            selected_target_ = target;
        }

        public Instruction BuildInstruction()
        {
            if (battle_context == null || selected_ability_ == null)
            {
                Debug.Log("Missing context or ability!");
                return null;
            }

            Character source = battle_context.active_character;

            if (source == null)
            {
                Debug.Log("Active character is null!");
                return null;
            }

            source.SetTargetCharacter(selected_target_);
            Instruction instruction = selected_ability_.BuildInstruction(source, battle_context);

            return instruction;
        }
    }
}

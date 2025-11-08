using BattleScripts.Actions;
using BattleScripts.Enums;

namespace BattleScripts.Abilities
{
    public class NormalAttackAbility : IAbility
    {
        public string name => "attack";
        public string display_name => "Attack";

        // public string description?
        // public string image_url?

        public Instruction BuildInstruction(Character source, BattleContext context)
        {
            Instruction instruction = new Instruction();
            Character target = source.target_character;

            DamageTypeEnum damage_type = (source.role != CharacterRoleEnum.MAGE)
                ? DamageTypeEnum.PHYSICAL
                : DamageTypeEnum.MAGICAL;

            SingleTargetAttackAction action = new SingleTargetAttackAction(
                source,
                target,
                damage_type
            );
            /**
             *  can do stuff like this maybe (?) or maybe this is the wrong implementation, anyways.
             *  if (has_coat_poison)
             *  actions.Add(new PoisonDebuffAction());
             *  yeah this is probably wrong, this should be done outside of this BuildInstruction
             *  
             *  Like instruction.actions.Add(external_attack_buff_source_action); // done outside this class
             */
            instruction.actions.Add(action);
            return instruction;
        }
    }
}

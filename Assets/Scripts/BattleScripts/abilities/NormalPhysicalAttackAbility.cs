using BattleScripts.Actions;
using BattleScripts.Enums;

namespace BattleScripts.Abilities
{
    public class NormalPhysicalAttackAbility : IAbility
    {
        public string name => "physical_attack";
        public string display_name => "Attack";

        // public string description?
        // public string image_url?

        public Instruction BuildInstruction(Character source, BattleContext context)
        {
            Instruction instruction = new Instruction();
            Character target = context.GetSelectedTarget();

            SingleTargetAttackAction action = new SingleTargetAttackAction(
                source,
                target,
                DamageTypeEnum.PHYSICAL
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

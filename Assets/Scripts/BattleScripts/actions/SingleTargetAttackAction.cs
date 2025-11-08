using UnityEngine;
using BattleScripts.Enums;

namespace BattleScripts.Actions
{
    public class SingleTargetAttackAction : IAction
    {
        Character attacker;
        Character target;
        DamageTypeEnum damage_type;

        public SingleTargetAttackAction(Character attacker, Character target, DamageTypeEnum damage_type)
        {
            this.attacker = attacker;
            this.target = target;
            this.damage_type = damage_type;
        }

        public void PerformAction(BattleContext context = null)
        {
            float outgoing_attack_damage = 0f;
            float defense = 0f;
            // take note of:
            // can do other stuff like crit and etc.
            // can do other stuff like damage mitigation evasion and etc.
            switch (damage_type)
            {
                case DamageTypeEnum.PHYSICAL:
                    // simple implementation for now
                    outgoing_attack_damage = attacker.physical_attack_base + attacker.physical_attack_modifier;
                    defense = target.physical_defense_base + target.physical_defense_modifier;
                    break;
                case DamageTypeEnum.MAGICAL:
                    outgoing_attack_damage = attacker.magical_attack_base + attacker.magical_attack_modifier;
                    defense = target.magical_defense_base + target.magical_defense_modifier;
                    break;
                default:
                    Debug.LogError("Unknown damage type");
                    break;
            }

            float net_damage = outgoing_attack_damage - defense;
            target.TakeDamage(net_damage);
            string type_str = damage_type == DamageTypeEnum.PHYSICAL ? "physical damage" : "magical damage";
            Debug.Log($"Attacker damage: {outgoing_attack_damage} ({type_str}); Target defense: {defense}");
            Debug.Log($"{attacker.char_name} dealt ({net_damage}) {type_str} to {target.char_name}!");
            Debug.Log(
                $"{target.char_name} health status: {target.GetEffectiveHealth()}/{target.GetEffectiveMaxHealth()}"
            );
        }
    }
}

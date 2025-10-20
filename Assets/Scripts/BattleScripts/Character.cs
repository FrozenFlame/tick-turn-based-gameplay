using UnityEngine;

public class Character : MonoBehaviour
{
    float health_base;
    float health_modifier;
    float health_max_base;
    float health_max_modifier;

    float mana_base;
    float mana_modifier;
    float mana_max_base;
    float mana_max_modifier;

    float speed_base;
    float speed_modifier;
    float speed_max_base;
    float speed_max_modifier;

    float action_points_base;
    float action_points_modifier;

    float readiness_threshold_base;
    float readiness_threshold_modifier;

    // stubs
    // Effect[] active_effects;
    // questions
    // - what other stats should a character have? besides the ones above
    //   strength, intelligence, agility?

    void Start()
    {

    }

    void Update()
    {

    }

    void BattleStart()
    {

    }

    void OnTick()
    {

    }

    void Ready()
    {

    }

    /***
     * Health Functions
     **/

    float GetEffectiveHealth()
    {
        return health_base + health_modifier;
    }

    void TakeDamage(float damage)
    {
        // stub logic
        health_modifier -= damage;
    }

    /***
     * Mana Functions
     **/

    void AddMana(float value)
    {
        // stub logic
        mana_modifier += value;
    }

    /***
     * Speed Functions
     **/


    /***
     * Actions Functions
     **/
    void Attack(Character character)
    {
        // stub ideas for now
        // hard coded damage for now
        character.TakeDamage(5);
    }

    // void CastSpell(Spell spell)

}

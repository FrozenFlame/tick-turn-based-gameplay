using UnityEngine;

public class Character : MonoBehaviour
{
    private string char_name_;
    public string char_name
    {
        get {  return char_name_; }
        set { char_name_ = value; }
    }


    private float health_base_;
    public float health_base
    {
        get { return health_base_; }
        // example benefit of adding get/set handling. TODO on other ones later on.
        set
        {
            if (value > 0) // ensures that the assigned health_base value is at least greater than 0
            {
                health_base_ = value;
            }
            else
            {
                health_base_ = 1;
            }
        }
    }
    public float health_modifier;
    private float health_max_base_;
    public float health_max_base
    {
        get { return health_max_base_; }
        set { health_max_base_ = value; }
    }
    public float health_max_modifier;


    private float mana_base_;
    public float mana_base
    {
        get { return mana_base_; }
        set { mana_base_ = value; }
    }
    public float mana_modifier;
    private float mana_max_base_;
    public float mana_max_base
    {
        get { return mana_max_base_; }
        set { mana_max_base_ = value; }
    }
    public float mana_max_modifier;


    private float speed_base_;
    public float speed_base
    {
        get { return speed_base_; }
        set { speed_base_ = value; }
    }
    public float speed_modifier;
    private float speed_max_base_;
    public float speed_max_base
    {
        get { return speed_max_base_; }
        set { speed_max_base_ = value; }
    }
    public float speed_max_modifier;


    private float action_points_base_;
    public float action_points_base
    {
        get { return action_points_base_; }
        set { action_points_base_ = value; }
    }
    public float action_points_modifier;


    private float readiness_threshold_base_;
    public float readiness_threshold_base
    {
        get { return readiness_threshold_base_; }
        set { readiness_threshold_base_ = value; }
    }
    public float readiness_threshold_modifier;

    private float physical_attack_base_;
    public float physical_attack_base
    {
        get { return physical_attack_base_; }
        set { physical_attack_base_ = value; }
    }
    public float physical_attack_modifier;


    private float physical_defense_base_;
    public float physical_defense_base
    {
        get { return physical_defense_base_; }
        set { physical_defense_base_ = value; }
    }
    public float physical_defense_modifier;


    private float magical_attack_base_;
    public float magical_attack_base
    {
        get { return magical_attack_base_; }
        set { magical_attack_base_ = value; }
    }
    public float magical_attack_modifier;


    private float magical_defense_base_;
    public float magical_defense_base
    {
        get { return magical_defense_base_; }
        set { magical_defense_base_ = value; }
    }
    public float magical_defense_modifier;


    // stubs
    // Effect[] active_effects;
    // Equipment[] equipment; (usually up to two only);

    void Start()
    {
        Debug.Log("I have been instantiated");
    }

    void OnDestroy()
    {
        Debug.Log("I have been destroyed");    
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
        return health_base_ + health_modifier;
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

    // void CastSpell(Spell spell) stub
}

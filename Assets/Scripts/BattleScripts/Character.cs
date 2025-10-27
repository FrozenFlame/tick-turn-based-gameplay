using UnityEngine;

public class Character : MonoBehaviour
{
    private string char_name_;
    public string char_name
    {
        get { return char_name_; }
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
    // State state -- state of some sort maybe like: waiting, ready, skipped, finished, etc. could be useful for other logic stuff.

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

    public void OnTick()
    {
        if (!IsReady())
        {
            TickActionPoints();
            Debug.Log("My action points ticked");
            Debug.Log(GetAccumulatedActionPoints());
        }
        else
        {
            // do whatever.
            Debug.Log("I AM READY - " + char_name);
        }
    }

    void Ready()
    {

    }

    /***
     * Health Functions
     **/

    float GetEffectiveHealth()
    {
        float net_health = health_base + health_modifier;
        // can do other stuff...
        return net_health;
    }

    void TakeDamage(float damage)
    {
        float net_damage = damage;
        // can do other stuff...
        health_modifier -= net_damage;
    }

    /***
     * Mana Functions
     **/

    void ModifyMana(float value)
    {
        float net_addition = value;
        // can do other things...
        mana_modifier += net_addition;
    }

    /***
     * Speed Functions
     **/
    float GetEffectiveSpeed()
    {
        float net_speed = speed_base + speed_modifier;
        // can do other stuff...
        return net_speed;
    }

    void TickActionPoints()
    {
        float added_points = GetEffectiveSpeed();
        // can do other stuff...
        action_points_modifier += added_points;
    }

    float GetAccumulatedActionPoints()
    {
        float net_action_points = action_points_base + action_points_modifier;
        // can do other stuff
        return net_action_points;
    }

    bool IsReady()
    {
        Debug.Log("GetAccumulatedActionPoints()");
        Debug.Log(GetAccumulatedActionPoints());
        Debug.Log("GetEffectiveReadinessThreshold()");
        Debug.Log(GetEffectiveReadinessThreshold());
        bool is_ready = GetAccumulatedActionPoints() >= GetEffectiveReadinessThreshold();
        // can do other stuff...
        return is_ready;
    }

    float GetEffectiveReadinessThreshold()
    {
        float net_threshold = readiness_threshold_base + readiness_threshold_modifier;
        // can do other things...
        return net_threshold;
    }


    /***
     * Actions Functions
     **/
    void Attack(Character character)
    {
        // hard coded damage for now
        character.TakeDamage(5);
    }

    // void CastSkill(Skill skill) stub
}

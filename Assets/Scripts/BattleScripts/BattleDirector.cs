using System.Collections.Generic;
using UnityEngine;

public class BattleDirector : MonoBehaviour
{
    // stubs
    // enum current_battle_state;
    List<Character> characters;
    // Map<Character[]> team_members; // stub idk if Map is the right tool for the job
    // Character[] ready_characters;
    // Character[] execution_queue;
    // int ticks;
    // bool should_execute_tick;
    // FieldEffect[] active_field_effects;
    // combat_log -- todo

    void Start()
    {
        Debug.Log("The BattleDirector");
        characters = new List<Character>();
        StartBattle();
    }

    void OnDestroy()
    {
        Debug.Log("BattleDirector Removed");
    }

    void Update()
    {
        
    }

    /***
     * Battle lifetime functions
     **/
    void StartBattle()
    {
        /*
         * TODO figure out way to specify what characters to spawn to which team
         */

        // temp for now, it will be hard-coded for this proof of concept
        DebugSimulateSummonWolves();
    }

    void EndBattle()
    {
        // some clean-up operations i guess
    }
    

    void CheckCondition()
    {

    }
    

    void ResolveBattle()
    {

    }

    /***
     * Battle flow functions
     **/
    //void ChangeState(enum new_state) stub

    void EmitOnTick()
    {
        // loop through `characters[]` to have them call their `OnTick()` functions
    }

    //void OnCharacterReady(Character character) stub
    void PrepareExecutionQueue()
    {

    }

    //void RemoveFromExecutionQueue(Character character) stub

    void EmptyQueues()
    {
        
    }


    /***
     * Debug and testing functions
     **/
    void DebugSimulateSummonWolves()
    {
        // add 1-3 wolf enemies; lazily for now since this is just debug code.
        List<GameObject> wolves = new List<GameObject>();
        int wolf_count = Random.Range(1, 4);
        // debug hard-coded enemy positions for now
        float enemy_position_y = 4;
        for (int i = 0; i < wolf_count; i++)
        {
            GameObject wolf_gameobject = DebugCreateWolf(i);
            float enemy_position_x = 2 + (i * 2);
            wolf_gameobject.transform.position = new Vector2(enemy_position_x, enemy_position_y);
            Character wolf_character = DebugAttachWolfCharacter(wolf_gameobject);
            characters.Add(wolf_character);
        }

        if (wolf_count > 0 && characters.Count > 0)
        {
            Debug.Log("Enemy Wolf Characters initialized");
            Debug.Log("number of wolves: " + wolf_count);
        }
    }

    GameObject DebugCreateWolf(int number)
    {
        GameObject wolf = new GameObject("wolf"+number);
        wolf.AddComponent<SpriteRenderer>();
        wolf.AddComponent<Rigidbody2D>();
        wolf.AddComponent<CapsuleCollider2D>();
        // TODO UI clickable stuff, idk.

        wolf.GetComponent<Rigidbody2D>().gravityScale = 0;

        // just a red capsule
        Color enemy_color = new Color(255, 43, 35);
        Sprite wolf_sprite = Resources.Load<Sprite>("Sprites/wolf");

        SpriteRenderer wolf_sprite_renderer = wolf.GetComponent<SpriteRenderer>();

        if (wolf_sprite != null)
        {
            Debug.Log("Sprite found");
            wolf_sprite_renderer.sprite = wolf_sprite;
            wolf_sprite_renderer.color = enemy_color;
        }
        else
        {
            Debug.Log("Sprite not found");
        }
        return wolf;
    }

    Character DebugAttachWolfCharacter(GameObject wolf)
    {
        // hard-coded stats for now
        wolf.AddComponent<Character>();
        Character wolf_character = wolf.GetComponent<Character>();
        wolf_character.char_name = wolf.name;
        wolf_character.health_base = 30;
        wolf_character.physical_attack_base = 5;
        wolf_character.magical_attack_base = 0;
        wolf_character.physical_defense_base = 2;
        wolf_character.magical_defense_base = 0;

        return wolf_character;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleScripts;
using BattleScripts.Enums;

public class BattleDirector : MonoBehaviour
{
    // stubs
    // enum current_battle_state;
    List<Character> characters;
    List<Character> ready_characters;
    Queue<Character> execution_queue;
    BattleContext battle_context;
    ControlContextEnum control_context;
    BattleStateEnum battle_state;

    int ticks;
    bool should_execute_tick;
    // FieldEffect[] active_field_effects;
    // combat_log -- todo

    event System.Action emit_instruction_issued; // placeholder

    [SerializeField] private GameObject action_panel_ui;

    void Start()
    {
        Debug.Log("The BattleDirector");
        // TODO: figure out how to get/communicate stuff from the overworld scene
        // Init() function of sorts
        // action_panel_ui // todo: hide it
        battle_context = new BattleContext();
        ticks = 0;
        should_execute_tick = false;
        ChangeControlContext(ControlContextEnum.WAITING);
        characters = new List<Character>();
        ready_characters = new List<Character>();
        execution_queue = new Queue<Character>();
        StartBattle();
    }

    void OnDestroy()
    {
        Debug.Log("BattleDirector Removed");
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
        DebugCreateHeroes(2);
        DebugSimulateSummonWolves();

        characters.ForEach(c => c.emit_character_ready += ListenOnCharacterReady);

        // note: probably we need to remove the listeners when the battle is resolved
        battle_context.SortRosters();
        ChangeBattleState(BattleStateEnum.WAITING);
        should_execute_tick = true;
        StartCoroutine(BattleLoop());
    }

    void EndBattle()
    {
        // some clean-up operations i guess
        characters.ForEach(c => c.emit_character_ready -= ListenOnCharacterReady);
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

    IEnumerator BattleLoop()
    {
        while (true) // TODO: change this to check current battle state for event/resolution handling
        {
            if (ready_characters.Count == 0)
            {
                yield return new WaitForSeconds(0.33f);
                OnTick();
            }
            else
            {
                PrepareExecutionQueue();
                while (execution_queue.Count > 0)
                {
                    Character next_character = PopFromExecutionQueue();
                    yield return StartCoroutine(HandleCharacterTurn(next_character));
                    Debug.Log("Finished handling turn for " + next_character.char_name);
                    emit_instruction_issued -= next_character.ListenForInstructions;
                }
                ready_characters.Clear();
            }
        }
    }

    void ChangeBattleState(BattleStateEnum new_state)
    {
        // can do stuff here when needed...
        battle_state = new_state;
    }

    void ChangeControlContext(ControlContextEnum new_context)
    {
        // can do stuff...
        control_context = new_context;
    }

    /***
     * Turn management functions
     **/

    void ListenOnCharacterReady(Character character)
    {
        if (!ready_characters.Contains(character)) ready_characters.Add(character);
    }
    

    void ListenCharacterTakeTurn(Character character)
    {
        Dictionary<string, Character> dict = new Dictionary<string, Character>();
        dict.Add(character.char_name, character);

        //dict["vov0"].health_modifier;

        /*
         
        Dictionary<string, CharacterUI> dict_ui = new Dictionary<string, CharacterUI>();
        dict_ui[character.char_name].ShowButton(true);
        */
    }

    IEnumerator HandleCharacterTurn(Character character)
    {
        // TODO: if player-controlled character:
        // reset action panel state, load up with character-specific info
        // show action panel
        emit_instruction_issued += character.ListenForInstructions;
        Debug.Log("Here is the position of the character: ");
        Transform character_transform = character.transform;
        RectTransform ui_rect_transform = action_panel_ui.GetComponent<RectTransform>();
        Debug.Log($"X:{character.transform.position.x}, Y: {character.transform.position.y}");
        Vector2 new_position = character_transform.position + new Vector3(0, 1.5f);
        Vector2 screen_position = Camera.main.WorldToScreenPoint(new_position);
        ui_rect_transform.position = screen_position;

        yield return character.TakeTurn();
    }

    void OnTick()
    {
        if (!should_execute_tick) return;

        ticks++;

        TickProcesses();
    }

    void TickProcesses()
    {
        if (characters != null) characters.ForEach(c => c.Tick());
    }

    void PrepareExecutionQueue()
    {
        Debug.Log("unsorted ready list:");
        ready_characters.ForEach(c => Debug.Log(c.char_name + " with speed " + c.GetEffectiveSpeed()));

        ready_characters.Sort((a, b) => b.GetEffectiveSpeed().CompareTo(a.GetEffectiveSpeed()));

        Debug.Log("sorted ready list:");
        ready_characters.ForEach(c => Debug.Log(c.char_name + " with speed " + c.GetEffectiveSpeed()));

        ready_characters.ForEach(c => PushIntoExecutionQueue(c));
    }

    Character PushIntoExecutionQueue(Character character)
    {
        execution_queue.Enqueue(character);
        // can do anything here.
        return character;
    }

    Character PopFromExecutionQueue()
    {
        Character character = execution_queue.Dequeue();
        // can do anything here.
        return character;
    }

    void EmptyQueues()
    {
        // stub
    }


    /***
     * Debug and testing functions. messy and unrefined
     **/
    void DebugSimulateSummonWolves()
    {
        // add 1-3 wolf enemies; lazily for now since this is just debug code.
        List<GameObject> wolves = new List<GameObject>();
        int wolf_count = Random.Range(1, 4);
        // debug hard-coded enemy positions for now
        for (int i = 0; i < wolf_count; i++)
        {
            GameObject wolf_gameobject = DebugCreateWolf(i);
            bool is_even = i % 2 == 0;
            float enemy_position_y = (is_even) ? 1.5f : 0;
            float enemy_position_x = (3 + (i * 2));

            wolf_gameobject.transform.position = new Vector2(enemy_position_x, enemy_position_y);
            Character wolf_character = DebugAttachWolfCharacter(wolf_gameobject);
            characters.Add(wolf_character);
            battle_context.AddCharacterToContext(wolf_character);
        }

        if (wolf_count > 0 && characters.Count > 0)
        {
            Debug.Log("Enemy Wolf Characters initialized");
            Debug.Log("number of wolves: " + wolf_count);
        }
    }

    GameObject DebugCreateWolf(int number)
    {
        GameObject wolf = new GameObject("wolf" + number);
        wolf.AddComponent<SpriteRenderer>();
        wolf.AddComponent<Rigidbody2D>();
        wolf.AddComponent<CapsuleCollider2D>();
        // TODO UI clickable stuff, idk.
        wolf.GetComponent<Rigidbody2D>().gravityScale = 0;

        // just a red capsule
        Sprite wolf_sprite = Resources.Load<Sprite>("Sprites/wolf");

        if (wolf_sprite != null)
        {
            Debug.Log("Sprite found");
            SpriteRenderer wolf_sprite_renderer = wolf.GetComponent<SpriteRenderer>();
            wolf_sprite_renderer.sprite = wolf_sprite;
            Color enemy_color = new Color(255, 43, 35);
            wolf_sprite_renderer.color = enemy_color;
        }
        else Debug.Log("Sprite not found");

        return wolf;
    }

    Character DebugAttachWolfCharacter(GameObject wolf)
    {
        // hard-coded stats for now
        wolf.AddComponent<Character>();
        Character wolf_character = wolf.GetComponent<Character>();
        wolf_character.char_name = wolf.name;
        wolf_character.health_base = 30;
        wolf_character.speed_base = 3;
        wolf_character.readiness_threshold_base = 100;
        wolf_character.physical_attack_base = 5;
        wolf_character.magical_attack_base = 0;
        wolf_character.physical_defense_base = 2;
        wolf_character.magical_defense_base = 0;
        wolf_character.faction = CharacterFactionEnum.ENEMY;

        return wolf_character;
    }

    void DebugCreateHeroes(int hero_count)
    {
        for (int i = 0; i < hero_count; i++)
        {
            GameObject hero_gameobject = DebugCreateHeroGameObject(i);
            int is_odd = i % 2;
            float hero_position_y = (is_odd == 0) ? 0 : - 1.5f;
            float hero_position_x = (4 + (i * 2)) * -1;

            hero_gameobject.transform.position = new Vector2(hero_position_x, hero_position_y);
            Character hero_character = DebugAttachHeroCharacter(hero_gameobject);
            characters.Add(hero_character);
            battle_context.AddCharacterToContext(hero_character);
        }
    }
    
    // writing this now, it's obvious I could make these functions more generic, lazy for now. -- also whoever is reading this, avoid making useless commentary like this. In more complete code.
    GameObject DebugCreateHeroGameObject(int number)
    {
        GameObject hero = new GameObject("hero" + number);


        hero.AddComponent<SpriteRenderer>();
        hero.AddComponent<Rigidbody2D>();
        hero.AddComponent<CapsuleCollider2D>();
        // TODO UI clickable stuff, idk.

        hero.GetComponent<Rigidbody2D>().gravityScale = 0;
        Sprite hero_sprite = Resources.Load<Sprite>("Sprites/hero");

        if (hero_sprite != null)
        {
            Debug.Log("Sprite found");
            SpriteRenderer hero_sprite_renderer = hero.GetComponent<SpriteRenderer>();
            hero_sprite_renderer.sprite = hero_sprite;
        }
        else Debug.Log("Sprite not found");

        return hero;
    }

    Character DebugAttachHeroCharacter(GameObject hero)
    {
        // hard-coded stats for now
        hero.AddComponent<Character>();
        Character hero_character = hero.GetComponent<Character>();
        hero_character.char_name = hero.name;
        hero_character.health_base = 30;
        hero_character.speed_base = 5;
        hero_character.readiness_threshold_base = 100;
        hero_character.physical_attack_base = 5;
        hero_character.magical_attack_base = 0;
        hero_character.physical_defense_base = 2;
        hero_character.magical_defense_base = 0;
        hero_character.faction = CharacterFactionEnum.FRIENDLY;

        return hero_character;
    }


    /***
     * Temporary exposed button function
     **/
    public void ClickedAttack()
    {
        Debug.Log("Attack button clicked");
        emit_instruction_issued?.Invoke();
    }
}


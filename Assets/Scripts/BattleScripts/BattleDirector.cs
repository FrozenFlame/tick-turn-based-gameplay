using UnityEngine;

public class BattleDirector : MonoBehaviour
{
    // stubs
    // enum current_battle_state;
    // Character[] characters;
    // Map<Character[]> team_members; // stub idk if Map is the right tool for the job
    // Character[] ready_characters;
    // Character[] execution_queue;
    // int ticks;
    // bool should_execute_tick;
    // FieldEffect[] active_field_effects;
    // combat_log -- todo

    void Start()
    {
        
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
}

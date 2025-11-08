using BattleScripts;
using UnityEngine;
using UnityEngine.EventSystems;

// stub stuff for now
public class SelectableTarget : MonoBehaviour, IPointerClickHandler
{
    private Character character_; // reference to your Character logic

    public void Initialize(Character character)
    {
        Debug.Log("SelectableTarget initialized for: " + character.char_name);
        character_ = character;
    }

    public void OnPointerClick(PointerEventData event_data)
    {
        Debug.Log("Clicked on: " + character_.char_name);
        if (BattleDirector.instance == null) Debug.Log("PlayerIntent instance is null");
        else BattleDirector.instance.ButtonTargetSelected(character_);
    }

    // Called by gamepad or keyboard confirmation input
    public void ConfirmSelection()
    {
        //PlayerIntent.Instance.SelectTarget(character);
    }
}
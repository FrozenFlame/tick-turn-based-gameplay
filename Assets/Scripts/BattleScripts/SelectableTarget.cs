using BattleScripts;
using UnityEngine;
using UnityEngine.EventSystems;

// stub stuff for now
public class SelectableTarget : MonoBehaviour, IPointerClickHandler
{
    public Character character; // reference to your Character logic

    public void OnPointerClick(PointerEventData event_data)
    {
        //PlayerIntent.Instance.SelectTarget(character);
    }

    // Called by gamepad or keyboard confirmation input
    public void ConfirmSelection()
    {
        //PlayerIntent.Instance.SelectTarget(character);
    }
}
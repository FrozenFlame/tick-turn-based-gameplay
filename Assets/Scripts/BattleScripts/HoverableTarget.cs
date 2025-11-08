using BattleScripts.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverableTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject hover_indicator_;
    private Sprite targeting_sprite_;

    public void Initialize(Character character)
    {
        targeting_sprite_ = Resources.Load<Sprite>("Sprites/chevron");
        hover_indicator_ = new GameObject($"{character.char_name}HoverIndicator");
        hover_indicator_.SetActive(false);
        hover_indicator_.transform.SetParent(character.transform);
        hover_indicator_.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        hover_indicator_.transform.position = character.transform.position + new Vector3(0f, 1.8f, 0f);

        SpriteRenderer sprite_renderer = hover_indicator_.AddComponent<SpriteRenderer>();
        sprite_renderer.sprite = targeting_sprite_;
        Debug.Log($"Initialized hover indicator for {character.char_name}");
    }
    public void OnPointerEnter(PointerEventData event_data)
    {
        Hover(true);
    }

    public void OnPointerExit(PointerEventData event_data)
    {
        Hover(false);
    }

    public void Hover(bool is_hovered)
    {
        ShowHoverIndicator(is_hovered);
    }

    public void ShowHoverIndicator(bool should_show)
    {
        if (
            hover_indicator_
            && BattleDirector.instance.control_context == ControlContextEnum.TARGETING
        )
        {
            hover_indicator_.SetActive(should_show);
        }
    }
}

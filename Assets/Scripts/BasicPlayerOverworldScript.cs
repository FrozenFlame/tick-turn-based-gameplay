using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPlayerOverworldScript : MonoBehaviour
{
    public Vector2 starting_position = new Vector2(-2.0f, 0.0f);

    void Start()
    {
        Debug.Log("start");
        transform.position = starting_position;
    }

    void Update()
    {
        float h_input = Input.GetAxisRaw("Horizontal");
        float v_input = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(h_input, v_input).normalized;

        transform.Translate(movement * 10 * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("you have collided");
            StartBattle();
        }
    }

    // for testing purposes, the character will simply induce the scene switch to battle.
    void StartBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }
}

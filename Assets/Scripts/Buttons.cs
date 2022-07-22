using System;
using UnityEngine;

public class Buttons : AbstractInput
{
    [SerializeField] private CharacterMover character;

    void OnGUI()
    {
        if (Check)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Point = character.gameObject.transform.position.x - 2.0f;
                GameEvents.CharacterMove(Point);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Point = character.gameObject.transform.position.x + 2.0f;
                GameEvents.CharacterMove(Point);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.onInputChanged.AddListener(addListener);
    }
    public override void GetNewTargetPosition(float pos)
    {
        character.Moving(pos);
    }

    public override void addListener()
    {
        if (GameEvents.events.GetComponent<GameEvents>().GetInputType == GameEvents.InputType.Buttons)
        {
            Check = true;
            GameEvents.CharacterShouldMove.AddListener(GetNewTargetPosition);
        }
        else
        {
            Check = false;
            GameEvents.CharacterShouldMove.RemoveListener(GetNewTargetPosition);
        }
    }
}

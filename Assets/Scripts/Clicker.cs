using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CharacterMover character;
    private float point;
    private bool check;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (check)
        {
            if (eventData.pointerId == -1)
            {
                point = eventData.pointerPressRaycast.worldPosition.x;
                Debug.Log(point);
                GameEvents.CharacterMove(point);
            }
        }
    }

    void Start()
    {
        GameEvents.onInputChanged.AddListener(addListener);
    }

    private void GetNewTargetPosition(float pos)
    {
        character.Moving(pos);
    }

    public void addListener()
    {
        if (GameEvents.events.GetComponent<GameEvents>().GetInputType == GameEvents.InputType.Drag)
        {
            check = true;
            GameEvents.CharacterShouldMove.AddListener(GetNewTargetPosition);
        }
        else
        {
            check = false;
            GameEvents.CharacterShouldMove.RemoveListener(GetNewTargetPosition);
        }
    }
}

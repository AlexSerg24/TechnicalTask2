using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : AbstractInput, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CharacterMover character;
    private Transform CharacterPosition;
    private void Awake()
    {
        character = gameObject.GetComponent<CharacterMover>();
        CharacterPosition = gameObject.transform;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (Check)
        {
            Point = eventData.pointerCurrentRaycast.worldPosition.x;
            if (Mathf.Abs(CharacterPosition.position.x - Point) > 0.3)
            {
                GameEvents.CharacterMove(Point);
                Debug.Log("OnDrag point = " + Point);
            }
        }
    }

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
        if (GameEvents.events.GetComponent<GameEvents>().GetInputType == GameEvents.InputType.Drag)
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

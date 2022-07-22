using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : AbstractInput, IPointerUpHandler, IPointerDownHandler
{
    private float startPoint;
    private float endPoint;
    [SerializeField] private CharacterMover character;

    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.pointerCurrentRaycast.worldPosition.x;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Check)
        {
            endPoint = eventData.pointerCurrentRaycast.worldPosition.x;
            if (Mathf.Abs(endPoint - startPoint) > 0.3f)
            {
                Point = character.gameObject.transform.position.x + endPoint - startPoint;
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
        if (GameEvents.events.GetComponent<GameEvents>().GetInputType == GameEvents.InputType.Swipe)
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

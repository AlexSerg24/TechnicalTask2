using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEvents : MonoBehaviour
{
    //[SerializeField] private GameObject Character;
    [SerializeField] private GameObject Switcher;
    [SerializeField] private GameObject Pause;
    [SerializeField] private Text currentInput;
    public static GameEvents events;

    public enum InputType { Buttons, Drag, Swipe}
    private InputType currentState;

    public InputType GetInputType
    {
        get
        {
            return currentState;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        events = this;
        currentState = InputType.Drag;
        ChangeInputText();
    }

    void Start()
    {
        onInputChanged.Invoke();
    }

    public static UnityEvent<float> CharacterShouldMove = new UnityEvent<float>();
    public static void CharacterMove(float point)
    {
        CharacterShouldMove.Invoke(point);
    }

    public static UnityEvent onPausePressed = new UnityEvent();
    public static void PauseClick()
    {
        events.Switcher.SetActive(true);
        events.Pause.SetActive(false);
        onPausePressed.Invoke();       
    }

    public static UnityEvent onInputChanged = new UnityEvent();
    public void ChooseButtons()
    {
        events.Switcher.SetActive(false);
        events.Pause.SetActive(true);
        currentState = InputType.Buttons;
        ChangeInputText();
        onInputChanged.Invoke();
    }
    public void ChooseDrag()
    {
        events.Switcher.SetActive(false);
        events.Pause.SetActive(true);
        currentState = InputType.Drag;
        ChangeInputText();
        onInputChanged.Invoke();
    }
    public void ChooseSwipe()
    {
        events.Switcher.SetActive(false);
        events.Pause.SetActive(true);
        currentState = InputType.Swipe;
        ChangeInputText();
        onInputChanged.Invoke();
    }
    private void ChangeInputText()
    {
        currentInput.text = currentState.ToString();
    }
}

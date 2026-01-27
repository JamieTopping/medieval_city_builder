using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HotbarHideScript : MonoBehaviour
{
    private UnityEvent hotbarSwitchEvent;
    [SerializeField] private bool isUp;
    [SerializeField] private Vector2 defaultPosition;
    [SerializeField] private Vector2 hiddenPosition;
    [SerializeField] private float setSlideSpeed;
    private Vector2 targetPosition;
    private RectTransform rectTransform;
    private bool isMoving;

    private void Awake()
    {
        isMoving = false;
        rectTransform = GetComponent<RectTransform>();
        if (isUp)
        {
            rectTransform.anchoredPosition = defaultPosition;
        }
        else
        {
            rectTransform.anchoredPosition = hiddenPosition;
        }
    }

    private void Start()
    {
        hotbarSwitchEvent = EventHandlerScript.Instance.hotbarSwitchEvent;
        hotbarSwitchEvent.AddListener(OnHotbarSwitch);
    }

    public void OnThisClicked()
    {
        if (isUp && !isMoving)
        {
            hotbarSwitchEvent.Invoke();
        }
    }

    private void OnHotbarSwitch()
    {
        if (isUp)
        {
            targetPosition = hiddenPosition;
            isUp = false;
            StartCoroutine(Slide());
        }
        else
        {
            targetPosition = defaultPosition;
            isUp = true;
            StartCoroutine(Slide());
        }
    }

    private IEnumerator Slide()
    {
        isMoving = true;
        while (rectTransform.anchoredPosition != targetPosition)
        {
            float step = setSlideSpeed * Time.deltaTime;
            rectTransform.anchoredPosition = (Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, step));
            yield return null;
        }
        isMoving = false;
    }
}

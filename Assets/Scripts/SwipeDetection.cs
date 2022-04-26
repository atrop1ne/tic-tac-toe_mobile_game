using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public delegate void OnSwipeInput(Vector2 direction);
    public static event OnSwipeInput SwipeEvent;

    private Vector2 tapPosition;
    private Vector2 swipeDelta;
    private float deadzone = 60f;

    private bool isSwiping;
    private bool isMobile;

    void Start()
    {
        isMobile = Application.isMobilePlatform;
    }

    void Update()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                tapPosition = Input.mousePosition;
            }

            else if (Input.GetMouseButtonUp(0))
            {
                resetSwipe();
            }
        }

        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwiping = true;
                    tapPosition = Input.GetTouch(0).position;
                }
            }

            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                resetSwipe();
            }
        }

        checkSwipe();
    }

    private void checkSwipe()
    {
        swipeDelta = Vector2.zero;

        if (isSwiping)
        {
            if (!isMobile && Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - tapPosition;
            }

            else if (Input.touchCount > 0)
            {
                swipeDelta = Input.GetTouch(0).position - tapPosition;
            }
        }

        if (swipeDelta.magnitude > deadzone)
        {
            if (SwipeEvent != null)
            {
                if(Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    SwipeEvent(swipeDelta.x > 0 ? Vector2.right : Vector2.left);
                }

                else
                {
                    SwipeEvent(swipeDelta.y > 0 ? Vector2.up : Vector2.down);
                }
            }

            resetSwipe();
        }
    }

    private void resetSwipe()
    {
        isSwiping = false;

        tapPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
    }
}

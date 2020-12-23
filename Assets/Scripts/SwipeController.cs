using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private bool _isDragging;
    private Vector2 _tapPosition, _swipeDelta;
    private float _minSwipeDelta = 50f;

    private Vector2 _firstMousePosition;
    private Vector2 _secondMousePosition;

    public enum SwipeType
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public delegate void OnSwipeInput(SwipeType type);
    public static event OnSwipeInput SwipeEvent;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            TouchSwipe();
            CalculateTouchSwipe();
        }
            MouseSwipe();
    }

    private void CalculateTouchSwipe()
    {

        _swipeDelta = Vector2.zero;
        if (_isDragging)
        {
            if (Input.touchCount > 0)
            {
                _swipeDelta = Input.touches[0].position - _tapPosition;
            }
        }
        if (_swipeDelta.magnitude > _minSwipeDelta)
        {
            if (SwipeEvent != null)
            {
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                    SwipeEvent(_swipeDelta.x < 0 ? SwipeType.LEFT : SwipeType.RIGHT);
                else SwipeEvent(_swipeDelta.y < 0 ? SwipeType.DOWN : SwipeType.UP);
            }
           ResetSwipe();
        }
    }

    private void CalculateMouseSwipe()
    {
        _swipeDelta = Vector2.zero;
        if (_isDragging)
        {
            _swipeDelta = new Vector2(Input.mousePosition.x - _firstMousePosition.x, Input.mousePosition.y - _firstMousePosition.y);
        }
        if (_swipeDelta.magnitude > _minSwipeDelta)
        {
            if (SwipeEvent != null)
            {
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                    SwipeEvent(_swipeDelta.x < 0 ? SwipeType.LEFT : SwipeType.RIGHT);
                else SwipeEvent(_swipeDelta.y < 0 ? SwipeType.DOWN : SwipeType.UP);
            }
        }

    }

    private void TouchSwipe()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _isDragging = true;
                _tapPosition = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
            {
                ResetSwipe();
            }
        }
        Debug.Log(_isDragging);
    }

    private void MouseSwipe()
    {
        if (_isDragging)
        {
            _firstMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }
    }

    private void OnMouseDrag()
    {
        _isDragging = true;
        CalculateMouseSwipe();
    }

    private void ResetSwipe()
    {
        _isDragging = false;
        _tapPosition = _swipeDelta = Vector2.zero;
        SwipeEvent(SwipeType.DOWN);
    }
}

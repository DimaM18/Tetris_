using System;

using Client.Scripts.Tools.Platform;

using UnityEngine;


namespace Client.Scripts.Tools.Services
{
    public class TouchService : IService
    {
        public event Action<Vector3> OnTouchBegin;
        public event Action<float> OnSwiped;
        public event Action<Vector3> OnTouchEnd;
        public event Action<Vector3> OnTouch;
        public event Action<Vector3> OnTouchHoldEnd;

        private readonly bool _isMobile;
        private Vector3 _touchBeginPosition;
        
        private Vector3 _oldMousePosition;
        
        private int _currentFingerId;
        private bool _isTouch;
        private bool _isClick;

        private readonly float _clickThreshold;

        private bool _isEnabled;

        public TouchService()
        {
            _isMobile = PlatformUtils.IsMobile();
            _isEnabled = true;

            float screenSize = Mathf.Min(Screen.height, Screen.width);
            _clickThreshold = screenSize * 0.01f;
            _clickThreshold *= _clickThreshold;
        }

        public void Enable(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }

        public void OnUpdate()
        {
            if (_isMobile)
            {
                UpdateTouch();
            }
            else
            {
                UpdateMouse();
#if UNITY_EDITOR
                UpdateKeyboard();
#endif
            }
        }

        public void DeInit()
        {
            OnSwiped = null;
            OnTouch = null;
            OnTouchBegin = null;
            OnTouchEnd = null;
            OnTouchHoldEnd = null;
        }

        private void UpdateMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _oldMousePosition = Input.mousePosition;
                TouchBegin(Input.mousePosition);
                Touch(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - _oldMousePosition;
                _oldMousePosition = Input.mousePosition;
                TouchMoved(delta);
                Touch(Input.mousePosition);
                CheckClickDistance(_oldMousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                TouchEnd(Input.mousePosition);
            }
        }

        private void UpdateKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TouchBegin(Input.mousePosition);
                TouchEnd(Input.mousePosition);
            }
        }

        private void UpdateTouch()
        {
            if (!_isTouch)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        _isTouch = true;
                        _currentFingerId = touch.fingerId;
                        TouchBegin(touch.position);
                        Touch(touch.position);
                    }
                }
            }
            else
            {
                foreach (Touch touch in Input.touches)
                {
                    _isTouch = false;
                    
                    if (touch.fingerId == _currentFingerId)
                    {
                        switch (touch.phase)
                        {
                            case TouchPhase.Moved:
                                _isTouch = true;
                                TouchMoved(touch.deltaPosition);
                                CheckClickDistance(touch.position);
                                Touch(touch.position);
                                break;
                            
                            case TouchPhase.Stationary:
                                _isTouch = true;
                                Touch(touch.position);
                                break;
                            
                            case TouchPhase.Ended:
                            case TouchPhase.Canceled:
                                TouchEnd(touch.position);
                                break;
                        }
                    }
                }
            }
        }

        private void TouchBegin(Vector3 point)
        {
            if (_isEnabled == false)
            {
                return;
            }
            
            _touchBeginPosition = point;
            _isClick = true;

            OnTouchBegin?.Invoke(point);
        }

        private void TouchMoved(Vector3 delta)
        {
            if (_isEnabled == false)
            {
                return;
            }
            
            OnSwiped?.Invoke(delta.x);
        }

        private void TouchEnd(Vector3 point)
        {
            if (_isEnabled == false)
            {
                return;
            }
            
            if (_isClick)
            {
                OnTouchEnd?.Invoke(point);
                OnTouchHoldEnd?.Invoke(point);
            }
            else
            {
                OnTouchHoldEnd?.Invoke(point);
            }
        }

        private void Touch(Vector3 point)
        {
            if (_isEnabled == false)
            {
                return;
            }
            
            OnTouch?.Invoke(point);
        }

        private void CheckClickDistance(Vector3 point)
        {
            if (!_isClick)
            {
                return;
            }
            
            Vector3 swipeVector = point - _touchBeginPosition;
            if (swipeVector.sqrMagnitude > _clickThreshold)
            {
                _isClick = false;
            }
        }
    }
}
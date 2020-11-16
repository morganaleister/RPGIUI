using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.MainframeReference
{

    public class MouseTracker : MonoBehaviour
    {
        public static MouseTracker Singleton { get; protected set; }

        public static event Action 
            MovingStarted, MouseMoving, MouseStopped,
            LeftDown, LeftUp, 
            RightDown, RightUp, 
            MidDown, MidUp;

        public UnityEvent 
            actionOnMouseMoving, actionOnMovingStarted, actionOnMouseStop, 
            actionOnLMBDown, actionOnLMBUp,
            actionOnRMBDown, actionOnRMBUp, 
            actionOnMMBDown, actionOnMMBUp;


        private static Vector3 m_lastPos, m_currPos;
        private static bool 
            _isLMBDown, _isRMBDown,_isMMBDown, 
            _isMouseMoving;

        public static Vector3 MousePos { get => m_currPos; }

        public static bool IsLMBDown
        {
            get => _isLMBDown;
            private set
            {
                if (_isLMBDown != value)
                {
                    _isLMBDown = value;
                    if (value) LeftDown?.Invoke();
                    else LeftUp?.Invoke();
                }
            }
        }
        public static bool IsRMBDown
        {
            get => _isRMBDown;
            private set
            {
                if (_isRMBDown != value)
                {
                    _isRMBDown = value;
                    if (value) RightDown?.Invoke();
                    else RightUp?.Invoke();
                }
            }
        }
        public static bool IsMMBDown
        {
            get => _isMMBDown;
            private set
            {
                if (_isMMBDown != value)
                {
                    _isMMBDown = value; 
                    if (value) MidDown?.Invoke();
                    else MidUp?.Invoke();
                }
            }
        }
        
        public static bool IsMouseMoving 
        {
            get => _isMouseMoving;
            private set
            {
                
                if (IsMouseMoving != value) //if it actually changes (true<->false) !(true->true||false->false)
                {
                    _isMouseMoving = value;//do the change and proceed to inform
                    if (IsMouseMoving) MovingStarted?.Invoke();
                    else MouseStopped?.Invoke();                   
                }

                if (value) MouseMoving?.Invoke();
            }
        }


        protected void Awake()
        {
            if (!Singleton) Singleton = this;
            else Destroy(gameObject);

            MouseMoving += OnMouseMoving;
            MovingStarted += OnMovingStarted;
            MouseStopped += OnMouseStop;
            LeftDown += OnLD;
            LeftUp += OnLU;
            RightDown += OnRD;
            RightUp += OnRU;
            MidDown += OnMD;
            MidUp += OnMU;
        }

        private void OnMovingStarted() => actionOnMovingStarted?.Invoke();
        private void OnMouseMoving() => actionOnMouseMoving?.Invoke();
        private void OnMouseStop() => actionOnMouseStop?.Invoke();
        private void OnLD() => actionOnLMBDown?.Invoke();
        private void OnLU() => actionOnLMBUp?.Invoke();
        private void OnRD() => actionOnRMBDown?.Invoke();
        private void OnRU() => actionOnRMBUp?.Invoke();
        private void OnMD() => actionOnMMBDown?.Invoke();
        private void OnMU() => actionOnMMBUp?.Invoke();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) IsLMBDown = true;
            if (Input.GetMouseButtonDown(1)) IsRMBDown = true;
            if (Input.GetMouseButtonDown(2)) IsMMBDown = true;
            if (Input.GetMouseButtonUp(0)) IsLMBDown = false;
            if (Input.GetMouseButtonUp(1)) IsRMBDown = false;
            if (Input.GetMouseButtonUp(2)) IsMMBDown = false;

            m_lastPos = m_currPos;
            m_currPos = Input.mousePosition;
            if (m_lastPos != m_currPos)
                IsMouseMoving = true;
            else IsMouseMoving = false;
        }

    }
}
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Mainframe
{

    public class MouseTracker : MonoBehaviour
    {
        public static MouseTracker Singleton { get; protected set; }

        public static event Action MouseMoving, LeftDown,
            LeftUp, RightDown, RightUp, MidDown, MidUp;

        public UnityEvent actionOnLMBDown, actionOnLMBUp,
            actionOnRMBDown, actionOnRMBUp, actionOnMMBDown, actionOnMMBUp;


        private static Vector3 m_lastPos, m_currPos;
        private static bool _isLMBDown, _isLMBUp, _isRMBDown,
            _isRMBUp, _isMMBDown, _isMMBUp;

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

        protected void Awake()
        {
            if (!Singleton) Singleton = this;
            else Destroy(gameObject);

            LeftDown += OnLD;
            LeftUp += OnLU;
            RightDown += OnRD;
            RightUp += OnRU;
            MidDown += OnMD;
            MidUp += OnMU;
        }

        private void OnLD() => actionOnLMBDown?.Invoke();
        private void OnLU() => actionOnLMBUp?.Invoke();
        private void OnRD() => actionOnRMBDown?.Invoke();
        private void OnRU() => actionOnRMBUp?.Invoke();
        private void OnMD() => actionOnMMBDown?.Invoke();
        private void OnMU() => actionOnMMBUp?.Invoke();

        private void FixedUpdate()
        {
            m_currPos = Input.mousePosition;
            if (m_lastPos != m_currPos)
            {
                MouseMoving?.Invoke();
                m_lastPos = m_currPos;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) IsLMBDown = true;
            if (Input.GetMouseButtonDown(1)) IsRMBDown = true;
            if (Input.GetMouseButtonDown(2)) IsMMBDown = true;
            if (Input.GetMouseButtonUp(0)) IsLMBDown = false;
            if (Input.GetMouseButtonUp(1)) IsRMBDown = false;
            if (Input.GetMouseButtonUp(2)) IsMMBDown = false;
        }

    }
}
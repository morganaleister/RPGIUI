using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Mainframe;
using UnityEngine.UIElements;

namespace Scripts.Controller
{
    public partial class MouseTracker : MonoBehaviour
    {
        public static event Action MouseMoving;
        public static event Action LeftClick;
        public static event Action RightClick;
        public static event Action MiddleClick;
        public static event Action LeftDoubleClick;
        public static event Action RightDoubleClick;
        public static event Action MiddleDoubleClick;

        public IHighlightable Highlighted { get; private set; }
        public float ClickSpeed { get => _clickSpeed; set => _clickSpeed = value; }
        public float DblClickSpeed { get => _dblClickSpeed; set => _dblClickSpeed = value; }

        private Vector3 m_lastPos;
        [SerializeField] private float _clickSpeed, _dblClickSpeed;
        private static float _mbLDownStarted, _mbLDownEnded, last_mbLClickTime;
        private static float _mbRDownStarted, _mbRDownEnded, last_mbRClickTime;
        private static float _mbMDownStarted, _mbMDownEnded, last_mbMClickTime;
        private static ByRef<bool> _dblLClicked = new ByRef<bool>(false);
        private static ByRef<bool> _dblRClicked = new ByRef<bool>(false);
        private static ByRef<bool> _dblMClicked = new ByRef<bool>(false);

        private void Awake()
        {
            MouseMoving += OnMove;
        }

        private void FixedUpdate()
        {
            if (m_lastPos != Input.mousePosition)
            {
                MouseMoving?.Invoke();
                m_lastPos = Input.mousePosition;
            }
        }

        private void Update()
        {


            if (Input.GetMouseButtonDown(0)) SetMbDown(out _mbLDownStarted);
            if (Input.GetMouseButtonUp(0))
                SetMbUp(_mbLDownStarted, ref _mbLDownEnded,
                    ref last_mbLClickTime, ref LeftClick,
                    ref LeftDoubleClick, _dblLClicked);

            if (Input.GetMouseButtonDown(1)) SetMbDown(out _mbRDownStarted);
            if (Input.GetMouseButtonUp(1))
                SetMbUp(_mbRDownStarted, ref _mbRDownEnded,
                    ref last_mbRClickTime, ref RightClick,
                    ref RightDoubleClick, _dblRClicked);

            if (Input.GetMouseButtonDown(2)) SetMbDown(out _mbMDownStarted);
            if (Input.GetMouseButtonUp(2))
                SetMbUp(_mbMDownStarted, ref _mbMDownEnded,
                    ref last_mbMClickTime, ref MiddleClick,
                    ref MiddleDoubleClick, _dblMClicked);
        }

        #region CheckClick
        private void SetMbDown(out float BtnDownStarted) => BtnDownStarted = Time.time;
        private void SetMbUp(float BtnDownStarted, ref float BtnDownEnded,
            ref float LastBtnClickTime, ref Action SingleClick,
            ref Action DoubleCick, ByRef<bool> DoubleClicked)
        {
            float LastMBUp = 0f;

            //BtnDownEnded 0 means this is the first mbUp. Therefore write there the curr time.
            if (BtnDownEnded == 0f) BtnDownEnded = Time.time;
            else // if there has been a mbUp, first:
            {
                LastMBUp = BtnDownEnded; //capture that time
                BtnDownEnded = Time.time; //overwtirte last w current time
            }

            //BtnUpTime(now) - BtnDownStart = click time.
            if (BtnDownEnded - BtnDownStarted <= ClickSpeed)
            {//this is an accepted click but first, check if was a double one

                if (LastBtnClickTime != 0f) // if there has been a previous successful click...
                {
                    //BtnUptime(now) - LastClickTime = doubleclicktime
                    if (BtnDownEnded - LastBtnClickTime <= DblClickSpeed)
                    {//this is an accepted double click

                        DoubleClicked.Value = true;
                        DoubleCick?.Invoke();
                        Debug.Log("Double clicked");
                    }
                    else //...but such was too long ago (BtnUpTime - LastClickTime > DblClickSpeed)..
                        //..wait 4 a next double click b4 click
                        StartCoroutine(WaitForDoubleClick(SingleClick, DoubleClicked, ClickSpeed, DblClickSpeed));

                }
                else //there hasn't ever been a successful click before (LastBtnClickTime == 0)
                    //so wait 4 a next double click b4 click
                    StartCoroutine(WaitForDoubleClick(SingleClick, DoubleClicked, ClickSpeed, DblClickSpeed));

                LastBtnClickTime = BtnDownEnded; //this is now the Last accepted click
            }
        }

        private static IEnumerator WaitForDoubleClick
            (Action SingleClick, ByRef<bool> DoubleClicked,
            float ClickSpeed, float DblClickSpeed)
        {
            yield return new WaitForSecondsRealtime(ClickSpeed + DblClickSpeed);

            if (!DoubleClicked.Value)
            {
                SingleClick?.Invoke(); //invoke a single click
                DoubleClicked.Value = false;
                Debug.Log("Single clicked");
            }
            else DoubleClicked.Value = false;

        }

        #endregion

        private void OnMove()
        {          
            

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {//hit something
                                
                var thisOne = hit.transform.GetComponent<IHighlightable>();
                
                if (thisOne != null)//hit is highlightable
                {//Highlight Target SET

                    IHighlightable lastOne = Highlighted;
                    Highlighted = thisOne;

                    if (lastOne != null)
                    {//last one exists so..
                        //..if last one is ! than current one
                        if (lastOne != Highlighted)
                        {
                            lastOne.Dehighlight();//dehighlight old one
                            //and highlight current one
                            Highlighted.Highlight();
                        }
                    }
                    else 
                        Highlighted.Highlight(); //Do highlight bc its the first ever
                }
            }
        }




    }
}
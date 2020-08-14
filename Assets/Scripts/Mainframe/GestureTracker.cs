using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Mainframe
{
    public class GestureTracker : MouseTracker
    {
        public static new GestureTracker Singleton { get; private set; }

        [SerializeField] private float _clickSpeed = .12f;
        public float ClickSpeed { get => _clickSpeed; set => _clickSpeed = value; }

        public UnityEvent actionOnMove, actionOnLClick,
            actionOnRClick, actionOnMClick, actionOnLDblClick,
            actionOnRDblClick, actionOnMDblClick;

        private static Queue<Timestamp>[] qts_MBHistory
            = new Queue<Timestamp>[]
            {
                new Queue<Timestamp>(),
                new Queue<Timestamp>(),
                new Queue<Timestamp>()
            };
        private static Queue<double>[] qdb_ClickHistory
            = new Queue<double>[]
            {
                new Queue<double>(),
                new Queue<double>(),
                new Queue<double>()
            };

        public static event Action
            LeftClick, LeftDblClick,
            RightClick, RightDblClick,
            MidClick, MidDblClick;

        private new void Awake() 
        {
            base.Awake();
            
            if (!Singleton) Singleton = this;
            else Destroy(gameObject);

            MouseTracker.MouseMoving += OnMove;

            MouseTracker.LeftDown += LMBDown;
            MouseTracker.LeftUp += LMBUp;
            LeftClick += OnLClick;
            LeftDblClick += OnLDblClick;

            MouseTracker.RightDown += RMBDown;
            MouseTracker.RightUp += RMBUp;
            RightClick += OnRClick;
            RightDblClick += OnRDblClick;

            MouseTracker.MidDown += MMBDown;
            MouseTracker.MidUp += MMBUp;
            MidClick += OnMClick;
            MidDblClick += OnMDblClick;
        }





        private void OnMove() => actionOnMove?.Invoke();
        private void LMBDown() => MouseButtonUpDown(0);
        private void LMBUp() => MouseButtonUpDown(0, false);
        private void OnLClick() => actionOnLClick?.Invoke();
        private void OnLDblClick() => actionOnLDblClick?.Invoke();

        private void RMBDown() => MouseButtonUpDown(1);
        private void RMBUp() => MouseButtonUpDown(1, false);
        private void OnRClick() => actionOnRClick?.Invoke();
        private void OnRDblClick() => actionOnRDblClick?.Invoke();

        private void MMBDown() => MouseButtonUpDown(2);
        private void MMBUp() => MouseButtonUpDown(2, false);
        private void OnMClick() => actionOnMClick?.Invoke();
        private void OnMDblClick() => actionOnMDblClick?.Invoke();



        private static void MouseButtonUpDown(int mouseButton, bool down = true)
        {
            UpdateMBtnHistory(mouseButton, down);
            CheckForGestures();
        }
        private static void UpdateMBtnHistory(int mouseButton, bool down)
        {

            if (qts_MBHistory[mouseButton].Count == 2)
                qts_MBHistory[mouseButton].Dequeue();

            var ts = new Timestamp(mouseButton, Time.time, down);

            ///debug
            ///
            ///SCDebugger.Text = string.Format("{0} detected @ {1}", Timestamp.PressReleasedToText(ts), ts.Time);

            qts_MBHistory[mouseButton].Enqueue(ts);

            ///debug
            ///
            ///Timestamp[] tsArray = qts_MBHistory[mouseButton].ToArray();
            ///for (int i = 0; i < tsArray.Length; i++)
            ///{
            ///    SCDebugger.Text += string.Format("\n" +
            ///         "{2} mouse button (index: [{0}]) was {1} @T:{3}",
            ///         i, Timestamp.PressReleasedToText(tsArray[i]),
            ///         Timestamp.ButtonToText(tsArray[i].Button), tsArray[i].Time);
            ///}

        }

        private static void CheckForGestures()
        {

            CheckForGesture_Click();
            //CheckForGesture_Drag();
            //CheckForGesture_DblClick();            
            //CheckForGesture_TrpClick();




        }

        private static void CheckForGesture_Click()
        {
            for (int i = 0; i < 3; i++) //recorre l,r&mid mb
            {//en mousebuttons history queues

                Timestamp[] tsArray = qts_MBHistory[i].ToArray();//stores array of Mousebutton 'i'

                for (int j = 0; j < tsArray.Length; j++)//recorre hist Q de MB'i' (stored array)
                {
                    Timestamp ts = tsArray[j]; //store timestamp 'j'

                    if (ts.UpOrDown == Timestamp.uod.up)//if up == odd #
                    {
                        if (j == 0)
                            continue;  //skip if 0 so it doesnt back-overflow (0-1)

                        float diff = ts.Time - tsArray[j - 1].Time;
                        bool click = diff <= Singleton.ClickSpeed;

                        ///debug
                        ///SCDebugger.Text += string.Format(
                        ///        "\nDiff:{0} <= {2}? ({1})",
                        ///        diff, click, Singleton.ClickSpeed);

                        if (click)
                        {
                            ///debug
                            ///SCDebugger.Text += string.Format("\n{0} click confirmed @:{1}"
                            ///    , Timestamp.ButtonToText(ts.Button), ts.Time);
                            UpdateClickHistory(ts.Button, ts.Time);
                        }
                    }
                }
            }
        }
        private static void UpdateClickHistory(int mouseButton, float time)
        {
            if (qdb_ClickHistory[mouseButton].Count == 3)
                qdb_ClickHistory[mouseButton].Dequeue();

            qdb_ClickHistory[mouseButton].Enqueue(time);

            switch (mouseButton)
            {
                case 0:
                    LeftClick?.Invoke();
                    break;
                case 1:
                    RightClick?.Invoke();
                    break;
                case 2:
                    MidClick?.Invoke();
                    break;
            }
        }

        private static void CheckForGesture_Drag()
        {
            throw new NotImplementedException();
        }
        private static void CheckForGesture_DblClick()
        {
            throw new NotImplementedException();
        }
        private static void CheckForGesture_TrpClick()
        {
            throw new NotImplementedException();
        }




        private static IEnumerator WaitForDoubleClick
            (Action SingleClick, ByRef<bool> DoubleClicked,
            float ClickSpeed, float DblClickSpeed)
        {
            yield return new WaitForSecondsRealtime(ClickSpeed);

            if (!DoubleClicked.Value)
            {
                SingleClick?.Invoke(); //invoke a single click
                DoubleClicked.Value = false;
                //Debug.Log("Single clicked");
            }
            else DoubleClicked.Value = false;

        }


    }
}
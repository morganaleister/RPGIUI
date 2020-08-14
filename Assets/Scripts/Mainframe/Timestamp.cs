using System;
using System.Data;
using UnityEngine;

namespace Scripts.Mainframe
{
    public class Timestamp
    {
        ///i = index
        ///b = button(pressed)
        ///d = down (wasItAPress?(orRelease)) 
        ///t = (float) timestamp recorded
        /// </summary>
        private Vector4 ibdt = new Vector4(-1, -1, -1, -1);        

        //public int Index { get => (int)ibdt.x; set => ibdt.x = (float)value; }
        public int Button { get => (int)ibdt.y; }
        public float Time { get => ibdt.w; }
        public uod UpOrDown { get; private set; } = uod.uninitialized;

        public Timestamp(int mouseButtonPressed, float timeItWasPressed, bool wasItAPress, int index = 0)
        {
            ibdt.x = index;
            ibdt.y = mouseButtonPressed;
            SetUpOrDown(wasItAPress);
            ibdt.w= timeItWasPressed;            
        }
        public static void SetUpOrDown(Timestamp t, bool down)
        {
            if (down)
            {
                t.UpOrDown = uod.down;
                t.ibdt.z = 1;
            }
            else 
            {
                t.UpOrDown = uod.up;
                t.ibdt.z = 0;
            }
        }

        public void SetUpOrDown(bool down)
        {
            SetUpOrDown(this, down);
        }
        public bool IsItDown()
        {
            return IsItDown(this);
        }
        public static bool IsItDown(Vector4 v4)
        {
            switch (v4.z)
            {
                case 1:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsItUp(Vector4 v4)
        {
            switch (v4.z)
            {
                case 0:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsItDown(Timestamp t)
        {
            switch (t.UpOrDown)
            {
                case uod.down:
                    return true;
                case uod.up:
                    return false;
                default:
                    throw new Exception("bool Timestamp.GetDown() is trying to return the bool equivalent to the" +
                        "uod Timestamp._UpOrDown value which isn't uod.up either uod.down, and the result as 'false'" +
                        "could be mistaken as an uod.up.\n Please initialize the uod Timestamp._UpOrDown variable first.");
            }
        }
        public enum uod
        {
            uninitialized = -1,
            down = 0,
            up = 1
        }

        public static string PressReleasedToText(Timestamp ts)
        {
            switch (ts.UpOrDown)
            {
                case Timestamp.uod.down:
                    return "Pressed";
                case Timestamp.uod.up:
                    return "Released";
                default:
                    return "";
            }
        }
        public static string ButtonToText(int button)
        {
            switch (button)
            {
                case 0:
                    return "Left";
                case 1:
                    return "Right";
                case 2:
                    return "Mid";
                default:
                    return "";
            }
        }

    }
}
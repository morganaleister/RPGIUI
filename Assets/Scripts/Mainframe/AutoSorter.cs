using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainframeReference
{
    [ExecuteAlways()]
    public class AutoSorter : MonoBehaviour
    {
        public UnityEvent OnChildrenChanged;

        public enum XYZ { xy, zy, xz }

        public XYZ _mode;
        [SerializeField] private uint _maxH = 1, _MaxV = 1;
        [SerializeField] private float _spaceH, _spaceV;
        [SerializeField] private List<Transform> _children = new List<Transform>();

        public float SpacingH { get => _spaceH; set => _spaceH = value; }
        public float SpacingV { get => _spaceV; set => _spaceV = value; }
        public uint MaxSlotsH { get => _maxH; set => _maxH = CustomClamp(value); }
        public uint MaxSlotsV { get => _MaxV; set => _MaxV = CustomClamp(value); }

        private static uint CustomClamp(uint value) =>
            (uint)Mathf.Clamp(value, 1, float.MaxValue);



        private void Start()
        {
            ArrangeChildren();
            OnChildrenChanged.AddListener(ArrangeChildren);

        }

        private void ArrangeChildren()
        {
            if (_children.Count == 0) return;
            if (!_children[0]) return;

            _children[0].localPosition = Vector3.zero;

            int _hcount = 1, _vcount = 1;
            for (int i = 1; i < _children.Count; i++)
            {
                if (!_children[i]) return;

                float newX;
                float newY;

                switch (_mode)
                {
                    case XYZ.xy:
                        //check if space to the "X" is available
                        if (_hcount < MaxSlotsH)
                        {

                            newX = _children[i - 1].localPosition.x + _spaceH; //set previous X + Hspacing
                            newY = _children[i - 1].localPosition.y; //sets previous Y

                            _hcount++; //move 1 space to the "X"
                        }
                        else if (_vcount < MaxSlotsV)  //when not space to the right available
                        {//check if space to the "Y" is available

                            _hcount = 1;//move to 1st space in "X"

                            newX = _children[0].localPosition.x; //set X == to first item
                            newY = _children[i - 1].localPosition.y + _spaceV; //set previous Y + Yspacing                            

                            _vcount++;//move 1 space to the "Y"
                        }
                        else //no spaces left available into "X" nor "Y"
                        {
                            Overflow(_children[i]);
                            break;
                        }

                        _children[i].localPosition = new Vector3(newX, newY, _children[i].localPosition.z);

                        break;
                    case XYZ.zy:

                        //check if space to the "X" is available
                        if (_hcount < MaxSlotsH)
                        {

                            newX = _children[i - 1].localPosition.z + _spaceH; //set previous X + Hspacing
                            newY = _children[i - 1].localPosition.y; //sets previous Y

                            _hcount++; //move 1 space to the "X"
                        }
                        else if (_vcount < MaxSlotsV)  //when not space to the right available
                        {//check if space to the "Y" is available

                            _hcount = 1;//move to 1st space in "X"

                            newX = _children[0].localPosition.z; //set X == to first item
                            newY = _children[i - 1].localPosition.y + _spaceV; //set previous Y + Yspacing                            

                            _vcount++;//move 1 space to the "Y"
                        }
                        else //no spaces left available into "X" nor "Y"
                        {
                            Overflow(_children[i]);
                            break;
                        }

                        _children[i].localPosition = new Vector3(_children[i].localPosition.x, newY, newX);


                        break;
                    case XYZ.xz:
                        //check if space to the "X" is available
                        if (_hcount < MaxSlotsH)
                        {

                            newX = _children[i - 1].localPosition.x + _spaceH; //set previous X + Hspacing
                            newY = _children[i - 1].localPosition.z; //sets previous Y

                            _hcount++; //move 1 space to the "X"
                        }
                        else if (_vcount < MaxSlotsV)  //when not space to the right available
                        {//check if space to the "Y" is available

                            _hcount = 1;//move to 1st space in "X"

                            newX = _children[0].localPosition.x; //set X == to first item
                            newY = _children[i - 1].localPosition.z + _spaceV; //set previous Y + Yspacing                            

                            _vcount++;//move 1 space to the "Y"
                        }
                        else //no spaces left available into "X" nor "Y"
                        {
                            Overflow(_children[i]);
                            break;
                        }

                        _children[i].localPosition = new Vector3(newX, _children[i].localPosition.y, newY);
                        break;
                    default:
                        break;
                }

            }
        }

        private void Overflow(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        private void OnGUI()
        {
            ArrangeChildren();
        }

        public void Add(Transform t) { _children.Add(t); OnChildrenChanged?.Invoke(); }
        public void Remove(Transform t) { _children.Remove(t); OnChildrenChanged?.Invoke(); }

    }



}

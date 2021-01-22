using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Mainframe
{
    [ExecuteAlways()]
    public class AutoSorter : MonoBehaviour
    {
        public UnityEvent OnChildrenChanged;
        public Transform ParentObject;        

        public enum XYZ { xy, zy, xz }

        public XYZ _mode;
        [SerializeField] private Vector2 _maxHV;
        [SerializeField] private Vector2 _spaceHV;
        [SerializeField] private List<Transform> _children = new List<Transform>();


        public Vector2 SpacingHV { get => _spaceHV; set => _spaceHV = value; }
        public Vector2 MaxSlotsHV { get => _maxHV; set => _maxHV = value; }



        private void Start()
        {
            if (ParentObject == null) throw new System.Exception("Need to specify Parent Object field");
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
                        if (_hcount < MaxSlotsHV.x)
                        {

                            newX = _children[i - 1].localPosition.x + _spaceHV.x; //set previous X + Hspacing
                            newY = _children[i - 1].localPosition.y; //sets previous Y

                            _hcount++; //move 1 space to the "X"
                        }
                        else if (_vcount < MaxSlotsHV.y)  //when not space to the right available
                        {//check if space to the "Y" is available

                            _hcount = 1;//move to 1st space in "X"

                            newX = _children[0].localPosition.x; //set X == to first item
                            newY = _children[i - 1].localPosition.y + _spaceHV.y; //set previous Y + Yspacing                            

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
                        if (_hcount < MaxSlotsHV.x)
                        {

                            newX = _children[i - 1].localPosition.z + _spaceHV.x; //set previous X + Hspacing
                            newY = _children[i - 1].localPosition.y; //sets previous Y

                            _hcount++; //move 1 space to the "X"
                        }
                        else if (_vcount < MaxSlotsHV.y)  //when not space to the right available
                        {//check if space to the "Y" is available

                            _hcount = 1;//move to 1st space in "X"

                            newX = _children[0].localPosition.z; //set X == to first item
                            newY = _children[i - 1].localPosition.y + _spaceHV.y; //set previous Y + Yspacing                            

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
                        if (_hcount < MaxSlotsHV.x)
                        {

                            newX = _children[i - 1].localPosition.x + _spaceHV.x; //set previous X + Hspacing
                            newY = _children[i - 1].localPosition.z; //sets previous Y

                            _hcount++; //move 1 space to the "X"
                        }
                        else if (_vcount < MaxSlotsHV.y)  //when not space to the right available
                        {//check if space to the "Y" is available

                            _hcount = 1;//move to 1st space in "X"

                            newX = _children[0].localPosition.x; //set X == to first item
                            newY = _children[i - 1].localPosition.z + _spaceHV.y; //set previous Y + Yspacing                            

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

        public void Add(Transform t) 
        {
            _children.Add(t);
            t.parent = ParentObject;
            OnChildrenChanged?.Invoke(); 
        }
        public void Remove(Transform t) 
        {
            _children.Remove(t); 
            OnChildrenChanged?.Invoke();
        }

    }


 
}

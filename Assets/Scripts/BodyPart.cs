using Scripts.Mainframe;
using System;
using UnityEngine;

namespace Scripts
{
    [System.Serializable]
    public class BodyPart : IEnhanceable
    {
        public enum Conditions
        {
            Phantom = -1,
            Absent,
            Disabled,
            Hindered,
            Injured,
            Wounded,
            Hurt,
            Optimal
        }

        public string _partName, _pluralName;
        public bool _critical;

        [SerializeField]
        private Gauge _hp;

        public BodyPart[] _sharedSubParts;
                
        
        public float HP
        {
            get
            {
                int bpCount = _sharedSubParts.Length;

                //gets only the private gauge
                if (_sharedSubParts == null
                    || bpCount == 0)
                    return _hp.Current; //  * LimitMultiplier;
                else //gets the average among children's hp and own's sum.
                {

                    float hpSum = _hp.Current;

                    foreach (BodyPart bp in _sharedSubParts)
                    {
                        hpSum += bp.HP;
                    }

                    return hpSum / (bpCount + 1);
                }
            }
            set
            {
                int bpCount = _sharedSubParts.Length;

                if (_sharedSubParts == null
                       || bpCount == 0)
                    //update private gauge
                    _hp.Current = value;// * LimitMultiplier;
                else //update own and each children an equally ammount divided among the total of children's and own.
                {
                    _hp.Current += value / (bpCount + 1);

                    foreach (BodyPart bp in _sharedSubParts)
                    {
                        bp._hp.Current += value / (bpCount + 1);
                    }
                }
            }
        }
        public Conditions Condition
        {
            get
            {
                var query = Math.Round(_hp.Normalized, 2);

                if (query >= .85f)
                    return Conditions.Optimal;
                else if (query >= .7f)
                    return Conditions.Hurt;
                else if (query >= .50f)
                    return Conditions.Wounded;
                else if (query >= .25f)
                    return Conditions.Injured;
                else if (query >= .1f)
                    return Conditions.Hindered;
                else if (query <= -50f)
                    return Conditions.Phantom;
                else if (query <= 0f)
                    return Conditions.Absent;
                else if (query <= .09f)
                    return Conditions.Disabled;
                else
                    throw new NotSupportedException();
            }
        }
        public bool Enhanced { get; set; } = false;


        public BodyPart(int subparts = 0)
        {
            _partName = "bodyPart";
            _pluralName = "bodyParts";
            
            _hp = new Gauge("HP", 1, 1, true, true);
            _sharedSubParts = new BodyPart[subparts];
        }
    }
    

}
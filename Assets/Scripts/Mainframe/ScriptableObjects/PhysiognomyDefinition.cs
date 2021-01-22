using UnityEngine;
using System.Collections.Generic;

namespace Scripts.Mainframe.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "[RaceName] Physiognomy", 
        menuName = "ScriptableObjects/Physiognomy Definition")]
    public class PhysiognomyDefinition : ScriptableObject
    {
        public string _name;
        public BodyPart[] _bodyParts;

        public static void Copy(PhysiognomyDefinition from, PhysiognomyDefinition to)
        {
            to._name = from._name;
            to._bodyParts = from._bodyParts;
        }
        public static void Merge(PhysiognomyDefinition from, PhysiognomyDefinition to)
        {
            List<BodyPart> mergedParts = new List<BodyPart>(to._bodyParts);
            mergedParts.AddRange(from._bodyParts);

            to._name += "+" + from._name;
            to._bodyParts = mergedParts.ToArray();
        }
    } 

}
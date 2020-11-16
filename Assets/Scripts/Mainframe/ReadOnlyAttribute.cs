
using System;
using UnityEngine;

namespace Scripts.MainframeReference
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute { }
}
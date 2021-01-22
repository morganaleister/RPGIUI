
using System;
using UnityEngine;

namespace Scripts.Mainframe
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute { }
}
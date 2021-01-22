using System.Collections;
using UnityEngine;

namespace Scripts.Mainframe
{
    public class Effects : MonoBehaviour
	{
		
		public void Cost(ref Gauge _gauge, float _ammount)
		{
			_gauge.Current -= _ammount;
		}

	}
}
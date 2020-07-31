using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomDebuger : MonoBehaviour
{
    public Text _text;

    public static Text s_text;

    private void Awake()
    {
        s_text = _text;
    }


}

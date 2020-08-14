using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCDebugger : MonoBehaviour
{
    public static SCDebugger Singleton { get; private set; }
    public static string Text
    {
        get => Singleton._CompText.text;
        set => Singleton._CompText.text = value;
    }


    [SerializeField] private Text _CompText;
  

    private void Awake()
    {
        if (!Singleton) Singleton = this;
        else Destroy(gameObject);
    }


}

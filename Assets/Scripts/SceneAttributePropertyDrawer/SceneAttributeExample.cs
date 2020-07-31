using UnityEngine;
class SceneAttributeExample : MonoBehaviour
{
    [Scene]
    public string unityScene;

    [Scene]
    [SerializeField]
    private string unityScene2;

}


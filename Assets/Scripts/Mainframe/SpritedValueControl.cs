using UnityEngine;

namespace Scripts.MainframeReference
{
    public class SpritedValueControl : ValueControl
    {

        [SerializeField] private SpriteRenderer _renderer;

        public Sprite Sprite
        {
            get => _renderer.sprite;
            set
            {
                _renderer.sprite = value;
                ValueChanged_Invoke(this, value);
            }
        }
    }
}
using UnityEngine;

namespace TryliomUtility
{
    public class TraditionalAnimator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TraditionalAnimation _animation;
        [SerializeField] [Tooltip("Optional")] private float _maxLifeTime;

        private void Start()
        {
            _animation.Init(_maxLifeTime);
            _spriteRenderer.sprite = _animation.GetNextSprite();
        }

        private void Update()
        {
            _spriteRenderer.sprite = _animation.GetNextSprite(Time.deltaTime);
        }
    }
}
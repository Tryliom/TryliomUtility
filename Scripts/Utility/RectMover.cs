using UnityEngine;

namespace TryliomUtility
{
    public class RectMover : MonoBehaviour
    {
        [SerializeField] private bool _preferLaterals;

        private Vector2 _originalPosition;

        private void Awake()
        {
            if (_originalPosition == Vector2.zero)
            {
                _originalPosition = transform.position;
            }
        }

        private void OnDisable()
        {
            transform.position = _originalPosition;
        }

        // Move this rect around the origin object to make it visible on screen
        public void Move(GameObject originObject, bool forceLaterals = false)
        {
            var originPosition = originObject.transform.position;
            var originRect = originObject.GetComponent<RectTransform>();
            var originSize = originRect.rect.size;

            Move(originPosition, originSize, forceLaterals);
        }

        public void Move(Vector2 originPosition, Vector2 originSize, bool forceLaterals = false)
        {
            const int offset = -30;
            var size = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
            var screenSize = new Vector2(Screen.width, Screen.height);
            var preferLaterals = _preferLaterals || forceLaterals;

            size *= transform.lossyScale;
            originSize *= transform.lossyScale;

            // If the window touch the top or bottom of the screen, enable laterals
            if (originPosition.y < size.y / 2f + offset + originSize.y ||
                originPosition.y > screenSize.y - size.y / 2f - offset - originSize.y)
            {
                preferLaterals = true;
            }

            if (preferLaterals)
            {
                if (originPosition.x < screenSize.x / 2f)
                {
                    originPosition.x += size.x / 2f + offset + originSize.x;
                }
                else
                {
                    originPosition.x -= size.x / 2f + offset + originSize.x;
                }

                originPosition.y = Mathf.Clamp(originPosition.y, size.y / 2f + offset + originSize.y,
                    screenSize.y - size.y / 2f - offset - originSize.y);
            }
            else
            {
                if (originPosition.x < screenSize.x / 2f)
                {
                    originPosition.x += size.x / 2f + offset + originSize.x;
                }
                else
                {
                    originPosition.x -= size.x / 2f + offset + originSize.x;
                }

                if (originPosition.y < screenSize.y / 2f)
                {
                    originPosition.y += size.y / 2f + offset + originSize.y;
                }
                else
                {
                    originPosition.y -= size.y / 2f + offset + originSize.y;
                }
            }

            transform.position = originPosition;
        }
    }
}
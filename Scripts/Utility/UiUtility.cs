using UnityEngine;

namespace TryliomUtility
{
    public static class UiUtility
    {
        /**
         * Resize a scroll view to fit its content.
         */
        public static void ResizeScrollView(RectTransform scrollView)
        {
            var minY = float.MaxValue;
            var maxY = float.MinValue;
            
            // Size of elements that are at top/bottom of the scroll view to calculate offset
            var sizeMinY = 0f;
            var sizeMaxY = 0f;
            
            foreach (RectTransform child in scrollView)
            {
                var y = child.localPosition.y;

                if (y > maxY)
                {
                    maxY = y;
                    sizeMaxY = Mathf.Max(sizeMaxY, child.rect.height / 2f);
                }
                
                if (y < minY)
                {
                    minY = y;
                    sizeMinY = Mathf.Max(sizeMinY, child.rect.height / 2f);
                }
            }
            
            var offsetY = -minY / 2f;
            var offset = sizeMaxY + sizeMinY;
            var totalHeight = -minY + offset;
            
            foreach (RectTransform child in scrollView)
            {
                var pos = child.localPosition;
                
                child.localPosition = new Vector3(pos.x, pos.y + offsetY, pos.z);
            }
            
            scrollView.sizeDelta = new Vector2(scrollView.sizeDelta.x, totalHeight);
        }
    }
}
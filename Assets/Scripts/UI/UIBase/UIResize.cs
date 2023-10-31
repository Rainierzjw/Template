using UnityEngine;

namespace UI
{
    public class UIResize : BaseUI
    {
        private UIEventDrag drag;

        protected override void OnLoaded()
        {
            if (parent == null)
            {
                Debug.LogWarning("parent is null");
                return;
            }

            drag = transform.gameObject.AddComponent<UIEventDrag>();
            drag.onDrag += OnDrag;
        }

        private void OnDrag(UIEventDrag arg1, Vector2 arg2)
        {
            parent.rectTransform.sizeDelta += arg2;
        }

        protected override void OnUnloaded()
        {
            base.OnUnloaded();

            drag.Dispose();
            drag = null;
        }
    }
}
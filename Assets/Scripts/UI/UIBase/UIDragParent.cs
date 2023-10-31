using UnityEngine;
using Misc;

namespace UI
{
    public class UIDragParent : BaseUI
    {
        private UIEventDrag drag;

        protected override void OnLoaded()
        {
            base.OnLoaded();

            drag = transform.gameObject.AddComponent<UIEventDrag>();
            drag.onDrag += OnDrag;
        }

        protected override void OnUnloaded()
        {
            drag.Dispose();
            drag = null;
        }

        private void OnDrag(UIEventDrag arg1, Vector2 arg2)
        {
            //            var pos = parent.tnf.anchoredPosition + arg2;
            //            pos.x = pos.x.Limit(0, Screen.width - parent.tnf.sizeDelta.x);
            //            pos.y = pos.y.Limit(-Screen.height + parent.tnf.sizeDelta.y,0);
            //            parent.tnf.anchoredPosition = pos;
            (transform as RectTransform).anchoredPosition += arg2;
        }
    }
}
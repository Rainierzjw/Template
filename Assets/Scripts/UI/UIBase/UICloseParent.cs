using UnityEngine.UI;

namespace UI
{
    public class UICloseParent : BaseUI
    {
        private Button btn_close;
        protected override void OnLoaded()
        {
            base.OnLoaded();

            btn_close = transform.GetComponent<Button>();
            btn_close.onClick.AddListener(() =>
            {
                parent.Hide();
            });
        }

        protected override void OnUnloaded()
        {
            base.OnUnloaded();
            
            btn_close.onClick.RemoveAllListeners();
            btn_close = null;
        }
    }
}
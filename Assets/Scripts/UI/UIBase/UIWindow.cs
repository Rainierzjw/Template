

using Misc;

namespace UI
{
    public class UIWindow : BaseUI
    {
        /// <summary>
        /// 拖拽的根节点,如果不指定不会加载
        /// </summary>
        public readonly WriteOnce<string> dragRoot = new WriteOnce<string>();

        /// <summary>
        /// 改变大小的根节点,如果不指定不会加载
        /// </summary>
        public readonly WriteOnce<string> resizeRoot = new WriteOnce<string>();

        /// <summary>
        /// 关闭的根节点,如果不指定不会加载
        /// </summary>
        public readonly WriteOnce<string> closeRoot = new WriteOnce<string>();

        public UIDragParent drag { get; private set; }
        public UIResize resize { get; private set; }
        public UICloseParent close { get; private set; }

        protected override void OnLoaded()
        {
            if (!dragRoot.empty)
            {
                drag = LoadChild<UIDragParent>(dragRoot);
            }

            if (!resizeRoot.empty)
            {
                resize = LoadChild<UIResize>(resizeRoot);
            }

            if (!closeRoot.empty)
            {
                close = LoadChild<UICloseParent>(closeRoot);
            }
        }
    }
}
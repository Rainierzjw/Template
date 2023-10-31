/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：InjectAttribute
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 默认IoC初始化器
    /// </summary>
    public class DefaultIoCInitializer
    {
        /// <summary>
        /// DefaultIoCInitializer 静态实例
        /// </summary>
        static DefaultIoCInitializer _instance = null;

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return _isInitialized;
            }
        }
        /// <summary>
        /// 是否已经初始化
        /// </summary>
        private bool _isInitialized = false;

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static DefaultIoCInitializer NewInstance()
        {
            if(_instance == null)
            {
                _instance = new DefaultIoCInitializer();
            }
            return _instance;
        }

        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private DefaultIoCInitializer()
        {

        }

        /// <summary>
        /// 强力清除数据
        /// </summary>
        public virtual void ForceClear()
        {
            if (!_isInitialized)
            {
                return;
            }

            DataModelPool dmpool = InjectService.Get<DataModelPool>();
            if(dmpool != null)
            {
                dmpool.Clear();
            }

            GameObjectPool gopool = InjectService.Get<GameObjectPool>();
            if(gopool != null)
            {
                gopool.Clear();
            }

            AssociatedRecordPool table = InjectService.Get<AssociatedRecordPool>();
            if(table != null)
            {
                table.Clear();
            }
        }

        /// <summary>
        /// 初始化IoC持久化
        /// </summary>
        public virtual void InitializeIoC()
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            //清理所有已经持久化的内容
            InjectService.UnregisterAll();

            if (InjectService.Get<DataModelPool>() == null)
            {
                //持久化EventDrivePool用来承载场景内所有EventDrivenBehaviour
                InjectService.RegisterSingleton(new DataModelPool());
            }

            if(InjectService.Get<GameObjectPool>() == null)
            {
                //持久化GameObject对象池
                InjectService.RegisterSingleton(new GameObjectPool());
            }
        }
    }
}

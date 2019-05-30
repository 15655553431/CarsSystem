using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBll
{
    public partial interface IApplicationInfoService : IBaseService<ApplicationInfo>
    {
        /// <summary>
        /// 用车申请通过接口，事务操作
        /// 1、司机变为忙碌状态
        /// 2、车辆变为忙碌状态
        /// 3、申请表更新
        /// </summary>
        /// <param name="ai"></param>
        /// <returns></returns>
        bool ApplicationPass(ApplicationInfo ai);

        /// <summary>
        /// 还车接口，事务操作
        /// 1、司机变为休息状态
        /// 2、车辆变为空闲状态
        /// 3、申请表更新为历史记录
        /// </summary>
        /// <param name="ai"></param>
        /// <returns></returns>
        bool ApplicationHistory(ApplicationInfo ai);
    }
}

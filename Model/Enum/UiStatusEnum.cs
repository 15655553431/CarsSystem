using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 司机状态枚举类
    /// </summary>
    public enum UiStatusEnum
    {

        /// <summary>
        /// 请假或需要休息状态  三级主管  30座以上
        /// </summary>
        Rest = 3,

        /// <summary>
        /// 忙碌工作状态，二级主管，15之30含
        /// </summary>
        Work = 2,

        /// <summary>
        /// 空闲可派活状态,普通主管，15座一下含
        /// </summary>
        Leisure = 1
    }
}

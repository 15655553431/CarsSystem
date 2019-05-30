using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 汽车状态枚举
    /// </summary>
    public enum CiStatusEnum
    {

        /// <summary>
        /// 保养或维修状态
        /// </summary>
        Maintain = 3,

        /// <summary>
        /// 忙碌工作状态
        /// </summary>
        Work = 2,

        /// <summary>
        /// 回库空闲可用状态
        /// </summary>
        Leisure = 1
    }
}

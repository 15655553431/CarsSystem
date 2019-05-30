using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 申请表状态枚举类
    /// </summary>
    public enum AiStatusEnum
    {
        /// <summary>
        /// 历史申请信息
        /// </summary>
        History = 4,

        /// <summary>
        /// 申请用车-审批通过
        /// </summary>
        Pass = 3,

        /// <summary>
        /// 申请用车-审批不通过
        /// </summary>
        NoPass = 2,

        /// <summary>
        /// 新申请用车-尚未获得审批
        /// </summary>
        NewApplicat = 1
    }
}

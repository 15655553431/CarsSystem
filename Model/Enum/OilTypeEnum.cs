using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 汽油类型枚举
    /// </summary>
    public enum OilTypeEnum
    {
        /// <summary>
        /// 新能源-混合动力
        /// </summary>
        Mix = 7,

        /// <summary>
        /// 新能源-纯电动
        /// </summary>
        Ele = 6,

        /// <summary>
        /// 新能源-天然气
        /// </summary>
        Gas = 5,

        /// <summary>
        /// 柴油
        /// </summary>
        Derv = 4,

        /// <summary>
        /// 97号汽油
        /// </summary>
        Oil97 = 3,

        /// <summary>
        /// 95号汽油
        /// </summary>
        Oil95 = 2,

        /// <summary>
        /// 92号汽油
        /// </summary>
        Oil92 = 1
    }
}

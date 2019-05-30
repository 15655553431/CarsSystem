using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Other
{
    //用来存储菜单信息的,管理员类型如果比较多，可以直接在数据库中建表
    public class Meansinfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">栏目名</param>
        /// <param name="url">url链接</param>
        /// <param name="layer">层级</param>
        /// <param name="pid">父id</param>
        /// <param name="type">所属管理员类型</param>
        public Meansinfo(int id, string name, string url, int layer, int pid, int type )
        {
            this.Id = id;
            this.Name = name;
            this.Url = url;
            this.Layer = layer;
            this.PId = pid;
            this.MType = type;
        }

        public int Id;
        public string Name;
        public string Url;
        public int Layer;
        public int PId;
        public int MType;

    }
}

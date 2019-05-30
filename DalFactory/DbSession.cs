
 using EFDal;
using IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalFactory
{
    public class DbSession:IDbSession
    {


		public IApplicationInfoDal ApplicationInfoDal
        {
            get { return StaticDalFactory.GetApplicationInfoDal(); }
        }


		public ICarsInfoDal CarsInfoDal
        {
            get { return StaticDalFactory.GetCarsInfoDal(); }
        }


		public INewsInfoDal NewsInfoDal
        {
            get { return StaticDalFactory.GetNewsInfoDal(); }
        }


		public IUserInfoDal UserInfoDal
        {
            get { return StaticDalFactory.GetUserInfoDal(); }
        }

		/// <summary>
        /// 拿到当前的EF的上下文，然后进行 把修改的实体进行一个整体提交
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
	}
}
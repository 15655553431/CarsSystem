
 using IBll;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bll
{


	public partial class ApplicationInfoService : BaseService<ApplicationInfo>, IApplicationInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ApplicationInfoDal;
        }
    }


	public partial class CarsInfoService : BaseService<CarsInfo>, ICarsInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.CarsInfoDal;
        }
    }


	public partial class NewsInfoService : BaseService<NewsInfo>, INewsInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.NewsInfoDal;
        }
    }


	public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoDal;
        }
    }


}
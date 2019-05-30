
 using IDal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal
{

	public partial class ApplicationInfoDal:BaseDal<ApplicationInfo>,IApplicationInfoDal
    {
       
    }

	public partial class CarsInfoDal:BaseDal<CarsInfo>,ICarsInfoDal
    {
       
    }

	public partial class NewsInfoDal:BaseDal<NewsInfo>,INewsInfoDal
    {
       
    }

	public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {
       
    }


}
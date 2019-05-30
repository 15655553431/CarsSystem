
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
    }
	public partial interface ICarsInfoService : IBaseService<CarsInfo>
    {
    }
	public partial interface INewsInfoService : IBaseService<NewsInfo>
    {
    }
	public partial interface IUserInfoService : IBaseService<UserInfo>
    {
    }

}
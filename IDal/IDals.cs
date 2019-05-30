
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDal
{


	public partial interface IApplicationInfoDal:IBaseDal<ApplicationInfo>
    {
    }



	public partial interface ICarsInfoDal:IBaseDal<CarsInfo>
    {
    }



	public partial interface INewsInfoDal:IBaseDal<NewsInfo>
    {
    }



	public partial interface IUserInfoDal:IBaseDal<UserInfo>
    {
    }



}
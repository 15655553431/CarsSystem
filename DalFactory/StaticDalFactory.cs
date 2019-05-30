
 using IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DalFactory
{
    public class StaticDalFactory
    {
		public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
		public static IApplicationInfoDal GetApplicationInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".ApplicationInfoDal") as IApplicationInfoDal;
        }
	

		public static ICarsInfoDal GetCarsInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".CarsInfoDal") as ICarsInfoDal;
        }
	

		public static INewsInfoDal GetNewsInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".NewsInfoDal") as INewsInfoDal;
        }
	

		public static IUserInfoDal GetUserInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".UserInfoDal") as IUserInfoDal;
        }
	

	}
}



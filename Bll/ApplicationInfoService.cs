using DalFactory;
using EFDal;
using IBll;
using IDal;
using Model;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{

    public partial class ApplicationInfoService : BaseService<ApplicationInfo>, IApplicationInfoService
    {

        private UserInfoService userinfoService = new UserInfoService();

        private CarsInfoService carsinfoService = new CarsInfoService();

        public bool ApplicationPass(ApplicationInfo ai)
        {

            UserInfo driverui = userinfoService.GetEntities(u => u.GUID == ai.DriverUGUID && u.Status == (int)UiStatusEnum.Leisure).FirstOrDefault();

            CarsInfo carsinfo = carsinfoService.GetEntities(u => u.GUID == ai.CGUID && u.Status == (int)CiStatusEnum.Leisure).FirstOrDefault();

            if (carsinfo != null && driverui != null)
            {
                driverui.Status = (int)UiStatusEnum.Work;
                carsinfo.Status = (int)CiStatusEnum.Work;

                //这里的事务操作涉及到三张表，按照这种方式完成事务
                userinfoService.ApplicationPassUi(driverui);
                carsinfoService.ApplicationPassCi(carsinfo);
                CurrentDal.Update(ai);

                return DbSession.SaveChanges() > 0;
            }

            return false;
        }


        public bool ApplicationHistory(ApplicationInfo ai)
        {
            UserInfo driverui = userinfoService.GetEntities(u => u.GUID == ai.DriverUGUID && u.Status == (int)UiStatusEnum.Work).FirstOrDefault();

            CarsInfo carsinfo = carsinfoService.GetEntities(u => u.GUID == ai.CGUID && u.Status == (int)CiStatusEnum.Work).FirstOrDefault();

            if (carsinfo != null && driverui != null)
            {
                driverui.Status = (int)UiStatusEnum.Leisure;
                driverui.Total += 1;
                driverui.Mileage += ai.Distance;
                carsinfo.Status = (int)CiStatusEnum.Leisure;
                ai.Status = (int)AiStatusEnum.History;
                ai.EndDate = DateTime.Now;
                userinfoService.ApplicationPassUi(driverui);
                carsinfoService.ApplicationPassCi(carsinfo);
                CurrentDal.Update(ai);

                return DbSession.SaveChanges() > 0;
            }

            return false;
        }
    }
}

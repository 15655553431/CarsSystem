using IBll;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public partial class CarsInfoService : BaseService<CarsInfo>, ICarsInfoService
    {
        public int ApplicationPassCi(CarsInfo ci)
        {
            CurrentDal.Update(ci);
            return 0;
        }
    }
}

using Bll;
using IBll;
using Model;
using Model.Enum;
using Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Common.Lambda;

namespace CarsSystem.Controllers
{
    public class CarsInfoController : Controller
    {
        //
        // GET: /CarsInfo/

        private IApplicationInfoService appinfoService = new ApplicationInfoService();

        private ICarsInfoService carsinfoService = new CarsInfoService();

        /// <summary>
        /// 空闲可用车辆展示
        /// </summary>
        /// <returns></returns>
        public ActionResult CarsIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CarsIndex(PagingInfo pi, CarsInfo ci)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ci.Number = String.IsNullOrWhiteSpace(ci.Number) ? "" : ci.Number;
            ci.LicencePlate = String.IsNullOrWhiteSpace(ci.LicencePlate) ? "" : ci.LicencePlate;

            Expression<Func<CarsInfo, bool>> wherelambda = u => (u.Status == (int)CiStatusEnum.Leisure && u.LicencePlate != null && u.Number != null && u.LicencePlate.Contains(ci.LicencePlate) && u.Number.Contains(ci.Number));

            if (ci.Seating > 0)
            {
                wherelambda=wherelambda.And(u=>u.Seating==ci.Seating);
            }
            int count = 0;
            var list = carsinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<CarsInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }



        /// <summary>
        /// 显示车辆信息
        /// </summary>
        /// <returns></returns>
        public ActionResult CarsShowIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CarsShowIndex(PagingInfo pi, CarsInfo ci)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ci.Number = String.IsNullOrWhiteSpace(ci.Number) ? "" : ci.Number;
            ci.LicencePlate = String.IsNullOrWhiteSpace(ci.LicencePlate) ? "" : ci.LicencePlate;

            Expression<Func<CarsInfo, bool>> wherelambda = u => ( u.LicencePlate != null && u.Number != null && u.LicencePlate.Contains(ci.LicencePlate) && u.Number.Contains(ci.Number));

            if (ci.Seating > 0)
            {
                wherelambda=wherelambda.And(u=>u.Seating==ci.Seating);
            }
            int count = 0;
            var list = carsinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<CarsInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }

        


        /// <summary>
        /// 在途车辆展示
        /// </summary>
        /// <returns></returns>
        public ActionResult CarsWorkIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CarsWorkIndex(PagingInfo pi, CarsInfo ci)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ci.Number = String.IsNullOrWhiteSpace(ci.Number) ? "" : ci.Number;
            ci.LicencePlate = String.IsNullOrWhiteSpace(ci.LicencePlate) ? "" : ci.LicencePlate;

            Expression<Func<CarsInfo, bool>> wherelambda = u => (u.Status == (int)CiStatusEnum.Work && u.LicencePlate != null && u.Number != null && u.LicencePlate.Contains(ci.LicencePlate) && u.Number.Contains(ci.Number));

            if (ci.Seating > 0)
            {
                wherelambda = wherelambda.And(u => u.Seating == ci.Seating);
            }
            int count = 0;
            var list = carsinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<CarsInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }


        /// <summary>
        /// 保养车辆
        /// </summary>
        /// <returns></returns>
        public ActionResult CarsMaintainIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CarsMaintainIndex(PagingInfo pi, CarsInfo ci)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ci.Number = String.IsNullOrWhiteSpace(ci.Number) ? "" : ci.Number;
            ci.LicencePlate = String.IsNullOrWhiteSpace(ci.LicencePlate) ? "" : ci.LicencePlate;

            Expression<Func<CarsInfo, bool>> wherelambda = u => (u.Status == (int)CiStatusEnum.Maintain && u.LicencePlate != null && u.Number != null && u.LicencePlate.Contains(ci.LicencePlate) && u.Number.Contains(ci.Number));

            if (ci.Seating > 0)
            {
                wherelambda = wherelambda.And(u => u.Seating == ci.Seating);
            }
            int count = 0;
            var list = carsinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<CarsInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }


        /// <summary>
        /// 创建车辆信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CarsInfo ci)
        {
            ci.GUID = Guid.NewGuid().ToString();
            ci.Status = (int)CiStatusEnum.Leisure;
            if (carsinfoService.Add(ci))
            {
                return Json("ok");
            }

            return Json("err");
        }


        public ActionResult Details(int id=0)
        {
            return View(carsinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }


        [HttpPost]
        public ActionResult Delete(int id=0)
        {
            CarsInfo ci = carsinfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            if (ci.Status==(int)CiStatusEnum.Leisure&&carsinfoService.Delete(ci))
            {
                return Json("ok");
            }

            return Json("err");
        }

        /// <summary>
        /// 设置汽车保养
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CarsMaintain(int id = 0)
        {
            CarsInfo ci = carsinfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            ci.Status = (int)CiStatusEnum.Maintain;
            if (carsinfoService.Update(ci))
            {
                return Json("ok");
            }

            return Json("err");
        }

        /// <summary>
        /// 设置正常回库，由保养转为正常
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CarsLeisure(int id = 0)
        {
            CarsInfo ci = carsinfoService.GetEntities(u => u.ID == id&&u.Status==(int)CiStatusEnum.Maintain).FirstOrDefault();
            if (ci != null)
            {
                ci.Status = (int)CiStatusEnum.Leisure;
                if (carsinfoService.Update(ci))
                {
                    return Json("ok");
                }
            }

            return Json("err");
        }


        /// <summary>
        /// 由在途转为回库，事务操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CarsWork2Leisure(int id = 0)
        {
            CarsInfo ci = carsinfoService.GetEntities(u => u.ID == id&&u.Status==(int)CiStatusEnum.Maintain).FirstOrDefault();
            if (ci != null)
            {
                ApplicationInfo ai = appinfoService.GetEntities(u => u.CGUID == ci.GUID && u.Status == (int)AiStatusEnum.Pass).FirstOrDefault();
                
                if (appinfoService.ApplicationHistory(ai))
                {
                    return Json("ok");
                }
            }

            return Json("err");
        }

        


        /// <summary>
        /// 汽车信息编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CarsEdit(int id = 0)
        {
            return View(carsinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult CarsEdit(CarsInfo ci)
        {
            CarsInfo ciedit = carsinfoService.GetEntities(u => u.ID == ci.ID).FirstOrDefault();
            if (ciedit != null)
            {
                ciedit.Number = ci.Number;
                ciedit.LicencePlate = ci.LicencePlate;
                ciedit.Mileage = ci.Mileage;
                ciedit.OilWear = ci.OilWear;
                ciedit.Seating = ci.Seating;
                ciedit.CarBrand = ci.CarBrand;
                ciedit.CarColor = ci.CarColor;
                ciedit.CarModel = ci.CarModel;
                if (carsinfoService.Update(ciedit))
                {
                    return Json("ok");
                }
            }
            return Json("err");
        }



        /// <summary>
        /// 获取空闲车辆信息
        /// </summary>
        /// <param name="seacher"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCarsInfo(string seacher)
        {
            return Json(carsinfoService.GetEntities(u => u.Status == (int)CiStatusEnum.Leisure).Take(5).ToList());
        }
        
        
    }
}

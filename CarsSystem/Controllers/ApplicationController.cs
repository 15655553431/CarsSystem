using Bll;
using Common.Cache;
using IBll;
using Model;
using Model.Enum;
using Model.Other;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace CarsSystem.Controllers
{
    public class ApplicationController : Controller
    {
        //
        // GET: /Application/

        private IApplicationInfoService appinfoService = new ApplicationInfoService();

        private ICarsInfoService carsinfoService = new CarsInfoService();

        private IUserInfoService userinfoService = new UserInfoService();


        /// <summary>
        /// 下载文件,等待审核的
        /// </summary>
        /// <returns></returns>
        public void downawait(int id = 0)
        {
            ApplicationInfo ai = appinfoService.GetEntities(u => u.ID == id).FirstOrDefault();

            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();

            //模板路径，从ajax的上一层test找到excel文件下的模板.xls
            string excelTempPath = Server.MapPath("~/Content/") + "ApplicationInfo.xls";
            //读取Excel模板
            using (FileStream fs = new FileStream(excelTempPath, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(fs);
            }

            ISheet sheet = workbook.GetSheetAt(0);//表示在模板的第一个工作簿里写入

            sheet.GetRow(1).GetCell(2).SetCellValue(ai.Number);
            sheet.GetRow(2).GetCell(1).SetCellValue(ai.AppName);
            sheet.GetRow(2).GetCell(3).SetCellValue(ai.ApplicationDate.ToString("yyyy年MM月dd日 HH:mm"));
            sheet.GetRow(3).GetCell(1).SetCellValue(ai.Origin);
            sheet.GetRow(3).GetCell(3).SetCellValue(ai.Destination);
            sheet.GetRow(4).GetCell(1).SetCellValue(ai.Distance.ToString() + "km");
            sheet.GetRow(4).GetCell(3).SetCellValue(ai.Department);
            sheet.GetRow(5).GetCell(2).SetCellValue(ai.EndDate.ToString("yyyy年MM月dd日 HH:mm"));
            sheet.GetRow(6).GetCell(1).SetCellValue(ai.Reason);


            //重新计算表格中所有公式
            sheet.ForceFormulaRecalculation = true;


            workbook.Write(ms);
            string fileName = ai.Number + "等待审批的用车申请单.xls";
            byte[] bytes = ms.ToArray();
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            HttpContext.Response.Clear();
            HttpContext.Response.ClearContent();
            HttpContext.Response.ClearHeaders();
            HttpContext.Response.Charset = "UTF-8";
            HttpContext.Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            HttpContext.Response.AddHeader("Content-Transfer-Encoding", "binary"); HttpContext.Response.BinaryWrite(bytes);
            HttpContext.Response.Flush();
            HttpContext.Response.End();

            
        }


        /// <summary>
        /// 下载文件,审核通过的
        /// </summary>
        /// <returns></returns>
        public void downpass(int id = 0)
        {
            ApplicationInfo ai = appinfoService.GetEntities(u => u.ID == id).FirstOrDefault();

            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();

            //模板路径，从ajax的上一层test找到excel文件下的模板.xls
            string excelTempPath = Server.MapPath("~/Content/") + "ApplicationInfo.xls";
            //读取Excel模板
            using (FileStream fs = new FileStream(excelTempPath, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(fs);
            }

            ISheet sheet = workbook.GetSheetAt(0);//表示在模板的第一个工作簿里写入

            sheet.GetRow(1).GetCell(2).SetCellValue(ai.Number);
            sheet.GetRow(2).GetCell(1).SetCellValue(ai.AppName);
            sheet.GetRow(2).GetCell(3).SetCellValue(ai.ApplicationDate.ToString("yyyy年MM月dd日 HH:mm"));
            sheet.GetRow(3).GetCell(1).SetCellValue(ai.Origin);
            sheet.GetRow(3).GetCell(3).SetCellValue(ai.Destination);
            sheet.GetRow(4).GetCell(1).SetCellValue(ai.Distance.ToString() + "km");
            sheet.GetRow(4).GetCell(3).SetCellValue(ai.Department);
            sheet.GetRow(5).GetCell(2).SetCellValue(ai.EndDate.ToString("yyyy年MM月dd日 HH:mm"));
            sheet.GetRow(6).GetCell(1).SetCellValue(ai.Reason);
            sheet.GetRow(7).GetCell(1).SetCellValue(ai.LeadName);
            sheet.GetRow(8).GetCell(1).SetCellValue(ai.DriverName);
            sheet.GetRow(8).GetCell(3).SetCellValue(ai.LicencePlate);
            sheet.GetRow(9).GetCell(1).SetCellValue("用车申请通过！请按照申请单进行派车！");
            


            //重新计算表格中所有公式
            sheet.ForceFormulaRecalculation = true;


            workbook.Write(ms);
            string fileName = ai.Number + "审核通过用车申请单.xls";
            byte[] bytes = ms.ToArray();
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            HttpContext.Response.Clear();
            HttpContext.Response.ClearContent();
            HttpContext.Response.ClearHeaders();
            HttpContext.Response.Charset = "UTF-8";
            HttpContext.Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            HttpContext.Response.AddHeader("Content-Transfer-Encoding", "binary"); HttpContext.Response.BinaryWrite(bytes);
            HttpContext.Response.Flush();
            HttpContext.Response.End();


        }


        /// <summary>
        /// 下载申请单，审核不通过的
        /// </summary>
        /// <param name="id"></param>
        public void downnopass(int id = 0)
        {
            ApplicationInfo ai = appinfoService.GetEntities(u => u.ID == id).FirstOrDefault();

            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();

            //模板路径，从ajax的上一层test找到excel文件下的模板.xls
            string excelTempPath = Server.MapPath("~/Content/") + "ApplicationInfo.xls";
            //读取Excel模板
            using (FileStream fs = new FileStream(excelTempPath, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(fs);
            }

            ISheet sheet = workbook.GetSheetAt(0);//表示在模板的第一个工作簿里写入

            sheet.GetRow(1).GetCell(2).SetCellValue(ai.Number);
            sheet.GetRow(2).GetCell(1).SetCellValue(ai.AppName);
            sheet.GetRow(2).GetCell(3).SetCellValue(ai.ApplicationDate.ToString("yyyy年MM月dd日 HH:mm"));
            sheet.GetRow(3).GetCell(1).SetCellValue(ai.Origin);
            sheet.GetRow(3).GetCell(3).SetCellValue(ai.Destination);
            sheet.GetRow(4).GetCell(1).SetCellValue(ai.Distance.ToString() + "km");
            sheet.GetRow(4).GetCell(3).SetCellValue(ai.Department);
            sheet.GetRow(5).GetCell(2).SetCellValue(ai.EndDate.ToString("yyyy年MM月dd日 HH:mm"));
            sheet.GetRow(6).GetCell(1).SetCellValue(ai.Reason);
            sheet.GetRow(7).GetCell(0).SetCellValue("驳回人");
            sheet.GetRow(7).GetCell(1).SetCellValue(ai.LeadName);
            sheet.GetRow(9).GetCell(1).SetCellValue("用车申请不通过！请重新申请！");
            


            //重新计算表格中所有公式
            sheet.ForceFormulaRecalculation = true;


            workbook.Write(ms);
            string fileName = ai.Number + "驳回申请单.xls";
            byte[] bytes = ms.ToArray();
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            HttpContext.Response.Clear();
            HttpContext.Response.ClearContent();
            HttpContext.Response.ClearHeaders();
            HttpContext.Response.Charset = "UTF-8";
            HttpContext.Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            HttpContext.Response.AddHeader("Content-Transfer-Encoding", "binary"); HttpContext.Response.BinaryWrite(bytes);
            HttpContext.Response.Flush();
            HttpContext.Response.End();


        }

        

        /// <summary>
        /// 等待申请用车列表
        /// </summary>
        /// <returns></returns>
        public ActionResult AwaitIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AwaitIndex(PagingInfo pi, ApplicationInfo ai)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ai.Number = String.IsNullOrWhiteSpace(ai.Number) ? "" : ai.Number;
            ai.Reason = String.IsNullOrWhiteSpace(ai.Reason) ? "" : ai.Reason;

            UserInfo userInfo = new UserInfo();
            userInfo.GUID = "";

            if (Request.Cookies["userLoginId"] != null)
            {
                string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
            }

            Expression<Func<ApplicationInfo, bool>> wherelambda = u => (u.Status == (int)AiStatusEnum.NewApplicat && u.AppUGUID == userInfo.GUID && u.Reason != null && u.Number != null && u.Reason.Contains(u.Reason) && u.Number.Contains(ai.Number));

            int count = 0;
            var list = appinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<ApplicationInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }


        /// <summary>
        /// 历史申请用车信息
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoryIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult HistoryIndex(PagingInfo pi, ApplicationInfo ai)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ai.Number = String.IsNullOrWhiteSpace(ai.Number) ? "" : ai.Number;
            ai.Reason = String.IsNullOrWhiteSpace(ai.Reason) ? "" : ai.Reason;

            UserInfo userInfo = new UserInfo();
            userInfo.GUID = "";

            if (Request.Cookies["userLoginId"] != null)
            {
                string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
            }

            Expression<Func<ApplicationInfo, bool>> wherelambda = u => (u.Status == (int)AiStatusEnum.History && u.AppUGUID == userInfo.GUID && u.Reason != null && u.Number != null && u.Reason.Contains(u.Reason) && u.Number.Contains(ai.Number));

            int count = 0;
            var list = appinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<ApplicationInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }




        /// <summary>
        /// 主管历史申请用车信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SatrapHistoryIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SatrapHistoryIndex(PagingInfo pi, ApplicationInfo ai)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ai.Number = String.IsNullOrWhiteSpace(ai.Number) ? "" : ai.Number;
            ai.Reason = String.IsNullOrWhiteSpace(ai.Reason) ? "" : ai.Reason;


            Expression<Func<ApplicationInfo, bool>> wherelambda = u => (u.Status == (int)AiStatusEnum.History && u.Reason != null && u.Number != null && u.Reason.Contains(u.Reason) && u.Number.Contains(ai.Number));

            int count = 0;
            var list = appinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<ApplicationInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }



        /// <summary>
        /// 设置申请用车已经回库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Pass2History(int id = 0)
        {
            ApplicationInfo ai = appinfoService.GetEntities(u => u.ID == id && u.Status == (int)AiStatusEnum.Pass).FirstOrDefault();
            if (ai != null)
            {
                if (appinfoService.ApplicationHistory(ai))
                {
                    return Json("ok");
                }
            }
            return HttpNotFound();
        }

        /// <summary>
        /// 主管等待审批页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SatrapAwaitIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SatrapAwaitIndex(PagingInfo pi, ApplicationInfo ai)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ai.Number = String.IsNullOrWhiteSpace(ai.Number) ? "" : ai.Number;
            ai.Reason = String.IsNullOrWhiteSpace(ai.Reason) ? "" : ai.Reason;

            Expression<Func<ApplicationInfo, bool>> wherelambda = u => (u.Status == (int)AiStatusEnum.NewApplicat && u.Reason != null && u.Number != null && u.Reason.Contains(u.Reason) && u.Number.Contains(ai.Number));

            int count = 0;
            var list = appinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<ApplicationInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }


        /// <summary>
        /// 管理员查看等待审批记录
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminAwaitIndex()
        {
            return View();
        }

        

        /// <summary>
        /// 准备审核通过，安排车辆和司机界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Pass(int id=0)
        {
            return View(appinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }

        
        [HttpPost]
        public ActionResult Pass(ApplicationInfo ai)
        {
            if (Request.Cookies["userLoginId"] != null)
            {
                //从缓存里面拿到userLoginId信息
                string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                UserInfo leadui = CacheHelper.GetCache(userLoginId) as UserInfo;

                UserInfo driverui = userinfoService.GetEntities(u => u.GUID == ai.DriverUGUID && u.Status == (int)UiStatusEnum.Leisure).FirstOrDefault();
                CarsInfo carsinfo = carsinfoService.GetEntities(u => u.GUID == ai.CGUID && u.Status == (int)CiStatusEnum.Leisure).FirstOrDefault();
                ApplicationInfo aipass = appinfoService.GetEntities(u => u.GUID == ai.GUID && u.Status == (int)AiStatusEnum.NewApplicat).FirstOrDefault();

                if (leadui != null && driverui != null && carsinfo != null && aipass != null)
                {
                    aipass.CGUID = carsinfo.GUID;
                    aipass.LicencePlate = carsinfo.LicencePlate;

                    aipass.DriverUGUID = driverui.GUID;
                    aipass.DriverName = driverui.Name;
                    aipass.DriverLogin = driverui.Login;

                    aipass.LeadUGUID = leadui.GUID;
                    aipass.LeadName = leadui.Name;
                    aipass.LeadLogin = leadui.Login;

                    aipass.Status = (int)AiStatusEnum.Pass;

                    if (leadui.Status == 1 && carsinfo.Seating > 15)
                    {
                        return Json("err");
                    }

                    if (leadui.Status == 2 && carsinfo.Seating > 30)
                    {
                        return Json("err");
                    }

                    //事务操作,司机需要变为工作状态，车辆需要变为忙碌状态，申请表更新
                    if (appinfoService.ApplicationPass(aipass))
                    {
                        return Json("ok");
                    }
                }


            }
            return Json("err");
        }


        /// <summary>
        /// 驳回申请
        /// </summary>
        /// <param name="ai"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NoPass(int id=0)
        {
            if (Request.Cookies["userLoginId"] != null)
            {
                //从缓存里面拿到userLoginId信息
                string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                UserInfo leadui = CacheHelper.GetCache(userLoginId) as UserInfo;

                ApplicationInfo aipass = appinfoService.GetEntities(u => u.ID == id && u.Status == (int)AiStatusEnum.NewApplicat).FirstOrDefault();

                if (leadui != null && aipass != null)
                {
                    aipass.LeadUGUID = leadui.GUID;
                    aipass.LeadName = leadui.Name;
                    aipass.LeadLogin = leadui.Login;

                    aipass.Status = (int)AiStatusEnum.NoPass;

                    if (appinfoService.Update(aipass))
                    {
                        return Json("ok");
                    }
                }


            }
            return Json("err");
        }
        




        /// <summary>
        /// 通过的申请
        /// </summary>
        /// <returns></returns>
        public ActionResult PassIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PassIndex(PagingInfo pi, ApplicationInfo ai)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ai.Number = String.IsNullOrWhiteSpace(ai.Number) ? "" : ai.Number;
            ai.Reason = String.IsNullOrWhiteSpace(ai.Reason) ? "" : ai.Reason;

            UserInfo userInfo = new UserInfo();
            userInfo.GUID = "";

            if (Request.Cookies["userLoginId"] != null)
            {
                string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
            }

            Expression<Func<ApplicationInfo, bool>> wherelambda = u => (u.Status == (int)AiStatusEnum.Pass && u.AppUGUID == userInfo.GUID && u.Reason != null && u.Number != null && u.Reason.Contains(u.Reason) && u.Number.Contains(ai.Number));

            int count = 0;
            var list = appinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<ApplicationInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }




        /// <summary>
        /// 通过的申请(主管)
        /// </summary>
        /// <returns></returns>
        public ActionResult SatrapPassIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SatrapPassIndex(PagingInfo pi, ApplicationInfo ai)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ai.Number = String.IsNullOrWhiteSpace(ai.Number) ? "" : ai.Number;
            ai.Reason = String.IsNullOrWhiteSpace(ai.Reason) ? "" : ai.Reason;

          
            Expression<Func<ApplicationInfo, bool>> wherelambda = u => (u.Status == (int)AiStatusEnum.Pass && u.Reason != null && u.Number != null && u.Reason.Contains(u.Reason) && u.Number.Contains(ai.Number));

            int count = 0;
            var list = appinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<ApplicationInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }




        /// <summary>
        /// 驳回的申请
        /// </summary>
        /// <returns></returns>
        public ActionResult NoPassIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NoPassIndex(PagingInfo pi, ApplicationInfo ai)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ai.Number = String.IsNullOrWhiteSpace(ai.Number) ? "" : ai.Number;
            ai.Reason = String.IsNullOrWhiteSpace(ai.Reason) ? "" : ai.Reason;

            UserInfo userInfo = new UserInfo();
            userInfo.GUID = "";

            if (Request.Cookies["userLoginId"] != null)
            {
                string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
            }

            Expression<Func<ApplicationInfo, bool>> wherelambda = u => (u.Status == (int)AiStatusEnum.NoPass && u.AppUGUID == userInfo.GUID && u.Reason != null && u.Number != null && u.Reason.Contains(u.Reason) && u.Number.Contains(ai.Number));

            int count = 0;
            var list = appinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<ApplicationInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }



        /// <summary>
        /// 驳回的申请
        /// </summary>
        /// <returns></returns>
        public ActionResult SatrapNoPassIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SatrapNoPassIndex(PagingInfo pi, ApplicationInfo ai)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ai.Number = String.IsNullOrWhiteSpace(ai.Number) ? "" : ai.Number;
            ai.Reason = String.IsNullOrWhiteSpace(ai.Reason) ? "" : ai.Reason;

            Expression<Func<ApplicationInfo, bool>> wherelambda = u => (u.Status == (int)AiStatusEnum.NoPass && u.Reason != null && u.Number != null && u.Reason.Contains(u.Reason) && u.Number.Contains(ai.Number));

            int count = 0;
            var list = appinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<ApplicationInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            if (appinfoService.Delete(appinfoService.GetEntities(u => u.ID == id).FirstOrDefault()))
            {
                return Json("ok");
            }
            return Json("err");
        }

        /// <summary>
        /// 创建新申请
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ApplicationInfo ai)
        {
            if (Request.Cookies["userLoginId"] != null)
            {
                //从缓存里面拿到userLoginId信息
                string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                UserInfo userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
                if (userInfo != null)
                {
                    ai.GUID = Guid.NewGuid().ToString();
                    ai.AppLogin = userInfo.Login;
                    ai.AppName = userInfo.Name;
                    ai.AppUGUID = userInfo.GUID;
                    ai.Department = userInfo.Department;

                    ai.CGUID = "空";
                    ai.DriverLogin = "空";
                    ai.DriverName = "空";
                    ai.DriverUGUID = "空";
                    ai.LeadLogin = "空";
                    ai.LeadName = "空";
                    ai.LeadUGUID = "空";
                    ai.LicencePlate = "空";

                    ai.GoDate = DateTime.Now;
                    ai.ApplicationDate = DateTime.Now;
                    ai.Status = (int)AiStatusEnum.NewApplicat;

                    ApplicationInfo aimax = appinfoService.GetEntities(u => true).OrderByDescending(u => u.ID).FirstOrDefault();
                    if (aimax == null)
                    {
                        ai.Number = "320821000001";
                    }
                    else
                    {
                        ai.Number = "32082" + (Convert.ToInt32(aimax.Number.Substring(5, 7)) + 1).ToString();
                    }

                    if (appinfoService.Add(ai))
                    {
                        return Json("ok");
                    }
                }
            }
            return Json("err");
        }


        /// <summary>
        /// 待审核的申请记录详情
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffDetail(int id=0)
        {
            return View(appinfoService.GetEntities(u => u.ID == id && u.Status == (int)AiStatusEnum.NewApplicat).FirstOrDefault());
        }


        /// <summary>
        /// 申请通过的详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PassDetail(int id = 0)
        {
            return View(appinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }

        /// <summary>
        /// 被驳回的申请详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult NoPassDetail(int id = 0)
        {
            return View(appinfoService.GetEntities(u => u.ID == id && u.Status == (int)AiStatusEnum.NoPass).FirstOrDefault());
        }
        
        
        /// <summary>
        /// 编辑新申请
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffEdit(int id=0)
        {
            return View(appinfoService.GetEntities(u => u.ID == id && u.Status == (int)AiStatusEnum.NewApplicat).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult StaffEdit(ApplicationInfo ai)
        {
            ApplicationInfo aiedit = appinfoService.GetEntities(u => u.ID == ai.ID && u.Status == (int)AiStatusEnum.NewApplicat).FirstOrDefault();

            aiedit.GoDate = DateTime.Now;
            aiedit.ApplicationDate = DateTime.Now;

            aiedit.Origin = ai.Origin;
            aiedit.Destination = ai.Destination;
            aiedit.Distance = ai.Distance;
            aiedit.EndDate = ai.EndDate;
            aiedit.Reason = ai.Reason;
            
            if (appinfoService.Update(aiedit))
            {
                return Json("ok");
            }
  
            return Json("err");
        }

        
    }
}

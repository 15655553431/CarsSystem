using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Common.Md5;
using IBll;
using Bll;
using CarsSystem.Models;
using Common.VCode;
using Common.Cache;
using Model.Other;
using System.Linq.Expressions;
using Model.Enum;

namespace CarsSystem.Controllers
{
    public class UserInfoController : Controller
    {
        private IApplicationInfoService appinfoService = new ApplicationInfoService();

        private IUserInfoService userinfoService = new UserInfoService();
        //
        // GET: /UserInfo/


        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult Login()
        {
            return View();
        }

        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult ShowVCode()
        {
            VCodeHelper validateCode = new VCodeHelper();
            string strCode = validateCode.RandomCode(4);

            //把验证码放到session中
            Session["VCode"] = strCode;

            byte[] imgBytes = validateCode.CreatVCodeImgs(strCode);

            return File(imgBytes, "image/jpeg");
        }

        /// <summary>
        /// 登陆信息验证
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult ProcessLogin()
        {
            //1 处理验证码

            string strCode = Request["vcode"];
            //拿到session中的验证码做对比
            string sessionCode = Session["VCode"] as string;

            //取过验证码，马上置空
            Session["VCode"] = null;

            //在测试阶段可以注释这段，就不需要填写验证码了
            //if (string.IsNullOrEmpty(sessionCode) || strCode != sessionCode)
            //{
            //    // "alert('ok！');", "text/javascript"
            //    return Json("验证码错误！");
            //}


            //2 处理用户名和密码
            string login = Request["login"];
            string pwd = Request["pwd"].GetMd5();

            var userinfo = userinfoService.GetEntities(u => u.Login == login && u.Pwd == pwd ).FirstOrDefault();

            if (userinfo == null)
            {
                return Json("用户名或密码错误!");
            }

            //登录成功，把用户信息放到Cache里,记录登陆时间
            userinfo.LoginDate = DateTime.Now;
            userinfoService.Update(userinfo);
            //1,立即分配一个标志，guid，作为key存入mm，同时把这个key存入客户端cookie
            string userLoginId = Guid.NewGuid().ToString();

            //把用户数据写入缓存
            CacheHelper.AddCache(userLoginId, userinfo, DateTime.Now.AddMinutes(20));

            //往客户端写入cookie
            Response.Cookies["userLoginId"].Value = userLoginId;


            //正确跳转到首页
            //return RedirectToAction("Test","Index");

            return Json("ok");

        }

        /// <summary>
        /// 登陆成功后，获取跳转的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ActionUrl()
        {
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 退出登陆请求
        /// </summary>
        /// <returns></returns>
        public ActionResult ExitLogin()
        {
            string userloginid = Request.Cookies["userLoginId"].Value.ToString();
            //登陆信息置空
            if (!string.IsNullOrWhiteSpace(userloginid))
            {
                CacheHelper.RemCache(userloginid);
            }

            return RedirectToAction("Login");
        }





        /// <summary>
        /// 管理员账户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AdminIndex(PagingInfo pi, UserInfo ui)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ui.Phone = String.IsNullOrWhiteSpace(ui.Phone) ? "" : ui.Phone;
            ui.Name = String.IsNullOrWhiteSpace(ui.Name) ? "" : ui.Name;
            ui.Login = String.IsNullOrWhiteSpace(ui.Login) ? "" : ui.Login;

            Expression<Func<UserInfo, bool>> wherelambda = u => (u.Type == (int)UiTypeEnum.Admin && u.Phone != null && u.Name != null && u.Login != null && u.Phone.Contains(ui.Phone) && u.Name.Contains(ui.Name) && u.Login.Contains(ui.Login));

            int count = 0;
            var guestlist = userinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<UserInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(guestlist, pi));
        }

        [HttpPost]
        public ActionResult UserInfoDelete(int id = 0)
        {
            UserInfo userinfo = userinfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            if (userinfo == null)
            {
                return HttpNotFound();
            }
            else
            {
                string userloginid = Request.Cookies["userLoginId"].Value.ToString();
                UserInfo loginui = CacheHelper.GetCache<UserInfo>(userloginid);
                if (loginui == null || loginui.GUID == userinfo.GUID)
                {
                    return HttpNotFound();
                }
                else
                {
                    userinfoService.Delete(userinfo);
                }
                
            }

            return Json("ok");
        }



        /// <summary>
        /// 修改密码界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditPwd(int id = 0)
        {
            UserInfo userinfo = userinfoService.GetEntities(u => u.ID == id).First() as UserInfo;
            if (userinfo == null)
            {
                return HttpNotFound();
            }
            return View(userinfo);
        }


        [HttpPost]
        public ActionResult EditPwd(UserInfo userinfo)
        {
            if (userinfoService.EditPwd(userinfo.ID, userinfo.Pwd))
            {
                return Json("ok");
            }
            return View(userinfo);
        }


        /// <summary>
        /// 管理员账户编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminEdit(int id = 0)
        {

            return View(userinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult AdminEdit(UserInfo ui)
        {
            UserInfo uiedit = userinfoService.GetEntities(u => u.ID == ui.ID).FirstOrDefault();
            uiedit.Name = ui.Name;
            uiedit.Phone = ui.Phone;
            uiedit.Gender = ui.Gender;
            if (uiedit.Type == (int)UiTypeEnum.Satrap)
            {
                uiedit.Status = ui.Status;
            }

            if (userinfoService.Update(uiedit))
            {
                return Json("ok");
            }
            return Json("err");
        }

        /// <summary>
        /// 管理员账户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AdminDetail(int id = 0)
        {

            return View(userinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }


        /// <summary>
        /// 创建新管理员账户
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAdmin(UserInfo ui)
        {
            ui.AddDate = DateTime.Now;
            ui.Department = "管理员";
            ui.GUID = Guid.NewGuid().ToString();
            ui.OfficeDate = DateTime.Now;
            ui.Pwd = "123456".GetMd5();
            ui.Type = (int)UiTypeEnum.Admin;
            ui.LoginDate = DateTime.Now;
            ui.Status = (int)UiStatusEnum.Leisure;
            if (!userinfoService.LoginExist(ui.Login))
            {
                if (userinfoService.Add(ui))
                {
                    return Json("ok");
                }
            }


            return Json("err");
        }




        /// <summary>
        /// 主管账户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SatrapIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SatrapIndex(PagingInfo pi, UserInfo ui)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ui.Phone = String.IsNullOrWhiteSpace(ui.Phone) ? "" : ui.Phone;
            ui.Name = String.IsNullOrWhiteSpace(ui.Name) ? "" : ui.Name;
            ui.Login = String.IsNullOrWhiteSpace(ui.Login) ? "" : ui.Login;

            Expression<Func<UserInfo, bool>> wherelambda = u => (u.Type == (int)UiTypeEnum.Satrap && u.Phone != null && u.Name != null && u.Login != null && u.Phone.Contains(ui.Phone) && u.Name.Contains(ui.Name) && u.Login.Contains(ui.Login));

            int count = 0;
            var guestlist = userinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<UserInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(guestlist, pi));
        }



        /// <summary>
        /// 创建新主管账户
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateSatrap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSatrap(UserInfo ui)
        {
            ui.AddDate = DateTime.Now;
            ui.Department = "主管";
            ui.GUID = Guid.NewGuid().ToString();
            ui.OfficeDate = DateTime.Now;
            ui.Pwd = "123456".GetMd5();
            ui.Type = (int)UiTypeEnum.Satrap;
            ui.LoginDate = DateTime.Now;
            ui.Status = ui.Status;
            if (!userinfoService.LoginExist(ui.Login))
            {
                if (userinfoService.Add(ui))
                {
                    return Json("ok");
                }
            }


            return Json("err");
        }


        /// <summary>
        /// 职工账户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult StaffIndex(PagingInfo pi, UserInfo ui)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;
            ui.Phone = String.IsNullOrWhiteSpace(ui.Phone) ? "" : ui.Phone;
            ui.Name = String.IsNullOrWhiteSpace(ui.Name) ? "" : ui.Name;
            ui.Login = String.IsNullOrWhiteSpace(ui.Login) ? "" : ui.Login;

            Expression<Func<UserInfo, bool>> wherelambda = u => (u.Type == (int)UiTypeEnum.Staff && u.Phone != null && u.Name != null && u.Login != null && u.Phone.Contains(ui.Phone) && u.Name.Contains(ui.Name) && u.Login.Contains(ui.Login));

            int count = 0;
            var guestlist = userinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<UserInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(guestlist, pi));
        }


        /// <summary>
        /// 创建新普通职员账户
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateStaff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStaff(UserInfo ui)
        {
            ui.AddDate = DateTime.Now;
            ui.GUID = Guid.NewGuid().ToString();
            //ui.Department = "司机";
            ui.Pwd = "123456".GetMd5();
            ui.Type = (int)UiTypeEnum.Staff;
            ui.LoginDate = DateTime.Now;
            ui.Status = (int)UiStatusEnum.Leisure;
            if (!userinfoService.LoginExist(ui.Login))
            {
                if (userinfoService.Add(ui))
                {
                    return Json("ok");
                }
            }


            return Json("err");
        }


        /// <summary>
        /// 职工账户编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffEdit(int id = 0)
        {

            return View(userinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult StaffEdit(UserInfo ui)
        {
            UserInfo uiedit = userinfoService.GetEntities(u => u.ID == ui.ID).FirstOrDefault();
            uiedit.Name = ui.Name;
            uiedit.Phone = ui.Phone;
            uiedit.Gender = ui.Gender;
            uiedit.Department = ui.Department;

            if (userinfoService.Update(uiedit))
            {
                return Json("ok");
            }
            return Json("err");
        }

        /// <summary>
        /// 职工账户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult StaffDetails(int id = 0)
        {
            return View(userinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }


        /// <summary>
        /// 所有司机展示
        /// </summary>
        /// <returns></returns>
        public ActionResult DriverShowIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DriverShowIndex(PagingInfo pi, UserInfo ui)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;
            ui.Phone = String.IsNullOrWhiteSpace(ui.Phone) ? "" : ui.Phone;
            ui.Name = String.IsNullOrWhiteSpace(ui.Name) ? "" : ui.Name;
            ui.Login = String.IsNullOrWhiteSpace(ui.Login) ? "" : ui.Login;

            Expression<Func<UserInfo, bool>> wherelambda = u => (u.Type == (int)UiTypeEnum.Driver && u.Phone != null && u.Name != null && u.Login != null && u.Phone.Contains(ui.Phone) && u.Name.Contains(ui.Name) && u.Login.Contains(ui.Login));

            int count = 0;
            var guestlist = userinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<UserInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(guestlist, pi));
        }

        

        /// <summary>
        /// 司机管理(空闲司机)
        /// </summary>
        /// <returns></returns>
        public ActionResult DriverIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DriverIndex(PagingInfo pi, UserInfo ui)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;
            ui.Phone = String.IsNullOrWhiteSpace(ui.Phone) ? "" : ui.Phone;
            ui.Name = String.IsNullOrWhiteSpace(ui.Name) ? "" : ui.Name;
            ui.Login = String.IsNullOrWhiteSpace(ui.Login) ? "" : ui.Login;

            Expression<Func<UserInfo, bool>> wherelambda = u => (u.Type == (int)UiTypeEnum.Driver && u.Status == (int)UiStatusEnum.Leisure && u.Phone != null && u.Name != null && u.Login != null && u.Phone.Contains(ui.Phone) && u.Name.Contains(ui.Name) && u.Login.Contains(ui.Login));

            int count = 0;
            var guestlist = userinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<UserInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(guestlist, pi));
        }


        /// <summary>
        /// 创建新司机账户
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateDriver()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDriver(UserInfo ui)
        {
            ui.AddDate = DateTime.Now;
            ui.GUID = Guid.NewGuid().ToString();
            ui.Department = "司机";
            ui.OfficeDate = DateTime.Now;
            ui.Pwd = "123456".GetMd5();
            ui.Type = (int)UiTypeEnum.Driver;
            ui.LoginDate = DateTime.Now;
            ui.Status = (int)UiStatusEnum.Leisure;
            ui.Mileage = 0;
            ui.Total = 0;
            if (!userinfoService.LoginExist(ui.Login))
            {
                if (userinfoService.Add(ui))
                {
                    return Json("ok");
                }
            }

            return Json("err");
        }


        /// <summary>
        /// 司机详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DriverDetails(int id = 0)
        {
            return View(userinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }


        /// <summary>
        /// 司机账户编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult DriverEdit(int id = 0)
        {

            return View(userinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DriverEdit(UserInfo ui)
        {
            UserInfo uiedit = userinfoService.GetEntities(u => u.ID == ui.ID).FirstOrDefault();
            uiedit.Name = ui.Name;
            uiedit.Phone = ui.Phone;
            uiedit.Gender = ui.Gender;

            if (userinfoService.Update(uiedit))
            {
                return Json("ok");
            }
            return Json("err");
        }

        
        /// <summary>
        /// 设置司机为请假休息状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DriverRest(int id=0)
        {
            UserInfo ui = userinfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            if (ui != null)
            {
                ui.Status = (int)UiStatusEnum.Rest;
                if (userinfoService.Update(ui))
                {
                    return Json("ok");
                }
            }
            return Json("err");
        }

        


        /// <summary>
        /// 司机由工作状态设置为休息等待工作状态
        /// 获取到司机当前的申请单
        /// 把申请单修改好，提交到bll事务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DriverWork2Leisure(int id = 0)
        {
            UserInfo ui = userinfoService.GetEntities(u => u.ID == id&&u.Status==(int)UiStatusEnum.Work).FirstOrDefault();
            if (ui != null)
            {
                ApplicationInfo ai = appinfoService.GetEntities(u => u.DriverUGUID == ui.GUID && u.Status == (int)AiStatusEnum.Pass).FirstOrDefault();

                if (appinfoService.ApplicationHistory(ai))
                {
                    return Json("ok");
                }
            }
            return Json("err");
        }


        /// <summary>
        /// 由休假状态，转为等待分派工作状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DriverRest2Leisure(int id = 0)
        {
            UserInfo ui = userinfoService.GetEntities(u => u.ID == id&&u.Status==(int)UiStatusEnum.Rest).FirstOrDefault();
            if (ui != null)
            {
                ui.Status = (int)UiStatusEnum.Leisure;
                if (userinfoService.Update(ui))
                {
                    return Json("ok");
                }
            }
            return Json("err");
        }

        

        /// <summary>
        /// 司机管理(在途司机)
        /// </summary>
        /// <returns></returns>
        public ActionResult DriverWorkIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DriverWorkIndex(PagingInfo pi, UserInfo ui)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;
            ui.Phone = String.IsNullOrWhiteSpace(ui.Phone) ? "" : ui.Phone;
            ui.Name = String.IsNullOrWhiteSpace(ui.Name) ? "" : ui.Name;
            ui.Login = String.IsNullOrWhiteSpace(ui.Login) ? "" : ui.Login;

            Expression<Func<UserInfo, bool>> wherelambda = u => (u.Type == (int)UiTypeEnum.Driver && u.Status == (int)UiStatusEnum.Work && u.Phone != null && u.Name != null && u.Login != null && u.Phone.Contains(ui.Phone) && u.Name.Contains(ui.Name) && u.Login.Contains(ui.Login));

            int count = 0;
            var guestlist = userinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<UserInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(guestlist, pi));
        }



        /// <summary>
        /// 司机管理(请假休息司机)
        /// </summary>
        /// <returns></returns>
        public ActionResult DriverRestIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DriverRestIndex(PagingInfo pi, UserInfo ui)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;
            ui.Phone = String.IsNullOrWhiteSpace(ui.Phone) ? "" : ui.Phone;
            ui.Name = String.IsNullOrWhiteSpace(ui.Name) ? "" : ui.Name;
            ui.Login = String.IsNullOrWhiteSpace(ui.Login) ? "" : ui.Login;

            Expression<Func<UserInfo, bool>> wherelambda = u => (u.Type == (int)UiTypeEnum.Driver && u.Status == (int)UiStatusEnum.Rest && u.Phone != null && u.Name != null && u.Login != null && u.Phone.Contains(ui.Phone) && u.Name.Contains(ui.Name) && u.Login.Contains(ui.Login));

            int count = 0;
            var guestlist = userinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<UserInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(guestlist, pi));
        }


        /// <summary>
        /// 当前登陆用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult NowUiDetails()
        {
            string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
            UserInfo userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
            return View(userInfo);
        }


        /// <summary>
        /// 当前登陆用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult NowStaffDetails()
        {
            string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
            UserInfo userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
            return View(userInfo);
        }


        /// <summary>
        /// 获取空闲司机信息
        /// </summary>
        /// <param name="seacher"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDriversInfo(string seacher)
        {
            return Json(userinfoService.GetEntities(u => u.Status == (int)UiStatusEnum.Leisure && u.Type == (int)UiTypeEnum.Driver).Take(5).ToList());
        }
        
    }
}
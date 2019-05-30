using Bll;
using Common.Cache;
using IBll;
using Model;
using Model.Enum;
using Model.Other;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult CarsChart()
        {
            return View();
        }


        public ActionResult DriveChart()
        {
            return View();
        }




        public ActionResult Index()
        {
            List<Meansinfo> milist = new List<Meansinfo>();
            //id,栏目名，url，层级，父id，所属账号类型
            milist.Add(new Meansinfo(1,"用户管理","",1,0,(int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(2, "管理员账户", "/UserInfo/AdminIndex", 2, 1, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(3, "主管账户", "/UserInfo/SatrapIndex", 2, 1, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(4, "职工账户", "/UserInfo/StaffIndex", 2, 1, (int)UiTypeEnum.Admin));


            milist.Add(new Meansinfo(5, "司机管理", "", 1, 0, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(6, "空闲司机", "/UserInfo/DriverIndex", 2, 5, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(7, "在途司机", "/UserInfo/DriverWorkIndex", 2, 5, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(8, "请假司机", "/UserInfo/DriverRestIndex", 2, 5, (int)UiTypeEnum.Admin));

            milist.Add(new Meansinfo(9, "车辆管理", "", 1, 0, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(10, "空闲车辆", "/CarsInfo/CarsIndex", 2, 9, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(11, "在途车辆", "/CarsInfo/CarsWorkIndex", 2, 9, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(12, "保养车辆", "/CarsInfo/CarsMaintainIndex", 2, 9, (int)UiTypeEnum.Admin));

            milist.Add(new Meansinfo(13, "通知新闻", "", 1, 0, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(14, "通知发布", "/NewsInfo/NewsIndex", 2, 13, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(15, "历史通知", "/NewsInfo/NewsHistoryIndex", 2, 13, (int)UiTypeEnum.Admin));

            milist.Add(new Meansinfo(16, "规章制度", "", 1, 0, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(17, "规章发布", "/NewsInfo/RulesIndex", 2, 16, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(18, "历史规章", "/NewsInfo/RulesHistoryIndex", 2, 16, (int)UiTypeEnum.Admin));

            milist.Add(new Meansinfo(19, "用车查询", "", 1, 0, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(20, "当前申请", "/Application/AdminAwaitIndex", 2, 19, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(20, "申请通过", "/Application/SatrapPassIndex", 2, 19, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(21, "驳回申请", "/Application/SatrapNoPassIndex", 2, 19, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(22, "历史申请记录", "/Application/SatrapHistoryIndex", 2, 19, (int)UiTypeEnum.Admin));

            milist.Add(new Meansinfo(49, "数据统计", "", 1, 0, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(50, "车辆数据", "/Home/CarsChart", 2, 49, (int)UiTypeEnum.Admin));
            milist.Add(new Meansinfo(51, "司机数据", "/Home/DriveChart", 2, 49, (int)UiTypeEnum.Admin));


            //主管账号
            milist.Add(new Meansinfo(23, "个人信息", "", 1, 0, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(24, "信息管理", "/UserInfo/NowUiDetails", 2, 23, (int)UiTypeEnum.Satrap));

            milist.Add(new Meansinfo(25, "用车审批", "", 1, 0, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(26, "等待审批", "/Application/SatrapAwaitIndex", 2, 25, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(27, "审批通过", "/Application/SatrapPassIndex", 2, 25, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(27, "已驳回申请", "/Application/SatrapNoPassIndex", 2, 25, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(28, "历史申请记录", "/Application/SatrapHistoryIndex", 2, 25, (int)UiTypeEnum.Satrap));

            milist.Add(new Meansinfo(29, "通知新闻", "", 1, 0, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(30, "通知发布", "/NewsInfo/NewsIndex", 2, 29, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(31, "历史通知", "/NewsInfo/NewsHistoryIndex", 2, 29, (int)UiTypeEnum.Satrap));

            milist.Add(new Meansinfo(32, "规章制度", "", 1, 0, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(33, "规章发布", "/NewsInfo/RulesIndex", 2, 32, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(34, "历史规章", "/NewsInfo/RulesHistoryIndex", 2, 32, (int)UiTypeEnum.Satrap));

            milist.Add(new Meansinfo(35, "司机查询", "", 1, 0, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(36, "司机信息", "/UserInfo/DriverShowIndex", 2, 35, (int)UiTypeEnum.Satrap));

            milist.Add(new Meansinfo(37, "车辆查询", "", 1, 0, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(38, "车辆信息", "/CarsInfo/CarsShowIndex", 2, 37, (int)UiTypeEnum.Satrap));

            milist.Add(new Meansinfo(52, "数据统计", "", 1, 0, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(53, "车辆数据", "/Home/CarsChart", 2, 52, (int)UiTypeEnum.Satrap));
            milist.Add(new Meansinfo(54, "司机数据", "/Home/DriveChart", 2, 52, (int)UiTypeEnum.Satrap));



            //普通职员
            milist.Add(new Meansinfo(39, "个人信息", "", 1, 0, (int)UiTypeEnum.Staff));
            milist.Add(new Meansinfo(40, "信息管理", "/UserInfo/NowStaffDetails", 2, 39, (int)UiTypeEnum.Staff));

            milist.Add(new Meansinfo(41, "用车申请", "", 1, 0, (int)UiTypeEnum.Staff));
            milist.Add(new Meansinfo(42, "等待审批", "/Application/AwaitIndex", 2, 41, (int)UiTypeEnum.Staff));
            milist.Add(new Meansinfo(42, "审批通过", "/Application/PassIndex", 2, 41, (int)UiTypeEnum.Staff));
            milist.Add(new Meansinfo(43, "被驳回申请", "/Application/NoPassIndex", 2, 41, (int)UiTypeEnum.Staff));
            milist.Add(new Meansinfo(44, "历史申请", "/Application/HistoryIndex", 2, 41, (int)UiTypeEnum.Staff));

            milist.Add(new Meansinfo(45, "司机查询", "", 1, 0, (int)UiTypeEnum.Staff));
            milist.Add(new Meansinfo(46, "司机信息", "/UserInfo/DriverShowIndex", 2, 45, (int)UiTypeEnum.Staff));

            milist.Add(new Meansinfo(47, "车辆查询", "", 1, 0, (int)UiTypeEnum.Staff));
            milist.Add(new Meansinfo(48, "车辆信息", "/CarsInfo/CarsShowIndex", 2, 47, (int)UiTypeEnum.Staff));

            string userloginid = Request.Cookies["userLoginId"].Value.ToString();
            UserInfo loginui = CacheHelper.GetCache<UserInfo>(userloginid);

            int logintype = 0;
            if (loginui != null)
            {
                logintype = Convert.ToInt32(loginui.Type);
            }

            return View(Tuple.Create(milist.Where(u => u.MType == logintype), loginui));
        }


        private INewsInfoService newsinfoService = new NewsInfoService();

        private IApplicationInfoService appinfoService = new ApplicationInfoService();

        public ActionResult Welcome()
        {
            var newslist = newsinfoService.GetEntities(u => u.Status == (int)NiStatusEnum.Show) as IEnumerable<NewsInfo>;

            var applist = appinfoService.GetEntities(u => u.Status == (int)AiStatusEnum.NewApplicat || u.Status == (int)AiStatusEnum.Pass) as IEnumerable<ApplicationInfo>; ;

            return View(Tuple.Create(newslist, applist));
        }
    }
}

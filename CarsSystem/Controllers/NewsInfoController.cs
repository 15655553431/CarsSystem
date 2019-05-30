using Bll;
using Common.Cache;
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

namespace CarsSystem.Controllers
{
    public class NewsInfoController : Controller
    {
        //
        // GET: /NewsInfo/

        private INewsInfoService newsinfoService = new NewsInfoService();

        /// <summary>
        /// 新闻通知展示
        /// </summary>
        /// <returns></returns>
        public ActionResult NewsIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewsIndex(PagingInfo pi, NewsInfo ni)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ni.Name = String.IsNullOrWhiteSpace(ni.Name) ? "" : ni.Name;
            ni.Content = String.IsNullOrWhiteSpace(ni.Content) ? "" : ni.Content;


            Expression<Func<NewsInfo, bool>> wherelambda = u => (u.Status == (int)NiStatusEnum.Show && u.Type == (int)NiTypeEnum.News&& u.Name != null && u.Content != null && (u.Content.Contains(ni.Content) || u.Title.Contains(ni.Content)));


            int count = 0;
            var list = newsinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<NewsInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }



        /// <summary>
        /// 历史新闻通知展示
        /// </summary>
        /// <returns></returns>
        public ActionResult NewsHistoryIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewsHistoryIndex(PagingInfo pi, NewsInfo ni)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ni.Name = String.IsNullOrWhiteSpace(ni.Name) ? "" : ni.Name;
            ni.Content = String.IsNullOrWhiteSpace(ni.Content) ? "" : ni.Content;


            Expression<Func<NewsInfo, bool>> wherelambda = u => (u.Status == (int)NiStatusEnum.History && u.Type == (int)NiTypeEnum.News && u.Name != null && u.Content != null && (u.Content.Contains(ni.Content) || u.Title.Contains(ni.Content)));


            int count = 0;
            var list = newsinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<NewsInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }


        /// <summary>
        /// 规章制度展示
        /// </summary>
        /// <returns></returns>
        public ActionResult RulesIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RulesIndex(PagingInfo pi, NewsInfo ni)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ni.Name = String.IsNullOrWhiteSpace(ni.Name) ? "" : ni.Name;
            ni.Content = String.IsNullOrWhiteSpace(ni.Content) ? "" : ni.Content;


            Expression<Func<NewsInfo, bool>> wherelambda = u => (u.Status == (int)NiStatusEnum.Show && u.Type == (int)NiTypeEnum.Rules && u.Name != null && u.Content != null && (u.Content.Contains(ni.Content) || u.Title.Contains(ni.Content)));


            int count = 0;
            var list = newsinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<NewsInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }


        /// <summary>
        /// 历史规章展示
        /// </summary>
        /// <returns></returns>
        public ActionResult RulesHistoryIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RulesHistoryIndex(PagingInfo pi, NewsInfo ni)
        {
            pi.PageIndex = pi.PageIndex == 0 ? 1 : pi.PageIndex;
            pi.PageSize = pi.PageSize == 0 ? 5 : pi.PageSize;

            ni.Name = String.IsNullOrWhiteSpace(ni.Name) ? "" : ni.Name;
            ni.Content = String.IsNullOrWhiteSpace(ni.Content) ? "" : ni.Content;


            Expression<Func<NewsInfo, bool>> wherelambda = u => (u.Status == (int)NiStatusEnum.History && u.Type == (int)NiTypeEnum.Rules && u.Name != null && u.Content != null && (u.Content.Contains(ni.Content) || u.Title.Contains(ni.Content)));


            int count = 0;
            var list = newsinfoService.GetPageEntities<int>(pi.PageSize, pi.PageIndex, out count, wherelambda, u => u.ID, true) as IEnumerable<NewsInfo>;
            pi.TotalItems = count;

            return Json(Tuple.Create(list, pi));
        }


        /// <summary>
        /// 发布新闻通知
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateRules()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateRules(NewsInfo ni, FormCollection form)
        {
            //这里注意必须使用 FormCollection form  获得富文本编辑器的内容
            //[ValidateInput(false)] 还需要加上这个，因为富文本编辑器传过来的是html代码。不这样会报错
            string editorValue = form["editorValue"];
            if (editorValue.Length > 1)
            {
                if (Request.Cookies["userLoginId"] != null)
                {
                    //从缓存里面拿到userLoginId信息
                    string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                    UserInfo userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
                    if (userInfo != null)
                    {
                        ni.GUID = Guid.NewGuid().ToString();
                        ni.Content = editorValue;
                        ni.UGUID = userInfo.GUID;
                        ni.EditDate = DateTime.Now;
                        ni.Name = userInfo.Name;
                        ni.Status = (int)NiStatusEnum.Show;
                        ni.Type = (int)NiTypeEnum.Rules;
                        if (newsinfoService.Add(ni))
                        {
                            return Json("ok");
                        }
                    }

                }

            }

            return Json("err");
        }



 

        /// <summary>
        /// 发布新闻通知
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateNews()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateNews(NewsInfo ni, FormCollection form)
        {
            //这里注意必须使用 FormCollection form  获得富文本编辑器的内容
            //[ValidateInput(false)] 还需要加上这个，因为富文本编辑器传过来的是html代码。不这样会报错
            string editorValue = form["editorValue"];
            if (editorValue.Length > 1)
            {
                if (Request.Cookies["userLoginId"] != null)
                {
                    //从缓存里面拿到userLoginId信息
                    string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                    UserInfo userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
                    if (userInfo != null)
                    {
                        ni.GUID = Guid.NewGuid().ToString();
                        ni.Content = editorValue;
                        ni.UGUID = userInfo.GUID;
                        ni.EditDate = DateTime.Now;
                        ni.Name = userInfo.Name;
                        ni.Status = (int)NiStatusEnum.Show;
                        ni.Type = (int)NiTypeEnum.News;
                        if (newsinfoService.Add(ni))
                        {
                            return Json("ok");
                        }
                    }

                }

            }

            return Json("err");
        }


        /// <summary>
        /// 展示新闻
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowNew(int id = 0)
        {
            return View(newsinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }

        /// <summary>
        /// 新闻编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            return View(newsinfoService.GetEntities(u => u.ID == id).FirstOrDefault());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NewsInfo ni, FormCollection form)
        {

            //这里注意必须使用 FormCollection form  获得富文本编辑器的内容
            //[ValidateInput(false)] 还需要加上这个，因为富文本编辑器传过来的是html代码。不这样会报错
            string editorValue = form["editorValue"];
            if (editorValue.Length > 1)
            {
                if (Request.Cookies["userLoginId"] != null)
                {
                    //从缓存里面拿到userLoginId信息
                    string userLoginId = Request.Cookies["userLoginId"].Value.ToString();
                    UserInfo userInfo = CacheHelper.GetCache(userLoginId) as UserInfo;
                    if (userInfo != null)
                    {

                        NewsInfo niedit = newsinfoService.GetEntities(u => u.ID == ni.ID).FirstOrDefault();

                        niedit.Title = ni.Title;
                        niedit.Content = editorValue;
                        niedit.UGUID = userInfo.GUID;
                        niedit.EditDate = DateTime.Now;
                        niedit.Name = userInfo.Name;

                        if (newsinfoService.Update(niedit))
                        {
                            return Json("ok");
                        }
                    }

                }

            }
            return Json("err");

        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            if (newsinfoService.Delete(newsinfoService.GetEntities(u => u.ID == id).FirstOrDefault()))
            {
                return Json("ok");
            }
            return Json("err");
        }


        /// <summary>
        /// 设置为历史新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetHistory(int id = 0)
        {
            NewsInfo ni = newsinfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            ni.Status = (int)NiStatusEnum.History;
            if (newsinfoService.Update(ni))
            {
                return Json("ok");
            }
            return Json("err");
        }


        /// <summary>
        /// 设置为显示新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetShow(int id = 0)
        {
            NewsInfo ni = newsinfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            ni.Status = (int)NiStatusEnum.Show;
            if (newsinfoService.Update(ni))
            {
                return Json("ok");
            }
            return Json("err");
        }
        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCreditApp1.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace MvcCreditApp1.Controllers
{
    public class HomeController : Controller
    {
        private CreditContext db = new CreditContext();
        public ActionResult Index()
        {
            var allCredits = db.Credits.ToList<Credit>();
            ViewBag.Credits = allCredits;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        private void GiveCredits()
        {
            var allCredits = db.Credits.ToList<Credit>();
            ViewBag.Credits = allCredits;
        }
        [HttpGet]
        public ActionResult CreateBid()
        {
            GiveCredits();
            var allBids = db.Bids.ToList<Bid>();
            ViewBag.Bids = allBids;
            return View();
        }
        [Authorize]
        [HttpPost]
        public string CreateBid(Bid newBid)
        {
            newBid.bidDate = DateTime.Now;
            // Добавляем новую заявку в БД
            db.Bids.Add(newBid);
            // Сохраняем в БД все изменения
            db.SaveChanges();
            return "Спасибо, <b>" + newBid.Name + "</b>, за выбор нашего банка. Ваша заявка будет рассмотрена в течении 10 дней.";
        }
        public ActionResult BidSearch(string name)
        {
            var allBids = db.Bids.Where(a => a.CreditHead.Contains(name)).ToList();
            if (allBids.Count == 0)
            {
                return Content("Указанный кредит " + name + " не найден");
                //return HttpNotFound();
            }
            return PartialView(allBids);
        }
    }
}
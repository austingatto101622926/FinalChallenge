using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalChallenge.Models;

namespace FinalChallenge.Controllers
{
    [Authorize(Roles = "Authorised")]
    public class MembersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Members
        public ActionResult Index()
        {
            List<AspNetUser> authorisedUsers = db.AspNetUsers.Include(I => I.AspNetRoles).Where(U => U.AspNetRoles.Any(R => R.Id == "1")).ToList();

            foreach (var user in authorisedUsers)
            {
                List<Game> userGames = db.Games.Where(G => G.AspNetUserId == user.Id).ToList();
                if (userGames.Count() == 0)
                {
                    user.TotalFeesPaid = 0;
                    continue;
                }
                user.TotalFeesPaid = userGames.Select(G => G.FeeAmount).Sum();
            }
            return View(authorisedUsers);
        }

        // GET: Members
        public ActionResult Pending()
        {
            List<AspNetUser> pendingUsers = db.AspNetUsers.Include(I => I.AspNetRoles).Where(U => U.AspNetRoles.All(R => R.Id != "1")).ToList();
            return View(pendingUsers);
        }

        // GET: Members
        public ActionResult Approve(string id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            db.AspNetRoles.Find("1").AspNetUsers.Add(user);
            db.AspNetRoles.Find("0").AspNetUsers.Remove(user);
            db.SaveChanges();

            return RedirectToAction("Pending");
        }

        // GET: Members
        public ActionResult Reject(string id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            db.AspNetRoles.Find("0").AspNetUsers.Add(user);
            db.AspNetRoles.Find("1").AspNetUsers.Remove(user);
            db.SaveChanges();

            return RedirectToAction("Pending");
        }







        // GET: Members/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUser);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

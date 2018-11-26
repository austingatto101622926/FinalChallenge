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
    public class GamesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Games
        public ActionResult Upcoming()
        {
            return View(db.Games.Where(G => G.DateTime > DateTime.Now).ToList());
        }

        // GET: Games
        public ActionResult Previous()
        {
            return View(db.Games.Where(G => G.DateTime < DateTime.Now).ToList());
        }

        // GET: Games1/AddPayee/5
        public ActionResult AddPayee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            List<AspNetUser> authorisedUsers = db.AspNetUsers.Where(U => U.AspNetRoles.Any(R => R.Name == "Authorised")).ToList();
            ViewBag.AspNetUserId = new SelectList(authorisedUsers, "Id", "Email", game.AspNetUserId);
            return View(game);
        }

        // POST: Games1/AddPayee/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPayee([Bind(Include = "GameId,AspNetUserId,FeeAmount")] Game game)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(game).State = EntityState.Modified;
                db.Games.Find(game.GameId).FeeAmount = game.FeeAmount;
                db.Games.Find(game.GameId).AspNetUserId = game.AspNetUserId;
                db.SaveChanges();
                return RedirectToAction("Previous");
            }
            List<AspNetUser> authorisedUsers = db.AspNetUsers.Where(U => U.AspNetRoles.Any(R => R.Name == "Authorised")).ToList();
            ViewBag.AspNetUserId = new SelectList(authorisedUsers, "Id", "Email", game.AspNetUserId);
            return View(game);
        }

        // GET: Games1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Include(g => g.FeePayee).Where(x => x.GameId == id).First();
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }












        // GET: Games1/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Games1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameId,DateTime,Venue,AspNetUserId,FeeAmount")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", game.AspNetUserId);
            return View(game);
        }

        // GET: Games1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", game.AspNetUserId);
            return View(game);
        }

        // POST: Games1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,DateTime,Venue,AspNetUserId,FeeAmount")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", game.AspNetUserId);
            return View(game);
        }

        // GET: Games1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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

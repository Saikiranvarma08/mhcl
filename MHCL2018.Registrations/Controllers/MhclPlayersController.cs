using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MHCL2018.Registrations.Models;

namespace MHCL2018.Registrations.Controllers
{
    public class MhclPlayersController : Controller
    {
        private MHCL2018DBEntities db = new MHCL2018DBEntities();

        // GET: MhclPlayers
        public ActionResult Index()
        {
			return View(db.MhclPlayer.Where(p => p.MhclTeam.Count == 0).ToList());
        }

        // GET: MhclPlayers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MhclPlayer mhclPlayer = db.MhclPlayer.Find(id);
            if (mhclPlayer == null)
            {
                return HttpNotFound();
            }

			List<string> allTeams = db.MhclTeam.Select(t => t.TeamName).ToList();
			List<SelectListItem> teamsData = new List<SelectListItem>();
			foreach (string teamName in allTeams)
			{
				SelectListItem item = new SelectListItem();
				item.Text = teamName;
				item.Value = teamName;
				if (mhclPlayer.MhclTeam.FirstOrDefault() != null)
				{
					if (mhclPlayer.MhclTeam.FirstOrDefault().TeamName == teamName)
					{
						item.Selected = true;
					}
					else
					{
						item.Selected = false;
					}
				}
				teamsData.Add(item);
			}
			ViewData["Teams"] = teamsData;

			TempData["CurrentPlayerId"] = id;

			return View(mhclPlayer);
        }

        // GET: MhclPlayers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MhclPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerId,PlayerName,Email,MID,Gender,Batsman,BatsmanRating,Bowler,BowlerRating,AvailabilityComments,OtherComments,PurchasePrice")] MhclPlayer mhclPlayer)
        {
            if (ModelState.IsValid)
            {
                db.MhclPlayer.Add(mhclPlayer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mhclPlayer);
        }

        // GET: MhclPlayers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MhclPlayer mhclPlayer = db.MhclPlayer.Find(id);
            if (mhclPlayer == null)
            {
                return HttpNotFound();
            }
			List<string> allTeams = db.MhclTeam.Select(t => t.TeamName).ToList();
			List<SelectListItem> teamsData = new List<SelectListItem>();
			foreach (string teamName in allTeams)
			{
				SelectListItem item = new SelectListItem();
				item.Text = teamName;
				item.Value = teamName;
				if (mhclPlayer.MhclTeam.FirstOrDefault() != null)
				{
					if (mhclPlayer.MhclTeam.FirstOrDefault().TeamName == teamName)
					{
						item.Selected = true;
					}
					else
					{
						item.Selected = false;
					}
				}
				teamsData.Add(item);
			}
			ViewData["Teams"] = teamsData;

            return View(mhclPlayer);
        }

        // POST: MhclPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerId,PlayerName,Email,MID,Gender,Batsman,BatsmanRating,Bowler,BowlerRating,AvailabilityComments,OtherComments")] MhclPlayer mhclPlayer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mhclPlayer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mhclPlayer);
        }

		public ActionResult AllocatePlayer(FormCollection formData)
		{
			int currPlayerId = Convert.ToInt32(TempData["CurrentPlayerId"]);
			TempData.Keep();
			MhclPlayer currPlayer = db.MhclPlayer.Find(currPlayerId);
			List<MhclTeam> allTeams = db.MhclTeam.ToList();
			MhclTeam teamToAllocate = allTeams.Where(t => t.TeamName == formData[1]).FirstOrDefault();
			List<MhclTeam> playerTeams = new List<MhclTeam>();
			playerTeams.Add(teamToAllocate);

			currPlayer.PurchasePrice = Convert.ToInt32(formData[0]);
			currPlayer.MhclTeam = playerTeams;

			db.Entry(currPlayer).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MhclPlayer mhclPlayer = db.MhclPlayer.Find(id);
            if (mhclPlayer == null)
            {
                return HttpNotFound();
            }
            return View(mhclPlayer);
        }

        // POST: MhclPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MhclPlayer mhclPlayer = db.MhclPlayer.Find(id);
            db.MhclPlayer.Remove(mhclPlayer);
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

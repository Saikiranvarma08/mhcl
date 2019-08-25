using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MHCL2018.Registrations.Models;
using System.Threading;

namespace MHCL2018.Registrations.Controllers
{
    public class MhclTeamsController : Controller
    {
        private MHCL2018DBEntities db = new MHCL2018DBEntities();

        // GET: MhclTeams
        public ActionResult Index()
        {
			List<MhclTeam> allTeams = db.MhclTeam.ToList();
			List<MhclTeamFullInfo> allTeamsInfo = new List<MhclTeamFullInfo>();

			foreach (MhclTeam team in allTeams)
			{
				MhclTeamFullInfo teamInfo = new MhclTeamFullInfo();
				teamInfo.TeamId = team.TeamId;
				teamInfo.TeamName = team.TeamName;
				teamInfo.TeamOwnerName = team.TeamOwnerName;
				teamInfo.TotalPlayerBought = team.MhclPlayer.Count;
				teamInfo.TotalPurchasePrice = team.MhclPlayer.Sum(p => Convert.ToInt32(p.PurchasePrice));
				teamInfo.RemainingPurchasePrice = 25000 - teamInfo.TotalPurchasePrice;
				teamInfo.AverageCostOfRemainingPlayers = 0;
				allTeamsInfo.Add(teamInfo);
			}

			return View(allTeamsInfo);
        }

        // GET: MhclTeams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MhclTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,TeamName,TeamOwnerName,TeamCreateTime,TeamUpdateTime")] MhclTeam mhclTeam)
        {
            if (ModelState.IsValid)
            {
				mhclTeam.TeamCreateTime = DateTime.Now;
				mhclTeam.TeamUpdateTime = DateTime.Now;
				db.MhclTeam.Add(mhclTeam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mhclTeam);
        }

        // GET: MhclTeams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MhclTeam mhclTeam = db.MhclTeam.Find(id);
            if (mhclTeam == null)
            {
                return HttpNotFound();
            }
            return View(mhclTeam);
        }

        // POST: MhclTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,TeamName,TeamOwnerName,TeamCreateTime,TeamUpdateTime")] MhclTeam mhclTeam)
        {
            if (ModelState.IsValid)
            {
				mhclTeam.TeamUpdateTime = DateTime.Now;
                db.Entry(mhclTeam).State = EntityState.Modified;
                db.SaveChanges();

				Thread.Sleep(5000);

                return RedirectToAction("Index");
            }
            return View(mhclTeam);
        }

        // GET: MhclTeams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MhclTeam mhclTeam = db.MhclTeam.Find(id);
            if (mhclTeam == null)
            {
                return HttpNotFound();
            }
            return View(mhclTeam);
        }

        // POST: MhclTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MhclTeam mhclTeam = db.MhclTeam.Find(id);
            db.MhclTeam.Remove(mhclTeam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		public ActionResult ShowTeamPlayers(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			MhclTeam mhclTeam = db.MhclTeam.Find(id);
			if (mhclTeam == null)
			{
				return HttpNotFound();
			}
			return View(mhclTeam);
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

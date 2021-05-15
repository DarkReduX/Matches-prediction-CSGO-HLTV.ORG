using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSGO_match_result_prediction_курсовая_практика_.Models;

namespace CSGO_match_result_prediction_курсовая_практика_.Controllers
{
    public class MatchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Match
        public async Task<ActionResult> Index()
        {
            var matchesInfo = await db.MatchesInfo
                .Include(m => m.Prediction)
                .Include(m => m.TeamsInfo)
                .OrderBy(m => m.StartTime)
                .Where(m => m.TeamsInfo.Count() == 2 && m.Result == null && m.StartTime > DateTime.Now)
                .ToListAsync();
            return View(matchesInfo);
        }

        public async Task<ActionResult> AlgorithmStatsCSGO()
        {
            var completedMatches = await db.MatchesInfo
                .Include(m => m.Prediction)
                .Include(m => m.Result)
                .Include(m => m.TeamsInfo)
                .Include(m => m.Result.Winner)
                .Where(m => m.TeamsInfo.Count == 2 && m.Result != null)
                .OrderByDescending(m => m.StartTime)
                .ToListAsync();
            int winCount = completedMatches.Where(m => m.Result.Winner.Id == m.Prediction.Id).Count();
            int lostCount = completedMatches.Count() - winCount;
            double winRate = Math.Round((double)winCount / (winCount + lostCount) * 100, 1);
            AlgorithmStatsViewModel viewModel = new AlgorithmStatsViewModel { MatchInfos = completedMatches, WinCount = winCount, LostCount = lostCount, WinRate = winRate };
            return View(viewModel);
        }
        public async Task<ActionResult> TeamDetails(string id)
        {
            if (id == null || id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamInfo team = await db.TeamsInfo.Include(t => t.Stats).SingleOrDefaultAsync(t => t.Id == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }
        public async Task<ActionResult> _LiveMatchList()
        {
            List<MatchInfo> liveMatches = db.MatchesInfo.Include(m => m.TeamsInfo)
                .Include(m => m.Prediction)
                .Include(m => m.Result)
                .Where(m => m.Result != null && m.TeamsInfo.Count() == 2 && m.StartTime < DateTime.Now)
                .ToList();
            return PartialView(liveMatches);
        }
        // GET: Match/Details/5
        //public async Task<ActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MatchInfo matchInfo = await db.MatchesInfo.FindAsync(id);
        //    if (matchInfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(matchInfo);
        //}

        // GET: Match/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Id = new SelectList(db.MatchResults, "MatchInfoId", "MapScore");
        //    return View();
        //}

        // POST: Match/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,MatchUrl,EventName,LogoUrl,StartTime,MatchFormat")] MatchInfo matchInfo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        matchInfo.Id = Guid.NewGuid();
        //        db.MatchesInfo.Add(matchInfo);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Id = new SelectList(db.MatchResults, "MatchInfoId", "MapScore", matchInfo.Id);
        //    return View(matchInfo);
        //}

        // GET: Match/Edit/5
        //public async Task<ActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MatchInfo matchInfo = await db.MatchesInfo.FindAsync(id);
        //    if (matchInfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Id = new SelectList(db.MatchResults, "MatchInfoId", "MapScore", matchInfo.Id);
        //    return View(matchInfo);
        //}

        // POST: Match/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,MatchUrl,EventName,LogoUrl,StartTime,MatchFormat")] MatchInfo matchInfo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(matchInfo).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Id = new SelectList(db.MatchResults, "MatchInfoId", "MapScore", matchInfo.Id);
        //    return View(matchInfo);
        //}

        //// GET: Match/Delete/5
        //public async Task<ActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MatchInfo matchInfo = await db.MatchesInfo.FindAsync(id);
        //    if (matchInfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(matchInfo);
        //}

        //// POST: Match/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(Guid id)
        //{
        //    MatchInfo matchInfo = await db.MatchesInfo.FindAsync(id);
        //    db.MatchesInfo.Remove(matchInfo);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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

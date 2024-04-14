using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab.Repository;
using Lab.Model.Domain;
using Lab.Service.Interface;

namespace lab1.Controllers
{
    public class ConcertsController : Controller
    {
        private readonly IConcertService concertService;

        public ConcertsController(IConcertService concertService)
        {
            this.concertService = concertService;
        }

        // GET: Concerts
        public IActionResult Index()
        {
            var allMovies = this.concertService.ListAllConcerts();
            return View(allMovies);
        }

        // GET: Concerts/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = this.concertService.GetConcertById(id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // GET: Concerts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ConcertName,ConcertDate,ConcertPrice,ConcertPlace,ConcertImage")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                this.concertService.CreateNewConcert(concert);
                return RedirectToAction(nameof(Index));
            }
            return View(concert);
        }

        // GET: Concerts/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = this.concertService.GetConcertById(id);
            if (concert == null)
            {
                return NotFound();
            }
            return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,ConcertName,ConcertDate,ConcertPrice,ConcertPlace,ConcertImage")] Concert concert)
        {
            if (id != concert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.concertService.UpdateConcert(concert);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertExists(concert.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(concert);
        }

        // GET: Concerts/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = this.concertService.GetConcertById(id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var concert = this.concertService.GetConcertById(id);
            if (concert != null)
            {
                this.concertService.DeleteConcerts(concert);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ConcertExists(Guid id)
        {
            return this.concertService.GetConcertById(id) != null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;
using System.IO;
using EventManagement.Models.ViewModels;

namespace EventManagement.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDBContext _context;
        public AdminController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.Events.Include(e=>e.EventDocuments));
        //{
        //    return View();
        //}

        public IActionResult Create()
        {
            PopulateEventTypeDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            var files = HttpContext.Request.Form.Files;
            //long fl = HttpContext.Request.Form.Files.Sum(f => f.Length);

            var filePath = @"d:\123\";//Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(@"d:\"+formFile.FileName, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            PopulateEventTypeDropDownList();
            return View(@event);
        }




        public async Task<IActionResult> Edit(int? eventId)
        {
            if (eventId == null)
            {
                return NotFound();
            }

            var Event = await _context.Events.FindAsync(eventId);

            var @event = await _context.Events
                .Include(e => e.EventDocuments)
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.EventId == eventId);

            //if (Event == null)
            //{
            //    return NotFound();
            //}

            //PopulateEventTypeDropDownList(Event.EventTypeId);
            //return View(Event);

            if (@event == null)
            {
                return NotFound();
            }

            PopulateEventTypeDropDownList(@event.EventTypeId);
            PopulateEventDocuments(@event);
            return View(@event);
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Event @event)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(@event);
                _context.Update(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }



        private void PopulateEventTypeDropDownList(object selectedEvent = null)
        {
            var eventTypesQuery = from d in _context.EventTypes
                                   orderby d.Name
                                   select d;
            ViewBag.EventTypeId = new SelectList(eventTypesQuery.AsNoTracking(), "EventTypeId", "Name", selectedEvent);

       
        }

        private void PopulateEventDocuments(Event @event)
        {
            var allDocuments = _context.EventDocuments;
            var eventDocument = new HashSet<string>(@event.EventDocuments.Select(e => e.FileName));
            //var viewModel = new List<EventDocumentsViewModel> ();
            //foreach (var doc in eventDocument)
            //{
            //    viewModel.Add(new EventDocumentsViewModel
            //    {
                    
            //    });
            //}

            ViewData["Documents"] = eventDocument.ToList();// viewModel;
        }
    }
}
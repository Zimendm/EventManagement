using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Data;
using EventManagement.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDBContext _context;
        public EventController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult List() => View(new EventsListViewModel {Events = _context.Events.Include(t=>t.EventType) });
    }
}
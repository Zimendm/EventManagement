using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private ApplicationDBContext _context;
        public NavigationMenuViewComponent(ApplicationDBContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["eventType"];
            return View(
                _context.Events.Include(e=>e.EventType)
                .Select(x=>x.EventType.Name)
                .Distinct()
                .OrderBy(x=>x)
                );
        }
    }
}

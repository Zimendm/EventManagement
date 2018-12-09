using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Models;

namespace EventManagement.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Events.Any())
            {
                return;
            }

            var eventTypes = new EventType[]
            {
                new EventType {Name="Gymnastics"},
                new EventType {Name="Karate"}
            };

            foreach (var item in eventTypes)
            {
                context.EventTypes.Add(item);
            }

            context.SaveChanges();

            var events = new Event[]
            {
                new Event{EventTypeId=1, Name="Gymnastics Demo"},
                new Event{EventTypeId=2, Name="Karate Demo"},
                new Event{EventTypeId=1, Name="Odessa Mama"},
                new Event{EventTypeId=1, Name="Gymnastics Demo #2"}
            };

            foreach (var item in events)
            {
                context.Events.Add(item);
            }

            context.SaveChanges();

        }
    }
}

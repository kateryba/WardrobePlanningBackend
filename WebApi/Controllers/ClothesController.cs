using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
//using Microsoft.Extensions.Logging;
using WebApi.Dtos;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("cloth")]
    public class ClothesController : Controller
    {
        private static readonly int[] Sizes = new[]
        {
            50, 56, 62, 68, 74, 80, 86, 92, 98, 104, 5, 6, 7 , 8, 10, 12, 14, 16
        };

        private static readonly string[] Owners = new[]
        {
            "Ann" , "John", "Liz", "Unknown"
        };

        private static readonly string[] Types = new[]
        {
            "Top" , "Bottom", "Overall", "Shoes", "Accessories"
        };

        private static readonly string[] Colors = new[]
        {
            "red", "orange", "yellow", "chartreuse green", "green", "spring green", "cyan", "azure", "blue", "violet", "magenta", "rose", "white", "black"
        };

        private static readonly string[] Seasons = new[]
        {
            "summer" , "winter", "autumn-spring"
        };

        private static readonly string[] Styles = new[]
        {
            "boys" , "girls", "uni"
        };

        private static readonly string[] FitTypes = new[]
        {
            "husky" , "slim", "regular"
        };



        //private readonly ILogger<FamilyMembersController> _logger;
        private static Random _rng = new Random();

        private static List<Cloth> Items;

        static ClothesController()
        {
            Items = Enumerable.Range(0, 12).Select(index => new Cloth
            {
                ID = index,
                Type = Types[_rng.Next(Types.Length)],
                Size = Sizes[_rng.Next(Sizes.Length)],
                Owner = Owners[_rng.Next(Owners.Length)],
                Color = Colors[_rng.Next(Colors.Length)],
                Season = Seasons[_rng.Next(Seasons.Length)],
                Style = Styles[_rng.Next(Styles.Length)],
                Fit = FitTypes[_rng.Next(FitTypes.Length)],
            })
            .ToList();

        }

  
        //public ClothesController(ILogger<ClothesController> logger)
        //{
        //    _logger = logger;
        //}

        // GET: Clothes
        [HttpGet]
        public IEnumerable<Cloth> Get()
        {
            return Items;
        }

        [HttpGet("{ID}")]
        // GET: Clothes/5
        public Cloth Get(int ID)
        {
            return Items[ID];
        }

        [HttpPost]
        // POST: Clothes
        public void Add(Cloth clothItem)
        {
            var id = Items.Count;
            clothItem.ID = id;
            Items.Add(clothItem);
        }

        [HttpPut("{ID}")]
        // PUT: Clothes/5
        public void Edit(int id, Cloth clothItem)
        {
            Items[id] = clothItem;
        }

        //[HttpPost("{ID}")]
        //// GET: Clothes/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Clothes/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// get: clothes/create
        //public actionresult create()
        //{
        //    return view(); 
        //}

        //// post: clothes/create
        //[httppost]
        //[validateantiforgerytoken]  // see a month later what it is
        //public actionresult create(iformcollection collection)
        //{
        //    try
        //    {
        //        // todo: add insert logic here

        //        return redirecttoaction(nameof(index));
        //    }
        //    catch
        //    {
        //        return view();
        //    }
        //}

        //// GET: Clothes/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Clothes/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Clothes/Delete/5
        //        public ActionResult Delete(int id)
        //        {
        //            return View();
        //        }

        //        // POST: Clothes/Delete/5
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Delete(int id, IFormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add delete logic here

        //                return RedirectToAction(nameof(Index));
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }
    }
}
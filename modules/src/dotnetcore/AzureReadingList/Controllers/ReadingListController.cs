using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AzureReadingList.Data;
using AzureReadingList.Models;

namespace AzureReadingList.Controllers
{
    public class ReadingListController : Controller
    {
        private ReadingListRepository repo = new ReadingListRepository();
        // GET: ReadingList
        public ActionResult Index()
        {
            ReadingListViewModel readingListContent = new ReadingListViewModel();
            readingListContent.LibraryBooks = repo.GetBooks();
            readingListContent.MyBooks = repo.GetBooksForUser("My Name");
            return View(readingListContent);
        }

        //// GET: ReadingList/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ReadingList/Create
        //public ActionResult Create()
        //{
        //    //return View();
        //}

        // POST: ReadingList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReadingList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReadingList/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReadingList/Delete/5
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

        // POST: ReadingList/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
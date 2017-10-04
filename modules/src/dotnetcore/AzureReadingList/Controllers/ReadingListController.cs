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
        private static string readerName = "richross";

        // GET: ReadingList
        public async Task<ActionResult> Index()
        {
            ReadingListViewModel readingListContent = new ReadingListViewModel();
            readingListContent.LibraryBooks = await ReadingListRepository<Recommendation>.GetBooks(d => d.type == "recommendation");

            
            readingListContent.MyBooks = (IEnumerable<Book>) await ReadingListRepository<Book>.GetBooksForUser(b => b.reader == readerName);
            return View(readingListContent);
        }

        // POST: ReadingList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                Book myNewBookToSave = new Book()
                {
                    id = "1001",
                    title = collection["title"],
                    isbn = collection["isbn"],
                    description = collection["description"],
                    author = collection["author"],
                    reader = "richross"
                };

                ReadingListRepository<Book>.Initialize();

                await ReadingListRepository<Book>.AddBookForUser(myNewBookToSave);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: ReadingList/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //get the requested record.
            ReadingListRepository<Book>.Initialize();

            IEnumerable<Book> myBooks = (IEnumerable<Book>) await ReadingListRepository<Book>.GetBooksForUser(b => b.id == id.ToString());

            return View(myBooks.First());
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
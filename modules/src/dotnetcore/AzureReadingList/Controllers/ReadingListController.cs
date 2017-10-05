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
        // GET: ReadingList
        public async Task<ActionResult> Index()
        {
            ReadingListViewModel readingListContent = new ReadingListViewModel();
            readingListContent.LibraryBooks = await ReadingListRepository<Recommendation>.GetBooks(d => d.type == "recommendation");

            ReadingListRepository<Book>.Initialize();   
            readingListContent.MyBooks = (IEnumerable<Book>) await ReadingListRepository<Book>.GetBooksForUser(b => b.reader == Settings.readerName);
            return View(readingListContent);
        }

        // POST: ReadingList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                Book myNewBookToSave = SaveCollectionAsBook(collection);

                ReadingListRepository<Book>.Initialize();

                await ReadingListRepository<Book>.UpsertBookForUser(myNewBookToSave);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        private static Book SaveCollectionAsBook(IFormCollection collection)
        {
            return new Book()
            {
                id = string.Concat(Settings.readerName, collection["isbn"]),
                title = collection["title"],
                isbn = collection["isbn"],
                description = collection["description"],
                author = collection["author"],
                reader = Settings.readerName
            };
        }

        // GET: ReadingList/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            //get the requested record.
            ReadingListRepository<Book>.Initialize();

            IEnumerable<Book> myBooks = (IEnumerable<Book>) await ReadingListRepository<Book>.GetBooksForUser(b => b.id == id.ToString());

            return View(myBooks.First());
        }

        // POST: ReadingList/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
            try
            {
                Book updatedBook = SaveCollectionAsBook(collection);

                ReadingListRepository<Book>.Initialize();
                await ReadingListRepository<Book>.UpsertBookForUser(updatedBook);
                    
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReadingList/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                ReadingListRepository<Book>.Initialize();
                await ReadingListRepository<Book>.RemoveBookForUser(id);
            }
            catch (Exception ex)
            {

            }
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
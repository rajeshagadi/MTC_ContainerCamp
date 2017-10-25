using AzureReadingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureReadingList.Data
{
    public interface IReadingListRepository : IDisposable
    {
        IEnumerable<Recommendation> GetBooks();
        IEnumerable<Book> GetBooksForUser(string userName);
        void AddBookToListForUser(string userName, Book book);
        void EditBookInUserList(string userName, Book book);
        void RemoveBookFromListForUser(string userName, Book book);
    }
}

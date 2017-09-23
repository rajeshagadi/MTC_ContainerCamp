using System;
using System.Collections.Generic;
using AzureReadingList.Models;

namespace AzureReadingList.Data
{
    public class ReadingListRepository : IReadingListRepository, IDisposable
    {
        private ReadingListContext context;
        public ReadingListRepository()
        {

        }
        //public void ReadingListRepository(ReadingListContext _context)
        //{
        //    context = _context;
        //}

        public void AddBookToListForUser(string userName, Book book)
        {
            throw new NotImplementedException();
        }

        public void EditBookInUserList(string userName, Book book)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recommendation> GetBooks()
        {
            //communicate with CosmosDB to get the list of available books.
            IEnumerable<Recommendation> libraryBooks = new List<Recommendation>()
            {
                new Recommendation() {
                    author ="Johnathan Baier",
                    description ="Learn Kubernetes the Right Way.",
                    id =1, isbn="01234",
                    title ="Get Started with Kubernetes",
                    imageURL = "https://mtchouimages.blob.core.windows.net/books/Kubernetes.jpg" },
                new Recommendation() {
                    author ="Rajdeep Das",
                    description ="Docker Networking Deep Dive",
                    id =2, isbn="95201",
                    title ="Learn Docker Networking",
                    imageURL ="https://mtchouimages.blob.core.windows.net/books/DockerNetworking.jpg"  },
                new Recommendation() {
                    author ="Johnathan Baier",
                    description ="Learn Kubernetes the Right Way.",
                    id =3, isbn="01234",
                    title ="Get Started with Kubernetes",
                    imageURL = "https://mtchouimages.blob.core.windows.net/books/Kubernetes.jpg" },
                new Recommendation() {
                    author ="Johnathan Baier",
                    description ="Learn Kubernetes the Right Way.",
                    id =4, isbn="01234",
                    title ="Get Started with Kubernetes",
                    imageURL = "https://mtchouimages.blob.core.windows.net/books/Kubernetes.jpg" },
                new Recommendation() {
                    author ="Johnathan Baier",
                    description ="Learn Kubernetes the Right Way.",
                    id =5, isbn="01234",
                    title ="Get Started with Kubernetes",
                    imageURL = "https://mtchouimages.blob.core.windows.net/books/Kubernetes.jpg" },
                new Recommendation() {
                    author ="Johnathan Baier",
                    description ="Learn Kubernetes the Right Way.",
                    id =6, isbn="01234",
                    title ="Get Started with Kubernetes",
                    imageURL = "https://mtchouimages.blob.core.windows.net/books/Kubernetes.jpg" },
                new Recommendation() {
                    author ="Johnathan Baier",
                    description ="Learn Kubernetes the Right Way.",
                    id =7, isbn="01234",
                    title ="Get Started with Kubernetes",
                    imageURL = "https://mtchouimages.blob.core.windows.net/books/Kubernetes.jpg" },
                new Recommendation() {
                    author ="Johnathan Baier",
                    description ="Learn Kubernetes the Right Way.",
                    id =8, isbn="01234",
                    title ="Get Started with Kubernetes",
                    imageURL = "https://mtchouimages.blob.core.windows.net/books/Kubernetes.jpg" },
            } as IEnumerable<Recommendation>;

            return libraryBooks;
        }

        public IEnumerable<Book> GetBooksForUser(string userName)
        {
            //communicate with CosmosDB to get the list of available books.
            IEnumerable<Book> myBooks = new List<Book>()
            {
                new Book() { author="John Smith", description="Lore ipsum.", id=4737283, isbn="488239238", title="Some Awesome new Book!" },
            } as IEnumerable<Book>;

            return myBooks;
        }

        public void RemoveBookFromListForUser(string userName, Book book)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ReadingListRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
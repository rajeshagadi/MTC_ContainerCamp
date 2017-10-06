using System;
using System.Collections.Generic;
using System.Configuration;
using AzureReadingList.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace AzureReadingList.Data
{
    public static class ReadingListRepository<T> where T : class
    {
        public static readonly string DatabaseId = Settings.DatabaseId;
        public static readonly string CollectionId = Settings.CollectionId;
        private static string endpoint = Settings.EndPoint;
        private static string authKey = Settings.ReadWriteAuthKey;
        
        private static DocumentClient client;

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(endpoint), authKey);
        }       
        
        public static async void StartUpMode()
        {
            //Load data into the new collection.
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

        }

        public static async Task<IEnumerable<T>> GetBooks(Expression<Func<T, bool>> predicate)
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

            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public static async Task<IEnumerable<Book>> GetBooksForUser(Expression<Func<Book, bool>> predicate)
        {
            List<Book> results = new List<Book>();

            try
            {
                IDocumentQuery<Book> query = client.CreateDocumentQuery<Book>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<Book>());
                }
            }
            catch (Exception ex)
            {
                
            }

            return results;
        }

        public static async Task UpsertBookForUser(Book myNewBook)
        {
            try
            {
                await client.UpsertDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                myNewBook);
            }
            catch (Exception ex)
            {
                
            }
        }

        public static async Task RemoveBookForUser(string bookId)
        {
            try
            {
                await client.DeleteDocumentAsync(
                    UriFactory.CreateDocumentUri(DatabaseId, CollectionId, bookId));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
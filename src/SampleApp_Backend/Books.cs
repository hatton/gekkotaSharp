using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http; //HttpResponseMessage
using System.Web.Http; //ApiController

namespace SampleApp_Backend
{
    public class Book
    {
	    public string Id;
	    public string Name;
    }

	public class BooksController : ApiController
	{
		//todo: we're going to need dependency injection, or change the behavior of making a new one each time... for now, static
		private static List<Book> _books;

		public BooksController()
		{
			if (_books == null)
			{
				_books = new List<Book>();

				_books.Add(new Book() {Name = "one", Id="1"});
				_books.Add(new Book() { Name = "two", Id = "2" });
				_books.Add(new Book() { Name = "three", Id = "3" });
			}
		}
		public IEnumerable<Book> GetAllBooks()
		{
				return _books;
		}

		public HttpResponseMessage DeleteBook(string id)
		{
			Book book = _books.SingleOrDefault(b => b.Id == id);
			if (book == null)
				throw new HttpResponseException(HttpStatusCode.NotFound); 

			_books.Remove(book);
			return new HttpResponseMessage(HttpStatusCode.NoContent);

		}



		/// <summary>
		/// Update and existing book
		/// </summary>
		public HttpResponseMessage PutBook(Book book)
		{
			if (ModelState.IsValid)
			{
				Debug.Assert(!string.IsNullOrEmpty(book.Id));
				Book foundBook = _books.Where(b => b.Id == book.Id).FirstOrDefault();

				if (foundBook == null)//why always false?
					return Request.CreateResponse<Book>(HttpStatusCode.NotFound, null);//review: what's the right code?

				_books.Remove(foundBook);
				_books.Add(book);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted, book);//review: what's the right code?
				//response.Headers.Location = new Uri(Request.RequestUri, string.Format("book/{0}", book.Id));//review
				return response;
			}
			else
			{
				return Request.CreateResponse<Book>(HttpStatusCode.BadRequest, null);
			}
		}

		//add a book
		public HttpResponseMessage PostBook(Book book)
		{
			if (ModelState.IsValid)
			{
				Debug.Assert(string.IsNullOrEmpty(book.Id));
				book.Id = Guid.NewGuid().ToString().Substring(0, 4);//4 should get is through the sample
				_books.Add(book);

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, book);
				response.Headers.Location = new Uri(Request.RequestUri, string.Format("book/{0}", book.Id));//review
				return response;
			}
			else
			{
				return Request.CreateResponse<Book>(HttpStatusCode.BadRequest, null);
			}
		}
	}
}

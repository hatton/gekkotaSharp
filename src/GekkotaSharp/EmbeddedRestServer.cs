using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Gekkota
{
	
	class EmbeddedRestServer
	{
		private static EmbeddedRestServer TheOneServer;

		public static EmbeddedRestServer StartupIfNeeded()
		{
			if(TheOneServer==null)
				TheOneServer = new EmbeddedRestServer();

			return TheOneServer;
		}

		private EmbeddedRestServer()
		{
			var config = new HttpSelfHostConfiguration("http://localhost:5432");
			//config.Routes.MapHttpRoute("default", "", new { controller = "SimpleFile" });
		
			config.Routes.MapHttpRoute("myRest", "api/{controller}/{id}", new { id = RouteParameter.Optional });
			config.MessageHandlers.Add(new SimpleFileHandler());

			//by taking out xml, we make it give json somehow
			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

			var server = new HttpSelfHostServer(config);
			server.OpenAsync().Wait();
		}


		//INteresting: this controller is *not* found, even though it's in the same assembly. To get a controler
		//to be seen, reference it in the DesktopWrapper, like this:
		//Type referenceToForceLoadSoWepAPISeesOurController = typeof(SampleApp_Backend.BooksController);
		public class HelloController : ApiController
		{
			public HttpResponseMessage Get()
			{
				return new HttpResponseMessage
				{
					Content = new StringContent("Hello HTTP")
				};
			}

		}

		class SimpleFileHandler : DelegatingHandler
		{
			private string _baseFolder;

			public SimpleFileHandler()
			{
				_baseFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			}

			protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
			{
				
				var path = request.RequestUri.AbsolutePath;

			
				//This may not be the right way to do things, but for now, use the index.html as our root
				if(path.EndsWith("index.html"))
				{
					_baseFolder = Path.GetDirectoryName(path);
					_baseFolder = _baseFolder.Trim(new char[]{'\\','/'})+ "/"; 
					path = Path.GetFileName(path);
				}
			
				if (path.StartsWith("/api/"))
				{
					return base.SendAsync(request, cancellationToken);
				}

				return Task<HttpResponseMessage>.Factory.StartNew(() =>
				{
					var fullPath = Path.Combine(_baseFolder, path.TrimStart(new char[]{'\\','/'}));

					if (File.Exists(fullPath))
					{
						var response = request.CreateResponse();
						var stream = new FileStream(fullPath, FileMode.Open);
						response.Content = new StreamContent(stream);	//review: would like to konw that this will do the disposing of the stream at the right time.
						response.Content.Headers.ContentType = GuessMediaTypeFromExtension(fullPath);
						return response;
					}
					else
					{
						return request.CreateErrorResponse(HttpStatusCode.NotFound, "File not found");
					}
				});
			}

			private MediaTypeHeaderValue GuessMediaTypeFromExtension(string path)
			{
				var ext = Path.GetExtension(path);

				switch (ext)
				{
					case ".htm":
					case ".html":
						return new MediaTypeHeaderValue(MediaTypeNames.Text.Html);

					case ".js":
						return new MediaTypeHeaderValue("text/javascript");

					case ".png":
						return new MediaTypeHeaderValue("image/png");

					case ".jpg":
					case ".jpeg":
						return new MediaTypeHeaderValue("image/jpeg");


					case ".css":
						return new MediaTypeHeaderValue("text/css");

					default:
						return new MediaTypeHeaderValue(MediaTypeNames.Text.Plain);
				}
			}
		}
	}
}

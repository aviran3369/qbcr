using QuickBookCarRental.Login.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace QuickBookCarRental.Login.Controllers
{

    [RoutePrefix("api/Files")]
    public class FilesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage TryGetFile(string id) // id not in use, just for routing isseues
        {
            string virtualPath = Request.RequestUri.LocalPath.ToLower();

            ///TODO:    search fileVirtualPath on DB to get the physical path of the file
            ///         table should be like:
            ///             Id int idnetity primary-key
            ///             OriginalFileName nvarchar
            ///             OriginalFileExtension varchar
            ///             VirtualPath nvarchar (SHOULD ALWAYS LOWERCASE FOR EASY SEARCHING)
            ///             PhysicalPath nvarchar
            ///             CreatedAt datetime
            ///             UpdatedAt datetime
            ///             [SOME EXTRA DATA]


            // Example of return file from api application folder

            string root = HttpContext.Current.Server.MapPath("~/Files/");
            string physicalPathFromDb = root + "Sketchpad_637230220276749578.png";

            var filestream = File.OpenRead(physicalPathFromDb);
            var stream = new MemoryStream();
            filestream.CopyTo(stream);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Sketchpad.png" // [OriginalFileName].[OriginalFileExtension] from db
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }

        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var provider = new ExtendedMultipartFileStreamProvider<TestModel>();
                await Request.Content.ReadAsMultipartAsync(provider);
                var stream = await provider.Contents[0].ReadAsStreamAsync();

                string root = HttpContext.Current.Server.MapPath("~/Files/");
                string filename = $"{provider.Data.Files[0].FileName}_{DateTime.Now.Ticks}.{provider.Data.Files[0].FileExtension}";
                FileStream fileStream = new FileStream(root + filename, FileMode.Create);
                stream.CopyTo(fileStream);

                ///TODO: 1. save file on DB/Cloud/CDN
                ///      2. save file details on DB 
                ///         {
                ///             "fileId": 1234, 
                ///             "virtualPath": "/api/docs/[PATH]/[FILE NAME].[FILE EXTENSION]"
                ///         }
                ///      3. return 

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

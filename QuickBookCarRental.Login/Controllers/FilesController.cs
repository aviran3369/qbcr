using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace QuickBookCarRental.Login.Controllers
{
    public class TestModel
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public int FileId { get; set; }
    }

    [RoutePrefix("api/Files")]
    public class FilesController : ApiController
    {
        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            ///NOTE:    Files location is ~/Files/[FILE_NAME]
            ///         Files gets a random name, and saved as binary data
            string root = HttpContext.Current.Server.MapPath("~/Files");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                TestModel test = new TestModel();
                test.Name1 = provider.FormData.GetValues("Name1")[0];
                test.Name2 = provider.FormData.GetValues("Name2")[0];

                /*
                    TODO:   read file in this location, the full path is here:
                            * provider.FileData[0].LocalFileName
                            
                            The original file name:
                            * provider.FileData[0].Headers.ContentDisposition.FileName
                            
                            File mime type: (png, jpg, etc...)
                            * provider.FileData[0].Headers.ContentType.MediaType
                            
                            **********************************************************
                            
                            You need to read this file and save it wherever you want and save details in db.
                            (REMOVE THE FILE FROM ITS TEMP LOCATION IN provider.FileData[0].LocalFileName)

                            Example:
                                Table name: MediaFiles
                                Columns:
                                * Id int primary-key (identity)
                                * OriginalName nvarchar
                                * FullPath varchar
                                * MimeType varchar
                                * CreatedDate dateTime
                                * UploadedBy string (The user id)
                                * VirtualPath nvarchar (for example: /api/car_documents/[CAR_ID]/license_01-01-2020.jpg)
                                        * Make sure it always lowercase (!!!!)
                                        * Whene client request file in this path, you can convert this path to lowercase and search file by VirtualPath
                            
                            Now you can create CarDocuments table like this:
                                * ......
                                * DocumentType int (from enum or DocumentTypes table)
                                * FileId int (with forign-key to MediaFiles table)

                 */


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

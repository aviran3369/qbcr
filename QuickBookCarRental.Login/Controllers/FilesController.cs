﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace QuickBookCarRental.Login.Controllers
{
    [RoutePrefix("api/Files")]
    public class FilesController : ApiController
    {
        [Route("")]
        [HttpPost]
        public async Task<string> Post()
        {

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            // extract file name and file contents
            var fileNameParam = provider.Contents[0].Headers.ContentDisposition.Parameters
                .FirstOrDefault(p => p.Name.ToLower() == "filename");
            string fileName = (fileNameParam == null) ? "" : fileNameParam.Value.Trim('"');
            byte[] file = await provider.Contents[0].ReadAsByteArrayAsync();

            // Here you can use EF with an entity with a byte[] property, or
            // an stored procedure with a varbinary parameter to insert the
            // data into the DB

            var result = string.Format("Received '{0}' with length: {1}", fileName, file.Length);
            return result;
        }
    }
}

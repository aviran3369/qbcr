using Newtonsoft.Json.Linq;
using QuickBookCarRental.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace QuickBookCarRental.Login
{
    public class ExtendedMultipartFileStreamProvider<T> : MultipartMemoryStreamProvider where T : BaseFileModel
    {
        private List<UploadedFile> _files { get; set; }

        public T Data { get; private set; }

        public ExtendedMultipartFileStreamProvider()
        {
            _files = new List<UploadedFile>();
        }

        public override Task ExecutePostProcessingAsync()
        {
            JObject json = new JObject();
            
            foreach (var file in Contents)
            {
                if (!string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                {
                    var filename = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                    var extensionStartIndex = filename.LastIndexOf('.');

                    _files.Add(new UploadedFile
                    {
                        FileName = filename.Substring(0, extensionStartIndex),
                        FileExtension = filename.Substring(extensionStartIndex + 1)
                    });
                }
                else
                {
                    var parameters = file.Headers.ContentDisposition.Parameters;
                    var name= parameters.First().Value.Replace("\"", string.Empty);
                    var value = file.ReadAsStringAsync().Result;
                    json.Add(name, value);
                }
            }

            Data = json.ToObject<T>();
            Data.Files = _files;

            return base.ExecutePostProcessingAsync();
        }
    }
}
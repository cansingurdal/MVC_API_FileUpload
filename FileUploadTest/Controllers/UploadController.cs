using FileUploadTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FileUploadTest.Controllers
{
    public class UploadController : ApiController
    {
        internal const string _fileName = "~/Files";
        internal const string _tempName = "~/Temp";
        public async Task<List<FileUpload>> Post()
        {
            MultipartFormDataStreamProvider streamProvider =
                new MultipartFormDataStreamProvider(HttpContext.Current.Request.MapPath(_tempName));

            await Request.Content.ReadAsMultipartAsync(streamProvider);
            var origin = Request.Headers.FirstOrDefault(x => x.Key == "origin").Value.FirstOrDefault();

            List<FileUpload> returnValue = streamProvider.FileData.Select(x =>
                new FileUpload(x.LocalFileName, x.Headers.ContentDisposition.FileName, origin)).ToList();
            return returnValue;
        }
    }
}

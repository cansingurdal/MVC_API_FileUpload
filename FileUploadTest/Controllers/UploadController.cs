using FileUploadTest.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FileUploadTest.Controllers
{
    public class UploadController : ApiController
    {
        internal const string FileName = "~/Files";
        internal const string TempName = "~/Temp";
        
        public async Task<List<FileUpload>> Post()
        {
            var streamProvider =
                new MultipartFormDataStreamProvider(HttpContext.Current.Request.MapPath(TempName));

            await Request.Content.ReadAsMultipartAsync(streamProvider);
            var origin = "";
            if (Request.Headers.Contains("Origin"))
                origin = Request.Headers.GetValues("Origin").FirstOrDefault();
        
            var returnValue = streamProvider.FileData.Select(x =>
                new FileUpload(x.LocalFileName, x.Headers.ContentDisposition.FileName, origin)).ToList();
            return returnValue;
        }
    }
}

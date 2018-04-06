using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace WebApplication10.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        [Route("api/UploadFile")]
        public HttpResponseMessage PostFile()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            var message1 = string.Format("Image Updated Successfully.");
            try
            {
                
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count == 0)
                {
                    var res = string.Format("Please Upload a image.");
                    dict.Add("error", res);
                    return Request.CreateResponse(HttpStatusCode.NotFound, dict);
                }
                for(int i=0;i<httpRequest.Files.Count;i++)
                {
                    var postedFile = httpRequest.Files[i];
                    var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + postedFile.FileName + extension);
                        postedFile.SaveAs(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                var res = string.Format(ex.Message);
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;

        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        // POST api/values
        public void Post([FromBody]string value)
        {
        }
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

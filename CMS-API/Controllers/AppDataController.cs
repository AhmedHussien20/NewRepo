using Application_Layer.Interfaces;
using CMS_API.Services;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace CMS_API.Controllers
{
    [AllowAnonymous]
    public class AppDataController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();
        public FIleUploader _fIleUploader = new();
        private readonly IWebHostEnvironment _env;

        public AppDataController(IDataService db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        //create
        [HttpPost]
        [Route("createAppData")]
        public async Task<ActionResult<AppDataModel>> createAppData([FromForm] AppDataModel model)
        {
            try
            {
                if (model.UploadFile != null)
                {
                    var fileName = await _fIleUploader.UploadFile(_env, model.UploadFile);
                    model.Link = fileName;
                }

                var output = await _db.AppDataCreate(model);
                return _response.getResponse(output, "Error while creating AppData");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getAppData")]
        public async Task<ActionResult<AppDataModel>> getAppDataById(int id)
        {
            try
            {
                var output = await _db.AppDataGet(id);
                return _response.getResponse(output, "AppData not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllAppData")]
        public async Task<ActionResult<IEnumerable<AppDataModel>>> getAllAppData()
        {
            try
            {
                var output = await _db.AppDataGetAll();

                //get Base URL
                var host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                output.Link = $"{output.Link}";

                return _response.getResponse(output, "AppData not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateAppData")]
        public async Task<ActionResult<AppDataModel>> updateAppData([FromForm] AppDataModel model)
        {
            try
            {
                if (model.UploadFile != null)
                {
                    var fileName = await _fIleUploader.UploadFile(_env, model.UploadFile);
                    model.Link = fileName;
                }
                else
                {
                    var data = await _db.AppDataGet(model.Id);
                    model.Link = data.Link;
                }

                await _db.AppDataUpdate(model);
                return _response.getResponse("", "AppData not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteAppData")]
        public async Task<ActionResult<AppDataModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.AppDataDelete(id);
                return _response.getResponse("", "AppData Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //public string FullyQualifiedApplicationPath
        //{
        //    get
        //    {
        //        //Return variable declaration
        //        var appPath = string.Empty;

        //        //Getting the current context of HTTP request
        //        var context = HttpContext.Current;

        //        //Checking the current context content
        //        if (context != null)
        //        {
        //            //Formatting the fully qualified website url/name
        //            appPath = string.Format("{0}://{1}{2}{3}",
        //                                    context.Request.Url.Scheme,
        //                                    context.Request.Url.Host,
        //                                    context.Request.Url.Port == 80
        //                                        ? string.Empty
        //                                        : ":" + context.Request.Url.Port,
        //                                    context.Request.ApplicationPath);
        //        }

        //        if (!appPath.EndsWith("/"))
        //            appPath += "/";

        //        return appPath;
        //    }
        //}
    }
}

using Application_Layer.Interfaces;
using CMS_API.Services;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_API.Controllers
{
    //Admins
    [AllowAnonymous]
    public class AdminsController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public AdminsController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createAdmins")]
        public async Task<ActionResult<AdminsModel>> createAdmins([FromBody] AdminsModel model)
        {
            try
            {
                //check if NRC exists
                var userData = await _db.AdminsGetByNrc(model.NrcNumber);
                if (userData == null)
                {
                    var output = await _db.AdminsCreate(model);
                    return _response.getResponse(output, "Error while creating Admins");
                }
                else
                {
                    return _response.errorResponse("Admin with included NRC already exists");
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getAdmins")]
        public async Task<ActionResult<AdminsModel>> getAdminsById(int id)
        {
            try
            {
                var output = await _db.AdminsGet(id);
                return _response.getResponse(output, "Admins not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getAdminsbyNrc")]
        public async Task<ActionResult<AdminsModel>> getAdminsbyNrc(string nrc)
        {
            try
            {
                var output = await _db.AdminsGetByNrc(nrc);
                return _response.getResponse(output, "Admins not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllAdmins")]
        public async Task<ActionResult<IEnumerable<AdminsModel>>> getAllAdmins()
        {
            try
            {
                var output = await _db.AdminsGetAll();
                return _response.getResponse(output, "Admins not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateAdmins")]
        public async Task<ActionResult<AdminsModel>> updateAdmins([FromBody] AdminsModel model)
        {
            try
            {
                await _db.AdminsUpdate(model);
                return _response.getResponse("Account Updated", "Admins not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteAdmins")]
        public async Task<ActionResult<AdminsModel>> deleteUserAsync(int id)
        {
            try
            {
                var userData = await _db.AdminsGet(id);
                await _db.AdminsDelete(id);
                await _db.UsersDelete(userData.AdminId);
                return _response.getResponse("Account Delete", "Admins Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
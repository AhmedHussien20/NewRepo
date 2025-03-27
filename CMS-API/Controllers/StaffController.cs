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
    //Staff
    [AllowAnonymous]
    public class StaffController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public StaffController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createStaff")]
        public async Task<ActionResult<StaffModel>> createStaff([FromBody] StaffModel model)
        {
            try
            {
                //check if NRC exists
                var userData = await _db.StaffGetByNrc(model.NRCNumber);
                //check if Location Exists
                var locationData = await _db.NetLocationGet(model.Location);
                if (userData == null)
                {
                    if (locationData != null)
                    {
                        var output = await _db.StaffCreate(model);
                        return _response.getResponse(output, "Error while creating Staff");
                    }
                    else
                    {
                        return _response.errorResponse("Passed location does not exist");
                    }
                }
                else
                {
                    return _response.errorResponse("Staff with included NRC already exists");
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getStaff")]
        public async Task<ActionResult<StaffModel>> getStaffById(int id)
        {
            try
            {
                var output = await _db.StaffGet(id);
                return _response.getResponse(output, "Staff not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getStaffbyNrc")]
        public async Task<ActionResult<StaffModel>> getStaffbyNrc(string nrc)
        {
            try
            {
                var output = await _db.StaffGetByNrc(nrc);
                return _response.getResponse(output, "Staff not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getStaffByLocation")]
        public async Task<ActionResult<IEnumerable<StaffModel>>> getStaffByLocation(string Location)
        {
            try
            {
                var output = await _db.StaffGetByLocation(Location);
                return _response.getResponse(output, "Staff not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllStaff")]
        public async Task<ActionResult<IEnumerable<StaffModel>>> getAllStaff()
        {
            try
            {
                var output = await _db.StaffGetAll();
                return _response.getResponse(output, "Staff not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateStaff")]
        public async Task<ActionResult<StaffModel>> updateStaff([FromBody] StaffModel model)
        {
            try
            {
                await _db.StaffUpdate(model);
                return _response.getResponse("", "Staff not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteStaff")]
        public async Task<ActionResult<StaffModel>> deleteUserAsync(int id)
        {
            try
            {
                var userData = await _db.StaffGet(id);
                await _db.StaffDelete(id);
                await _db.UsersDelete(userData.StaffId);
                return _response.getResponse("", "Staff Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
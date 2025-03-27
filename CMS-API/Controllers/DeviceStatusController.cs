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
    [AllowAnonymous]
    public class DeviceStatusController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public DeviceStatusController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createDeviceStatus")]
        public async Task<ActionResult<DeviceStatusModel>> createDeviceStatus([FromBody] DeviceStatusModel model)
        {
            try
            {
                var output = await _db.DeviceStatusCreate(model);
                return _response.getResponse(output, "Error while creating DeviceStatus");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getDeviceStatus")]
        public async Task<ActionResult<DeviceStatusModel>> getDeviceStatusById(int id)
        {
            try
            {
                var output = await _db.DeviceStatusGet(id);
                return _response.getResponse(output, "DeviceStatus not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllDeviceStatus")]
        public async Task<ActionResult<IEnumerable<DeviceStatusModel>>> getAllDeviceStatus()
        {
            try
            {
                var output = await _db.DeviceStatusGetAll();
                return _response.getResponse(output, "DeviceStatus not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateDeviceStatus")]
        public async Task<ActionResult<DeviceStatusModel>> updateDeviceStatus([FromBody] DeviceStatusModel model)
        {
            try
            {
                await _db.DeviceStatusUpdate(model);
                return _response.getResponse("", "DeviceStatus not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteDeviceStatus")]
        public async Task<ActionResult<DeviceStatusModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.DeviceStatusDelete(id);
                return _response.getResponse("", "DeviceStatus Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
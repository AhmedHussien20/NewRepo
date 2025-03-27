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
    public class DistrictController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public DistrictController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createDistrict")]
        public async Task<ActionResult<DistrictModel>> createDistrict([FromBody] DistrictModel model)
        {
            try
            {
                var output = await _db.DistrictCreate(model);
                return _response.getResponse(output, "Error while creating District");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getDistrict")]
        public async Task<ActionResult<DistrictModel>> getDistrictById(int id)
        {
            try
            {
                var output = await _db.DistrictGet(id);
                return _response.getResponse(output, "District not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getDistrictByProvince")]
        public async Task<ActionResult<IEnumerable<DistrictModel>>> getDistrictByProvince(int id)
        {
            try
            {
                var output = await _db.DistrictGetFromProvince(id);
                return _response.getResponse(output, "District not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllDistrict")]
        public async Task<ActionResult<IEnumerable<DistrictModel>>> getAllDistrict()
        {
            try
            {
                var output = await _db.DistrictGetAll();
                return _response.getResponse(output, "District not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateDistrict")]
        public async Task<ActionResult<DistrictModel>> updateDistrict([FromBody] DistrictModel model)
        {
            try
            {
                await _db.DistrictUpdate(model);
                return _response.getResponse("", "District not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteDistrict")]
        public async Task<ActionResult<DistrictModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.DistrictDelete(id);
                return _response.getResponse("", "District Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
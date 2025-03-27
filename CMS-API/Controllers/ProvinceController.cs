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
    public class ProvinceController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public ProvinceController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createProvince")]
        public async Task<ActionResult<ProvinceModel>> createProvince([FromBody] ProvinceModel model)
        {
            try
            {
                var output = await _db.ProvinceCreate(model);
                return _response.getResponse(output, "Error while creating Province");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getProvince")]
        public async Task<ActionResult<ProvinceModel>> getProvinceById(int id)
        {
            try
            {
                var output = await _db.ProvinceGet(id);
                return _response.getResponse(output, "Province not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllProvince")]
        public async Task<ActionResult<IEnumerable<ProvinceModel>>> getAllProvince()
        {
            try
            {
                var output = await _db.ProvinceGetAll();
                return _response.getResponse(output, "Province not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateProvince")]
        public async Task<ActionResult<ProvinceModel>> updateProvince([FromBody] ProvinceModel model)
        {
            try
            {
                await _db.ProvinceUpdate(model);
                return _response.getResponse("", "Province not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteProvince")]
        public async Task<ActionResult<ProvinceModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.ProvinceDelete(id);
                return _response.getResponse("", "Province Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
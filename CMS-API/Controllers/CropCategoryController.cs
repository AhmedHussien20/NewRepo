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
    public class CropCategoryController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public CropCategoryController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createCropCategory")]
        public async Task<ActionResult<CropCategoryModel>> createCropCategory([FromBody] CropCategoryModel model)
        {
            try
            {
                var output = await _db.CropCategoryCreate(model);
                return _response.getResponse(output, "Error while creating CropCategory");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getCropCategory")]
        public async Task<ActionResult<CropCategoryModel>> getCropCategoryById(int id)
        {
            try
            {
                var output = await _db.CropCategoryGet(id);
                return _response.getResponse(output, "CropCategory not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllCropCategory")]
        public async Task<ActionResult<IEnumerable<CropCategoryModel>>> getAllCropCategory()
        {
            try
            {
                var output = await _db.CropCategoryGetAll();
                return _response.getResponse(output, "CropCategory not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateCropCategory")]
        public async Task<ActionResult<CropCategoryModel>> updateCropCategory([FromBody] CropCategoryModel model)
        {
            try
            {
                await _db.CropCategoryUpdate(model);
                return _response.getResponse("", "CropCategory not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteCropCategory")]
        public async Task<ActionResult<CropCategoryModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.CropCategoryDelete(id);
                return _response.getResponse("", "CropCategory Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
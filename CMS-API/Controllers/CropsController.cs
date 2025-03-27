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
    public class CropsController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public CropsController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createCrops")]
        public async Task<ActionResult<CropsModel>> createCrops([FromBody] CropsModel model)
        {
            try
            {
                var output = await _db.CropsCreate(model);
                return _response.getResponse(output, "Error while creating Crops");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getCrops")]
        public async Task<ActionResult<CropsModel>> getCropsById(int id)
        {
            try
            {
                var output = await _db.CropsGet(id);
                return _response.getResponse(output, "Crops not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllCrops")]
        public async Task<ActionResult<IEnumerable<CropsModel>>> getAllCrops()
        {
            try
            {
                var output = await _db.CropsGetAll();
                return _response.getResponse(output, "Crops not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getCropsAll")]
        public async Task<ActionResult<IEnumerable<CropsModel>>> getCropsAll()
        {
            try
            {
                var output = await _db.GetAllCrops();
                return _response.getResponse(output, "Crops not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateCrops")]
        public async Task<ActionResult<CropsModel>> updateCrops([FromBody] CropsModel model)
        {
            try
            {
                await _db.CropsUpdate(model);
                return _response.getResponse("", "Crops not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteCrops")]
        public async Task<ActionResult<CropsModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.CropsDelete(id);
                return _response.getResponse("", "Crops Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //create
        [HttpPost]
        [Route("createCropsCommercial")]
        public async Task<ActionResult<CropsModel>> createCropsCommercial([FromBody] CropsModel model)
        {
            try
            {
                var output = await _db.CropsCommercialCreate(model);
                return _response.getResponse(output, "Error while creating Crops Commercial");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getCropsCommercial")]
        public async Task<ActionResult<CropsModel>> getCropsCommercialById(int id)
        {
            try
            {
                var output = await _db.CropsCommercialGet(id);
                return _response.getResponse(output, "Commercial Crops not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllCropsCommercial")]
        public async Task<ActionResult<IEnumerable<CropsModel>>> getAllCropsCommercial()
        {
            try
            {
                var output = await _db.CropsCommercialGetAll();
                return _response.getResponse(output, "Commercial Crops not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getCropsCommercialAll")]
        public async Task<ActionResult<IEnumerable<CropsModel>>> getCropsCommercialAll()
        {
            try
            {
                var output = await _db.GetAllCropsCommercial();
                return _response.getResponse(output, "Commercial Crops not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateCropsCommercial")]
        public async Task<ActionResult<CropsModel>> updateCropsCommercial([FromBody] CropsModel model)
        {
            try
            {
                await _db.CropsCommercialUpdate(model);
                return _response.getResponse("", "Commercial Crops not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteCropsCommercial")]
        public async Task<ActionResult<CropsModel>> deleteCropsCommercialAsync(int id)
        {
            try
            {
                await _db.CropsCommercialDelete(id);
                return _response.getResponse("", "Commercial Crops Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
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
    public class SateliteController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public SateliteController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createSatelite")]
        public async Task<ActionResult<SateliteModel>> createSatelite([FromBody] SateliteModel model)
        {
            try
            {
                var output = await _db.SateliteCreate(model);
                return _response.getResponse(output, "Error while creating Satelite");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getSatelite")]
        public async Task<ActionResult<SateliteModel>> getSateliteById(int id)
        {
            try
            {
                var output = await _db.SateliteGet(id);
                return _response.getResponse(output, "Satelite not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getSateliteByDistrict")]
        public async Task<ActionResult<IEnumerable<SateliteModel>>> getAllSatelite(int id)
        {
            try
            {
                var output = await _db.SateliteGetFromDistrict(id);
                return _response.getResponse(output, "Satelite not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllSatelite")]
        public async Task<ActionResult<IEnumerable<SateliteModel>>> getAllSatelite()
        {
            try
            {
                var output = await _db.SateliteGetAll();
                return _response.getResponse(output, "Satelite not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateSatelite")]
        public async Task<ActionResult<SateliteModel>> updateSatelite([FromBody] SateliteModel model)
        {
            try
            {
                await _db.SateliteUpdate(model);
                return _response.getResponse("", "Satelite not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteSatelite")]
        public async Task<ActionResult<SateliteModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.SateliteDelete(id);
                return _response.getResponse("", "Satelite Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
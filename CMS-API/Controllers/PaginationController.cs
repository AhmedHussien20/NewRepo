using Application_Layer.Interfaces;
using CMS_API.Services;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_API.Controllers
{
    [AllowAnonymous]
    public class PaginationController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public PaginationController(IDataService db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("paginationData")]
        public async Task<ActionResult<PaginationData>> paginationData(PaginationData model)
        {
            try
            {
                var output = await _db.PaginationData(model);
                return _response.getResponse(output, "PaginationData not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}

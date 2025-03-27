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
    public class netPURLOCATIONController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public netPURLOCATIONController(IDataService db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("getProvinces")]
        public async Task<ActionResult<IEnumerable<netPurLocationModel>>> getProvinces()
        {
            try
            {
                var output = await _db.NetProvinceGet();
                return _response.getResponse(output, "Province not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getProvincesByCode")]
        public async Task<ActionResult<IEnumerable<netPurLocationModel>>> getProvincesByCode(string provinceCode)
        {
            try
            {
                var output = await _db.NetProvinceGetByCode(provinceCode);
                return _response.getResponse(output, "Province not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getDistricts")]
        public async Task<ActionResult<IEnumerable<netPurLocationModel>>> getDistricts(string provinceCode)
        {
            try
            {
                var output = await _db.NetDistrictGet(provinceCode);
                return _response.getResponse(output, "Districts not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getDistrictsByName")]
        public async Task<ActionResult<IEnumerable<netPurLocationModel>>> getDistrictsByName(string provinceCode, string district)
        {
            try
            {
                var output = await _db.NetDistrictGetByName(provinceCode, district);
                return _response.getResponse(output, "Districts not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getSatelites")]
        public async Task<ActionResult<IEnumerable<netPurLocationModel>>> getSatelites(string disctrictCode)
        {
            try
            {
                var output = await _db.NetSateliteGet(disctrictCode);
                return _response.getResponse(output, "Satellite not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getByLocation")]
        public async Task<ActionResult<IEnumerable<netPurLocationModel>>> getByLocation(string location)
        {
            try
            {
                var output = await _db.NetLocationGet(location);
                return _response.getResponse(output, "Location not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

    }
}

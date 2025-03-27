using Application_Layer.Interfaces;
using CMS_API.Services;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Domain_Layer.Models.StatsModel;

namespace CMS_API.Controllers
{
    [AllowAnonymous]
    public class StatsController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public StatsController(IDataService db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("getDashboardStats")]
        public async Task<ActionResult<DashboardStatsModel>> DashboardStats([FromBody] StatsFilterModel model)
        {
            try
            {
                var output = await _db.DashboardStats(model);
                return _response.getResponse(output, "DashboardStats not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getDeviceStats")]
        public async Task<ActionResult<DevicesModel>> DeviceStats()
        {
            try
            {
                var output = await _db.DeviceStats();
                return _response.getResponse(output, "Device Stats found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getTransactionStats")]
        public async Task<ActionResult<DevicesModel>> TransactionStats()
        {
            try
            {
                var output = await _db.SalesStats();
                return _response.getResponse(output, "Sales Stats found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getSalesStats")]
        public async Task<ActionResult<DevicesModel>> SalesStats()
        {
            try
            {
                var output = await _db.SalesStats();
                return _response.getResponse(output, "Transaction Stats found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getTransactionStatsByDistrict")]
        public async Task<ActionResult<DevicesModel>> TransactionStatsByDistrict(string districtCode)
        {
            try
            {
                var output = await _db.TransactionStatsByDistrict(districtCode);
                return _response.getResponse(output, "Transaction Stats found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getSalesStatsByDistrict")]
        public async Task<ActionResult<DevicesModel>> SalesStatsByDistrict(string districtCode)
        {
            try
            {
                var output = await _db.SalesStatsByDistrict(districtCode);
                return _response.getResponse(output, "Sales Stats found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }


        [HttpPost]
        [Route("getTransactionStatsByYear")]
        public async Task<ActionResult<StatsByYear>> TransactionStatsByYear([FromBody] StatsFilterModel model)
        {
            try
            {
                var output = await _db.TransactionStatsByYear(model);
                return _response.getResponse(output, " Stats found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("getTransactionStatsByWeek")]
        public async Task<ActionResult<StatsByWeek>> TransactionStatsByWeek([FromBody] StatsFilterModel model)
        {
            try
            {
                var output = await _db.TransactionStatsByWeek(model);
                return _response.getResponse(output, " Stats found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("getTransactionStatsBySatelietSales")]
        public async Task<ActionResult<StatsBySatelietSales>> TransactionStatsBySatelietSales([FromBody] StatsFilterModel model)
        {
            try
            {
                var output = await _db.TransactionStatsBySatelietSales(model);
                return _response.getResponse(output, " Stats found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("getTransactionStatsByCropsSold")]
        public async Task<ActionResult<StatsBySatelietSales>> TransactionStatsByCropsSold([FromBody] StatsFilterModel model)
        {
            try
            {
                var output = await _db.TransactionStatsByCropsSold(model);
                return _response.getResponse(output, " Stats found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
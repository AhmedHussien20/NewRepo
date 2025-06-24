using Application_Layer.Interfaces;
using CMS_API.Models;
using CMS_API.Services;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrustructure_Layer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_API.Controllers
{
    [AllowAnonymous]
    public class SalesController : BaseController
    {
        private readonly IDataService _db;
        public ResponseModel responseBody = new ResponseModel();
        public JsonResponse _response = new JsonResponse();

        public SalesController(IDataService db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("createGIN")]
        public async Task<ActionResult<GINModel>> createGIN([FromBody] GINRequestModel model)
        {
            General _gl = new();
            try
            {
                List<ResponseModel> jsonResponseArray = new List<ResponseModel>();
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        if (modelData.DOPreparedBy != null)
                        {
                            modelData.GINStatus = "Approved";
                        }

                        //get crop details
                        var cropDetails = await _db.CropsGetByName(modelData.ItemDescr);
                        modelData.ItemNum = cropDetails.ItemNo;
                        //modelData.sUnit = cropDetails.StockUnit;

                        var output = await _db.GINCreate(modelData);
                        if (output == 0)
                        {
                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = false,
                                results = await _db.GINGetById(modelData.STranNo)
                            });
                        }
                        else
                        {
                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = true,
                                errorMessage = "Error while making sale"
                            });
                            //await _gl.UpdateErrorLogAsync(Exeption e, "GIN CREATE 1");
                        }
                    }
                    catch (Exception e)
                    {
                        jsonResponseArray.Add(new ResponseModel
                        {
                            error = true,
                            errorMessage = e.Message
                        });
                        await _gl.UpdateErrorLogAsync(e, "GIN CREATE");
                    }
                }
                return _response.getResponse(jsonResponseArray, "");
            }
            catch (Exception e)
            {
                await _gl.UpdateErrorLogAsync(e, "GIN CREATE3");
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getGIN")]
        public async Task<ActionResult<GINModel>> getGINById(string id)
        {
            try
            {
                var output = await _db.GINGetById(id);
                return _response.getResponse(output, "GIN not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("updateGIN")]
        public async Task<ActionResult<TransactionsModel>> updateGIN([FromBody] GINModel model)
        {
            General _gl = new();
            try
            {
                await _db.GINUpdate(model);
                return _response.getResponse("", "GIN not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
                await _gl.UpdateErrorLogAsync(e, "UPDATE CREATE");
            }
        }

        //getAll
        [HttpPost]
        [Route("getAllGIN")]
        public async Task<ActionResult<IEnumerable<GINModel>>> getAllGIN([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.GINGetAll(model);
                return _response.getResponse(output, "GIN not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("getAllGINReport")]
        public async Task<ActionResult<IEnumerable<GINModel>>> getAllGINReport([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.GINGetAllReport(model);
                return _response.getResponse(output, "GIN not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpPost]
        [Route("getAllGINExcel")]
        public async Task<ActionResult<IEnumerable<GINModel>>> getAllGINExcel([FromBody] TransactionFilter model)
        {
            try
            {
                //var output = await _db.GINGetAll(model);
                List<GINModel> invList = await _db.GINGetAllExcel(model);

                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SLNO,DATE,DELIVERY ORDER NO,GIN Number,Transporter,Truck Number,QTY in Bags,QTY in TONs,Price,Representative,Identity,QTY Paid in TONs,Bal in Bags,Bal in TONs");

                foreach (var product in invList)
                {
                    var row_product = $"\"{product.OrderID}\",\"{product.EntryDate}\",\"{product.SLinkedTranNo}\",\"{product.STranNo}\",\"{product.STransporter}\",\"{product.SVehicleNumber}\",\"{product.Quantity * 20}\",\"{product.Quantity}\",\"{product.UnitPrice / 20}\",\"{product.SBillContact}\",\"{product.SIDNo}\",\"{""}\",\"{product.DQtyBO * 20}\",\"{product.DQtyBO}\"";
                    stringBuilder.AppendLine(row_product);
                }

                byte[] byteArray = Encoding.UTF8.GetBytes(stringBuilder.ToString());
                string fileName = $"{Guid.NewGuid()}.csv";

                return File(byteArray, "application/vnd.ms-excel", fileName);
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getGINByLocation")]
        public async Task<ActionResult<IEnumerable<GINModel>>> GINGetByLocation(String location, int page = 1, int rowCount = 80)
        {
            try
            {
                var output = await _db.GINGetByLocation(location, page, rowCount);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "netTrpTGin",
                        Column = "SLocationID",
                        Id = location,
                        rowCount = rowCount
                    };
                    var paginationDataa = await _db.PaginationData(paginationData);
                    DynamicResponse response = new DynamicResponse
                    {
                        error = false,
                        TotalRows = paginationDataa.TotalRows,
                        TotalPage = paginationDataa.TotalPage,
                        results = output
                    };
                    return Ok(response);
                }
                else
                {
                    ResponseModel response = new ResponseModel
                    {
                        error = true,
                        errorMessage = "GIN does not exist"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}

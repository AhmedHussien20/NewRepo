using Application_Layer.Interfaces;
using CMS_API.Models;
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
    public class TransactionsController : BaseController
    {
        private readonly IDataService _db;
        public ResponseModel responseBody = new ResponseModel();
        public JsonResponse _response = new JsonResponse();

        public TransactionsController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createTransactions")]
        public async Task<ActionResult<TransactionsModel>> createTransactions([FromBody] TransactionsRequestModel model)
        {
            try
            {
                List<ResponseModel> jsonResponseArray = new List<ResponseModel>();
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        var locationData = await _db.NetLocationGet(modelData.Location);
                        if (locationData != null)
                        {
                            var output = await _db.TransactionsCreate(modelData);
                            if (output > 0)
                            {
                                jsonResponseArray.Add(new ResponseModel
                                {
                                    error = false,
                                    results = output
                                });
                            }
                            else
                            {
                                jsonResponseArray.Add(new ResponseModel
                                {
                                    error = true,
                                    errorMessage = "Error while making purchase"
                                });
                            }
                        }
                        else
                        {
                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = true,
                                errorMessage = "Location not found"
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        jsonResponseArray.Add(new ResponseModel
                        {
                            error = true,
                            errorMessage = e.Message
                        });
                    }
                }
                return _response.getResponse(jsonResponseArray, "");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getTransactions")]
        public async Task<ActionResult<TransactionsModel>> getTransactionsById(int id)
        {
            try
            {
                var output = await _db.TransactionsGet(id);
                return _response.getResponse(output, "Transactions not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpPost]
        [Route("getAllTransactions")]
        public async Task<ActionResult<IEnumerable<TransactionsModel>>> getAllTransactions([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.TransactionsGetAll(model);
                return _response.getResponse(output, "Transactions not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateTransactions")]
        public async Task<ActionResult<TransactionsModel>> updateTransactions([FromBody] TransactionsModel model)
        {
            try
            {
                await _db.TransactionsUpdate(model);
                return _response.getResponse("", "Transactions not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteTransactions")]
        public async Task<ActionResult<TransactionsModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.TransactionsDelete(id);
                return _response.getResponse("", "Transactions Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
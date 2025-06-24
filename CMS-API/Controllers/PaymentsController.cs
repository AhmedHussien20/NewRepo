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
    public class PaymentsController : BaseController
    {
        private readonly IDataService _db;
        public ResponseModel responseBody = new ResponseModel();
        public JsonResponse _response = new JsonResponse();
        List<ResponseModel> jsonResponseArray = new List<ResponseModel>();

        public PaymentsController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("kycValidate")]
        public async Task<ActionResult<ResponseModelKyc>> farmerKyc([FromBody] KYCRequestModel models)
        {
            Microsoft.AspNetCore.Http.IHeaderDictionary headers = HttpContext.Request.Headers;
            try
            {
                var auth = await _db.GetAuthorization();
                if (auth != null)
                {
                    var output = await _db.FarmerKyc(auth.Token, models.Results);
                    if (output.Status == 700)
                    {
                        var ad = await authDevice();
                        if (ad.Value.Status == 0)
                        {
                            auth = await _db.GetAuthorization();
                            output = await _db.FarmerKyc(auth.Token, models.Results);
                        }
                    }
                    return _response.getResponse(output, "Authentication Failed"); // Returning the dummy device authentication model
                }
                else
                {
                    ResponseModel response = new ResponseModel
                    {
                        error = true,
                        errorMessage = "Failed to get authorization"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }


        //create
        [HttpPost]
        [Route("authDevices")]
        public async Task<ActionResult<DevicesAuthModel>> authDevice()
        {
            try
            {
                var auth = await _db.GetAuthorization();
                if (auth != null)
                {
                    string username = auth.Username; // Assuming "username" is the header key for username
                    string password = auth.Password; // Assuming "password" is the header key for password

                    // For demonstration purposes, creating a dummy DevicesAuthModel
                    var output = await _db.DevicesAuthentication(username, password);
                    return _response.getResponse(output, "Authentication Failed"); // Returning the dummy device authentication model

                }
                else
                {
                    ResponseModel response = new ResponseModel
                    {
                        error = true,
                        errorMessage = "Failed to get authorization"
                    };
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //create
        [HttpGet]
        [Route("getPaymentProviderInformation")]
        public async Task<ActionResult<PaymentProviderModel>> getPaymentProviderInformation()
        {
            Microsoft.AspNetCore.Http.IHeaderDictionary headers = HttpContext.Request.Headers;
            try
            {
                var auth = await _db.GetAuthorization();
                if (auth != null)
                {
                    string username = auth.Username; // Assuming "username" is the header key for username
                    string password = auth.Password; // Assuming "password" is the header key for password
                    string token = auth.Token; // Assuming "password" is the header key for password

                    // Validate username and password, perform authentication logic

                    // Example validation: Check if username and password are not empty
                    if (string.IsNullOrEmpty(token))
                    {
                        return BadRequest("Token Invalid");
                    }

                    // You can continue with your authentication logic here using username and password

                    // For demonstration purposes, creating a dummy DevicesAuthModel
                    var output = await _db.GetPaymentProviders(token);

                    if (output.Success)
                    {
                        try
                        {
                            foreach (var modelData in output.Data)
                            {
                                try
                                {

                                    int data = await _db.PaymentProviderCreate(modelData);


                                    if (modelData.Branches != null)
                                    {
                                        foreach (var branch in modelData.Branches)
                                        {
                                            data = await _db.BranchCreate(branch, modelData.Id);
                                        }
                                    }

                                    jsonResponseArray.Add(new ResponseModel
                                    {
                                        error = false,
                                        results = data
                                    });

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
                        }
                        catch (Exception e)
                        {
                            return _response.errorResponse(e.Message);
                        }
                    }

                    return _response.getResponse(jsonResponseArray, ""); // Returning the dummy device authentication model
                }
                else
                {
                    ResponseModel response = new ResponseModel
                    {
                        error = true,
                        errorMessage = "Failed to get authorization"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("GetPaymentProviders")]
        public async Task<ActionResult<DataModelPay>> GetPaymentProvider(int page = 1, int rowCount = 750)
        {
            try
            {
                var output = await _db.GetPaymentProviderApp(page, rowCount);
                if (output != null)
                {
                    DynamicResponse response = new DynamicResponse
                    {
                        error = false,
                        TotalRows = 1,
                        TotalPage = 1,
                        results = output
                    };
                    return Ok(response);
                }
                else
                {
                    ResponseModel response = new ResponseModel
                    {
                        error = true,
                        errorMessage = "Payment Provider does not exist"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("GetBranches")]
        public async Task<ActionResult<Branches>> GetBranch(int page = 1, int rowCount = 750)
        {
            try
            {
                var output = await _db.GetBranchesApp(page, rowCount);
                if (output != null)
                {
                    DynamicResponse response = new DynamicResponse
                    {
                        error = false,
                        TotalRows = 1,
                        TotalPage = 1,
                        results = output
                    };
                    return Ok(response);
                }
                else
                {
                    ResponseModel response = new ResponseModel
                    {
                        error = true,
                        errorMessage = "Branches does not exist"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
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
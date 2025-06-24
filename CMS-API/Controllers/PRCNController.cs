using Application_Layer.Interfaces;
using CMS_API.Models;
using CMS_API.Services;
using Domain_Layer.Models;
using Infrustructure_Layer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CMS_API.Controllers
{
    [AllowAnonymous]
    public class PRCNController : BaseController
    {
        private readonly IDataService _db;
        public ResponseModel responseBody = new ResponseModel();
        public JsonResponse _response = new JsonResponse();

        public PRCNController(IDataService db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("createPRCN")]
        public async Task<ActionResult<PRCNModel>> createPRCN([FromBody] PRCNRequestModel model)
        {
            General _gl = new();
            try
            {
                List<ResponseModel> jsonResponseArray = new List<ResponseModel>();

                foreach (var modelData in model.Results)
                {
                    try
                    {
                        if (modelData.verifyUser != null)
                        {
                            modelData.Status = "Approved";
                        }

                        //get crop details
                        var cropDetails = await _db.CropsGetByName(modelData.sItemDescr);
                        modelData.sItemNo = cropDetails.ItemNo;
                        modelData.sUnit = cropDetails.StockUnit;

                        var output = await _db.PRCNNCreate(modelData);
                        if (output == 0)
                        {
                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = false,
                                results = await _db.PRCNNGetById(modelData.sTranNo)
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
                    catch (Exception e)
                    {
                        jsonResponseArray.Add(new ResponseModel
                        {
                            error = true,
                            errorMessage = e.Message
                        });

                        await _gl.UpdateErrorLogAsync(e, "PRCN CREATE");
                    }
                }
                return _response.getResponse(jsonResponseArray, "");
            }
            catch (Exception e)
            {
                //await _gl.UpdateErrorLogAsync(e, "PRCN CREATE");
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("createPRCNTransaction")]
        public async Task<ActionResult<PRCNModel>> createPRCNTransaction([FromBody] PRCNRequestModel model)
        {
            try
            {
                List<ResponseModel> jsonResponseArray = new List<ResponseModel>();
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        if (modelData.verifyUser != null)
                        {
                            modelData.Status = "Approved";
                        }

                        //get crop details

                        var cropDetails = await _db.CropsGetByName(modelData.sItemDescr);
                        if (modelData.Type == 1)
                        {
                            cropDetails = await _db.CropsCommercialGetByName(modelData.sItemDescr);
                        }
                        modelData.sItemNo = cropDetails.ItemNo;
                        modelData.sUnit = cropDetails.StockUnit;

                        var output = await _db.PRCNCreate(modelData);
                        if (output == 0)
                        {
                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = false,
                                results = await _db.PRCNNGetById(modelData.sTranNo)
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
        [Route("getPRCN")]
        public async Task<ActionResult<PRCNModel>> getPRCNById(string id)
        {
            try
            {
                var output = await _db.PRCNNGet(id);
                return _response.getResponse(output, "PRCN not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("updatePRCN")]
        public async Task<ActionResult<TransactionsModel>> updatePRCN([FromBody] PRCNModel model)
        {
            try
            {
                await _db.PRCNUpdate(model);
                return _response.getResponse("", "PRCN not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }


        //getAll
        [HttpPost]
        [Route("getAllPRCN")]
        public async Task<ActionResult<IEnumerable<PRCNModel>>> getAllPRCN([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.PRCNGetAll(model);
                return _response.getResponse(output, "PRCN not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpPost]
        [Route("getAllBCL")]
        public async Task<ActionResult<IEnumerable<BCLModel>>> getAllBCL([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.BCLGetAll(model);
                return _response.getResponse(output, "BCL not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("getAllBCLPending")]
        public async Task<ActionResult<IEnumerable<BCLModel>>> getAllBCLPending([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.BCLGetAllPending(model);
                return _response.getResponse(output, "BCL not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
        [HttpPost]
        [Route("getAllBCLPosted")]
        public async Task<ActionResult<IEnumerable<BCLModelPOSTED>>> getAllBCLPosted([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.BCLGetAllPosted(model);
                return _response.getResponse(output, "BCL not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("authDevice")]
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

        [HttpPost]
        [Route("postBCL")]
        public async Task<ActionResult<IEnumerable<PostBCLModelResponse>>> PostBCL([FromBody] PostBCLModel model)
        {
            try
            {
                // Get request headers
                var headers = HttpContext.Request.Headers;

                // Retrieve authorization
                var auth = await _db.GetAuthorization();
                if (auth == null)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Failed to get authorization" });
                }

                // Retrieve BCL
                var status = model.status;
                var prcn = await _db.PRCNNGet(model.sTranNo.Trim());
                var bcl = await _db.BCLGet(model.sTranNo);
                var devices = await _db.DevicesGetByDoc(prcn.SATELITE, prcn.terminal);
                var farmer = await _db.FarmersGetFromNrc(prcn.sVendorID);

                if (bcl == null)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Failed to get BCL" });
                }

                if (prcn == null)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Failed to get PRCN" });
                }

                if (devices == null)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Failed to get Device" });
                }

                if (farmer == null)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Failed to get Farmer" });
                }

                string username = auth.Username; // Assuming "username" is the header key for username
                string password = auth.Password; // Assuming "password" is the header key for password

                // For demonstration purposes, creating a dummy DevicesAuthModel
                var deviceAuthentication = await _db.DevicesAuthentication(username, password);

                // Create BCL
                var output = await _db.BCLCreate(bcl, deviceAuthentication.Data.Token);
                if (output.status != 0)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Create BCL failed", results = output });
                }

                // Check if status is 700
                if (output.status == 700)
                {
                    // Get authorization device
                    var ad = await authDevice();
                    /*DevicesAuthModel responseObject = JsonConvert.DeserializeObject<DevicesAuthModel>(ad.ToString());
                    if (apod.Value.Status != 0)
                    {
                        return Ok(new ResponseModel { error = true, errorMessage = "Failed to get reauthorization", results = ad });
                    */

                    // Reauthorize and create BCL again
                    auth = await _db.GetAuthorization();
                    output = await _db.BCLCreate(bcl, deviceAuthentication.Data.Token);
                    if (output.status != 0)
                    {
                        return Ok(new ResponseModel { error = true, errorMessage = "Create BCL failed after reauthorization", results = output });
                    }

                }


                // Create and attach BCL
                output = await _db.BCLCreateAttach(bcl, prcn, devices, farmer, deviceAuthentication.Data.Token);
                if (output.status != 0)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Failed to create and attach BCL", results = output });
                }

                // Update BCL status
                var bclUpdate = await _db.BCLUpdateStatus(bcl, status);
                return Ok(new ResponseModel { error = false, errorMessage = "Success", results = bclUpdate });

                //return Ok(new ResponseModel { error = false, errorMessage = "Success", results = bcl });
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("postBCLBulk")]
        public async Task<ActionResult<IEnumerable<PostBCLModelResponse>>> PostBCLBuik([FromBody] BulkBCLModel model)
        {
            try
            {
                // Get request headers (not used, so this can be removed if unnecessary)
                var headers = HttpContext.Request.Headers;

                // Retrieve authorization
                var auth = await _db.GetAuthorization();
                if (auth == null)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Failed to get authorization" });
                }

                // Retrieve BCL from DB
                var status = model.status;
                var BCLnumber = model.sTranNo;
                var bcl = await _db.BulkBCLGet(model.sTranNo);
                //var bcl = bclList.FirstOrDefault();
                if (bcl == null)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Failed to get BCL" });
                }

                // Authenticate device
                var deviceAuthentication = await _db.DevicesAuthentication(auth.Username, auth.Password);
                if (deviceAuthentication == null || deviceAuthentication.Data == null)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Device authentication failed" });
                }

                string token = deviceAuthentication.Data.Token;

                // Step 3: Prepare dynamic BCLModel from DB data
                var modelToPost = new BulkBCLModel
                {
                    sTranNo = bcl.sTranNo,
                    PROVINCE = bcl.PROVINCE,
                    TotalAmount = bcl.TotalAmount,
                    status = model.status,
                    bclnumber = bcl.bclnumber,
                    Transactions = new List<TransactionModel>()
                };

                // Assuming bcl.Lines contains list of line items for the transaction
                foreach (var line in bcl.Lines)
                {
                    var txn = new TransactionModel
                    {
                        AccountNumber = line.AccountNumber,
                        Amount = line.dDocTotal,
                        BankBranchId = line.BankBranchId,
                        Names = line.Names,
                        NRCNumber = line.NRCNumber,
                        NoOfBags = line.NoOfBags,
                        PaymentProviderId = line.PaymentProviderId,
                        PrcnNumber = line.rcpno,
                        SerialNumber = line.sTranNo
                    };

                    modelToPost.Transactions.Add(txn);
                }

                // Step 4: Post BCL bulk attachment
                var result = await _db.BulkBCL(modelToPost, token);
                var output = result.FirstOrDefault();
                if (output.status != 0)
                {
                    return Ok(new ResponseModel { error = true, errorMessage = "Failed to create and attach BCL", results = output });
                }

                // Step 5: Update BCL status (uncomment if implemented)
                status = 1;
                var bclUpdate = await _db.BulkBCLUpdateStatus(BCLnumber, status);

                return Ok(new ResponseModel { error = false, errorMessage = "Success" /* results = bclUpdate */ });
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("updateBCLStatus")]
        public async Task<ActionResult<TransactionsModel>> updateBCLStatus([FromBody] UpdateBCLModel model)
        {
            try
            {
                int status = 1;
                int rowsAffected = await _db.UpdateBCL(model, status);

                if (rowsAffected > 0)
                {
                    return Ok(new
                    {
                        response_code = 100,
                        status = "success",
                        message = "Record Updated Successfully",
                        data = new
                        {
                            bclnumber = model.bclnumber,
                            sTransNo = model.sTranNo
                        }
                    });
                }
                else
                {
                    return Ok(new
                    {
                        response_code = 105,
                        status = "Failed",
                        message = "Record not found to Update",
                        data = new { }
                    });
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getPRCNByLocation")]
        public async Task<ActionResult<IEnumerable<PRCNModel>>> PRCNGetByLocation(String location, int page = 1, int rowCount = 80)
        {
            try
            {
                var output = await _db.PRCNGetByLocation(location, page, rowCount);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "netPRCNverify",
                        Column = "sTranNo",
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
                        errorMessage = "PRCN does not exist"
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

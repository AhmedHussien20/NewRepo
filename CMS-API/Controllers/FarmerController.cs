using Application_Layer.Interfaces;
using CMS_API.Models;
using CMS_API.Services;
using Crop_Management_System.Services;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS_API.Controllers
{
    //Farmer
    [AllowAnonymous]
    public class FarmerController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();
        public ResponseModel responseBody = new ResponseModel();
        public DynamicResponse _farmerResponse;
        List<ResponseModel> jsonResponseArray = new List<ResponseModel>();

        public FarmerController(IDataService db)
        {
            _db = db;
        }

        #region Local Farmers

        //--create
        [HttpPost]
        [Route("createFarmer")]
        public async Task<ActionResult<FarmerModel>> createFarmer([FromBody] FarmersRequestModel model)
        {
            EmailHandlerApi email = new EmailHandlerApi();
            int attempt = 1;

            try
            {
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        var farmerData = await _db.FarmersGetFromNrc(modelData.NRCNumber);
                        if (farmerData == null)
                        {
                            var output = await _db.FarmerCreate(modelData);
                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = false,
                                results = output
                            });
                            /*await email.emailLogger("muyangwam@netone.co.zm",
                            output.ToString() + ", Account Name: " +
                            modelData.AccountName + ", Account Number: " +
                            modelData.AccountNumber + ", Bank Name: " +
                            modelData.BankName + ", Branch Code: " +
                            modelData.BranchCode + ", Branch Name: " +
                            modelData.BranchName + ", Created By: " +
                            modelData.CreatedBy + ", District: " +
                            modelData.District + ", DOB: " +
                            modelData.DOB + ", Gender: " +
                            modelData.Gender + ", Modify By: " +
                            modelData.ModifyBy + ", Modify On: " +
                            modelData.ModifyOn + ", Names: " +
                            modelData.Names + ", NRC: " +
                            modelData.NRCNumber + ", Payment Mode: " +
                            modelData.PaymentMode + ", Phone: " +
                            modelData.PhoneNumber + ", Province: " +
                            modelData.Province + ", Serial: " +
                            modelData.SerialNumber + ", Verified: " +
                            modelData.Verified + ", Verified String: " +
                            modelData.VerifiedString + ", Id: " +
                            modelData.Id + ", ", "Farmer Create");

                            while (attempt < 2)
                            {
                                

                                if (output == 0)
                                {
                                    attempt++;
                                }
                                else
                                {
                                    jsonResponseArray.Add(new ResponseModel
                                    {
                                        error = false,
                                        results = output
                                    });
                                    await email.emailLogger("muyangwam@netone.co.zm",
                                output.ToString() + ", Account Name: " +
                                modelData.AccountName + ", Account Number: " +
                                modelData.AccountNumber + ", Bank Name: " +
                                modelData.BankName + ", Branch Code: " +
                                modelData.BranchCode + ", Branch Name: " +
                                modelData.BranchName + ", Created By: " +
                                modelData.CreatedBy + ", District: " +
                                modelData.District + ", DOB: " +
                                modelData.DOB + ", Gender: " +
                                modelData.Gender + ", Modify By: " +
                                modelData.ModifyBy + ", Modify On: " +
                                modelData.ModifyOn + ", Names: " +
                                modelData.Names + ", NRC: " +
                                modelData.NRCNumber + ", Payment Mode: " +
                                modelData.PaymentMode + ", Phone: " +
                                modelData.PhoneNumber + ", Province: " +
                                modelData.Province + ", Serial: " +
                                modelData.SerialNumber + ", Verified: " +
                                modelData.Verified + ", Verified String: " +
                                modelData.VerifiedString + ", Id: " +
                                modelData.Id + ", ", "Farmer Create");
                                    attempt = 4;
                                }
                            }*/

                        }
                        else
                        {
                            /*jsonResponseArray.Add(new ResponseModel
                            {
                                error = true,
                                errorMessage = "NRC Number already exists"
                            });*/
                            var output = await _db.FarmerUpdate(modelData);

                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = false,
                                results = output
                            });

                            /*await email.emailLogger("muyangwam@netone.co.zm",
                                output.ToString() + ", Account Name: " +
                                modelData.AccountName + ", Account Number: " +
                                modelData.AccountNumber + ", Bank Name: " +
                                modelData.BankName + ", Branch Code: " +
                                modelData.BranchCode + ", Branch Name: " +
                                modelData.BranchName + ", Created By: " +
                                modelData.CreatedBy + ", District: " +
                                modelData.District + ", DOB: " +
                                modelData.DOB + ", Gender: " +
                                modelData.Gender + ", Modify By: " +
                                modelData.ModifyBy + ", Modify On: " +
                                modelData.ModifyOn + ", Names: " +
                                modelData.Names + ", NRC: " +
                                modelData.NRCNumber + ", Payment Mode: " +
                                modelData.PaymentMode + ", Phone: " +
                                modelData.PhoneNumber + ", Province: " +
                                modelData.Province + ", Serial: " +
                                modelData.SerialNumber + ", Verified: " +
                                modelData.Verified + ", Verified String: " +
                                modelData.VerifiedString + ", Id: " +
                                modelData.Id + ", ", "Farmer Update");*/
                        }
                    }
                    catch (Exception e)
                    {
                        jsonResponseArray.Add(new ResponseModel
                        {
                            error = true,
                            errorMessage = e.Message
                        });

                        /*await email.emailLogger("muyangwam@netone.co.zm", e.Message + ", Account Name: " +
                                modelData.AccountName + ", Account Number: " +
                                modelData.AccountNumber + ", Bank Name: " +
                                modelData.BankName + ", Branch Code: " +
                                modelData.BranchCode + ", Branch Name: " +
                                modelData.BranchName + ", Created By: " +
                                modelData.CreatedBy + ", District: " +
                                modelData.District + ", DOB: " +
                                modelData.DOB + ", Gender: " +
                                modelData.Gender + ", Modify By: " +
                                modelData.ModifyBy + ", Modify On: " +
                                modelData.ModifyOn + ", Names: " +
                                modelData.Names + ", NRC: " +
                                modelData.NRCNumber + ", Payment Mode: " +
                                modelData.PaymentMode + ", Phone: " +
                                modelData.PhoneNumber + ", Province: " +
                                modelData.Province + ", Serial: " +
                                modelData.SerialNumber + ", Verified: " +
                                modelData.Verified + ", Verified String: " +
                                modelData.VerifiedString + ", Id: " +
                                modelData.Id + ", Attempts: " +
                                attempt, "Farmer Exception");*/
                    }
                }
                return _response.getResponse(jsonResponseArray, "");
            }
            catch (Exception e)
            {
                await email.emailLogger("muyangwam@netone.co.zm", e.Message, "API Exception");
                return _response.errorResponse(e.Message);
            }
        }

        //--create
        [HttpPost]
        [Route("createFarmerv2")]
        public async Task<ActionResult<FarmerModel>> createFarmerv2([FromBody] FarmersRequestModel model)
        {
            EmailHandlerApi email = new EmailHandlerApi();
            int attempt = 1;

            try
            {
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        var farmerData = await _db.FarmersGetFromNrc(modelData.NRCNumber);
                        if (farmerData == null)
                        {
                            var output = await _db.FarmerCreatev2(modelData);
                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = false,
                                results = output
                            });
                        }
                        else
                        {
                            var output = await _db.FarmerUpdatev2(modelData);

                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = false,
                                results = output
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
                await email.emailLogger("thomasn@netone.co.zm", e.Message, "API Exception");
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getFarmer")]
        public async Task<ActionResult<FarmerModel>> getFarmerById(int id)
        {
            try
            {
                var output = await _db.FarmerGet(id);
                return _response.getResponse(output, "Farmer not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getFarmerByNrc")]
        public async Task<ActionResult<FarmerModel>> getFarmerByNrc(string nrc)
        {
            try
            {
                var output = await _db.FarmersGetFromNrc(nrc);
                return _response.getResponse(output, "Farmer not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getFarmerByLocation")]
        public async Task<ActionResult<IEnumerable<FarmerModel>>> getFarmerByLocation(string location, int page = 1)
        {
            try
            {
                var output = await _db.FarmerGetByLocation(location, page);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "Farmers",
                        Column = "Province",
                        Id = location,
                        rowCount = 750
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
                        errorMessage = "Farmer does not exist"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllFarmer")]
        public async Task<ActionResult<IEnumerable<FarmerModel>>> getAllFarmer()
        {
            try
            {
                var output = await _db.FarmerGetAll();
                return _response.getResponse(output, "Farmer not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }


        //update
        [HttpPost]
        [Route("updateFarmer")]
        public async Task<ActionResult<FarmerModel>> updateFarmer([FromBody] FarmerModel model)
        {
            try
            {
                await _db.FarmerUpdate(model);
                return _response.getResponse("", "Farmer not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteFarmer")]
        public async Task<ActionResult<FarmerModel>> deleteUserAsync(int id)
        {
            try
            {
                var userData = await _db.FarmerGet(id);
                await _db.FarmerDelete(id);
                return _response.getResponse("", "Farmer Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        #endregion

        #region Commercial Farmers

        //--create
        [HttpPost]
        [Route("createFarmerCommercial")]
        public async Task<ActionResult<FarmerModel>> createFarmerCommercial([FromBody] FarmersRequestModel model)
        {
            EmailHandlerApi email = new EmailHandlerApi();
            int attempt = 1;

            try
            {
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        var farmerData = await _db.FarmersCommercialGetFromCode(modelData.FarmerCode);
                        if (farmerData == null)
                        {
                            var output = await _db.FarmerCommercialCreate(modelData);
                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = false,
                                results = output
                            });
                        }
                        else
                        {
                            var output = await _db.FarmerUpdate(modelData);

                            jsonResponseArray.Add(new ResponseModel
                            {
                                error = false,
                                results = output
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
                await email.emailLogger("muyangwam@netone.co.zm", e.Message, "API Exception");
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getFarmerCommercial")]
        public async Task<ActionResult<FarmerModel>> getFarmerCommercialById(int id)
        {
            try
            {
                var output = await _db.FarmerCommercialGet(id);
                return _response.getResponse(output, "Commercial Farmer not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getFarmerCommercialByCode")]
        public async Task<ActionResult<FarmerModel>> getFarmerCommercialByCode(string code)
        {
            try
            {
                var output = await _db.FarmersCommercialGetFromCode(code);
                return _response.getResponse(output, "Commercial Farmer not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getFarmerCommercialByLocation")]
        public async Task<ActionResult<IEnumerable<FarmerModel>>> getFarmerCommercialByLocation(string location, int page = 1)
        {
            try
            {
                var output = await _db.FarmerCommercialGetByLocation(location, page);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "FarmersCommercial",
                        Column = "Province",
                        Id = location,
                        rowCount = 750
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
                        errorMessage = "Commercial Farmer does not exist"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllCommercialFarmer")]
        public async Task<ActionResult<IEnumerable<FarmerModel>>> getAllCommercialFarmer()
        {
            try
            {
                var output = await _db.FarmerGetAll();
                return _response.getResponse(output, "Commercial Farmer not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateFarmerCommercial")]
        public async Task<ActionResult<FarmerModel>> updateFarmerCommercial([FromBody] FarmerModel model)
        {
            try
            {
                await _db.FarmerCommercialUpdate(model);
                return _response.getResponse("", "Commercial Farmer not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteFarmerCommercial")]
        public async Task<ActionResult<FarmerModel>> deleteFarmerCommercialAsync(int id)
        {
            try
            {
                var userData = await _db.FarmerCommercialGet(id);
                await _db.FarmerCommercialDelete(id);
                return _response.getResponse("", "Commercial Farmer Could not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        #endregion
    }
}
using Application_Layer.Interfaces;
using CMS_API.Models;
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
    public class TransportationController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();
        public ResponseModel responseBody = new ResponseModel();
        List<ResponseModel> jsonResponseArray = new List<ResponseModel>();

        public TransportationController(IDataService db)
        {
            _db = db;
        }

        #region IDTs

        [HttpPost]
        [Route("IDTCreate")]
        public async Task<ActionResult<IDTModel>> IDTCreate([FromBody] IDTRequestModel model)
        {
            try
            {
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        //get location
                        var locationData = await _db.NetLocationGet(modelData.FromLoc);
                        if (locationData != null)
                        {
                            modelData.GITLOC = locationData.GITLOC;
                            var output = await _db.IDTCreate(modelData);
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
                                errorMessage = $"Invalid location '{modelData.FromLoc}' passed"
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

        [HttpPost]
        [Route("updateIDT")]
        public async Task<ActionResult<IDTModel>> updateIDT([FromBody] IDTModel model)
        {
            try
            {
                await _db.IDTUpdate(model);
                return _response.getResponse("IDT Updated", "IDT not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("updateIDTStatus")]
        public async Task<ActionResult<IDTModel>> updateIDTStatus([FromBody] IDTModel model)
        {
            try
            {
                await _db.IDTUpdateStatus(model);
                return _response.getResponse("", "IDT Status not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("IDTGetByLocation")]
        public async Task<ActionResult<IDTModel>> IDTGetByLocation(string location, int page = 1, int rowCount = 750)
        {
            try
            {
                var output = await _db.IDTGetByLocation(location, page, rowCount);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "netTrpTrs",
                        Column = "ToLoc",
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
                        errorMessage = "IDT does not exist"
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
        [Route("getAllIDT")]
        public async Task<ActionResult<IEnumerable<IDTModel>>> getAllIDT([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.IDTGetAll(model);
                return _response.getResponse(output, "IDT not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        #endregion IDTs

        #region GRNs

        [HttpPost]
        [Route("GRNCreate")]
        public async Task<ActionResult<GRNModel>> GRNCreate([FromBody] GRNRequestModel model)
        {
            try
            {
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        var output = await _db.GRNCreate(modelData);
                        jsonResponseArray.Add(new ResponseModel
                        {
                            error = false,
                            results = output
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
                return _response.getResponse(jsonResponseArray, "");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("updateGRN")]
        public async Task<ActionResult<GRNModel>> updateGRN([FromBody] GRNModel model)
        {
            try
            {
                await _db.GRNUpdate(model);
                return _response.getResponse("GRN Updated", "GRN not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("getAllGRN")]
        public async Task<ActionResult<IEnumerable<GRNModel>>> getAllGRN([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.GRNGetAll(model);
                return _response.getResponse(output, "GRN not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("GRNGetByLocation")]
        public async Task<ActionResult<GRNModel>> GRNGetByLocation(string location, int page = 1, int rowCount = 750)
        {
            try
            {
                var output = await _db.GRNGetByLocation(location, page, rowCount);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "netTrpTGrn",
                        Column = "FromLoc",
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
                        errorMessage = "GRN does not exist"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        #endregion GRNs

        #region Loading Orders

        [HttpPost]
        [Route("getAllLoadingOrders")]
        public async Task<ActionResult<IEnumerable<LoadingOrdersModel>>> getAllLoadingOrders([FromBody] TransactionFilter model)
        {
            try
            {
                var output = await _db.LoadingOrdersGetAll(model);
                return _response.getResponse(output, "Loading Orders not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("LoadingOrdersGetByLocation")]
        public async Task<ActionResult<LoadingOrdersModel>> LoadingOrdersGetByLocation(string location, int page = 1, int rowCount = 750)
        {
            try
            {
                var output = await _db.LoadingOrdersGetByLocation(location, page, rowCount);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "netTrpLO",
                        Column = "FromLoc",
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
                        errorMessage = "Loading Order does not exist"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        #endregion Loading Orders

        #region Delivery Orders

        [HttpGet]
        [Route("DeliveryOrdersGetByLocation")]
        public async Task<ActionResult<DeliveryOrdersModel>> DeliveryOrdersGetByLocation(string location, int page = 1, int rowCount = 750)
        {
            try
            {
                var output = await _db.DeliveryOrdersGetByLocation(location, page, rowCount);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "tblOrderDetail",
                        Column = "sLocationID",
                        Id = location,
                        rowCount = rowCount
                    };
                    var paginationDataa = await _db.PaginationDataSrpos(paginationData);
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
                        errorMessage = "Delivery Order does not exist"
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
        [Route("DeliveryOrdersDetailsGetByLocation")]
        public async Task<ActionResult<DeliveryOrdersDetailsModel>> DeliveryOrdersDetailsGetByLocation(string location, int page = 1, int rowCount = 750)
        {
            try
            {
                var output = await _db.DeliveryOrdersDetailsGetByLocation(location, page, rowCount);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "tblOrderDetail",
                        Column = "sLocationID",
                        Id = location,
                        rowCount = rowCount
                    };
                    var paginationDataa = await _db.PaginationDataSrpos(paginationData);
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
                        errorMessage = "Delivery Order Details does not exist"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        #endregion Delivery Orders

        #region GINs

        [HttpPost]
        [Route("GINCreate")]
        public async Task<ActionResult<GINModel>> GINCreate([FromBody] GINRequestModel model)
        {
            try
            {
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        var output = await _db.GINCreate(modelData);
                        jsonResponseArray.Add(new ResponseModel
                        {
                            error = false,
                            results = output
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
                return _response.getResponse(jsonResponseArray, "");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("GINDetailsCreate")]
        public async Task<ActionResult<GINDetailsModel>> GINDetailsCreate([FromBody] GINDetailsRequestModel model)
        {
            try
            {
                foreach (var modelData in model.Results)
                {
                    try
                    {
                        //get location
                        var locationData = await _db.NetLocationGet(modelData.SLocationID);
                        if (locationData != null)
                        {
                            //modelData.GITLOC = locationData.GITLOC;
                            var output = await _db.GINDetailsCreate(modelData);
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
                                errorMessage = $"Invalid location '{modelData.SLocationID}' passed"
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

        [HttpPost]
        [Route("GINUpdate")]
        public async Task<ActionResult<GINModel>> updateGIN([FromBody] GINModel model)
        {
            try
            {
                await _db.GINUpdate(model);
                return _response.getResponse("GIN Updated", "GIN not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("updateGINDetails")]
        public async Task<ActionResult<GINDetailsModel>> updateGINDetails([FromBody] GINDetailsModel model)
        {
            try
            {
                await _db.GINDetailsUpdate(model);
                return _response.getResponse("GIN Details Updated", "GIN Details not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("updateGINStatus")]
        public async Task<ActionResult<GINModel>> updateGINStatus([FromBody] GINModel model)
        {
            try
            {
                await _db.GINUpdateStatus(model);
                return _response.getResponse("", "GIN Status not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("GINGetByLocation")]
        public async Task<ActionResult<GINModel>> GINGetByLocation(string location, int page = 1, int rowCount = 750)
        {
            try
            {
                var output = await _db.GINGetByLocation(location, page, rowCount);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "netTrpTGin",
                        Column = "STerminal",
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

        [HttpGet]
        [Route("GINDetailsGetByLocation")]
        public async Task<ActionResult<GINDetailsModel>> GINDetailsGetByLocation(string location, int page = 1, int rowCount = 750)
        {
            try
            {
                var output = await _db.GINDetailsGetByLocation(location, page, rowCount);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "tblOrderDetail",
                        Column = "sLocationID",
                        Id = location,
                        rowCount = rowCount
                    };
                    var paginationDataa = await _db.PaginationDataSrpos(paginationData);
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
                        errorMessage = "GIN Details does not exist"
                    };
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        #endregion GINs
    }
}

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
    public class AccountStatusController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public AccountStatusController(IDataService db)
        {
            _db = db;
        }

        //create
        [HttpPost]
        [Route("createAccountStatus")]
        public async Task<ActionResult<AccountStatusModel>> createAccountStatus([FromBody] AccountStatusModel model)
        {
            try
            {
                var output = await _db.AccountStatusCreate(model);
                return _response.getResponse(output, "Error while creating AccountStatus");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getAccountStatus")]
        public async Task<ActionResult<AccountStatusModel>> getAccountStatusById(int id)
        {
            try
            {
                var output = await _db.AccountStatusGet(id);
                return _response.getResponse(output, "AccountStatus not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllAccountStatus")]
        public async Task<ActionResult<IEnumerable<AccountStatusModel>>> getAllAccountStatus()
        {
            try
            {
                var output = await _db.AccountStatusGetAll();
                return _response.getResponse(output, "AccountStatus not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateAccountStatus")]
        public async Task<ActionResult<AccountStatusModel>> updateAccountStatus([FromBody] AccountStatusModel model)
        {
            try
            {
                await _db.AccountStatusUpdate(model);
                return _response.getResponse("", "AccountStatus not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteAccountStatus")]
        public async Task<ActionResult<AccountStatusModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.AccountStatusDelete(id);
                return _response.getResponse("", "AccountStatus Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
using Application_Layer.Interfaces;
using CMS_API.Models;
using CMS_API.Services;
using Crop_Management_System.Services;
using Domain_Layer.Models;
using Infrustructure_Layer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_API.Controllers
{
    [AllowAnonymous]
    public class UsersController : BaseController
    {
        private readonly IDataService _db;
        private readonly IConfiguration _config;
        public ResponseModel response;
        public JsonResponse _response = new JsonResponse();
        public Encryption _encryption = new Encryption();

        public UsersController(IDataService db, IConfiguration _config)
        {
            _db = db;
            this._config = _config;
        }

        //create
        [HttpPost]
        [Route("createUsers")]
        public async Task<ActionResult<UsersModel>> createUsers([FromBody] UsersModel model)
        {
            try
            {
                //check if email exists
                var account = await _db.UsersGetByEmail(model.Email);
                if (account == null)
                {
                    var output = await _db.UsersCreate(model);
                    if (output != 0)
                    {
                        //getUser
                        var accountData = await _db.UsersGetByEmail(model.Email);
                        //send Reg Email
                        //await _emailHandler.sendRegistrationEmailAsync(context: HttpContext,
                        //                             _config: _config,
                        //                             email: accountData.Email,
                        //                             password: _encryption.Decrypt(accountData.Password),
                        //                             key: accountData.SecurityStamp);
                    }
                    return _response.getResponse(output, "Error while creating Users");
                }
                else
                {
                    return _response.errorResponse("Account already exists");
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("sendSubscription")]
        public async Task<int> sendSubscription()
        {
            try
            {

                //send Reg Email
                EmailHandler _emailHandler = new EmailHandler();
                await _emailHandler.sendSubscriptionEmailAsync(context: HttpContext,
                                                     _config: _config,
                                                     email: "muyangwam@netone.co.zm"
                                                     );
                return 1;

            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<UsersModel>> getUsersById(int id)
        {
            try
            {
                var output = await _db.UsersGet(id);
                return _response.getResponse(output, "Users not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("deviceLogin")]
        public async Task<ActionResult<UsersModel>> UsersDeviceLogin([FromBody] UsersModel model)
        {
            try
            {
                var output = await _db.UsersDeviceLogin(model);
                return _response.getResponse(output, "Users not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("deviceRegistrationLogin")]
        public async Task<ActionResult<UsersModel>> UsersDeviceRegistrationLogin([FromBody] UsersModel model)
        {
            try
            {
                var output = await _db.UsersDeviceRegistrationLogin(model);

                if (output != null)
                {
                    if (output.AccountStatus.Title != "Active")
                    {
                        return _response.getResponse(null, "Cannot Register Device. Your Account is " + output.AccountStatus.Title);
                    }
                    return _response.getResponse(output, "User Found Successfully");
                }

                return _response.getResponse(null, "User not found!! Please verify your email or password or authorization rights!!");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("portalLogin")]
        public async Task<ActionResult<UsersModel>> UsersPortalLogin([FromBody] UsersModel model)
        {
            try
            {
                var output = await _db.UsersPortalLogin(model);
                if (output != null)
                {
                    response = new ResponseModel
                    {
                        error = false,
                        results = output
                    };
                    return Ok(response);
                }
                else
                {
                    //check if is device sign 
                    var data = await _db.UsersDeviceLogin(model);
                    if (data != null)
                        response = new ResponseModel
                        {
                            error = true,
                            errorMessage = "You do not have the appropriate credentials to access this portal, Please contact an administrator"
                        };
                    else
                        response = new ResponseModel
                        {
                            error = true,
                            errorMessage = "Account not found"
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
        [Route("usersResetPassword")]
        public async Task<ActionResult<UsersModel>> usersResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                var output = await _db.UsersResetPassword(model);
                return _response.getResponse(output, "Users not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("getAllUsers")]
        public async Task<ActionResult<IEnumerable<UsersModel>>> getAllUsers()
        {
            try
            {
                var output = await _db.UsersGetAll();
                return _response.getResponse(output, "Users not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateUsers")]
        public async Task<ActionResult<UsersModel>> updateUsers([FromBody] UsersModel model)
        {
            try
            {
                await _db.UsersUpdate(model);
                return _response.getResponse("", "Users not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteUsers")]
        public async Task<ActionResult<UsersModel>> deleteUserAsync(int id)
        {
            try
            {
                await _db.UsersDelete(id);
                return _response.getResponse("", "Users Cold not be deleted");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }


    }
}
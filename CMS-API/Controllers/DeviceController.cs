using Application_Layer.Interfaces;
using CMS_API.Models;
using CMS_API.Services;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_API.Controllers
{
    [AllowAnonymous]
    public class DeviceController : BaseController
    {
        private readonly IDataService _db;
        public JsonResponse _response = new JsonResponse();

        public DeviceController(IDataService db)
        {
            _db = db;
        }

        //get
        [HttpGet]
        [Route("getDevicesCount")]
        public async Task<ActionResult<int>> getDevicesCount()
        {
            try
            {
                var output = await _db.DevicesGetCount();
                return _response.getResponse(output, "Devices not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //create
        [HttpPost]
        [Route("createDevices")]
        public async Task<ActionResult<DevicesModel>> CreateDevices([FromBody] DevicesModel model)
        {
            try
            {
                // Check if the device exists
                var deviceData = await _db.DevicesGetBySerialNumber(model.SerialNumber);

                if (deviceData != null)
                {
                    // Update existing device status
                    deviceData.DeviceStatusId = 4;
                    deviceData.ModifiedBy = model.Username;
                    deviceData.Username = model.Username;
                    await _db.DevicesUpdate(deviceData);
                    var updatedDevice = await _db.DevicesGetBySerialNumber(model.SerialNumber);
                    return _response.getResponse(updatedDevice, "Device updated successfully");
                }

                // Check device count before adding a new device
                if (await _db.DevicesGetCount() >= 1750)
                {
                    return _response.errorResponse("Device Limit Reached");
                }

                // Create the new device
                model.DeviceStatusId = 4;
                //deviceData.CreatedBy = model.Username;
                //deviceData.Username = model.Username;
                await _db.DevicesCreate(model);

                // Retrieve and return the newly created device
                var newDevice = await _db.DevicesGetBySerialNumber(model.SerialNumber);
                return _response.getResponse(newDevice, "Device created successfully");
            }
            catch (Exception e)
            {
                return _response.errorResponse($"An error occurred: {e.Message}");
            }
        }


        //get
        [HttpGet]
        [Route("getDevices")]
        public async Task<ActionResult<DevicesModel>> getDevicesById(int id)
        {
            try
            {
                var output = await _db.DevicesGet(id);
                return _response.getResponse(output, "Devices not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getAllDevices")]
        public async Task<ActionResult<DevicesModel>> getAllDevices()
        {
            try
            {
                var output = await _db.DevicesGetAll();
                return _response.getResponse(output, "Devices not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //get
        [HttpGet]
        [Route("deviceSync")]
        public async Task<ActionResult<DevicesModel>> DeviceSync(string imei)
        {
            try
            {
                var output = await _db.DeviceSync(imei);
                return _response.getResponse(output, "Device not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getDeviceBySerialNumber")]
        public async Task<ActionResult<IEnumerable<DevicesModel>>> getDeviceBySerial(string serialNumber, int page = 1, int rowCount = 80)
        {
            try
            {
                var output = await _db.DevicesGetBySerialNumber(serialNumber);
                if (output != null)
                {
                    PaginationData paginationData = new PaginationData
                    {
                        tablename = "Devices",
                        Column = "SerialNumber",
                        Id = serialNumber,
                        rowCount = rowCount
                    };
                    var paginationDataa = await _db.PaginationData(paginationData);
                    DynamicResponse response = new DynamicResponse
                    {
                        error = false,
                        errorMessage = null,
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
                        errorMessage = "Device does not exist"
                    };
                    return Ok(response);
                }
                //return _response.getResponse(output, "Devices not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpGet]
        [Route("getDeviceByLocation")]
        public async Task<ActionResult<DevicesModel>> getDeviceByLocation(string location)
        {
            try
            {
                var output = await _db.DevicesGetByLocation(location);
                return _response.getResponse(output, "Devices not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //getAll
        [HttpGet]
        [Route("DevicesGetBySateliteId")]
        public async Task<ActionResult<IEnumerable<DevicesModel>>> DevicesGetBySateliteId(int id)
        {
            try
            {
                var output = await _db.DevicesGetBySateliteId(id);
                return _response.getResponse(output, "Devices not found");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //update
        [HttpPost]
        [Route("updateDevices")]
        public async Task<ActionResult<DevicesModel>> updateDevices([FromBody] DevicesModel model)
        {
            try
            {
                //get device data
                var data = await _db.DevicesGet(model.Id);
                if (data != null)
                {
                    //if device prefix is null, update
                    if (data.Prefix == null)
                    {
                        //get devices within same location.
                        var deviceList = await _db.DevicesGetByLocation(model.Location);
                        var count = deviceList.Count;
                        if (count > 0)
                        {
                            //if devices returned > 0 then prefix should follow sequence
                            count++;
                            model.Prefix = "A" + count.ToString("00");
                        }
                        else
                        {
                            //default prefix
                            model.Prefix = "A01";
                        }
                    }
                    else
                    {
                        if (data.Location == model.Location)
                        {
                            model.Prefix = data.Prefix;
                        }
                        else
                        {
                            //if current device location does not match passed location, check what the next sequence is in db
                            var deviceList = await _db.DevicesGetByLocation(model.Location);
                            var count = deviceList.Count;
                            if (count > 0)
                            {
                                count++;
                                model.Prefix = "A" + count.ToString("00");
                            }
                            else
                            {
                                model.Prefix = "A01";
                            }
                        }
                    }
                }
                await _db.DevicesUpdate(model);
                return _response.getResponse("Device Updated", "Devices not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        [HttpPost]
        [Route("updateDevicePcrn")]
        public async Task<ActionResult<DevicesModel>> updateDevicePcrn([FromBody] DevicesModel model)
        {
            try
            {
                await _db.DevicesUpdatePrcn(model);
                return _response.getResponse("PRCN Updated", "Devices not updated");
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }

        //delete
        [HttpGet]
        [Route("deleteDevices")]
        public async Task<ActionResult<DevicesModel>> deleteUserAsync(int id)
        {
            try
            {
                //check if exist
                var deviceData = await _db.DevicesGet(id);
                if (deviceData != null)
                {
                    await _db.DevicesDelete(id);
                    return _response.getResponse("Device Deleted", "Devices Cold not be deleted");
                }
                else
                {
                    return _response.errorResponse("Device Not Found");
                }
            }
            catch (Exception e)
            {
                return _response.errorResponse(e.Message);
            }
        }
    }
}
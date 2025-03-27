using CMS_API.Models;
using Infrustructure_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace CMS_API.Services
{
    public class JsonResponse : Controller
    {
        public ResponseModel response;
        public TrimWhitespace _trimSpaces = new TrimWhitespace();

        public dynamic getResponse(dynamic output, string errorMessage)
        {
            if (output != null)
            {
                try
                {
                    _trimSpaces.trim(output);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                response = new ResponseModel
                {
                    error = false,
                    results = output
                };
                return Ok(response);
            }
            else
            {
                response = new ResponseModel
                {
                    error = true,
                    errorMessage = errorMessage
                };
                return Ok(response);
            }
        }

        public dynamic errorResponse(string ErrorMessage)
        {
            response = new ResponseModel
            {
                error = true,
                errorMessage = ErrorMessage
            };
            return Ok(response);
        }
    }
}
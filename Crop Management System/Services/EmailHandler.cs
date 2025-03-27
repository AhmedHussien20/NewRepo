using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Crop_Management_System.Services
{
    public class EmailHandler
    {
        string url = "https://swishandroid.swish.co.zm/api/SendCustomEmail";
        //string url = "https://android.swish.co.zm/api/SendCustomEmail";

        //TEST SERVER - SendEmailController/SendCustomEmail
        public async Task<bool> passwordResetEmailAsync(HttpContext context, IConfiguration _config, String email, string key)
        {
            var host = $"{context.Request.Scheme}://{context.Request.Host}";
            string callback = $"{host}/Views/Account/PasswordReset?accountReset=true&key={key}";
            string mailBody = @"<!doctype html>
<html>
<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title>FRA CMS MAIL CONFIRMATION</title>
    <style>
        /* -------------------------------------
          GLOBAL RESETS
      ------------------------------------- */

        /*All the styling goes here*/

        img {
            border: none;
            -ms-interpolation-mode: bicubic;
            max-width: 100%;
        }

        body {
            background-color: #f6f6f6;
            font-family: sans-serif;
            -webkit-font-smoothing: antialiased;
            font-size: 14px;
            line-height: 1.4;
            margin: 0;
            padding: 0;
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
        }

        table {
            border-collapse: separate;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            width: 100%;
        }

        .madWidth {
            display: inline-block;
            width: 100%;
            text-align: center;
        }

        table td {
            font-family: sans-serif;
            font-size: 14px;
            vertical-align: top;

        }

        /* -------------------------------------
          BODY & CONTAINER
      ------------------------------------- */

        .body {
            background-color: #f6f6f6;
            width: 100%;
        }

        /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */
        .container {
            display: block;
            margin: 0 auto !important;
            /* makes it centered */
            max-width: 580px;
            padding: 10px;
            width: 580px;
        }

        /* This should also be a block element, so that it will fill 100% of the .container */
        .content {
            box-sizing: border-box;
            display: block;
            margin: 0 auto;
            max-width: 580px;
            padding: 10px;
        }

        /* -------------------------------------
          HEADER, FOOTER, MAIN
      ------------------------------------- */
        .main {
            background: #ffffff;
            border-radius: 3px;
            width: 100%;
        }

        .wrapper {
            box-sizing: border-box;
            padding: 20px;
        }

        .content-block {
            padding-bottom: 10px;
            padding-top: 10px;
        }

        .footer {
            clear: both;
            margin-top: 10px;
            text-align: center;
            width: 100%;
        }

        .footer td,
        .footer p,
        .footer span,
        .footer a {
            color: #999999;
            font-size: 12px;
            text-align: center;
        }

        /* -------------------------------------
          TYPOGRAPHY
      ------------------------------------- */
        h1,
        h2,
        h3,
        h4 {
            color: #000000;
            font-family: sans-serif;
            font-weight: 600;
            margin: 0;
        }

        h1 {
            font-size: 35px;
            font-weight: 300;
            text-align: center;
            text-transform: capitalize;
        }

        p,
        ul,
        ol {
            font-family: sans-serif;
            font-size: 14px;
            font-weight: normal;
            margin: 0;
            margin-bottom: 15px;
        }

        p li,
        ul li,
        ol li {
            list-style-position: inside;
            margin-left: 5px;
        }

        a {
            color: #3498db;
            text-decoration: underline;
        }

        /* -------------------------------------
          BUTTONS
      ------------------------------------- */
        .btn {
            box-sizing: border-box;
            width: 100%;
        }

        .btn>tbody>tr>td {
            padding-bottom: 15px;
        }

        .btn table {
            width: auto;
        }

        .btn table td {
            background-color: #ffffff;
            border-radius: 5px;
            text-align: center;
        }

        .btn a {
            background-color: #ffffff;
            border: solid 1px #3498db;
            border-radius: 5px;
            box-sizing: border-box;
            color: #3498db;
            cursor: pointer;
            display: inline-block;
            font-size: 14px;
            font-weight: bold;
            margin: 0;
            padding: 12px 25px;
            text-decoration: none;
            text-transform: capitalize;
        }

        .btn-primary table td {
            background-color: #3498db;
        }

        .btn-primary a {
            background-color: #3498db;
            border-color: #3498db;
            color: #ffffff;
        }

        /* -------------------------------------
          OTHER STYLES THAT MIGHT BE USEFUL
      ------------------------------------- */
        .last {
            margin-bottom: 0;
        }

        .first {
            margin-top: 0;
        }

        .align-center {
            text-align: center;
            width: 100%;
        }

        .align-right {
            text-align: right;
        }

        .align-left {
            text-align: left;
        }

        .clear {
            clear: both;
        }

        .mt0 {
            margin-top: 0;
        }

        .mb0 {
            margin-bottom: 0;
        }

        .preheader {
            color: transparent;
            display: none;
            height: 0;
            max-height: 0;
            max-width: 0;
            opacity: 0;
            overflow: hidden;
            mso-hide: all;
            visibility: hidden;
            width: 0;
        }

        .powered-by a {
            text-decoration: none;
        }

        hr {
            border: 0;
            border-bottom: 1px solid #be25cc;
            margin: 20px 0;
        }

        /* -------------------------------------
          RESPONSIVE AND MOBILE FRIENDLY STYLES
      ------------------------------------- */
        @media only screen and (max-width: 620px) {
            table.body h1 {
                font-size: 28px !important;
                margin-bottom: 10px !important;
            }

            table.body p,
            table.body ul,
            table.body ol,
            table.body td,
            table.body span,
            table.body a {
                font-size: 16px !important;
            }

            table.body .wrapper,
            table.body .article {
                padding: 10px !important;
            }

            table.body .content {
                padding: 0 !important;
            }

            table.body .container {
                padding: 0 !important;
                width: 100% !important;
            }

            table.body .main {
                border-left-width: 0 !important;
                border-radius: 0 !important;
                border-right-width: 0 !important;
            }

            table.body .btn table {
                width: 100% !important;
            }

            table.body .btn a {
                width: 100% !important;
            }

            table.body .img-responsive {
                height: auto !important;
                max-width: 100% !important;
                width: auto !important;
            }
        }

        /* -------------------------------------
          PRESERVE THESE STYLES IN THE HEAD
      ------------------------------------- */
        @media all {
            .ExternalClass {
                width: 100%;
            }

            .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div {
                line-height: 100%;
            }

            .apple-link a {
                color: inherit !important;
                font-family: inherit !important;
                font-size: inherit !important;
                font-weight: inherit !important;
                line-height: inherit !important;
                text-decoration: none !important;
            }

            #MessageViewBody a {
                color: inherit;
                text-decoration: none;
                font-size: inherit;
                font-family: inherit;
                font-weight: inherit;
                line-height: inherit;
            }

            .btn-primary table td:hover {
                background-color: #34495e !important;
            }

            .btn-primary a:hover {
                background-color: #34495e !important;
                border-color: #34495e !important;
            }
        }

        .RoundedTing {
            border: 1px solid rgba(128, 128, 128, 0.322);
            border-radius: 8px;
            padding: 10px;
        }
    </style>
</head>

<body class=''>
    <span class='preheader'>Swish Managment Portal Confirmation</span>
    <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'>
        <tr>
            <td>&nbsp;</td>
            <td class='container'>
                <div class='content'>

                    <!-- START CENTERED WHITE CONTAINER -->
                    <table role='presentation' class='main'>

                        <!-- START MAIN CONTENT AREA -->
                        <tr>
                            <td class='wrapper'>
                                <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                                    <tr>
                                        <td>
                                            <div>
                                                <h2 class='madWidth'>FRA CROP MANAGEMENT CONSOLE</h2>
                                                <br>
                                                <hr>
                                            </div>
                                            <h3>Hi there,</h3>
                                            <p>Please click the link below to reset your password</p>
                                            <div class='btn btn-primary w-100'>
                                                <a class='madWidth' href='" + callback + @"' target='_blank'>Activate Account</a>
                                            </div>
                                            <br> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <!-- END MAIN CONTENT AREA -->
                    </table>
                    <div class='footer'>
                        <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                            <tr>
                                <td class='content-block'>
                                    <span class='apple-link'>© Copyright 2019 NetOne Payment Systems Ltd – All Rights
                                        Reserved </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- END FOOTER -->
                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</body> 
</html>";

            messageBody obj = new messageBody
            {
                email = email,
                message = mailBody,
                subject = "FRA SMS Password Reset"
            };


            var response = await postRequestAsync(obj, url);
            if (response.IsSuccessStatusCode)
            {
                var serverResponse = await response.Content.ReadAsStringAsync();
                var serverResponseBody = JObject.Parse(serverResponse);
                Debug.Write($"\n\nSms Response Body: {serverResponseBody}");
                return true;
            }
            else
            {
                return false;
            }
        }

        //move to portal//
        public async Task<bool> sendRegistrationEmailAsync(HttpContext context, IConfiguration _config, String email, string password, string key)
        {
            var host = $"{context.Request.Scheme}://{context.Request.Host}";
            string callback = $"{host}/Views/Account/Authorisation?key={key}";
            string mailBody = @"<!doctype html>
<html>

<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title>SWISH MAIL CONFIRMATION</title>
    <style>
        /* -------------------------------------
          GLOBAL RESETS
      ------------------------------------- */

        /*All the styling goes here*/

        img {
            border: none;
            -ms-interpolation-mode: bicubic;
            max-width: 100%;
        }

        body {
            background-color: #f6f6f6;
            font-family: sans-serif;
            -webkit-font-smoothing: antialiased;
            font-size: 14px;
            line-height: 1.4;
            margin: 0;
            padding: 0;
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
        }

        table {
            border-collapse: separate;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            width: 100%;
        }

        .madWidth {
            display: inline-block;
            width: 100%;
            text-align: center;
        }

        table td {
            font-family: sans-serif;
            font-size: 14px;
            vertical-align: top;

        }

        /* -------------------------------------
          BODY & CONTAINER
      ------------------------------------- */

        .body {
            background-color: #f6f6f6;
            width: 100%;
        }

        /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */
        .container {
            display: block;
            margin: 0 auto !important;
            /* makes it centered */
            max-width: 580px;
            padding: 10px;
            width: 580px;
        }

        /* This should also be a block element, so that it will fill 100% of the .container */
        .content {
            box-sizing: border-box;
            display: block;
            margin: 0 auto;
            max-width: 580px;
            padding: 10px;
        }

        /* -------------------------------------
          HEADER, FOOTER, MAIN
      ------------------------------------- */
        .main {
            background: #ffffff;
            border-radius: 3px;
            width: 100%;
        }

        .wrapper {
            box-sizing: border-box;
            padding: 20px;
        }

        .content-block {
            padding-bottom: 10px;
            padding-top: 10px;
        }

        .footer {
            clear: both;
            margin-top: 10px;
            text-align: center;
            width: 100%;
        }

        .footer td,
        .footer p,
        .footer span,
        .footer a {
            color: #999999;
            font-size: 12px;
            text-align: center;
        }

        /* -------------------------------------
          TYPOGRAPHY
      ------------------------------------- */
        h1,
        h2,
        h3,
        h4 {
            color: #000000;
            font-family: sans-serif;
            font-weight: 600;
            margin: 0;
        }

        h1 {
            font-size: 35px;
            font-weight: 300;
            text-align: center;
            text-transform: capitalize;
        }

        p,
        ul,
        ol {
            font-family: sans-serif;
            font-size: 14px;
            font-weight: normal;
            margin: 0;
            margin-bottom: 15px;
        }

        p li,
        ul li,
        ol li {
            list-style-position: inside;
            margin-left: 5px;
        }

        a {
            color: #3498db;
            text-decoration: underline;
        }

        /* -------------------------------------
          BUTTONS
      ------------------------------------- */
        .btn {
            box-sizing: border-box;
            width: 100%;
        }

        .btn>tbody>tr>td {
            padding-bottom: 15px;
        }

        .btn table {
            width: auto;
        }

        .btn table td {
            background-color: #ffffff;
            border-radius: 5px;
            text-align: center;
        }

        .btn a {
            background-color: #ffffff;
            border: solid 1px #3498db;
            border-radius: 5px;
            box-sizing: border-box;
            color: #3498db;
            cursor: pointer;
            display: inline-block;
            font-size: 14px;
            font-weight: bold;
            margin: 0;
            padding: 12px 25px;
            text-decoration: none;
            text-transform: capitalize;
        }

        .btn-primary table td {
            background-color: #3498db;
        }

        .btn-primary a {
            background-color: #3498db;
            border-color: #3498db;
            color: #ffffff;
        }

        /* -------------------------------------
          OTHER STYLES THAT MIGHT BE USEFUL
      ------------------------------------- */
        .last {
            margin-bottom: 0;
        }

        .first {
            margin-top: 0;
        }

        .align-center {
            text-align: center;
            width: 100%;
        }

        .align-right {
            text-align: right;
        }

        .align-left {
            text-align: left;
        }

        .clear {
            clear: both;
        }

        .mt0 {
            margin-top: 0;
        }

        .mb0 {
            margin-bottom: 0;
        }

        .preheader {
            color: transparent;
            display: none;
            height: 0;
            max-height: 0;
            max-width: 0;
            opacity: 0;
            overflow: hidden;
            mso-hide: all;
            visibility: hidden;
            width: 0;
        }

        .powered-by a {
            text-decoration: none;
        }

        hr {
            border: 0;
            border-bottom: 1px solid #be25cc;
            margin: 20px 0;
        }

        /* -------------------------------------
          RESPONSIVE AND MOBILE FRIENDLY STYLES
      ------------------------------------- */
        @media only screen and (max-width: 620px) {
            table.body h1 {
                font-size: 28px !important;
                margin-bottom: 10px !important;
            }

            table.body p,
            table.body ul,
            table.body ol,
            table.body td,
            table.body span,
            table.body a {
                font-size: 16px !important;
            }

            table.body .wrapper,
            table.body .article {
                padding: 10px !important;
            }

            table.body .content {
                padding: 0 !important;
            }

            table.body .container {
                padding: 0 !important;
                width: 100% !important;
            }

            table.body .main {
                border-left-width: 0 !important;
                border-radius: 0 !important;
                border-right-width: 0 !important;
            }

            table.body .btn table {
                width: 100% !important;
            }

            table.body .btn a {
                width: 100% !important;
            }

            table.body .img-responsive {
                height: auto !important;
                max-width: 100% !important;
                width: auto !important;
            }
        }

        /* -------------------------------------
          PRESERVE THESE STYLES IN THE HEAD
      ------------------------------------- */
        @media all {
            .ExternalClass {
                width: 100%;
            }

            .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div {
                line-height: 100%;
            }

            .apple-link a {
                color: inherit !important;
                font-family: inherit !important;
                font-size: inherit !important;
                font-weight: inherit !important;
                line-height: inherit !important;
                text-decoration: none !important;
            }

            #MessageViewBody a {
                color: inherit;
                text-decoration: none;
                font-size: inherit;
                font-family: inherit;
                font-weight: inherit;
                line-height: inherit;
            }

            .btn-primary table td:hover {
                background-color: #34495e !important;
            }

            .btn-primary a:hover {
                background-color: #34495e !important;
                border-color: #34495e !important;
            }
        }

        .RoundedTing {
            border: 1px solid rgba(128, 128, 128, 0.322);
            border-radius: 8px;
            padding: 10px;
        }
    </style>
</head>

<body class=''>
    <span class='preheader'>Swish Managment Portal Confirmation</span>
    <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'>
        <tr>
            <td>&nbsp;</td>
            <td class='container'>
                <div class='content'>

                    <!-- START CENTERED WHITE CONTAINER -->
                    <table role='presentation' class='main'>

                        <!-- START MAIN CONTENT AREA -->
                        <tr>
                            <td class='wrapper'>
                                <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                                    <tr>
                                        <td>
                                            <div>
                                                <h2 class='madWidth'>FRA CMS Console</h2>
                                                <br>
                                                <hr>
                                            </div>
                                            <h3>Hi there,</h3>
                                            <p>Your registration was successful, Please click the link below to get
                                                started</p>
                                            <div class='btn btn-primary w-100'>
                                                <a class='madWidth' href='" + callback + @"' target='_blank'>Activate Account</a>
                                            </div>
                                            <br>
                                            <h3>Account Information</h3>
                                            <hr>
                                            <div>
                                                <table class='RoundedTing'>
                                                    <tr>
                                                        <td class='content-block'>
                                                            <p><b>Email:</b> </p>
                                                            <p>" + email + @"</p>
                                                            <p><b>Password:</b></p>
                                                            <p>" + password + @"</p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <!-- END MAIN CONTENT AREA -->
                    </table>
                    <div class='footer'>
                        <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                            <tr>
                                <td class='content-block'>
                                    <span class='apple-link'>© Copyright 2019 NetOne Payment Systems Ltd – All Rights
                                        Reserved </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- END FOOTER -->

                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</body>
</html>";

            messageBody obj = new messageBody
            {
                email = email,
                message = mailBody,
                subject = "FRA CMS Registration"
            };

            var response = await postRequestAsync(obj, url);
            if (response.IsSuccessStatusCode)
            {
                var serverResponse = await response.Content.ReadAsStringAsync();
                var serverResponseBody = JObject.Parse(serverResponse);
                Debug.Write($"\n\nSms Response Body: {serverResponseBody}");
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> sendSubscriptionEmailAsync(HttpContext context, IConfiguration _config, String email)
        {
            var host = $"{context.Request.Scheme}://{context.Request.Host}";
            //string callback = $"{host}/Views/Account/Authorisation?key={key}";
            string mailBody = @"<!doctype html>
<html>

<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title>SWISH MAIL CONFIRMATION</title>
    <style>
        /* -------------------------------------
          GLOBAL RESETS
      ------------------------------------- */

        /*All the styling goes here*/

        img {
            border: none;
            -ms-interpolation-mode: bicubic;
            max-width: 100%;
        }

        body {
            background-color: #f6f6f6;
            font-family: sans-serif;
            -webkit-font-smoothing: antialiased;
            font-size: 14px;
            line-height: 1.4;
            margin: 0;
            padding: 0;
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
        }

        table {
            border-collapse: separate;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            width: 100%;
        }

        .madWidth {
            display: inline-block;
            width: 100%;
            text-align: center;
        }

        table td {
            font-family: sans-serif;
            font-size: 14px;
            vertical-align: top;

        }

        /* -------------------------------------
          BODY & CONTAINER
      ------------------------------------- */

        .body {
            background-color: #f6f6f6;
            width: 100%;
        }

        /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */
        .container {
            display: block;
            margin: 0 auto !important;
            /* makes it centered */
            max-width: 580px;
            padding: 10px;
            width: 580px;
        }

        /* This should also be a block element, so that it will fill 100% of the .container */
        .content {
            box-sizing: border-box;
            display: block;
            margin: 0 auto;
            max-width: 580px;
            padding: 10px;
        }

        /* -------------------------------------
          HEADER, FOOTER, MAIN
      ------------------------------------- */
        .main {
            background: #ffffff;
            border-radius: 3px;
            width: 100%;
        }

        .wrapper {
            box-sizing: border-box;
            padding: 20px;
        }

        .content-block {
            padding-bottom: 10px;
            padding-top: 10px;
        }

        .footer {
            clear: both;
            margin-top: 10px;
            text-align: center;
            width: 100%;
        }

        .footer td,
        .footer p,
        .footer span,
        .footer a {
            color: #999999;
            font-size: 12px;
            text-align: center;
        }

        /* -------------------------------------
          TYPOGRAPHY
      ------------------------------------- */
        h1,
        h2,
        h3,
        h4 {
            color: #000000;
            font-family: sans-serif;
            font-weight: 600;
            margin: 0;
        }

        h1 {
            font-size: 35px;
            font-weight: 300;
            text-align: center;
            text-transform: capitalize;
        }

        p,
        ul,
        ol {
            font-family: sans-serif;
            font-size: 14px;
            font-weight: normal;
            margin: 0;
            margin-bottom: 15px;
        }

        p li,
        ul li,
        ol li {
            list-style-position: inside;
            margin-left: 5px;
        }

        a {
            color: #3498db;
            text-decoration: underline;
        }

        /* -------------------------------------
          BUTTONS
      ------------------------------------- */
        .btn {
            box-sizing: border-box;
            width: 100%;
        }

        .btn>tbody>tr>td {
            padding-bottom: 15px;
        }

        .btn table {
            width: auto;
        }

        .btn table td {
            background-color: #ffffff;
            border-radius: 5px;
            text-align: center;
        }

        .btn a {
            background-color: #ffffff;
            border: solid 1px #3498db;
            border-radius: 5px;
            box-sizing: border-box;
            color: #3498db;
            cursor: pointer;
            display: inline-block;
            font-size: 14px;
            font-weight: bold;
            margin: 0;
            padding: 12px 25px;
            text-decoration: none;
            text-transform: capitalize;
        }

        .btn-primary table td {
            background-color: #3498db;
        }

        .btn-primary a {
            background-color: #3498db;
            border-color: #3498db;
            color: #ffffff;
        }

        /* -------------------------------------
          OTHER STYLES THAT MIGHT BE USEFUL
      ------------------------------------- */
        .last {
            margin-bottom: 0;
        }

        .first {
            margin-top: 0;
        }

        .align-center {
            text-align: center;
            width: 100%;
        }

        .align-right {
            text-align: right;
        }

        .align-left {
            text-align: left;
        }

        .clear {
            clear: both;
        }

        .mt0 {
            margin-top: 0;
        }

        .mb0 {
            margin-bottom: 0;
        }

        .preheader {
            color: transparent;
            display: none;
            height: 0;
            max-height: 0;
            max-width: 0;
            opacity: 0;
            overflow: hidden;
            mso-hide: all;
            visibility: hidden;
            width: 0;
        }

        .powered-by a {
            text-decoration: none;
        }

        hr {
            border: 0;
            border-bottom: 1px solid #be25cc;
            margin: 20px 0;
        }

        /* -------------------------------------
          RESPONSIVE AND MOBILE FRIENDLY STYLES
      ------------------------------------- */
        @media only screen and (max-width: 620px) {
            table.body h1 {
                font-size: 28px !important;
                margin-bottom: 10px !important;
            }

            table.body p,
            table.body ul,
            table.body ol,
            table.body td,
            table.body span,
            table.body a {
                font-size: 16px !important;
            }

            table.body .wrapper,
            table.body .article {
                padding: 10px !important;
            }

            table.body .content {
                padding: 0 !important;
            }

            table.body .container {
                padding: 0 !important;
                width: 100% !important;
            }

            table.body .main {
                border-left-width: 0 !important;
                border-radius: 0 !important;
                border-right-width: 0 !important;
            }

            table.body .btn table {
                width: 100% !important;
            }

            table.body .btn a {
                width: 100% !important;
            }

            table.body .img-responsive {
                height: auto !important;
                max-width: 100% !important;
                width: auto !important;
            }
        }

        /* -------------------------------------
          PRESERVE THESE STYLES IN THE HEAD
      ------------------------------------- */
        @media all {
            .ExternalClass {
                width: 100%;
            }

            .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div {
                line-height: 100%;
            }

            .apple-link a {
                color: inherit !important;
                font-family: inherit !important;
                font-size: inherit !important;
                font-weight: inherit !important;
                line-height: inherit !important;
                text-decoration: none !important;
            }

            #MessageViewBody a {
                color: inherit;
                text-decoration: none;
                font-size: inherit;
                font-family: inherit;
                font-weight: inherit;
                line-height: inherit;
            }

            .btn-primary table td:hover {
                background-color: #34495e !important;
            }

            .btn-primary a:hover {
                background-color: #34495e !important;
                border-color: #34495e !important;
            }
        }

        .RoundedTing {
            border: 1px solid rgba(128, 128, 128, 0.322);
            border-radius: 8px;
            padding: 10px;
        }
    </style>
</head>

<body class=''>
    <span class='preheader'>Swish Managment Portal Confirmation</span>
    <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'>
        <tr>
            <td>&nbsp;</td>
            <td class='container'>
                <div class='content'>

                    <!-- START CENTERED WHITE CONTAINER -->
                    <table role='presentation' class='main'>

                        <!-- START MAIN CONTENT AREA -->
                        <tr>
                            <td class='wrapper'>
                                <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                                    <tr>
                                        <td>
                                            <div>
                                                <h2 class='madWidth'>FRA-POS-572 Subscription Expiration</h2>
                                                <br>
                                                <hr>
                                            </div>
                                            <h3>Dear [Recipient's Name],</h3>
<br>
                                            <p>We would like to express our sincere appreciation for your utilization of our CMS system.</p>
<p>However, we bring to your attention an important matter regarding your FRA-POS-572 License. It has come to our notice that your license is set to expire this upcoming Friday 18th August 2023 at midnight.</p>
<p>To ensure uninterrupted access to our services and to maintain the continuity of your operations, we kindly request your prompt attention to this matter. We urge you to take the necessary steps to extend your license before the expiration date.</p> 
<p>Should you require any assistance or have questions regarding the renewal process, please do not hesitate to reach out to your relationship manager, Arun James at arunj@netone.co.zm or +260969239252.</p>
<p>Your continued satisfaction and success are of paramount importance to us, and we are committed to assisting you throughout this process.</p>
<p>Thank you for your attention to this matter. We look forward to your prompt action and the opportunity to continue serving your needs.</p>
<p>Best Regards</p>
                                            <br>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <!-- END MAIN CONTENT AREA -->
                    </table>
                    <div class='footer'>
                        <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                            <tr>
                                <td class='content-block'>
                                    <span class='apple-link'>© Copyright 2019 NetOne Payment Systems Ltd – All Rights
                                        Reserved </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- END FOOTER -->

                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</body>
</html>";

            messageBody obj = new messageBody
            {
                email = email,
                message = mailBody,
                subject = "FRA-POS-572 LICENSE EXPIRATION"
            };

            var response = await postRequestAsync(obj, url);
            if (response.IsSuccessStatusCode)
            {
                var serverResponse = await response.Content.ReadAsStringAsync();
                var serverResponseBody = JObject.Parse(serverResponse);
                Debug.Write($"\n\nSms Response Body: {serverResponseBody}");
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<HttpResponseMessage> postRequestAsync(dynamic obj, string url)
        {
            try
            {
                string jsonBody = JsonConvert.SerializeObject(obj);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage results = await client.PostAsync(url, content);
                return results;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"postRequestError: {e.Message}");
                return null;
            }
        }

        public class messageBody
        {
            public string email { get; set; }
            public string subject { get; set; }
            public string message { get; set; }
        }
    }
}

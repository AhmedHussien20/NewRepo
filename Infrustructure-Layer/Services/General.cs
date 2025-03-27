using Crop_Management_System.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure_Layer.Services
{
    public class General
    {
        public static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        EmailHandlerApi email = new EmailHandlerApi();

        public async Task UpdateErrorLogAsync(Exception ex, string method)
        {
            string strErrorMessage = ex.Message;
            try
            {
                if (strErrorMessage.ToUpper() != "THREAD WAS BEING ABORTED.")
                {
                    string strErrorLogFilePath = "";
                    TimeZoneInfo INDIAN_ZONE1 = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE1);
                    StringBuilder strErrorLog = new StringBuilder();

                    strErrorLog.Append("Date : ");
                    strErrorLog.AppendLine(indianTime.ToString());

                    strErrorLog.Append("Method : ");
                    strErrorLog.AppendLine(method);
                    strErrorLog.Append("Exception : ");
                    strErrorLog.Append(ex.ToString());
                    strErrorLogFilePath = "C:\\Logs\\Exception";
                    //strErrorLogFilePath = HttpContext.Current.Server.MapPath("~") + "\\FileUploads\\Exception";
                    WriteErrorLog(strErrorLog, strErrorLogFilePath);

                    try
                    {
                        await email.emailLogger("muyangwam@netone.co.zm",
                            strErrorLog.ToString(), method);
                    }
                    catch { }
                }
            }
            catch
            {
                return;
            }

        }

        public async Task UpdateErrorLogStringAsync(string message, string method)
        {
            string strErrorMessage = message;
            try
            {
                if (strErrorMessage.ToUpper() != "THREAD WAS BEING ABORTED.")
                {
                    string strErrorLogFilePath = "";
                    TimeZoneInfo INDIAN_ZONE1 = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE1);
                    StringBuilder strErrorLog = new StringBuilder();

                    strErrorLog.Append("Date : ");
                    strErrorLog.AppendLine(indianTime.ToString());

                    strErrorLog.Append("Method : ");
                    strErrorLog.AppendLine(method);
                    strErrorLog.Append("Exception : ");
                    strErrorLog.Append(message);
                    strErrorLogFilePath = "C:\\Logs\\Exception";
                    //strErrorLogFilePath = HttpContext.Current.Server.MapPath("~") + "\\FileUploads\\Exception";
                    WriteErrorLog(strErrorLog, strErrorLogFilePath);

                    try
                    {
                        await email.emailLogger("muyangwam@netone.co.zm",
                            strErrorLog.ToString(), method);
                    }
                    catch { }
                }
            }
            catch
            {
                return;
            }

        }

        public static void WriteErrorLog(StringBuilder strErrorLog, string strErrorLogFilePath)
        {
            try
            {
                FileInfo file = new FileInfo(strErrorLogFilePath + "\\Exceptions.txt");

                DirectoryInfo dir = new DirectoryInfo(strErrorLogFilePath);

                if (dir.Exists == false)
                {
                    Directory.CreateDirectory(dir.FullName);
                }

                if (!File.Exists(file.FullName))
                {
                    FileStream fs = File.Create(file.FullName);
                    fs.Dispose();
                }
                strErrorLog.Append(Environment.NewLine);
                strErrorLog.Append("-------------------------------------------------------------------");
                strErrorLog.Append(Environment.NewLine);
                File.AppendAllText(file.FullName, strErrorLog.ToString());
            }
            catch { }
        }

        public static void WriteLog(StringBuilder strErrorLog)
        {
            try
            {
                string strLogFilePath = "C:\\Logs\\Exception";
                FileInfo file = new FileInfo(strLogFilePath + "\\Logs.txt");

                DirectoryInfo dir = new DirectoryInfo(strLogFilePath);

                if (dir.Exists == false)
                {
                    Directory.CreateDirectory(dir.FullName);
                }

                if (!File.Exists(file.FullName))
                {
                    FileStream fs = File.Create(file.FullName);
                    fs.Dispose();
                }
                strErrorLog.Append(Environment.NewLine);
                strErrorLog.Append("-------------------------------------------------------------------");
                strErrorLog.Append(Environment.NewLine);
                File.AppendAllText(file.FullName, strErrorLog.ToString());
            }
            catch { }
        }


        public void HandleException(Exception ex, string title, string companyKey, string cashierName)
        {
            UpdateErrorLogAsync(ex, $"{title} : {companyKey} : {cashierName}");
        }

    }
}

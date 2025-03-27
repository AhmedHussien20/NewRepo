using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Infrustructure_Layer.Services;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Domain_Layer.Models.StatsModel;

namespace Infrustructure_Layer.DataAccess
{

    public class DataService : IDataService
    {
        private readonly IDataAccess _db;
        private const string connectionStringName = "SqlDb";
        private const string connectionStringNameSrpos = "SqlDbSrpos";
        public StoredProcedures _sp = new StoredProcedures();
        public Encryption _encryption = new Encryption();


        public DataService(IDataAccess db)
        {
            _db = db;
        }

        public async Task<int> RolesCreate(RolesModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.RolesCreate,
                                             new
                                             {
                                                 Title = model.Title
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task RolesUpdate(RolesModel model)
        {
            var data = await _db.SaveDataAsync(_sp.RolesUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Title = model.Title
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task RolesDelete(int Id)
        {
            await deleteValue(_sp.RolesDelete, Id);
        }

        public async Task<List<PermissionsModel>> RolesPermissionsGet(int Id)
        {
            List<PermissionsModel> output = new List<PermissionsModel>();
            output = await _db.LoadDataAsync<PermissionsModel, dynamic>(_sp.RolesPermissionsGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<PermissionsModel>> RolesPermissionsGetAll()
        {
            List<PermissionsModel> output = new List<PermissionsModel>();
            output = await _db.LoadDataAsync<PermissionsModel, dynamic>(_sp.RolesPermissionsGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<RolesModel> RolesGet(int Id)
        {
            List<RolesModel> output = new List<RolesModel>();
            output = await _db.LoadDataAsync<RolesModel, dynamic>(_sp.RolesGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<RolesModel>> RolesGetAll()
        {
            List<RolesModel> output = new List<RolesModel>();
            output = await _db.LoadDataAsync<RolesModel, dynamic>(_sp.RolesGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<int> DevicesCreate(DevicesModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.DevicesCreate,
                                             new
                                             {
                                                 SerialNumber = model.SerialNumber,
                                                 LastPrcnNumber = model.LastPrcnNumber,
                                                 LastGrnNumber = model.LastGrnNumber,
                                                 LastGinNumber = model.LastGinNumber,
                                                 LastIdtNumber = model.LastIdtNumber,
                                                 Location = model.Location,
                                                 Title = model.Title,
                                                 DeviceId = model.DeviceId,
                                                 DeviceBearerId = model.DeviceBearerId,
                                                 DeviceStatusId = model.DeviceStatusId,
                                                 SateliteId = model.SateliteId,
                                                 DeviceSupervisorId = model.DeviceSupervisorId,
                                                 Username = model.Username,
                                                 SyncPeriod = model.SyncPeriod
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task<int> DevicesUpdate(DevicesModel model)
        {
            var output = 0;
            output = await _db.SaveDataAsync(_sp.DevicesUpdate,
                                             new
                                             {
                                                 SerialNumber = model.SerialNumber,
                                                 Id = model.Id,
                                                 Prefix = model.Prefix,
                                                 LastPrcnNumber = model.LastPrcnNumber,
                                                 LastGrnNumber = model.LastGrnNumber,
                                                 LastGinNumber = model.LastGinNumber,
                                                 LastIdtNumber = model.LastIdtNumber,
                                                 Location = model.Location,
                                                 Title = model.Title,
                                                 DeviceId = model.DeviceId,
                                                 DeviceBearerId = model.DeviceBearerId,
                                                 DeviceStatusId = model.DeviceStatusId,
                                                 SateliteId = model.SateliteId,
                                                 DeviceSupervisorId = model.DeviceSupervisorId,
                                                 Username = model.Username,
                                                 SyncPeriod = model.SyncPeriod
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task DevicesUpdatePrcn(DevicesModel model)
        {
            await _db.SaveDataAsync(_sp.DevicesUpdatePrcn,
                                             new
                                             {
                                                 Id = model.Id,
                                                 LastPrcnNumber = model.LastPrcnNumber
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task DevicesDelete(int Id)
        {
            await deleteValue(_sp.DevicesDelete, Id);
        }

        public async Task DevicesUpdateLastsync(int Id, DateTime lastSync)
        {
            await _db.SaveDataAsync(_sp.DevicesUpdateLastsync,
                                            new
                                            {
                                                Id = Id,
                                                lastSync = lastSync
                                            },
                                            connectionStringName,
                                            true);
        }

        public async Task<DevicesModel> DevicesGet(int Id)
        {
            List<DevicesModel> output = new List<DevicesModel>();
            output = await _db.LoadDataAsync<DevicesModel, dynamic>(_sp.DevicesGet,
                                                 new
                                                 {
                                                     Id = Id
                                                 },
                                                 connectionStringName,
                                                 true);

            foreach (var item in output)
            {
                var status = await DeviceStatusGet(item.DeviceStatusId);
                var location = await NetLocationGet(item.Location);
                var bearer = await UsersGet(item.DeviceBearerId);
                var supervisor = await UsersGet(item.DeviceSupervisorId);

                if (status != null)
                    item.DeviceStatusData = status;
                if (location != null)
                    item.netPurLocationModel = location;
                if (bearer != null)
                    item.DeviceBearerData = bearer;
                if (supervisor != null)
                    item.DeviceSuperevisorData = supervisor;
            }

            return output.FirstOrDefault();
        }

        public async Task<List<DevicesModel>> DevicesGetByLocation(string location)
        {
            List<DevicesModel> output = new List<DevicesModel>();
            output = await _db.LoadDataAsync<DevicesModel, dynamic>(_sp.DevicesGetByLocation,
                                                 new
                                                 { location = location },
                                                 connectionStringName,
                                                 true);

            foreach (var item in output)
            {
                var status = await DeviceStatusGet(item.DeviceStatusId);
                var locations = await NetLocationGet(item.Location);
                var bearer = await UsersGet(item.DeviceBearerId);
                var supervisor = await UsersGet(item.DeviceSupervisorId);

                if (status != null)
                    item.DeviceStatusData = status;
                if (locations != null)
                    item.netPurLocationModel = locations;
                if (bearer != null)
                    item.DeviceBearerData = bearer;
                if (supervisor != null)
                    item.DeviceSuperevisorData = supervisor;
            }

            return output;
        }

        public async Task<DevicesModel> DevicesGetByDoc(string location, string prefix)
        {
            List<DevicesModel> output = new List<DevicesModel>();
            output = await _db.LoadDataAsync<DevicesModel, dynamic>(_sp.DevicesGetByDoc,
                                                 new
                                                 {
                                                     location = location,
                                                     prefix = prefix
                                                 },
                                                 connectionStringName,
                                                 true);

            foreach (var item in output)
            {
                var status = await DeviceStatusGet(item.DeviceStatusId);
                var locations = await NetLocationGet(item.Location);
                var bearer = await UsersGet(item.DeviceBearerId);
                var supervisor = await UsersGet(item.DeviceSupervisorId);

                if (status != null)
                    item.DeviceStatusData = status;
                if (locations != null)
                    item.netPurLocationModel = locations;
                if (bearer != null)
                    item.DeviceBearerData = bearer;
                if (supervisor != null)
                    item.DeviceSuperevisorData = supervisor;
            }

            return output.FirstOrDefault();
        }

        public async Task<DevicesModel> DeviceSync(string SerialNumber)
        {
            List<DevicesModel> output = new List<DevicesModel>();
            output = await _db.LoadDataAsync<DevicesModel, dynamic>(_sp.DeviceSync,
                                                 new
                                                 {
                                                     SerialNumber = SerialNumber
                                                 },
                                                 connectionStringName,
                                                 true);

            foreach (var item in output)
            {
                var status = await DeviceStatusGet(item.DeviceStatusId);
                var location = await NetLocationGet(item.Location);
                var bearer = await UsersGet(item.DeviceBearerId);
                var supervisor = await UsersGet(item.DeviceSupervisorId);

                if (status != null)
                    item.DeviceStatusData = status;
                if (location != null)
                    item.netPurLocationModel = location;
                if (bearer != null)
                    item.DeviceBearerData = bearer;
                if (supervisor != null)
                    item.DeviceSuperevisorData = supervisor;
            }

            return output.FirstOrDefault();
        }

        public async Task<List<DevicesModel>> DevicesGetAll()
        {
            List<DevicesModel> output = new List<DevicesModel>();
            output = await _db.LoadDataAsync<DevicesModel, dynamic>(_sp.DevicesGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);

            foreach (var item in output)
            {
                var status = await DeviceStatusGet(item.DeviceStatusId);
                var location = await NetLocationGet(item.Location);
                var bearer = await UsersGet(item.DeviceBearerId);
                var supervisor = await UsersGet(item.DeviceSupervisorId);

                if (status != null)
                    item.DeviceStatusData = status;
                if (location != null)
                    item.netPurLocationModel = location;
                if (bearer != null)
                    item.DeviceBearerData = bearer;
                if (supervisor != null)
                    item.DeviceSuperevisorData = supervisor;
            }

            return output;
        }

        public async Task<int> DevicesGetCount()
        {
            List<int> output = new List<int>();
            output = await _db.LoadSingleDataAsync<int>(_sp.DevicesGetCount,
                                                 connectionStringName,
                                                 true);

            return output.First();
        }

        public async Task<List<DevicesModel>> DevicesGetBySateliteId(int Id)
        {
            List<DevicesModel> output = new List<DevicesModel>();
            output = await _db.LoadDataAsync<DevicesModel, dynamic>(_sp.DevicesGetBySateliteId,
                                                 new
                                                 { id = Id },
                                                 connectionStringName,
                                                 true);

            foreach (var item in output)
            {
                var status = await DeviceStatusGet(item.DeviceStatusId);
                var location = await NetLocationGet(item.Location);
                var bearer = await UsersGet(item.DeviceBearerId);
                var supervisor = await UsersGet(item.DeviceSupervisorId);

                if (status != null)
                    item.DeviceStatusData = status;
                if (location != null)
                    item.netPurLocationModel = location;
                if (bearer != null)
                    item.DeviceBearerData = bearer;
                if (supervisor != null)
                    item.DeviceSuperevisorData = supervisor;
            }

            return output;
        }

        public async Task<DevicesModel> DevicesGetBySerialNumber(string SerialNumber)
        {
            List<DevicesModel> output = new List<DevicesModel>();
            output = await _db.LoadDataAsync<DevicesModel, dynamic>(_sp.DevicesGetBySerialNumber,
                                                 new
                                                 { SerialNumber = SerialNumber },
                                                 connectionStringName,
                                                 true);

            foreach (var item in output)
            {
                var status = await DeviceStatusGet(item.DeviceStatusId);
                var location = await NetLocationGet(item.Location);
                var bearer = await UsersGet(item.DeviceBearerId);
                var supervisor = await UsersGet(item.DeviceSupervisorId);

                if (status != null)
                    item.DeviceStatusData = status;
                if (location != null)
                    item.netPurLocationModel = location;
                if (bearer != null)
                    item.DeviceBearerData = bearer;
                if (supervisor != null)
                {
                    supervisor.Password = _encryption.Decrypt(supervisor.Password);
                    item.DeviceSuperevisorData = supervisor;
                }
            }

            return output.FirstOrDefault();
        }

        public async Task<DevicesModel> DevicesGetBySerialNumberCount(string SerialNumber)
        {
            List<DevicesModel> output = new List<DevicesModel>();
            output = await _db.LoadDataAsync<DevicesModel, dynamic>(_sp.DevicesGetBySerialNumber,
                                                 new
                                                 { SerialNumber = SerialNumber },
                                                 connectionStringName,
                                                 true);

            return output.FirstOrDefault();
        }

        public async Task<DeviceStatusModel> DevicesGetStatus(int DeviceId)
        {
            List<DeviceStatusModel> output = new List<DeviceStatusModel>();
            output = await _db.LoadDataAsync<DeviceStatusModel, dynamic>(_sp.DevicesGetStatus,
                                                 new
                                                 {
                                                     DeviceId = DeviceId
                                                 },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<int> DeviceStatusCreate(DeviceStatusModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.DeviceStatusCreate,
                                             new
                                             {
                                                 Title = model.Title
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task DeviceStatusUpdate(DeviceStatusModel model)
        {
            await _db.SaveDataAsync(_sp.DeviceStatusUpdate,
                                              new
                                              {
                                                  Title = model.Title,
                                                  Id = model.Id
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task DeviceStatusDelete(int Id)
        {
            await deleteValue(_sp.DeviceStatusDelete, Id);
        }

        public async Task<DeviceStatusModel> DeviceStatusGet(int Id)
        {
            List<DeviceStatusModel> output = new List<DeviceStatusModel>();
            output = await _db.LoadDataAsync<DeviceStatusModel, dynamic>(_sp.DeviceStatusGet,
                                                 new
                                                 {
                                                     Id = Id
                                                 },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<DeviceStatusModel>> DeviceStatusGetAll()
        {
            List<DeviceStatusModel> output = new List<DeviceStatusModel>();
            output = await _db.LoadDataAsync<DeviceStatusModel, dynamic>(_sp.DeviceStatusGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<int> AccountStatusCreate(AccountStatusModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.AccountStatusCreate,
                                             new
                                             {
                                                 Title = model.Title
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task AccountStatusUpdate(AccountStatusModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.AccountStatusUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Title = model.Title
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task AccountStatusDelete(int Id)
        {
            await deleteValue(_sp.AccountStatusDelete, Id);
        }

        public async Task<AccountStatusModel> AccountStatusGet(int Id)
        {
            List<AccountStatusModel> output = new List<AccountStatusModel>();
            output = await _db.LoadDataAsync<AccountStatusModel, dynamic>(_sp.AccountStatusGet,
                                                 new
                                                 {
                                                     Id = Id
                                                 },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<AccountStatusModel>> AccountStatusGetAll()
        {
            List<AccountStatusModel> output = new List<AccountStatusModel>();
            output = await _db.LoadDataAsync<AccountStatusModel, dynamic>(_sp.AccountStatusGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<int> UsersCreate(UsersModel model)
        {
            int output = 0;
            String SecurityStamp = Guid.NewGuid().ToString();
            output = await _db.SaveDataAsync(_sp.UsersCreate,
                                             new
                                             {
                                                 SecurityStamp = SecurityStamp,
                                                 FirstName = model.FirstName,
                                                 PhoneNumber = model.PhoneNumber,
                                                 Gender = model.Gender,
                                                 DOB = model.DOB,
                                                 LastName = model.LastName,
                                                 Email = model.Email,
                                                 Password = _encryption.encrypt(model.Password),
                                                 RoleId = model.RoleId,
                                                 AccountStatusId = model.AccountStatusId,
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }
        //55
        //3
        //1

        public async Task UsersUpdate(UsersModel model)
        {
            await _db.SaveDataAsync(_sp.UsersUpdate,
                                              new
                                              {
                                                  Id = model.Id,
                                                  PhoneNumber = model.PhoneNumber,
                                                  Password = model.Password != null ? _encryption.encrypt(model.Password) : null,
                                                  Gender = model.Gender,
                                                  DOB = model.DOB,
                                                  FirstName = model.FirstName,
                                                  LastName = model.LastName,
                                                  Email = model.Email,
                                                  RoleId = model.RoleId,
                                                  AccountStatusId = model.AccountStatusId,
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task UsersDelete(int Id)
        {
            await deleteValue(_sp.UsersDelete, Id);
        }

        public async Task<int> UsersResetPassword(ResetPasswordModel model, string key = null)
        {
            var output = 0;
            output = await _db.SaveDataAsync(_sp.UsersResetPassword,
                                                new
                                                {
                                                    key = key,
                                                    userId = model.userId,
                                                    newPassword = _encryption.encrypt(model.newPassword),
                                                    defaultPasswoord = _encryption.encrypt(model.defaultPasswoord),
                                                },
                                                connectionStringName,
                                                true);
            return output;
        }

        public async Task<bool> ActivateAccount(string SecurityStamp)
        {

            string Query = @"Select * from Users where SecurityStamp = @SecurityStamp";
            List<UsersModel> output = new List<UsersModel>();
            output = await _db.LoadDataAsync<UsersModel, dynamic>(Query,
                                                 new
                                                 { SecurityStamp = SecurityStamp },
                                                 connectionStringName,
                                                 false);

            if (output.Count() > 0)
            {
                //Update status
                String id = Guid.NewGuid().ToString();
                string Query_Activate = @"Update Users
                Set isDefaultPassword = 1 , EmailAuthorised = 1,SecurityStamp=@netStamp
                where SecurityStamp = @SecurityStamp;";

                await _db.SaveDataAsync(Query_Activate,
                                              new
                                              {
                                                  SecurityStamp = SecurityStamp,
                                                  netStamp = Guid.NewGuid().ToString()
                                              },
                                              connectionStringName,
                                              false);
                return true;
            }
            else
                return false;
        }

        public async Task<UsersModel> UsersDeviceLogin(UsersModel model)
        {
            List<UsersModel> output = new List<UsersModel>();
            output = await _db.LoadDataAsync<UsersModel, dynamic>(_sp.UsersDeviceLogin,
                                                 new
                                                 {
                                                     Email = model.Email,
                                                     Password = _encryption.encrypt(model.Password)
                                                 },
                                                 connectionStringName,
                                                 true);

            foreach (var data in output)
            {
                var status = await AccountStatusGet(data.AccountStatusId);
                var role = await RolesGet(data.RoleId);

                if (status != null)
                    data.AccountStatus = status;
                if (role != null)
                    data.RolesModel = role;
            }

            return output.FirstOrDefault();
        }

        public async Task<UsersModel> UsersDeviceRegistrationLogin(UsersModel model)
        {
            List<UsersModel> output = new List<UsersModel>();
            output = await _db.LoadDataAsync<UsersModel, dynamic>(_sp.UsersDeviceRegistrationLogin,
                                                 new
                                                 {
                                                     Email = model.Email,
                                                     Password = _encryption.encrypt(model.Password)
                                                 },
                                                 connectionStringName,
                                                 true);

            foreach (var data in output)
            {
                var status = await AccountStatusGet(data.AccountStatusId);
                var role = await RolesGet(data.RoleId);

                if (status != null)
                    data.AccountStatus = status;
                if (role != null)
                    data.RolesModel = role;
            }

            return output.FirstOrDefault();
        }

        public async Task<UsersModel> UsersPortalLogin(UsersModel model)
        {
            List<UsersModel> output = new List<UsersModel>();
            output = await _db.LoadDataAsync<UsersModel, dynamic>(_sp.UsersPortalLogin,
                                                 new
                                                 {
                                                     Email = model.Email,
                                                     Password = _encryption.encrypt(model.Password)
                                                 },
                                                 connectionStringName,
                                                 true);

            foreach (var data in output)
            {
                var staff = await AdminsGetByAdminId(data.Id);
                var status = await AccountStatusGet(data.AccountStatusId);
                var role = await RolesGet(data.RoleId);

                if (status != null)
                    data.AccountStatus = status;
                if (role != null)
                    data.RolesModel = role;
                if (staff != null)
                {
                    data.Province = staff.Province;
                    data.District = staff.District;
                }

            }

            return output.FirstOrDefault();
        }

        public async Task<UsersModel> UsersGet(int Id)
        {
            List<UsersModel> output = new List<UsersModel>();
            output = await _db.LoadDataAsync<UsersModel, dynamic>(_sp.UsersGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);

            foreach (var data in output)
            {
                var status = await AccountStatusGet(data.AccountStatusId);
                var role = await RolesGet(data.RoleId);

                if (status != null)
                    data.AccountStatus = status;
                if (role != null)
                    data.RolesModel = role;
            }

            return output.FirstOrDefault();
        }

        public async Task<UsersModel> UsersGetByEmail(string email)
        {
            List<UsersModel> output = new List<UsersModel>();
            output = await _db.LoadDataAsync<UsersModel, dynamic>(_sp.UsersGetByEmail,
                                                 new
                                                 { email = email },
                                                 connectionStringName,
                                                 true);

            foreach (var data in output)
            {
                var status = await AccountStatusGet(data.AccountStatusId);
                var role = await RolesGet(data.RoleId);

                if (status != null)
                    data.AccountStatus = status;
                if (role != null)
                    data.RolesModel = role;
            }

            return output.FirstOrDefault();
        }

        public async Task<List<UsersModel>> UsersGetAll()
        {
            List<UsersModel> output = new List<UsersModel>();
            output = await _db.LoadDataAsync<UsersModel, dynamic>(_sp.UsersGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);

            foreach (var data in output)
            {
                //try
                //{
                //    data.Password = _encryption.Decrypt(data.Password);
                //}
                //catch (Exception)
                //{ 
                //}

                var status = await AccountStatusGet(data.AccountStatusId);
                var role = await RolesGet(data.RoleId);

                if (status != null)
                    data.AccountStatus = status;
                if (role != null)
                    data.RolesModel = role;
            }
            return output;
        }

        public async Task<int> CropCategoryCreate(CropCategoryModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.CropCategoryCreate,
                                             new
                                             {
                                                 Title = model.Title
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task CropCategoryUpdate(CropCategoryModel model)
        {
            await _db.SaveDataAsync(_sp.CropCategoryUpdate,
                                            new
                                            {
                                                Id = model.Id,
                                                Title = model.Title
                                            },
                                            connectionStringName,
                                            true);
        }

        public async Task CropCategoryDelete(int Id)
        {
            await deleteValue(_sp.CropCategoryDelete, Id);
        }

        public async Task<CropCategoryModel> CropCategoryGet(int Id)
        {
            List<CropCategoryModel> output = new List<CropCategoryModel>();
            output = await _db.LoadDataAsync<CropCategoryModel, dynamic>(_sp.CropCategoryGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<CropCategoryModel>> CropCategoryGetAll()
        {
            List<CropCategoryModel> output = new List<CropCategoryModel>();
            output = await _db.LoadDataAsync<CropCategoryModel, dynamic>(_sp.CropCategoryGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);

            return output;
        }

        public async Task<int> CropsCreate(CropsModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.CropsCreate,
                                             new
                                             {
                                                 FMTITEMNO = model.FMTITEMNO,
                                                 StockUnit = model.StockUnit,
                                                 ItemNo = model.ItemNo,
                                                 Title = model.Title,
                                                 Description = model.Description,
                                                 CropCategoryId = model.CropCategoryId,
                                                 Price = model.Price
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task CropsUpdate(CropsModel model)
        {
            await _db.SaveDataAsync(_sp.CropsUpdate,
                                              new
                                              {
                                                  FMTITEMNO = model.FMTITEMNO,
                                                  StockUnit = model.StockUnit,
                                                  ItemNo = model.ItemNo,
                                                  Id = model.Id,
                                                  Title = model.Title,
                                                  Description = model.Description,
                                                  CropCategoryId = model.CropCategoryId,
                                                  Price = model.Price
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task CropsDelete(int Id)
        {
            await deleteValue(_sp.CropsDelete, Id);
        }

        public async Task<CropsModel> CropsGet(int Id)
        {
            List<CropsModel> output = new List<CropsModel>();
            output = await _db.LoadDataAsync<CropsModel, dynamic>(_sp.CropsGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var CropCategory = await CropCategoryGet(data.CropCategoryId);
                if (CropCategory != null)
                {
                    data.CropCategory = CropCategory;
                }
            }
            return output.FirstOrDefault();
        }

        public async Task<CropsModel> CropsGetByName(String Title)
        {
            List<CropsModel> output = new List<CropsModel>();
            output = await _db.LoadDataAsync<CropsModel, dynamic>(_sp.CropsGetByName,
                                                 new
                                                 { Title = Title },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var CropCategory = await CropCategoryGet(data.CropCategoryId);
                if (CropCategory != null)
                {
                    data.CropCategory = CropCategory;
                }
            }
            return output.FirstOrDefault();
        }

        public async Task<List<CropsModel>> CropsGetAll()
        {
            List<CropsModel> output = new List<CropsModel>();
            output = await _db.LoadDataAsync<CropsModel, dynamic>(_sp.CropsGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var CropCategory = await CropCategoryGet(data.CropCategoryId);
                if (CropCategory != null)
                {
                    data.CropCategory = CropCategory;
                }
            }
            return output;
        }

        public async Task<List<CropsModel>> GetAllCrops()
        {
            List<CropsModel> output = new List<CropsModel>();
            output = await _db.LoadDataAsync<CropsModel, dynamic>(_sp.GetAllCrops,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var CropCategory = await CropCategoryGet(data.CropCategoryId);
                if (CropCategory != null)
                {
                    data.CropCategory = CropCategory;
                }
            }
            return output;
        }

        public async Task<int> CropsCommercialCreate(CropsModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.CropsCommercialCreate,
                                             new
                                             {
                                                 FMTITEMNO = model.FMTITEMNO,
                                                 StockUnit = model.StockUnit,
                                                 ItemNo = model.ItemNo,
                                                 Title = model.Title,
                                                 Description = model.Description,
                                                 CropCategoryId = model.CropCategoryId,
                                                 Price = model.Price
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task CropsCommercialUpdate(CropsModel model)
        {
            await _db.SaveDataAsync(_sp.CropsCommercialUpdate,
                                              new
                                              {
                                                  FMTITEMNO = model.FMTITEMNO,
                                                  StockUnit = model.StockUnit,
                                                  ItemNo = model.ItemNo,
                                                  Id = model.Id,
                                                  Title = model.Title,
                                                  Description = model.Description,
                                                  CropCategoryId = model.CropCategoryId,
                                                  Price = model.Price
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task CropsCommercialDelete(int Id)
        {
            await deleteValue(_sp.CropsCommercialDelete, Id);
        }

        public async Task<CropsModel> CropsCommercialGet(int Id)
        {
            List<CropsModel> output = new List<CropsModel>();
            output = await _db.LoadDataAsync<CropsModel, dynamic>(_sp.CropsCommercialGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var CropCategory = await CropCategoryGet(data.CropCategoryId);
                if (CropCategory != null)
                {
                    data.CropCategory = CropCategory;
                }
            }
            return output.FirstOrDefault();
        }

        public async Task<CropsModel> CropsCommercialGetByName(String Title)
        {
            List<CropsModel> output = new List<CropsModel>();
            output = await _db.LoadDataAsync<CropsModel, dynamic>(_sp.CropsCommercialGetByName,
                                                 new
                                                 { Title = Title },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var CropCategory = await CropCategoryGet(data.CropCategoryId);
                if (CropCategory != null)
                {
                    data.CropCategory = CropCategory;
                }
            }
            return output.FirstOrDefault();
        }

        public async Task<List<CropsModel>> CropsCommercialGetAll()
        {
            List<CropsModel> output = new List<CropsModel>();
            output = await _db.LoadDataAsync<CropsModel, dynamic>(_sp.CropsCommercialGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var CropCategory = await CropCategoryGet(data.CropCategoryId);
                if (CropCategory != null)
                {
                    data.CropCategory = CropCategory;
                }
            }
            return output;
        }

        public async Task<List<CropsModel>> GetAllCropsCommercial()
        {
            List<CropsModel> output = new List<CropsModel>();
            output = await _db.LoadDataAsync<CropsModel, dynamic>(_sp.GetAllCropsCommercial,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var CropCategory = await CropCategoryGet(data.CropCategoryId);
                if (CropCategory != null)
                {
                    data.CropCategory = CropCategory;
                }
            }
            return output;
        }

        public async Task<int> FarmerCreate(FarmerModel model)
        {
            if (model.VerifiedString != null)
            {
                if (model.VerifiedString == "true")
                {
                    model.Verified = true;
                }
                else
                {
                    model.Verified = false;
                }
            }
            int output = 0;
            output = await _db.SaveDataAsync(_sp.FarmerCreate,
                                             new
                                             {
                                                 NRCNumber = model.NRCNumber,
                                                 Names = model.Names,
                                                 Province = model.Province,
                                                 District = model.District,
                                                 Verified = model.Verified,
                                                 CreatedBy = model.CreatedBy,
                                                 DOB = model.DOB,
                                                 PhoneNumber = model.PhoneNumber,
                                                 Gender = model.Gender,
                                                 PaymentMode = model.PaymentMode,
                                                 BankName = model.BankName,
                                                 BranchName = model.BranchName,
                                                 BranchCode = model.BranchCode,
                                                 AccountName = model.AccountName,
                                                 AccountNumber = model.AccountNumber,
                                                 ModifyBy = model.ModifyBy,
                                                 ModifyOn = model.ModifyOn
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task<int> FarmerCreatev2(FarmerModel model)
        {
            if (model.VerifiedString != null)
            {
                if (model.VerifiedString == "true")
                {
                    model.Verified = true;
                }
                else
                {
                    model.Verified = false;
                }
            }
            int output = 0;
            output = await _db.SaveDataAsync(_sp.FarmerCreatev2,
                                             new
                                             {
                                                 NRCNumber = model.NRCNumber,
                                                 Names = model.Names,
                                                 Province = model.Province,
                                                 District = model.District,
                                                 Verified = model.Verified,
                                                 CreatedBy = model.CreatedBy,
                                                 DOB = model.DOB,
                                                 PhoneNumber = model.PhoneNumber,
                                                 Gender = model.Gender,
                                                 PaymentMode = model.PaymentMode,
                                                 PaymentProviderId = model.PaymentProviderId,
                                                 BankBranchId = model.BankBranchId,
                                                 BankName = model.BankName,
                                                 BranchName = model.BranchName,
                                                 BranchCode = model.BranchCode,
                                                 AccountName = model.AccountName,
                                                 AccountNumber = model.AccountNumber,
                                                 ModifyBy = model.ModifyBy,
                                                 ModifyOn = model.ModifyOn
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task<int> FarmerUpdate(FarmerModel model)
        {
            if (model.VerifiedString != null)
            {
                if (model.VerifiedString == "true")
                {
                    model.Verified = true;
                }
                else
                {
                    model.Verified = false;
                }
            }
            int output = 0;
            output = await _db.SaveDataAsync(_sp.FarmerUpdate,
                                              new
                                              {
                                                  Id = model.Id,
                                                  NRCNumber = model.NRCNumber,
                                                  Names = model.Names,
                                                  Province = model.Province,
                                                  District = model.District,
                                                  Verified = model.Verified,
                                                  CreatedBy = model.CreatedBy,
                                                  DOB = model.DOB,
                                                  PhoneNumber = model.PhoneNumber,
                                                  Gender = model.Gender,
                                                  PaymentMode = model.PaymentMode,
                                                  BankName = model.BankName,
                                                  BranchName = model.BranchName,
                                                  BranchCode = model.BranchCode,
                                                  AccountName = model.AccountName,
                                                  AccountNumber = model.AccountNumber,
                                                  ModifyBy = model.ModifyBy,
                                                  ModifyOn = model.ModifyOn
                                              },
                                              connectionStringName,
                                              true);
            return output;
        }

        public async Task<int> FarmerUpdatev2(FarmerModel model)
        {
            if (model.VerifiedString != null)
            {
                if (model.VerifiedString == "true")
                {
                    model.Verified = true;
                }
                else
                {
                    model.Verified = false;
                }
            }
            int output = 0;
            output = await _db.SaveDataAsync(_sp.FarmerUpdatev2,
                                              new
                                              {
                                                  Id = model.Id,
                                                  NRCNumber = model.NRCNumber,
                                                  Names = model.Names,
                                                  Province = model.Province,
                                                  District = model.District,
                                                  Verified = model.Verified,
                                                  CreatedBy = model.CreatedBy,
                                                  DOB = model.DOB,
                                                  PhoneNumber = model.PhoneNumber,
                                                  Gender = model.Gender,
                                                  PaymentMode = model.PaymentMode,
                                                  BankName = model.BankName,
                                                  BranchName = model.BranchName,
                                                  BranchCode = model.BranchCode,
                                                  AccountName = model.AccountName,
                                                  AccountNumber = model.AccountNumber,
                                                  PaymentProviderId = model.PaymentProviderId,
                                                  BankBranchId = model.BankBranchId,
                                                  ModifyBy = model.ModifyBy,
                                                  ModifyOn = model.ModifyOn
                                              },
                                              connectionStringName,
                                              true);
            return output;
        }

        public async Task FarmerDelete(int Id)
        {
            await deleteValue(_sp.FarmerDelete, Id);
        }

        public async Task<FarmerModel> FarmerGet(int Id)
        {
            List<FarmerModel> output = new List<FarmerModel>();
            output = await _db.LoadDataAsync<FarmerModel, dynamic>(_sp.FarmerGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<FarmerModel>> FarmerGetByLocation(string Location, int Page = 1)
        {
            List<FarmerModel> output = new List<FarmerModel>();
            output = await _db.LoadDataAsync<FarmerModel, dynamic>(_sp.FarmerGetByLocation,
                                                 new
                                                 {
                                                     Location = Location,
                                                     Page = Page
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<FarmerModel> FarmersGetFromNrc(string Nrc)
        {
            List<FarmerModel> output = new List<FarmerModel>();
            output = await _db.LoadDataAsync<FarmerModel, dynamic>(_sp.FarmersGetFromNrc,
                                                 new
                                                 { nrc = Nrc },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<FarmerModel>> FarmerGetAll()
        {
            List<FarmerModel> output = new List<FarmerModel>();
            output = await _db.LoadDataAsync<FarmerModel, dynamic>(_sp.FarmerGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<int> FarmerCommercialCreate(FarmerModel model)
        {
            if (model.VerifiedString != null)
            {
                if (model.VerifiedString == "true")
                {
                    model.Verified = true;
                }
                else
                {
                    model.Verified = false;
                }
            }
            int output = 0;
            output = await _db.SaveDataAsync(_sp.FarmerCommercialCreate,
                                             new
                                             {
                                                 FarmerCode = model.FarmerCode,
                                                 NRCNumber = model.NRCNumber,
                                                 Names = model.Names,
                                                 Province = model.Province,
                                                 District = model.District,
                                                 Verified = model.Verified,
                                                 CreatedBy = model.CreatedBy,
                                                 DOB = model.DOB,
                                                 PhoneNumber = model.PhoneNumber,
                                                 Gender = model.Gender,
                                                 PaymentMode = model.PaymentMode,
                                                 BankName = model.BankName,
                                                 BranchName = model.BranchName,
                                                 BranchCode = model.BranchCode,
                                                 AccountName = model.AccountName,
                                                 AccountNumber = model.AccountNumber,
                                                 ModifyBy = model.ModifyBy,
                                                 ModifyOn = model.ModifyOn
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task<int> FarmerCommercialUpdate(FarmerModel model)
        {
            if (model.VerifiedString != null)
            {
                if (model.VerifiedString == "true")
                {
                    model.Verified = true;
                }
                else
                {
                    model.Verified = false;
                }
            }
            int output = 0;
            output = await _db.SaveDataAsync(_sp.FarmerCommercialUpdate,
                                              new
                                              {
                                                  Id = model.Id,
                                                  NRCNumber = model.NRCNumber,
                                                  FarmerCode = model.FarmerCode,
                                                  Names = model.Names,
                                                  Province = model.Province,
                                                  District = model.District,
                                                  Verified = model.Verified,
                                                  CreatedBy = model.CreatedBy,
                                                  DOB = model.DOB,
                                                  PhoneNumber = model.PhoneNumber,
                                                  Gender = model.Gender,
                                                  PaymentMode = model.PaymentMode,
                                                  BankName = model.BankName,
                                                  BranchName = model.BranchName,
                                                  BranchCode = model.BranchCode,
                                                  AccountName = model.AccountName,
                                                  AccountNumber = model.AccountNumber,
                                                  ModifyBy = model.ModifyBy,
                                                  ModifyOn = model.ModifyOn
                                              },
                                              connectionStringName,
                                              true);
            return output;
        }

        public async Task FarmerCommercialDelete(int Id)
        {
            await deleteValue(_sp.FarmerCommercialDelete, Id);
        }

        public async Task<FarmerModel> FarmerCommercialGet(int Id)
        {
            List<FarmerModel> output = new List<FarmerModel>();
            output = await _db.LoadDataAsync<FarmerModel, dynamic>(_sp.FarmerCommercialGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<FarmerModel>> FarmerCommercialGetByLocation(string Location, int Page = 1)
        {
            List<FarmerModel> output = new List<FarmerModel>();
            output = await _db.LoadDataAsync<FarmerModel, dynamic>(_sp.FarmerCommercialGetByLocation,
                                                 new
                                                 {
                                                     Location = Location,
                                                     Page = Page
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<FarmerModel> FarmersCommercialGetFromCode(string code)
        {
            List<FarmerModel> output = new List<FarmerModel>();
            output = await _db.LoadDataAsync<FarmerModel, dynamic>(_sp.FarmersCommercialGetFromCode,
                                                 new
                                                 { Code = code },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<FarmerModel>> FarmerCommercialGetAll()
        {
            List<FarmerModel> output = new List<FarmerModel>();
            output = await _db.LoadDataAsync<FarmerModel, dynamic>(_sp.FarmerCommercialGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<int> StaffCreate(StaffModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.StaffCreate,
                                             new
                                             {
                                                 StaffId = model.StaffId,
                                                 NRCNumber = model.NRCNumber,
                                                 Location = model.Location
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task StaffUpdate(StaffModel model)
        {
            await _db.SaveDataAsync(_sp.StaffUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 StaffId = model.StaffId,
                                                 NRCNumber = model.NRCNumber,
                                                 Location = model.Location
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task StaffDelete(int Id)
        {
            await deleteValue(_sp.StaffDelete, Id);
        }

        public async Task<StaffModel> StaffGet(int Id)
        {
            List<StaffModel> output = new List<StaffModel>();
            output = await _db.LoadDataAsync<StaffModel, dynamic>(_sp.StaffGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var userData = await UsersGet(data.StaffId);
                var locationData = await NetLocationGet(data.Location);
                if (userData != null)
                    data.StaffData = userData;
                data.StaffData.Password = _encryption.Decrypt(data.StaffData.Password);
                if (locationData != null)
                    data.LocationData = locationData;
            }
            return output.FirstOrDefault();
        }

        public async Task<List<StaffModel>> StaffGetByLocation(string Location)
        {
            List<StaffModel> output = new List<StaffModel>();
            output = await _db.LoadDataAsync<StaffModel, dynamic>(_sp.StaffGetByLocation,
                                                 new
                                                 { Location = Location },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var userData = await UsersGet(data.StaffId);
                var locationData = await NetLocationGet(data.Location);
                if (userData != null)
                    data.StaffData = userData;
                if (locationData != null)
                    data.LocationData = locationData;
            }
            return output;
        }

        public async Task<StaffModel> StaffGetByNrc(string nrc)
        {
            List<StaffModel> output = new List<StaffModel>();
            output = await _db.LoadDataAsync<StaffModel, dynamic>(_sp.StaffGetByNrc,
                                                 new
                                                 { nrc = nrc },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var userData = await UsersGet(data.StaffId);
                var locationData = await NetLocationGet(data.Location);
                if (userData != null)
                    data.StaffData = userData;
                if (locationData != null)
                    data.LocationData = locationData;
            }
            return output.FirstOrDefault();
        }

        public async Task<List<StaffModel>> StaffGetAll()
        {
            List<StaffModel> output = new List<StaffModel>();
            output = await _db.LoadDataAsync<StaffModel, dynamic>(_sp.StaffGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var userData = await UsersGet(data.StaffId);
                var locationData = await NetLocationGet(data.Location);
                if (userData != null)
                    data.StaffData = userData;
                if (locationData != null)
                    data.LocationData = locationData;
            }
            return output;
        }

        public async Task<int> AdminsCreate(AdminsModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.AdminsCreate,
                                             new
                                             {
                                                 AdminId = model.AdminId,
                                                 NRCNumber = model.NrcNumber,
                                                 Province = model.Province,
                                                 District = model.District
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task AdminsUpdate(AdminsModel model)
        {
            await _db.SaveDataAsync(_sp.AdminsUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 NRCNumber = model.NrcNumber,
                                                 Province = model.Province,
                                                 District = model.District
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task AdminsDelete(int Id)
        {
            await deleteValue(_sp.AdminsDelete, Id);
        }

        public async Task<AdminsModel> AdminsGet(int Id)
        {
            List<AdminsModel> output = new List<AdminsModel>();
            output = await _db.LoadDataAsync<AdminsModel, dynamic>(_sp.AdminsGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var userData = await UsersGet(data.AdminId);
                if (userData != null)
                {
                    data.UserData = userData;
                }
                var districtData = await NetDistrictGetByName(data.Province, data.District);
                if (districtData != null)
                {
                    data.DistrictName = districtData.FirstOrDefault().NAME;
                }
                var provinceData = await NetProvinceGetByCode(data.Province);
                if (provinceData != null)
                {
                    data.ProvinceName = provinceData.FirstOrDefault().NAME;
                }
            }
            return output.FirstOrDefault();
        }

        public async Task<AdminsModel> AdminsGetByAdminId(int Id)
        {
            List<AdminsModel> output = new List<AdminsModel>();
            output = await _db.LoadDataAsync<AdminsModel, dynamic>(_sp.AdminsGetByAdminId,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var userData = await UsersGet(data.AdminId);
                if (userData != null)
                {
                    data.UserData = userData;
                }
            }
            return output.FirstOrDefault();
        }

        public async Task<AdminsModel> AdminsGetByNrc(string nrc)
        {
            List<AdminsModel> output = new List<AdminsModel>();
            output = await _db.LoadDataAsync<AdminsModel, dynamic>(_sp.AdminGetByNrc,
                                                 new
                                                 { nrc = nrc },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var userData = await UsersGet(data.AdminId);
                if (userData != null)
                {
                    data.UserData = userData;
                }
            }
            return output.FirstOrDefault();
        }

        public async Task<List<AdminsModel>> AdminsGetAll()
        {
            List<AdminsModel> output = new List<AdminsModel>();
            output = await _db.LoadDataAsync<AdminsModel, dynamic>(_sp.AdminsGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var userData = await UsersGet(data.AdminId);
                if (userData != null)
                {
                    data.UserData = userData;
                }
            }
            return output;
        }

        /// <summary>
        /// PRCN HANDLER
        /// </summary>
        public async Task<PRCNModel> PRCNNGet(string STranNo)
        {
            List<PRCNModel> output = new List<PRCNModel>();
            output = await _db.LoadDataAsync<PRCNModel, dynamic>(_sp.PRCNNGet,
                                                 new
                                                 { sTranNo = STranNo },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<BCLModel> BCLGet(string STranNo)
        {
            List<BCLModel> output = new List<BCLModel>();
            output = await _db.LoadDataAsync<BCLModel, dynamic>(_sp.BCLGet,
                                                 new
                                                 { sTranNo = STranNo },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<PRCNModel>> PRCNGetAll(TransactionFilter model)
        {
            List<PRCNModel> output = new List<PRCNModel>();
            output = await _db.LoadDataAsync<PRCNModel, dynamic>(_sp.PRCNGetAll,
                                                 new
                                                 {
                                                     shouldFilter = model.shouldFilter,
                                                     dateFilter = model.dateFilter,
                                                     locationFilter = model.locationFilter,
                                                     startIndex = model.StartIndex,
                                                     batchSize = model.BatchSize,
                                                     StartDate = model.StartDate,
                                                     EndDate = model.EndDate,
                                                     location = model.location
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<BCLModel>> BCLGetAll(TransactionFilter model)
        {
            List<BCLModel> output = new List<BCLModel>();
            output = await _db.LoadDataAsync<BCLModel, dynamic>(_sp.BCLGetAll,
                                                 new
                                                 {
                                                     shouldFilter = model.shouldFilter,
                                                     dateFilter = model.dateFilter,
                                                     locationFilter = model.locationFilter,
                                                     startIndex = model.StartIndex,
                                                     batchSize = model.BatchSize,
                                                     StartDate = model.StartDate,
                                                     EndDate = model.EndDate,
                                                     location = model.location
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<PRCNModel>> PRCNNGetById(string sTranNo)
        {
            List<PRCNModel> output = new List<PRCNModel>();
            output = await _db.LoadDataAsync<PRCNModel, dynamic>(_sp.PRCNNGetById,
                                                 new
                                                 {
                                                     sTranNo = sTranNo,
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<PRCNModel>> PRCNGetByLocation(string location, int page = 1, int rowCount = 100)
        {
            List<PRCNModel> output = new List<PRCNModel>();
            output = await _db.LoadDataAsync<PRCNModel, dynamic>(_sp.PRCNGetByLocation,
                                                 new
                                                 {
                                                     location = location,
                                                     Page = page,
                                                     rowCount = rowCount
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task PRCNUpdate(PRCNModel model)
        {
            await _db.SaveDataAsync(_sp.PRCNUpdate,
                                           new
                                           {
                                               Id = model.sTranNo,
                                               Status = model.Status
                                           },
                                           connectionStringName,
                                           true);
        }

        public async Task<int> BCLUpdateStatus(BCLModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.BCLUpdate,
                                           new
                                           {
                                               Id = model.sTranNo,
                                               Status = model.bclStatus
                                           },
                                           connectionStringName,
                                           true);
            return output;
        }

        public async Task<int> PRCNNCreate(PRCNModel model)
        {
            int output = 0;
            var farmerID = await FarmersGetFromNrc(model.sVendorID);
            var deviceId = await DevicesGetBySerialNumber(model.DeviceSerialNumber);
            var prcn = await PRCNNGet(model.sTranNo);
            var locationData = await NetLocationGet(model.SATELITE);

            if (prcn == null)
            {
                if (farmerID != null && deviceId != null && locationData != null)
                {
                    output = await _db.SaveDataAsync(_sp.PRCNNCreate,
                                                 new
                                                 {
                                                     sTranNo = model.sTranNo,
                                                     rcpno = model.rcpno,
                                                     dtDateTime = model.dtDateTime,
                                                     dateverify = model.dateverify,
                                                     sVendorID = model.sVendorID,
                                                     sVendorName = model.sVendorName,
                                                     PROVINCE = locationData.PROVINCE,
                                                     DISTRICT = locationData.DISTRICT,
                                                     SATELITE = model.SATELITE,
                                                     sItemNo = model.sItemNo,
                                                     sItemDescr = model.sItemDescr,
                                                     sUnit = model.sUnit,
                                                     NoOfBags = model.NoOfBags,
                                                     sUnitCost = model.sUnitCost,
                                                     dDocTotal = model.dDocTotal,
                                                     bclpost = model.bclpost,
                                                     bcllisting = model.bcllisting,
                                                     verifyUser = model.verifyUser,
                                                     dateModifyBy = model.dateModifyBy,
                                                     dateModify = model.dateModify,
                                                     sGrainType = model.sGrainType,
                                                     dDocTotalUsd = model.dDocTotalUsd,
                                                     Type = model.Type
                                                 },
                                                 connectionStringName,
                                                 true);

                    if (output > 0)
                    {

                    }

                    return output;
                }
                else
                {
                    string error = "ERROR: ";
                    if (farmerID == null)
                    {
                        error += "Farmer NRC does not exist\n";
                    }
                    else if (deviceId == null)
                    {
                        error += "Device ID does not exist\n";
                    }
                    else if (locationData == null)
                    {
                        error += "Location does not exist\n";
                    }
                    throw new ArgumentException(error);
                }
            }
            else
            {
                return 0;
                // throw new ArgumentException($"PRCN '{model.sTranNo}' already exists");
            }
            /*if (farmerID == null)
            {
                await _db.SaveDataAsync(_sp.FarmerCreate,
                                             new
                                             {
                                                 NRCNumber = model.sVendorID,
                                                 Names = model.sVendorName,
                                                 Province = locationData.PROVINCE,
                                                 District = locationData.DISTRICT,
                                                 Verified = false,
                                                 CreatedBy = "FRA",
                                                 DOB = DateTime.Now,
                                                 PhoneNumber = "260977101010",
                                                 Gender = "Male",
                                                 PaymentMode = "Cash",
                                                 BankName = "",
                                                 BranchName = "",
                                                 BranchCode = "",
                                                 AccountName = "",
                                                 AccountNumber = "",
                                                 ModifyBy = "FRA",
                                                 ModifyOn = DateTime.Now
                                             },
                                             connectionStringName,
                                             true);
            }

            farmerID = await FarmersGetFromNrc(model.sVendorID); */


        }

        public async Task<int> PRCNCreate(PRCNModel model)
        {
            int output = 0;
            var farmerID = await FarmersGetFromNrc(model.sVendorID);
            if (model.Type == 1)
            {
                farmerID = await FarmersCommercialGetFromCode(model.sVendorID);
            }
            var deviceId = await DevicesGetBySerialNumber(model.DeviceSerialNumber);
            var prcn = await PRCNNGet(model.sTranNo);
            var locationData = await NetLocationGet(model.SATELITE);

            if (prcn == null)
            {
                if (farmerID != null && deviceId != null && locationData != null)
                {
                    output = await _db.SaveDataAsync(_sp.PRCNCreate,
                                                 new
                                                 {
                                                     sTranNo = model.sTranNo,
                                                     rcpno = model.rcpno,
                                                     dtDateTime = model.dtDateTime,
                                                     dateverify = model.dateverify,
                                                     sVendorID = model.sVendorID,
                                                     sVendorName = model.sVendorName,
                                                     PROVINCE = locationData.PROVINCE,
                                                     DISTRICT = locationData.DISTRICT,
                                                     SATELITE = model.SATELITE,
                                                     sItemNo = model.sItemNo,
                                                     sItemDescr = model.sItemDescr,
                                                     sUnit = model.sUnit,
                                                     NoOfBags = model.NoOfBags,
                                                     sUnitCost = model.sUnitCost,
                                                     sGrainType = model.sGrainType,
                                                     dDocTotal = model.dDocTotal,
                                                     dDocTotalUsd = model.dDocTotalUsd,
                                                     dDocRate = model.dDocRate,
                                                     bclpost = model.bclpost,
                                                     bcllisting = model.bcllisting,
                                                     verifyUser = model.verifyUser,
                                                     dateModifyBy = model.dateModifyBy,
                                                     dateModify = model.dateModify,
                                                     Type = model.Type
                                                 },
                                                 connectionStringName,
                                                 true);
                    return output;
                }
                else
                {
                    string error = "ERROR: ";
                    if (farmerID == null)
                    {
                        error += "Farmer does not exist\n";
                    }
                    else if (deviceId == null)
                    {
                        error += "Device ID does not exist\n";
                    }
                    else if (locationData == null)
                    {
                        error += "Location does not exist\n";
                    }
                    throw new ArgumentException(error);
                }
            }
            else
            {
                return 0;
                // throw new ArgumentException($"PRCN '{model.sTranNo}' already exists");
            }
        }

        /// <summary>
        /// TRANSACTION hANDLER
        /// </summary>
        public async Task<int> TransactionsCreate(TransactionsModel model)
        {
            int output = 0;
            var farmerID = await FarmersGetFromNrc(model.FarmerNrc);
            var deviceId = await DevicesGetBySerialNumber(model.DeviceSerialNumber);
            var prcn = await TransactionsGetByPRCN(model.PRCN);
            if (prcn == null)
            {
                if (farmerID != null && deviceId != null)
                {
                    model.FarmerId = farmerID.Id;
                    model.DeviceId = deviceId.Id;
                    output = await _db.SaveDataAsync(_sp.TransactionsCreate,
                                                 new
                                                 {
                                                     PRCN = model.PRCN,
                                                     CropId = model.CropId,
                                                     FarmerId = model.FarmerId,
                                                     Quantity = model.Quantity,
                                                     Total = model.Total,
                                                     Location = model.Location,
                                                     DeviceId = model.DeviceId,
                                                     Status = model.Status
                                                 },
                                                 connectionStringName,
                                                 true);
                    return output;
                }
                else
                {
                    string error = "ERROR";
                    if (farmerID == null)
                    {
                        error = "Farmer NRC does not exist";
                    }
                    else if (deviceId == null)
                    {
                        error = "Device ID does not exist";
                    }
                    throw new ArgumentException(error);
                }
            }
            else
            {
                throw new ArgumentException($"PRCN '{model.PRCN}' already exists");
            }
        }

        public async Task TransactionsUpdate(TransactionsModel model)
        {
            var farmerID = await FarmersGetFromNrc(model.FarmerNrc);
            if (farmerID != null)
            {
                model.FarmerId = farmerID.Id;
            }

            await _db.SaveDataAsync(_sp.TransactionsUpdate,
                                           new
                                           {
                                               Id = model.Id,
                                               Status = model.Status
                                           },
                                           connectionStringName,
                                           true);
        }

        public async Task TransactionsDelete(int Id)
        {
            await deleteValue(_sp.TransactionsDelete, Id);
        }

        public async Task<TransactionsModel> TransactionsGetByPRCN(string PRCN)
        {
            List<TransactionsModel> output = new List<TransactionsModel>();
            output = await _db.LoadDataAsync<TransactionsModel, dynamic>(_sp.TransactionsGetByPRCN,
                                                 new
                                                 { PRCN = PRCN },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var farmerdata = await FarmerGet(data.FarmerId);
                var cropData = await CropsGet(data.CropId);
                var deviceData = await DevicesGet(data.DeviceId);
                if (farmerdata != null)
                    data.FarmerData = farmerdata;
                if (cropData != null)
                    data.CropData = cropData;
                if (deviceData != null)
                    data.DeviceData = deviceData;
            }
            return output.FirstOrDefault();
        }

        public async Task<TransactionsModel> TransactionsGet(int Id)
        {
            List<TransactionsModel> output = new List<TransactionsModel>();
            output = await _db.LoadDataAsync<TransactionsModel, dynamic>(_sp.TransactionsGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var farmerdata = await FarmerGet(data.FarmerId);
                var cropData = await CropsGet(data.CropId);
                var deviceData = await DevicesGet(data.DeviceId);
                if (farmerdata != null)
                    data.FarmerData = farmerdata;
                if (cropData != null)
                    data.CropData = cropData;
                if (deviceData != null)
                    data.DeviceData = deviceData;
            }
            return output.FirstOrDefault();
        }

        public async Task<List<TransactionsModel>> TransactionsGetAll(TransactionFilter model)
        {
            List<TransactionsModel> output = new List<TransactionsModel>();
            output = await _db.LoadDataAsync<TransactionsModel, dynamic>(_sp.TransactionsGetAll,
                                                 new
                                                 {
                                                     shouldFilter = model.shouldFilter,
                                                     dateFilter = model.dateFilter,
                                                     locationFilter = model.locationFilter,
                                                     StartDate = model.StartDate,
                                                     EndDate = model.EndDate,
                                                     location = model.location
                                                 },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var farmerdata = await FarmerGet(data.FarmerId);
                var cropData = await CropsGet(data.CropId);
                var deviceData = await DevicesGet(data.DeviceId);
                if (farmerdata != null)
                    data.FarmerData = farmerdata;
                if (cropData != null)
                    data.CropData = cropData;
                if (deviceData != null)
                    data.DeviceData = deviceData;
            }
            return output;
        }

        public async Task<int> SateliteCreate(SateliteModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.SateliteCreate,
                                             new
                                             {
                                                 Title = model.Title,
                                                 Prefix = model.Prefix,
                                                 DistrictId = model.DistrictId
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task SateliteUpdate(SateliteModel model)
        {
            await _db.SaveDataAsync(_sp.SateliteUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Title = model.Title,
                                                 Prefix = model.Prefix,
                                                 DistrictId = model.DistrictId
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task SateliteDelete(int Id)
        {
            await deleteValue(_sp.SateliteDelete, Id);
        }

        public async Task<SateliteModel> SateliteGet(int Id)
        {
            List<SateliteModel> output = new List<SateliteModel>();
            output = await _db.LoadDataAsync<SateliteModel, dynamic>(_sp.SateliteGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var DistrictModel = await DistrictGet(data.DistrictId);
                if (DistrictModel != null)
                {
                    data.DistrictData = DistrictModel;
                }
            }
            return output.FirstOrDefault();
        }

        public async Task<List<SateliteModel>> SateliteGetFromDistrict(int Id)
        {
            List<SateliteModel> output = new List<SateliteModel>();
            output = await _db.LoadDataAsync<SateliteModel, dynamic>(_sp.SateliteGetFromDistrict,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var DistrictModel = await DistrictGet(data.DistrictId);
                if (DistrictModel != null)
                {
                    data.DistrictData = DistrictModel;
                }
            }
            return output;
        }

        public async Task<List<SateliteModel>> SateliteGetAll()
        {
            List<SateliteModel> output = new List<SateliteModel>();
            output = await _db.LoadDataAsync<SateliteModel, dynamic>(_sp.SateliteGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var DistrictModel = await DistrictGet(data.DistrictId);
                if (DistrictModel != null)
                {
                    data.DistrictData = DistrictModel;
                }
            }
            return output;
        }



        public async Task<int> DistrictCreate(DistrictModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.DistrictCreate,
                                             new
                                             {
                                                 Title = model.Title,
                                                 Prefix = model.Prefix,
                                                 ProvinceId = model.ProvinceId
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task DistrictUpdate(DistrictModel model)
        {
            await _db.SaveDataAsync(_sp.DistrictUpdate,
                                              new
                                              {
                                                  Id = model.Id,
                                                  Title = model.Title,
                                                  Prefix = model.Prefix,
                                                  ProvinceId = model.ProvinceId
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task DistrictDelete(int Id)
        {
            await deleteValue(_sp.DistrictDelete, Id);
        }

        public async Task<DistrictModel> DistrictGet(int Id)
        {
            List<DistrictModel> output = new List<DistrictModel>();
            output = await _db.LoadDataAsync<DistrictModel, dynamic>(_sp.DistrictGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var ProvinceModel = await ProvinceGet(data.ProvinceId);
                if (ProvinceModel != null)
                {
                    data.ProvinceData = ProvinceModel;
                }
            }
            return output.FirstOrDefault();
        }

        public async Task<List<DistrictModel>> DistrictGetFromProvince(int Id)
        {
            List<DistrictModel> output = new List<DistrictModel>();
            output = await _db.LoadDataAsync<DistrictModel, dynamic>(_sp.DistrictGetFromProvince,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var ProvinceModel = await ProvinceGet(data.ProvinceId);
                if (ProvinceModel != null)
                {
                    data.ProvinceData = ProvinceModel;
                }
            }
            return output;
        }

        public async Task<List<DistrictModel>> DistrictGetAll()
        {
            List<DistrictModel> output = new List<DistrictModel>();
            output = await _db.LoadDataAsync<DistrictModel, dynamic>(_sp.DistrictGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            foreach (var data in output)
            {
                var ProvinceModel = await ProvinceGet(data.ProvinceId);
                if (ProvinceModel != null)
                {
                    data.ProvinceData = ProvinceModel;
                }
            }
            return output;
        }

        public async Task<int> ProvinceCreate(ProvinceModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.ProvinceCreate,
                                             new
                                             {
                                                 Title = model.Title,
                                                 Prefix = model.Prefix
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task ProvinceUpdate(ProvinceModel model)
        {
            await _db.SaveDataAsync(_sp.ProvinceUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Title = model.Title,
                                                 Prefix = model.Prefix
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task ProvinceDelete(int Id)
        {
            await deleteValue(_sp.ProvinceDelete, Id);
        }

        public async Task<ProvinceModel> ProvinceGet(int Id)
        {
            List<ProvinceModel> output = new List<ProvinceModel>();
            output = await _db.LoadDataAsync<ProvinceModel, dynamic>(_sp.ProvinceGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<ProvinceModel>> ProvinceGetAll()
        {
            List<ProvinceModel> output = new List<ProvinceModel>();
            output = await _db.LoadDataAsync<ProvinceModel, dynamic>(_sp.ProvinceGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<DashboardStatsModel> DashboardStats(StatsFilterModel model)
        {
            List<DashboardStatsModel> output = new List<DashboardStatsModel>();
            output = await _db.LoadDataAsync<DashboardStatsModel, dynamic>(_sp.DashboardStats,
                                                 new
                                                 {
                                                     provinceId = model.provinceId,
                                                     shouldFilter = model.shouldFilter,
                                                     cropId = model.cropId,
                                                     startDateFilter = model.startDateFilter,
                                                     endDateFilter = model.endDateFilter,
                                                     districtId = model.districtId,
                                                     satelliteId = model.satelliteId,
                                                 },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();

        }

        public async Task<DeviceStatsModel> DeviceStats()
        {
            List<DeviceStatsModel> output = new List<DeviceStatsModel>();
            output = await _db.LoadDataAsync<DeviceStatsModel, dynamic>(_sp.DeviceStats,
                                                 new
                                                 {

                                                 },
                                                 connectionStringName,
                                                 true);

            return output.FirstOrDefault();
        }

        public async Task<TransactionStatsModel> TransactionStats()
        {
            List<TransactionStatsModel> output = new List<TransactionStatsModel>();
            output = await _db.LoadDataAsync<TransactionStatsModel, dynamic>(_sp.TransactionStats,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<TransactionStatsModel> SalesStats()
        {
            List<TransactionStatsModel> output = new List<TransactionStatsModel>();
            output = await _db.LoadDataAsync<TransactionStatsModel, dynamic>(_sp.SalesStats,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<TransactionStatsModel> TransactionStatsByDistrict(string DistrictCode)
        {
            List<TransactionStatsModel> output = new List<TransactionStatsModel>();
            output = await _db.LoadDataAsync<TransactionStatsModel, dynamic>(_sp.TransactionStatsByDistrict,
                                                 new
                                                 { DistrictCode = DistrictCode },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<TransactionStatsModel> SalesStatsByDistrict(string DistrictCode)
        {
            List<TransactionStatsModel> output = new List<TransactionStatsModel>();
            output = await _db.LoadDataAsync<TransactionStatsModel, dynamic>(_sp.SalesStatsByDistrict,
                                                 new
                                                 { DistrictCode = DistrictCode },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<List<StatsByYear>> TransactionStatsByYear(StatsFilterModel model)
        {
            List<StatsByYear> output = new List<StatsByYear>();
            output = await _db.LoadDataAsync<StatsByYear, dynamic>(_sp.TransactionStatsByYear,
                                                 new
                                                 {
                                                     provinceId = model.provinceId,
                                                     shouldFilter = model.shouldFilter,
                                                     cropId = model.cropId,
                                                     startDateFilter = model.startDateFilter,
                                                     endDateFilter = model.endDateFilter,
                                                     districtId = model.districtId,
                                                     satelliteId = model.satelliteId,
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<StatsByWeek>> TransactionStatsByWeek(StatsFilterModel model)
        {
            List<StatsByWeek> output = new List<StatsByWeek>();
            output = await _db.LoadDataAsync<StatsByWeek, dynamic>(_sp.TransactionStatsByWeek,
                                                 new
                                                 {
                                                     provinceId = model.provinceId,
                                                     shouldFilter = model.shouldFilter,
                                                     cropId = model.cropId,
                                                     startDateFilter = model.startDateFilter,
                                                     endDateFilter = model.endDateFilter,
                                                     districtId = model.districtId,
                                                     satelliteId = model.satelliteId,
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<StatsBySatelietSales>> TransactionStatsBySatelietSales(StatsFilterModel model)
        {
            List<StatsBySatelietSales> output = new List<StatsBySatelietSales>();
            output = await _db.LoadDataAsync<StatsBySatelietSales, dynamic>(_sp.TransactionStatsBySatelietSales,
                                                 new
                                                 {
                                                     provinceId = model.provinceId,
                                                     shouldFilter = model.shouldFilter,
                                                     cropId = model.cropId,
                                                     startDateFilter = model.startDateFilter,
                                                     endDateFilter = model.endDateFilter,
                                                     districtId = model.districtId,
                                                     satelliteId = model.satelliteId,
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<StatsByByCropsSold>> TransactionStatsByCropsSold(StatsFilterModel model)
        {
            List<StatsByByCropsSold> output = new List<StatsByByCropsSold>();
            output = await _db.LoadDataAsync<StatsByByCropsSold, dynamic>(_sp.TransactionStatsByCropsSold,
                                                 new
                                                 {
                                                     provinceId = model.provinceId,
                                                     shouldFilter = model.shouldFilter,
                                                     cropId = model.cropId,
                                                     startDateFilter = model.startDateFilter,
                                                     endDateFilter = model.endDateFilter,
                                                     districtId = model.districtId,
                                                     satelliteId = model.satelliteId,
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<netPurLocationModel>> NetProvinceGet()
        {
            List<netPurLocationModel> output = new List<netPurLocationModel>();
            output = await _db.LoadDataAsync<netPurLocationModel, dynamic>(_sp.NetProvinceGet,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);

            return output;
        }

        public async Task<List<netPurLocationModel>> NetProvinceGetByCode(string ProvinceCode)
        {
            List<netPurLocationModel> output = new List<netPurLocationModel>();
            output = await _db.LoadDataAsync<netPurLocationModel, dynamic>(_sp.NetProvinceGetByCode,
                                                 new
                                                 { ProvinceCode = ProvinceCode },
                                                 connectionStringName,
                                                 true);

            return output;
        }

        public async Task<List<netPurLocationModel>> NetSateliteGet(string DistrictCode)
        {
            List<netPurLocationModel> output = new List<netPurLocationModel>();
            output = await _db.LoadDataAsync<netPurLocationModel, dynamic>(_sp.NetSateliteGet,
                                                 new
                                                 { DistrictCode = DistrictCode },
                                                 connectionStringName,
                                                 true);

            return output;
        }

        public async Task<List<netPurLocationModel>> NetDistrictGet(string ProvinceCode)
        {
            List<netPurLocationModel> output = new List<netPurLocationModel>();
            output = await _db.LoadDataAsync<netPurLocationModel, dynamic>(_sp.NetDistrictGet,
                                                new
                                                { ProvinceCode = ProvinceCode },
                                                connectionStringName,
                                                true);

            return output;
        }

        public async Task<List<netPurLocationModel>> NetDistrictGetByName(string ProvinceCode, string District)
        {
            List<netPurLocationModel> output = new List<netPurLocationModel>();
            output = await _db.LoadDataAsync<netPurLocationModel, dynamic>(_sp.NetDistrictGetByName,
                                                new
                                                { ProvinceCode = ProvinceCode, District = District },
                                                connectionStringName,
                                                true);

            return output;
        }

        public async Task<netPurLocationModel> NetLocationGet(string Location)
        {
            List<netPurLocationModel> output = new List<netPurLocationModel>();
            output = await _db.LoadDataAsync<netPurLocationModel, dynamic>(_sp.NetLocationGet,
                                                new
                                                { Location = Location },
                                                connectionStringName,
                                                true);

            return output.FirstOrDefault();
        }

        public async Task<int> IDTCreate(IDTModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.IDTCreate,
                                             new
                                             {
                                                 IDTNo = model.IDTNo,
                                                 //DIDTNo = model.DIDTNo,
                                                 IDTDate = model.IDTDate,
                                                 LONumber = model.LONumber,
                                                 FromLoc = model.FromLoc,
                                                 FromLocName = model.FromLocName,
                                                 ToLoc = model.ToLoc,
                                                 ToLocName = model.ToLocName,
                                                 sContrLoc = model.sContrLoc,
                                                 GITLOC = model.GITLOC,
                                                 sItemNo = model.sItemNo,
                                                 sDesc = model.sDesc,
                                                 sUnit = model.sUnit,
                                                 SQtyTransBG = model.SQtyTransBG,
                                                 SAvgWeig = model.SAvgWeig,
                                                 SQtyAvail = model.SQtyAvail,
                                                 SBagWeiged = model.SBagWeiged,
                                                 sVendor = model.sVendor,
                                                 sVendorName = model.sVendorName,
                                                 Driver = model.Driver,
                                                 Id = model.Id,
                                                 Vehicle = model.Vehicle,
                                                 IDTPreparedBy = model.IDTPreparedBy,
                                                 IDTRef = model.IDTRef,
                                                 IDTRefDate = model.IDTRefDate,
                                                 IDComplete = model.IDComplete,
                                                 IDTPrinted = model.IDTPrinted,
                                                 IDTIntegrate = model.IDTIntegrate,
                                                 IDTModifyBy = model.IDTModifyBy,
                                                 IDTModifyDate = model.IDTModifyDate
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task IDTUpdate(IDTModel model)
        {
            await _db.SaveDataAsync(_sp.IDTUpdate,
                                              new
                                              {
                                                  IDTNo = model.IDTNo,
                                                  //DIDTNo = model.DIDTNo,
                                                  IDTDate = model.IDTDate,
                                                  LONumber = model.LONumber,
                                                  FromLoc = model.FromLoc,
                                                  FromLocName = model.FromLocName,
                                                  ToLoc = model.ToLoc,
                                                  ToLocName = model.ToLocName,
                                                  sContrLoc = model.sContrLoc,
                                                  GITLOC = model.GITLOC,
                                                  sItemNo = model.sItemNo,
                                                  sDesc = model.sDesc,
                                                  sUnit = model.sUnit,
                                                  SQtyTransBG = model.SQtyTransBG,
                                                  SAvgWeig = model.SAvgWeig,
                                                  SQtyAvail = model.SQtyAvail,
                                                  SBagWeiged = model.SBagWeiged,
                                                  sVendor = model.sVendor,
                                                  sVendorName = model.sVendorName,
                                                  Driver = model.Driver,
                                                  Id = model.Id,
                                                  Vehicle = model.Vehicle,
                                                  IDTPreparedBy = model.IDTPreparedBy,
                                                  IDTRef = model.IDTRef,
                                                  IDTRefDate = model.IDTRefDate,
                                                  IDComplete = model.IDComplete,
                                                  IDTPrinted = model.IDTPrinted,
                                                  IDTIntegrate = model.IDTIntegrate,
                                                  IDTModifyBy = model.IDTModifyBy,
                                                  IDTModifyDate = model.IDTModifyDate
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task IDTUpdateStatus(IDTModel model)
        {
            await _db.SaveDataAsync(_sp.IDTUpdateStatus,
                                           new
                                           {
                                               Id = model.IDTNo,
                                               Status = model.IDTStatus
                                           },
                                           connectionStringName,
                                           true);
        }

        public async Task<List<IDTModel>> IDTGetByLocation(string location, int page = 1, int rowCount = 250)
        {
            List<IDTModel> output = new List<IDTModel>();
            output = await _db.LoadDataAsync<IDTModel, dynamic>(_sp.IDTGetByLocation,
                                                new
                                                {
                                                    Location = location,
                                                    Page = page,
                                                    rowCount = rowCount
                                                },
                                                connectionStringName,
                                                true);

            return output;
        }

        public async Task<List<IDTModel>> IDTGetAll(TransactionFilter model)
        {
            List<IDTModel> output = new List<IDTModel>();
            output = await _db.LoadDataAsync<IDTModel, dynamic>(_sp.IDTGetAll,
                                                 new
                                                 {
                                                     shouldFilter = model.shouldFilter,
                                                     dateFilter = model.dateFilter,
                                                     locationFilter = model.locationFilter,
                                                     startIndex = model.StartIndex,
                                                     batchSize = model.BatchSize,
                                                     StartDate = model.StartDate,
                                                     EndDate = model.EndDate,
                                                     location = model.location
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<int> GRNCreate(GRNModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.GRNCreate,
                                             new
                                             {
                                                 GRNTDate = model.GRNTDate,
                                                 GRNTNo = model.GRNTNo,
                                                 GRNIntegrate = model.GRNIntegrate,
                                                 GrnPrinted = model.GrnPrinted,
                                                 GrnComplete = model.GrnComplete,
                                                 GRNRefDate = model.GRNRefDate,
                                                 GrnRef = model.GrnRef,
                                                 GrnParedBy = model.GrnParedBy,
                                                 GrnVehicle = model.GrnVehicle,
                                                 GrnId = model.GrnId,
                                                 GrnDriver = model.GrnDriver,
                                                 sVendorName = model.sVendorName,
                                                 sVendor = model.sVendor,
                                                 QtyVariance = model.QtyVariance,
                                                 SBagWeiged = model.SBagWeiged,
                                                 SAvgWeig = model.SAvgWeig,
                                                 SQtyGRNBG = model.SQtyGRNBG,
                                                 sUnit = model.sUnit,
                                                 sDesc = model.sDesc,
                                                 sItemNo = model.sItemNo,
                                                 GITLOC = model.GITLOC,
                                                 sContrLoc = model.sContrLoc,
                                                 ToLocName = model.ToLocName,
                                                 ToLoc = model.ToLoc,
                                                 FromLocName = model.FromLocName,
                                                 FromLoc = model.FromLoc,
                                                 LONumber = model.LONumber,
                                                 IDTNo = model.IDTNo,
                                                 GrnPosted = model.GrnPosted,
                                                 GRNModifyBy = model.GRNModifyBy,
                                                 GRNModifyDate = model.GRNModifyDate
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task GRNUpdate(GRNModel model)
        {
            await _db.SaveDataAsync(_sp.GRNUpdate,
                                             new
                                             {
                                                 GRNTDate = model.GRNTDate,
                                                 GRNTNo = model.GRNTNo,
                                                 GRNIntegrate = model.GRNIntegrate,
                                                 GrnPrinted = model.GrnPrinted,
                                                 GrnComplete = model.GrnComplete,
                                                 GRNRefDate = model.GRNRefDate,
                                                 GrnRef = model.GrnRef,
                                                 GrnParedBy = model.GrnParedBy,
                                                 GrnVehicle = model.GrnVehicle,
                                                 GrnId = model.GrnId,
                                                 GrnDriver = model.GrnDriver,
                                                 sVendorName = model.sVendorName,
                                                 sVendor = model.sVendor,
                                                 QtyVariance = model.QtyVariance,
                                                 SBagWeiged = model.SBagWeiged,
                                                 SAvgWeig = model.SAvgWeig,
                                                 SQtyGRNBG = model.SQtyGRNBG,
                                                 sUnit = model.sUnit,
                                                 sDesc = model.sDesc,
                                                 sItemNo = model.sItemNo,
                                                 GITLOC = model.GITLOC,
                                                 sContrLoc = model.sContrLoc,
                                                 ToLocName = model.ToLocName,
                                                 ToLoc = model.ToLoc,
                                                 FromLocName = model.FromLocName,
                                                 FromLoc = model.FromLoc,
                                                 LONumber = model.LONumber,
                                                 IDTNo = model.IDTNo,
                                                 GrnPosted = model.GrnPosted,
                                                 GRNModifyBy = model.GRNModifyBy,
                                                 GRNModifyDate = model.GRNModifyDate
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task GRNUpdateStatus(GRNModel model)
        {
            await _db.SaveDataAsync(_sp.GRNUpdateStatus,
                                           new
                                           {
                                               Id = model.IDTNo,
                                               //Status = model.Status
                                           },
                                           connectionStringName,
                                           true);
        }

        public async Task<List<GRNModel>> GRNGetAll(TransactionFilter model)
        {
            List<GRNModel> output = new List<GRNModel>();
            output = await _db.LoadDataAsync<GRNModel, dynamic>(_sp.GRNGetAll,
                                                 new
                                                 {
                                                     shouldFilter = model.shouldFilter,
                                                     dateFilter = model.dateFilter,
                                                     locationFilter = model.locationFilter,
                                                     startIndex = model.StartIndex,
                                                     batchSize = model.BatchSize,
                                                     StartDate = model.StartDate,
                                                     EndDate = model.EndDate,
                                                     location = model.location
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<GRNModel>> GRNGetByLocation(string location, int page = 1, int rowCount = 250)
        {
            List<GRNModel> output = new List<GRNModel>();
            output = await _db.LoadDataAsync<GRNModel, dynamic>(_sp.GRNGetByLocation,
                                                new
                                                {
                                                    Location = location,
                                                    Page = page,
                                                    rowCount = rowCount,
                                                },
                                                connectionStringName,
                                                true);

            return output;
        }

        public async Task<List<LoadingOrdersModel>> LoadingOrdersGetAll(TransactionFilter model)
        {
            List<LoadingOrdersModel> output = new List<LoadingOrdersModel>();
            output = await _db.LoadDataAsync<LoadingOrdersModel, dynamic>(_sp.LoadingOrdersGetAll,
                                                 new
                                                 {
                                                     shouldFilter = model.shouldFilter,
                                                     dateFilter = model.dateFilter,
                                                     locationFilter = model.locationFilter,
                                                     startIndex = model.StartIndex,
                                                     batchSize = model.BatchSize,
                                                     StartDate = model.StartDate,
                                                     EndDate = model.EndDate,
                                                     location = model.location
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<LoadingOrdersModel>> LoadingOrdersGetByLocation(string location, int page = 1, int rowCount = 250)
        {
            List<LoadingOrdersModel> output = new List<LoadingOrdersModel>();
            output = await _db.LoadDataAsync<LoadingOrdersModel, dynamic>(_sp.LoadingOrdersGetByLocation,
                                                new
                                                {
                                                    Location = location,
                                                    Page = page,
                                                    rowCount = rowCount,
                                                },
                                                connectionStringName,
                                                true);

            return output;
        }

        /* GINs */

        public async Task<int> GINCreate(GINModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.GINCreate,
                                             new
                                             {
                                                 OrderID = model.OrderID,
                                                 OrderType = model.OrderType,
                                                 CustomerID = model.CustomerID,
                                                 EntryDate = model.EntryDate,
                                                 PurchaseOrderNumber = model.PurchaseOrderNumber,
                                                 ShipName = model.ShipName,
                                                 ShipAddress = model.ShipAddress,
                                                 ShipCity = model.ShipCity,
                                                 ShipStateOrProvince = model.ShipStateOrProvince,
                                                 ShipCountry = model.ShipCountry,
                                                 ShipPhoneNumber = model.ShipPhoneNumber,
                                                 ShipContact = model.ShipContact,
                                                 Printed = model.Printed,
                                                 Notes = model.Notes,
                                                 SShipEmail = model.SShipEmail,
                                                 NPosted = model.NPosted,
                                                 SLocationID = model.SLocationID,
                                                 STranNo = model.STranNo,
                                                 DTranNo = model.DTranNo,
                                                 NComplete = model.NComplete,
                                                 SLinkedTranNo = model.SLinkedTranNo,
                                                 STerminal = model.STerminal,
                                                 SBillAddr = model.SBillAddr,
                                                 SBillCity = model.SBillCity,
                                                 SBillProvince = model.SBillProvince,
                                                 SBillCountry = model.SBillCountry,
                                                 SBillPhoneNo = model.SBillPhoneNo,
                                                 SBillEmail = model.SBillEmail,
                                                 SBillContact = model.SBillContact,
                                                 NOnHold = model.NOnHold,
                                                 DCustOpenBalance = model.DCustOpenBalance,
                                                 SIDNo = model.SIDNo,
                                                 STransporter = model.STransporter,
                                                 SVehicleNumber = model.SVehicleNumber,
                                                 Depot = model.Depot,
                                                 DOPreparedBy = model.DOPreparedBy,
                                                 GINStatus = model.GINStatus,
                                                 GINSource = model.GINSource,
                                                 GINModifyBy = model.GINModifyBy,
                                                 GINModifyDate = model.GINModifyDate,
                                                 ItemNum = model.ItemNum,
                                                 ItemDescr = model.ItemDescr,
                                                 Quantity = model.Quantity,
                                                 UnitPrice = model.UnitPrice,
                                                 UnitType = model.UnitType,
                                                 LineTotal = model.LineTotal,
                                                 LineVat = model.LineVat,
                                                 SQTYOrdered = model.SQtyOrdered,
                                                 DQtyBO = model.DQtyBO,
                                                 LAdditionalNumber01 = model.LAdditionalNumber01,
                                                 GINType = model.GINType,
                                                 GINTransactionType = model.GINTransactionType,
                                                 GINWeightSource = model.GINWeightSource,
                                                 GinBagType = model.GinBagType
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task<int> GINDetailsCreate(GINDetailsModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.GINDetailsCreate,
                                             new
                                             {
                                                 OrderID = model.OrderID,
                                                 OrderType = model.OrderType,
                                                 OrderDetailID = model.OrderDetailID,
                                                 ItemNum = model.ItemNum,
                                                 ItemDescr = model.ItemDescr,
                                                 Quantity = model.Quantity,
                                                 UnitPrice = model.UnitPrice,
                                                 UnitType = model.UnitType,
                                                 Discount = model.Discount,
                                                 TaxRate = model.TaxRate,
                                                 AltItem = model.AltItem,
                                                 LineTotal = model.LineTotal,
                                                 LineVat = model.LineVat,
                                                 SFMTItemNo = model.SFMTItemNo,
                                                 SQTYOrdered = model.SQtyOrdered,
                                                 NDiscType = model.NDiscType,
                                                 SCatCode = model.SCatCode,
                                                 DQtyBO = model.DQtyBO,
                                                 LCNReasonID = model.LCNReasonID,
                                                 LCNComment = model.LCNComment,
                                                 LType = model.LType,
                                                 SSerialNo = model.SSerialNo,
                                                 SComment = model.SComment,
                                                 SSalesmanNo = model.SSalesmanNo,
                                                 SComboNo = model.SComboNo,
                                                 LCompID = model.LCompID,
                                                 SLocationID = model.SLocationID,
                                                 SPricelist = model.SPricelist,
                                                 LCostType = model.LCostType,
                                                 DUnitCost = model.DUnitCost,
                                                 LDecimals = model.LDecimals,
                                                 DUnitWeight = model.DUnitWeight,
                                                 NTaxIncl = model.NTaxIncl,
                                                 DQtyIBT = model.DQtyIBT,
                                                 SPriceUnit = model.SPriceUnit,
                                                 DPriceUnitConv = model.DPriceUnitConv,
                                                 SShipUnit = model.SShipUnit,
                                                 DShipUnitConv = model.DShipUnitConv,
                                                 DNonStockCost = model.DNonStockCost,
                                                 LLineParent = model.LLineParent,
                                                 LLineChild = model.LLineChild,
                                                 DQtySupplied = model.DQtySupplied,
                                                 LSupplyAction = model.LSupplyAction,
                                                 DPriceUnitPrice = model.DPriceUnitPrice,
                                                 NDeleted = model.NDeleted,
                                                 DQtySuppliedInv = model.DQtySuppliedInv,
                                                 DValueA = model.DValueA,
                                                 DValueB = model.DValueB,
                                                 DValueC = model.DValueC,
                                                 DValueD = model.DValueD,
                                                 STaxCode = model.STaxCode,
                                                 SPromoID = model.SPromoID,
                                                 SPromoDescr = model.SPromoDescr,
                                                 LAdditionalNumber01 = model.LAdditionalNumber01,
                                                 NPriceByWeight = model.NPriceByWeight,
                                                 DTotalWeight = model.DTotalWeight,
                                                 GINStatus = model.GINStatus,
                                                 GINSource = model.GINSource
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task GINUpdate(GINModel model)
        {
            await _db.SaveDataAsync(_sp.GINUpdate,
                                           new
                                           {
                                               Id = model.STranNo,
                                               Status = model.GINStatus
                                           },
                                           connectionStringName,
                                           true);
        }

        public async Task GINDetailsUpdate(GINDetailsModel model)
        {
            await _db.SaveDataAsync(_sp.GINUpdate,
                                              new
                                              {
                                                  OrderID = model.OrderID,
                                                  OrderType = model.OrderType,
                                                  OrderDetailID = model.OrderDetailID,
                                                  ItemNum = model.ItemNum,
                                                  ItemDescr = model.ItemDescr,
                                                  Quantity = model.Quantity,
                                                  UnitPrice = model.UnitPrice,
                                                  UnitType = model.UnitType,
                                                  Discount = model.Discount,
                                                  TaxRate = model.TaxRate,
                                                  AltItem = model.AltItem,
                                                  LineTotal = model.LineTotal,
                                                  LineVat = model.LineVat,
                                                  SFMTItemNo = model.SFMTItemNo,
                                                  SQTYOrdered = model.SQtyOrdered,
                                                  NDiscType = model.NDiscType,
                                                  SCatCode = model.SCatCode,
                                                  DQtyBO = model.DQtyBO,
                                                  LCNReasonID = model.LCNReasonID,
                                                  LCNComment = model.LCNComment,
                                                  LType = model.LType,
                                                  SSerialNo = model.SSerialNo,
                                                  SComment = model.SComment,
                                                  SSalesmanNo = model.SSalesmanNo,
                                                  SComboNo = model.SComboNo,
                                                  LCompID = model.LCompID,
                                                  SLocationID = model.SLocationID,
                                                  SPricelist = model.SPricelist,
                                                  LCostType = model.LCostType,
                                                  DUnitCost = model.DUnitCost,
                                                  LDecimals = model.LDecimals,
                                                  DUnitWeight = model.DUnitWeight,
                                                  NTaxIncl = model.NTaxIncl,
                                                  DQtyIBT = model.DQtyIBT,
                                                  SPriceUnit = model.SPriceUnit,
                                                  DPriceUnitConv = model.DPriceUnitConv,
                                                  SShipUnit = model.SShipUnit,
                                                  DShipUnitConv = model.DShipUnitConv,
                                                  DNonStockCost = model.DNonStockCost,
                                                  LLineParent = model.LLineParent,
                                                  LLineChild = model.LLineChild,
                                                  DQtySupplied = model.DQtySupplied,
                                                  LSupplyAction = model.LSupplyAction,
                                                  DPriceUnitPrice = model.DPriceUnitPrice,
                                                  NDeleted = model.NDeleted,
                                                  DQtySuppliedInv = model.DQtySuppliedInv,
                                                  DValueA = model.DValueA,
                                                  DValueB = model.DValueB,
                                                  DValueC = model.DValueC,
                                                  DValueD = model.DValueD,
                                                  STaxCode = model.STaxCode,
                                                  SPromoID = model.SPromoID,
                                                  SPromoDescr = model.SPromoDescr,
                                                  LAdditionalNumber01 = model.LAdditionalNumber01,
                                                  NPriceByWeight = model.NPriceByWeight,
                                                  DTotalWeight = model.DTotalWeight,
                                                  GINStatus = model.GINStatus,
                                                  GINSource = model.GINSource
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task GINUpdateStatus(GINModel model)
        {
            await _db.SaveDataAsync(_sp.GINUpdateStatus,
                                           new
                                           {
                                               Id = model.STranNo,
                                               Status = model.NComplete
                                           },
                                           connectionStringName,
                                           true);
        }

        public async Task<List<GINModel>> GINGetAll(TransactionFilter model)
        {
            List<GINModel> output = new List<GINModel>();
            output = await _db.LoadDataAsync<GINModel, dynamic>(_sp.GINGetAll,
                                                 new
                                                 {
                                                     shouldFilter = model.shouldFilter,
                                                     dateFilter = model.dateFilter,
                                                     locationFilter = model.locationFilter,
                                                     startIndex = model.StartIndex,
                                                     batchSize = model.BatchSize,
                                                     StartDate = model.StartDate,
                                                     EndDate = model.EndDate,
                                                     location = model.location
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<GINModel>> GINGetAllReport(TransactionFilter model)
        {
            List<GINModel> output = new List<GINModel>();
            output = await _db.LoadDataAsync<GINModel, dynamic>(_sp.GINGetAllReport,
                                                 new
                                                 {
                                                     shouldFilter = model.shouldFilter,
                                                     dateFilter = model.dateFilter,
                                                     locationFilter = model.locationFilter,
                                                     startIndex = model.StartIndex,
                                                     batchSize = model.BatchSize,
                                                     StartDate = model.StartDate,
                                                     EndDate = model.EndDate,
                                                     location = model.location,
                                                     deliveryOrderNo = model.DeliveryOrderNo
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<GINModel>> GINGetAllExcel(TransactionFilter model)
        {
            List<GINModel> output = new List<GINModel>();
            output = await _db.LoadDataAsync<GINModel, dynamic>(_sp.GINGetAllExcel,
                                                 new
                                                 {
                                                     shouldFilter = model.shouldFilter,
                                                     dateFilter = model.dateFilter,
                                                     locationFilter = model.locationFilter,
                                                     startIndex = model.StartIndex,
                                                     batchSize = model.BatchSize,
                                                     StartDate = model.StartDate,
                                                     EndDate = model.EndDate,
                                                     location = model.location,
                                                     deliveryOrderNo = model.DeliveryOrderNo
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<GINModel>> GINGetByLocation(string location, int page = 1, int rowCount = 250)
        {
            List<GINModel> output = new List<GINModel>();
            output = await _db.LoadDataAsync<GINModel, dynamic>(_sp.GINGetByLocation,
                                                new
                                                {
                                                    Location = location,
                                                    Page = page,
                                                    rowCount = rowCount
                                                },
                                                connectionStringName,
                                                true);

            return output;
        }

        public async Task<List<GINModel>> GINGetById(string sTranNo)
        {
            List<GINModel> output = new List<GINModel>();
            output = await _db.LoadDataAsync<GINModel, dynamic>(_sp.PRCNNGetById,
                                                 new
                                                 {
                                                     sTranNo = sTranNo,
                                                 },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task<List<GINDetailsModel>> GINDetailsGetByLocation(string location, int page = 1, int rowCount = 250)
        {
            List<GINDetailsModel> output = new List<GINDetailsModel>();
            output = await _db.LoadDataAsync<GINDetailsModel, dynamic>(_sp.GINDetailsGetByLocation,
                                                new
                                                {
                                                    Location = location,
                                                    Page = page,
                                                    rowCount = rowCount
                                                },
                                                connectionStringName,
                                                true);

            return output;
        }


        /* Delivery Orders */

        public async Task<List<DeliveryOrdersModel>> DeliveryOrdersGetByLocation(string location, int page = 1, int rowCount = 250)
        {
            List<DeliveryOrdersModel> output = new List<DeliveryOrdersModel>();
            output = await _db.LoadDataAsync<DeliveryOrdersModel, dynamic>(_sp.DeliveryOrdersGetByLocation,
                                                new
                                                {
                                                    Location = location,
                                                    Page = page,
                                                    rowCount = rowCount,
                                                },
                                                connectionStringName,
                                                true);

            return output;
        }

        public async Task<List<DeliveryOrdersDetailsModel>> DeliveryOrdersDetailsGetByLocation(string location, int page = 1, int rowCount = 250)
        {
            List<DeliveryOrdersDetailsModel> output = new List<DeliveryOrdersDetailsModel>();
            output = await _db.LoadDataAsync<DeliveryOrdersDetailsModel, dynamic>(_sp.DeliveryOrdersDetailsGetByLocation,
                                                new
                                                {
                                                    Location = location,
                                                    Page = page,
                                                    rowCount = rowCount,
                                                },
                                                connectionStringName,
                                                true);

            return output;
        }

        /*App Data*/
        public async Task<int> AppDataCreate(AppDataModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.AppDataCreate,
                                             new
                                             {
                                                 Link = model.Link,
                                                 AppVersion = model.AppVersion,
                                                 ChangeLog = model.ChangeLog
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task AppDataUpdate(AppDataModel model)
        {
            await _db.SaveDataAsync(_sp.AppDataUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 AppVersion = model.AppVersion,
                                                 Link = model.Link,
                                                 ChangeLog = model.ChangeLog
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task AppDataDelete(int Id)
        {
            await deleteValue(_sp.AppDataDelete, Id);
        }

        public async Task<AppDataModel> AppDataGet(int Id)
        {
            List<AppDataModel> output = new List<AppDataModel>();
            output = await _db.LoadDataAsync<AppDataModel, dynamic>(_sp.AppDataGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<AppDataModel> AppDataGetAll()
        {
            List<AppDataModel> output = new List<AppDataModel>();
            output = await _db.LoadDataAsync<AppDataModel, dynamic>(_sp.AppDataGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        /* PAGINATION DATA */
        public async Task<PaginationData> PaginationData(PaginationData model)
        {
            List<PaginationData> output = new List<PaginationData>();
            output = await _db.LoadDataAsync<PaginationData, dynamic>(_sp.PaginationData,
                                                 new
                                                 {
                                                     tablename = model.tablename,
                                                     Column = model.Column,
                                                     Id = model.Id,
                                                     rowCount = model.rowCount
                                                 },
                                                 connectionStringName,
                                                 true);
            return output.FirstOrDefault();
        }

        public async Task<PaginationData> PaginationDataSrpos(PaginationData model)
        {
            List<PaginationData> output = new List<PaginationData>();
            output = await _db.LoadDataAsync<PaginationData, dynamic>(_sp.PaginationData,
                                                 new
                                                 {
                                                     tablename = model.tablename,
                                                     Column = model.Column,
                                                     Id = model.Id,
                                                     rowCount = model.rowCount
                                                 },
                                                 connectionStringNameSrpos,
                                                 true);
            return output.FirstOrDefault();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////// 
        private async Task deleteValue(string sp, int Id)
        {
            await _db.SaveDataAsync(sp,
                                             new
                                             {
                                                 Id = Id
                                             },
                                             connectionStringName,
                                             true);
        }

        public static string CreateBasicAuthToken(string username, string password)
        {
            // Concatenate username and password with a colon
            string credentials = $"{username}:{password}";

            // Convert the credentials string to a byte array
            byte[] byteCredentials = Encoding.UTF8.GetBytes(credentials);

            // Encode the byte array to a Base64 string
            string base64Credentials = Convert.ToBase64String(byteCredentials);

            // Prepend "Basic " to the Base64 string
            string token = $"{base64Credentials}";

            return token;
        }

        // Device Authentication
        public async Task<DevicesAuthModel> DevicesAuthentication(string username, string password)
        {
            DevicesAuthModel output = new DevicesAuthModel();
            var apiClient = new ApiClient("http://41.173.23.214:4560");

            try
            {
                // Specify the endpoint for device authentication
                string endpoint = "/fra/v1/e-payments/DeviceServices/auth";

                // Create an object with authentication data to send in the POST request
                var authData = new
                {
                    Username = username,
                    Password = password
                };

                string token = CreateBasicAuthToken(username, password);

                // Serialize the authentication data object to JSON
                string requestData = JsonConvert.SerializeObject(authData);

                // Make a POST request to authenticate the device
                string responseJson = await apiClient.PostAsync(endpoint, requestData, token);

                // Deserialize the JSON response into a DevicesAuthModel object
                DevicesAuthModel deviceAuth = JsonConvert.DeserializeObject<DevicesAuthModel>(responseJson);

                if (deviceAuth.Success && deviceAuth.Status == 0)
                {
                    await _db.SaveDataAsync(_sp.AuthUpdate,
                                             new
                                             {
                                                 Token = deviceAuth.Data.Token
                                             },
                                             connectionStringName,
                                             true);

                }
                deviceAuth.Message = token;
                output = deviceAuth;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Dispose of the HttpClient instance
                apiClient.Dispose();
            }

            return output;
        }

        public async Task<ResponseModelKyc> FarmerKyc(string token, FarmerKycModel model)
        {
            ResponseModelKyc output = new ResponseModelKyc();
            var apiClient = new ApiClient("http://41.173.23.214:4560");

            try
            {
                // Specify the endpoint for device authentication
                string endpoint = "/fra/v1/e-payments/auth/Device/UserServices/KycValidate";

                var authData = new
                {
                    mobile_number = model.Mobile_Number,
                    nrc_number = model.Nrc_Number,
                    first_name = model.First_Name,
                    other_name = model.Other_Name,
                    last_name = model.Last_Name,
                    date_of_birth = model.Date_Of_Birth,
                    gender = model.Gender,
                    nationality = model.Nationality,
                    depot_code = model.Depot_Code,
                    payment_provider_id = model.Payment_Provider_Id,
                    bank_branch_id = model.Bank_Branch_Id,
                    account_number = model.Account_Number
                };

                // Serialize the authentication data object to JSON
                string requestData = JsonConvert.SerializeObject(authData);

                // Make a POST request to authenticate the device
                string responseJson = await apiClient.PostAsyncBearer(endpoint, requestData, token);

                // Deserialize the JSON response into a DevicesAuthModel object
                ResponseModelKyc deviceAuth = JsonConvert.DeserializeObject<ResponseModelKyc>(responseJson);


                output = deviceAuth;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Dispose of the HttpClient instance
                apiClient.Dispose();
            }

            return output;
        }

        public async Task<PostBCLModelResponse> BCLCreate(BCLModel model, string token)
        {
            PostBCLModelResponse output = new PostBCLModelResponse();
            var apiClient = new ApiClient("http://41.173.23.214:4560");

            try
            {
                // Specify the endpoint for device authentication
                string endpoint = "/fra/v1/e-payments/auth/Device/BCLServices/CreateBCL";

                var authData = new
                {
                    reference = model.sTranNo,
                    count = 1,
                    province_code = model.PROVINCE.Trim(),
                    total_amount = model.dDocTotal.ToString()
                };

                Debug.WriteLine("authData: " + authData);
                Console.WriteLine("authData: " + authData);

                // Serialize the authentication data object to JSON
                string requestData = JsonConvert.SerializeObject(authData);

                Debug.WriteLine("requestData: " + requestData);

                // Make a POST request to authenticate the device
                string responseJson = await apiClient.PostAsyncBearer(endpoint, requestData, token);

                Debug.WriteLine("responseJson: " + responseJson);
                Console.WriteLine("responseJson: " + responseJson);

                // Deserialize the JSON response into a DevicesAuthModel object
                PostBCLModelResponse deviceAuth = JsonConvert.DeserializeObject<PostBCLModelResponse>(responseJson);
                Debug.WriteLine("deviceAuth: " + deviceAuth);

                output = deviceAuth;
                output.steps = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                output.steps = 0;
            }
            finally
            {
                // Dispose of the HttpClient instance
                apiClient.Dispose();
            }

            return output;
        }

        public async Task<PostBCLModelResponse> BCLCreateAttach(BCLModel model, PRCNModel prcn, DevicesModel devices, FarmerModel farmer, string token)
        {
            PostBCLModelResponse output = new PostBCLModelResponse();
            var apiClient = new ApiClient("http://41.173.23.214:4560");

            try
            {
                // Specify the endpoint for device authentication
                string endpoint = "/fra/v1/e-payments/auth/Device/BCLServices/AttachToBCL";

                /*PRCNModel prcn = await PRCNNGet(model.sTranNo);
                Console.WriteLine(prcn);

                DevicesModel devices = await DevicesGetByDoc(prcn.SATELITE, prcn.terminal);

                FarmerModel farmer = await FarmersGetFromNrc(model.sVendorID);
                */
                var authData = new
                {
                    reference = model.sTranNo.Trim(),
                    serial_number = devices.SerialNumber.Trim(),
                    prcn_number = model.rcpno.Trim(),
                    farmer_name = model.sVendorName.Trim(),
                    farmer_nrc = model.sVendorID.Trim(),
                    account_number = farmer.AccountNumber.Trim(),
                    payment_provider_id = farmer.PaymentProviderId.Trim(),
                    bank_branch_id = farmer.BankBranchId.Trim(),
                    number_of_bags = int.Parse(model.NoOfBags.ToString()),
                    amount = model.dDocTotal.ToString()
                };

                // Serialize the authentication data object to JSON
                string requestData = JsonConvert.SerializeObject(authData);

                // Make a POST request to authenticate the device
                string responseJson = await apiClient.PostAsyncBearer(endpoint, requestData, token);

                Console.WriteLine(responseJson);

                // Deserialize the JSON response into a DevicesAuthModel object
                PostBCLModelResponse deviceAuth = JsonConvert.DeserializeObject<PostBCLModelResponse>(responseJson);


                output = deviceAuth;
                output.steps = 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Dispose of the HttpClient instance
                apiClient.Dispose();
            }

            return output;
        }

        public async Task<List<DataModelPay>> GetPaymentProviderApp(int page = 1, int rowCount = 250)
        {
            List<DataModelPay> output = new List<DataModelPay>();
            output = await _db.LoadDataAsync<DataModelPay, dynamic>(_sp.GetPaymentProviderApp,
                                                new
                                                {
                                                    Page = page,
                                                    rowCount = rowCount
                                                },
                                                connectionStringName,
                                                true);

            return output;
        }

        public async Task<List<Branches>> GetBranchesApp(int page = 1, int rowCount = 250)
        {
            List<Branches> output = new List<Branches>();
            output = await _db.LoadDataAsync<Branches, dynamic>(_sp.GetBranchesApp,
                                                new
                                                {
                                                    Page = page,
                                                    rowCount = rowCount
                                                },
                                                connectionStringName,
                                                true);

            return output;
        }

        public async Task<PaymentProviderModel> GetPaymentProviders(string token)
        {
            PaymentProviderModel output = new PaymentProviderModel();
            var apiClient = new ApiClient("http://41.173.23.214:4560");

            try
            {
                // Specify the endpoint for retrieving payment providers
                string endpoint = "/fra/v1/e-payments/auth/Device/PaymentServices/getPaymentProviders";

                // Make a GET request to retrieve payment providers
                string responseJson = await apiClient.GetAsync(endpoint, token);

                // Deserialize the JSON response into a PaymentProviderModel object
                PaymentProviderModel providers = JsonConvert.DeserializeObject<PaymentProviderModel>(responseJson);

                output = providers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Dispose of the HttpClient instance
                apiClient.Dispose();
            }

            return output;
        }

        public async Task<int> PaymentProviderCreate(DataModelPay model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.PaymentProviderCreate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Name = model.Name,
                                                 Type = model.Type
                                             },
                                             connectionStringName,
                                             true);

            return output;
        }

        public async Task<int> BranchCreate(Branches model, int providerId)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.BranchCreate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Name = model.Name,
                                                 PaymentProviderId = providerId
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task<AuthorizationModel> GetAuthorization()
        {
            List<AuthorizationModel> output = new List<AuthorizationModel>();
            output = await _db.LoadDataAsync<AuthorizationModel, dynamic>(_sp.AuthGet,
                                                new
                                                { },
                                                connectionStringName,
                                                true);

            return output.FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure_Layer.Services
{
    public class StoredProcedures
    {
        public static string baseValue = "dbo.";

        //Province
        public string ProvinceCreate = baseValue + "ProvinceCreate";
        public string ProvinceUpdate = baseValue + "ProvinceUpdate";
        public string ProvinceDelete = baseValue + "ProvinceDelete";
        public string ProvinceGet = baseValue + "ProvinceGet";
        public string ProvinceGetAll = baseValue + "ProvinceGetAll";

        //District
        public string DistrictCreate = baseValue + "DistrictCreate";
        public string DistrictUpdate = baseValue + "DistrictUpdate";
        public string DistrictDelete = baseValue + "DistrictDelete";
        public string DistrictGet = baseValue + "DistrictGet";
        public string DistrictGetFromProvince = baseValue + "DistrictGetFromProvince";
        public string DistrictGetAll = baseValue + "DistrictGetAll";

        //Satelite
        public string SateliteCreate = baseValue + "SateliteCreate";
        public string SateliteUpdate = baseValue + "SateliteUpdate";
        public string SateliteDelete = baseValue + "SateliteDelete";
        public string SateliteGet = baseValue + "SateliteGet";
        public string SateliteGetFromDistrict = baseValue + "SateliteGetFromDistrict";
        public string SateliteGetAll = baseValue + "SateliteGetAll";

        //roles
        public string RolesCreate = baseValue + "RolesCreate";
        public string RolesUpdate = baseValue + "RolesUpdate";
        public string RolesDelete = baseValue + "RolesDelete";
        public string RolesGet = baseValue + "RolesGet";
        public string RolesGetAll = baseValue + "RolesGetAll";
        public string RolesPermissionsGet = baseValue + "RolesPermissionsGet";
        public string RolesPermissionsGetAll = baseValue + "RolesPermissionsGetAll";

        //Admins
        public string AdminsCreate = baseValue + "AdminsCreate";
        public string AdminsUpdate = baseValue + "AdminsUpdate";
        public string AdminsDelete = baseValue + "AdminsDelete";
        public string AdminsGet = baseValue + "AdminsGet";
        public string AdminsGetByAdminId = baseValue + "AdminsGetByAdminId";
        public string AdminGetByNrc = baseValue + "AdminGetByNrc";
        public string AdminsGetAll = baseValue + "AdminsGetAll";

        //Devices
        public string DevicesCreate = baseValue + "DevicesCreate";
        public string DevicesUpdate = baseValue + "DevicesUpdate";
        public string DevicesDelete = baseValue + "DevicesDelete";
        public string DevicesGet = baseValue + "DevicesGet";
        public string DevicesUpdatePrcn = baseValue + "DevicesUpdatePrcn";
        public string DevicesGetByLocation = baseValue + "DevicesGetByLocation";
        public string DevicesGetByDoc = baseValue + "DevicesGetByDoc";
        public string DeviceSync = baseValue + "DeviceSync";
        public string DevicesGetStatus = baseValue + "DevicesGetStatus";
        public string DevicesGetBySateliteId = baseValue + "DevicesGetBySateliteId";
        public string DevicesUpdateLastsync = baseValue + "DevicesUpdateLastsync";
        public string DevicesGetAll = baseValue + "DevicesGetAll";
        public string DevicesGetCount = baseValue + "DevicesGetCount";
        public string DevicesGetBySerialNumber = baseValue + "DevicesGetBySerialNumber";

        //DeviceStatus
        public string DeviceStatusCreate = baseValue + "DeviceStatusCreate";
        public string DeviceStatusUpdate = baseValue + "DeviceStatusUpdate";
        public string DeviceStatusDelete = baseValue + "DeviceStatusDelete";
        public string DeviceStatusGet = baseValue + "DeviceStatusGet";
        public string DeviceStatusGetAll = baseValue + "DeviceStatusGetAll";

        //AccountStatus
        public string AccountStatusCreate = baseValue + "AccountStatusCreate";
        public string AccountStatusUpdate = baseValue + "AccountStatusUpdate";
        public string AccountStatusDelete = baseValue + "AccountStatusDelete";
        public string AccountStatusGet = baseValue + "AccountStatusGet";
        public string AccountStatusGetAll = baseValue + "AccountStatusGetAll";

        //Users
        public string UsersCreate = baseValue + "UsersCreate";
        public string UsersUpdate = baseValue + "UsersUpdate";
        public string UsersDelete = baseValue + "UsersDelete";
        public string UsersGet = baseValue + "UsersGet";
        public string UsersGetByEmail = baseValue + "UsersGetByEmail";
        public string UsersGetAll = baseValue + "UsersGetAll";
        public string UsersResetPassword = baseValue + "UsersResetPassword";
        public string UsersDeviceLogin = baseValue + "UsersDeviceLogin";
        public string UsersDeviceRegistrationLogin = baseValue + "UsersDeviceRegistrationLogin";
        public string UsersPortalLogin = baseValue + "UsersPortalLogin";

        //CropCategory
        public string CropCategoryCreate = baseValue + "CropCategoryCreate";
        public string CropCategoryUpdate = baseValue + "CropCategoryUpdate";
        public string CropCategoryDelete = baseValue + "CropCategoryDelete";
        public string CropCategoryGet = baseValue + "CropCategoryGet";
        public string CropCategoryGetAll = baseValue + "CropCategoryGetAll";

        //Crops
        public string CropsCreate = baseValue + "CropsCreate";
        public string CropsUpdate = baseValue + "CropsUpdate";
        public string CropsDelete = baseValue + "CropsDelete";
        public string CropsGet = baseValue + "CropsGet";
        public string CropsGetAll = baseValue + "CropsGetAll";
        public string GetAllCrops = baseValue + "GetAllCrops";
        public string CropsGetByName = baseValue + "CropsGetByName";

        //Commercial Crops
        public string CropsCommercialCreate = baseValue + "CropsCommercialCreate";
        public string CropsCommercialUpdate = baseValue + "CropsCommercialUpdate";
        public string CropsCommercialDelete = baseValue + "CropsCommercialDelete";
        public string CropsCommercialGet = baseValue + "CropsCommercialGet";
        public string CropsCommercialGetAll = baseValue + "CropsCommercialGetAll";
        public string GetAllCropsCommercial = baseValue + "GetAllCropsCommercial";
        public string CropsCommercialGetByName = baseValue + "CropsCommercialGetByName";

        //Farmer
        public string FarmerCreate = baseValue + "FarmersCreate";
        public string FarmerUpdate = baseValue + "FarmersUpdate";
        public string FarmerCreatev2 = baseValue + "FarmersCreatev2";
        public string FarmerUpdatev2 = baseValue + "FarmersUpdatev2";
        public string FarmerDelete = baseValue + "FarmersDelete";
        public string FarmerGet = baseValue + "FarmersGet";
        public string FarmerGetByLocation = baseValue + "FarmerGetByLocation";
        public string FarmerGetAll = baseValue + "FarmersGetAll";
        public string FarmersGetFromNrc = baseValue + "FarmersGetFromNrc";

        //Commercial Farmer
        public string FarmerCommercialCreate = baseValue + "FarmersCommercialCreate";
        public string FarmerCommercialUpdate = baseValue + "FarmersCommercialUpdate";
        public string FarmerCommercialDelete = baseValue + "FarmersCommercialDelete";
        public string FarmerCommercialGet = baseValue + "FarmersCommercialGet";
        public string FarmerCommercialGetByLocation = baseValue + "FarmerCommercialGetByLocation";
        public string FarmerCommercialGetAll = baseValue + "FarmersCommercialGetAll";
        public string FarmersCommercialGetFromCode = baseValue + "FarmersCommercialGetFromCode";


        //Staff
        public string StaffCreate = baseValue + "StaffCreate";
        public string StaffUpdate = baseValue + "StaffUpdate";
        public string StaffDelete = baseValue + "StaffDelete";
        public string StaffGet = baseValue + "StaffGet";
        public string StaffGetByLocation = baseValue + "StaffGetByLocation";
        public string StaffGetByNrc = baseValue + "StaffGetByNrc";
        public string StaffGetAll = baseValue + "StaffGetAll";

        //Transactions
        public string TransactionsCreate = baseValue + "TransactionsCreate";
        public string TransactionsUpdate = baseValue + "TransactionsUpdate";
        public string TransactionsDelete = baseValue + "TransactionsDelete";
        public string TransactionsGet = baseValue + "TransactionsGet";
        public string TransactionsGetByPRCN = baseValue + "TransactionsGetByPRCN";
        public string TransactionsGetAll = baseValue + "TransactionsGetAll";

        //PRCN
        public string PRCNNCreate = baseValue + "PRCNNCreate";
        public string PRCNCreate = baseValue + "PRCNCreate";
        public string PRCNNGet = baseValue + "PRCNNGet";
        public string BCLGet = baseValue + "BCLGet";
        public string BCLGetBulk = baseValue + "BCLGetBulk";
        public string BCLGetAllPending = baseValue + "BCLGetAllPending";
        public string BCLGetAllPosted = baseValue + "BCLGetAllPosted";
        public string BCLUpdateAPI = baseValue + "BCLUpdateAPI";
        public string PRCNGetAll = baseValue + "PRCNGetAll";
        public string BCLGetAll = baseValue + "BCLGetAll";
        public string PRCNUpdate = baseValue + "PRCNUpdate";
        public string BCLUpdate = baseValue + "BCLUpdate";
        public string PRCNGetByLocation = baseValue + "PRCNGetByLocation";
        public string PRCNNGetById = baseValue + "PRCNNGetById";

        //
        public string NetProvinceGet = baseValue + "NetProvinceGet";
        public string NetProvinceGetByCode = baseValue + "NetProvinceGetByCode";
        public string NetSateliteGet = baseValue + "NetSateliteGet";
        public string NetDistrictGet = baseValue + "NetDistrictGet";
        public string NetDistrictGetByName = baseValue + "NetDistrictGetByName";
        public string NetLocationGet = baseValue + "NetLocationGet";


        //stats 
        public string DeviceStats = baseValue + "DeviceStats";
        public string DashboardStats = baseValue + "DashboardStats";
        public string TransactionStats = baseValue + "TransactionStats";
        public string TransactionStatsByDistrict = baseValue + "TransactionStatsByDistrict";
        public string TransactionStatsByYear = baseValue + "TransactionStatsByYear";
        public string TransactionStatsByWeek = baseValue + "TransactionStatsByWeek";
        public string TransactionStatsBySatelietSales = baseValue + "TransactionStatsBySatelietSales";
        public string TransactionStatsByCropsSold = baseValue + "TransactionStatsByCropsSold";
        public string SalesStats = baseValue + "SalesStats";
        public string SalesStatsByDistrict = baseValue + "SalesStatsByDistrict";


        //IDT
        public string IDTCreate = baseValue + "IDTCreate";
        public string IDTUpdate = baseValue + "IDTUpdate";
        public string IDTUpdateStatus = baseValue + "IDTUpdateStatus";
        public string IDTGetByLocation = baseValue + "IDTGetByLocation";
        public string IDTGetAll = baseValue + "IDTGetAll";
        public string GRNCreate = baseValue + "GRNCreate";
        public string GRNUpdate = baseValue + "GRNUpdate";
        public string GRNUpdateStatus = baseValue + "GRNUpdateStatus";
        public string GRNGetByLocation = baseValue + "GRNGetByLocation";
        public string GRNGetAll = baseValue + "GRNGetAll";
        public string LoadingOrdersGetByLocation = baseValue + "LoadingOrdersGetByLocation";
        public string LoadingOrdersGetAll = baseValue + "LoadingOrdersGetAll";


        //GIN
        public string GINCreate = baseValue + "GINCreate";
        public string GINDetailsCreate = baseValue + "GINDetailsCreate";
        public string GINUpdate = baseValue + "GINUpdate";
        public string GINDetailsUpdate = baseValue + "GINDetailsUpdate";
        public string GINUpdateStatus = baseValue + "GINUpdateStatus";
        public string GINGetByLocation = baseValue + "GINGetByLocation";
        public string GINDetailsGetByLocation = baseValue + "GINDetailsGetByLocation";
        public string GINGetById = baseValue + "GINGetById";
        public string GINGetAll = baseValue + "GINGetAll";
        public string GINGetAllReport = baseValue + "GINGetAllReport";
        public string GINGetAllExcel = baseValue + "GINGetAllExcel";


        //Delivery Orders
        public string DeliveryOrdersGetByLocation = baseValue + "DeliveryOrdersGetByLocation";
        public string DeliveryOrdersDetailsGetByLocation = baseValue + "DeliveryOrdersDetailsGetByLocation";

        //Pagination
        public string PaginationData = baseValue + "PaginationData";

        //App Data
        public string AppDataCreate = baseValue + "AppDataCreate";
        public string AppDataUpdate = baseValue + "AppDataUpdate";
        public string AppDataGet = baseValue + "AppDataGet";
        public string AppDataGetAll = baseValue + "AppDataGetAll";
        public string AppDataDelete = baseValue + "AppDataDelete";

        //Payment Provider
        public string PaymentProviderCreate = baseValue + "PaymentProviderCreate";
        public string BranchCreate = baseValue + "BranchCreate";
        public string GetPaymentProviderApp = baseValue + "GetPaymentProviderApp";
        public string GetBranchesApp = baseValue + "GetBranchesApp";
        public string AuthUpdate = baseValue + "AuthUpdate";
        public string AuthGet = baseValue + "AuthGet";
    }
}

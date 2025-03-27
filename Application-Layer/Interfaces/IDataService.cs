using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Domain_Layer.Models.StatsModel;

namespace Application_Layer.Interfaces
{
    public interface IDataService
    {
        //roles
        Task<int> RolesCreate(RolesModel model);
        Task RolesUpdate(RolesModel model);
        Task RolesDelete(int Id);
        Task<RolesModel> RolesGet(int Id);
        Task<List<PermissionsModel>> RolesPermissionsGet(int Id);
        Task<List<PermissionsModel>> RolesPermissionsGetAll();
        Task<List<RolesModel>> RolesGetAll();

        //Devices
        Task<int> DevicesCreate(DevicesModel model);
        Task<int> DevicesUpdate(DevicesModel model);
        Task DevicesDelete(int Id);
        Task DevicesUpdateLastsync(int Id, DateTime lastSync);
        Task<DevicesModel> DevicesGet(int Id);
        Task<DevicesModel> DevicesGetByDoc(string Location, string Prefix);
        Task DevicesUpdatePrcn(DevicesModel model);
        Task<List<DevicesModel>> DevicesGetByLocation(string Id);
        Task<DevicesModel> DeviceSync(string SerialNumber);
        Task<List<DevicesModel>> DevicesGetBySateliteId(int Id);
        Task<DevicesModel> DevicesGetBySerialNumber(string SerialNumber);
        Task<DevicesModel> DevicesGetBySerialNumberCount(string SerialNumber);
        Task<List<DevicesModel>> DevicesGetAll();
        Task<int> DevicesGetCount();
        Task<DeviceStatusModel> DevicesGetStatus(int DeviceId);

        //DeviceStatus Status
        Task<int> DeviceStatusCreate(DeviceStatusModel model);
        Task DeviceStatusUpdate(DeviceStatusModel model);
        Task DeviceStatusDelete(int Id);
        Task<DeviceStatusModel> DeviceStatusGet(int Id);
        Task<List<DeviceStatusModel>> DeviceStatusGetAll();

        //AccountStatus Status
        Task<int> AccountStatusCreate(AccountStatusModel model);
        Task AccountStatusUpdate(AccountStatusModel model);
        Task AccountStatusDelete(int Id);
        Task<AccountStatusModel> AccountStatusGet(int Id);
        Task<List<AccountStatusModel>> AccountStatusGetAll();

        //Users
        Task<int> UsersCreate(UsersModel model);
        Task UsersUpdate(UsersModel model);
        Task UsersDelete(int Id);
        Task<int> UsersResetPassword(ResetPasswordModel model, string key = null);
        Task<UsersModel> UsersDeviceLogin(UsersModel model);
        Task<UsersModel> UsersDeviceRegistrationLogin(UsersModel model);
        Task<UsersModel> UsersPortalLogin(UsersModel model);
        Task<UsersModel> UsersGet(int Id);
        Task<UsersModel> UsersGetByEmail(string email);
        Task<List<UsersModel>> UsersGetAll();
        Task<bool> ActivateAccount(string securityKey);


        //CropCategory
        Task<int> CropCategoryCreate(CropCategoryModel model);
        Task CropCategoryUpdate(CropCategoryModel model);
        Task CropCategoryDelete(int Id);
        Task<CropCategoryModel> CropCategoryGet(int Id);
        Task<List<CropCategoryModel>> CropCategoryGetAll();

        //Crops
        Task<int> CropsCreate(CropsModel model);
        Task CropsUpdate(CropsModel model);
        Task CropsDelete(int Id);
        Task<CropsModel> CropsGet(int Id);
        Task<CropsModel> CropsGetByName(String Title);
        Task<List<CropsModel>> CropsGetAll();
        Task<List<CropsModel>> GetAllCrops();

        //Commercial Crops
        Task<int> CropsCommercialCreate(CropsModel model);
        Task CropsCommercialUpdate(CropsModel model);
        Task CropsCommercialDelete(int Id);
        Task<CropsModel> CropsCommercialGet(int Id);
        Task<CropsModel> CropsCommercialGetByName(String Title);
        Task<List<CropsModel>> CropsCommercialGetAll();
        Task<List<CropsModel>> GetAllCropsCommercial();

        //Farmer
        Task<int> FarmerCreate(FarmerModel model);
        Task<int> FarmerCreatev2(FarmerModel model);
        Task<int> FarmerUpdate(FarmerModel model);
        Task<int> FarmerUpdatev2(FarmerModel model);
        Task FarmerDelete(int Id);
        Task<FarmerModel> FarmerGet(int Id);
        Task<List<FarmerModel>> FarmerGetByLocation(string Id, int page = 1);
        Task<FarmerModel> FarmersGetFromNrc(string nrc);
        Task<List<FarmerModel>> FarmerGetAll();

        //Commercial Farmer
        Task<int> FarmerCommercialCreate(FarmerModel model);
        Task<int> FarmerCommercialUpdate(FarmerModel model);
        Task FarmerCommercialDelete(int Id);
        Task<FarmerModel> FarmerCommercialGet(int Id);
        Task<List<FarmerModel>> FarmerCommercialGetByLocation(string Id, int page = 1);
        Task<FarmerModel> FarmersCommercialGetFromCode(string code);
        Task<List<FarmerModel>> FarmerCommercialGetAll();

        //Staff
        Task<int> StaffCreate(StaffModel model);
        Task StaffUpdate(StaffModel model);
        Task StaffDelete(int Id);
        Task<StaffModel> StaffGet(int Id);
        Task<List<StaffModel>> StaffGetByLocation(string Id);
        Task<StaffModel> StaffGetByNrc(string Id);
        Task<List<StaffModel>> StaffGetAll();

        //Transactions
        Task<int> TransactionsCreate(TransactionsModel model);
        Task TransactionsUpdate(TransactionsModel model);
        Task TransactionsDelete(int Id);
        Task<TransactionsModel> TransactionsGet(int Id);
        Task<TransactionsModel> TransactionsGetByPRCN(string Id);
        Task<List<TransactionsModel>> TransactionsGetAll(TransactionFilter model);

        //PRCN
        Task<int> PRCNNCreate(PRCNModel model);
        Task<int> PRCNCreate(PRCNModel model);
        Task<PRCNModel> PRCNNGet(string Id);
        Task<BCLModel> BCLGet(string Id);
        Task<List<PRCNModel>> PRCNGetAll(TransactionFilter model);
        Task<List<BCLModel>> BCLGetAll(TransactionFilter model);
        Task<List<PRCNModel>> PRCNNGetById(string Id);
        Task<List<PRCNModel>> PRCNGetByLocation(string location, int page = 1, int rowCount = 100);
        Task PRCNUpdate(PRCNModel model);

        //Satelite
        Task<int> SateliteCreate(SateliteModel model);
        Task SateliteUpdate(SateliteModel model);
        Task SateliteDelete(int Id);
        Task<SateliteModel> SateliteGet(int Id);
        Task<List<SateliteModel>> SateliteGetAll();
        Task<List<SateliteModel>> SateliteGetFromDistrict(int Id);

        //District  
        Task<int> DistrictCreate(DistrictModel model);
        Task DistrictUpdate(DistrictModel model);
        Task DistrictDelete(int Id);
        Task<DistrictModel> DistrictGet(int Id);
        Task<List<DistrictModel>> DistrictGetAll();
        Task<List<DistrictModel>> DistrictGetFromProvince(int Id);

        //Province
        Task<int> ProvinceCreate(ProvinceModel model);
        Task ProvinceUpdate(ProvinceModel model);
        Task ProvinceDelete(int Id);
        Task<ProvinceModel> ProvinceGet(int Id);
        Task<List<ProvinceModel>> ProvinceGetAll();

        //Admins
        Task<int> AdminsCreate(AdminsModel model);
        Task AdminsUpdate(AdminsModel model);
        Task AdminsDelete(int Id);
        Task<AdminsModel> AdminsGet(int Id);
        Task<AdminsModel> AdminsGetByNrc(string Nrc);
        Task<List<AdminsModel>> AdminsGetAll();

        //stats 
        Task<DashboardStatsModel> DashboardStats(StatsFilterModel model);
        Task<DeviceStatsModel> DeviceStats();
        Task<TransactionStatsModel> TransactionStats();
        Task<TransactionStatsModel> TransactionStatsByDistrict(string DistrictCode);
        Task<List<StatsByYear>> TransactionStatsByYear(StatsFilterModel model);
        Task<List<StatsByWeek>> TransactionStatsByWeek(StatsFilterModel model);
        Task<List<StatsBySatelietSales>> TransactionStatsBySatelietSales(StatsFilterModel model);
        Task<List<StatsByByCropsSold>> TransactionStatsByCropsSold(StatsFilterModel model);
        Task<TransactionStatsModel> SalesStats();
        Task<TransactionStatsModel> SalesStatsByDistrict(string DistrictCode);

        //stats 
        Task<List<netPurLocationModel>> NetProvinceGet();
        Task<List<netPurLocationModel>> NetProvinceGetByCode(string ProvinceCode);
        Task<List<netPurLocationModel>> NetSateliteGet(string DistrictCode);
        Task<List<netPurLocationModel>> NetDistrictGet(string ProvinceCode);
        Task<List<netPurLocationModel>> NetDistrictGetByName(string ProvinceCode, string District);
        Task<netPurLocationModel> NetLocationGet(string location);

        //IDT's
        Task<int> IDTCreate(IDTModel iDTModel);
        Task IDTUpdate(IDTModel model);
        Task IDTUpdateStatus(IDTModel model);
        Task<List<IDTModel>> IDTGetByLocation(string location, int page = 1, int rowCount = 250);
        Task<List<IDTModel>> IDTGetAll(TransactionFilter model);
        Task<int> GRNCreate(GRNModel iDTModel);
        Task GRNUpdate(GRNModel model);
        Task GRNUpdateStatus(GRNModel model);
        Task<List<GRNModel>> GRNGetByLocation(string location, int page = 1, int rowCount = 250);
        Task<List<GRNModel>> GRNGetAll(TransactionFilter model);
        Task<List<LoadingOrdersModel>> LoadingOrdersGetByLocation(string location, int page = 1, int rowCount = 250);
        Task<List<LoadingOrdersModel>> LoadingOrdersGetAll(TransactionFilter model);

        //GIN's
        Task<int> GINCreate(GINModel model);
        Task<int> GINDetailsCreate(GINDetailsModel iDTModel);
        Task GINUpdate(GINModel model);
        Task GINDetailsUpdate(GINDetailsModel model);
        Task GINUpdateStatus(GINModel model);
        Task<List<GINModel>> GINGetByLocation(string location, int page = 1, int rowCount = 250);
        Task<List<GINDetailsModel>> GINDetailsGetByLocation(string location, int page = 1, int rowCount = 250);
        Task<List<GINModel>> GINGetById(string Id);
        Task<List<GINModel>> GINGetAll(TransactionFilter model);
        Task<List<GINModel>> GINGetAllReport(TransactionFilter model);
        Task<List<GINModel>> GINGetAllExcel(TransactionFilter model);

        //Delivery Orders
        Task<List<DeliveryOrdersModel>> DeliveryOrdersGetByLocation(string location, int page = 1, int rowCount = 250);
        Task<List<DeliveryOrdersDetailsModel>> DeliveryOrdersDetailsGetByLocation(string location, int page = 1, int rowCount = 250);

        //Pagination
        Task<PaginationData> PaginationData(PaginationData data);
        Task<PaginationData> PaginationDataSrpos(PaginationData data);

        //App Data
        Task<int> AppDataCreate(AppDataModel model);
        Task AppDataUpdate(AppDataModel model);
        Task AppDataDelete(int Id);
        Task<AppDataModel> AppDataGet(int Id);
        Task<AppDataModel> AppDataGetAll();

        // Probase Integration
        Task<AuthorizationModel> GetAuthorization();
        Task<DevicesAuthModel> DevicesAuthentication(string username, string password);
        Task<ResponseModelKyc> FarmerKyc(string token, FarmerKycModel model);
        Task<PostBCLModelResponse> BCLCreate(BCLModel model, string Token);
        Task<PostBCLModelResponse> BCLCreateAttach(BCLModel model, PRCNModel prcn, DevicesModel devices, FarmerModel farmer, string Token);
        Task<int> BCLUpdateStatus(BCLModel model);
        Task<PaymentProviderModel> GetPaymentProviders(string token);
        Task<int> PaymentProviderCreate(DataModelPay model);
        Task<int> BranchCreate(Branches model, int providerId);
        Task<List<DataModelPay>> GetPaymentProviderApp(int page = 1, int rowCount = 250);
        Task<List<Branches>> GetBranchesApp(int page = 1, int rowCount = 250);

    }
}

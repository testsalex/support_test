using Dependencies;
using Documents.Factory;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MyFunctionHelper;
using Paperless.Activities;
using Paperless.Ardani;
using Paperless.Backup;
using Paperless.BackupDB;
using Paperless.Clients;
using Paperless.CPAA;
using Paperless.Documents.Admin;
using Paperless.Documents.Get;
using Paperless.Documents.Infra;
using Paperless.Documents.Update;
using Paperless.Documents.Upload;
using Paperless.Extras;
using Paperless.Feed;
using Paperless.Imports;
using Paperless.Inquiry;
using Paperless.MySyncfusion.CreatePDF;
using Paperless.Newsletters;
using Paperless.PipeDrives;
using Paperless.Queue;
using Paperless.Sales;
using Paperless.Send;
using Paperless.Tasks;
using Paperless.Topic;
using Paperless.Users.Account;
using Paperless.Users.Admin;
using Paperless.Users.DTO;
using Paperless.Users.Update;
using RedisCacheService;
using Suppliers.Admin;
using Users.Get;

// https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-azure-functions-csharp
// Else DI throws
[assembly: FunctionsStartup(typeof(UserRegistration.Startup))]
namespace UserRegistration
{
  public class Startup : MyFunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      AddServices(builder.Services);
      MyConfigure(builder);
      RegisterSyncfusion(builder);
    }
    private void AddServices(IServiceCollection services)
    {
      // Created for SupportFunction by DIChecker with 114 services on 16/10/2022 18:02
      AddBaseServices(services);

      services.AddSingleton<IRedisService, RedisService>();
      services.AddSingleton<IKeyVaultService, KeyVaultService>();
      services.AddSingleton<ICacheThroughService, /*MemoryCacheThroughService*/ RedisCacheThroughService>();
      services.AddSingleton<IAppSettingsService, AppSettingsService>();

      var di = new DiContainer(services);

      services.AddScoped<IAccountRowsAdminService, AccountRowsAdminService>();
      services.AddScoped<IAccountRowService, AccountRowService>();
      services.AddScoped<IActivityService, ActivityService>();
      services.AddScoped<IAnalyzerService, AnalyzerService>();
      services.AddScoped<IArdaniAccountService, ArdaniAccountService>();
      services.AddScoped<IArdaniAdminService, ArdaniAdminService>();
      services.AddScoped<IArdaniDocumentsService, ArdaniDocumentsService>();
      services.AddScoped<IArdaniIndexService, ArdaniIndexService>();
      services.AddScoped<IArdaniUsersService, ArdaniUsersService>();
      services.AddSingleton<IAutomateQueueSenderService, AutomateQueueSenderService>();
      services.AddScoped<IBackupDBService, BackupDBService>();
      services.AddSingleton<IBillingPackageHelperService, BillingPackageHelperService>();
      services.AddScoped<BillingsACService>();
      services.AddScoped<BillingsBUService>();
      services.AddScoped<IBillingsPackageService, BillingsPackageService>();
      services.AddScoped<BillingsRefundService>();
      services.AddScoped<IBillingsService, BillingsService>();
      services.AddScoped<IBillingsUserService, BillingsUserService>();
      services.AddScoped<IBooksGetService, BooksGetService>();
      services.AddScoped<IBooksService, BooksService>();
      services.AddScoped<IClearingService, ClearingService>();
      services.AddScoped<IClientService, ClientService>();
      services.AddScoped<ICollectionService, CollectionService>();
      services.AddScoped<CPAAAccountService>();
      services.AddScoped<CreatePDFService>();
      //services.AddScoped<ICreditGuardService, CreditGuardService>();
      services.AddSingleton<IDocumentFactory, DocumentFactory>();
      services.AddScoped<DocumentsAdminDeleteService>();
      services.AddScoped<IDocumentsDeleteService, DocumentsDeleteService>();
      services.AddScoped<IDocumentsDuplicateService, DocumentsDuplicateService>();
      services.AddScoped<IDocumentsGetService, DocumentsGetService>();
      services.AddScoped<IDocumentsIncomingService, DocumentsIncomingService>();
      services.AddSingleton<IDocumentsIsService, DocumentsIsService>();
      services.AddSingleton<DocumentsManagerQueueSenderService>();
      services.AddScoped<IDocumentsManagerService, DocumentsManagerService>();
      services.AddScoped<IDocumentsMigrateService, DocumentsMigrateService>();
      services.AddScoped<IDocumentsSearchService, DocumentsSearchService>();
      services.AddSingleton<IDocumentsToService, DocumentsToService>();
      services.AddScoped<IDocumentsUpdateAfterService, DocumentsUpdateAfterService>();
      services.AddScoped<IDocumentsUpdateService, DocumentsUpdateService>();
      services.AddScoped<IDocumentsUploadService, DocumentsUploadService>();
      services.AddScoped<IDocumentsWhatsAppService, DocumentsWhatsAppService>();
      services.AddScoped<DoxsMigrate>();
      services.AddScoped<IDoxsService, DoxsService>();
      services.AddScoped<IExtrasService, ExtrasService>();
      services.AddScoped<IFeedService, FeedService>();
      services.AddScoped<FeesService>();
      services.AddScoped<ImportBKMVService>();
      services.AddScoped<ImportCardsIndexService>();
      services.AddScoped<ImportFinbotImagesService>();
      services.AddScoped<ImportFinbotService>();
      services.AddScoped<IIndexService, IndexService>();
      services.AddScoped<IInquiriesService, InquiriesService>();
      services.AddSingleton<InquiryTurnService>();
      services.AddScoped<IInvoiceDatasService, InvoiceDatasService>();
      services.AddScoped<InvoiceElementsService>();
      services.AddScoped<InvoicesService>();
      services.AddScoped<IManasService, ManasService>();
      //services.AddScoped<IMeshulamService, MeshulamService>();
      services.AddScoped<MigrateService>();
      services.AddScoped<IMiscService, MiscService>();
      services.AddScoped<MovementsAdminService>();
      services.AddScoped<IMovementsGetService, MovementsGetService>();
      services.AddScoped<IMovementsService, MovementsService>();
      services.AddScoped<INewsletterService, NewsletterService>();
      services.AddScoped<INewsletterServiceBU, NewsletterServiceBU>();
      //services.AddSingleton<IOmnitelecomService, OmnitelecomService>();
      //services.AddScoped<IPipeDrive, PipeDrive>();
      services.AddScoped<IPipeDriveService, PipeDriveService>();
      services.AddScoped<PolicyService>();
      services.AddScoped<ReconcileBankService>();
      services.AddScoped<IReconcileCardService, ReconcileCardService>();
      services.AddScoped<RestoreDBService>();
      services.AddScoped<ISalesDataService, SalesDataService>();
      services.AddScoped<ISalesLogService, SalesLogService>();
      services.AddScoped<ISendService, SendService>();
      services.AddScoped<IStreamingService, StreamingService>();
      services.AddSingleton<ISupplierGetService, SupplierGetService>();
      services.AddSingleton<ISupplierSetService, SupplierGetService>();
      services.AddSingleton<ISuppliersGetClientService, SuppliersGetClientService>();
      services.AddScoped<ISuppliersManagerService, SuppliersManagerService>();
      services.AddSingleton<ITopicSender, SuppliersTopicSenderService>();
      services.AddSingleton<ISuppliersUpdateClientService, SuppliersUpdateClientService>();
      services.AddScoped<ISuppliersUpdateService, SuppliersUpdateService>();
      services.AddScoped<ITagsService, TagsService>();
      services.AddSingleton<TasksQueueSenderService>();
      services.AddScoped<TasksQueueService>();
      services.AddScoped<ITicketDTOService, TicketDTOService>();
      //services.AddScoped<TicketsAdminService>();
      services.AddScoped<ITicketsService, TicketsService>();
      services.AddScoped<IUsersAccountService, UsersAccountService>();
      services.AddScoped<UsersDTOGetService>();
      services.AddScoped<UsersDTOService>();
      services.AddScoped<UsersManagerService>();
      services.AddScoped<IUsersSearchService, UsersSearchService>();
      services.AddScoped<IUsersTokenService, UsersTokenService>();
      services.AddScoped<IUsersUpdateService, UsersUpdateService>();
      services.AddScoped<IExcel, Excel>();
      // DIChecker checker end with 114 services
    }
    public void RegisterSyncfusion(IFunctionsHostBuilder builder)
    {
      var sLicenseKey = GetSyncfusionLicense(builder);
      Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(sLicenseKey);
    }
  }
}

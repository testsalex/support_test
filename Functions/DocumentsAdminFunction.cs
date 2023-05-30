using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Paperless.Backup;
using Paperless.Documents.Update;
using Paperless.Imports;
using Paperless.Tasks;
using Paperless.Users.Admin;
using System;
using System.Threading.Tasks;

namespace Paperless.Functions
{
  [Obsolete]
  public class DocumentsAdminFunction : MyFunctionHelper
  {
    // Domain Services
    private readonly IAccountRowService _account;
    private readonly IAccountRowsAdminService _accountAdmin;
    private readonly IActivityService _activity;
    private readonly IArdaniDocumentsService _ardani;
    private readonly IArdaniAdminService _ardaniAdmin;
    private readonly IArdaniIndexService _ardaniIndex;
    private readonly IBackupDBService _backupDB;
    private readonly IBooksService _books;
    //private readonly CacheService _cache;
    private readonly IClearingService _clearing;
    private readonly IClientService _client;
    private readonly IDocumentsGetService _dget;
    private readonly IDocumentsIsService _dis;
    private readonly IDocumentsToService _dto;
    private readonly IDoxsService _doxs;
    private readonly DoxsMigrate _doxsmigrate;
    private readonly IDocumentsUpdateAfterService _dua;
    private readonly IMeshulamService _meshulam;
    private readonly DocumentsAdminDeleteService _dAdminDelete;
    private readonly IDocumentsDeleteService _ddelete;
    private readonly IDocumentsIncomingService _dIncoming;
    //private readonly InquiryService _dinqs;
    private readonly IDocumentsMigrateService _dmigrate;
    private readonly IDocumentsManagerService _dms;
    private readonly IDocumentsUploadService _dupload;
    //private readonly ExportCardsService _exportCards;
    //private readonly FacebookService _fb;
    private readonly FeesService _fees;
    private readonly IFeedService _feed;
    private readonly ImportBKMVService _importBikoret;
    private readonly ImportFinbotService _importFinbot;
    private readonly ImportFinbotImagesService _importFinbotImages;
    private readonly ImportCardsIndexService _importIndexCards;
    private readonly IInquiriesService _inquiries;
    private readonly MovementsAdminService _mAdmin;
    private readonly MigrateService _migrate;
    private readonly UsersManagerService _ms;
    private readonly RestoreDBService _restore;
    private readonly IUsersAccountService _rs;
    private readonly ISalesLogService _sales;
    private readonly ISalesDataService _salesData;
    private readonly ISalesLogService _salesLog;
    private readonly ISendService _send;
    private readonly IAutomateQueueSenderService _servicebus;
    private readonly ITagsService _tags;
    private readonly TasksQueueService _tasks;
    //private readonly TasksService _tasks;
    //private readonly TasksListService _tasksList;
    private readonly IDocumentsWhatsAppService _whatsapp;
    //private readonly UserReportService _ureport;
    private readonly IBooksGetService _booksGet;
    //private readonly UserReportRefreshService _ureportRefresh;
    //private readonly UserReportStepService _ureportStep;
    private readonly IUsersUpdateService _us;

    // HelperServices
    private readonly ICryptoUtil _cryptoUtil;
    private readonly IEmail _email;
    private readonly IPipeDrive _pipeDrive;
    private readonly QueryServer _queryServer;
    private readonly INewsletterService _newsletterService;
    private readonly INewsletterServiceBU _newsletterServiceBU;

    // For playground
    private readonly IBillingsService _billing;
    private readonly BillingsACService _billingAC;
    private readonly BillingsBUService _billingBU;
    private readonly IBillingsPackageService _billingPackage;
    private readonly BillingsRefundService _billingRefund;
    private readonly IBillingsUserService _billingUser;
    private readonly IMovementsService _movement;
    public DocumentsAdminFunction(
      IUsersGetService get,
      IUsersIsService @is,
      IUsersToService to,
      ILogService log,

      IClientLib clientLib,
      IEmailRaw emailRaw,

      IDocumentsGetService dget,
      IDocumentsIsService dis,
      IDocumentsToService dto,

      IEmail email,

      IAccountRowService account,
      IAccountRowsAdminService accountAdmin,
      IActivityService activity,
      IArdaniAdminService ardaniAdmin,
      IArdaniDocumentsService ardani,
      IArdaniIndexService ardaniIndex,
      IBackupDBService backup,
      IBooksService books,
      //CacheService _cache,
      IClearingService clearing,
      IClientService client,
      IDoxsService doxs,
      DoxsMigrate doxsmigrate,
      IDocumentsUpdateAfterService dua,
      IMeshulamService meshulam,
      IAutomateQueueSenderService servicebus,
      TasksQueueService tasks,
      //TasksService _tasks,
      //TasksListService _tasksList,

      DocumentsAdminDeleteService dAdminDelete,
      IDocumentsDeleteService ddelete,
      IDocumentsIncomingService dIncoming,
      //InquiryService _dinqs,
      IDocumentsMigrateService dmigrate,
      IDocumentsManagerService dms,
      IDocumentsUploadService dupload,
      //ExportCardsService _exportCards,
      //FacebookService _fb,
      FeesService fees,
      IFeedService feed,
      ImportBKMVService importBikoret,
      ImportFinbotService importFinbot,
      ImportFinbotImagesService importFinbotImages,
      ImportCardsIndexService importIndexCards,
      IInquiriesService inquiries,
      MovementsAdminService mAdmin,
      MigrateService migrate,
      UsersManagerService ms,
      ITagsService tags,
      RestoreDBService restore,
      IUsersAccountService rs,
      ISalesLogService sales,
      ISalesDataService salesData,
      ISalesLogService salesLog,
      IDocumentsWhatsAppService whatsapp,
      //UserReportService _ureport,
      IBooksGetService booksGet,
      //UserReportRefreshService _ureportRefresh,
      //UserReportStepService _ureportStep,
      IUsersUpdateService us,

      ICryptoUtil cryptoUtil,
      IItemsQueue itemsQueue,
      IPipeDrive pipeDrive,
      QueryServer queryServer,
      INewsletterServiceBU newsletterServiceBU,
      INewsletterService newsletterService,

      IBillingsService billing,
      BillingsACService billingAC,
      BillingsBUService billingBU,
      IBillingsPackageService billingPackage,
      BillingsRefundService billingRefund,
      IBillingsUserService billingUser,
      IMovementsService movement,
      ISendService send
    ) : base(clientLib, emailRaw, itemsQueue, get, @is, to, log)
    {
      _account = account;
      _accountAdmin = accountAdmin;
      _activity = activity;
      _ardaniAdmin = ardaniAdmin;
      _ardaniIndex = ardaniIndex;
      _ardani = ardani;
      _backupDB = backup;
      _billing = billing;
      _billingAC = billingAC;
      _billingBU = billingBU;
      _billingPackage = billingPackage;
      _billingRefund = billingRefund;
      _billingUser = billingUser;
      _books = books;
      //this._cache = _cache;
      _clearing = clearing;
      _client = client;
      _dget = dget;
      _dis = dis;
      _dto = dto;
      _doxs = doxs;
      _doxsmigrate = doxsmigrate;
      _dua = dua;
      _meshulam = meshulam;
      _dAdminDelete = dAdminDelete;
      _ddelete = ddelete;
      _dIncoming = dIncoming;
      //this._dinqs = _dinqs;
      _dmigrate = dmigrate;
      _dms = dms;
      _dupload = dupload;
      //this._exportCards = _exportCards;
      //this._fb = _fb;
      _fees = fees;
      _feed = feed;
      _importBikoret = importBikoret;
      _importFinbot = importFinbot;
      _importFinbotImages = importFinbotImages;
      _importIndexCards = importIndexCards;
      _inquiries = inquiries;
      _mAdmin = mAdmin;
      _migrate = migrate;
      _movement = movement;
      _ms = ms;
      _sales = sales;
      _salesData = salesData;
      _salesLog = salesLog;
      _send = send;
      _servicebus = servicebus;
      _restore = restore;
      _rs = rs;
      _tags = tags;
      _tasks = tasks;
      //this._tasksList = _tasksList;
      _whatsapp = whatsapp;
      //this._ureport = _ureport;
      _booksGet = booksGet;
      //this._ureportRefresh = _ureportRefresh;
      //this._ureportStep = _ureportStep;
      _us = us;

      _cryptoUtil = cryptoUtil;
      _email = email;
      _pipeDrive = pipeDrive;
      _queryServer = queryServer;
      _newsletterService = newsletterService;
      _newsletterServiceBU = newsletterServiceBU;
    }

    [FunctionName("PingSupportDocuments")]
    public IActionResult PingSupportUsers(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/documents/ping")] HttpRequest req)
    {
      // Be sure to change Prefix
      // http://localhost:7089/support/ping
      // https://plsupport.azurewebsites.net/support/ping
      // https://api.paperless.tax/support/ping
      var sRet = GetUploadTime();
      return new OkObjectResult(sRet);
    }
    //[HttpGet]
    //[Route("users/testplayground")]
    //public async Task<ActionResult> TestPlayGround()
    //{
    [FunctionName("TestPlayGroundDocs")]
    public async Task TestPlayGroundDocs(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/documents/testplayground")] HttpRequest req,
      ILogger log)
    {
      // http://localhost:7089/support/documents/testplayground
      if (!_clientLib.IsLocalHost()) throw new UnauthorizedAccessException();
      //await _dms.MergeSuppliers("e6p1m7xek1", "dz0j5auw9f", true, true);
      //var d = await _dget.Get("922w7p6nem"); var u = await _get.GetAdmin(); var uOwner = await _dget.GetOwner(d); await _ddelete.Delete(d, uOwner, u); // Delete one document
      //await _dus.ReplaceTagsGlobally("10016", "3685");
      //await _dms.ReplaceUserSpecificTags("5dz6wp3r1s", "201", "10036");
      //await _dms.ReplaceACUsersSpecificTags("i0s5n3zaet", "201", "10033");
      //var d = await _dget.Get("f0hapn3ctu", null); await _dus.FillPending(d, false);
      //await _dinqs.RefactorChats();
      //var u = await _get.GetByID("9urx7kbhe6"); await _dinqs.CloseDisabledUserInquiries(u);
      //await _dus.RefactorLinkedIDs();
      //await _dus.RefreshDocumentsPeriods();
      //await _dms.PushExpensesToPeriod("elezl6he68", "2101", "2107", "2108");
      //await _dms.PushExpensesToPeriodByACAfterDate();
      //await _dms.UpdateUploadedPeriod();
      //await _dms.MovePeriods();
      //var d = await _dget.Get("9fj4uudc01"); await _duplicate.MarkAsNotDuplicate(d);
      //var u = await _get.GetByID("btvlohslsn"); var uAC = await _get.GetAccountant(u); await _ddelete.DeleteAllUserDocs(u, uAC);
      //await _dms.FixOrphanPendingDocs(); // In case Ezra BUs are missing from EzraBKs
      //await _dus.ConvertCarsToTag2(); // Ran on 24.08.2020
      //await _dget.FixWithholdingDocsSign();
      //await _dget.FixSelfInvoice();
      //await _dus.RemoveOldAnalyzedData();
      //await _dus.RemoveOldAnalyzers();
      //await _dms.RerunPostProcessFailed();
      //await _dms.RemoveDuplicateInvoices();
      //await _dms.RerunPendingAnalyzer();
      //await _dms.ReanalyzeDueToBilling();
      //await _dus.FixMispelledOnlyReceipt();
      //var docs = await _db3.GetItems<Document>(d => d.sDocNumber == "34432712");
      //await _dus.ConvertPaturReceiptsToMursheReceiptsRetro("d20io2vxvh", new DateTime(2021, 4, 2));
      //await RerunIncomingCloudmailin();
      //var uBU = await _get.GetByID("cnuv4wjmvl"); var docs = await _dget.GetAllByOwner(uBU); var ds = docs.Where(d => d.sPeriod == "").ToList();
      //await _dms.FixIsraeliTrans();
      //await _dms.RefreshPendingCountersALL();
      //await _dms.FixUrgent();
      //await _dms.FillUrgent();
      //await _dms.MoveAllDocumentsToAnotherBU();
      //await _dms.BuildDailyHistogram();
      //await _dms.FixNoTransDate();
      //await _dms.FixPaperlessSupplierlessInvoices();
      //await _dms.FixLastModified();
      //await _dms.FixEmptyTag2();
      //await _dms.FixInvoiceApp();
      //await _dms.FixInvoicePaymentMovement();
      //var u = await _get.GetByID("e9rerb9c56"); await _dua.NotifyUserMissingRecurrentSupplier(u);
      //var uBU = await _get.GetByID("dm3h3zxdba"); var (aMissing, _) = await _dua.CalcMissingIncomeInvoices(uBU, null);
      //var uAC = await _get.GetByID("26jha3k5ic"); await _dms.RefreshAllACReports(uAC);
      //var uAC = await _get.GetByID("adgdjgdm2v"); await _dms.RemoveDocNumberPrefix(uAC);
      //await _dua.NotifyPostponedPayment("hv9vd9iv5a");
      //var uBU = await _get.GetByID("igrv1x3e2i"); await _dAdminDelete.RemoveImportedDuplicated(uBU);

      // Invoices
      //await _dms.MergeClients();
      //await _dms.RefreshClientDocCount();

      // EzraBK
      //await _dms.AssociatePendingToEzraBK("5ozn1fbpsp");
      //await _dget.GetAllSelfInvoices();
      //await _dinqs.MigrateEzraBKInquiries(DomainModels.User.sNuritBitranID);
      //await _dus.UpdateLastModified();
      //var uBU = await _get.GetByID("1gto9hqiuu"); await _ddelete.DeletePending(uBU);
      //var uBU = await _get.GetByID("btvlohslsn"); await _ddelete.DeleteAllUserDocs(uBU, uBU);
      //await _dAdminDelete.DeleteSpecificUserDocs();
      //await _dus.MovePendingDocsToAnotherBU();
      //var uBU = await _get.GetByID("e92il031st"); await _ureport.AddMissingPeriods(uBU);

      // movements
      //var uBU = await _get.GetByID("cp38s0u829"); await _movement.FindDuplicates(uBU);
      //var uBU = await _get.GetByID("cnvjfz2oe1"); var d = await _dget.Get("cpdgi90krs"); await _movement.UpdateDocument(d, uBU);
      //var uBU = await _get.GetByID("iwxul4ic3f"); await _movement.SetOpeningBalance(uBU, 2022);
      //var uBU = await _get.GetByID("btvlohslsn"); await _movement.DeleteAllUserMovements(uBU);
      //await _movement.Fix();

      // Import Bank Accounts
      //var uBU = await _get.GetByID("g0lchl04fo"); await _ddelete.DeleteAllChecks(uBU); await _account.DeleteAll(uBU);
      //await _account.CleanHiddenChars();
      //await _accountAdmin.DeleteImportedRows();
      //await _account.CompletePageBalance();
      //await _accountAdmin.AddMissingPaperlessCreditTotalRows();
      //await _dms.FindDatalessRows();
      //await _dms.CountBKaaSDocuments();
      //await _dms.CountBKaaSDocumentsByBK();
      //await _account.SendBankOTPAPP("", null);

      //var d = await _dget.Get("dehea08yir"); var u = await _dget.GetOwner(d); await _movement.UpdateDocument(d, u);
      //await _movement.Deconcile();
      //await _movement.RefactorMRs();
      //await _movement.RefactorARs();
      //await _movement.FixTrans();
      //await _movement.RenameMatches();
      //await _movement.FixBankMatches();
      //await _mAdmin.FixBankMatches2();
      //await _mAdmin.FixDuplicateMatches();
      //await _movement.FixOpenBankMatches();
      //await _movement.FillMatchCardID();
      //await _movement.FillDT2();
      //await _mAdmin.ReplaceMovementsCard();
      //await _mAdmin.AddDebitAccountID();
      //await _mAdmin.CleanMovementCurrency();
      //await _mAdmin.RenameMovementCurrency();
      //await _mAdmin.ReplaceCardCurrency();

      //await _dupload.FinishFailedUploads();
      //await _dms.FixUserMissingClients();

      // Reconcile
      //var uBU = await _get.GetByID("cp38s0u829"); await _reconcile.CloseAllOpenBankMovements(uBU, "2101", "2112", 1);
      //var uBU = await _get.GetByID("cp38s0u829"); await _ddelete.DeleteAllOpenBankMovements(uBU, "2101", "2112", 1);

      if (false)
      {
        var aIDs = new[] { // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types
          // 19.02.21
          //"cp38s0u829", // תיצוג
          //"crcysmrfry", // אושר בריבוע
          //"cq0of84gdu", // טל סגל של חנימוב
          //"ciyqq0accx", // אלכסנדר פטרופוליאדיס
          //"cpo8jqii6p", // יונתן לינקס 22.02
          //"cl3d06vkzo", // קרדיט 24.02
          //"cnvjfz2oe1", // יוסף עזריה
          //"cjlymfrvex", // רייזיס
          //"cf9leiv0qz", // הדובדבן שבקצפת
          //"cjk571emgg" // סמארטסן בע"מ
          //"cnlb1o7x6d" // אס. סי. קאר נטסולושנס בע"מ
          //"c94tiuzznr", // סטיב ספינר
          //"cgs3c3vtsn", // שטיינר 
          //"c8iggk46rn", // ARVR
          //"cl6h5zud93", // 27.04 סאלם נצאצרה
          //"chiokrg1ep", // האגס 1
          //"ddbdf35fxb", // שחק מעבדות
          //"1gto9hqiuu", // חברת לדוגמא

          //"clp8q8xydp", // ספרול
          //"cgwv05hpx2", // גלובל מופ
          //"cjluvxoho0", // נמט
          //"cjlxn7n6sj", // CoreIT
          //"cjli9hu65p", // עידו פריד
          //"cmyeghbu9x", // עמית גרינברג
          //"dfdtd3kw7i", // גטהון טפרה

          //"lxy8fanpr", // פייפרלס
          //"cq13imnw09", // 02.05.21 יאנג מדיה
          //"dhe28t173o" // המשותפת
          //"d752a7srii" // אלי נחמה
          //"cg8br68jdq", // פלשין נדלן 24/02
          //"cnlb1o7x6d" // הילה קרסינטה
          //"aqmpmkco18", // אניגמה 
          //"aqlxrb3umz", // פרנסיס
          "c6f84e31d6", // wesales
        };
        //_cache.Invalidate();
        foreach (var sID in aIDs)
        {
          var uBU2 = await _get.GetByID(sID);
          await _movement.BuildFromDocuments(uBU2);
          //await _movement.RefreshAllReports(uBU2);
        }
      }
    }
  }
}
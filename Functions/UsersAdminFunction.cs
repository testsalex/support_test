using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Paperless.Functions
{
  [Obsolete]
  public class UsersAdminFunction : MyFunctionHelper
  {
    // Domain Services
    //private readonly ActivityService _activity;
    //private readonly ArdaniDocumentsService _ardani;
    //private readonly ArdaniAdminService _ardaniAdmin;
    //private readonly ArdaniIndexService _ardaniIndex;
    //private readonly BackupDBService _backupDB;
    //private readonly BooksService _books;
    //private readonly CacheService _cache;
    //private readonly ClearingService _clearing;
    //private readonly ClientService _client;
    //private readonly DocumentsGetService _dget;
    //private readonly DocumentsIsService _dis;
    //private readonly DocumentsToService _dto;
    //private readonly DoxsService _doxs;
    //private readonly DoxsMigrate _doxsmigrate;
    //private readonly MeshulamService _meshulam;
    //private readonly DocumentsDeleteService _ddelete;
    //private readonly DocumentsIncomingService _dIncoming;
    ////private readonly InquiryService _dinqs;
    //private readonly DocumentsMigrateService _dmigrate;
    //private readonly DocumentsManagerService _dms;
    //private readonly DocumentsUploadService _dupload;
    ////private readonly ExportCardsService _exportCards;
    ////private readonly FacebookService _fb;
    //private readonly FeesService _fees;
    //private readonly FeedService _feed;
    //private readonly ImportBKMVService _importBikoret;
    //private readonly ImportFinbotService _importFinbot;
    //private readonly ImportFinbotImagesService _importFinbotImages;
    //private readonly ImportCardsIndexService _importIndexCards;
    //private readonly InquiriesService _inquiries;
    //private readonly MigrateService _migrate;
    //private readonly UsersManagerService _ms;
    //private readonly PipeDriveService _pipe;
    //private readonly RestoreDBService _restore;
    //private readonly UsersAccountService _rs;
    //private readonly SalesLogService _sales;
    //private readonly SalesDataService _salesData;
    //private readonly SalesLogService _salesLog;
    //private readonly SendService _send;
    //private readonly AutomateQueueSenderService _servicebus;
    //private readonly TagsService _tags;
    //private readonly TasksQueueService _tasks;
    ////private readonly TasksService _tasks;
    ////private readonly TasksListService _tasksList;
    //private readonly TicketsService _tickets;
    //private readonly TicketsAdminService _ticketsAdmin;
    //private readonly DocumentsWhatsAppService _whatsapp;
    ////private readonly UserReportService _ureport;
    //private readonly BooksGetService _booksGet;
    ////private readonly UserReportRefreshService _ureportRefresh;
    ////private readonly UserReportStepService _ureportStep;
    //private readonly UsersUpdateService _us;

    //// HelperServices
    //private readonly CryptoUtil CryptoUtil;
    //private readonly Email Email;
    //private readonly PipeDrive PipeDrive;
    //private readonly QueryServer QueryServer;
    //private readonly NewsletterService NewsletterService;
    //private readonly NewsletterServiceBU NewsletterServiceBU;

    //// For playground
    //private readonly BillingsService _billing;
    //private readonly BillingsACService _billingAC;
    //private readonly BillingsBUService _billingBU;
    //private readonly BillingsPackageService _billingPackage;
    //private readonly BillingsRefundService _billingRefund;
    //private readonly BillingsUserService _billingUser;
    //private readonly MovementsService _movement;
    public UsersAdminFunction(
      IUsersGetService get,
      IUsersIsService @is,
      IUsersToService to,
      ILogService log,

      IClientLib clientLib,
      IEmailRaw emailRaw,

      //DocumentsGetService _dget,
      //DocumentsIsService _dis,
      //DocumentsToService _dto,

      //Email Email,

      //ActivityService _activity,
      //ArdaniAdminService _ardaniAdmin,
      //ArdaniDocumentsService _ardani,
      //ArdaniIndexService _ardaniIndex,
      //BackupDBService _backup,
      //BooksService _books,
      ////CacheService _cache,
      //ClearingService _clearing,
      //ClientService _client,
      //DoxsService _doxs,
      //DoxsMigrate _doxsmigrate,
      //MeshulamService _meshulam,
      //AutomateQueueSenderService _servicebus,
      //TasksQueueService _tasks,
      ////TasksService _tasks,
      ////TasksListService _tasksList,

      //DocumentsDeleteService _ddelete,
      //DocumentsIncomingService _dIncoming,
      ////InquiryService _dinqs,
      //DocumentsMigrateService _dmigrate,
      //DocumentsManagerService _dms,
      //DocumentsUploadService _dupload,
      ////ExportCardsService _exportCards,
      ////FacebookService _fb,
      //FeesService _fees,
      //FeedService _feed,
      //ImportBKMVService _importBikoret,
      //ImportFinbotService _importFinbot,
      //ImportFinbotImagesService _importFinbotImages,
      //ImportCardsIndexService _importIndexCards,
      //InquiriesService _inquiries,
      //MigrateService _migrate,
      //UsersManagerService _ms,
      //PipeDriveService _pipe,
      //TagsService _tags,
      //RestoreDBService _restore,
      //UsersAccountService _rs,
      //SalesLogService _sales,
      //SalesDataService _salesData,
      //SalesLogService _salesLog,
      //TicketsService _tickets,
      //TicketsAdminService _ticketsAdmin,
      //DocumentsWhatsAppService _whatsapp,
      ////UserReportService _ureport,
      //BooksGetService _booksGet,
      ////UserReportRefreshService _ureportRefresh,
      ////UserReportStepService _ureportStep,
      //UsersUpdateService _us,

      //CryptoUtil CryptoUtil,
      IItemsQueue itemsQueue
    //PipeDrive pipeDrive,
    //QueryServer QueryServer,
    //NewsletterServiceBU NewsletterServiceBU,
    //NewsletterService NewsletterService,

    //BillingsService _billing,
    //BillingsACService _billingAC,
    //BillingsBUService _billingBU,
    //BillingsPackageService _billingPackage,
    //BillingsRefundService _billingRefund,
    //BillingsUserService _billingUser,
    //MovementsService _movement,
    //SendService _send
    ) : base(clientLib, emailRaw, itemsQueue, get, @is, to, log)
    {
      //this._activity = _activity;
      //this._ardaniAdmin = _ardaniAdmin;
      //this._ardaniIndex = _ardaniIndex;
      //this._ardani = _ardani;
      //_backupDB = _backup;
      //this._billing = _billing;
      //this._billingAC = _billingAC;
      //this._billingBU = _billingBU;
      //this._billingPackage = _billingPackage;
      //this._billingRefund = _billingRefund;
      //this._billingUser = _billingUser;
      //this._books = _books;
      //this._cache = _cache;
      //this._clearing = _clearing;
      //this._client = _client;
      //this._dget = _dget;
      //this._dis = _dis;
      //this._dto = _dto;
      //this._doxs = _doxs;
      //this._doxsmigrate = _doxsmigrate;
      //this._meshulam = _meshulam;
      //this._ddelete = _ddelete;
      //this._dIncoming = _dIncoming;
      ////this._dinqs = _dinqs;
      //this._dmigrate = _dmigrate;
      //this._dms = _dms;
      //this._dupload = _dupload;
      //this._exportCards = _exportCards;
      //this._fb = _fb;
      //this._fees = _fees;
      //this._feed = _feed;
      //this._importBikoret = _importBikoret;
      //this._importFinbot = _importFinbot;
      //this._importFinbotImages = _importFinbotImages;
      //this._importIndexCards = _importIndexCards;
      //this._inquiries = _inquiries;
      //this._migrate = _migrate;
      //this._movement = _movement;
      //this._ms = _ms;
      //this._pipe = _pipe;
      //this._sales = _sales;
      //this._salesData = _salesData;
      //this._salesLog = _salesLog;
      //this._send = _send;
      //this._servicebus = _servicebus;
      //this._restore = _restore;
      //this._rs = _rs;
      //this._tags = _tags;
      //this._tasks = _tasks;
      ////this._tasksList = _tasksList;
      //this._tickets = _tickets;
      //this._ticketsAdmin = _ticketsAdmin;
      //this._whatsapp = _whatsapp;
      ////this._ureport = _ureport;
      //this._booksGet = _booksGet;
      //this._ureportRefresh = _ureportRefresh;
      //this._ureportStep = _ureportStep;
      //this._us = _us;

      //this.CryptoUtil = CryptoUtil;
      //this.Email = Email;
      //PipeDrive = pipeDrive;
      //this.QueryServer = QueryServer;
      //this.NewsletterService = NewsletterService;
      //this.NewsletterServiceBU = NewsletterServiceBU;
    }

    [FunctionName("PingSupportUsers")]
    public IActionResult PingSupportUsers(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/users/ping")] HttpRequest req)
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
    //https://stackoverflow.com/questions/55586322/azure-function-app-timeout-for-app-service-plan
    [Timeout("3:00:00")]
    [FunctionName("TestPlayGroundUsers")]
    public async Task TestPlayGroundUsers(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/users/testplayground")] HttpRequest req,
      ILogger log)
    {
      var data = GetPostData(req);
      // http://localhost:7089/support/users/testplayground
      if (!_clientLib.IsLocalHost()) throw new NotImplementedException();

      //var u = await _get.GetByID("ej95kurc3p"); StringLib.CleanHiddenChars(u.sEmail);
      //var uBU = await _get.GetByID("fvts9rohv"); var docs = await _dget.GetByOwner(uBU); foreach (var d in docs) _db.Update(d); // Test parallel DBFlush()
      //var sDealID = CryptoUtil.Decrypt("BrJnjpWosgs", true);
      //var p = Syncfusion.Windows.Forms.PdfViewer.PdfiumNative.path; var b = System.IO.File.Exists(p); await EmailRaw.SendEmail("PdfiumNative.path: " + p + " - " + b); return Ok();

      // EMAIL
      //await Email.SendEmail("aaa@adskjfhdsf.aam", "Bounce test", "somebody", true);
      //await EmailRaw.SendEmail("Test mail", "test body");
      //await _send.HandleBouncedEmail("danadonna@walls.co.il", false);
      //await EmailRaw.SendEmail("info@paperless.tax", "eshet.itay@gmail.com", "status test", "body");
      //await EmailRaw.SendEmail("info@paperless.tax", "info@gotoezra.com", "status test", "body");
      //await EmailRaw.SendEmail("newsletter@paperless.tax", "info@gotoezra.com", "status test", "body");
      //await EmailRaw.SendEmail("invitations@paperless.tax", "info@gotoezra.com", "status test", "body");
      //await Email.SendEmailTemplateGeneral("invitations@paperless.tax", "כותרת בדיקה", "תוכן בדיקה", null, null);
      //var ics = _send.CreateCalendarInvite(DateTime.Now); await SES.SendRawEmail(ics);
      //var uBU = await _get.GetByEmailBU("oren@rotlevy-cpa.co.il"); uBU = await _get.GetAccountantBU(uBU);
      //var uBU = await _get.GetByEmail("oren@rotlevy-cpa.co.il"); if (_is.IsAccountant(uBU) && _is.IsPersonal(uBU)) uBU = await _get.GetAccountantBU(uBU);

      // SEND

      //var u = await _get.GetByID(Users.User.sBusinessTesterID); await _send.SendBusinessWelcomeMessages(u, 0, false);
      //await _send.SendDueDateReminders();
      //await _send.SendInquiryTeamReminders();
      //await _send.SendWakeUpReminders();
      //await _ddelete.RemoveDeletedDocuments();
      //await _send.SendInquiryBusinessReminders();
      //await _send.BeforeScheduleCall("14197");
      //await _send.PrepareScheduleCall("14197");
      //await _send.SendCollectionReminders();
      //var dt = Dates.ToIsraelTime(true).AddMinutes(15); await _tasks.AddTask(dt.AddMinutes(-15), TaskTypes.ScheduleCallReminder.ToString() + ",14197"); // 15 mins heads up
      //await _tasks.AddMissingTasks();
      //await EmailRaw.SendEmail("invoices@paperless.tax", "aa@sal987mjfs.com", "SES error", "test", null, null); // Simulate HandleSESNotification (doesn't work ?)
      //await _tasks.GetTasks("h75g73h1co");

      // SMS
      //_itemsQueue.QSMS(null, "בדיקה", true);
      //ItemsQueue.QSMS("0546539353", "בדיקה");
      //var uAC = await _get.GetByID("jqrpxsr8z"); await _ms.SendNewACSMS(uAC);
      //var u = await _get.GetByID("aa0vnaw8mp"); await _send.SendBusinessWelcomeSMS(u, 0, true);
      //var u = await _get.GetByID(DomainModels.User.sBusinessTesterID); await _send.SendBusinessWelcomeSMS(u, 0);
      //ItemsQueue.QSMS("0542240290", "שלום. הנה קבלה https://api.paperless.tax/income/view/XXX");
      //ItemsQueue.QSMS("0542240290", "קוד האימות שלך הוא: 62530", false, "BHapoalim");
      //ItemsQueue.QSMS("0542240290", "{\"data\": [\"From: BHapoalim\nקוד האימות שלך הוא: 62530\"]}", false, "BHapoalim");

      // SERVICE BUS
      //await _servicebus.SendMessage(new Payload() { sAction = "PostProcess", sID = "8rxp9wob3c" });
      //await _servicebus.SendMessage(new AutomatePayload() { sAction = "Page", sID = DomainModels.User.sEzraOfficeID });

      // BILLING BU
      //await _billingBU.PrepareBatch();
      //await _billingBU.Charge();
      //await _billingBU.CreatePendingInvoices();
      //var u = await _get.GetByID("jhjfhi9d1"); _billing.CreateForUser(u);
      //await _billingBU.MigrateDisabled();
      //await _billingBU.BuildCClessPayingBUs();
      //await _billingBU.RefundDisabledUsers();
      //await _billingBU.GetBUWithClearing();

      //await _billing.ClearBillings();
      //var u = await _get.GetByID("7fg6j7apjd"); var b = await _billing.GetForUser(u); await _billing.UpdatePackage(u, b, DomainModels.BillingModel.ProductType.Invoices20, 1);
      //await _billing.CreateThings();
      //await _billing.FindMissing();
      //await _billing.MoveTransactions();
      //var uAC = await _get.GetByID("bs0z64fw8n"); await _billingPackage.StopPackage(uAC, ProductType.BrandedApp);
      //await _billingUser.FillAccountantID();
      //await _billing.RemoveDUplicateTokens();
      //await _billingUser.IsUsed("1020109197322525", false);
      //await _billingAC.MigrateNewPackages();

      // BILLING AC
      //await _billingAC.PrepareBatch();
      //await _billingAC.Charge();
      //await _billingAC.CreatePendingInvoices();
      //await _billingRefund.RefundDisabledUsersAC();

      // Billing Fee
      //await _billing. AddPackageFeeInvoices("bkcupscxcr");
      //await _fee.FillAccountantID();
      //var uAC = await _get.GetByID("9i6uwmoh0a"); // בלה טל - מחייב במאי את 2105
      //var uAC = await _get.GetByID("b689ee5oco"); // עודד טוביה
      //var uAC = await _get.GetByID("bkcupscxcr"); // קרולין עובד
      //var sPeriod = "2202";
      //await _fee.PrepareBatch(uAC, sPeriod); // 1
      //await _fee.Charge(uAC); // 2
      //await _fee.CreatePendingInvoices(uAC); // 3

      //await _billing.AddPackageBranded("c6l5ldc2ki", 100);
      //await _billingAC.FindWrongCPAA();
      //var uBU = await _get.GetByID("bxh13pbzpj"); var uAC = await _get.GetAccountant(uBU); _billing.UpdateBillingStart(uAC, uBU); _db.Update(uBU);
      // *** var uAC = await _get.GetByID("8ogv973m0q"); var b = await _billing.GetForUser(uAC); await _billing.Charge(uAC, b, 105300); // ** Really charges !! Then, manually create invoice with existing client
      //await _us.FixArdaniBillingStart();
      //await _billingBU.FixMissingBUPaysPackage();
      //await _billing.RefundBatch(); 
      //await _billingBU.RefreshToken();
      //await _dget.CalcBKaaS();
      //await _ms.GetOnlyInvoices();
      //await _billingBU.BuildPackageHistogram();

      // USER MANAGER
      // ** Changing EzraBK in user's profile fixes everything
      //await _rs.DeleteACAndUsersAndDocuments();
      //var uAC = await _get.GetByID("dwvpc465a3"); await _rs.DeleteACBUs(uAC);
      //await _ms.ProcessLead("זאב רייציק", "Zeevcpa100@gmail.com", "0537363683", "קמפניה");
      //await _ms.ProcessLead("מוחמד סלימאן", "muhi100@gmail.com", "0507259953", "קמפיין לידים בפייסבוק", "cpa-fb-lead");
      //await _ms.DiluteEzraBK();
      // Run DocumentService.TestPlayGround.MigrateEzraBKInquiries() !!
      //await _migrate.MigrateBUToACBatch();
      //await _ms.FixArdaniIDs();
      //await _ms.FixPeriodStart();
      //await _get.ValidateMobiles();
      //var uAC = await _get.GetByID("axctt1fjtw"); await _ms.AddReferralID(uAC, "R" + "9dzgnmxras", null);
      //await _ms.AddSwitchUsers("b74v054i2l", "b74s1t7luf");
      //await _ms.AddSwitchUsers("bkn2oafibw", "cg6mfxjb18", "cg6mg0j981");
      //var u = await _get.GetByEmail("elad@azulay.co.il", true);
      //var u = await _get.GetByEmail("miron@miron-cpa.com", true);
      //var u2 = await _get.GetByEmailBU("miron@miron-cpa.com");
      //await _ms.SetIsDisabled();
      //await _ms.AddExtraForBKs();
      //await _ms.RemoveDuplicateUsers();
      //await _ms.ChangeAccountant();
      //await _ms.ResetNotApprovedBKaaS();
      //await _ms.FixMissingBase();
      //await _ms.MoveAccounts();
      //await _ms.PrepareList();
      //await _ms.FixCollectionActive();
      //await _ms.CleanArdaniIDs();
      //await _ms.CancelAllBKaaSPackages();

      //await _doxsmigrate.BuildBooks();
      //await _doxsmigrate.FixPeriodsOffset();
      //await _doxsmigrate.FixMissingBooks();
      //await _doxsmigrate.FixPeriods();
      //await _doxsmigrate.FixIncome();
      //await _doxsmigrate.FixOpenReported();
      //await _doxsmigrate.FixVATBalance();
      //await _doxsmigrate.ClearBUSent();
      //await _doxsmigrate.RoundNumbers();
      //await _doxsmigrate.FixIncomeZero();

      //await _doxsmigrate.CloseAllBLDoxs();
      //await _doxsmigrate.MarkPaidAsOK();
      //await _doxsmigrate.UpdateAllUsersStep();

      //var uBUs = await _get.GetAllAdminBusinessesFull(); uBUs = await _get.ExcludeDisabledCheckAC(uBUs, true); var rs = await _booksGet.GetAllReports(); await _doxs.PayAllAutoPaidBL(uBUs, rs);
      //await _doxs.RunDaily();
      //var uAC = await _get.GetByID("b5mlzoenip"); var aBUs = await _get.GetAllAccountantUsersFull(uAC); foreach (var uBU in aBUs) await _doxs.CloseReportedPeriods(uBU);

      // INCOMING
      //await _dIncoming.ProcessIncomingEmail(null);
      //await _whatsapp.ProcessIncoming(null);
      //var u = await _get.GetByMobileBU("0533955412");

      // BKMV
      //var u = await _get.GetByID("a9srn0kriq"); await _importBikoret.Check(u);
      //var u = await _get.GetByID("btjn64bh3w"); var uAC = await _get.GetAccountant(u); await _tags.PrepareUserTags(uAC, u); _tags.ClearForeignTags(uAC, u);
      //var u = await _get.GetByID("a9srn0kriq"); var uAC = await _get.GetAccountant(u); await _importBikoret.Import(u, uAC);
      //http://localhost:4200/admin/import-bkmv-cards;sUserID=aafn4b6iou
      //var u = await _get.GetByID("c6f84e31d6"); await _importIndexCards.Import(u);

      //var uBU = await _get.GetByID("cp38s0u829"); await _movement.ReplaceCard(uBU, 50022, 90005);
      // User Report
      //await _ureport.PayAllZeroIncome();
      //var u = await _get.GetByID("bsl0afynzi"); var d = await _dget.Get("cnt3k1ojpu"); await _ureport.NotifyUserMissingIncomeInvoices(u, d);
      //var u = await _get.GetByID("98rgq3k3oj"); _ureport.ClosePeriod(u, "1912", DateTime.UtcNow);
      //await _ureport.CloseAllOpenPeriods();
      //await _ureport.BuildLastReviewed();
      //await _ureport.BuildIncomeAdvance();
      //await _ureport.FixMissingMyInvoices();
      //await _ureport.UpdateMyInvoicesCounter();
      //await _ureport.PayAllAutoPaidBL();

      //var uAC = await _get.GetByID("7evwebso30");
      //var users = await _get.GetAllAccountantUsersFull(uAC);
      //var us = users.Where(u => u.Business.aPasswordsVATEnc != null);
      //var uBU = await _get.GetByID("947d3wf1sn"); await _ureport.TransmitVAT(uBU, new List<string>() { "2011", "2012" }, Period.PaymentTypes.VAT);
      //var uBU = await _get.GetByID("bzzsq78onj"); await _ureport.TransmitVAT(uBU, new List<string>() { "2011", "2012" }, Period.PaymentTypes.VAT);
      //var uBU = await _get.GetByID("bqkuokxjbuXX"); await _ureport.TransmitVAT(uBU, new List<string>() { "2103", "2104" }, Period.PaymentTypes.VAT);
      //var uBU = await _get.GetByID("lxy8fanprXX"); await _ureport.TransmitVAT(uBU, new List<string>() { "2103", "2104" }, Period.PaymentTypes.VAT);

      // Update
      //await _us.UpdateACServiceTypes();
      //await _us.UpdateSMSlessMobile();
      //await _get.ReplaceSMSLess();
      //await _us.RecalcnDocs();
      //var uAC = await _get.GetByID("d2kyb19g45"); await _us.JoinAutoPaymentAsk(uAC);

      // NEWSLETTER
      //await NewsletterService.Reset();
      //var NL = await NewsletterService.Get(); await NewsletterService.SendPosts(NL, -3, true, null); // Test
      //var uACs = await _get.GetAllAdminAccountantsFull(); var NL = await NewsletterService.Get(); await NewsletterService.SendPosts(NL, -3, false, uACs);  // **** SPECIAL + Sending
      //var uACs = await _get.GetAllAdminAccountantsFull(); var NL = await NewsletterService.Get(); await NewsletterService.SendPosts(NL, 0, false, uACs);  // **** Sending
      //var User = await Models._get.GetByID("333oix4wn9"); await Newsletter.Remove(User);
      //await NewsletterService.SendFirstOfMonthReminders();

      // NEWSLETTERBU
      //var NL = await NewsletterService.Get(); await NewsletterServiceBU.SendPosts(NL, true);
      //var NL = await NewsletterService.Get(); var users = await _get.GetNewsletterBusinessUsers(); await NewsletterServiceBU.SendPosts(NL, users);

      // USERS
      //await _us.BuildAccountantLastSignup();
      //await _us.SendDormantsReport();
      //await _us.RefreshDormants();
      //var u = await _get.GetByID("bcdrj3leku"); var iDormant = _to.ToDormantType(u);

      // Clearing
      //var u = await _get.GetByID(DomainModels.User.sBusinessTesterID); await _clearing.Join(u, false);
      //var sHash = await _clearing.SaveTransaction("err%5Bid%5D=414&err%5Bmessage%5D=%D7%90%D7%A8%D7%A2%D7%94+%D7%A9%D7%92%D7%99%D7%90%D7%94+-+%D7%A0%D7%90+%D7%9C%D7%A0%D7%A1%D7%95%D7%AA+%D7%91%D7%A9%D7%A0%D7%99%D7%AA+%D7%90%D7%95+%D7%9C%D7%A4%D7%A0%D7%95%D7%AA+%D7%9C%D7%9E%D7%95%D7%A7%D7%93+%D7%94%D7%A9%D7%99%D7%A8%D7%95%D7%AA&status=0&data%5Bid%5D=108431&data%5Bnew_payment_id%5D=108432&data%5Btoken%5D=49fc8b6e18319f7d34db0e5fe80dcc14&data%5Bnew_token%5D=6f0cfc009cfa8d985d3810df0c0dc923&company_api_extra_details=9kfwb8fbga");
      //var b = _clearing.GetCardBrand(sHash); var cc = _clearing.GetDetailByHash(sHash, "data[card_suffix]"); var sAmount = _clearing.GetDetailByHash(sHash, "data[sum]");
      //var sURL = await _meshulam.GetURL("abcd", "לקוח לדוגמא", "05420001234", "1234.56", "עסק לדוגמא");
      //var s = "err=&status=1&data%5Bid%5D=108357&data%5Bpayment_sum%5D=1%2C234.56&data%5Bfirst_payment_sum%5D=0&data%5Bperiodical_payment_sum%5D=0&data%5Bpayments_num%5D=0&data%5Ball_payments_num%5D=1&data%5Bpayment_date%5D=28%2F1%2F20&data%5Basmachta%5D=45440466&data%5Bbusiness_title%5D=%D7%A2%D7%96%D7%A8%D7%90&data%5Bpayment_desc%5D=%D7%AA%D7%A9%D7%9C%D7%95%D7%9D+%D7%A2%D7%9C+%D7%A1%D7%9A+1234.56+%D7%9C%D7%98%D7%95%D7%91%D7%AA+%D7%A2%D7%A1%D7%A7+%D7%9C%D7%93%D7%95%D7%92%D7%9E%D7%90&data%5Bfull_name%5D=%D7%9C%D7%A7%D7%95%D7%97+%D7%9C%D7%93%D7%95%D7%92%D7%9E%D7%90&data%5Bpayer_phone%5D=0000000000&data%5Bpayer_email%5D=&data%5Btz%5D=000000000&data%5Bcard_suffix%5D=4580&data%5Bcard_type%5D=Foreign&data%5Bcard_type_code%5D=2&data%5Bcard_brand%5D=Visa&data%5Bcard_brand_code%5D=3&data%5Bcard_exp%5D=0120&data%5Bpayment_type%5D=2&data%5Btoken%5D=8111c448729f1fdd78663078b54b53cb&data%5Bsum%5D=1234.56&company_api_extra_details=abcd"; await _clearing.SaveTransaction(s);
      //var sData = "status=1&err=&data%5Bapi_key%5D=c237665b374d&data%5Buser_id%5D=c16cb0e1ee89b5b9&data%5Bbusiness_title%5D=%D7%90%D7%99%D7%AA%D7%9F+%D7%A4%D7%99%D7%9C%D7%A8%D7%A1%D7%93%D7%95%D7%A8%D7%A3&data%5Bname%5D=%D7%90%D7%99%D7%AA%D7%9F+%D7%A4%D7%99%D7%9C%D7%A8%D7%A1%D7%93%D7%95%D7%A8%D7%A3&data%5Bphone%5D=0548000195&data%5Bpackage_id%5D=1&data%5Bpackage_name%5D=%D7%9C%D7%90+%D7%A1%D7%9C%D7%A7%D7%AA+%D7%9C%D7%90+%D7%A9%D7%99%D7%9C%D7%9E%D7%AA+%28%D7%96%D7%99%D7%9B%D7%95%D7%99+%D7%97%D7%95%D7%93%D7%A9%D7%99%29&data%5Btracking_code%5D=d97f0b056a7e&data%5Btracking_status%5D%5Bid%5D=3&data%5Btracking_status%5D%5Bmessage%5D=%D7%94%D7%A2%D7%A1%D7%A7+%D7%94%D7%95%D7%A7%D7%9D+%D7%91%D7%94%D7%A6%D7%9C%D7%97%D7%94"; await _clearing.AfterSignup(sData);
      //var sTrackingCode = "3fea1016b27b"; var sMeshulamUserID = "d0e1664219379e21"; await _clearing.Register(sTrackingCode, sMeshulamUserID);
      //await _clearing.Register("3693ed100d46", "aa736fc6463b7613");
      //var uBU = await _get.GetByID("cj0gsr8izu"); await _clearing.Register(uBU, "f424c10375cc7856");

      //var u = await _get.GetByID("cfmcgpefyh"); await _client.ImportBusinessClients(u, "xlsx");
      //var u = await _get.GetByID("ipwrfaaq4t"); await _client.SetAllClientsAsTemp(u);
      //var u = await _get.GetByID("ipwrfaaq4t"); await _client.SetAllClientsAsFix(u);
      // Pipedrive
      //await PipeDrive.AddFollower("14566", PipeDrive.Users.Sales1);
      //await PipeDrive.AddActivity("Call", "14586", DateTime.UtcNow, "aaa");
      //await PipeDrive.AddDealDo("Label Deal", PipeDrive.Stages.Lead, "14206", "14214", PipeDrive.Users.Sales1, "testing", PipeDrive.PDOfficeTypes.Accountant, PipeDrive.Labels.Unknown);
      //await _ms.ImportXLSLeadsTarget();
      //await _ms.ImportXLSLeadsBizPhone();
      //await _ms.ImportXLSLeadsGoldenPages();
      //await PipeDrive.UpdateDealField("15432", PipeDrive.Fields.MeaningfulCallCounter, DateTime.Now.Second.ToString());
      //var s = await PipeDrive.GetDealField("15432", PipeDrive.Fields.MeaningfulCallCounter);
      //var i = await PipeDrive.IncDealField("13988", PipeDrive.Fields.MeaningfulCallCounter);
      //var details = await PipeDrive.GetDealDetails("14325");
      //var sDealID = await PipeDrive.GetDealIDByEmail("0523280261", false);
      //var sDealID = await PipeDrive.GetPersonIDByEmail("089430515", false);
      //var sNumber = Validation.ProperMobile("972523280261"); var sDealID = await PipeDrive.GetDealIDByEmail(sNumber, false);
      //await PipeDrive.UpdatePerson("14214", null, "איתי", "a@a.com", "1111");
      //await PipeDrive.UpdatePerson("14214", null, "רוח תרגול", "trainingac@gotoezra.com", "0542240290");
      //var dts = await PipeDrive.GetActivities(PipeDrive.Users.Support1);
      //await PipeDrive.UpdateDealOwnerFollowersAll();
      //await PipeDrive.UpdateDealOwnerFollowersStage(PipeDrive.Stages.FirstUsers);
      //await PipeDrive.UpdateDealOwnerFollowersPipe(PipeDrive.Pipes.Leads);
      //var d = await PipeDrive.GetDealDetails("19352"); await PipeDrive.UpdateDealOwnerFollowers(d);
      //await _ms.AddBKsToPipeDrive();
      //await PipeDrive.ListDealFields();
      //await _rs.DeleteAllFromBULabelDeals();
      //await _ms.RefreshActiveUsers();
      //await _ms.UpdateACStages();
      //await _ms.UpdateACDealNames();
      //var uAC = await _get.GetByID("ccv8ol2q4j"); await _pipe.UpdateStage(uAC); ItemsQueue.Update(uAC);

      //await _client.DeleteAllUserClients();
      //await _client.MoveClientsBetweenUsers();

      // RingOver
      //_uSearch.AddCaller(DomainModels.User.sVladi, "0542240290");
      //_uSearch.AddCaller(DomainModels.User.sVladi, "0507853792");

      // Sales
      //await _sales.CallEnded(null);
      //var w = WorkerService.aWorkers[WorkerService.WorkerID.Telemeeting2]; await _salesData.SendReport(w.sSalesLogID, new DateTime(2020, 09, 29));
      //await _salesData.SendReport(SalesLogService.Users.TeleMeeting2, new DateTime(2020, 09, 24));
      //await _salesData.SendReports(DateTime.Now.Date.AddDays(-1));

      // Load check
      //var tasks = new List<Task>(); for (var i = 0; i < 1000; i++) tasks.Add(_dget.Get("a8k164zav6")); await Task.WhenAll(tasks); // Comment DBService.cs Line 56
      //for (var i = 0; i < 200; i++) QueryServer.QueryServerBackground("http://localhost:65216/loadtest?" + DateTime.Now.Ticks);
      //for (var i = 0; i < 2000; i++) QueryServer.QueryServerBackground("http://api.paperless.tax/loadtest?" + DateTime.Now.Ticks);
      //var dt = new DateTime(637134336000000000);
      //await _tasks.MigrateToTasksQueue();

      // Tags
      //var uBU = await _get.GetByID("bgn7d0643k"); var uAC = await _get.GetAccountant(uBU); await _tags.PrepareUserTags(uAC, uBU); var tag = _tags.GetByForeignKey("110"); var aTags = new List<string>() { tag.iID.ToString() };
      //await _tags.RefactorShowInPL();
      //await _tags.CleanWater();

      // Finbot
      //var uBU = await _get.GetByID("cg8fccmkv3"); var uAC = await _get.GetAccountant(uBU); await _importFinbot.Check(uBU);
      //var uBU = await _get.GetByID("cg8fccmkv3"); var uAC = await _get.GetAccountant(uBU); await _importFinbot.Import(uBU, uAC);
      //var uAC = await _get.GetByID("b4dsoul2pb"); var uBUs = await _get.GetAllAccountantUsersFull(uAC); foreach (var uBU in uBUs) await _importFinbot.UploadImages(uBU);
      //var uBU = await _get.GetByID("cp4yz158k9"); await _importFinbot.UploadImages(uBU);
      //var uBU = await _get.GetByID("j5hvtege47"); await _importFinbotImages.UploadImages(uBU, Documents.DocType.Income);

      // Backup
      //await _backupDB.Backup();
      //await _restore.RestoreMany();
      //await _restore.RestoreOneDocument<Document>("gi9q31gxkh"); // ge3dudiupa,gi9q31gxkh,gi9q4ej7f1
      //await _restore.RestoreOneDocument<User>("b52tegykov");
      //await _restore.RestoreManyAccounts();
      //await _restore.RestoreManyARsByIDs();
      //var uAC = await _get.GetByID("bomsa0dmka"); await _backup.GetUnsignedDocs(uAC);


      // Activity
      //var w = WorkerService.aWorkers[WorkerService.WorkerID.Sales1]; var u = await _get.GetByID(w.sPaperlessID); await _activity.Log(u, ActivityState.Extend, "CallEnded", DateTime.Now.SetTime(10,17));

      // Notifications
      //var u = await _get.GetByID(DomainModels.User.sBusinessTesterID); _feed.Push(u, NotificationTypes.Support, "Upload_Logo");
      //var uBU = await _get.GetByID(Users.User.sBusinessTesterID); var uAC = await _get.GetAccountant(uBU); await _feed.Push(uAC, FeedNotificationTypes.Message, new List<string>() { _to.ToName(uBU) + " מנסה להעלות מסמכים למערכת ללא חבילת הבסיס", "goto", "/admin/users-packages" });
      //await _ms.SplitNotificationPolicy();

      // Log
      //var u = await _get.GetAdmin(); _log.Log(LogGroupTypes.Activity, LogTypes.Activity_Set, "Test", "Body", u);
      //await _log.BuildHistogram();
      //await _log.SearchWhatsAppUploaders();

      // App
      //await _billing.AddAppIcon();
      // Run Test From Browser
      //await _send.NotifyUrgentAppVerUpdateMessage();
      //await _send.NotifySystemDown();

      // tickets
      //await _tickets.AutoClose();
      //await _ticketsAdmin.SetPrivate();
      //await _ticketsAdmin.SetSupportChatTypes();
      //await _tickets.NotifyBeforeDoneByEmail("i6m6pcohlw");

      //var u = await _get.GetByID("dp6k0thl0x"); await _notify.NotifyIfPaturReachCeiling(u, null);
      //var d = await _dget.Get("dno8lij0pg", null); var sURL = _dto.GetImagePreSignedURL(d, DocumentsToService.sImageSuffixFull); await GeminaService.Process(sURL, d.sID);
      return;

      // Test on prod ios app from store
      //await _send.PushNotificationAdmin("שלום", "כאן עזרא", "document/5fnz5zxd9c");
      //var User = await Models._get.GetByID("hwdh3vmu2"); await User.PushNotification("שלום", "כאן עזרא", "document/55eys2jsvj");
      //await Models.User.ReadyToProcessReminder();
      //User User = await Models._get.GetByID(User.sBusinessTesterID); await User.SendWelcomeEmail("123456", "a", "a", "");
      //var Document = await Models.Document.Get("jgvhippui"); await Document.CreateThumbnail();

      //5lswy1dj8v      
      //var Document = await Models.Document.Get("5iusnqzx1a"); await Document.Analyze(); // PDF
      //var Document = await Models.Document.Get("2bjolkw88u"); await Document.Analyze(); // PDF
      //var Document = await Models.Document.Get("26y43e9pl9"); await Document.Analyze();
      //await Document.Analyze("26wimq72hb");
      //await PipeDrive.GetDealIDByEmail("Chenoffice.adi@gmail.com");
      //await FacebookController.GetLeadDetails("1091257314358519");
      //await PipeDrive.GetDealIDByEmail("fg@spetz.org");
      //var sDealID = await PipeDrive.GetDealIDByEmail("0525949799");
      //await Models.User.Disable("2i8uvhcurw");
      //var User = await Models._get.GetByID("lxy8fanpr"); await User.AllowIncome(DateTime.Today.AddYears(1));
      //await Document.CalcFeedTime();
      //await Models.User.SendInquiryReminders();
      //await Models.User.SendTrialMailAC("nimrodr@savetax.co.il", "נמרוד", "רטנר", "0523934284");
      //await Models.User.SendTrialMailAC("itay@gotoezra.com", "נמרוד", "רטנר", "0523934284");
      //var aDocuments = await Document.GetByAccountantID("28473evvlf");
      //var User = await Models._get.GetByID("jqrpxsr8z"); await User.LoginReset();
      //var User = await Models.User.GetAdmin(); await User.InitStats();
      //var User = await Models._get.GetByID(User.sBusinessTesterID); await User.SendBusinessWelcomeEmail(await User.GetAccountant(), "aa");
      //await Models.User.SendLoginFirstReminers();
      //var u = await _get.GetByID("7x82s1hfbm"); await _send.SendBusinessWelcomeMessages(u);

      //var Document = await Models.Document.Get("5x0os046z6"); await Document.ProcessImage(0, true);
      //var User = await Models._get.GetByID(User.sBusinessTesterID); await User.SendEmailTemplateGeneral("מספר ואותיות", "גגג: " + "<span style='direction:ltr'>1abc</span>", null, null);

      //var User = await Models._get.GetByID(Models.User.sBusinessTesterID); await Billing.Billing.GetToken(User, "4580960101040223", "1223", "025395617", "069");
      //await Billing.Billing.ChargeUnpaidPackages();
      //var User = await Models._get.GetByID(Models.User.sBusinessTesterID);  await User.NotifyChargeFailure();
      //await Billing.Billing.CreatePendingInvoices();
      //await Billing.Billing.ResetPayingUsers();
      //await Billing.Billing.ResetPayingCashUsers();
      //await _billing.SendChargeReminders();
      //var User = await Models._get.GetByID("4f2es56ohj"); await Billing.ChargeUnpaidPackages(User);
      //var document = await Document.Get("63zhzmhp0b"); await document.CreateAnalyzer();
      //var document = await Document.Get("6466esn4qj"); await document.CreateAnalyzer(); // Pango Resized
      //var document = await Document.Get("641m2fi2lp"); await document.CreateAnalyzer(); // Pango
      //var document = await Document.Get("6ckdcy7qfh"); await document.PostProcess();
      //await _ms.CleanACs();

      //var uAC = await _get.GetByID("2arjqfnrg6"); var bAC = await _billing.GetForUser(uAC); await _billing.UpdatePackagesAC(uAC, bAC, 20 * 100, 10 * 100, 45 * 100, 2250); // Udi
      //var uAC = await _get.GetByID("3r5ypril8x"); var bAC = await _billing.GetForUser(uAC); await _billing.UpdatePackagesAC(uAC, bAC, 0, 5 * 100, 50 * 100, 25 * 100); // Amit
      //var uAC = await _get.GetByID("5780w55mxu"); var bAC = await _billing.GetForUser(uAC); await _billing.UpdatePackagesAC(uAC, bAC, 5 * 100, 5 * 100, 0, 0); // Doron 19.02.19
      //var uAC = await _get.GetByID("26jha3k5ic"); var bAC = await _billing.GetForUser(uAC); await _billing.UpdatePackagesAC(uAC, bAC, 20 * 100, 10 * 100, 40 * 100, 20 * 100); // Dekel, 20% off, WhatsApp 06.01.19
      //await EmailRaw.SendEmail("return to", "Hi all");
      //var dt = TimeStamp.TimeStampToDateTime("0273560400000");
      //await _dget.CalcSuppliersSum();
      //await _fb.GetLeadDetails("701297880304282");
      //await _dget.PreProcessImageAllImages();
      //await _get.FixInvoiceLogoExt();
      //await _exportCards.CleanACTags();

      //await _us.SetPeriodStart();
      //var u = await _get.GetByID("86plsv676m"); await _ureport.NotifyUserDigitalSuppliers(u);
      //await _ureport.RefactorReport();
      //var u = await _get.GetByID("fvts9rohv"); var u2 = await _get.GetByID("5hxj27nv7j"); u2.sFirstName = "aaa";  var d = StringLib.TextDiff(u, u2);
      //await _get.AssociateEzraBK();
      //await _tasks.RefactorList();

      //var u = await _get.GetByID("4yciblydim"); Queue.QueueBackgroundWorkItem(ct => _ureport.NotifyUserMissingIncomeInvoices(u));
    }
  }
}
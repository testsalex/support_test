using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Paperless.Backup;
using Paperless.Documents.Update;
using Paperless.ImageProcessing;
using Paperless.Imports;
using Paperless.Queue;
using Paperless.Tasks;
using Paperless.Users.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paperless.Functions
{
  [Obsolete]
  public class SupportFunction : MyFunctionHelper
  {
    private const string sSupportAPIToken = "hrgs0ppPqU63zg18vu0G";
    // Domain Services
    private readonly IActivityService _activity;
    private readonly IArdaniDocumentsService _ardani;
    private readonly IArdaniAdminService _ardaniAdmin;
    private readonly IArdaniIndexService _ardaniIndex;
    private readonly IBackupDBService _backupDB;
    private readonly IBooksService _books;
    private readonly IBooksGetService _booksGet;
    //private readonly CacheService _cache;
    private readonly IClearingService _clearing;
    private readonly IClientService _client;
    private readonly DocumentsAdminDeleteService _dAdminDelete;
    private readonly IDocumentsGetService _dget;
    private readonly IDocumentsIsService _dis;
    private readonly IDocumentsToService _dto;
    private readonly IDoxsService _doxs;
    private readonly DoxsMigrate _doxsmigrate;
    private readonly IMeshulamService _meshulam;
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
    private readonly InvoicesService _invoice;
    private readonly MovementsAdminService _mAdmin;
    private readonly MigrateService _migrate;
    private readonly IMiscService _miscs; // Better not using books when getting documents
    private readonly UsersManagerService _ms;
    private readonly RestoreDBService _restore;
    private readonly IUsersAccountService _rs;
    private readonly ISalesLogService _sales;
    private readonly ISalesDataService _salesData;
    private readonly ISalesLogService _salesLog;
    private readonly ISendService _send;
    private readonly IAutomateQueueSenderService _servicebus;
    private readonly ISuppliersGetClientService _sget;
    private readonly ISuppliersUpdateClientService _sUpdate;
    private readonly ITagsService _tags;
    private readonly TasksQueueService _tasks;
    private readonly TasksQueueSenderService _tasksQ;
    //private readonly TasksService _tasks;
    //private readonly TasksListService _tasksList;
    private readonly ITicketsService _tickets;
    private readonly IDocumentsWhatsAppService _whatsapp;
    //private readonly UserReportService _ureport;
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
    public SupportFunction(
      IUsersGetService get,
      IUsersIsService @is,
      IUsersToService to,
      ILogService log,

      IClientLib clientLib,
      IEmailRaw emailRaw,

      DocumentsAdminDeleteService dAdminDelete,
      IDocumentsGetService dget,
      IDocumentsIsService dis,
      IDocumentsToService dto,

      IEmail email,

      IActivityService activity,
      IArdaniAdminService ardaniAdmin,
      IArdaniDocumentsService ardani,
      IArdaniIndexService ardaniIndex,
      IBackupDBService backup,
      IBooksService books,
      IBooksGetService booksGet,
      //CacheService _cache,
      IClearingService clearing,
      IClientService client,
      IDoxsService doxs,
      DoxsMigrate doxsmigrate,
      IMeshulamService meshulam,
      IAutomateQueueSenderService servicebus,
      TasksQueueService tasks,
      //TasksService _tasks,
      //TasksListService _tasksList,

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
      InvoicesService invoice,
      MovementsAdminService mAdmin,
      MigrateService migrate,
      IMiscService miscs,
      UsersManagerService ms,
      ITagsService tags,
      RestoreDBService restore,
      IUsersAccountService rs,
      ISalesLogService sales,
      ISalesDataService salesData,
      ISalesLogService salesLog,
      ISuppliersGetClientService sget,
      ISuppliersUpdateClientService sUpdate,
      ITicketsService tickets,
      IDocumentsWhatsAppService whatsapp,
      //UserReportService _ureport,
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
      ISendService send,
      TasksQueueSenderService tasksQ
    ) : base(clientLib, emailRaw, itemsQueue, get, @is, to, log)
    {
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
      _booksGet = booksGet;
      //this._cache = _cache;
      _clearing = clearing;
      _client = client;
      _dAdminDelete = dAdminDelete;
      _dget = dget;
      _dis = dis;
      _dto = dto;
      _doxs = doxs;
      _doxsmigrate = doxsmigrate;
      _meshulam = meshulam;
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
      _invoice = invoice;
      _mAdmin = mAdmin;
      _migrate = migrate;
      _movement = movement;
      _miscs = miscs;
      _ms = ms;
      _sales = sales;
      _salesData = salesData;
      _salesLog = salesLog;
      _send = send;
      _servicebus = servicebus;
      _restore = restore;
      _rs = rs;
      _sget = sget;
      _sUpdate = sUpdate;
      _tags = tags;
      _tasks = tasks;
      _tasksQ = tasksQ;
      //this._tasksList = _tasksList;
      _tickets = tickets;
      _whatsapp = whatsapp;
      //this._ureport = _ureport;
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

    [FunctionName("PingSupport")]
    public IActionResult PingSupport(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/ping")] HttpRequest req)
    {
      // Be sure to change Prefix
      // http://localhost:7089/support/ping
      // https://plsupport.azurewebsites.net/support/ping
      // https://pl-support-prod.azurewebsites.net/support/ping
      // https://api.paperless.tax/support/ping
      var sRet = GetUploadTime();
      return new OkObjectResult(sRet);
    }
    //[HttpPost]
    //[Route("support/runscript")]
    //public async Task RunScript([FromBody] dynamic data)
    //{
    [FunctionName("RunScript")]
    public async Task RunScript(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "support/runscript")] HttpRequest req,
      ILogger log)
    {
      var data = GetPostData(req);
      var uAC = await _get.GetCurrentUser(); // null if from index.html
      var bCan = _clientLib.IsLocalHost(); // && ClientLib.IsHome();
      if (!bCan) throw new UnauthorizedAccessException();
      string
        s = data.i,
        sUserID = data.sUserID,
        sText = data.sText,
        sDocID = data.sDocID; // if called from sDocumentID route
      var uBU = await _get.GetByID(sUserID);
      var i = Convert.ToInt32(s);
      List<int> a;
      //if (_is.IsAdmin(uAC, true) && !i.In(16)) throw new NotImplementedException();
      if (_is.IsAdmin(uAC) && !i.In(4, 5, 8, 14, 16, 17, 25, 27, 30, 31, 32, 33, 34, 41, 56, 64, 65, 69)) throw new NotImplementedException();
      if (_is.IsBookKeeper(uAC)) uAC = await _get.GetAccountant(uBU ?? uAC); // Allow admin to run script when on BU page
      if (!i.In(2, 3, 4, 5, 8, 9, 10, 11, 14, 15, 16, 20, 22, 23, 24, 25, 28, 32, 36, 38, 39, 41, 42, 46, 47, 48,
        49, 50, 52, 54, 55, 56, 57, 61, 63, 65, 66, 67, 71)) ;
      switch (i)
      {
        case 1: await _ardaniAdmin.FixArdaniTags(uAC); break;
        case 2: await _dAdminDelete.DeletePending(uBU ?? uAC, sText); break;
        //case 3: await _us.MakePersonal(uBU.sID); break;
        //case 4: await _ms.MigrateBUToAC(uBU, sText); break;
        case 5: _ms.SetSMSLess(uBU ?? uAC); break;
        case 6: await _billingUser.UpdateBillingStartAC(uAC); break;
        case 7: { var d = await _dget.Get(sDocID, null); var uOwner = await _dget.GetOwner(d); uAC = await _get.GetAccountant(uOwner); await _ms.MigrateEzraBKByAC(uAC); break; }
        case 8: await _get.GetByMobileBU(Validation.ProperMobile(sText.Trim())); break;
        //case 9: await _dms.MovePendingDocsToAnotherBU(uBU, sText); break;
        case 10: if (sText == "YES") await _rs.DeleteAC(uAC); break;
        case 11: await _billing.AddOneTimeRefund(uBU ?? uAC, (int)(Convert.ToDouble(sText) * 100)); break; // שלילי זה זיכוי. סכום באגורות
        case 12: await _billingPackage.StopPackageClearing(uBU ?? uAC); break; // First BU if from AC else BU if BU
        case 13: await _billingPackage.StopPackageBrandedApp(uAC); break;
        case 14: await _backupDB.Backup(); break;
        case 15: await _ardaniAdmin.ReplaceDocumentTags(uAC); break;
        case 16: await _dms.PostProcessUserPendingDocs(uBU); break;
        //case 17: var u = uBU ?? uAC; var r = await _booksGet.GetForUser(u); await _movement.RefreshAllReports(u, r); break;
        //case 18: await _ms.MigrateArdaniEzraBKDocs(); break;
        //case 19: await _dms.MoveAllDocumentsToAnotherBU(uBU, sText); break;
        case 20: await _tags.PrepareUserTags(uAC); _ardaniIndex.FixFuel(uAC); break; // החלפת דלק מונית בדלק כלי רכב
        case 21: await _dAdminDelete.UndoLastImport(uBU); break;
        case 22: await _tags.CleanTags(uAC); break;
        //case 23: await _us.DisableBK(sText); break;
        case 24: await _clearing.Register(uBU ?? uAC, sText); break;
        case 25: await _dIncoming.ProcessIncomingEmail(null); break;
        case 26: await _mAdmin.FixObsoleteSuppliers(uBU); break;
        case 27: await _billingUser.RevokeCC(uBU ?? uAC); break;
        case 28: a = ToNumbers(); await _tags.PrepareUserTags(uAC); _tags.AddForeignCard(a[0], a[1], null, a[2] == 1); break; // TT // 10047,101,1 // 10001,9001,0 כרטיס תרומות 10001 ישוייך ל 9001 בלי אקסטרא
        case 29: _tags.SwitchUserTags(uAC, 1030, Convert.ToInt32(sText)); break; // החלפת הכנסות מעמלות עם הכנסות ממתן שירותים ללקוחות ישנים
        //case 30: await _log.DeleteStale(); break;
        case 31: await _dupload.FinishFailedUploads((uBU ?? uAC).sID); break;
        case 32: await _us.RemoveLogo(uBU ?? uAC); break;
        case 33: await _movement.RemoveOrphanMovements(uBU); break;
        case 34: await _movement.BuildFromDocuments(uBU ?? uAC); break;
        case 35: await _dms.UpdateMyInvoicesCounter(uBU ?? uAC); break;
        case 36: await AddMissingPeriods(uBU ?? uAC); break;
        case 37: a = ToNumbers(); var t = await _tags.PrepareUserTags(uAC); _tags.ReplaceForeignKey(uAC, a[0], a[1]); break; // מייצג רוצה שכל ההכנסות יהיו רק 101 במקום גם 101 וגם 110 ולכן 1030,101
        case 38: await _movement.BuildFromDocument(sDocID); break;
        case 39: { var d = await _dget.Get(sDocID, null); await _inquiries.CloseUserInquiries(d); break; }
        case 40: await _ardaniIndex.RemoveACTagsWithMissingFKs(uAC); break;
        case 41:
          {
            var d = await _dget.Get(sDocID, null);
            var sURL = _dto.GetImagePreSignedURL(d, DocumentsConstants.sImageSuffixFull);
            await GeminaService.Process(sURL, d.sID);
            break;
          }
        //case 42: await _us.JoinBKaaSSet(uAC); break;
        case 43: if (sText == "YES") await _billingRefund.RefundInvoice(sDocID); break;
        //case 44: await _us.JoinAutoPaymentSet(uAC); break;
        case 45: await _ardaniAdmin.RestoreFromTagNameBU(uAC, uBU, null); break;
        case 46: await _dmigrate.MigrateACSpecificTags(uBU, sText); break;
        case 47: _itemsQueue.QSMS((uBU ?? uAC).sMobile, "הודעת בדיקה"); break;
        //case 48: await _dms.AssociatePendingToEzraBK(uBU); break;
        case 49: await _dms.ReanalyzeUserPendingDocs(uBU); break;
        case 50: await _dms.ConvertPaturReceipt(sDocID); break;
        case 51: await _ardaniIndex.PromoteTagID(uAC, sText); break;
        case 52: await RefreshAllOpenReports(uBU ?? uAC); break;
        case 53: _us.MarkWasFinbot(uAC); break;
        case 54: { var r = await _booksGet.GetForUser(uBU ?? uAC); /*_doxsmigrate.MigrateBU(uBU ?? uAC, r);*/ break; }
        case 55: { var r = await _booksGet.GetForUser(uBU ?? uAC); _doxs.RefreshUserStep(uBU ?? uAC, r); break; }
        case 56: await _doxs.RefreshOpenDoxs(uBU ?? uAC); break;
        case 57: _us.MarkSMSReceive(uAC); break;
        case 58: await _ardaniAdmin.SaveTagName(uAC); break;
        case 59: await _ardaniIndex.UpdateTags(uAC, null); break; // Use latest full
        case 60: await _ardaniAdmin.RestoreFromTagName(uAC); break;
        case 61: await _doxs.CloseReportedPeriods(uBU ?? uAC); break;
        case 62: await UpdateAllACBUsPending(uAC); break;
        case 63: var dt = Dates.ToIsraelTime(false).AddMinutes(1); await _tasksQ.Schedule(dt, TaskTypes.SchedulerTest, "63"); break;
        case 64: await _sUpdate.InvalidateCache(); break;
        case 65: await _us.JoinAutoPaymentAsk(uAC); break;
        //case 65: _doxs.ReopenPLReview(uBU); break;
        case 66: await _dms.SplitVAT(sDocID); break;
        case 67: await _ddelete.DeleteInvoice(sDocID, sText); break;
        case 68: await _us.RemoveAllSwitchUsers(uBU ?? uAC); break;
        case 69: await _mAdmin.CreateVATBalanceMovements(uBU); break;
        case 70: await _dms.MergeClients(uBU ?? uAC); break;
        case 71: _us.Disconnect(uBU ?? uAC); break;
      }
      List<int> ToNumbers() { return sText.Split(',').Select(s => Convert.ToInt32(s)).ToList(); }
    }
    [FunctionName("RunAction")]
    public async Task RunAction(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "support/action")] HttpRequest req,
    ILogger log)
    {
      var supportDTO = JsonConvert.DeserializeObject<SupportDTO>(myreq.sBody);
      if (supportDTO.sAuthKey != sSupportAPIToken) throw new UnauthorizedAccessException();
      //var u = await _get.GetByID(supportDTO.sOwnerID);
      var t = await _tickets.GetByID(supportDTO.sTicketID);
      var uBU = await _get.GetByID(t.sBUID);
      var uOwner = await _tickets.GetOwner(t);
      switch (supportDTO.iType)
      {
        case SupportActionTypes.MeshulamCode: await _clearing.Register(uOwner, supportDTO.sText); break;
        case SupportActionTypes.RefreshOpenDoxs: await _doxs.RefreshOpenDoxs(uBU); break;
        case SupportActionTypes.RevokeCC: await _billingUser.RevokeCC(uBU ?? uOwner); break;
        case SupportActionTypes.FromFinbot: _us.MarkWasFinbot(uOwner); break;
        case SupportActionTypes.JoinCPAA: await _us.JoinAutoPaymentAsk(uOwner); break;
        case SupportActionTypes.Reprocess: await Reprocess(t.sRoute.Replace("/admin/document;sDocumentID=", "")); break; // "/admin/document;sDocumentID=idcqvuprlv",
        default: throw new NotImplementedException();
      }
    }
    private async Task UpdateAllACBUsPending(IUser uAC)
    {
      // 26.06.22 Manually run from maintenance when needed
      var aBUs = await _get.GetAllAccountantUsersFull(uAC);
      foreach (var uBU in aBUs)
      {
        var aDocumentsDTO = await _dget.GetPendingDocsAll(uBU, uAC);
        _miscs.RefreshPendingDocsBU(uBU, uAC, aDocumentsDTO); // Counters are for all pending docs
      }
    }
    public async Task AddMissingPeriods(IUser uBU)
    {
      var aPeriods = Period.SpanPeriods("2001", "2212");
      await RefreshReports(uBU, aPeriods);
    }
    public async Task RefreshAllOpenReports(IUser uBU)
    {
      var r = await _booksGet.GetForUser(uBU);
      var aPeriods = _books.GetAllOpenPeriods(r); // Get all not reported periods
      aPeriods.Remove(Period.CalcCurrentPeriod()); // Remove current as it is not reported now
      await RefreshReports(uBU, aPeriods); // Recreate reports
    }
    private async Task RefreshReports(IUser uBU, List<string> aPeriods)
    {
      if (_is.IsCompany(uBU))
        await _movement.RefreshReports(uBU, aPeriods); // Recreate reports
      else
        await _dms.RefreshReports(uBU, aPeriods); // Supports multiple months
    }
    //[HttpGet]
    //[Route("support/getts/{sID}")]
    //public string GetTimeStamp(string sID)
    //{
    [FunctionName("GetTimeStamp")]
    public string GetTimeStamp(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/getts/{sID}")] HttpRequest req,
      string sID, ILogger log)
    {
      // http://localhost:7089/support/getts/h16qzq5fe3
      // https://api.paperless.tax/support/getts/h16qzq5fe3
      return TimeStamp.UniqueIDToDateTime(sID).ToString();
    }
    //[HttpPut]
    //[Route("support/updatefreshchatrestoreid/{sRestoreID}")]
    //public async Task UpdateFreshchatRestoreID(string sRestoreID)
    //{
    [FunctionName("UpdateFreshchatRestoreID")]
    public async Task UpdateFreshchatRestoreID(
      [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "support/updatefreshchatrestoreid/{sRestoreID}")] HttpRequest req,
      string sRestoreID, ILogger log)
    {
      var u = await GetCurrentUser();
      u.sFreshchatRestoreID = sRestoreID;
      _itemsQueue.Update(u);
    }
    //[HttpPost]
    //[Route("support/sendsteps")]
    //public async Task SendSteps([FromBody] dynamic data)
    //{
    //[FunctionName("SendSteps")]
    //public async Task SendSteps(
    //  [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "support/sendsteps")] HttpRequest req,
    //  ILogger log)
    //{
    //  var data = GetPostData(req);
    //  var u = await _get.GetCurrentUser(); // null if from index.html
    //  if (!_is.IsSupport(u) &&
    //    !new[] { 
    //      //"cjnjyd8h0p", // Rivka
    //      "cp3y58wno7", // Ronit
    //      "cjnnvpf2ax",  // Rivka
    //      "b1nr9vhsbg", // שוקי
    //      "dq5yur6w5u", // הודיה
    //    }.Contains(u.sID)) throw new UnauthorizedAccessException();

    //  string
    //    sUserID = data.sUserID,
    //    sName = data.sName;

    //  var uTarget = Validation.IsValidMobileNumber(sUserID) ?
    //    await _get.GetByMobileBU(sUserID) :
    //    await _get.GetByID(sUserID);
    //  await _feed.Push(uTarget, FeedNotificationTypes.Support, sName);
    //}
    //[HttpGet]
    //[Route("documents/update/reprocess/{sDocumentID}")]
    //public async Task Reprocess(string sDocumentID)
    //{
    [FunctionName("Reprocess")]
    public async Task Reprocess(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/reprocess/{sDocumentID}")] HttpRequest req,
      string sDocumentID, ILogger log)
    {
      // Used by Admin from document component
      var u = await GetCurrentUser();
      if (!_is.IsAdmin(u, true) && !_clientLib.IsLocalHost()) throw new UnauthorizedAccessException();
      await Reprocess(sDocumentID);

      //if (ClientLib.IsLocalHost() || _is.IsTester(u))
      //{
      //await _postProcessingQ.ReProcess(sDocumentID);
      //await _analyzerQ.Analyze(sDocumentID);
      //}

      //for (var i = 0; i < 50; i++) QueryServer.QueryFunctionBackground("postprocess", sDocumentID); // For load testing
      //await QueryServer.QueryFunction("reprocess", sDocumentID);
      ////var d = await _dget.Get(sDocumentID);
      ////var bSuccess = await _dprocess.ProcessImage(d, 0, true);
      //return Ok();
    }
    private async Task Reprocess(string sDocumentID)
    {
      var d = await _dget.Get(sDocumentID, null);
      if (_dis.IsMyInvoice(d))
        await _invoice.CreateInvoice(d.sID, null); // Creates PDF and sub-images only after commit. Can re-run also if failed before based on d,u,c
      else
        _itemsQueue.QueuePostProcess(sDocumentID, false);
    }
    //[HttpGet]
    //[Route("analyzer/reanalyze/{sDocumentID}")]
    //public async Task<ActionResult> reanalyze(string sDocumentID)
    //{
    [FunctionName("Reanalyze")]
    public async Task Reanalyze(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/reanalyze/{sDocumentID}")] HttpRequest req,
      string sDocumentID, ILogger log)
    {
      // Used by Admin from document component
      var u = await GetCurrentUser();
      //if (!IsAdmin(u) && !ClientLib.IsLocalHost()) throw new UnauthorizedAccessException();
      if (!_is.IsAdmin(u, true) && !_clientLib.IsLocalHost()) throw new UnauthorizedAccessException();
      _itemsQueue.QueueAnalyzer(sDocumentID, null);
    }
  }
}

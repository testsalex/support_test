using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyFunctionHelper;
using Paperless.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paperless.Functions
{
  [Obsolete]
  public class SuppliersAdminFunction : MyFunctionHelper
  {
    private readonly IActivityService _activity;
    private readonly IDocumentsManagerService _dms;
    private readonly ISuppliersManagerService _sAdmin;
    private readonly ISuppliersGetClientService _sgetClient;
    private readonly ISuppliersUpdateService _supplier;
    private readonly ISuppliersUpdateClientService _sUpdate;

    public SuppliersAdminFunction(
      IUsersGetService get,
      IUsersIsService @is,
      IUsersToService to,
      ILogService log,

      IClientLib clientLib,
      IEmailRaw emailRaw,
      IItemsQueue itemsQueue,

      IActivityService activity,
      IDocumentsManagerService dms,
      ISuppliersManagerService sAdmin,
      ISuppliersGetClientService sgetClient,
      ISuppliersUpdateService supplier,
      ISuppliersUpdateClientService sUpdate
    ) : base(clientLib, emailRaw, itemsQueue, get, @is, to, log)
    {
      _activity = activity;
      _dms = dms;
      _sAdmin = sAdmin;
      _sgetClient = sgetClient;
      _supplier = supplier;
      _sUpdate = sUpdate;
    }

    [FunctionName("TestPlayGroundSuppliers")]
    public async Task TestPlayGroundSuppliers(
     [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/suppliers/testplayground")] HttpRequest req,
     ILogger log)
    {
      // http://localhost:7089/support/suppliers/testplayground
      if (!_clientLib.IsLocalHost()) throw new UnauthorizedAccessException();
      //var d = await _dget.Get("8at1hk9sx8"); var u = await _get.GetAdmin(); var uOwner = await _dget.GetOwner(d); await _ddelete.Delete(d, uOwner, u);
      //await _supplier.Loop();
      //await _supplier.EncodeHash();
      switch (5) // 5
      {
        case 0: await _sUpdate.Ping(); break;
        //case 1: await _sget.Dump(); break;
        case 2: await _sAdmin.CleanAll(); break;
        //case 3: await _dus.MergeNamesAll(); break;
        //case 4: await _dus.MergeNumbersAll(); break;
        case 5: await _sAdmin.MergeDuplicates(); break;
        case 6: await _sAdmin.LoadBalance(); break;
        case 7: await _sUpdate.RemoveFromCache("aa"); break;
        case 8: await _sAdmin.ImportCompanies(); break;
        //case 8.5: await _sAdmin.ImportMunis(); break;
        case 9: await _sAdmin.CleanRashamCompanies(); break;
        case 10: await _sAdmin.CleanEmptyNames(); break;
        case 11: await _sAdmin.RemoveImageHash("JDYiMEFxcZd3C1AseeBhg1sAks0"); break;
      }
    }
    //[HttpGet]
    //[Route("suppliers/getlist/{iList}")]
    //public async Task<List<SupplierDTO>> GetList(int iList)
    [FunctionName("GetList")]
    public async Task<PascalJsonResult<List<SupplierDTO>>> GetList(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/suppliers/getlist/{iList}")] HttpRequest req,
      int iList, ILogger log)
    {
      // Used by Search and document
      var u = await GetCurrentUser();
      if (!IsAdmin(u) && !_is.IsEzraAC(u.sID) && !_is.IsSupplierSpecialist(u)) throw new UnauthorizedAccessException(); // sUserID is null when Admin checks deleted docs
      var suppliers = await _sAdmin.GetList(iList);
      return new PascalJsonResult<List<SupplierDTO>>(suppliers);
    }
    //[HttpPut]
    //[Route("suppliers/clean")]
    //public async Task<SupplierDTO> Clean([FromBody] dynamic data)
    [FunctionName("Clean")]
    public async Task<PascalJsonResult<SupplierDTO>> Clean(
      [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "support/suppliers/clean")] HttpRequest req,
      ILogger log)
    {
      var data = GetPostData(req);
      var u = await GetCurrentUser();
      var bCan = _is.IsAdmin(u) || _is.IsEzraAC(u.sID) || _clientLib.IsLocalHost() || _is.IsSupplierSpecialist(u);
      if (!bCan) throw new UnauthorizedAccessException(); // Only local admin or maya (remote ezraac)
      var sSupplierID = (string)data.id;
      var iType = (int)data.type;
      var sKey = (string)data.key;
      if (iType == 8)
      { await _dms.DetachSupplier(sSupplierID); return null; }
      else
      {
        var s = await _sAdmin.Clean(sSupplierID, iType, sKey);
        return new PascalJsonResult<SupplierDTO>(s);
      }
    }
    //[HttpPut]
    //[Route("suppliers/merge")]
    //public async Task<SupplierDTO> Merge([FromBody] dynamic data)
    [FunctionName("Merge")]
    public async Task<PascalJsonResult<SupplierDTO>> Merge(
      [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "support/suppliers/merge")] HttpRequest req,
      ILogger log)
    {
      var data = GetPostData(req);
      var u = await GetCurrentUser();
      var bCan = _is.IsAdmin(u) || _is.IsEzraAC(u.sID) || _clientLib.IsLocalHost();
      if (!bCan) throw new UnauthorizedAccessException(); // Only local admin or maya (remote ezraac)
      var sSupplierID1 = (string)data.id1;
      var sSupplierID2 = (string)data.id2;
      var (sToKeep, sToDelete) = await _sAdmin.MergeSuppliers(sSupplierID1, sSupplierID2);
      var s = _supplier.CreateSupplierDTO(sToKeep, true);
      return new PascalJsonResult<SupplierDTO>(s);
    }
    [FunctionName("Merges")]
    public async Task<string> Merges(
     [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "support/suppliers/merges")] HttpRequest req,
     ILogger log)
    {
      var data = GetPostData(req);
      var u = await GetCurrentUser();
      var bCan = _is.IsAdmin(u) || _is.IsEzraAC(u.sID) || _clientLib.IsLocalHost();
      if (!bCan) throw new UnauthorizedAccessException(); // Only local admin or maya (remote ezraac)
      var aSupplierIDs = (List<string>)data.aIDs.ToObject<List<string>>();
      var dSuppliers = await _sgetClient.GetByIDs(aSupplierIDs); // Load batch
      aSupplierIDs = dSuppliers.Values.OrderBy(s => s.nCount + (s.IsLocked() ? 10000 : 0)).Select(s => s.sID).ToList(); // Prefer locked
      var sToKeepID = aSupplierIDs.Last(); // The one with most documents
      var sToKeep = dSuppliers[sToKeepID]; // Keeping the one with most documents
      for (var i = 0; i < aSupplierIDs.Count - 1; i++) // Excluding last
      {
        var sToDeleteID = aSupplierIDs[i];
        var sToDelete = dSuppliers[sToDeleteID];
        var (sToKeep2, sToDelete2) = await _sAdmin.MergeSuppliers(sToKeepID, sToDeleteID);
        if (sToKeepID != sToKeep2.sID) break; // Protection. If suppliers order changed because number of docs differ from s.nCount, break and retry again manually
      }
      await _activity.Log(u, ActivityState.Extend);
      return sToKeepID;
    }
    //[HttpPost]
    //[Route("suppliers/replacevalue")]
    //public async Task<SupplierDTO> ReplaceValue([FromBody] dynamic data)
    [FunctionName("ReplaceValue")]
    public async Task<PascalJsonResult<SupplierDTO>> ReplaceValue(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "support/suppliers/replacevalue")] HttpRequest req,
      ILogger log)
    {
      var data = GetPostData(req);
      var uAC = await _get.GetCurrentUser(); // null if from index.html
      if (!_is.IsAdmin(uAC) && !_is.IsEzraAC(uAC.sID) && !_clientLib.IsLocalHost()) throw new UnauthorizedAccessException();
      string
        sPrev = data.sPrev,
        sNew = data.sText,
        sType = data.iType,
        sSupplierID = data.sSupplierID;
      var sDTO = await _sAdmin.ReplaceValue(sSupplierID, sType, sPrev, sNew);
      return new PascalJsonResult<SupplierDTO>(sDTO);
    }
    [FunctionName("New")]
    public async Task<PascalJsonResult<SupplierDTO>> New(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "support/suppliers/new")] HttpRequest req,
      ILogger log)
    {
      var data = GetPostData(req);
      var uAC = await _get.GetCurrentUser(); // null if from index.html
      if (!_is.IsAdmin(uAC) && !_is.IsEzraAC(uAC.sID) && !_clientLib.IsLocalHost()) throw new UnauthorizedAccessException();
      string
        sSupplierName = data.sSupplierName,
        sSupplierNumber = data.sSupplierNumber;
      var p = new SupplierParams
      {
        sNumber = sSupplierNumber,
        sName = sSupplierName,
      };
      var s = await _supplier.UpdateSupplier(p);
      var sDTO = _supplier.CreateSupplierDTO(s, true);
      return new PascalJsonResult<SupplierDTO>(sDTO);
    }
    [FunctionName("AddNumber")]
    public async Task<PascalJsonResult<SupplierDTO>> AddNumber(
      [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "support/suppliers/addnumber")] HttpRequest req,
      ILogger log)
    {
      var data = GetPostData(req);
      var u = await GetCurrentUser();
      var bCan = _is.IsAdmin(u) || _is.IsEzraAC(u.sID) || _clientLib.IsLocalHost();
      if (!bCan) throw new UnauthorizedAccessException(); // Only local admin or maya (remote ezraac)
      var sSupplierID = (string)data.sSupplierID;
      var sSupplierNumber = (string)data.sSupplierNumber;
      var bIsIhud = (bool)data.bIsIhud;
      var s = await _sAdmin.AddNumber(sSupplierID, sSupplierNumber, bIsIhud);
      return new PascalJsonResult<SupplierDTO>(s);
    }
  }
}
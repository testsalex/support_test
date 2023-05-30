using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Paperless.Ardani;
using Paperless.Users.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paperless.Functions
{
  [Obsolete]
  public class ArdaniAdminFunction : MyFunctionHelper
  {
    private readonly IArdaniDocumentsService _ardaniDocs;
    private readonly IArdaniIndexService _ardaniIndex;
    private readonly IArdaniUsersService _ardaniUser;
    private readonly IArdaniAccountService _ardaniRS;
    private readonly UsersDTOGetService _ugetDTO;
    //private readonly MiscService _miscs;
    //private readonly TagsService _tags;
    //private readonly DocumentsGetService _dget;
    //private readonly DocumentsIsService _dis;
    //private readonly DocumentsToService _dto;
    public ArdaniAdminFunction(
      IClientLib clientLib,
      IEmailRaw emailRaw,
      IItemsQueue itemsQueue,
      IUsersGetService get,
      IUsersIsService @is,
      IUsersToService to,
      ILogService log,

      //DocumentsGetService _dget,
      //DocumentsIsService _dis,
      //DocumentsToService _dto,

      IArdaniDocumentsService ardaniDocs,
      IArdaniIndexService ardaniIndex,
      IArdaniUsersService ardaniUser,
      IArdaniAccountService ardaniRS,
      UsersDTOGetService ugetDTO
    //MiscService _miscs
    //TagsService _tags
    ) : base(clientLib, emailRaw, itemsQueue, get, @is, to, log)
    {
      //this._dget = _dget;
      //this._dis = _dis;
      //this._dto = _dto;

      _ardaniDocs = ardaniDocs;
      _ardaniIndex = ardaniIndex;
      _ardaniUser = ardaniUser;
      _ardaniRS = ardaniRS;
      _ugetDTO = ugetDTO;
      //this._miscs = _miscs;
      //this._tags = _tags;
    }

    [FunctionName("TestPlayGroundArdani")]
    public async Task TestPlayGroundArdani(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "support/ardani/testplayground")] HttpRequest req,
      ILogger log)
    {
      // http://localhost:7089/support/ardani/testplayground
      if (!_clientLib.IsLocalHost()) throw new UnauthorizedAccessException();
      //await _ardaniUser.FillSyncDate2();
      switch (116)
      {
        case 1:
          {
            for (var i = 0; i < 30; i++)
              await _emailRaw.SendEmail("aa: " + i);
            break;
          }
        case 3: // Full
          {
            var uAC = await _get.GetByID("gk2jsv7vmp"); // Ardani_UpdateTags
            await _ardaniIndex.UpdateTags(uAC, null);
            break; // Use latest full
          }

        case 110:
          {
            var (data, l) = await _log.GetData("g6lripb0tr"); // Ardani_UpdateDocuments_Req
            var users_docs = (Dictionary<string, List<ArdaniDocument>>)data.ToObject<Dictionary<string, List<ArdaniDocument>>>(); // http://stackoverflow.com/questions/13565245/convert-newtonsoft-json-linq-jarray-to-a-list-of-specific-object-type
            var uAC = await _get.GetByID(l.sID1);
            //uAC = await GetACByDocID(users_docs.Values.First()[0].MisparTnuaa);
            await _ardaniDocs.UpdateDocuments(uAC, users_docs);
          }
          break;

        case 111:
          {
            var (data, l) = await _log.GetData("f4fzqp8gzs");
            var uAC = await _get.GetByID(l.sID1);
            var dUserIDs = await _ardaniRS.SignupBUs(uAC, data);
            break;
          }

        case 113:
          {
            var (data, l) = await _log.GetData("gpcbdevabv"); // Ardani_GetDocuments_Req
            var uAC = await _get.GetByID(l.sID1);
            var aArdaniIDs = (Dictionary<string, long>)data.ToObject<Dictionary<string, long>>(); // http://stackoverflow.com/questions/13565245/convert-newtonsoft-json-linq-jarray-to-a-list-of-specific-object-type
            var dto = await _ardaniDocs.GetDocuments(uAC, aArdaniIDs, false);
            break;
          }

        case 114: // Single index
          {
            var (data, l) = await _log.GetData("gnfi8v2l6x"); // Ardani_UpdateTags
            var uAC = await _get.GetByID(l.sID1);
            await _ardaniIndex.UpdateTags(uAC, data);
            break;
          }

        case 115:
          {
            var (data, l) = await _log.GetData("gql6qdgzpb"); // Ardani_DocumentsImport_Req
            var uAC = await _get.GetByID(l.sID1);
            var users_docs = (Dictionary<string, List<ArdaniDocument>>)data.ToObject<Dictionary<string, List<ArdaniDocument>>>(); // http://stackoverflow.com/questions/13565245/convert-newtonsoft-json-linq-jarray-to-a-list-of-specific-object-type
            var ret = await _ardaniDocs.Import(uAC, users_docs);
            break;
          }

        case 116:
          {
            var (data, l) = await _log.GetData("hsxbtcz596"); // Ardani_SignupBUs_Before
            var uAC = await _get.GetByID(l.sID1);
            var ret = await _ardaniRS.SignupBUs(uAC, data);
            break;
          }
      }
    }
  }
}

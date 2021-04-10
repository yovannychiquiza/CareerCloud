using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeServiceImp : SystemLanguageCodeService.SystemLanguageCodeServiceBase
    {
        public override Task<SystemLanguageCodeObj> GetSystemLanguageCode(IdRequestSystemLanguageCode request, ServerCallContext context)
        {
            var _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
            SystemLanguageCodePoco poco = _logic.Get(request.LanguageID);

            if (poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return TranslateFromPoco(poco);
        }

        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodeObjs request, ServerCallContext context)
        {

            var _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(item.LanguageID);
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CreateSystemLanguageCode(SystemLanguageCodeObjs request, ServerCallContext context)
        {
            var _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();

            foreach (var item in request.Obj)
            {
                pocos.Add(TranslateFromProto(item, new SystemLanguageCodePoco()));
            }

            _logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodeObjs request, ServerCallContext context)
        {
            var _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
            List<SystemLanguageCodePoco> pocos = new List<SystemLanguageCodePoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(item.LanguageID);
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private Task<SystemLanguageCodeObj> TranslateFromPoco(SystemLanguageCodePoco poco)
        {
            return Task.FromResult(new SystemLanguageCodeObj
            {
                LanguageID = poco.LanguageID,
                Name = poco.Name,
                NativeName = poco.NativeName
            });
        }

        private SystemLanguageCodePoco TranslateFromProto(SystemLanguageCodeObj proto, SystemLanguageCodePoco poco)
        {
            poco.LanguageID = proto.LanguageID;
            poco.Name = proto.Name;
            poco.NativeName = proto.NativeName;
            return poco;
        }
    }
}

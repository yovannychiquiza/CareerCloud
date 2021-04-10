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
    public class SecurityLoginsLogServiceImp : SecurityLoginsLogService.SecurityLoginsLogServiceBase
    {
        public override Task<SecurityLoginsLogObj> GetSecurityLoginsLog(IdRequestSecurityLoginsLog request, ServerCallContext context)
        {
            var _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return TranslateFromPoco(poco);
        }

        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogObjs request, ServerCallContext context)
        {

            var _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CreateSecurityLoginsLog(SecurityLoginsLogObjs request, ServerCallContext context)
        {
            var _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();

            foreach (var item in request.Obj)
            {
                pocos.Add(TranslateFromProto(item, new SecurityLoginsLogPoco()));
            }

            _logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogObjs request, ServerCallContext context)
        {
            var _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
            List<SecurityLoginsLogPoco> pocos = new List<SecurityLoginsLogPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private Task<SecurityLoginsLogObj> TranslateFromPoco(SecurityLoginsLogPoco poco)
        {
            return Task.FromResult(new SecurityLoginsLogObj
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                SourceIP = poco.SourceIP,
                LogonDate = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.LogonDate, DateTimeKind.Utc)),
                IsSuccesful = poco.IsSuccesful
            });
        }

        private SecurityLoginsLogPoco TranslateFromProto(SecurityLoginsLogObj proto, SecurityLoginsLogPoco poco)
        {
            poco.Id = Guid.Parse(proto.Id);
            poco.Login = Guid.Parse(proto.Login);
            poco.SourceIP = proto.SourceIP;
            poco.LogonDate = proto.LogonDate.ToDateTime();
            poco.IsSuccesful = proto.IsSuccesful;
            return poco;
        }
    }
}

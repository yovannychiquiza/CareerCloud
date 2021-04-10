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
    public class SecurityLoginServiceImp : SecurityLoginService.SecurityLoginServiceBase
    {
        public override Task<SecurityLoginObj> GetSecurityLogin(IdRequestSecurityLogin request, ServerCallContext context)
        {
            var _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
            SecurityLoginPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return TranslateFromPoco(poco);
        }

        public override Task<Empty> DeleteSecurityLogin(SecurityLoginObjs request, ServerCallContext context)
        {

            var _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CreateSecurityLogin(SecurityLoginObjs request, ServerCallContext context)
        {
            var _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();

            foreach (var item in request.Obj)
            {
                pocos.Add(TranslateFromProto(item, new SecurityLoginPoco()));
            }

            _logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateSecurityLogin(SecurityLoginObjs request, ServerCallContext context)
        {
            var _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
            List<SecurityLoginPoco> pocos = new List<SecurityLoginPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private Task<SecurityLoginObj> TranslateFromPoco(SecurityLoginPoco poco)
        {
            return Task.FromResult(new SecurityLoginObj
            {
                Id = poco.Id.ToString(),
                Login = poco.Login,
                Password = poco.Password,
                Created = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.Created, DateTimeKind.Utc)),
                PasswordUpdate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.PasswordUpdate, DateTimeKind.Utc)),
                AgreementAccepted = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.AgreementAccepted, DateTimeKind.Utc)),
                IsLocked = poco.IsLocked,
                IsInactive = poco.IsInactive,
                EmailAddress = poco.EmailAddress,
                PhoneNumber = poco.PhoneNumber,
                FullName = poco.FullName,
                ForceChangePassword = poco.ForceChangePassword,
                PrefferredLanguage = poco.PrefferredLanguage,
        });
        }

        private SecurityLoginPoco TranslateFromProto(SecurityLoginObj proto, SecurityLoginPoco poco)
        {
            poco.Id = Guid.Parse(proto.Id);
            poco.Login = proto.Login;
            poco.Password = proto.Password;
            poco.Created = proto.Created.ToDateTime();
            poco.PasswordUpdate = proto.PasswordUpdate.ToDateTime();
            poco.AgreementAccepted = proto.AgreementAccepted.ToDateTime();
            poco.IsLocked = proto.IsLocked;
            poco.IsInactive = proto.IsInactive;
            poco.EmailAddress = proto.EmailAddress;
            poco.PhoneNumber = proto.PhoneNumber;
            poco.FullName = proto.FullName;
            poco.ForceChangePassword = proto.ForceChangePassword;
            poco.PrefferredLanguage = proto.PrefferredLanguage;
            return poco;
        }
    }
}

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
    public class ApplicantProfileServiceImp : ApplicantProfileService.ApplicantProfileServiceBase
    {
        public override Task<ApplicantProfileObj> GetApplicantProfile(IdRequestApplicantProfile request, ServerCallContext context)
        {
            var _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            ApplicantProfilePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return TranslateFromPoco(poco);
        }

        public override Task<Empty> DeleteApplicantProfile(ApplicantProfileObjs request, ServerCallContext context)
        {

            var _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CreateApplicantProfile(ApplicantProfileObjs request, ServerCallContext context)
        {
            var _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();

            foreach (var item in request.Obj)
            {
                pocos.Add(TranslateFromProto(item, new ApplicantProfilePoco()));
            }

            _logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateApplicantProfile(ApplicantProfileObjs request, ServerCallContext context)
        {
            var _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            List<ApplicantProfilePoco> pocos = new List<ApplicantProfilePoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private Task<ApplicantProfileObj> TranslateFromPoco(ApplicantProfilePoco poco)
        {
            return Task.FromResult(new ApplicantProfileObj
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                CurrentSalary = poco.CurrentSalary is null ? 0.0 : (double)poco.CurrentSalary,
                CurrentRate = poco.CurrentRate is null ? 0.0 : (double)poco.CurrentRate,
                Currency = poco.Currency,
                Country = poco.Country,
                Province = poco.Province,
                Street = poco.Street,
                City = poco.City,
                PostalCode = poco.PostalCode
            });
        }

        private ApplicantProfilePoco TranslateFromProto(ApplicantProfileObj proto, ApplicantProfilePoco poco)
        {
            poco.Id = Guid.Parse(proto.Id);
            poco.Login = Guid.Parse(proto.Login);
            poco.CurrentSalary = (Decimal)proto.CurrentSalary;
            poco.CurrentRate = (Decimal)proto.CurrentRate;
            poco.Currency = proto.Currency;
            poco.Country = proto.Country;
            poco.Province = proto.Province;
            poco.Street = proto.Street;
            poco.City = proto.City;
            poco.PostalCode = proto.PostalCode;
            return poco;
        }
    }
}

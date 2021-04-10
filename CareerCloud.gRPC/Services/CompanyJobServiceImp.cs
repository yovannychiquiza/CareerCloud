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
    public class CompanyJobServiceImp : CompanyJobService.CompanyJobServiceBase
    {
        public override Task<CompanyJobObj> GetCompanyJob(IdRequestCompanyJob request, ServerCallContext context)
        {
            var _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return TranslateFromPoco(poco);
        }

        public override Task<Empty> DeleteCompanyJob(CompanyJobObjs request, ServerCallContext context)
        {

            var _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CreateCompanyJob(CompanyJobObjs request, ServerCallContext context)
        {
            var _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();

            foreach (var item in request.Obj)
            {
                pocos.Add(TranslateFromProto(item, new CompanyJobPoco()));
            }

            _logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateCompanyJob(CompanyJobObjs request, ServerCallContext context)
        {
            var _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
            List<CompanyJobPoco> pocos = new List<CompanyJobPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private Task<CompanyJobObj> TranslateFromPoco(CompanyJobPoco poco)
        {
            return Task.FromResult(new CompanyJobObj
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                ProfileCreated = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.ProfileCreated, DateTimeKind.Utc)),
                IsInactive = poco.IsInactive,
                IsCompanyHidden = poco.IsCompanyHidden
            });
        }

        private CompanyJobPoco TranslateFromProto(CompanyJobObj proto, CompanyJobPoco poco)
        {
            poco.Id = Guid.Parse(proto.Id);
            poco.Company = Guid.Parse(proto.Company);
            poco.ProfileCreated = proto.ProfileCreated.ToDateTime();
            poco.IsInactive = proto.IsInactive;
            poco.IsCompanyHidden = proto.IsCompanyHidden;
            return poco;
        }
    }
}

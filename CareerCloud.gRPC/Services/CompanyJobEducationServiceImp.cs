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
    public class CompanyJobEducationServiceImp : CompanyJobEducationService.CompanyJobEducationServiceBase
    {
        public override Task<CompanyJobEducationObj> GetCompanyJobEducation(IdRequestCompanyJobEducation request, ServerCallContext context)
        {
            var _logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
            CompanyJobEducationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return TranslateFromPoco(poco);
        }

        public override Task<Empty> DeleteCompanyJobEducation(CompanyJobEducationObjs request, ServerCallContext context)
        {

            var _logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CreateCompanyJobEducation(CompanyJobEducationObjs request, ServerCallContext context)
        {
            var _logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();

            foreach (var item in request.Obj)
            {
                pocos.Add(TranslateFromProto(item, new CompanyJobEducationPoco()));
            }

            _logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateCompanyJobEducation(CompanyJobEducationObjs request, ServerCallContext context)
        {
            var _logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
            List<CompanyJobEducationPoco> pocos = new List<CompanyJobEducationPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private Task<CompanyJobEducationObj> TranslateFromPoco(CompanyJobEducationPoco poco)
        {
            return Task.FromResult(new CompanyJobEducationObj
            {
                Id = poco.Id.ToString(),
                Major = poco.Major,
                Job = poco.Job.ToString(),
                Importance = (int)poco.Importance,
            });
        }

        private CompanyJobEducationPoco TranslateFromProto(CompanyJobEducationObj proto, CompanyJobEducationPoco poco)
        {
            poco.Id = Guid.Parse(proto.Id);
            poco.Major = proto.Major;
            poco.Job = Guid.Parse(proto.Job);
            poco.Importance = (short)proto.Importance;
            return poco;
        }
    }
}

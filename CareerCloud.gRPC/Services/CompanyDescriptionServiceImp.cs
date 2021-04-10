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
    public class CompanyDescriptionServiceImp : CompanyDescriptionService.CompanyDescriptionServiceBase
    {
        public override Task<CompanyDescriptionObj> GetCompanyDescription(IdRequestCompanyDescription request, ServerCallContext context)
        {
            var _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
            CompanyDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return TranslateFromPoco(poco);
        }

        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptionObjs request, ServerCallContext context)
        {

            var _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
            List<CompanyDescriptionPoco> pocos = new List<CompanyDescriptionPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CreateCompanyDescription(CompanyDescriptionObjs request, ServerCallContext context)
        {
            var _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
            List<CompanyDescriptionPoco> pocos = new List<CompanyDescriptionPoco>();

            foreach (var item in request.Obj)
            {
                pocos.Add(TranslateFromProto(item, new CompanyDescriptionPoco()));
            }

            _logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptionObjs request, ServerCallContext context)
        {
            var _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
            List<CompanyDescriptionPoco> pocos = new List<CompanyDescriptionPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private Task<CompanyDescriptionObj> TranslateFromPoco(CompanyDescriptionPoco poco)
        {
            return Task.FromResult(new CompanyDescriptionObj
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                LanguageId = poco.LanguageId.ToString(),
                CompanyName = poco.CompanyName,
                CompanyDescription = poco.CompanyDescription,
            });
        }

        private CompanyDescriptionPoco TranslateFromProto(CompanyDescriptionObj proto, CompanyDescriptionPoco poco)
        {
            poco.Id = Guid.Parse(proto.Id);
            poco.Company = Guid.Parse(proto.Company);
            poco.LanguageId = proto.LanguageId;
            poco.CompanyName = proto.CompanyName;
            poco.CompanyDescription = proto.CompanyDescription;
            return poco;
        }
    }
}

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
    public class ApplicantEducationServiceImp : ApplicantEducationService.ApplicantEducationServiceBase
    {
        public override Task<ApplicantEducationObj> GetApplicantEducation(IdRequestApplicantEducation request, ServerCallContext context)
        {
            var _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
            ApplicantEducationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return TranslateFromPoco(poco);
        }

        public override Task<Empty> DeleteApplicantEducation(ApplicantEducationObjs request, ServerCallContext context)
        {

            var _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CreateApplicantEducation(ApplicantEducationObjs request, ServerCallContext context)
        {
            var _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();

            foreach (var item in request.Obj)
            {
                pocos.Add(TranslateFromProto(item, new ApplicantEducationPoco()));
            }

            _logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateApplicantEducation(ApplicantEducationObjs request, ServerCallContext context)
        {
            var _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private Task<ApplicantEducationObj> TranslateFromPoco(ApplicantEducationPoco poco)
        {
            return Task.FromResult(new ApplicantEducationObj
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Major = poco.Major,
                CertificateDiploma = poco.CertificateDiploma,
                StartDate = poco.StartDate is null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind(poco.StartDate.GetValueOrDefault(), DateTimeKind.Utc)),
                CompletionDate = poco.CompletionDate is null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind(poco.CompletionDate.GetValueOrDefault(), DateTimeKind.Utc)),
                CompletionPercent = poco.CompletionPercent is null ? 0 : (int)poco.CompletionPercent
            });
        }

        private ApplicantEducationPoco TranslateFromProto(ApplicantEducationObj proto, ApplicantEducationPoco poco)
        {
            poco.Id = Guid.Parse(proto.Id);
            poco.Applicant = Guid.Parse(proto.Applicant);
            poco.Major = proto.Major;
            poco.CertificateDiploma = proto.CertificateDiploma;
            poco.StartDate = proto.StartDate.ToDateTime();
            poco.CompletionDate = proto.CompletionDate.ToDateTime();
            poco.CompletionPercent = Convert.ToByte(proto.CompletionPercent);
            return poco;
        }
    }
}

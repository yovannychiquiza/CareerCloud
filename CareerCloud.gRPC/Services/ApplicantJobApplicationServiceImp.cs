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
    public class ApplicantJobApplicationServiceImp : ApplicantJobApplicationService.ApplicantJobApplicationServiceBase
    {
        public override Task<ApplicantJobApplicationObj> GetApplicantJobApplication(IdRequestApplicantJobApplication request, ServerCallContext context)
        {
            var _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
            ApplicantJobApplicationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return TranslateFromPoco(poco);
        }

        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplicationObjs request, ServerCallContext context)
        {

            var _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CreateApplicantJobApplication(ApplicantJobApplicationObjs request, ServerCallContext context)
        {
            var _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();

            foreach (var item in request.Obj)
            {
                pocos.Add(TranslateFromProto(item, new ApplicantJobApplicationPoco()));
            }

            _logic.Add(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplicationObjs request, ServerCallContext context)
        {
            var _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
            List<ApplicantJobApplicationPoco> pocos = new List<ApplicantJobApplicationPoco>();

            foreach (var item in request.Obj)
            {
                var poco = _logic.Get(Guid.Parse(item.Id));
                pocos.Add(TranslateFromProto(item, poco));
            }

            _logic.Update(pocos.ToArray());
            return Task.FromResult(new Empty());
        }

        private Task<ApplicantJobApplicationObj> TranslateFromPoco(ApplicantJobApplicationPoco poco)
        {
            return Task.FromResult(new ApplicantJobApplicationObj
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
                ApplicationDate = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.ApplicationDate, DateTimeKind.Utc)),
            });
        }

        private ApplicantJobApplicationPoco TranslateFromProto(ApplicantJobApplicationObj proto, ApplicantJobApplicationPoco poco)
        {
            poco.Id = Guid.Parse(proto.Id);
            poco.Applicant = Guid.Parse(proto.Applicant);
            poco.Job = Guid.Parse(proto.Job);
            poco.ApplicationDate = proto.ApplicationDate.ToDateTime();
            return poco;
        }
    }
}

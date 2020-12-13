using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(
            IDataRepository<CompanyJobDescriptionPoco> repo): base(repo)
        {}

        public override void Add(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var item in pocos)
            {
                if (string.IsNullOrEmpty(item.JobName))
                {
                    exceptions.Add(new ValidationException(300, $"{item.Id} JobName cannot be empty"));
                }
                if (string.IsNullOrEmpty(item.JobDescriptions))
                {
                    exceptions.Add(new ValidationException(301, $"{item.Id} JobDescriptions cannot be empty"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}

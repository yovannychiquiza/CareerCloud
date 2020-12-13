using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(
            IDataRepository<ApplicantSkillPoco> repo): base(repo)
        {}

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var item in pocos)
            {
                if (item.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101, $"Cannot be greater than 12"));
                }
                if (item.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, $"Cannot be greater than 12"));
                }
                if (item.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(103, $"Cannot be less then 1900"));
                }
                if (item.StartYear > item.EndYear)
                {
                    exceptions.Add(new ValidationException(104, $"Cannot be less then StartYear"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}

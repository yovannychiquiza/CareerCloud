using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(
            IDataRepository<CompanyDescriptionPoco> repo): base(repo)
        {}

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var item in pocos)
            {
                if (string.IsNullOrEmpty(item.CompanyDescription) || item.CompanyDescription.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"{item.Id} Must be greater then 2 characters"));
                }
                if (string.IsNullOrEmpty(item.CompanyName) || item.CompanyName.Length < 3)
                {
                    exceptions.Add(new ValidationException(106, $"{item.Id} Must be greater then 2 characters"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}

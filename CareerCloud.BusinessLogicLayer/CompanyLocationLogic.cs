using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(
            IDataRepository<CompanyLocationPoco> repo): base(repo)
        {}

        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var item in pocos)
            {
                if (string.IsNullOrEmpty(item.CountryCode))
                {
                    exceptions.Add(new ValidationException(500, $"{item.Id} CountryCode cannot be empty"));
                }
                if (string.IsNullOrEmpty(item.Province))
                {
                    exceptions.Add(new ValidationException(501, $"{item.Id} Province cannot be empty"));
                }
                if (string.IsNullOrEmpty(item.Street))
                {
                    exceptions.Add(new ValidationException(502, $"{item.Id} CountryCode cannot be empty"));
                }
                if (string.IsNullOrEmpty(item.City))
                {
                    exceptions.Add(new ValidationException(503, $"{item.Id} CountryCode cannot be empty"));
                }
                if (string.IsNullOrEmpty(item.PostalCode))
                {
                    exceptions.Add(new ValidationException(504, $"{item.Id} CountryCode cannot be empty"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}

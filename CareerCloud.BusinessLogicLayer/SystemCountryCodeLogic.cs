using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic : BaseLogic<SystemCountryCodePoco>
    {
        public SystemCountryCodeLogic(
            IDataRepository<SystemCountryCodePoco> repo): base(repo)
        {}

        public override void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var item in pocos)
            {
                if (string.IsNullOrEmpty(item.Code))
                {
                    exceptions.Add(new ValidationException(900, $"{item.Id} cannot be empty"));
                }
                if (string.IsNullOrEmpty(item.Name))
                {
                    exceptions.Add(new ValidationException(901, $"{item.Id} cannot be empty"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}

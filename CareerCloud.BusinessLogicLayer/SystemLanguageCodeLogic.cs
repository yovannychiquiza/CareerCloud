using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic : BaseLogic<SystemLanguageCodePoco>
    {
        public SystemLanguageCodeLogic(
            IDataRepository<SystemLanguageCodePoco> repo): base(repo)
        {}

        public override void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var item in pocos)
            {
                if (string.IsNullOrEmpty(item.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, $"{item.Id} LanguageID cannot be empty"));
                }
                if (string.IsNullOrEmpty(item.Name))
                {
                    exceptions.Add(new ValidationException(1001, $"{item.Id} Name cannot be empty"));
                }
                if (string.IsNullOrEmpty(item.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, $"{item.Id} NativeName cannot be empty"));
                }               
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public SystemLanguageCodePoco Get(string id)
        {
            return _repository.GetSingle(c => c.LanguageID == id);
        }
    }
}

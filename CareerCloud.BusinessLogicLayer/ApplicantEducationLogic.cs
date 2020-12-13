﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(
            IDataRepository<ApplicantEducationPoco> repo): base(repo)
        {}

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var item in pocos)
            {
                if (string.IsNullOrEmpty(item.Major))
                {
                    exceptions.Add(new ValidationException(107, $"{item.Id} Cannot be empty or less than 3 characters"));
                }
                else if(item.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"{item.Id} Cannot be empty or less than 3 characters"));
                }
                if(item.StartDate > DateTime.Now)
                {
                    exceptions.Add(new ValidationException(108, $"Cannot be greater than today"));
                }
                if(item.CompletionDate < item.StartDate)
                {
                    exceptions.Add(new ValidationException(109, $"CompletionDate cannot be earlier than StartDate"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
            //base.Verify(pocos);
        }
    }
}

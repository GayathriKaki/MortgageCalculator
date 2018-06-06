using System;
using System.Collections.Generic;
using System.Linq;
using MortgageCalculator.Dto;

namespace MortgageCalculator.Api.Repos
{
    public interface IMortgageRepo
    {
        List<Mortgage> GetAllMortgages();
    }

    public class MortgageRepo : IMortgageRepo
    {
        public List<Mortgage> GetAllMortgages()
        {
            using (var context = new MortgageData.MortgageDataContext())
            {
                var mortgages = context.Mortgages.ToList();
                List<Mortgage> result = new List<Mortgage>();
                foreach (var mortgage in mortgages)
                {
                    result.Add(new Mortgage()
                    {
                        Name = mortgage.Name,
                        EffectiveStartDate = mortgage.EffectiveStartDate,
                        EffectiveEndDate = mortgage.EffectiveEndDate,
                        CancellationFee = mortgage.CancellationFee,
                        EstablishmentFee = mortgage.EstablishmentFee,
                        InterestRepayment = (InterestRepayment)Enum.Parse(typeof(InterestRepayment), mortgage.InterestRepayment.ToString()),
                        MortgageId = mortgage.MortgageId,
                        MortgageType = (MortgageType)Enum.Parse(typeof(MortgageType), mortgage.MortgageType.ToString()),
                        TermsInMonths = (mortgage.EffectiveEndDate.Month - mortgage.EffectiveStartDate.Month) + 12 * (mortgage.EffectiveEndDate.Year - mortgage.EffectiveStartDate.Year),
                        MortgageTypeValue = Enum.GetName(typeof(MortgageType), mortgage.MortgageType),
                        InterestRate = mortgage.InterestRate 
                    }
                    );
                }

                result = result.Where(t => t.EffectiveStartDate < DateTime.Now && t.EffectiveEndDate > DateTime.Now).ToList<Mortgage>();
                result = result.OrderBy(m => m.MortgageTypeValue).ThenBy(n => n.InterestRate).ToList<Mortgage>();

                return result;
            }
        }
    }
}
using System;

namespace FinancialDic.Lib.Entity
{
    [Serializable]
    public class Word : VL2.Prevalence.Entity.BaseEntity
    {
        public string Portuguese { get; set; }

        public string English { get; set; }
    }
}
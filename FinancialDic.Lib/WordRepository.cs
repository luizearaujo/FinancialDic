using FinancialDic.Lib.Entity;
using VL2.Prevalence.Repository;

namespace FinancialDic.Lib
{
    public class WordRepository
    {
        public GenericRepository<Word> Repository { get; set; }

        public WordRepository()
        {
            Repository = new GenericRepository<Word>();
        }
    }
}

using System;
using System.IO;
using FinancialDic.Lib.Entity;
using VL2.Prevalence.Repository;

namespace FinancialDic.Load
{
    class Program
    {
        private static GenericRepository<Word> _repository;

        static void Main(string[] args)
        {
            var loadFile = File.ReadAllLines(@"c:\users\luiz.araujo\documents\visual studio 2015\Projects\FinancialDic\FinancialDic.Load\load.csv");

            _repository = new GenericRepository<Word>();

            var count = 0;

            foreach (var line in loadFile)
            {
                var words = line.Split(';');

                count ++;

                var word = new Word()
                {
                    Id = count,
                    Portuguese = words[0],
                    English = words[1]
                };

                _repository.AddOrUpdate(word);
            }

            Console.WriteLine("Fim");
        }
    }
}

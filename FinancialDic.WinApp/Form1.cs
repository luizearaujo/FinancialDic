using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FinancialDic.Lib.Entity;
using VL2.Prevalence.Repository;

namespace FinancialDic.WinApp
{
    public partial class Form1 : Form
    {
        private List<Word> _words = new List<Word>();
        private static GenericRepository<Word> _repository;

        public Form1()
        {
            InitializeComponent();

            _repository = new GenericRepository<Word>();

            _words = _repository.GetAll().ToList();

            this.LoadGrid();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox1.Text.Length > 2)
            {
                var words = _words.Where(x => x.Portuguese.Contains(textBox1.Text));
                var source = new BindingSource() { DataSource = words };
                dgWords.DataSource = source;
            }
            if (textBox1.Text.Length == 0)
                this.LoadGrid();
        }

        private void btnInvestor_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"http://www.investopedia.com/search/default.aspx?q={textBox1.Text}");
        }

        private void LoadGrid()
        {
            var source = new BindingSource() { DataSource = _words };
            dgWords.DataSource = source;
            dgWords.Columns[0].Width = 525;
            dgWords.Columns[1].Width = 525;
            dgWords.Columns[2].Visible = false;
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                _repository.RemoveAll();
                

                var loadFile = File.ReadAllLines(openFileDialog1.FileName);

                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = loadFile.Length;

                var count = 0;

                foreach (var line in loadFile)
                {
                    var words = line.Split(';');

                    count++;
                    progressBar1.Value = count;

                    var word = new Word()
                    {
                        Id = count,
                        Portuguese = words[0],
                        English = words[1]
                    };

                    _repository.AddOrUpdate(word);

                    _words.Add(word);
                }

                _repository.TakeSnapshot();

                this.LoadGrid();

                MessageBox.Show(@"Dados carregados com sucesso.");
                progressBar1.Visible = false;
            }

        }
    }
}

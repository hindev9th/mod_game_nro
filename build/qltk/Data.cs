using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QLTK
{
    class Data
    {
        public DataGridView DataGridView
        {
            get
            {
                return this.dataGridView;
            }
            set
            {
                this.dataGridView = value;
            }
        }
        public TextBox Acc
        {
            get
            {
                return this.acc;
            }
            set
            {
                this.acc = value;
            }
        }
        public Data(DataGridView dataGridView, TextBox textBox)
        {
            this.DataGridView = dataGridView;
            this.Acc = textBox;
        }
        public Data(TextBox textBox)
        {
            this.Acc = textBox;
        }
        public void ExportFile()
        {
            List<account> dataAcc = new List<account>();
            for (int i = 0; i < this.DataGridView.Rows.Count; i++)
            {
                this.DataGridView.Rows[i].Cells[0].Value = i + 1;
            }

            for (int j = 0; j < this.DataGridView.Rows.Count; j++)
            {
                dataAcc.Add(new account() {
                    stt = this.DataGridView.Rows[j].Cells[0].Value.ToString(),
                    tk = this.DataGridView.Rows[j].Cells[1].Value.ToString(),
                    mk = this.DataGridView.Rows[j].Cells[2].Value.ToString(),
                    server = this.DataGridView.Rows[j].Cells[3].Value.ToString(),
                    ghiChu = this.DataGridView.Rows[j].Cells[4].Value.ToString(),
                });
            }

            String fileName = $"data/data.json";
            using (StreamWriter sw = File.CreateText($"{fileName}"))
            {
                var DataAcc = JsonConvert.SerializeObject(dataAcc);
                sw.WriteLine(DataAcc);
            }

        }
        public void LoadFile()
        {
            try
            {
                this.DataGridView.Rows.Clear();

                List <account> orderData = new List<account>();
                using (StreamReader sr = File.OpenText(@"data/data.json"))
                {
                    var obj = sr.ReadToEnd();
                    orderData = JsonConvert.DeserializeObject<List<account>>(obj);
                }

                foreach(var array in orderData)
                {
                    this.DataGridView.Rows.Add(new object[] {
                        array.stt,
                        array.tk,
                        array.mk,
                        array.server,
                        array.ghiChu
                });
                }
                
            }
            catch (Exception)
            {
                
            }
        }
        public void LoadFileSize()
        {
            if (File.Exists("data/Size.txt"))
            {
                string[] array = File.ReadAllText("data/Size.txt").Split(new char[]
                {
                    'x'
                });
                width = array[0];
                height = array[1];
            }
        }
        public static string width, height;
        public DataGridView dataGridView;
        private TextBox acc;
    }

}

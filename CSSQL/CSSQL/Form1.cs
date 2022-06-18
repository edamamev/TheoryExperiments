using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string path = @"C:\CSSQLtmp";
        private const string dbLocation = path + @"\test.db";

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(path)) {
                File.Create(dbLocation);
            }
            SQLiteConnection conn = new SQLiteConnection(dbLocation);
            SQLiteCommand comm = conn.CreateCommand();
            SQLiteDataAdapter sda = new SQLiteDataAdapter();



        }

        /// <summary>
        /// If we have a blank db file, we give it a structure.
        /// </summary>
        private void CreateTableFromNull(string dbLocation) {
            
        }
        ///https://zetcode.com/csharp/sqlite/
    }
}

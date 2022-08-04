using System;
using System.Data;
using System.Windows.Forms;

namespace testProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string CreateCard = "CREATE TABLE IF NOT EXISTS card " +
            "(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, quantity INTEGER, idclass INTEGER)";
        private const string CreateCardClass = "CREATE TABLE IF NOT EXISTS cardclass " +
            "(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT)";

        private void Form1_Load(object sender, EventArgs e)
        {
            new SqlLiteUtils().ExecSql("DROP TABLE IF EXISTS cardclass");
            DefaultSettings();
            FillTheComboBox();
            FillTheDataGrid();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                new SqlLiteUtils().ExecSql("INSERT INTO card ('name', 'quantity', 'idclass') " +
                    $"values ('{textBox1.Text}', '{numericUpDown1.Value}', '{comboBox1.SelectedIndex+1}')");
            }
        }
        private void FillTheDataGrid()
        {
            DataGridFirst.DataSource = new SqlLiteUtils().GetTable("select id, name from cardclass");
        }
        private void FillTheComboBox()
        {
            DataTable dataTable = new SqlLiteUtils().GetTable("select id, name from cardclass");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                comboBox1.Items.Add(dataTable.Rows[i][1]);
            }
        }
        private void SelectAllBtn_Click(object sender, EventArgs e)
        {
            DataGridFirst.DataSource = new SqlLiteUtils().GetTable("select * from cardclass cc join card c where cc.id = c.idclass");
        }
        private void JnlyCardClassBtn_Click(object sender, EventArgs e)
        {
            DataGridFirst.DataSource = new SqlLiteUtils().GetTable("select id, name from cardclass");
        }
        private void DefaultSettings()
        {
            SqlLiteUtils sqlLiteUtils = new SqlLiteUtils();
            sqlLiteUtils.ExecSql(CreateCard);
            sqlLiteUtils.ExecSql(CreateCardClass);
            sqlLiteUtils.ExecSql("INSERT OR IGNORE INTO cardclass ('name') values ('Фрукты')");
            sqlLiteUtils.ExecSql("INSERT OR IGNORE INTO cardclass ('name') values ('Овощи')");
        }

        private void DataGridFirst_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataFridSecond.DataSource = new SqlLiteUtils().GetTable("select id, name, quantity, idclass " +
                    $"from card where idclass = {DataGridFirst["id", e.RowIndex].Value}");
            }
        }
    }
}

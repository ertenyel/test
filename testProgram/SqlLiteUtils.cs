using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace testProgram
{
    class SqlLiteUtils
    {
        private const string dbFileName = "sample.sqlite";
        private static SQLiteConnection sqliteConnection;

        public SqlLiteUtils()
        {
            try
            {
                sqliteConnection = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                sqliteConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable GetTable(string query)
        {
            DataTable dTable = new DataTable();
            try
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, sqliteConnection))
                {
                    adapter.Fill(dTable);
                }
                return dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void ExecSql(string nonQuery)
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(nonQuery, sqliteConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace namespace_ITMO.CSCourse2022.WinApp.Exam

{    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Integrated Security = SSPI; Persist Security Info = False; Initial Catalog = test_block;" +
                "Data Source = A-NB\\SQLEXPRESS"))
            {
                const string sql = "SELECT * FROM [dbo].[t1]";

                using (SqlCommand sqlCommand = new SqlCommand(sql, conn))
                {
                    try
                    {
                        conn.Open();

                        using (SqlDataReader read = sqlCommand.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(read);
                            this.dataGridView.DataSource = dataTable;
                            read.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("The requested order could not be loaded into the form.");
                    }
                    finally
                    {                        
                        conn.Close();
                    }
                }
            }
        } 

        private void btnDel_Click(object sender, EventArgs e)
        {
            string conn_string = @"Integrated Security = SSPI; Persist Security Info = False; Initial Catalog = test_block;" +
                "Data Source = A-NB\\SQLEXPRESS";            
            int index;
            string num_table;
            index = dataGridView.CurrentRow.Index;
            num_table = Convert.ToString(dataGridView[0, index].Value);
            string cmd_text = "DELETE FROM [dbo].[t1] WHERE [ID] = '" + num_table + "'";

            SqlConnection sql_conn = new SqlConnection(conn_string);
            SqlCommand sql_comm = new SqlCommand(cmd_text, sql_conn);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("The requested order could not be deleted into the form.");
            }
            finally
            {
                sql_conn.Close();
            }            
        }

        private void butUpd_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);           
        }
    }    
}

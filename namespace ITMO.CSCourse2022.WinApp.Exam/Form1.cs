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
{
    public partial class Form1 : Form
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
                // Define a t-SQL query string that has a parameter for orderID.
                const string sql = "SELECT * FROM [dbo].[t1]";

                // Create a SqlCommand object.
                using (SqlCommand sqlCommand = new SqlCommand(sql, conn))
                {
                    // Define the @orderID parameter and set its value.
                    //sqlCommand.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                    //sqlCommand.Parameters["@orderID"].Value = parsedOrderID;

                    try
                    {
                        conn.Open();

                        // Run the query by calling ExecuteReader().
                        using (SqlDataReader read = sqlCommand.ExecuteReader())
                        {
                            // Create a data table to hold the retrieved data.
                            DataTable dataTable = new DataTable();

                            // Load the data from SqlDataReader into the data table.
                            dataTable.Load(read);

                            // Display the data from the data table in the data grid view.
                            this.dataGridView.DataSource = dataTable;

                            // Close the SqlDataReader.
                            read.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("The requested order could not be loaded into the form.");
                    }
                    finally
                    {
                        // Close the connection.
                        conn.Close();
                    }
                }
            }
        }
    }    
}

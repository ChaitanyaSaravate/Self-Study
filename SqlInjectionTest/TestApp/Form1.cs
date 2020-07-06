using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Server = WL357485; Database = Learning; Trusted_Connection = True;"))
            {
                connection.Open();
                IDbCommand command = new SqlCommand();

                if (checkBox1.Checked)
                    command = UseInlineQuery(connection);
                else if (checkBox2.Checked)
                    command = UseParameterizedQueryWithSqlParameter(connection);
                else if (checkBox3.Checked)
                    command = UseDbFactory(connection);


                using (command)
                {
                    SqlDataAdapter sda = new SqlDataAdapter((SqlCommand)command);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private IDbCommand UseDbFactory(SqlConnection connection)
        {
            var instance = SqlClientFactory.Instance;

            IDbDataParameter param = instance.CreateParameter();
            param.ParameterName = "@UserName";
            param.Value = textBox1.Text;
            param.DbType = DbType.AnsiString;

            IDbDataParameter param2 = instance.CreateParameter();
            param2.ParameterName = "@Password";
            param2.Value = "Password1";
            param.DbType = DbType.AnsiString;

            IDbCommand command = connection.CreateCommand();
            command.CommandText = $"select * from TestSqlInjection where UserName = @UserName and Password = @Password";
            ((DbCommand)command).Parameters.Add(param);
            ((DbCommand)command).Parameters.Add(param2);

            return command;
        }

        private SqlCommand UseInlineQuery(SqlConnection connection)
        {
            return new SqlCommand($"select * from TestSqlInjection where UserName = '{textBox1.Text}' and Password = 'Password1';", connection);
        }

        private SqlCommand UseParameterizedQueryWithSqlParameter(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = $"select * from TestSqlInjection where UserName = @UserName and Password = @Password";
            command.Connection = connection;

            SqlParameter parameter = new SqlParameter("@UserName", SqlDbType.NVarChar);
            parameter.Value = textBox1.Text;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter("@Password", SqlDbType.NVarChar);
            parameter2.Value = "Password1";
            command.Parameters.Add(parameter2);

            return command;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = checkBox3.Checked = false;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = checkBox3.Checked = false;
            }
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Checked = checkBox1.Checked = false;
            }
        }
    }
}

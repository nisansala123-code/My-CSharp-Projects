using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class frm_CODA : Form
    {
        public frm_CODA()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = txt_Un.Text.Trim();
            string password = txt_Password.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter Username and Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connString = "Data Source=CampusOne.db;Version=3;";

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username=@user AND Password=@pass";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_Password.Clear();
                            txt_Un.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Un.Clear();
            txt_Password.Clear();
            txt_Un.Focus();
        }
    }
}





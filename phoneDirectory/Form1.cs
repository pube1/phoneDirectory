using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.OleDb;
using System.IO;

namespace phoneDirectory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SQLiteConnection conn = new SQLiteConnection("Data Source=phoneDirection.db; Version=3");
        
        public void showdata(string data)
        {
            MessageBox.Show(Directory.GetCurrentDirectory());
            SQLiteDataAdapter da = new SQLiteDataAdapter(data, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {

            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection=conn;
            cmd.CommandText = ("Select * from contacts");
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToString(dr["number"])==maskedTextBox1.Text)
                {
                    MessageBox.Show("this number is registered to a person named " + dr["name"] + " " + dr["surname"]);
                    dr.Close();
                    conn.Close();
                    return;
                }
            }
            dr.Close();
            cmd.CommandText = ("INSERT INTO contacts(name,surname,number) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" +
                maskedTextBox1.Text + "')");
            cmd.ExecuteNonQuery();
            showdata("select * from contacts");
            conn.Close();
            MessageBox.Show("Registration done");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showdata("select * from contacts");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from contacts where id=" + deleteIDTb.Text;

            cmd.ExecuteNonQuery();
            showdata("select * from contacts");
            conn.Close();
            MessageBox.Show("Deletion has taken place");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            maskedTextBox1.Clear();
            deleteFindTb.Clear();
            deleteIDTb.Clear();
            updateIdTb.Clear();
            updateNmbrMtb.Clear();
            updateNmTb.Clear();
            updateSearchTb.Clear();
            updateSnTb.Clear();

            if (tabControl1.SelectedIndex == 1)
            {
                MessageBox.Show("Enter the ID of the person to be deleted for deletion");
            }

            else if (tabControl1.SelectedIndex == 2)
            {
                MessageBox.Show("To update, enter the ID of the person to update and enter the information to update");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            showdata("select * from contacts where name like" + "'" + "%" + textBox3.Text  + "%" + "'");
        }

        private void deleteFindTb_TextChanged(object sender, EventArgs e)
        {
            showdata("select * from contacts where name like" + "'" + "%" + deleteFindTb.Text + "%" + "'");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            showdata("select * from contacts where name like" + "'" + "%" + updateSearchTb.Text + "%" + "'");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection=conn;
            if (updateIdTb.Text == "")
            {
                MessageBox.Show("Please fill in the blanks");
                conn.Close();
                return;
            }

            else
            {
                cmd.CommandText = "UPDATE contacts set name='" + updateNmTb.Text + "',surname='" + updateSnTb.Text + "',number='" + updateNmbrMtb.Text +
                    "' where id=" + updateIdTb.Text;
            }
            cmd.ExecuteNonQuery();
            showdata("select * from contacts");
            conn.Close();
        }
    }
}

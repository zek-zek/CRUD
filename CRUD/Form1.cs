using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CRUD
{
    public partial class Form1 : Form
    {
        
        person _person;
        dbBoilerPlate _dbBoilerPlate  = new dbBoilerPlate();
        int id;


        public bool isValid(string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
        public void isAddOrUpdate(string sql, string btn)
        {
            string text = btn.Equals("ADD") ? "ADD" : "UPDATE";

            if (txtFname.Text != "" || txtLname.Text != "" || txtAge.Text != "" || txtEmail.Text != "")
            {
                if (isValid(txtEmail.Text))
                {
                    _person = new person(txtFname.Text, txtLname.Text, Convert.ToInt32(txtAge.Text), txtEmail.Text);
                    if (Convert.ToInt32(txtAge.Text) >= 7)
                    {

                        _dbBoilerPlate.dtQueries(sql, _person.Fname, _person.Lname, _person.Age, _person.Email);
                        if (MessageBox.Show("Record "+text+" Successfully") == DialogResult.OK)
                        {
                            _dbBoilerPlate.dtTable("SELECT * FROM data order by id asc", dtGrid);
                            btnClear.PerformClick();
                            btnAdd.Enabled = true;
                            btnUpdate.Enabled = false;
                            btnDelete.Enabled = false;

                        }
                    }
                    else
                    {
                        MessageBox.Show("Age must be greater than or equal to 7");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Email");
                }

            }
            else
            {
                MessageBox.Show("Please fill all the fields");
            }

        }

        public Form1()
        {
            InitializeComponent();
            _dbBoilerPlate.dtTable("SELECT * FROM data order by id asc", dtGrid);
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFname.Clear();
            txtLname.Clear();
            txtAge.Clear();
            txtEmail.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            isAddOrUpdate("insert into data values(@fname,@lname,@age,@email)",btnAdd.Text);

        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Please enter an integer");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            isAddOrUpdate("update data set firstname = @fname, lastname = @lname, age = @age, email = @email where id = "+id+"", btnUpdate.Text);

        }

        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (dtGrid.CurrentRow.Selected == true)
                    {
                      
                            btnAdd.Enabled = false;
                            btnUpdate.Enabled = true;
                            btnDelete.Enabled = true;
                            
                            id = Convert.ToInt32(dtGrid.Rows[e.RowIndex].Cells[0].Value);
                            txtFname.Text = dtGrid.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                            txtLname.Text = dtGrid.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                            txtAge.Text = Convert.ToInt32(dtGrid.Rows[e.RowIndex].Cells[3].Value).ToString();
                            txtEmail.Text = dtGrid.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                        
                    }
                }
            }
            catch
            { 
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _person = new person(txtFname.Text, txtLname.Text, Convert.ToInt32(txtAge.Text), txtEmail.Text);
            if (txtFname.Text != "" || txtLname.Text != "" || txtAge.Text != "" || txtEmail.Text != "")
            {
                if (isValid(_person.Email))
                {
                    
                    if (MessageBox.Show("Are you sure you want to delete this data?" +
                        "\nName: " + _person.Fname + " " + _person.Lname + "" +
                        "\nAge: " + _person.Age + "" +
                        "\nEmail: " + _person.Email, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        _dbBoilerPlate.dtQueries("delete from data where id = " + id + "", _person.Fname, _person.Lname, _person.Age, _person.Email);
                        if (MessageBox.Show("Deleted Successfully") == DialogResult.OK)
                        {
                            _dbBoilerPlate.dtTable("SELECT * FROM data order by id asc", dtGrid);
                            btnClear.PerformClick();
                            btnAdd.Enabled = true;
                            btnUpdate.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Email");
                }
            }
            else
            {
                MessageBox.Show("Please select again to delete a data");
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string data = txtSearch.Text;
            _dbBoilerPlate.dtTable("select * from data where concat(id, firstname, lastname, age, email) like '%" + data + "%'", dtGrid);
        }
    }
}

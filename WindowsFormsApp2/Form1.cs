using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OgrenciBilgiSistemi
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-7EPCTQ4;Initial Catalog=OgrenciBilgiSistemi;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Ogrenciler (Name, LastName, BirthDate, Class) VALUES (@Name, @LastName, @BirthDate, @Class)", con);
                    cmd.Parameters.AddWithValue("@Name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBoxLastName.Text);
                    cmd.Parameters.AddWithValue("@BirthDate", string.IsNullOrEmpty(textBoxBirthDate.Text) ? (object)DBNull.Value : textBoxBirthDate.Text);
                    cmd.Parameters.AddWithValue("@Class", textBoxClass.Text);
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Ogrenciler SET Name=@Name, LastName=@LastName, BirthDate=@BirthDate, Class=@Class WHERE ID=@ID", con);
                    cmd.Parameters.AddWithValue("@ID", textBoxID.Text);
                    cmd.Parameters.AddWithValue("@Name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBoxLastName.Text);
                    cmd.Parameters.AddWithValue("@BirthDate", string.IsNullOrEmpty(textBoxBirthDate.Text) ? (object)DBNull.Value : textBoxBirthDate.Text);
                    cmd.Parameters.AddWithValue("@Class", textBoxClass.Text);
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@ID", con);
                    cmd.Parameters.AddWithValue("@ID", textBoxID.Text);
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Ogrenciler WHERE Name LIKE '%' + @Search + '%'", con);
                    da.SelectCommand.Parameters.AddWithValue("@Search", textBoxName.Text);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxID.Clear();
            textBoxName.Clear();
            textBoxLastName.Clear();
            textBoxBirthDate.Clear();
            textBoxClass.Clear();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBoxID.Text = row.Cells["ID"].Value.ToString();
                textBoxName.Text = row.Cells["Name"].Value.ToString();
                textBoxLastName.Text = row.Cells["LastName"].Value.ToString();
                textBoxBirthDate.Text = row.Cells["BirthDate"].Value.ToString();
                textBoxClass.Text = row.Cells["Class"].Value.ToString();
            }
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Ogrenciler", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            // ID için işlemler
            if (string.IsNullOrEmpty(textBoxID.Text) || (int.TryParse(textBoxID.Text, out int id) && id == 0))
            {
                // ID boşsa veya sıfırsa yapılacak işlemler
            }
        }

        private void textBoxBirthDate_TextChanged(object sender, EventArgs e)
        {
            // Doğum tarihi için işlemler
            if (string.IsNullOrEmpty(textBoxBirthDate.Text))
            {
                // Doğum tarihi boşsa yapılacak işlemler
            }
            else if (DateTime.TryParse(textBoxBirthDate.Text, out DateTime birthDate))
            {
                // Geçerli bir tarih formatıysa yapılacak işlemler
            }
            else
            {
                // Geçersiz tarih formatı
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            // İsim için işlemler
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            // Soyisim için işlemler
        }

        private void textBoxClass_TextChanged(object sender, EventArgs e)
        {
            // Sınıf için işlemler
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}


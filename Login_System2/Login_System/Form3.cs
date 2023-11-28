using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver.Core.Configuration;
using MySql.Data.MySqlClient;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Login_System
{
    public partial class Form3 : Form
    {


        MySqlConnection recipe = new MySqlConnection
        ("datasource=localhost;port=3306;username=root;password=''");
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 loginPage = new Form1();
            loginPage.ShowDialog();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Assuming you have TextBox controls for ID, Name, Ingredients, and Instructions
                if (int.TryParse(textBox1.Text, out int recipeIdToUpdate))
                {
                    string newName = textBox2.Text;
                    string newIngredients = textBox3.Text;
                    string newInstructions = textBox4.Text;

                    using (MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=''"))
                    {
                        connection.Open();

                        string query = "UPDATE loginportal.recipebook SET `Name` = @newName, `Ingredients` = @newIngredients, `Instructions` = @newInstructions WHERE ID = @recipeId";
                        MySqlCommand command = new MySqlCommand(query, connection);

                        command.Parameters.AddWithValue("@newName", newName);
                        command.Parameters.AddWithValue("@newIngredients", newIngredients);
                        command.Parameters.AddWithValue("@newInstructions", newInstructions);
                        command.Parameters.AddWithValue("@recipeId", recipeIdToUpdate);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No rows updated. Recipe ID not found.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Recipe ID must be a valid integer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text)
                 || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Please fill out all information!", "Error");
                return;
            }
            else
            {

                int id;
                if (!int.TryParse(textBox1.Text, out id))
                {
                    MessageBox.Show("Please enter a valid ID.");
                    return;
                }
                string name = textBox2.Text;
                string ingredients = textBox3.Text;
                string Instructions = textBox4.Text;

                try
                {
                    using (MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=''"))
                    {
                        connection.Open();

                        // Corrected column name to 'Instruction' in the SQL query
                        string query = "INSERT INTO loginportal.recipebook (`Name`, `Ingredients`, `Instructions`) VALUES (@name, @ingredients, @instructions)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@ingredients", ingredients);
                        command.Parameters.AddWithValue("@instructions", Instructions);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No rows updated. ID not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=''"))
                {
                    connection.Open();

                    string query = "SELECT * FROM loginportal.recipebook";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Assuming dataGridView1 is the name of your DataGridView control
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void LoadData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=''"))
                {
                    connection.Open();

                    string query = "SELECT * FROM loginportal.recipebook";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Assuming dataGridView1 is the name of your DataGridView control
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Call the LoadData method in your form load event or wherever appropriate
        private void Form_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=''"))
                {
                    connection.Open();

                    // Assuming you have an identifier (e.g., recipe ID) to determine which row to delete
                    int recipeIdToDelete = GetRecipeIdToDelete(); // Implement this method to get the ID from your application

                    string query = "DELETE FROM loginportal.recipebook WHERE ID = @recipeId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@recipeId", recipeIdToDelete);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 1)
                    {
                        MessageBox.Show("Data deleted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No rows deleted. Recipe ID not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Implement this method to get the ID from your application
        private int GetRecipeIdToDelete()
        {
            return 1;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string searchKeyword = textBox1.Text.Trim();

                using (MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=''"))
                {
                    connection.Open();

                    string query = "SELECT * FROM loginportal.recipebook WHERE Name LIKE @searchKeyword OR Ingredients LIKE @searchKeyword OR Instructions LIKE @searchKeyword";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchKeyword", "%" + searchKeyword + "%");

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            // Assuming dataGridView is the name of your DataGridView control
                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("No matching records found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}


using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

namespace GSBModel.Data
{
    public class Dbal
    {
        private MySqlConnection connection;


        //Constructor
        public Dbal()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            string connectionString;
            connectionString = ConfigurationManager.ConnectionStrings["gsb_frais_db"].ConnectionString;

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;

        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        //CURQuery: Create, Update, Delete query execution method
        private void CUDQuery(string query)
        {
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Insert statement
        public void Insert(string queryPart)
        {
            string query = "INSERT INTO " + queryPart; // tablename (field1, field2) VALUES('value 1', 'value 2')";
            CUDQuery(query);

        }

        //Update statement
        public void Update(string query)
        {
            query = "UPDATE " + query;

            CUDQuery(query);
        }

        //Delete statement
        public void Delete(string query)
        {
            query = "DELETE FROM " + query;

            CUDQuery(query);
        }

        //RQuery: Read query method (to execute SELECT queries)
        private DataSet RQuery(string query)
        {
            DataSet dataset = new DataSet();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Add query data in a DataSet
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.Fill(dataset);
                CloseConnection();
            }
            return dataset;
        }


        //Select statement
        public DataTable SelectAll(string table)
        {
            string query = "SELECT * FROM " + table;
            DataSet dataset = RQuery(query);

            return dataset.Tables[0];
        }

        public DataTable SelectByField(string table, string fieldTestCondition)
        {
            string query = "SELECT * FROM " + table + " where " + fieldTestCondition;
            DataSet dataset = RQuery(query);

            return dataset.Tables[0];
        }

        public DataRow SelectById(string table, string id)
        {
            string query = "SELECT * FROM " + table + " where id='" + id + "'";
            DataSet dataset = RQuery(query);

            return dataset.Tables[0].Rows[0];
        }

        //équivalent à SelectById mais pour une table dont la clé primaire est composée de 2 colonnes
        public DataRow SelectByComposedPK2(string table, string keyname1, string keyvalue1, string keyname2, string keyvalue2)
        {
            string query = "SELECT * FROM " + table + " where " + keyname1 + "= '" + keyvalue1 + "' AND " + keyname2 + " = '" + keyvalue2 +"'";
            DataSet dataset = RQuery(query);
            if (dataset.Tables[0].Rows.Count != 0)
            {
                return dataset.Tables[0].Rows[0];
            }
            else
                return null;
        }


        public DataTable SelectByComposedFK2(string table, string keyname1, string keyvalue1, string keyname2, string keyvalue2)
        {
            string query = "SELECT * FROM " + table + " where " + keyname1 + "= '" + keyvalue1 + "' AND " + keyname2 + " = '" + keyvalue2 + "'";
            DataSet dataset = RQuery(query);

            return dataset.Tables[0];
        }

        //équivalent à SelectById mais pour une table dont la clé primaire est composée de 3 colonnes
        public DataRow SelectByComposedPK3(string table, string keyname1, string keyvalue1, string keyname2, string keyvalue2, string keyname3, string keyvalue3)
        {
            string query = "SELECT * FROM " + table + " where " + keyname1 + "= '" + keyvalue1 + "' AND " + keyname2 + " = '" + keyvalue2 + "' AND " + keyname3 + " = '" + keyvalue3 + "'";
            DataSet dataset = RQuery(query);

            return dataset.Tables[0].Rows[0];
        }

        public DataTable SelectByComposedFK3(string table, string keyname1, string keyvalue1, string keyname2, string keyvalue2, string keyname3, string keyvalue3)
        {
            string query = "SELECT * FROM " + table + " where " + keyname1 + "= '" + keyvalue1 + "' AND " + keyname2 + " = '" + keyvalue2 + "' AND " + keyname3 + " = '" + keyvalue3 + "'";
            DataSet dataset = RQuery(query);

            return dataset.Tables[0];
        }

        //select distinct pour récupération des valeurs uniques
        public DataTable SelectDistinct (string field, string table, string orderby, string orderbytype)
        {
            string query = "SELECT DISTINCT " + field + " FROM " + table + " ORDER BY " + orderby + " " + orderbytype;
            DataSet dataset = RQuery(query);

            return dataset.Tables[0];
        }
    }
}

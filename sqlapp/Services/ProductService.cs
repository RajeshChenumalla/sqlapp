using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "appserverrazor.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Azuresql@9963";
        private static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var _bulder = new SqlConnectionStringBuilder();
            _bulder.DataSource = db_source;
            _bulder.UserID = db_user;
            _bulder.Password = db_password;
            _bulder.InitialCatalog = db_database;

            return new SqlConnection(_bulder.ConnectionString);

        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> _product_lst = new List<Product>();
            string statement = "SELECT ProductID,ProductName,Quantity FROM Products";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) {
                        Product _product = new Product()
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Quantity = reader.GetInt32(2)
                        };

                        _product_lst.Add(_product);
                    }
                }

                conn.Close();
                return _product_lst;
            }
            catch (Exception ex)
            {
                throw new Exception(statement, ex);
            }
        }
    }
}

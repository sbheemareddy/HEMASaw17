using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace HEMASaw.DAO
{
    public static class HemaSawDAO
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["HemaSawDBConnection"].ConnectionString;
        public static WOData GetSystemData(int workOrder,string slicebatch,string blockbatch, string sliceNum)
        {
            WOData wOData = new WOData();
            string storedProcedureName = "spGetSystemData";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@workOrder", workOrder);
                    command.Parameters.AddWithValue("@slice_batch", slicebatch);
                    command.Parameters.AddWithValue("@block_batch", blockbatch);
                    command.Parameters.AddWithValue("@sliceNum", sliceNum);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string densityTol = double.Parse(reader["DensityTol"].ToString()).ToString("0.000");
                        string density = double.Parse(reader["Density"].ToString()).ToString("0.000");
                        wOData.Material = reader["Material"].ToString();
                        wOData.SliceBatch = reader["Slice_Batch"].ToString();
                        wOData.Density = double.Parse(density);
                        wOData.DensityTol = double.Parse(densityTol);
                        wOData.Description = reader["Description"].ToString();
                        wOData.VisualPartID = reader["VisualPartID"].ToString();
                        wOData.SliceNum = reader["SliceNum"].ToString();
                        wOData.Thickness = reader["Thickness"].ToString();
                        wOData.TargetCellCount = reader["TargetCellCount"].ToString();
                        wOData.MinCellCount = reader["MinCellCount"].ToString();
                        wOData.MaxCellCount = reader["MaxCellCount"].ToString();
                        wOData.Length = double.Parse(reader["Length"].ToString());
                        wOData.Width = double.Parse(reader["Width"].ToString());
                        wOData.Weight = double.Parse(reader["Weight"].ToString());
                    }

                    reader.Close();
                }
            }

            return wOData;

        }
        public static QRCodeData GetQRDataFromSystem(int workOrder, string slicebatch, string blockbatch, string sliceNum)
        {
            QRCodeData qRCodeData = new QRCodeData();
            string storedProcedureName = "spGetQRDataFromSystem";
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a command to execute the stored procedure
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Set the command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter to the command
                    command.Parameters.AddWithValue("@workOrder", workOrder);
                    command.Parameters.AddWithValue("@slice_batch", slicebatch);
                    command.Parameters.AddWithValue("@block_batch", blockbatch);
                    command.Parameters.AddWithValue("@sliceNum", sliceNum);

                    // Execute the command
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        qRCodeData.QRCodeDate = "";
                        qRCodeData.WO = workOrder;
                        qRCodeData.BlockBatch = blockbatch;
                        qRCodeData.SliceBatch = slicebatch;
                        qRCodeData.Saw = reader["SawNum"].ToString();
                        qRCodeData.Min = double.Parse(reader["MinThk"].ToString());
                        qRCodeData.Max= double.Parse(reader["MaxThk"].ToString());
                        qRCodeData.Ave = double.Parse(reader["AvgThk"].ToString());

                    }

                    reader.Close();
                }
            }

            return qRCodeData;

        }
        public static DataTable GetUniqueQRRecord(QRCodeData qRCodeData)
        {
            DataTable dataTable = new DataTable(); 
            string storedProcedureName = "spGetUniqueQRRecord";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a command to execute the stored procedure
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Set the command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter to the command
                    command.Parameters.AddWithValue("@workOrder", qRCodeData.WO);
                    command.Parameters.AddWithValue("@block_batch", qRCodeData.BlockBatch);
                    command.Parameters.AddWithValue("@sliceNum", qRCodeData.SliceNum);
                    command.Parameters.AddWithValue("@saw", qRCodeData.Saw);
                    command.Parameters.AddWithValue("@max", qRCodeData.Max);
                    command.Parameters.AddWithValue("@min", qRCodeData.Min);
                    command.Parameters.AddWithValue("@ave", qRCodeData.Ave);
                    // Execute the command

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Load the data from the reader into the DataTable
                        dataTable.Load(reader);
                    }
                    //SqlDataReader reader = command.ExecuteReader();

                    //while (reader.Read())
                    //{
                    //    dataTable.Load(reader);
                    //}

                    //reader.Close();
                }
            }

            return dataTable;

        }
        public static DataTable SearchWO(int workOrder, string slicebatch, string blockbatch)
        {
            string storedProcedureName = "spGetSearchWO";
            DataTable dataTable = new DataTable(); // Create a new DataTable to hold the results

            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a command to execute the stored procedure
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Set the command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter to the command
                    command.Parameters.AddWithValue("@workOrder", workOrder);
                    command.Parameters.AddWithValue("@slice_batch", slicebatch);
                    command.Parameters.AddWithValue("@block_batch", blockbatch);

                    // Execute the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Load the data from the reader into the DataTable
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable; // Return the filled DataTable
        }
        public static List<User> GetUserList(Char isActive = 'Y')
        {
            List<User> users = new List<User>();
            string storedProcedureName = "spGetUserList";
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a command to execute the stored procedure
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Set the command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter to the command
                    command.Parameters.AddWithValue("@active", isActive);

                    // Execute the command
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        user.EmployeeID = reader["EmployeeID"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Active = reader["Active"].ToString() == "1" ? true : false; ;
                        user.CreateDate = (DateTime)reader["CreateDate"];
                        user.TermDate = reader["TermDate"] == DBNull.Value ? null : (DateTime?)reader["TermDate"];
                        user.EmployeeRoleDesc = reader["employeeRoleDesc"].ToString();
                        users.Add(user);
                    }

                    reader.Close();
                }
            }

            return users;

        }
        public static User GetUserById(string employeeId)
        {
            User user = new User();
            string storedProcedureName = "spGetUserByID";
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();


                // Create a command to execute the stored procedure
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Set the command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter to the command
                    command.Parameters.AddWithValue("@employeeId", employeeId);

                    // Execute the command
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user.EmployeeID = reader["EmployeeID"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Active = reader["Active"].ToString().ToUpper() == "Y" ? true : false; 
                        user.CreateDate = (DateTime)reader["CreateDate"];
                        user.TermDate = reader["TermDate"] == DBNull.Value ? null : (DateTime?)reader["TermDate"];
                        user.EmployeeRoleDesc = reader["employeeRole"].ToString().ToUpper() == "1" ? "Operator" : "Supervisor";
                        user.EmployeeRole = int.Parse(reader["employeeRole"].ToString());
                        user.EmployeePassword = reader["Password"].ToString();
                        user.FirstTimeLogin = bool.Parse(reader["bFirstTimeLogin"].ToString());
                    }
                    reader.Close();
                }
            }

            return user;

        }
        public static void UpsertEmployeeRecord(User employee)
        {
            List<User> users = new List<User>();
            string storedProcedureName = "UpsertEmployee";
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a command to execute the stored procedure
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@Active", employee.Active  ? "Y" : "N");
                    command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    command.Parameters.AddWithValue("@TermDate", employee.TermDate <= SqlDateTime.MinValue.Value ? null : employee.TermDate);
                    command.Parameters.AddWithValue("@employeeRole", employee.EmployeeRole);
                    command.Parameters.AddWithValue("@bFirstTimeLogin", employee.bChangePassword);
                    command.Parameters.AddWithValue("@Password", employee.HashedPassword);
                    try
                    {
                        command.ExecuteNonQuery();
                        //Console.WriteLine("Employee data inserted or updated successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public static DataSet GetSliceSummaryLabel(int workOrder , string sliceBatch , string blockBatch,string sliceNum)
        {
            // Create a new SqlConnection using the connection string
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a new SqlCommand for the stored procedure
                using (SqlCommand command = new SqlCommand("spGetSliceLabel", connection))
                {
                    // Specify that the command is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.Add("@workOrder", SqlDbType.Int).Value = workOrder;
                    command.Parameters.Add("@slice_batch", SqlDbType.VarChar).Value = sliceBatch;
                    command.Parameters.Add("@block_batch", SqlDbType.VarChar).Value = blockBatch;
                    command.Parameters.Add("@sliceNum", SqlDbType.SmallInt).Value = int.Parse(sliceNum);

                    // Create a new SqlDataAdapter to execute the command and fill a DataSet
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // Create a new DataSet to hold the results
                        DataSet dataSet = new DataSet();

                        // Open the connection
                        connection.Open();

                        // Fill the DataSet with the results of the command
                        adapter.Fill(dataSet);

                        // Close the connection
                        connection.Close();

                        return dataSet;
                    }
                }
            }
        }
        public static void AcceptSliceData(int Id , string EmployeeID, string ExpanderNum,double Length, double Width, double Weight, string Comments, double DensityPCF, double DensityPSF, int CellCount , DateTime QrCodeDate)
        {
            // Create connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create command
                using (SqlCommand command = new SqlCommand("spAcceptSliceData", connection))
                {
                    // Set command type to stored procedure
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@employeeID", EmployeeID);
                    command.Parameters.AddWithValue("@expanderNum", ExpanderNum);
                    command.Parameters.AddWithValue("@length", Length);
                    command.Parameters.AddWithValue("@width", Width);
                    command.Parameters.AddWithValue("@weight", Weight);
                    command.Parameters.AddWithValue("@comments", Comments);
                    command.Parameters.AddWithValue("@densityPCF", DensityPCF);
                    command.Parameters.AddWithValue("@densityPSF", DensityPSF);
                    command.Parameters.AddWithValue("@QrCodeDate", QrCodeDate);
                    command.Parameters.AddWithValue("@CellCount", CellCount);

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if any rows were affected
                    if (rowsAffected > 0)
                    {
                        // Rows were affected (updated successfully)
                        Console.WriteLine("Stored procedure executed successfully. Rows affected: " + rowsAffected);
                    }
                    else
                    {
                        // No rows were affected (maybe the ID didn't match any record)
                        Console.WriteLine("Stored procedure executed successfully, but no rows were affected.");
                    }
                }
            }
        }
        public static bool UpdatePassword(string employeeID, string password)
        {
            string saltedPasswordHash = GetHashedPassword(password);

            // Define the stored procedure name
            string storedProcedureName = "spUpdateEmployeePassword";

            // Create and open connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create command object
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Specify that command is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters and set their values
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    command.Parameters.AddWithValue("@Password", saltedPasswordHash);
                    // Execute the stored procedure
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if any rows were affected
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static string GetHashedPassword(string password)
        {
            // Generate a random salt
            string salt = GenerateSalt();

            // Compute the hash of the password with the salt
            string hashedPassword = ComputeHash(password, salt);

            // Concatenate salt and hashed password with a delimiter
            string saltedPasswordHash = hashedPassword + ":" + salt;
            return saltedPasswordHash;
        }

        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        public static string ComputeHash(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Combine password and salt, then compute the hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

                // Convert byte array to a string representation
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}

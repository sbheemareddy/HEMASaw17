﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace HEMASaw.DAO
{
    public static class HemaSawDAO
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["HemaSawDBConnection"].ConnectionString;
        public static WOData GetSystemData(int workOrder,string slicebatch,string blockbatch, string sliceNum)
        {
            WOData wOData = new WOData();
            // Connection string to your SQL Server database
            //string connectionString = ConfigurationManager.ConnectionStrings["HemaSawDBConnection"].ConnectionString;
            ////Name of your stored procedure
            string storedProcedureName = "spGetSystemData";
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
                    }

                    reader.Close();
                }
            }

            return wOData;

        }
        public static QRCodeData GetQRDataFromSystem(int workOrder, string slicebatch, string blockbatch, string sliceNum)
        {
            QRCodeData qRCodeData = new QRCodeData();
            // Connection string to your SQL Server database
            //string connectionString = ConfigurationManager.ConnectionStrings["HemaSawDBConnection"].ConnectionString;
            ////Name of your stored procedure
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
                        user.Active = reader["Active"].ToString() == "1" ? true : false; ;
                        user.CreateDate = (DateTime)reader["CreateDate"];
                        user.TermDate = reader["TermDate"] == DBNull.Value ? null : (DateTime?)reader["TermDate"];

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
        public static DataSet GetSliceSummaryLabel(int workOrder , string sliceBatch , string blockBatch)
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
    }
}

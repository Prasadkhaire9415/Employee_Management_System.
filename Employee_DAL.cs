
using System;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using Employee_ManageMent_System.Models;
using System.Configuration;

namespace Employee_ManageMent_System.DAL
{
    public class Employee_DAL
    {
        MySqlConnection connection = null;
        MySqlCommand command = null;
        public static IConfiguration configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            configuration = builder.Build();

            return configuration.GetConnectionString("DefaultConnection");

        }

        public  List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();
            using (connection = new MySqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "FetchEmployeeData";
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                    
                            employee.Id = reader.GetInt32("id");
                    employee.Firstname = reader.GetString("firstname");
					employee.Lastname = reader.GetString("lastname");
					employee.DateOfBirth = reader.GetDateTime("dataofbirth");
                            employee.Email = reader.GetString("email");
                             employee.Salary = reader.GetInt32("salary");
                        list.Add(employee);
                    }
                    connection.Close();
                
            }

            return list;
        }
        public bool Insert(Employee employee)
        {
            int status = 0;
          
                connection = new MySqlConnection(GetConnectionString());
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "insertdata";
                command.Parameters.AddWithValue("@f_name", employee.Firstname);
                command.Parameters.AddWithValue("@l_name", employee.Lastname);
                command.Parameters.AddWithValue("@dob", employee.DateOfBirth.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@mail", employee.Email);
                command.Parameters.AddWithValue("@s", employee.Salary);
                connection.Open();
            status = command.ExecuteNonQuery();
          
            
            return status>0 ? true : false;
		}
        public Employee GetById(int id)
        {
            Employee employee = new Employee();
            using (connection = new MySqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getbyid";
                command.Parameters.AddWithValue("@emp_id", id);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    employee.Id = reader.GetInt32("id");
                    employee.Firstname = reader.GetString("firstname");
					employee.Lastname = reader.GetString("lastname");
					employee.DateOfBirth = reader.GetDateTime("dataofbirth");
                    employee.Email = reader.GetString("email");
                    employee.Salary = reader.GetInt32("salary");
                }
                
                connection.Close();

            }

            return employee;
        }
        public bool Update(Employee employee)
        {
            int status = 0;

            connection = new MySqlConnection(GetConnectionString());
            command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "updatedata";
            command.Parameters.AddWithValue("@emp_id", employee.Id);
            command.Parameters.AddWithValue("@f_name", employee.Firstname);
            command.Parameters.AddWithValue("@l_name", employee.Lastname);
            command.Parameters.AddWithValue("@dob", employee.DateOfBirth.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@mail", employee.Email);
            command.Parameters.AddWithValue("@s", employee.Salary);
            connection.Open();
            status = command.ExecuteNonQuery();


            return status > 0 ? true : false;
        }
        public bool Delete(int id)
        {
            int status = 0;
            connection = new MySqlConnection(GetConnectionString());
            command = connection.CreateCommand();
              command.CommandType=CommandType.StoredProcedure;
            command.CommandText= "deletedata";
			command.Parameters.AddWithValue("@emp_id", id);
			connection.Open();
            status = command.ExecuteNonQuery();
			return status > 0 ? true : false;
		}
    }
}

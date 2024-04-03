using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Repos
{
    public class ToDoRepo
    {
        string connStr;

        public ToDoRepo()
        {
            connStr = ConfigurationManager.ConnectionStrings["TodoDb"].ConnectionString;
        }

        public List<ToDo> GetToDos()
        {
            List<ToDo> toDos = new List<ToDo>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ToDo", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ToDo toDo = new ToDo();
                    toDo.Id = reader.GetInt32(0);
                    toDo.Title = reader.GetString(1);
                    toDo.Description = reader.GetString(2);
                    toDo.Created = reader.GetDateTime(3);
                    toDo.Completed = reader.GetBoolean(4);
                    toDos.Add(toDo);
                }
            }
            return toDos;
        }

        public int AddToDo(ToDo todo)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Assignment (Title, Description, Created, Completed) VALUES (@Title, @Description, @Created, @Completed); SELECT CAST(scope_identity() AS int);", conn);
                cmd.Parameters.AddWithValue("@Title", todo.Title);
                cmd.Parameters.AddWithValue("@Description", todo.Description);
                cmd.Parameters.AddWithValue("@Created", todo.Created);
                cmd.Parameters.AddWithValue("@Completed", todo.Completed);
                conn.Open();
                todo.Id = (int) cmd.ExecuteScalar();
            }
            return todo.Id;
        }

        public void UpdateToDo(ToDo todo)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand($"Update Todo " +
                                                $"SET Title = @Title," +
                                                $"SET Description = @Description," +
                                                $"SET Created = @Created," +
                                                $"SET Completed = @Completed " + $"WHERE ID = @ID; ", conn);
                cmd.Parameters.AddWithValue("@Title", todo.Title);
                cmd.Parameters.AddWithValue("@Description", todo.Description);
                cmd.Parameters.AddWithValue("@Created", todo.Created);
                cmd.Parameters.AddWithValue("@Completed", todo.Completed);
                cmd.Parameters.AddWithValue("@ID", todo.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteToDo(int todoId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM ToDo WHERE ID = @ID;", conn);
                cmd.Parameters.AddWithValue("@ID", todoId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

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

        //public void AddAssignment(Assignment assignment)
        //{
        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        SqlCommand cmd = new SqlCommand($"INSERT INTO Assignment (Title, Score, StudentId) VALUES (@Title, @Score, @StudentId);", conn);
        //        cmd.Parameters.AddWithValue("@Title", assignment.Title);
        //        cmd.Parameters.AddWithValue("@Score", assignment.Score);
        //        cmd.Parameters.AddWithValue("@StudentId", assignment.StudentId);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //public void UpdateAssignment(Assignment assignment)
        //{
        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        SqlCommand cmd = new SqlCommand($"UPDATE Course " +
        //                                        $"SET Title = @Title," +
        //                                        $"SET Score = @Score," +
        //                                        $"SET StudentId = @StudentId " + $"WHERE ID = @ID; ", conn);
        //        cmd.Parameters.AddWithValue("@Name", assignment.Title);
        //        cmd.Parameters.AddWithValue("@Score", assignment.Score);
        //        cmd.Parameters.AddWithValue("@StudentId", assignment.StudentId);
        //        cmd.Parameters.AddWithValue("@ID", assignment.Id);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //public void DeleteAssignment(int assignmentId)
        //{
        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        SqlCommand cmd = new SqlCommand($"DELETE FROM Assignment WHERE ID = @ID;", conn);
        //        cmd.Parameters.AddWithValue("@ID", assignmentId);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}
    }
}

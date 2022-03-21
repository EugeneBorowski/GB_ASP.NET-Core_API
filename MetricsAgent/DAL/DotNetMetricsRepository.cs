using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using MetricsAgent.Interface;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetrics>
    {

    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {


        private const string ConnectString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IList<DotNetMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM dotnetmetrics";
            var returnlist = new List<DotNetMetrics>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnlist.Add(new DotNetMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnlist;
        }

        public DotNetMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM dotnetmetrics WHERE id=@id";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new DotNetMetrics()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(1))
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public void Create(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO dotnetmetrics (value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "UPDATE dotnetmetrics SET value = @value, time=@time WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "DELETE FROM dotnetmetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}

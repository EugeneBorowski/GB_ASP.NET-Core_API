using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.Interface;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface IRamMetricsRepository : IRepository<RamMetrics>
    {

    }
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string ConnectString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IList<RamMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM rammetrics";
            var returnlist = new List<RamMetrics>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnlist.Add(new RamMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnlist;
        }

        public RamMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM rammetrics WHERE id=@id";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new RamMetrics()
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

        public void Create(RamMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO rammetrics (value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(RamMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "UPDATE rammetrics SET value = @value, time=@time WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(RamMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "DELETE FROM rammetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}

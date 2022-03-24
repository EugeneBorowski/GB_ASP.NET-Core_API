using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.Interface;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface IHddMetricsRepository : IRepository<HddMetrics>
    {

    }
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private const string ConnectString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IList<HddMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics";
            var returnlist = new List<HddMetrics>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnlist.Add(new HddMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnlist;
        }

        public HddMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE id=@id";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new HddMetrics()
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

        public void Create(HddMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO hddmetrics (value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(HddMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "UPDATE hddmetrics SET value = @value, time=@time WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(HddMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "DELETE FROM hddmetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}

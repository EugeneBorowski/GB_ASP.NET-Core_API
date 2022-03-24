using MetricsAgent.Interface;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetrics>
    {

    }
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private const string ConnectString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IList<CpuMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics";
            var returnlist = new List<CpuMetrics>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnlist.Add(new CpuMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnlist;
        }

        public CpuMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics WHERE id=@id";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new CpuMetrics
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

        public void Create(CpuMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO cpumetrics (value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value",item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(CpuMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "UPDATE cpumetrics SET value = @value, time=@time WHERE id=@id";
            cmd.Parameters.AddWithValue("@id",item.Id);
            cmd.Parameters.AddWithValue("@value",item.Value);
            cmd.Parameters.AddWithValue("@time",item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(CpuMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "DELETE FROM cpumetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}

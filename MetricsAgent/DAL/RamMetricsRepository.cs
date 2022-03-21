using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.Interface;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface IRamMetricsRepository : IRepository<RamMetrics>
    {

    }
    public class RamMetricsRepository : IRamMetricsRepository
    {
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        private const string ConnectString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IList<RamMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                return connection.Query<RamMetrics>("SELECT * FROM Rammetrics").ToList();
            }
        }

        public RamMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectString);
            return connection.QuerySingle<RamMetrics>("SELECT * FROM Rammetrics WHERE id=@id", new
            {
                id
            });
        }

        public void Create(RamMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                connection.Execute("INSERT INTO Rammetrics (value, time) VALUES(@value, @time)", new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds
                });
            }
        }

        public void Update(RamMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                connection.Execute("UPDATE Rammetrics SET value = @value, time=@time WHERE id=@id", new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds,
                    id = item.Id
                });
            }
        }

        public void Delete(RamMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Execute("DELETE FROM Rammetrics WHERE id=@id", new
            {
                id = item.Id
            });
        }
    }
}

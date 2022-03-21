using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
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
        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }


        private const string ConnectString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IList<DotNetMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                return connection.Query<DotNetMetrics>("SELECT * FROM dotnetmetrics").ToList();
            }
        }

        public DotNetMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectString);
            return connection.QuerySingle<DotNetMetrics>("SELECT * FROM dotnetmetrics WHERE id=@id", new
            {
                id
            });
        }

        public void Create(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                connection.Execute("INSERT INTO dotnetmetrics (value, time) VALUES(@value, @time)", new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds
                });
            }
        }

        public void Update(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                connection.Execute("UPDATE dotnetmetrics SET value = @value, time=@time WHERE id=@id", new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds,
                    id = item.Id
                });
            }
        }

        public void Delete(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id", new
            {
                id = item.Id
            });
        }
    }
}

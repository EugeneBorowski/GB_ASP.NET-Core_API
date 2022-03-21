using Dapper;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetrics>
    {

    }
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        private const string ConnectString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IList<CpuMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                return connection.Query<CpuMetrics>("SELECT * FROM cpumetrics").ToList();
            }
        }

        public CpuMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectString);
            return connection.QuerySingle<CpuMetrics>("SELECT * FROM cpumetrics WHERE id=@id", new
            {
                id
            });
        }

        public void Create(CpuMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                connection.Execute("INSERT INTO cpumetrics (value, time) VALUES(@value, @time)", new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds
                });
            }
        }

        public void Update(CpuMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                connection.Execute("UPDATE cpumetrics SET value = @value, time=@time WHERE id=@id", new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds,
                    id = item.Id
                });
            }
        }

        public void Delete(CpuMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Execute("DELETE FROM cpumetrics WHERE id=@id", new
            {
                id = item.Id
            });
        }
    }
}

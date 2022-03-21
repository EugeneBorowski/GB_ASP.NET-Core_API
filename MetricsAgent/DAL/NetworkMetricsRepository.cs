using Dapper;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetrics>
    {

    }
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        private const string ConnectString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IList<NetworkMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                return connection.Query<NetworkMetrics>("SELECT * FROM networkmetrics").ToList();
            }
        }

        public NetworkMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectString);
            return connection.QuerySingle<NetworkMetrics>("SELECT * FROM networkmetrics WHERE id=@id", new
            {
                id
            });
        }

        public void Create(NetworkMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                connection.Execute("INSERT INTO networkmetrics (value, time) VALUES(@value, @time)", new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds
                });
            }
        }

        public void Update(NetworkMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            {
                connection.Execute("UPDATE networkmetrics SET value = @value, time=@time WHERE id=@id", new
                {
                    value = item.Value,
                    time = item.Time.TotalSeconds,
                    id = item.Id
                });
            }
        }

        public void Delete(NetworkMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectString);
            connection.Execute("DELETE FROM networkmetrics WHERE id=@id", new
            {
                id = item.Id
            });
        }
    }
}

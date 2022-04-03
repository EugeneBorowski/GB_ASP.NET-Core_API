using System;
using System.Data;
using Dapper;

namespace MetricsAgent.DAL
{
    public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override void SetValue(IDbDataParameter parameter, TimeSpan value)
        {
            parameter.Value = value;
        }

        public override TimeSpan Parse(object value)
        {
            return TimeSpan.FromSeconds((long)value);
        }
    }
}

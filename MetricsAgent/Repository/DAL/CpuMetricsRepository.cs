using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Repository.DAL
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private ISqlSettings _sqlSettings;
        public CpuMetricsRepository(ISqlSettings sqlSettings)
        {
            _sqlSettings = sqlSettings;
        }

        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(_sqlSettings.GetConnestionString());
            connection.Open();

            // создаем команду
            using var cmd = new SQLiteCommand (connection);

            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";

            // добавляем параметры в запрос из нашего объекта
            cmd.Parameters.AddWithValue("@value", item.Value);

            // в таблице будем хранить время в секундах, потому преобразуем перед записью в секунды
            // через свойство
            cmd.Parameters.AddWithValue("@time", item.Time.ToString());

            // подготовка команды к выполнению
            cmd.Prepare();

            // выполнение команды
            cmd.ExecuteNonQuery();
        }


        public IList<CpuMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_sqlSettings.GetConnestionString());
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = "SELECT * FROM cpumetrics";

            var returnList = new List<CpuMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new CpuMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTime.Parse(reader.GetString(2))
                    }); 
                }
            }

            return returnList;

        }

        public IList<CpuMetric> GetByTimePeriod(DateTime fromTime, DateTime toTime)
        {
            using var connection = new SQLiteConnection(_sqlSettings.GetConnestionString());
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
           
            cmd.CommandText = "SELECT * FROM cpumetrics";

            var returnList = new List<CpuMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
             
                while (reader.Read())
                {
                    DateTime dbDateTime = DateTime.Parse(reader.GetString(2));
                    if ( fromTime <= dbDateTime && dbDateTime <= toTime)
                    {
                        returnList.Add(new CpuMetric
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetInt32(1),
                            Time = DateTime.Parse(reader.GetString(2))
                        });
                    }
                 
                }
            }

            return returnList;

        }

        
    }
}

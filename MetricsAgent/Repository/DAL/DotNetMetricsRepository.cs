using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Repository.DAL
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private ISqlSettings _sqlSettings;
        public DotNetMetricsRepository(ISqlSettings sqlSettings)
        {
            _sqlSettings = sqlSettings;
        }
        public void Create(DotNetMetric item)
        {
            using var connection = new SQLiteConnection(_sqlSettings.GetConnestionString());
            connection.Open();

            // создаем команду
            using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)";

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

        public IList<DotNetMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_sqlSettings.GetConnestionString());
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = "SELECT * FROM dotnetmetrics";

            var returnList = new List<DotNetMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new DotNetMetric
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

        public DotNetMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_sqlSettings.GetConnestionString());
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM dotnetmetrics WHERE id=@id";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // если удалось что то прочитать
                if (reader.Read())
                {
                    // возвращаем прочитанное
                    return new DotNetMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTime.Parse(reader.GetString(2))
                    };
                }
                else
                {
                    // не нашлось запись по идентификатору, не делаем ничего
                    return null;
                }
            }
        }
    }
}

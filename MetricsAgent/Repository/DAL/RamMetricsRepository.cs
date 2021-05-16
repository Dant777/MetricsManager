﻿using System.Data.SQLite;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsAgent.Controllers;

namespace MetricsAgent.Repository.DAL
{
    public class RamMetricsRepository : IRamMetricsRepository
    {

        private ISqlSettings _sqlSettings;
        public RamMetricsRepository(ISqlSettings sqlSettings)
        {
            _sqlSettings = sqlSettings;
        }

        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(_sqlSettings.GetConnestionString());
            connection.Open();

            // создаем команду
            using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO rammetrics(value, time) VALUES(@value, @time)";

            // добавляем параметры в запрос из нашего объекта
            cmd.Parameters.AddWithValue("@value", item.Value);

            // в таблице будем хранить время в секундах, потому преобразуем перед записью в секунды
            // через свойство
            cmd.Parameters.AddWithValue("@time", item.Time);

            // подготовка команды к выполнению
            cmd.Prepare();

            // выполнение команды
            cmd.ExecuteNonQuery();
        }

        public IList<RamMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_sqlSettings.GetConnestionString());
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = "SELECT * FROM rammetrics";

            var returnList = new List<RamMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new RamMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = reader.GetInt64(2)
                    });
                }
            }

            return returnList;
        }

        public IList<RamMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(_sqlSettings.GetConnestionString());
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "SELECT * FROM rammetrics";

            var returnList = new List<RamMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    DateTimeOffset dbDateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2));
                    if (fromTime.DateTime <= dbDateTime.DateTime && dbDateTime.DateTime <= toTime.DateTime)
                    {
                        returnList.Add(new RamMetric
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetInt32(1),
                            Time = reader.GetInt64(2)
                        });
                    }

                }
            }

            return returnList;
        }
    }
}

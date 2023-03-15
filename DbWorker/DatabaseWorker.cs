using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Domain;

namespace DbWorker
{
    public class DatabaseWorker
    {
        private const string sqlConnectionString = "Data Source=localhost;Initial Catalog=Lab2_1;Integrated Security=True";
        private const string sqlQueryGetAllLocalityNames = @"SELECT * FROM dbo.locality_name";
        private const string sqlQueryGetAllPlacementAlongTheRoad = @"SELECT * FROM dbo.placement_along_the_road";
        private const string sqlQueryGetAllRoad = @"SELECT * FROM dbo.road";
        private const string sqlQueryGetLocalityNameById = @"SELECT * FROM dbo.locality_name WHERE id=:id";
        private const string sqlQueryGetStopOnTheRoad = @"SELECT * FROM dbo.stop_on_the_road";
        private const string sqlQueryGetRoadById = @"SELECT * FROM dbo.road WHERE id=:id";
        private const string sqlQueryGetPlacementAlongTheRoadById = @"SELECT * FROM dbo.placement_along_the_road WHERE id=:id";

        private SqlConnection connection = null;

        public DatabaseWorker()
        {
            connection = new SqlConnection(sqlConnectionString);
            connection.Open();
        }

        public ObservableCollection<LocalityName> GetAllLocalityNames()
        {
            ObservableCollection<LocalityName> result = new();
            SqlCommand command = new SqlCommand(sqlQueryGetAllLocalityNames, connection);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        LocalityName temporary = new LocalityName();
                        temporary.Id = (long)dataReader.GetInt32(0);
                        temporary.Name = dataReader.GetString(1);
                        result.Add(temporary);
                    }
                }
            }
            return result;
        }

        public ObservableCollection<PlacementAlongTheRoad> GetAllPlacementAlongTheRoad()
        {
            ObservableCollection<PlacementAlongTheRoad> result = new();
            SqlCommand command = new SqlCommand(sqlQueryGetAllPlacementAlongTheRoad, connection);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        PlacementAlongTheRoad temporary = new PlacementAlongTheRoad();
                        temporary.Id = (long)dataReader.GetInt32(0);
                        temporary.Name = dataReader.GetString(1);
                        result.Add(temporary);
                    }
                }
            }
            return result;
        }

        public LocalityName GetLocalityNameById(long id)
        {
            SqlCommand command = new SqlCommand(sqlQueryGetLocalityNameById.Replace(":id", id.ToString()), connection);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read()) 
                    {
                        LocalityName result = new();
                        result.Id = (long)dataReader.GetInt32(0);
                        result.Name = dataReader.GetString(1);
                        return result;
                    }
                }
            }
            return null;
        }

        public ObservableCollection<Road> GetAllRoads()
        {
            ObservableCollection<Road> result = new();
            SqlCommand command = new SqlCommand(sqlQueryGetAllRoad, connection);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Road temporary = new ();
                        temporary.Id = (long)dataReader.GetInt32(0);
                        temporary.StartPoint = GetLocalityNameById(dataReader.GetInt32(1));
                        temporary.EndPoint = GetLocalityNameById(dataReader.GetInt32(2));

                        result.Add(temporary);
                    }
                }
            }
            return result;
        }

        public Road GetRoadById(int id)
        {
            SqlCommand command = new SqlCommand(sqlQueryGetRoadById.Replace(":id", id.ToString()), connection);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Road result = new();
                        result.Id = (long)dataReader.GetInt32(0);
                        result.StartPoint = GetLocalityNameById(dataReader.GetInt32(1));
                        result.EndPoint = GetLocalityNameById(dataReader.GetInt32(2));
                        return result;
                    }
                }
            }
            return null;
        }

        public PlacementAlongTheRoad GetPlacementAlongTheRoadById(int id)
        {
            SqlCommand command = new SqlCommand(sqlQueryGetPlacementAlongTheRoadById.Replace(":id", id.ToString()), connection);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        PlacementAlongTheRoad result = new();
                        result.Id = (long)dataReader.GetInt32(0);
                        result.Name = dataReader.GetString(1);
                        return result;
                    }
                }
            }
            return null;
        }

        public ObservableCollection<StopOnTheRoad> GetAllStopOnTheRoad() 
        {
            ObservableCollection<StopOnTheRoad> result = new();
            SqlCommand command = new SqlCommand(sqlQueryGetStopOnTheRoad, connection);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        StopOnTheRoad temporary = new();
                        temporary.Id = (long)dataReader.GetInt32(0);
                        temporary.Road = GetRoadById(dataReader.GetInt32(1));
                        temporary.IsHavePavilion = dataReader.GetString(2);
                        temporary.PlacementAlongTheRoad = GetPlacementAlongTheRoadById(dataReader.GetInt32(3));
                        temporary.RangeFromStart = dataReader.GetFloat(4);
                        temporary.BusStopName = dataReader.GetString(5);

                        result.Add(temporary);
                    }
                }
            }
            return result;
        }
    }
}
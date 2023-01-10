using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using FlightPlanApi.Models;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Security;

namespace FlightPlanApi.Data
{
    public class MongoDBdatabase : IDatabaseAdapter
    {
        private IMongoCollection<BsonDocument> GetCollection(string databaseName, string collectionName)
        {
            var client = new MongoClient();
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);
            return collection;
        }

        private FlightPlan ConvertBsonToFlightPlan(BsonDocument document)
        {
            if (document == null) return null;

            return new FlightPlan
            {
                FlightPlanId = document["flight_plan_id"].AsString,
                Altitude = document["altitude"].AsInt32,
                Airspeed = document["airspeed"].AsInt32,
                AircraftIdentification = document["aircraft_identification"].AsString,
                AircraftType = document["aircraft_type"].AsString,
                ArrivalAirPort = document["arrival_airport"].AsString,
                FlightType = document["flight_type"].AsString,
                // 'ToUniversalTime' = UTC
                DepartureTime = document["departure_time"].AsBsonDateTime.ToUniversalTime(),
                ArrivalTime = document["estimated_arrival_time"].AsBsonDateTime.ToUniversalTime(),
                Route = document["route"].AsString,
                Remarks = document["remarks"].AsString,
                FuelHours = document["fuel_hours"].AsInt32,
                FuleMinutes = document["fuel_minutes"].AsInt32,
                NumberOnboard = document["number_onboard"].AsInt32


            };


        }

        public async Task<List<FlightPlan>> GetAllFlightPlans()
        {
            var collection = GetCollection("pluralsight", "flight_plans");
            // The Find method returns a list of all documents
            var documents = await collection.Find(_ => true).ToListAsync();

            var flightPlanList = new List<FlightPlan>();
            // If no documents in the Monogodb -> return empty flight plan list
            if (documents == null) return flightPlanList;

            foreach (var document in documents)
            {
                // Convert Mongodb's Bson document to FlightPlan POCO
                flightPlanList.Add(ConvertBsonToFlightPlan(document));
            }

            return flightPlanList;
        }

        public async Task<FlightPlan> GetFlightPlanbyId(string flightPlanId)
        {
            var collection = GetCollection("pluralsight", "flight_plans");
            // The Find method returns a list of all documents
            var flightPlanCursor = await collection.FindAsync(Builders<BsonDocument>.Filter.Eq("flight_plan_id", flightPlanId));

            var document = flightPlanCursor.FirstOrDefault();
            var flightPlan = ConvertBsonToFlightPlan(document);

            if (flightPlan == null)
            {
                return new FlightPlan();
            }

            return flightPlan;

        }

        public async Task<bool> FileFlightPlan(FlightPlan flightPlan)
        {
            var collection = GetCollection("pluralsight", "flight_plans");

            // convert the FlightPlan C# object to Bson document
            var document = new BsonDocument
            {
               {"flight_plan_id", Guid.NewGuid().ToString("N") },
                {"altitude", flightPlan.Altitude },
                {"airspeed", flightPlan.Airspeed },
                {"aircraft_identification", flightPlan.AircraftIdentification },
                {"aircraft_type", flightPlan.AircraftType },
                {"arrival_airport", flightPlan.ArrivalAirPort },
                {"flight_type", flightPlan.FlightType },
                {"departing_airport", flightPlan.DepartureAirPort },
                {"departure_time", flightPlan.DepartureTime },
                {"estimated_arrival_time", flightPlan.ArrivalTime },
                {"route", flightPlan.Route },
                {"remarks", flightPlan.Remarks },
                {"fuel_hours", flightPlan.FuelHours },
                {"fuel_minutes", flightPlan.FuleMinutes },
                {"number_onboard", flightPlan.NumberOnboard }

            };

            // Try to insert the document to the DB
            try
            {
                await collection.InsertOneAsync(document);
            }
            catch
            {
                // If flight plan to the DB failed -> return false
                return false;
            }

            // If flight plan to the DB succeded -> return true
            return true;
        }

        public async Task<bool> UpdateFlightPlan(string FlightPlanId, FlightPlan flightPlan)
        {
            var collection = GetCollection("pluralsight", "flight_plans");
            var filter = Builders<BsonDocument>.Filter.Eq("flight_plan_id", FlightPlanId);
            var update = Builders<BsonDocument>.Update
                .Set("altitude", flightPlan.Altitude)
                .Set("airspeed", flightPlan.Airspeed)
                .Set("aircraft_identification", flightPlan.AircraftIdentification)
                .Set("aircraft_type", flightPlan.AircraftType)
                .Set("arrival_airport", flightPlan.ArrivalAirPort)
                .Set("flight_type", flightPlan.FlightType)
                .Set("departing_airport", flightPlan.DepartureAirPort)
                .Set("departure_time", flightPlan.DepartureTime)
                .Set("estimated_arrival_time", flightPlan.ArrivalTime)
                .Set("route", flightPlan.Route)
                .Set("remarks", flightPlan.Remarks)
                .Set("fuel_hours", flightPlan.FuelHours)
                .Set("fuel_minutes", flightPlan.FuleMinutes)
                .Set("numberOnBoard", flightPlan.NumberOnboard);
            var result = await collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteFlightPlan(string flightPlanId)
        {
            var collection = GetCollection("pluralsight", "flight_plans");
            var result = await collection.DeleteOneAsync(
                Builders<BsonDocument>.Filter.Eq("flight_plan_id", flightPlanId));

            return result.DeletedCount > 0;
        }
    }
}
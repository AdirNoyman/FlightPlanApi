using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightPlanApi.Models
{
    public class FlightPlan
    {
        // We add the 'JsonPropertyName' so .NET could easly serialise and deserialise this class(model) to and from JSON
        [JsonPropertyName("flight_plan_id")]
        public string FlightPlanId { get; set; }
        [JsonPropertyName("adircraft_identification")]
        public string AircraftIdentification { get; set; }
        [JsonPropertyName("adircraft_type")]
        public string AircraftType { get; set; }
        [JsonPropertyName("airspeed")]
        public int Airspeed { get; set; }
        [JsonPropertyName("altitude")]
        public int Altitude { get; set; }
        [JsonPropertyName("flight_type")]
        public string FlightType { get; set; }
        [JsonPropertyName("fuel_hours")]
        public int FuelHours { get; set; }
        [JsonPropertyName("fule_minutes")]
        public int FuleMinutes { get; set; }
        [JsonPropertyName("departure_time")]
        public DateTime DepartureTime { get; set; }
        [JsonPropertyName("estimated_arrival_time")]
        public DateTime ArrivalTime { get; set; }
        [JsonPropertyName("departing_airport")]
        public string DepartureAirPort { get; set; }
        [JsonPropertyName("arrival_airport")]
        public string ArrivalAirPort { get; set; }
        [JsonPropertyName("route")]
        public string Route { get; set; }
        [JsonPropertyName("remarks")]
        public string Remarks { get; set; }
        [JsonPropertyName("number_onboard")]
        public int NumberOnboard { get; set; }

    }
}
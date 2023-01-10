using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanApi.Models;

namespace FlightPlanApi.Data
{
    public interface IDatabaseAdapter
    {

        Task<List<FlightPlan>> GetAllFlightPlans();
        Task<FlightPlan> GetFlightPlanbyId(string flightPlanId);
        Task<bool> FileFlightPlan(FlightPlan flightPlan);
        Task<bool> UpdateFlightPlan(string FlightPlanId, FlightPlan flightPlan);
        Task<bool> DeleteFlightPlan(string flightPlanId);

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanApi.Data;
using FlightPlanApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Controllers
{
    [ApiController]
    [Route("api/v1/flightplan")]
    public class FlightPlanController : ControllerBase
    {
        private IDatabaseAdapter _database;

        public FlightPlanController(IDatabaseAdapter database)
        {
            _database = database;
        }

        public async Task<IActionResult> GetAllFlightPlans()
        {

        }

        public async Task<IActionResult> GetFlightPlan(string flightPlanId)
        {

        }

        public async Task<IActionResult> CreateFlightPlan(FlightPlan flightPlan)
        {

        }

        public async Task<IActionResult> UpdateFlightPlan(FlightPlan flightPlan)
        {

        }

        public async Task<IActionResult> DeleteFlightPlan(string flightPlanId)
        {

        }

        public async Task<IActionResult> GetFlightPlanDepartureAirport(string flightPlanId)
        {

        }

        public async Task<IActionResult> GetFlightPlanRoute(string flightPlanId)
        {

        }

        public async Task<IActionResult> GetFlightPlanTimeEnRoute(string flightPlanId)
        {

        }

    }
}
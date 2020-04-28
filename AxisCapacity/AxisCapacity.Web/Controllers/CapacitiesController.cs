using System;
using System.Globalization;
using System.Linq;
using AxisCapacity.Common;
using AxisCapacity.Data;
using AxisCapacity.Engine;
using Microsoft.AspNetCore.Mvc;

namespace AxisCapacity.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CapacitiesController : ControllerBase
    {
        private readonly ICapacityRepository _repository;

        private readonly ICapacityEngine _engine;

        public CapacitiesController(ICapacityRepository repository, ICapacityEngine engine)
        {
            _repository = repository;
            _engine = engine;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string terminal, [FromQuery] string shift, [FromQuery] string date, [FromQuery] string view)
        {
            try
            {
                var queryDate = string.IsNullOrEmpty(date) ? (DateTime?) null : DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                var queryShift = string.IsNullOrEmpty(shift) ? null : Shift.From(shift);
                var queryView = string.IsNullOrEmpty(view) ? null : View.From(view);

                if (queryDate.HasValue && !string.IsNullOrEmpty(terminal) && queryShift != null)
                {
                    return HandleExactQuery(terminal, queryShift, queryDate.Value);
                }
                
                if (queryDate.HasValue && !string.IsNullOrEmpty(terminal) && queryView != null)
                {
                    return HandleViewQuery(terminal, queryView, queryDate.Value);
                }

                return BadRequest("Terminal, shift, date are required fields.");
            }
            catch (FormatException e)
            {
                return BadRequest(e);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
        }

        private IActionResult HandleExactQuery(string terminal, Shift shift, DateTime date)
        {
            var result = _repository.GetCapacity(terminal, shift, date);
            if (result == null)
            {
                return NotFound($"Did not find corresponding depot capacity entry for terminal '{terminal}', shift '{shift.Value()}, date '{date:yyyy/MM/dd}'");
            }

            var overrideResult = _repository.GetDateCapacity(terminal, shift, date);
            if (overrideResult != null)
            {
                result = overrideResult;
            }

            result.Capacity ??= _engine.CalculateCapacity(result.Load, result.Deliveries, result.Shifts);
            if (!result.Capacity.HasValue)
            {
                return BadRequest("Could not calculate capacity for terminal '{terminal}', shift '{shift.Value()}, date '{date:yyyy/MM/dd}'. Missing depot entry parameter values?");
            }

            return Ok(result);
        }

        private IActionResult HandleViewQuery(string terminal, View view, DateTime dateTime)
        {
            if (view.IsMonth())
            {
                return HandleMonth(terminal, dateTime);
            }

            return BadRequest("Only a view of month is currently supported.");
        }

        
        private IActionResult HandleMonth(string terminal, in DateTime date)
        {
            var weekCapacity = _repository.GetCapacities(terminal, null, null);
            if (weekCapacity == null)
            {
                return NotFound($"Did not find corresponding depot capacity entry for terminal '{terminal}'.");
            }

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            var monthCapacity = _repository.GetDateCapacities(terminal, null, firstDayOfMonth, lastDayOfMonth);

            var finalResult = weekCapacity.Concat(monthCapacity).ToList();

            foreach (var item in finalResult)
            {
                if (!item.Capacity.HasValue)
                {
                    item.Capacity = _engine.CalculateCapacity(item.Load, item.Deliveries, item.Shifts);
                }
            }


            return Ok(finalResult);
        }


    }
}
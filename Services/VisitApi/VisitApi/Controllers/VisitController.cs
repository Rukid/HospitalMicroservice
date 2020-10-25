using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitApi.Domain;
using VisitApi.Models;
using VisitApi.Service.Command;
using VisitApi.Service.Query;

namespace VisitApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public VisitController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a new visit in the database.
        /// </summary>
        /// <param name="visitModel">Model to create a new visit</param>
        /// <returns>Returns the created visit</returns>
        /// <response code="200">Returned if the visit was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Visit>> Visit([FromBody] VisitModel visitModel)
        {
            try
            {
                return await _mediator.Send(new CreateVisitCommand
                {
                    Visit = _mapper.Map<Visit>(visitModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Action to retrieve all pay visits.
        /// </summary>
        /// <returns>Returns a list of all initial visits or an empty list</returns>
        /// <response code="200">Returned if the list of visits was retrieved</response>
        /// <response code="400">Returned if the visits could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<Visit>>> Visits()
        {
            try
            {
                return await _mediator.Send(new GetInitialVisitsQuery());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Visit action.
        /// </summary>
        /// <param name="id">The id of the visit</param>
        /// <returns>Returns the completed visit</returns>
        /// <response code="200">Returned if the visit was updated</response>
        /// <response code="400">Returned if the visit could not be found with the provided id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("Completed/{id}")]
        public async Task<ActionResult<Visit>> Pay(Guid id)
        {
            try
            {
                var visit = await _mediator.Send(new GetVisitByIdQuery
                {
                    Id = id
                });

                if (visit == null)
                {
                    return BadRequest($"No visit found with the id {id}");
                }

                visit.VisitId = 2;

                return await _mediator.Send(new VisitCompletedCommand()
                {
                    Visit = _mapper.Map<Visit>(visit)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

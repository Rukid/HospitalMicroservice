using System;
using MediatR;
using AutoMapper;
using DoctorApi.Models;
using DoctorApi.Data.Repository;
using DoctorApi.Domain.Entities;
using DoctorApi.Service.Command;
using DoctorApi.Service.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]   
    public class DoctorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDoctorRepository _doctoRepository;

        public DoctorController(IMapper mapper, IMediator mediator, IDoctorRepository doctorRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _doctoRepository = doctorRepository;
        }

        /// <summary>
        /// Action to retrieve all doctors.
        /// </summary>
        /// <returns>Returns a list of all doctors</returns>
        /// <response code="200">Returned if the list of doctors was retrieved</response>
        /// <response code="400">Returned if the doctors could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Doctors()
        {
            try
            {
                return await _doctoRepository.GetAllAsync();
            }
            catch (Exception ex)
            {               
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to create a new doctor in the database.
        /// </summary>
        /// <param name="createDoctorModel">Model to create a new doctor</param>
        /// <returns>Returns the created doctor</returns>
        /// <response code="200">Returned if the doctor was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the doctor couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Doctor>> DoctorAsync([FromBody] CreateDoctorModel createDoctorModel)
        {
            try
            {
                return await _mediator.Send(new CreateDoctorCommand
                {
                    Doctor = _mapper.Map<Doctor>(createDoctorModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to update an existing doctor
        /// </summary>
        /// <param name="updateDoctorModel">Model to update an existing doctor</param>
        /// <returns>Returns the updated doctor</returns>
        /// <response code="200">Returned if the doctor was updated</response>
        /// <response code="400">Returned if the model couldn't be parsed or the doctor couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<Doctor>> DoctorAsync([FromBody] UpdateDoctorModel updateDoctorModel)
        {
            try
            {
                var doctor = await _mediator.Send(new GetDoctorByIdQuery
                {
                    Id = updateDoctorModel.Id
                });

                if (doctor == null)
                {
                    return BadRequest($"No doctor found with the id {updateDoctorModel.Id}");
                }

                return await _mediator.Send(new UpdateDoctorCommand
                {
                    Doctor = _mapper.Map(updateDoctorModel, doctor)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

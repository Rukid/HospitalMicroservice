using System;
using MediatR;
using AutoMapper;
using ClientApi.Models;
using ClientApi.Data.Repository;
using ClientApi.Domain.Entities;
using ClientApi.Service.Command;
using ClientApi.Service.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]   
    public class ClientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IClientRepository _clientRepository;
        private readonly IGenderRepository _genderRepository;

        public ClientController(IMapper mapper, IMediator mediator, IClientRepository clientRepository, IGenderRepository genderRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _clientRepository = clientRepository;
            _genderRepository = genderRepository;
        }

        /// <summary>
        /// Action to retrieve all clients.
        /// </summary>
        /// <returns>Returns a list of all clients</returns>
        /// <response code="200">Returned if the list of clients was retrieved</response>
        /// <response code="400">Returned if the clients could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Clients()
        {
            try
            {
                return await _clientRepository.GetAllAsync();
            }
            catch (Exception ex)
            {               
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to create a new client in the database.
        /// </summary>
        /// <param name="createClientModel">Model to create a new client</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the client was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the client couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Client>> ClientAsync([FromBody] CreateClientModel createClientModel)
        {
            try
            {
                return await _mediator.Send(new CreateClientCommand
                {
                    Client = _mapper.Map<Client>(createClientModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to update an existing client
        /// </summary>
        /// <param name="updateClientModel">Model to update an existing client</param>
        /// <returns>Returns the updated client</returns>
        /// <response code="200">Returned if the client was updated</response>
        /// <response code="400">Returned if the model couldn't be parsed or the client couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<Client>> ClientAsync([FromBody] UpdateClientModel updateClientModel)
        {
            try
            {
                var client = await _mediator.Send(new GetClientByIdQuery
                {
                    Id = updateClientModel.Id
                });

                if (client == null)
                {
                    return BadRequest($"No client found with the id {updateClientModel.Id}");
                }

                return await _mediator.Send(new UpdateClientCommand
                {
                    Client = _mapper.Map(updateClientModel, client)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to retrieve gender types.
        /// </summary>
        /// <returns>Returns a list of gender types</returns>
        /// <response code="200">Returned if the list of genders was retrieved</response>
        /// <response code="400">Returned if the genders could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("gendertypes")]
        public async Task<ActionResult<List<GenderType>>> GenderTypes()
        {
            try
            {
                return await _genderRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

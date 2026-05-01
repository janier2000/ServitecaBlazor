using Serviteca.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Serviteca.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Backend.UnitsOfWork.Interfaces;

namespace Serviteca.Backend.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class DocumentTypeController : GenericController<DocumentType>
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        public DocumentTypeController(IGenericUnitOfWork<DocumentType> unitOfWork, IDocumentTypeRepository documentTypeRepository) : base(unitOfWork)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _documentTypeRepository.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _documentTypeRepository.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _documentTypeRepository.GetComboAsync());
        }
    }
}
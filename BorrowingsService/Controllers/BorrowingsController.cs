using BorrowingsService.Models;
using BorrowingsService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BorrowingsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;
        private readonly ILogger _logger;

        public BorrowingsController(IBorrowingService borrowingService, ILogger logger)
        {
            _borrowingService = borrowingService ?? throw new ArgumentNullException(nameof(borrowingService));
            _logger = logger ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllAsync()
        {
            var allBorrowings = await _borrowingService.ReadAllAsync();
            return Ok(allBorrowings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadOneAsync(int id)
        {
            try
            {
                var borrowing = await _borrowingService.ReadOneAsync(id);
                if (borrowing == null)
                {
                    _logger.Error(messageTemplate: $"Code 404 returned reading item with id {id} - operation failed.");
                    return NotFound();
                                    }
                _logger.Information(messageTemplate: $"Code 200 returned reading item with id {id} - operation properly completed.");
                return Ok(borrowing);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, messageTemplate: $"An exceprion was throwned reading item with id {id}:");
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Borrowing borrowing)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _borrowingService.CreateAsync(borrowing);
                    //return CreatedAtAction(nameof(ReadOneAsync), new { id = borrowing.Id }, borrowing);
                    return Ok();
                }
                else
                    return BadRequest();
            } 
            catch (Exception)
            {
                throw;
            }
            
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Borrowing borrowing) 
        {
            try
            {
                if (ModelState.IsValid && borrowing.Id == id)
                {
                    var result = await _borrowingService.UpdateAsync(id, borrowing);
                    if (result != null)
                        return Ok();
                    else
                        return NotFound();
                }
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _borrowingService.DeleteAsync(id);
                if (result != null)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

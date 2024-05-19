using TodoApi.Models;
using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BooksController : ControllerBase
{
  private readonly BooksService _bookService;

  public BooksController(BooksService bookService) => _bookService = bookService;

  [HttpGet]
  public async Task<List<Book>> Get() => await _bookService.GetAsync();

  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Book>> Get(string id)
  {
    var book = await _bookService.GetAsync(id);

    if (book is null)
    {
      return NotFound();
    }

    return book;
  }

  [HttpPost]
  public async Task<IActionResult> Post(Book newBook)
  {
    await _bookService.CreateAsync(newBook);

    return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Put(string id, Book updatedBook)
  {
    var book = await _bookService.GetAsync(id);

    if (book is null)
    {
      return NotFound();
    }

    updatedBook.Id = book.Id;

    await _bookService.UpdateAsync(id, updatedBook);

    return NoContent();
  }

  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var book = await _bookService.GetAsync(id);

    if (book is null)
    {
      return NotFound();
    }

    await _bookService.RemoveAsync(id);

    return NoContent();
  }
}
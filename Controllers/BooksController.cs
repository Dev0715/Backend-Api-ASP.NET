using TodoApi.Models;
using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BooksController : ControllerBase
{
  private readonly BooksService _bookService;

  public BooksController(BooksService bookService)
  {
    _bookService = bookService;
  }

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
  public async Task<ActionResult<Book>> Post(Book newBook)
  {
    await _bookService.CreateAsync(newBook);

    return CreatedAtRoute(nameof(Get), new { id = newBook.Id }, newBook);
  }
}
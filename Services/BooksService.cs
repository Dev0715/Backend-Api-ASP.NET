using TodoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TodoApi.Services;

public class BooksService
{
  private readonly IMongoCollection<Book> _booksCollection;

  public BooksService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
  {
    var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
    var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
    _booksCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName);
  }

  public async Task<List<Book>> GetAsync() => await _booksCollection.Find(_ => true).ToListAsync();

  public async Task<Book?> GetAsync(string id) => await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
}
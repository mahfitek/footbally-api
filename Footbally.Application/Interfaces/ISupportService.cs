namespace Footbally.Application.Interfaces;

public interface ISupportService
{
    Task<string> AskAsync(string question);
}
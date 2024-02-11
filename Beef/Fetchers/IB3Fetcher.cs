namespace Beef.Fetchers; 

public interface IB3Fetcher<TRequest, TResponse> {
    Task<IEnumerable<TResponse>> Fetch(TRequest request);
}
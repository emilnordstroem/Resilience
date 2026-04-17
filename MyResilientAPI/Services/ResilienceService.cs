using Polly.Registry;

namespace MyResilientAPI.Services
{
	public class ResilienceService
	{
		private readonly HttpClient _client;
		private readonly ResiliencePipelineProvider<string> _pipelineProvider;

		public ResilienceService(HttpClient client, ResiliencePipelineProvider<string> pipelineProvider)
		{
			_client = client;
			_pipelineProvider = pipelineProvider;
		}
		public async Task<string?> DoWork(CancellationToken cancellationToken)
		{
			var pipeline = _pipelineProvider.GetPipeline("my-strategy");
			return await pipeline.ExecuteAsync(async token =>
			{
				return await _client.GetFromJsonAsync<string>("https://localhost:7181/api/fault/timeout", token);
			}, cancellationToken);
		}
	}
}
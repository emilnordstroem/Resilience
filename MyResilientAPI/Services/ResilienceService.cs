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
				var response = await _client.GetAsync(
					"https://localhost:7181/api/fault/unstable",
					token);
				return await response.Content.ReadAsStringAsync(token);
			}, cancellationToken);
		}
	}
}
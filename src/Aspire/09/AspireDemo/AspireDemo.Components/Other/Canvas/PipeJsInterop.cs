using Microsoft.JSInterop;

namespace AspireDemo.Components.Other.Canvas
{
    internal class PipeJsInterop(IJSRuntime jsRuntime) : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/AspireDemo.Components/pipe.js").AsTask());

        public async ValueTask DrawPipeAsync(PipeDisplay pipe)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("drawPipe", pipe);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}

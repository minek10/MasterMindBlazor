using Microsoft.AspNetCore.Components;

namespace MasterMind.Pages.Modal
{
    public partial class EndGameModal
    {
        [Parameter] public bool IsWin { get; set; }
        [Parameter] public int CounterSecond { get; set; }
        [Parameter] public string UserName { get; set; } = string.Empty;
        [Parameter] public int Attempt { get; set; }
        [Parameter] public EventCallback<bool> CloseModalCallBack { get; set; }
        public string Chrono { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(CounterSecond));
            Chrono = t.ToString(@"mm\:ss");
            StateHasChanged();
        }

        public async Task ReturnToGame()
        {
            await CloseModalCallBack.InvokeAsync(false);
        }
    }
}

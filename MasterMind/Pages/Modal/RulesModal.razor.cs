using Microsoft.AspNetCore.Components;

namespace MasterMind.Pages.Modal
{
    public partial class RulesModal
    {
        [Parameter] public EventCallback<bool> CloseModalCallBack { get; set; }
        public List<string> Colors { get; set; } = new List<string>() { "red", "blue", "orange", "green", "yellow", "brown", "pink", "purple" };

        public async Task CloseModal()
        {
            await CloseModalCallBack.InvokeAsync(false);
        }
    }
}

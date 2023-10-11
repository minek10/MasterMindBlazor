using Microsoft.AspNetCore.Components;

namespace MasterMind.Pages
{
    public partial class Index
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        public bool IsShowRulesModal { get; set; } = false;
        public bool IsShowDownloadAppModal { get; set; } = false;

        public void RedirectToGame()
        {
            NavigationManager.NavigateTo("/game");
        }

        public void CloseRulesModal()
        {
            IsShowRulesModal = false;
            StateHasChanged();
        }

        public void CloseDownloadAppModal()
        {
            IsShowDownloadAppModal = false;
            StateHasChanged();
        }
    }
}

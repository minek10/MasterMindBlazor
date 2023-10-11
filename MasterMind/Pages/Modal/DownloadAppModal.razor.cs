using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;

namespace MasterMind.Pages.Modal
{
    public partial class DownloadAppModal
    {
        [Parameter] public EventCallback<bool> CloseModalCallBack { get; set; }

        public bool IsAndroid { get; set; } = true;
        public bool IsApple { get; set; } = false;
        public bool IsWindows { get; set; } = false;


        public async Task CloseModal()
        {
            await CloseModalCallBack.InvokeAsync(false);
        }

        protected override async Task OnInitializedAsync()
        {
            //DetectOs();
        }

        public void DetectOs()
        {
            bool IsApple = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
         
            if (!IsApple && !IsWindows)
            {
                IsAndroid = true;
            }
            var aa = 0;
        }

        public void ActivePlatform(string platform)
        {
            switch (platform)
            {
                case "android" :
                    IsAndroid = true;
                    IsApple = false;
                    IsWindows = false;
                    break;
                case "ios":
                    IsAndroid = false;
                    IsApple = true;
                    IsWindows = false;
                    break;
                case "windows":
                    IsAndroid = false;
                    IsApple = false;
                    IsWindows = true;
                    break;
            }
            StateHasChanged();
        }
    }
}

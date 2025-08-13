using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorAppPWA.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        IJSRuntime? JS { get; set; }

        public bool visibleDialogInstallApp { get; set; } = true;



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var isInstalled = await JS.InvokeAsync<bool>("pwaHelper.isAppInstalled");
                if (!isInstalled)
                {
                    Console.WriteLine("App não instalado");
                    _ = Task.Run(() => JS.InvokeVoidAsync("pwaHelper.listenForInstallPrompt", DotNetObjectReference.Create(this)));
                }
            }
        }

        protected async override void OnInitialized()
        {

        }

        [JSInvokable("ShowInstallPrompt")]
        public void ShowInstallPrompt()
        {
            visibleDialogInstallApp = false;
            StateHasChanged();
        }

        private async Task InstallApp()
        {
            await JS.InvokeVoidAsync("pwaHelper.triggerInstallPrompt");
            visibleDialogInstallApp = true;
        }

    }
}

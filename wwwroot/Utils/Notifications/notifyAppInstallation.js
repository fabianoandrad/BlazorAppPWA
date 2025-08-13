window.pwaHelper = {
  isAppInstalled: function () {
    return window.matchMedia('(display-mode: standalone)').matches || window.navigator.standalone === true;
  },
    listenForInstallPrompt: function (dotNetHelper) {
        try {
            console.log('Entramos em Listening beforeinstallprompt event');

            window.addEventListener("beforeinstallprompt", (event) => {
                console.log('Entramos em beforeinstallprompt event');
                event.preventDefault();
                window.PWADeferredPrompt = event;
                dotNetHelper.invokeMethodAsync('ShowInstallPrompt');
            });

            window.addEventListener("appinstalled", (event) => {
                console.log('App instalado com sucesso!!!!!!!');
            })
        } catch (error) {
            console.error('Error in listenForInstallPrompt:', error);
        }
  },
    triggerInstallPrompt: function () {
        console.log('Entramos em triggerInstallPrompt');

        if (window.PWADeferredPrompt) {
            window.PWADeferredPrompt.prompt();
            window.PWADeferredPrompt.userChoice.then(function (choiceResult) {
                window.PWADeferredPrompt = null;
            });
        }

    }
};

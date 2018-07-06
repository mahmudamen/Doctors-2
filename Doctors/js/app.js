

/**
add serverWorker
*/
registerServiceWorker = () => {
if ('serviceWorker' in navigator) {
  window.addEventListener('load', function () {
    navigator.serviceWorker.register('/js/sw.js', {scope: '/'})
      .then(reg => {
        console.log('sw on main page has been registered')
      }).catch(err => {
      console.log('sw registration fails')
    });
  });
}
};
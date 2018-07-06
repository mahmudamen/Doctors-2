importScripts('/js/idb.js');
importScripts('/js/idbhelper.js');

let staticCacheName = 'restaurants-static-v3';
self.addEventListener('install', (event) => {
      let cacheurl = [
			        './',
              './index.html',
      ];
      event.waitUntil(
    		caches.open(staticCacheName).then( (cache) => {
    			return cache.addAll(cacheurl);
    		})
    	);
});
self.addEventListener('sync', function(event) {
  if (event.tag == 'myFirstSync') {
    event.waitUntil(console.log('myFirstSync'));
  }
});


self.addEventListener('activate', event => {
  event.waitUntil(
    caches.keys().then(cachesNames => {
      return Promise.all(
        cachesNames.filter(cachesName => {
          return cachesName.startsWith('restaurants-') && cachesName != staticCacheName;
        }).map(cachesName => {
          return caches.delete(cachesName);
        })
      )
    })
  );
});

self.addEventListener('fetch', event => {
  event.respondWith(
    caches.match(event.request, { ignoreSearch: true }).then(response => {
      if (response) return response;
      return fetch(event.request);
    })
  )
});
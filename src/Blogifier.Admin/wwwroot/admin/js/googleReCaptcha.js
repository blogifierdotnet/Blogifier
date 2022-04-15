function googleReCaptcha(dotNetObject, selector, sitekeyApiRoute) {
  let sitekeyValue = GetSiteKeyFromUrl(sitekeyApiRoute);
  console.log(sitekeyValue)
  return grecaptcha.render(selector, {
    'sitekey': sitekeyValue,
    'callback': (response) => {
      dotNetObject.invokeMethodAsync('CallbackOnSuccess', response);
    },
    'expired-callback': () => {
      dotNetObject.invokeMethodAsync('CallbackOnExpired', response);
    }
  });
}

function GetSiteKeyFromUrl(url) {
  let xmlHttpReq = new XMLHttpRequest();
  xmlHttpReq.open("GET", url, false);
  xmlHttpReq.send(null);
  return xmlHttpReq.responseText;
}

function getResponse(response) {
  return grecaptcha.getResponse(response);
}

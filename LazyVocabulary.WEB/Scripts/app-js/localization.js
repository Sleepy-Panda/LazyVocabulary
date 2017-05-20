'use strict';

console.log('Supported languages: ' + navigator.languages);
var locale = getCookie('locale');

if (locale != 'ru' && locale != 'en') {
    var browserLanguage = navigator.language || navigator.userLanguage || navigator.browserLanguage || navigator.systemLanguage || 'ru';
    console.log('Browser language: ' + browserLanguage);

    // Transform Russian, Belorussian and Ukrainian language to Russian.
    if (browserLanguage == 'ru' || browserLanguage == 'be' || browserLanguage == 'uk') {
        browserLanguage = 'ru';
    }
        // Transform other languages to English.
    else {
        browserLanguage = 'en';
    }

    locale = browserLanguage;
    setCookie('locale', browserLanguage);
}

var iconName = '#flag-' + locale;
$(iconName).attr('disabled', true);

$('img[id^="flag-"]').click(function (event) {
    var code = event.target.id.slice(-2);
    setCookie('locale', code);
    location.reload(true);
});

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
      "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function setCookie(name, value) {
    value = encodeURIComponent(value);
    var updatedCookie = name + "=" + value;

    // 30 days.
    var date = new Date;
    date.setDate(date.getDate() + 30);

    updatedCookie += '; path=/;';
    updatedCookie += 'expires=' + date.toUTCString() + ';';
    document.cookie = updatedCookie;
}
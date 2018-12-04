jQuery(function() {
	initFoundation();
});

function initFoundation() {
	jQuery(document).foundation();

	jQuery('#search-form, #menu').each(function() {
		var holder = jQuery(this);
		var opener = jQuery('[data-responsive-toggle="' + holder.attr('id') + '"]');

		jQuery(document).on('click', function(e) {
			var target = jQuery(e.target);

			if (!target.is(holder) && !target.closest(holder).length && holder.is(':visible')) {
				opener.foundation('toggleMenu');
			}
		});
	});
}

// Cookie Policy Banner
(function ($) {
    var $cookieBannerContainer = $('.js-cookie-banner-container');

    if ($cookieBannerContainer.length === 0) {
        return;
    }

    var $bannerTemplate = $("#cookie-banner-template");

    var cookieDuration = getDataAttributeValueOrDefault($cookieBannerContainer, "cookieDuration", 14);
    var cookieName = getDataAttributeValueOrDefault($bannerTemplate, "cookieName", "complianceCookie");
    var cookieValue = getDataAttributeValueOrDefault($bannerTemplate, "cookieValue", "true");

    if (checkCookie(cookieName) === cookieValue || $bannerTemplate.length === 0) {
        return;
    }

    displayCookieBanner($cookieBannerContainer, $bannerTemplate);
    createCookie(cookieName, cookieValue, cookieDuration);

    function createCookie(name, value, days) {
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();

            document.cookie = name + "=" + value + expires + "; path=/";
        }
    }

    function checkCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    function displayCookieBanner($container, $bannerTemplate) {
        var htmlContent = $bannerTemplate.text();
        $container.prepend(htmlContent);
    }

    function getDataAttributeValueOrDefault($element, key, defaultValue) {
        var dataAttribute = $element.data(key);

        if (dataAttribute === undefined) {
            return defaultValue;
        }

        return dataAttribute;
    }
})($);
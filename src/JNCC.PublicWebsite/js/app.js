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

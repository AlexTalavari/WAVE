(function($)
{

	$("#owl-actions").owlCarousel({
		autoPlay: 5000,
		items: 5,
		itemsCustom : [
	        [0, 1],
	        [450, 1],
	        [600, 1],
	        [700, 2],
	        [1000, 3],
	        [1200, 3],
	        [1400, 3],
	        [1600, 3]
		]
	})
	.find('a').on('click', function(e){
		e.stopPropagation();
	});
	$("#owl-users").owlCarousel({
	    autoPlay: 5000,
	    items: 5,
	    itemsCustom: [
	        [0, 1],
	        [450, 1],
	        [600, 1],
	        [700, 2],
	        [1000, 3],
	        [1200, 3],
	        [1400, 3],
	        [1600, 3]
	    ]
	})
	.find('a').on('click', function (e) {
	    e.stopPropagation();
	});
	$("#owl-endorsments").owlCarousel({
	    autoPlay: 5000,
	    items: 5,
	    itemsCustom: [
	        [0, 1],
	        [450, 1],
	        [600, 1],
	        [700, 2],
	        [1000, 2],
	        [1200, 2],
	        [1400, 2],
	        [1600, 2]
	    ]
	})
	.find('a').on('click', function (e) {
	    e.stopPropagation();
	});
})(jQuery);
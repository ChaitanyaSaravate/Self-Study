$(function () {

	var onUserSearchTextSubmitted = function () {
		var $form = $(this);

		var options = {
			url: $form.attr("action"),
			type: $form.attr("method"),
			data: $form.serialize()
		};

		$.ajax(options).done(function (data) {
			var $target = $($form.attr("data-myOwnAttribute-target"));
			var $newHtml = $(data);
			$target.replaceWith($newHtml);
			$newHtml.effect("highlight");
		});

		return false; // Stops page navigation which is default behavior
	};

	var submitOnTextSelection = function(event, ui) {
		var $input = $(this);
		$input.val(ui.item.label);

		var $form = $input.parents("form:first");
		$form.submit();
	};

	var handleAutoComplete = function() {
		var $input = $(this);
		var options = {
			source: $input.attr("data-myOwnAttribute-autocomplete"),
			select: submitOnTextSelection
		};

		$input.autocomplete(options);
	};


	$("form[data-myOwnAttribute-ajax='true']").submit(onUserSearchTextSubmitted);
	$("input[data-myOwnAttribute-autocomplete]").each(handleAutoComplete);
});
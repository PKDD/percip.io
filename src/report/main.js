$('document').ready(function(){

	$('table').find('tr').filter(':even').addClass('striped');
	$('table').filterTable({
    callback: function(term, table) {
        table.find('tr').removeClass('striped').filter(':visible:even').addClass('striped');
    }
});
})

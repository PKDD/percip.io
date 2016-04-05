$('document').ready(function(){

	$('table').find('tr').filter(':even').addClass('striped');
	var today = new Date();
var dd = today.getDate();
var mm = today.getMonth()+1; //January is 0!
var yyyy = today.getFullYear();

if(dd<10) {
    dd='0'+dd
}

if(mm<10) {
    mm='0'+mm
}

today = dd+'.'+mm+'.'+yyyy;
	$('table').filterTable({
		quickList:[today, 'In', 'Out'],
    callback: function(term, table) {
        table.find('tr').removeClass('striped').filter(':visible:even').addClass('striped');
    }
});
})

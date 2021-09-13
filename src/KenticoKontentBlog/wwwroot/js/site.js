$(document).ready(function () {
    formatRichTextTables();
});

function formatRichTextTables() {
    $('div.js-content-container > table').addClass('table table-striped');

    $('<thead></thead>').prependTo('div.js-content-container > table');
    $('<tr></tr>').prependTo('div.js-content-container > table thead').append(
        $('td', 'div.js-content-container > table tbody tr:first').map(
            function (i, e) {
                return $('<th scope="col">').html(e.textContent).get(0);
            }
        )
    );

    $('div.js-content-container > table tbody tr:first').remove();
}
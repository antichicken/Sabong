var charData = ["banker", "banker", "banker", "banker", "banker", "banker", "banker", "banker", "banker", "banker", "banker", "banker", "banker", "banker", "banker","draw", "draw",
    "player", "player", "player","draw", "player", "player",
    "banker", "banker", "banker", "banker", "banker", "banker",
    "player", "player", "player", "player", "player", "player", "player", "player", "player", "player",
    "banker",
    "player", "player",
    "banker", "banker", "banker",
    "banker", "banker", "banker",
    "player", "draw", "draw" ];

var bacarattable = '';
var rightchart = '';
$(document).ready(function() {
    
    DrawChart(charData);
    DrawNormalChart(charData);
    $('.bk-content-wrap').scrollLeft(4000);
});

function ResetTable() {
    $('#bacarat').replaceWith(bacarattable);
    $('#rightChart').replaceWith(rightchart);
}

function DrawChart(data) {
    var maxRow = 4;
    var chartRoot = '#bacarat';
    var currentCol = -1;
    var currentRow = -1;
    var jump = 0;

    bacarattable =$(chartRoot)[0].outerHTML;

    for (var i = 0; i < data.length; i++) {
        var current = data[i];
        
        if (current == "banker") {
            if (currentCol == -1 && currentRow == -1) {
                currentCol++;
                currentRow++;
                $('#'+currentCol + '_' + currentRow).addClass("banker");
            } else {
                NextPosition(current);
            }
        } else if (current == "player") {
            if (currentCol == -1 && currentRow == -1) {
                currentCol++;
                currentRow++;
                $('#' + currentCol + '_' + currentRow).addClass("player");
            } else {
                NextPosition(current);
            }
        } else {
            NextPosition(current);
        }
    }
    
    function ColumnCount() {
        var row = $(chartRoot).find('tr').first();
        return row.find('td').length;
    }

    function AddOneCollumn() {
        var rows = $(chartRoot).find('tr');
        for (var i = 0; i < rows.length; i++) {
            var colCount = $(rows[i]).find('td').length;
            var id = colCount + '_' + i;
            $(rows[i]).append('<td id="' + id + '"></td>');
        }
        $(chartRoot).css('width', $(chartRoot).width() + 32);
    }

    function GetCellInfo(cellId) {
        var tmp = cellId.split('_');
        return { col: parseInt(tmp[0]), row: parseInt(tmp[1]) };
    }

    function CanGoAhead(curentCell) {
        if (curentCell.row >= maxRow) {
            return false;
        } else {
            var nextCell = $('#' + curentCell.col + '_' + (curentCell.row + 1));
            if (nextCell.hasClass('banker') || nextCell.hasClass('player')) {
                return false;
            }
        }
        return true;
    }

    function NextPosition(winner) {
        var currentCell = $('#' + currentCol + '_' + currentRow);
        if (winner == "draw") {
            var drawCount = currentCell.attr('data-dcount');
            if (drawCount) {
                var x = parseInt(drawCount) + 1;
                currentCell.attr('data-dcount', x);
                currentCell.html('<span class="draw-sign">' + x + '</span>');
            } else {
                currentCell.attr('data-dcount', 1);
                currentCell.html('<span class="draw-sign">' + 1 + '</span>');
            }
            if (!currentCell.hasClass('draw')) {
                currentCell.addClass('draw');
            }
        } else {
            if (currentCell.hasClass(winner)) {
                if (CanGoAhead({ row: currentRow, col: currentCol }) && currentCol <= jump) {
                    currentRow++;
                } else {
                    currentCol++;
                }

            } else { // jump to new column
                while (true) {
                    jump++;
                    currentRow = 0;
                    currentCol = jump;
                    nextcell = $('#' + currentCol + '_' + currentRow);
                    if (!nextcell.hasClass("player") && !nextcell.hasClass("banker")) {
                        break;
                    }
                }

            }
            if (currentCol >= ColumnCount()) {
                AddOneCollumn();
            }
            var nextcell = $('#' + currentCol + '_' + currentRow);
            nextcell.addClass(winner);
        }

    }
}



function DrawNormalChart(data) {
    var chartRoot = '#rightChart';
    var maxRow = 4;
    var currentCol = -1;
    var currentRow = -1;
    rightchart = $(chartRoot)[0].outerHTML;
    
    for (var i = 0; i < data.length; i++) {
        if (currentCol<0 && currentRow<0) {
            currentCol = 0;
            currentRow = 0;
        } else {
            if (currentRow >= maxRow) {
                currentCol++;
                currentRow = 0;

            } else {
                currentRow++;
            }
        }
        
        if (currentCol >= ColumnCount()) {
            AddOneCollumn();
        }
        $('#r_' + currentCol + '_' + currentRow).addClass('r_' + data[i]);
    }
    
    function ColumnCount() {
        var row = $(chartRoot).find('tr').first();
        return row.find('td').length;
    }

    function AddOneCollumn() {
        var rows = $(chartRoot).find('tr');
        for (var i = 0; i < rows.length; i++) {
            var colCount = $(rows[i]).find('td').length;
            var id = colCount + '_' + i;
            $(rows[i]).append('<td id="r_' + id + '"></td>');
        }
        $(chartRoot).css('width', $(chartRoot).width() + 32);
    }
}
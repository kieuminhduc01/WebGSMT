
var tableDevices = function () {
    var table = $('#loadTableData');
    // begin first table
    table.DataTable({
        responsive: true,
        searchDelay: 500,
        processing: true,
        serverSide: true,
        info: false,
        language: {
            "processing": "Đang xử lý..."
        },
        ajax: {
            url: "/Users/Home/loadtabledata",
            type: 'GET',
            data: {
                DeviceName: function () { return DEVICENAME }
            }
        },
        columns: [
            { data: 'tagName', name: "Thẻ tên" },
            { data: 'deviceName', name: "Tên thiết bị" },
            {
                data: 'value', name: "Giá trị"
            },
            {
                data: 'time',
                name: "Thời gian",
                render: function (data, type, full, meta) {
                    var d = new Date(data),
                        month = '' + (d.getMonth() + 1),
                        day = '' + d.getDate(),
                        year = '' + d.getFullYear(),
                        hour = '' + d.getHours(),
                        min = '' + d.getMinutes(),
                        second = '' + d.getSeconds();

                    if (month.length < 2) month = '0' + month;
                    if (day.length < 2) day = '0' + day;
                    if (hour.length < 2) hour = '0' + hour;
                    if (min.length < 2) min = '0' + min;
                    if (second.length < 2) second = '0' + second;
                    return [day, month, year].join('/') + '  ' + [hour, min, second].join(':');
                }
            },
            {
                data: 'connected',
                name: "Trạng thái",
                render: function (data, type, full, meta) {
                    if (data == true) {
                        return "Có kết nối";
                    }
                    else {
                        return "Mất kết nối";
                    }

                }
            },
        ],
    });
};

jQuery(document).ready(function () {

    tableDevices();
    setInterval(function () {
        $('#loadTableData').DataTable().ajax.reload(null, false);
    }, 20000);
});
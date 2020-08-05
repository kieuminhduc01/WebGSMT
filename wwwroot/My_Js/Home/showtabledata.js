 
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
                { data: 'value', name: "Giá trị" },
                { data: 'time', name:"Thời gian"},
                { data: 'connected', name:"Trạng thái"},
            ],
        });
    };

jQuery(document).ready(function () {
    tableDevices();
});
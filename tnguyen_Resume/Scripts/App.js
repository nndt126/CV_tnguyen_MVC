var myApp = angular.module('myApp', ['angularUtils.directives.dirPagination']);

myApp.controller('projectController', function ($scope, $http) {
    getDataProject();

    //******=========Get All Project=========******
    function getDataProject() {
        $http.get('/Project/GetProjects').success(function (result) {
            $scope.Projects = result;
        })
         .error(function (data) {
             console.log("loi roi");
         });
    };
    $scope.getData = function () {
        getDataProject();
    };

    //******=========Get Project=========******
    $scope.getProjectByID = function (data) {
        ShowRow();
        $http.get('/Project/GetProjectsByID/' + data.ID)
        .success(function (data) {
            //debugger;
            $scope.sID = data.ID; $scope.sProjectTitle = data.ProjectTitle;
            $scope.sProjectInfo = data.ProjectInfo; $scope.sProjectDetail = data.ProjectDetail;
            $scope.sProjectJob = data.ProjectJob; $scope.sProjectURL = data.ProjectURL;
            $scope.sProjectTime = data.ProjectTime; $scope.sProjectImage = data.ProjectImage;
        })
        .error(function (data) {
            console.log("loi roi");
        });
    };

    //******=========Save Project=========******
    $scope.editProject = function () {
        if ($scope.sID != null) {
            sID = $scope.sID;
        }
        else
            sID = 0;
        var date = $('#datetimepicker1').val();
        $scope.sProjectTime = date;

        if ($scope.sProjectImage != null) {
            var sPath = "/Content/images/portfolio/";
            SaveImage(sPath);
            $scope.sProjectImage = $("#txtImg").val();
        }
        $http.post('/Project/EditProject/', { sID, sProjectTitle: $scope.sProjectTitle, sProjectInfo: $scope.sProjectInfo, sProjectImage: $scope.sProjectImage, sProjectDetail: $scope.sProjectDetail, sProjectJob: $scope.sProjectJob, sProjectURL: $scope.sProjectURL, sProjectTime: $scope.sProjectTime})
        .success(function (result) {
            $scope.Projects = result;
            $scope.sID = null;
            $scope.getData();
        })
        .error(function (data) {
            console.log("loi roi");
        });
    };

});
myApp.controller('informationController', function ($scope, $http) {

    getDataInfor();

    //******=========Get All Infor=========******
    function getDataInfor() {
        $http.get('/Admin/GetInfors').success(function (result) {
            $scope.Infors = result;
        })
         .error(function (data) {
             console.log("loi roi");
         });
    };
    $scope.getData = function () {
        getDataInfor();
    };

    //******=========Get Information=========******
    $scope.getInformationByID = function (data) {
        ShowRow();
        $http.get('/Admin/GetInforByID/' + data.ID)
        .success(function (data) {
            //debugger;
            $scope.sID = data.ID;
            $scope.sFullName = data.FullName; $scope.sName = data.Name;
            $scope.sPhone = data.Phone; $scope.sAddress = data.Address;
            $scope.sCity = data.City; $scope.sEmail = data.Email;
            $scope.sImage = data.Image; $scope.sAbout = data.About;
        })
        .error(function (data) {
            console.log("loi roi");
        });
    };

    //******=========Save Information=========******
    $scope.editInfor = function () {
        if ($scope.sID != null) {
            sID = $scope.sID;
        }
        if ($scope.sImage != null)
        {
            var sPath = "/Content/images/profile/";
            var sPathResize = "/Content/images/profile/modals";
            SaveImage(sPath, sPathResize);
            $scope.sImage = $("#txtImg").val();
        }
        $http.post('/Admin/EditInfor/', { sID, sFullName: $scope.sFullName, sName: $scope.sName, sImage : $scope.sImage, sPhone: $scope.sPhone, sAddress: $scope.sAddress, sCity: $scope.sCity, sEmail: $scope.sEmail, sAbout: $scope.sAbout})
        .success(function (result) {
            $scope.Infors = result;
            $scope.sID = null;
            $scope.getData();
        })
        .error(function (data) {
            console.log("loi roi");
        });
    };

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    };
});

myApp.controller('workController', function ($scope, $http) {
    getDataWork();

    //******=========Get All Works=========******
    function getDataWork() {
        $http.get('/Admin/GetWorks').success(function (result) {
            $scope.Works = result;
            $scope.Date = result.WorksDate;
            //$scope.Date = formatJSONDate(Date(result.WorksDate));
            //alert('Date2 = ' + $scope.Date);
        })
         .error(function (data) {
             console.log("loi roi");
         });
    };
    $scope.getData = function () {
        getDataWork();
        $scope.sWorksTitle = ""; $scope.sWorksInfo = ""; $scope.sWorksDetail = ""; $scope.sWorkDate = "";
    };

    //******=========Get Single Work=========******
    $scope.getWorkByID = function (data) {
        ShowRow();
        $http.get('/Admin/GetWorkById/' + data.ID)
        .success(function (data) {
            //debugger;
            $scope.sID = data.ID;
            $scope.sWorksTitle = data.WorksTitle; $scope.sWorksInfo = data.WorksInfo;
            $scope.sWorksDetail = data.WorksDetail; $scope.sWorkDate = data.WorksDate;
            
            //getallData();
        })
        .error(function (data) {
            console.log("loi roi");
        });
    };
    
    //******=========Save Customer=========******
    $scope.addWork = function () {
        if ($scope.sID != null) {
            sID = $scope.sID;
        }
        else
            sID = 0;
        var date = $('#datetimepicker1').val();
        $scope.sWorkDate = date;
        
        $http.post('/Admin/AddWork/', { sID, sWorksTitle: $scope.sWorksTitle, sWorksInfo: $scope.sWorksInfo, sWorksDetail: $scope.sWorksDetail, sWorkDate: $scope.sWorkDate })
        .success(function (result) {
            $scope.Works = result;
            $scope.sID = null;
            $scope.getData();
        })
        .error(function (data) {
            console.log("loi roi");
        });
    };

    //******=========Delete Customer=========******
    $scope.deleteWork = function (data) {
        var IsConf = confirm('You are about to delete ' + data.WorksTitle + '. Are you sure?');
        if (IsConf) {
            $http.post('/Admin/DeleteWork/', { delWork: data })
            .success(function (result) {
                $scope.Works = result;
                getallData();
            })
            .error(function (data) {
                console.log("loi roi");
            });
        }
    };

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    };

});

function formatJSONDate(jsonDate) {
    var date = new Date(jsonDate).toDateString();
    return date;
}

function ShowRow() {
    var result_style = document.getElementById('result_tr').style;
    result_style.display = '';
}

function HiddenRow() {
    var result_style = document.getElementById('result_tr').style;
    result_style.display = 'none';
}

function SaveImage(sPath, sPathResize) {
    var data = new FormData();
    var files = $("#UploadImg").get(0).files;

    if (files.length > 0) {
        data.append("MyImages", files[0]);
    }
    if (sPath == undefined || sPathResize == undefined) {
        sPath = "/Content/images/profile/";
        sPathResize = null;
    }
    data.append("sPath", sPath);
    if (sPathResize != null)
        data.append("sPathResize", sPathResize);
    
    $.ajax({
        url: "/Admin/UploadFile",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        // Tắt đồng bộ hóa để nó upload trước rồi mới qua hành động #
        async: false,
        success: function (response) {
            //code after success
            $("#txtImg").val(response);
            $("#imgPreview").attr('src', sPath + response);
            
        },
        error: function (er) {
            alert(er);
        }
    });
}
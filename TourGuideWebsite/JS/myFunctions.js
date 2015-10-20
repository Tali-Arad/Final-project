function sortByTourField(field) {
    console.log("sortByTourField");
    console.log(field);
    var Data = {
        Field: field
    }

    Type = "POST";
    Url = "http://localhost:53887/TourGuideSvc/TourGuideSvc.svc/SortToursByTourField"; // Do not hard code the URL! change this!
    ContentType = "application/json; charset=utf-8";
    DataType = "json"; varProcessData = true;

    $.ajax({
        type: Type,                   //GET or POST or PUT or DELETE verb
        url: Url,                     // Location of the service
        data: JSON.stringify(Data),             //Data sent to server
        contentType: ContentType,               // content type sent to server
        dataType: DataType,                     //Expected data format from server
        processdata: varProcessData,            //True or False
        success: function (msg) {               //On Successfull service call
            sortByTourFieldSucceeded(msg);
        },
        error: ServiceFailed// When Service call fails
    });
}

function ServiceFailed(result) {
    alert('Service call failed: ' + result.status + ' ' + result.statusText);
    Type = null;
    varUrl = null;
    Data = null;
    ContentType = null;
    DataType = null;
    ProcessData = null;
}
function sortByTourFieldSucceeded(result) {
    objUL = document.getElementById("sortingBy");
    objUL.innerHTML = "";
    if (objUL != null) {

        for (i = 0; i < result.length; i++) {
            var date = result[i].TourDate.split("(");
            date = date[1].split("+");
            date = date[0];;
            date = new Date(parseInt(date));
            var tourId = result[i].TourID;
            tourDate = result[i].TourDate;
            var tourName = result[i].TourName;
            var cSharpDate = new Date(result[i]);
            var objli = document.createElement("li");
            objli.className = "list-group-item";
            objli.innerHTML = '<a href="../Home/EventDetails/' + result[i].TourID + '?date=' + date.toISOString() + '" class="img-rounded">\
                              <div class="row">\
                              <div class="col-md-4">\
                        <img class="img-responsive img-rounded img-hover" style="width:200px;height:160px" src="../Tours/GetImage/?tourid=' + result[i].TourID + '" alt="">\
                    </div>\
                    <div class="col-md-8">\
                        <h2>'+ date + '</h2>\
                        <h3 style="color:coral">'+ tourName + '</h3>\
                        <p><a href="../Home/EventDetails/' + result[i].TourID + '?date=' + date.toISOString() + '"">Details and Registration</a></p>\
                    </div>\
                </div>\
           </a>'
            objUL.appendChild(objli);
        }

        //for (i = 0; i < result.length; i++) {
        //    var objli = document.createElement("li");
        //    console.log(result[i].TourDate);
        //    var date = result[i].TourDate.split("(");
        //    date = date[1].split("+");
        //    date = date[0];
        //    console.log(date);
        //    date = new Date(parseInt(date));
        //    console.log(date);
        //    var tourId = result[i].TourID;
        //    var tourDate = result[i].TourDate;

        //    //    var cSharpDate = "" + year + "" + month + "" + day + "" + hours + "" + minutes + "00";
        //    //  console.log(cSharpDate);
        //    var cSharpDate = new Date(result[i]);
        //    console.log(cSharpDate);
        //    // Decide about the date format. Make it consistent.
        //    //  objli.innerHTML = "<a href='../Home/EventDetails'>" + result[i].TourName + " " + date + "</a>";
        //    objli.innerHTML = "<a href='../Home/EventDetails/" + result[i].TourID + "?date=" + date.toISOString() + "')>" + result[i].TourName + " " + date + "</a>";
        //    objUL.appendChild(objli);
        //}
    }
}

function sortByEventField(field) {
    console.log("sortByEventField");
    console.log(field);
    var Data = {
        Field: field
    }

    Type = "POST";
    Url = "http://localhost:53887/TourGuideSvc/TourGuideSvc.svc/SortToursByEventField"; // Do not hard code the URL! change this!
    ContentType = "application/json; charset=utf-8";
    DataType = "json"; varProcessData = true;

    $.ajax({
        type: Type,                   //GET or POST or PUT or DELETE verb
        url: Url,                     // Location of the service
        data: JSON.stringify(Data),             //Data sent to server
        contentType: ContentType,               // content type sent to server
        dataType: DataType,                     //Expected data format from server
        processdata: varProcessData,            //True or False
        success: function (msg) {               //On Successfull service call
            sortByEventFieldSucceeded(msg);
        },
        error: ServiceFailed// When Service call fails
    });
}

function ServiceFailed(result) {
    alert('Service call failed: ' + result.status + ' ' + result.statusText);
    Type = null;
    varUrl = null;
    Data = null;
    ContentType = null;
    DataType = null;
    ProcessData = null;
}
function sortByEventFieldSucceeded(result) {
    objUL = document.getElementById("sortingBy");
    objUL.innerHTML = "";
    if (objUL != null) {
        //for (i = 0; i < result.length; i++) {
        //    var objli = document.createElement("li");
        //    console.log(result[i].TourDate);
        //    var date = result[i].TourDate.split("(");
        //    date = date[1].split("+");
        //    date = date[0];
        //    console.log(date);
        //    date = new Date(parseInt(date));
        //    console.log(date);
        //    var tourId = result[i].TourID;
        //    var tourDate = result[i].TourDate;

        //    //    var cSharpDate = "" + year + "" + month + "" + day + "" + hours + "" + minutes + "00";
        //    //  console.log(cSharpDate);
        //    var cSharpDate = new Date(result[i]);
        //    console.log(cSharpDate);
        //    // Decide about the date format. Make it consistent.
        //    //  objli.innerHTML = "<a href='../Home/EventDetails'>" + result[i].TourName + " " + date + "</a>";
        //    objli.innerHTML = "<a href='../Home/EventDetails/" + result[i].TourID + "?date=" + date.toISOString() + "')>" + result[i].TourName + " " + date + "</a>";
        
        for (i = 0; i < result.length; i++) {
            var date = result[i].TourDate.split("(");
            date = date[1].split("+");
            date = date[0];;
            date = new Date(parseInt(date));
            var tourId = result[i].TourID;
            tourDate = result[i].TourDate;
            var tourName = result[i].TourName;
            var cSharpDate = new Date(result[i]);
            var objli = document.createElement("li");
            objli.className = "list-group-item";
            objli.innerHTML = '<a href="../Home/EventDetails/' + result[i].TourID + '?date=' + date.toISOString() + '" class="img-rounded">\
                              <div class="row">\
                              <div class="col-md-4">\
                        <img class="img-responsive img-rounded img-hover" style="width:200px;height:160px" src="../Tours/GetImage/?tourid=' + result[i].TourID + '" alt="">\
                    </div>\
                    <div class="col-md-8">\
                        <h2>'+date+'</h2>\
                        <h3 style="color:coral">'+ tourName + '</h3>\
                        <p><a href="../Home/EventDetails/' + result[i].TourID + '?date=' + date.toISOString() + '"">Details and Registration</a></p>\
                    </div>\
                </div>\
           </a>'
              objUL.appendChild(objli);
        }
    }

}
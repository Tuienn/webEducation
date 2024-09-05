// Lấy các thẻ cần thiết
var daysTag = document.querySelector(".days"),
    currentDate = document.querySelector(".current-date"),
    prevNextIcon = document.querySelectorAll(".icons span");

// Lấy ngày tháng năm hiện tại
var date = new Date(),
    currYear = date.getFullYear(),
    currMonth = date.getMonth();

// storing full name of all months in array
var months = [
    "Tháng 1",
    "Tháng 2",
    "Tháng 3",
    "Tháng 4",
    "Tháng 5",
    "Tháng 6",
    "Tháng 7",
    "Tháng 8",
    "Tháng 9",
    "Tháng 10",
    "Tháng 11",
    "Tháng 12",
];
function returnFomatDate(date, month, year) {
    var year = year;
    var month = (month + 1).toString().padStart(2, "0");
    var day = date.toString().padStart(2, "0");

    return `${year}-${month}-${day}`;
}
function removeActiveLi() {
    var group_day_selected = document.querySelectorAll(".days li");
    for (let i = 0; i < group_day_selected.length; i++) {
        if (group_day_selected[i].classList.contains("day_miss")) {
            group_day_selected[i].classList.remove("day_miss");
        } else if (group_day_selected[i].classList.contains("day_tomorrow")) {
            group_day_selected[i].classList.remove("day_tomorrow");
        } else if (group_day_selected[i].classList.contains("day_pass")) {
            group_day_selected[i].classList.remove("day_pass");
        }
    }
}
function renderCalendar() {
    // Lấy thứ đầu tiên của tháng với 0=T2, 1=T3, 2=T4...
    let firstDayofMonth = new Date(currYear, currMonth, 1).getDay() - 1;
    // Lấy ngày cuối cùng của tháng (28/29/30/31)
    let lastDateofMonth = new Date(currYear, currMonth + 1, 0).getDate();
    // Lấy thứ cuối cùng của tháng
    let lastDayofMonth = new Date(
        currYear,
        currMonth,
        lastDateofMonth
    ).getDay();
    // Lấy ngày cuối cùng của tháng
    let lastDateofLastMonth = new Date(currYear, currMonth, 0).getDate();

    var prevMonth = 0,
        nextMonth = 0,
        prevYear = 0,
        nextYear = 0;
    if (currMonth - 1 == -1) {
        prevMonth = 11;
        prevYear = currYear - 1;
    } else {
        prevMonth = currMonth - 1;
        prevYear = currYear;
    }

    if (currMonth + 1 == 12) {
        nextMonth = 0;
        nextYear = currYear + 1;
    } else {
        nextMonth = currMonth + 1;
        nextYear = currYear;
    }
    let liTag = "";

    // Vòng for chạy từ thứ cuối cùng của tháng trở lại(set cho các con số mờ)
    for (let i = firstDayofMonth; i > 0; i--) {
        // creating li of previous month last days
        liTag += `<li class="day_inactive" title= "${returnFomatDate(
            lastDateofLastMonth - i + 1,
            prevMonth,
            prevYear
        )}">${lastDateofLastMonth - i + 1}</li>`;
    }
    // Vòng lặp for set cho các thứ trong tháng hiện tại
    for (let i = 1; i <= lastDateofMonth; i++) {
        // creating li of all days of current month
        // Ngày hiện tại thì có classNam là active, còn không className là day
        let isToday =
            i === date.getDate() &&
            currMonth === new Date().getMonth() &&
            currYear === new Date().getFullYear()
                ? "day_now"
                : "day_active";
        liTag += `<li class="${isToday}" title="${returnFomatDate(
            i,
            currMonth,
            currYear
        )}">${i}</li>`;
    }
    // Vòng for chạy từ thứ cuối cùng của tháng hiện tại đển thứ của tháng tiếp
    for (let i = lastDayofMonth; i < 6; i++) {
        // creating li of next month first days
        liTag += `<li class="day_inactive" title= "${returnFomatDate(
            i - lastDayofMonth + 1,
            nextMonth,
            nextYear
        )}">${i - lastDayofMonth + 1}</li>`;
    }
    currentDate.innerText = `${months[currMonth]} - ${currYear}`; // passing current mon and yr as currentDate text
    daysTag.innerHTML = liTag;

    removeActiveLi();
    if (arrayAttendance != undefined && arraySchedule != undefined) {
        handleDataCalendarArray();
    }
    handleEventSelectDay();
}
renderCalendar();

prevNextIcon.forEach((icon) => {
    // getting prev and next icons
    icon.addEventListener("click", () => {
        // adding click event on both icons
        // if clicked icon is previous icon then decrement current month by 1 else increment it by 1
        currMonth = icon.id === "prev" ? currMonth - 1 : currMonth + 1;

        if (currMonth <= 0 || currMonth > 11) {
            // if current month is less than 0 or greater than 11
            // creating a new date of current year & month and pass it as date value
            date = new Date(currYear, currMonth, new Date().getDate());
            currYear = date.getFullYear(); // updating current year with new date year
            currMonth = date.getMonth(); // updating current month with new date month
        } else {
            date = new Date(); // pass the current date as date value
        }
        renderCalendar(); // calling renderCalendar function
    });
});

var flag = false;

// Sự kiện chọn ngày
function handleEventSelectDay() {
    var group_day_selected = document.querySelectorAll(".days li");

    var day_selected = document.querySelector(".calendar_day_selected_title");
    var day_work = document.querySelector(
        ".calendar_day_selected_work ul li div"
    );

    function selectDay(calendar_day_selected) {
        day_selected.innerText = calendar_day_selected.title;

        if (calendar_day_selected.classList.contains("day_pass")) {
            day_work.innerText = "Đã diểm danh";
        } else if (calendar_day_selected.classList.contains("day_miss")) {
            day_work.innerText = "Chưa diểm danh";
        } else if (calendar_day_selected.classList.contains("day_tomorrow")) {
            day_work.innerText = "Chưa học";
        } else {
            day_work.innerText = "Nghỉ";
        }
    }

    for (let i = 0; i < group_day_selected.length; i++) {
        group_day_selected[i].addEventListener("click", function () {
            if (flag == false) {
                document
                    .querySelector(".calendar_day:first-child")
                    .setAttribute("style", "display: none");
                document
                    .querySelector(".calendar_day:last-child")
                    .setAttribute("style", "display: flex");

                flag = true;
                // Dùng flag để 2 function trên chỉ chạy 1 lần
            }
            selectDay(group_day_selected[i]);
        });
    }
}

// Xử lý API
var studentSpecialAPI = "https://localhost:7256/api/User/student/infiniteId";

var classFollowChildGET = "https://localhost:7256/api/UserClass/user";
var ScheduleFollowStudentAPI = "https://localhost:7256/api/Schedule";

function handleStudentData(callback1) {
    fetch(studentSpecialAPI)
        .then(function (response) {
            return response.json();
        })
        .then(callback1)
        .catch(function (error) {
            console.log(error);
        });
}
function startStudent() {
    handleStudentData(getStudentNearest);
}

startStudent();

function getStudentNearest(arrayStudent_spe) {
    var array = arrayStudent_spe.map(function (student) {
        return {
            userId: student.userId,
        };
    });
    console.log(array);
    var urlId = array[array.length - 1].userId;
    console.log(urlId);

    //Dòng quan trọng
    getClassFollowChild(urlId, renderClassInfor);
    returnArrayCalendar(urlId, getCalendar, handleDataCalendarArray);
}
function getClassFollowChild(urlID, callback) {
    fetch(classFollowChildGET + "/" + urlID)
        .then(function (response) {
            return response.json();
        })
        .then(callback)
        .catch(function (error) {
            console.log(error);
        });
}

//***Bỏ class ở dataClass.class
var group_infor_class_content = document.querySelectorAll(
    ".infor_class_content"
);
function renderClassInfor(dataClass) {
    var classInfor = {
        urlImg: dataClass[0].urlImg,
        grade: dataClass[0].grade,
        className: dataClass[0].className,
        year: dataClass[0].year,
        // shift: dataClass.scheduleDto.shift,
        // numOfSession: dataClass.scheduleDto.numOfSession
    };

    document.querySelector(".container_first_img img").src = classInfor.urlImg;

    group_infor_class_content[0].innerText = classInfor.grade;
    group_infor_class_content[1].innerText = classInfor.className;
    group_infor_class_content[2].innerText = classInfor.year;
    // group_infor_class_content[3].innerText = classInfor.numOfSession;

    // setTimeSession(classInfor.shift)
}
function setTimeSession(shift) {
    var time;
    if (shift == 1) {
        time = "Sáng 7h -> 9h";
    } else if (shift == 2) {
        time = "Chiều 14h -> 16h";
    } else {
        time = "Tối 18h -> 20h";
    }

    group_infor_class_content[4].innerText = time;
}
//Lấy lịch học và lịch điểm danh
var arraySchedule = [];
var arrayAttendance = [];

function returnArrayCalendar(urlID, callback1, callback2) {
    // console.log(urlID);
    fetch(ScheduleFollowStudentAPI + "/" + urlID + "/dates")
        .then(function (response) {
            // console.log(urlID);
            // console.log(response);
            return response.json();
        })
        .then(callback1)
        .then(callback2)
        .catch(function (error) {
            console.log(error);
        });
}

function getCalendar(arrayCalendar) {
    var array = {
        array1: arrayCalendar.schedule,
        array2: arrayCalendar.attendance,
    };
    arraySchedule = array.array1;
    arrayAttendance = array.array2;
}

function findTagLi_title(title) {
    var group_day_selected = document.querySelectorAll(".days li");
    for (let i = 0; i < group_day_selected.length; i++) {
        if (group_day_selected[i].title == title) {
            return group_day_selected[i];
        }
    }
    return null;
}

function handleDataCalendarArray() {
    // console.log(arraySchedule);
    // console.log(arrayAttendance);
    var lengthAttend = arrayAttendance.length;
    var lengthSchedule = arraySchedule.length;
    var group_day_selected = document.querySelectorAll(".days li");

    for (let i = 0; i < lengthAttend; i++) {
        var tag = findTagLi_title(arraySchedule[i]);

        if (tag != null) {
            // console.log(tag.title, arraySchedule[i]);
            if (arrayAttendance[i] == true) {
                tag.classList.add("day_pass");
            } else {
                tag.classList.add("day_miss");
            }
        }
    }
    for (let i = lengthAttend; i < lengthSchedule; i++) {
        var tag = findTagLi_title(arraySchedule[i]);
        if (tag != null) {
            tag.classList.add("day_tomorrow");
        }
    }
}

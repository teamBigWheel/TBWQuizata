$(document).ready(function () {
    var questionSet = 0;
    var activityItem = null;
    var eventItem = null;
    var consumerItem = null;
    var currentQuestion = 0;
    var previousQuestion = currentQuestion;
    $.get(
        "/Nodes/GetNextNode",
        { currentNodeId: currentQuestion, category: "Activity" },
        function (data) {
            currentQuestion = data.Id;
            AskQuestion($("#SwipeImg"), data.Question, data.ImagePath);
        });
    $("#SwipeImg").on("swiperight", chgImgRight);
    $("#SwipeImg").on("swipeleft", chgImgLeft);
    $("#rightArrow").on("click", chgImgRight);
    $("#rightArrow").mousedown(function(){
        $(this).attr("src", "/Content/Images/button_right_pressed.png");
    });
    $("#rightArrow").mouseup(function () {
        $(this).attr("src", "/Content/Images/button_right.png");
    });
    $("#leftArrow").mousedown(function () {
        $(this).attr("src", "/Content/Images/button_left_pressed.png");
    });
    $("#leftArrow").mouseup(function () {
        $(this).attr("src", "/Content/Images/button_left.png");
    });
    $("#backButton").mousedown(function () {
        $(this).attr("src", "/Content/Images/button_undo_pressed.png");
    });
    $("#backButton").mouseup(function () {
        $(this).attr("src", "/Content/Images/button_undo.png");
    });
    $("#leftArrow").on("click", chgImgLeft);
    $("#backButton").on("click", goBack);

    function chgImgRight(event) {
        var newCategory = false;
        $("#SwipeImg").animate(
                {
                    'margin-left': '100%'
                    // to move it towards the right and, probably, off-screen.
                }, 300,
                function () {
                    $("#SwipeImg").hide();
                    $.get(
                        "/Nodes/GetNextNode",
                        { currentNodeId: currentQuestion, category: "", direction: "Right" },
                        function (data) {
                            GetQuestion(data);
                        }
                    );
                }
        );

        $("#SwipeImg").animate(
                {
                    'margin-left': '0%'
                    // to move it towards the right and, probably, off-screen.
                }, 0,
                function () {
                    $("#SwipeImg").fadeIn(1000);
                }
        );
    }

    function chgImgLeft(event) {
        $("#SwipeImg").animate(
                {
                    'margin-left': '-150%'
                    // to move it towards the right and, probably, off-screen.
                }, 300,
                function () {
                    $("#SwipeImg").hide();
                    $.get(
                        "/Nodes/GetNextNode",
                        { currentNodeId: currentQuestion, category: "", direction: "Left" },
                        function (data) {
                            GetQuestion(data);
                        }
                    );
                }
        );

        $("#SwipeImg").animate(
                {
                    'margin-left': '0%'
                }, 0,
                function () {
                    $("#SwipeImg").fadeIn(1000);
                }
                );
    }

    function GetQuestion(data) {
        currentQuestion = data.Id;
        if (data.ParentId != null) {
            previousQuestion = data.ParentId;
        }

        if (currentQuestion != 1 && currentQuestion != 2 && currentQuestion != 3 && currentQuestion != 4 &&
            currentQuestion != 5 && currentQuestion != 6 && currentQuestion != 7 && currentQuestion != 13 &&
            currentQuestion != 5 && currentQuestion != 6 && currentQuestion != 7 && currentQuestion != 13 &&
            currentQuestion != 11 && currentQuestion != 16 && currentQuestion != 19 && currentQuestion != 20) {
            currentQuestion = 0;
            var category = "";
            if (activityItem == null) {
                category = "Event";
                activityItem = "Activity";
            }
            else if (eventItem == null) {
                category = "Consumer";
                eventItem = "Event";
            }
            else if (consumerItem == null) {
                consumerItem = "Consumer";
                window.location.replace("/Home/Results?activityItem=" + activityItem + "&eventItem=" + eventItem + "&consumerItem=" + consumerItem);
            }
            $.get(
                "/Nodes/GetNextNode",
                { currentNodeId: currentQuestion, category: category },
                function (data) {
                    currentQuestion = data.Id;
                    AskQuestion($("#SwipeImg"), data.Question, data.ImagePath);
                });
        }
        else {
            AskQuestion($("#SwipeImg"), data.Question, data.ImagePath);
        }
    }

    function AskQuestion(element, question, imgName) {
        element.attr("src", "/Content/Images/" + imgName);
        $("#Question").html(question);
        
    }

    function goBack() {
        currentQuestion = previousQuestion;
        $.get(
                "/Nodes/GetPreviousNode",
                { previousNodeId: currentQuestion },
                function (data) {
                    if (data.ParentId != null) {
                        previousQuestion = data.ParentId;
                    }
                    AskQuestion($("#SwipeImg"), data.Question, data.ImagePath);
                });
    }

    function pickRandom(range) {
        if (Math.random)
            return Math.round(Math.random() * (range - 1));
        else {
            var now = new Date();
            return (now.getTime() / 1000) % range;
        }
    }
});



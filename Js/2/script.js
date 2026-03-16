
$(function () {
  $(".menu-title").on("click", function () {
    $(".menu-list").slideToggle(200);
  });

  $(".zoom-image").hover(
    function () {
      $(this).css("transform", "scale(1.3)");
    },
    function () {
      $(this).css("transform", "scale(1)");
    }
  );

  $(".feedback-form").on("submit", function (event) {
    event.preventDefault();
    const message = $("#message").val().trim();
    if (message.length === 0) {
      $("#feedbackResult").text("Пожалуйста, введите сообщение.");
      return;
    }
    $("#feedbackResult").text(
      'Хорошо, ваше сообщение: "' + message + '" отправлено'
    );
  });
});


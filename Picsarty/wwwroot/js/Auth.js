console.log("done!")
function Login() {
    var UserName_l = $("#UserName").val();
    var Password_a = $("#Password").val();
    $.ajax({
        url: '/R_User/Home/Login',
        type: 'Post',
        dataType: 'json',
        data: {
            UserName: UserName_l,
            Password: Password_a
        }
    })
}
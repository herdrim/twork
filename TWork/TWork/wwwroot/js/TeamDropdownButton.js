$(document).ready(function () {
    var url = "MyTeam/MyTeams";
    $.get(url).done(function (dataGet) {
        if (dataGet !== null) {
            var myTeamsArray = JSON.parse(dataGet);
            for (var i = 0; i < myTeamsArray.length; i++) {
                $("#dropdownMyTeams").append('<a class="dropdown-item" href="MyTeam/Details?teamId=' + myTeamsArray[i].TeamId + '">' + myTeamsArray[i].TeamName + '</a>');
            }
        }
    });
});
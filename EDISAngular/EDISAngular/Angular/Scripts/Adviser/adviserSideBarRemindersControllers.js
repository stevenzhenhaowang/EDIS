app.controller("adviserSideBarRemindersController", function ($scope, $http) {
    $scope.reminders = [
        {name: 'Clients Having Birthday', value: 55},
        {name: 'Clients Turning 55 NextMonth', value: 55},
        {name: 'Clients Entering Retirement', value: 55},
        {name: 'Portfolio Reviews', value: 55},
        {name: 'Policies Due Today', value: 55 },
        {name: 'Term Deposits Maturity Due', value: 55 },
        {name: 'IPOs Due Next 14 Days', value: 55 },
    ]
});
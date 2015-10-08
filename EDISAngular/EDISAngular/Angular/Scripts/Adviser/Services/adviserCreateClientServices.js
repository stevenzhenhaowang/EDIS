angular.module("EDIS")
.factory("adviserCreateClientService", function ($http, $resource, $filter, $q, AppStrings)
{


    app.factory("getAllExistingGroup", function ($http, $resource, $filter, AppStrings) {
        return function () {
            $resource(AppStrings.EDIS_IP + "api/Personclient/GetAllGlientGroup");
        }
    })


    //var existingGroups =
    //    function () {
    //    return $resource(AppStrings.EDIS_IP + "api/Personclient/GetAllGlientGroup");
    //    console.log("aaa");
    //};


    //var existingGroups = {
    //    getData: function () {
    //        return $resource(AppStrings.EDIS_IP + "api/Personclient/GetAllGlientGroup");
    //    }
    //};
    var existingGroups = [
        {
            id: "00001",
            name: "Mr. X and Mrs. Y",
            accountNumber: "00001",
            numberOfLinks: 5,
            dateCreated: new Date(),
        }, {
            id: "00002",
            name: "Mr. A and Mrs. B",
            accountNumber: "00002",
            numberOfLinks: 8,
            dateCreated: new Date(),
        }, {
            id: "00003",
            name: "Mr. C and Mrs. D",
            accountNumber: "00003",
            numberOfLinks: 3,
            dateCreated: new Date(),
        }, {
            id: "00004",
            name: "Mr. E and Mrs. F",
            accountNumber: "00004",
            numberOfLinks: 9,
            dateCreated: new Date(),
        },
    ];

    var entityTypes = [
        {
            name: "Company",
            id:"001"
        }, {
            name: "Sole Trader",
            id:"002"
        }
    ]

    var riskProfileQuestions = [
        {
            question: "Short Term Goals (Less than 2 Years):",
        }, {
            question: "Medium Term Goals (2 to 5 Years):",
        }, {
            question: "Long Term Goals (Greater than 5 Years):",
        }, {
            question: "Additional Commentary:",
        }, {
            question: "At What age would you like to retire?:",
        }, {
            question: "How much income would you need in retirement per annum?:",
        }, {
            question: "Source of Income when in retirement?:",
        }, {
            question: "Your Investment Objective for this portfolio:",
        }, {
            question: "Primary Investment Objective:",
        }, {
            question: "Secondary Investment Objective:",
        }, {
            question: "Third Investment Objective:",
        }, {
            question: "What is your desired investment time horizon:",
        }, {
            question: "Describe your investment knowledge:",
        }, {
            question: "Describe your attitude to risk:",
        }, {
            question: "Describe your attitude to capital loss:",
        }, {
            question: "Will you be investing for Short Term Trading (Yes/No):",
        }, {
            question: " % of total Asset for Short Term Trading:",
        }, {
            question: "% of total Domestic Equity for Short Term Trading:",
        }, {
            question: "Are there any particular asset classes you wish to include in the portfolio?",
        }, {
            question: "Are there any specific Domestic Equity stocks you wish to include?",
        }, {
            question: "Are there any sectors you wish to include in the portfolio?",
        }, {
            question: "Are there any particular Managed Investment vehicle you wish to include?",
        }, {
            question: "Are there any additional comments you wish to add?",
        }, {
            question: "Are there any particular asset classes you wish to avoid in the portfolio?",
        }, {
            question: "Are there any specific Domestic Equity stocks you wish to avoid?",
        }, {
            question: "Are there any sectors you wish to avoid in the portfolio?",
        }, {
            question: "Are there any particular Managed Investment vehicle you wish to avoid?",
        }, {
            question: "Are there any additional comments you wish to add?",
        },
    ];
    var riskLevels = [
        {
            value: 1,
            name:"Defensive"
        }, {
            value: 2,
            name: "Conservative"
        }, {
            value: 3,
            name: "Balanced"
        }, {
            value: 4,
            name: "Assertive"
        }, {
            value: 5,
            name: "Aggressive"
        },
    ];

    console.log("aaa");
  //  console.log(existingGroups);

    return {
        
      
        //existingGroups: getExistingGroups,
        addGroup: function (group) {
            group.id = "id",
            group.accountNumber = "number";
            existingGroups.push(group)
        },
        getRiskQuestions: riskProfileQuestions,
        getRiskLevels: riskLevels,
        getCurrentAdviserId: "ADV001",
        getEntityTypes:entityTypes
    }

});
//START OF ML SERVICES
var app = angular.module("EDIS");
app.factory("clientPortfolioMLRatingInfo", function (AppStrings, $resource) {
    return $resource(AppStrings.EDIS_IP + "api/Client/MarginLendingPortfolio/RatingInfo");
});
app.factory("clientPortfolioMLCashflowDetails", function (AppStrings, $resource, $filter) {
    return $resource(AppStrings.EDIS_IP + "api/Client/MarginLendingPortfolio/CashflowDetails");
});
app.factory("clientPortfolioMLStatistics", function (AppStrings, $resource, $filter) {
    return $resource(AppStrings.EDIS_IP + "api/Client/MarginLendingPortfolio/Stats");
})
app.factory("clientPortfolioMLLoanInfo", function (AppStrings, $resource) {
    return $resource(AppStrings.EDIS_IP + "api/Adviser/MarginLendingPortfolio/AccountLoanInfo");
})
app.factory("clientPortfolioMLCompanyProfile", function (AppStrings, $resource) {
    return $resource(AppStrings.EDIS_IP + "api/Client/MarginLendingPortfolio/allCompanies");
});
app.factory("clientPortfolioMLLoanDetails", function (AppStrings, $resource) {
    return $resource(AppStrings.EDIS_IP + "api/Adviser/MarginLendingPortfolio/AccountLoanCompanyDetails");
})
app.factory("clientPortfolioMLSuitabilityDetails", function (AppStrings, $resource) {
    return $resource(AppStrings.EDIS_IP + "api/Client/MarginLendingPortfolio/SuitabilityDetails");
})
app.factory("clientPortfolioMLImpactOfMLtoCashFlow", function (AppStrings, $resource) {
    return $resource(AppStrings.EDIS_IP + "api/Client/MarginLendingPortfolio/ImpactToCashflow");
})
app.factory("clientPortfolioMLTypeOfGearingStrategy", function (AppStrings, $resource) {
    return $resource(AppStrings.EDIS_IP + "api/Client/MarginLendingPortfolio/TypeOfGearing");
})
app.factory("clientPortfolioMLLVRDiversification", function (AppStrings, $resource) {
    return $resource(AppStrings.EDIS_IP + "api/Client/MarginLendingPortfolio/Diversification");
})
app.factory("clientPortfolioMLAccountGet", function (AppStrings, $resource) {
    return $resource(AppStrings.EDIS_IP + "api/Client/clientaccounts");
})
//END OF ML SERVICES
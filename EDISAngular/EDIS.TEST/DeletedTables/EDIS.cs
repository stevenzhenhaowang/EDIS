namespace EDIS.Test
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EDIS : DbContext
    {
        public EDIS()
            : base("name=EDIS")
        {
        }


        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Admin_Posts> Admin_Posts { get; set; }
        public virtual DbSet<Adviser_CommissionsAndFees> Adviser_CommissionsAndFees { get; set; }
        public virtual DbSet<Adviser_CV> Adviser_CV { get; set; }
        public virtual DbSet<Adviser_DealerGroupDetails> Adviser_DealerGroupDetails { get; set; }
        public virtual DbSet<Adviser_Description> Adviser_Description { get; set; }
        public virtual DbSet<Adviser_Details> Adviser_Details { get; set; }
        public virtual DbSet<Adviser_Education> Adviser_Education { get; set; }
        public virtual DbSet<Adviser_ManagementDetails> Adviser_ManagementDetails { get; set; }
        public virtual DbSet<Adviser_ProfessionalAssociations> Adviser_ProfessionalAssociations { get; set; }
        public virtual DbSet<Adviser_ProfessionalType> Adviser_ProfessionalType { get; set; }
        public virtual DbSet<Adviser_ProfilePictures> Adviser_ProfilePictures { get; set; }
        public virtual DbSet<Adviser_Ratings> Adviser_Ratings { get; set; }
        public virtual DbSet<Adviser_ServicesProviding> Adviser_ServicesProviding { get; set; }
        public virtual DbSet<Adviser_Subscriptions> Adviser_Subscriptions { get; set; }
        public virtual DbSet<Adviser_TargetClients> Adviser_TargetClients { get; set; }
        public virtual DbSet<Adviser_Verified_Status> Adviser_Verified_Status { get; set; }
        public virtual DbSet<AdviserCapabilityLink> AdviserCapabilityLinks { get; set; }
        public virtual DbSet<AnnualIncomeLevel> AnnualIncomeLevels { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<ApproxClientNumber> ApproxClientNumbers { get; set; }        
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<AttitudeToCapitalLoss> AttitudeToCapitalLosses { get; set; }
        public virtual DbSet<AttitudeToRisk> AttitudeToRisks { get; set; }
        public virtual DbSet<AustEquityCurrentScore> AustEquityCurrentScores { get; set; }
        public virtual DbSet<AustEquityHistoricalScore> AustEquityHistoricalScores { get; set; }
        public virtual DbSet<Beneficiary> Beneficiaries { get; set; }
        public virtual DbSet<Bookmark> Bookmarks { get; set; }
        public virtual DbSet<CapabilityGroup> CapabilityGroups { get; set; }
        public virtual DbSet<CapabilityType> CapabilityTypes { get; set; }
        public virtual DbSet<CashHolding> CashHoldings { get; set; }
        public virtual DbSet<Client_AdvisoryNeeds> Client_AdvisoryNeeds { get; set; }
        public virtual DbSet<Client_Details> Client_Details { get; set; }
        public virtual DbSet<Client_FinancialPosition> Client_FinancialPosition { get; set; }
        public virtual DbSet<Client_InvestmentProfile> Client_InvestmentProfile { get; set; }
        public virtual DbSet<Client_Ratings> Client_Ratings { get; set; }
        public virtual DbSet<ClientFileAction> ClientFileActions { get; set; }
        public virtual DbSet<ClientFileInformation> ClientFileInformations { get; set; }
        public virtual DbSet<ClientGroup> ClientGroups { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<CommissionsAndFee> CommissionsAndFees { get; set; }
        public virtual DbSet<CommunicationMethod> CommunicationMethods { get; set; }
        public virtual DbSet<CommunicationRequest> CommunicationRequests { get; set; }
        public virtual DbSet<ComplianceIncomeExpens> ComplianceIncomeExpenses { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<DailyPrice> DailyPrices { get; set; }
        public virtual DbSet<DealerGroup> DealerGroups { get; set; }
        public virtual DbSet<DeclineCommunicationReason> DeclineCommunicationReasons { get; set; }
        public virtual DbSet<Dependant> Dependants { get; set; }
        public virtual DbSet<DirectProperty> DirectProperties { get; set; }
        public virtual DbSet<DistributionList> DistributionLists { get; set; }
        public virtual DbSet<DividendHistory> DividendHistories { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<EducationRecord> EducationRecords { get; set; }
        public virtual DbSet<EmploymentRecord> EmploymentRecords { get; set; }
        public virtual DbSet<EmploymentStatu> EmploymentStatus { get; set; }
        public virtual DbSet<Exchanx> Exchanges { get; set; }
        public virtual DbSet<Expens> Expenses { get; set; }
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
        public virtual DbSet<FinancialHealthGrade> FinancialHealthGrades { get; set; }
        public virtual DbSet<FixedIncomeHolding> FixedIncomeHoldings { get; set; }
        public virtual DbSet<FundStock> FundStocks { get; set; }
        public virtual DbSet<FundStructure> FundStructures { get; set; }
        public virtual DbSet<GlobalCategory> GlobalCategories { get; set; }
        public virtual DbSet<GlobalCategoryGroup> GlobalCategoryGroups { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<InterEquityCurrentScore> InterEquityCurrentScores { get; set; }
        public virtual DbSet<InterEquityHistoricalScore> InterEquityHistoricalScores { get; set; }
        public virtual DbSet<InvestedProduct> InvestedProducts { get; set; }
        public virtual DbSet<InvestibleAssetsLevel> InvestibleAssetsLevels { get; set; }
        public virtual DbSet<InvestmentKnowledge> InvestmentKnowledges { get; set; }
        public virtual DbSet<InvestmentType> InvestmentTypes { get; set; }
        public virtual DbSet<IPOs> IPOs { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<ManagedFundsCurrentScore> ManagedFundsCurrentScores { get; set; }
        public virtual DbSet<ManagedFundsHistoricalScore> ManagedFundsHistoricalScores { get; set; }
        public virtual DbSet<MarginLoan> MarginLoans { get; set; }
        public virtual DbSet<MorningStarCategory> MorningStarCategories { get; set; }
        public virtual DbSet<MorningStarRating> MorningStarRatings { get; set; }
        public virtual DbSet<NewsletterService> NewsletterServices { get; set; }
        public virtual DbSet<NoteLink> NoteLinks { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<NoteType> NoteTypes { get; set; }
        public virtual DbSet<PictureApprovalStatu> PictureApprovalStatus { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<PolicyDetail> PolicyDetails { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<ProfessionalAssociation> ProfessionalAssociations { get; set; }
        public virtual DbSet<ProfessionType> ProfessionTypes { get; set; }
        public virtual DbSet<Profile_RejectReasons> Profile_RejectReasons { get; set; }
        public virtual DbSet<ProfilePhoto_RejectReasons> ProfilePhoto_RejectReasons { get; set; }
        public virtual DbSet<ProfileProductLink> ProfileProductLinks { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<QualificationType> QualificationTypes { get; set; }
        public virtual DbSet<Ratio> Ratios { get; set; }
        public virtual DbSet<RatioType> RatioTypes { get; set; }
        public virtual DbSet<Referral> Referrals { get; set; }
        public virtual DbSet<ResourcesReference> ResourcesReferences { get; set; }
        public virtual DbSet<RiskProfile> RiskProfiles { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<ServiceLevelAction> ServiceLevelActions { get; set; }
        public virtual DbSet<ServiceLevel> ServiceLevels { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Statement> Statements { get; set; }
        public virtual DbSet<StockDistribution> StockDistributions { get; set; }
        public virtual DbSet<StockHolding> StockHoldings { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockTransaction> StockTransactions { get; set; }
        public virtual DbSet<Strategy> Strategies { get; set; }
        public virtual DbSet<SubscriptionPlanType> SubscriptionPlanTypes { get; set; }
        public virtual DbSet<SubscriptionStatu> SubscriptionStatus { get; set; }
        public virtual DbSet<SubService> SubServices { get; set; }
        public virtual DbSet<TimeFrameForAdvice> TimeFrameForAdvices { get; set; }
        public virtual DbSet<TimeHorizon> TimeHorizons { get; set; }
        public virtual DbSet<TotalAssetsLevel> TotalAssetsLevels { get; set; }
        public virtual DbSet<TrackAdviserView> TrackAdviserViews { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<User_DistributionList> User_DistributionList { get; set; }
        public virtual DbSet<User_DistributionLists> User_DistributionLists { get; set; }
        public virtual DbSet<User_NewsletterServices> User_NewsletterServices { get; set; }
        public virtual DbSet<UserData> UserDatas { get; set; }
        public virtual DbSet<VerifiedStatu> VerifiedStatus { get; set; }
        public virtual DbSet<WebStats_NumberOfLogins> WebStats_NumberOfLogins { get; set; }
        public virtual DbSet<Tally> Tallies { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.AddressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.AddressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Postcode)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Adviser_ServicesProviding>()
                .Property(e => e.SubServiceProvideYesNo)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.AppointmentType)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.DurationHours)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Users)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.Bookmarks)
                .WithRequired(e => e.aspnet_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.EducationRecords)
                .WithRequired(e => e.aspnet_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.EmploymentRecords)
                .WithRequired(e => e.aspnet_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.Qualifications)
                .WithRequired(e => e.aspnet_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetUser>()
                .HasOptional(e => e.UserData)
                .WithRequired(e => e.AspNetUser);

            modelBuilder.Entity<AssetType>()
                .Property(e => e.AssetType1)
                .IsUnicode(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.CashHoldings)
                .WithRequired(e => e.AssetType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.AssetType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.FixedIncomeHoldings)
                .WithRequired(e => e.AssetType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.Loans)
                .WithRequired(e => e.AssetType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.MarginLoans)
                .WithRequired(e => e.AssetType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.DirectProperties)
                .WithRequired(e => e.AssetType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.Statements)
                .WithRequired(e => e.AssetType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.StockHoldings)
                .WithRequired(e => e.AssetType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.AssetType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.AttachmentType)
                .IsUnicode(false);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.EPSGrowth)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.MorningstarRecommendation)
                .IsUnicode(false);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.DIVYield)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.Frank)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.ROA)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.ROE)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.IntCover)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.DebtEquity)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.PE)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityCurrentScore>()
                .Property(e => e.IntrinsicValueVariation)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.EPSGrowth)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.DIVYield)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.Frank)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.ROA)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.ROE)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.IntCover)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.DebtEquity)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.PE)
                .HasPrecision(9, 2);

            modelBuilder.Entity<AustEquityHistoricalScore>()
                .Property(e => e.Beta5Year)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.Relationship)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.Address3)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.PostCode)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Beneficiary>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Bookmark>()
                .Property(e => e.BookmarkName)
                .IsUnicode(false);

            modelBuilder.Entity<Bookmark>()
                .Property(e => e.BookmarkLink)
                .IsUnicode(false);

            modelBuilder.Entity<Bookmark>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<CapabilityGroup>()
                .Property(e => e.CapabilityGroup1)
                .IsUnicode(false);

            modelBuilder.Entity<CapabilityGroup>()
                .HasMany(e => e.CapabilityTypes)
                .WithRequired(e => e.CapabilityGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CapabilityType>()
                .Property(e => e.CapabilityType1)
                .IsUnicode(false);

            modelBuilder.Entity<CapabilityType>()
                .HasMany(e => e.AdviserCapabilityLinks)
                .WithRequired(e => e.CapabilityType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CashHolding>()
                .Property(e => e.InterestRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<CashHolding>()
                .Property(e => e.Frequency)
                .HasPrecision(9, 2);

            modelBuilder.Entity<CashHolding>()
                .Property(e => e.DebitInterest)
                .HasPrecision(9, 2);

            modelBuilder.Entity<CashHolding>()
                .Property(e => e.StatementMethod)
                .IsUnicode(false);

            modelBuilder.Entity<Client_Details>()
                .Property(e => e.ClientGroupId)
                .IsFixedLength();

            modelBuilder.Entity<ClientFileAction>()
                .Property(e => e.ClientFileAction1)
                .IsUnicode(false);

            modelBuilder.Entity<ClientFileInformation>()
                .Property(e => e.Response)
                .IsUnicode(false);

            modelBuilder.Entity<ClientFileInformation>()
                .HasOptional(e => e.ClientFileInformations1)
                .WithRequired(e => e.ClientFileInformation1);

            modelBuilder.Entity<ClientGroup>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<ClientGroup>()
                .Property(e => e.AccountName)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.DesignationAccount)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.CashHoldings)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.ComplianceIncomeExpenses)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Dependants)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.FixedIncomeHoldings)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Loans)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.MarginLoans)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Notes)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Policies)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.DirectProperties)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.RiskProfiles)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.StockDistributions)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.StockHoldings)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.StockTransactions)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.Centrelink)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.ReceivedMaintenance)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.SuperannunationPension)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.OtherTaxableIncome)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.OtherForeignIncome)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.NonTaxableIncome)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.FoodExpenses)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.ClothingExpenses)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.MedicalExpenses)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.UtilitiesBills)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.CommunicationsBills)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.Furniture)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.MortgageRental)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.HomeInsurance)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.VehicleInsurance)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.Repairs)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.CouncilRates)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.VehicleRegistration)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.Petrol)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.VehicleLoans)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.Entertainment)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ComplianceIncomeExpens>()
                .Property(e => e.HolidayTravel)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Currency>()
                .Property(e => e.Currency1)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.DividendHistories)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.FundStocks)
                .WithRequired(e => e.Currency)
                .HasForeignKey(e => e.PortfolioCurrencyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.FundStocks1)
                .WithRequired(e => e.Currency1)
                .HasForeignKey(e => e.FundSizeBaseCurrencyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Currency)
                .HasForeignKey(e => e.BaseCurrencyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailyPrice>()
                .Property(e => e.Price)
                .HasPrecision(9, 4);

            modelBuilder.Entity<DailyPrice>()
                .Property(e => e.High)
                .HasPrecision(9, 4);

            modelBuilder.Entity<DailyPrice>()
                .Property(e => e.Low)
                .HasPrecision(9, 4);

            modelBuilder.Entity<DailyPrice>()
                .Property(e => e.High52WeekPrice)
                .HasPrecision(9, 4);

            modelBuilder.Entity<DailyPrice>()
                .Property(e => e.Low52WeekPrice)
                .HasPrecision(9, 4);

            modelBuilder.Entity<DealerGroup>()
                .Property(e => e.DealerGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<Dependant>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Dependant>()
                .Property(e => e.Relationship)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.Region)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.YearsOnLoan)
                .HasPrecision(9, 2);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.LoanRateType)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.FixedRate)
                .HasPrecision(9, 3);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.FixedLoanPercent)
                .HasPrecision(9, 3);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.InterestRepayment)
                .HasPrecision(9, 3);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.VariableRate)
                .HasPrecision(9, 3);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.AgentName)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.AgentCompany)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.AgentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.AgentPhone)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.AgentFax)
                .IsUnicode(false);

            modelBuilder.Entity<DirectProperty>()
                .Property(e => e.AgentEmail)
                .IsUnicode(false);

            modelBuilder.Entity<DividendHistory>()
                .Property(e => e.Amount)
                .HasPrecision(9, 4);

            modelBuilder.Entity<DividendHistory>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<EducationRecord>()
                .Property(e => e.EducationLevel)
                .IsUnicode(false);

            modelBuilder.Entity<EducationRecord>()
                .Property(e => e.EducationInstitution)
                .IsUnicode(false);

            modelBuilder.Entity<EducationRecord>()
                .Property(e => e.CourseAttended)
                .IsUnicode(false);

            modelBuilder.Entity<EducationRecord>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EducationRecord>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<EmploymentRecord>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<EmploymentRecord>()
                .Property(e => e.EmploymentType)
                .IsUnicode(false);

            modelBuilder.Entity<EmploymentRecord>()
                .Property(e => e.EmployerName)
                .IsUnicode(false);

            modelBuilder.Entity<EmploymentRecord>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<EmploymentRecord>()
                .Property(e => e.HoursPerWeek)
                .HasPrecision(9, 2);

            modelBuilder.Entity<EmploymentRecord>()
                .Property(e => e.SickLeave)
                .HasPrecision(9, 2);

            modelBuilder.Entity<EmploymentRecord>()
                .Property(e => e.AnnualLeave)
                .HasPrecision(9, 2);

            modelBuilder.Entity<EmploymentRecord>()
                .Property(e => e.LongServiceLeave)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Exchanx>()
                .Property(e => e.ExchangeName)
                .IsUnicode(false);

            modelBuilder.Entity<Exchanx>()
                .Property(e => e.ExchangeCountry)
                .IsUnicode(false);

            modelBuilder.Entity<Exchanx>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Exchanx)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Expens>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Expens>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<ExpenseType>()
                .Property(e => e.ExpenseType1)
                .IsUnicode(false);

            modelBuilder.Entity<ExpenseType>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.ExpenseType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FinancialHealthGrade>()
                .Property(e => e.FinancialHealthGrade1)
                .IsUnicode(false);

            modelBuilder.Entity<FinancialHealthGrade>()
                .HasMany(e => e.FundStocks)
                .WithRequired(e => e.FinancialHealthGrade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FixedIncomeHolding>()
                .Property(e => e.IssuerName)
                .IsUnicode(false);

            modelBuilder.Entity<FixedIncomeHolding>()
                .Property(e => e.Ticker)
                .IsUnicode(false);

            modelBuilder.Entity<FixedIncomeHolding>()
                .Property(e => e.AccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<FixedIncomeHolding>()
                .Property(e => e.AccountUserID)
                .IsUnicode(false);

            modelBuilder.Entity<FixedIncomeHolding>()
                .Property(e => e.ClearingSponsor)
                .IsUnicode(false);

            modelBuilder.Entity<FixedIncomeHolding>()
                .Property(e => e.HIN)
                .IsUnicode(false);

            modelBuilder.Entity<FundStock>()
                .Property(e => e.PrimaryProspectusBenchmark)
                .IsUnicode(false);

            modelBuilder.Entity<FundStock>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.FundStock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FundStructure>()
                .Property(e => e.FundStructure1)
                .IsUnicode(false);

            modelBuilder.Entity<FundStructure>()
                .HasMany(e => e.FundStocks)
                .WithRequired(e => e.FundStructure)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GlobalCategory>()
                .Property(e => e.GlobalCategory1)
                .IsUnicode(false);

            modelBuilder.Entity<GlobalCategoryGroup>()
                .Property(e => e.GlobalCategoryGroup1)
                .IsUnicode(false);

            modelBuilder.Entity<GlobalCategoryGroup>()
                .HasMany(e => e.FundStocks)
                .WithRequired(e => e.GlobalCategoryGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Industry>()
                .Property(e => e.Industry1)
                .IsUnicode(false);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.FiveYearTotalReturn)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.MorningstarRecommendation)
                .IsUnicode(false);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.DIVYield)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.ROA)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.ROE)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.FinancialLeverage)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.YearRevenueGrowth)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.DebtEquityRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.CreditRating)
                .IsUnicode(false);

            modelBuilder.Entity<InterEquityCurrentScore>()
                .Property(e => e.ValueVariation)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.OneYearReturn)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.DIVYield)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.ROA)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.ROE)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.QuickRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.CurrentRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.DebtEquityRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.PERatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InterEquityHistoricalScore>()
                .Property(e => e.Beta5Year)
                .HasPrecision(9, 2);

            modelBuilder.Entity<InvestmentType>()
                .Property(e => e.InvestmentType1)
                .IsUnicode(false);

            modelBuilder.Entity<InvestmentType>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.InvestmentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IPOs>()
                .Property(e => e.IssuerName)
                .IsUnicode(false);

            modelBuilder.Entity<IPOs>()
                .Property(e => e.InstrumentCode)
                .IsUnicode(false);

            modelBuilder.Entity<IPOs>()
                .Property(e => e.OfferType)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.LoanType)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.DataFeed)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.InterestRate)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Loan>()
                .Property(e => e.PaymentAmount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Loan>()
                .Property(e => e.AccountBalance)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Loan>()
                .Property(e => e.AccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.LoanProvider)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.FinancialInstitution)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.RelationshipManager)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.AddressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.AddressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.Postcode)
                .IsUnicode(false);

            modelBuilder.Entity<Loan>()
                .Property(e => e.RealEstateAddress)
                .IsUnicode(false);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.OneYearReturn)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.MorningstarRecommendation)
                .IsUnicode(false);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.OneYearAlpha)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.OneYearBeta)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.OneYearInfoRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.OneYearTrackError)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.OneYearSharpRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.MaxManagementExpenseRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.PerformanceFee)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsCurrentScore>()
                .Property(e => e.YearsSinceInception)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.FiveYearReturn)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.FiveYearAlpha)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.FiveYearBeta)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.FiveYearInfoRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.FiveYearStdDev)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.FiveYearSkewRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.FiveYearTrackError)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.FiveYearSharpRatio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ManagedFundsHistoricalScore>()
                .Property(e => e.GlobalCategory)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.HIN)
                .IsUnicode(false);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.LoanProvider)
                .IsUnicode(false);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.VariableRateLoan)
                .IsUnicode(false);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.VariableRateAmount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.FixedRateLoan)
                .IsUnicode(false);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.FixedRateAmount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.UnsettledTransactions)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.TotalBalance)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.NetInterestYTD)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.MonthlyInterest)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.MinBrokerage)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.MaxBrokerage)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.TotalBrokerage)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.TotalGST)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.ManagementFee)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.AdvisersComission)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.LoanLimit)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.SpendingLimit)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .Property(e => e.AvailableCash)
                .HasPrecision(9, 2);

            modelBuilder.Entity<MarginLoan>()
                .HasMany(e => e.Securities)
                .WithRequired(e => e.MarginLoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MorningStarCategory>()
                .Property(e => e.MorningStarCategory1)
                .IsUnicode(false);

            modelBuilder.Entity<MorningStarCategory>()
                .HasMany(e => e.MorningStarRatings)
                .WithRequired(e => e.MorningStarCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MorningStarRating>()
                .Property(e => e.RatingOverall)
                .IsUnicode(false);

            modelBuilder.Entity<MorningStarRating>()
                .Property(e => e.AnalystRating)
                .IsUnicode(false);

            modelBuilder.Entity<MorningStarRating>()
                .Property(e => e.Rating3Yr)
                .IsUnicode(false);

            modelBuilder.Entity<MorningStarRating>()
                .Property(e => e.Rating5Yr)
                .IsUnicode(false);

            modelBuilder.Entity<MorningStarRating>()
                .Property(e => e.Rating10Yr)
                .IsUnicode(false);

            modelBuilder.Entity<MorningStarRating>()
                .Property(e => e.RiskOverall)
                .IsUnicode(false);

            modelBuilder.Entity<MorningStarRating>()
                .Property(e => e.Risk3Yr)
                .IsUnicode(false);

            modelBuilder.Entity<MorningStarRating>()
                .Property(e => e.Risk5Yr)
                .IsUnicode(false);

            modelBuilder.Entity<MorningStarRating>()
                .Property(e => e.Risk10Yr)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.AssetClass)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.ProductClass)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Product)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Purpose)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.NoteSerial)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.FollowupActions)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .HasMany(e => e.NoteLinks)
                .WithRequired(e => e.Note)
                .HasForeignKey(e => e.NoteID2)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Note>()
                .HasMany(e => e.NoteLinks1)
                .WithRequired(e => e.Note1)
                .HasForeignKey(e => e.NoteID1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NoteType>()
                .Property(e => e.NoteType1)
                .IsUnicode(false);

            modelBuilder.Entity<NoteType>()
                .HasMany(e => e.Notes)
                .WithRequired(e => e.NoteType)
                .HasForeignKey(e => e.NoteTypesID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.InsuranceType)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.PolicyType)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.PolicyName)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.PolicyNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.AccountName)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.AccountAddress1)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.AccountAddress2)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.AccountAddress3)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.AccountCity)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.AccountState)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.AccountPostCode)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.Commentary)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .Property(e => e.Institution)
                .IsUnicode(false);

            modelBuilder.Entity<Policy>()
                .HasOptional(e => e.PolicyDetail)
                .WithRequired(e => e.Policy);

            modelBuilder.Entity<PolicyDetail>()
                .Property(e => e.DetailType)
                .IsUnicode(false);

            modelBuilder.Entity<PolicyDetail>()
                .Property(e => e.DetailName)
                .IsUnicode(false);

            modelBuilder.Entity<PolicyDetail>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PolicyDetail>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.ProductType1)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.ProfileProductLinks)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Qualification>()
                .Property(e => e.Qualification1)
                .IsUnicode(false);

            modelBuilder.Entity<QualificationType>()
                .Property(e => e.QualificationType1)
                .IsUnicode(false);

            modelBuilder.Entity<QualificationType>()
                .HasMany(e => e.Qualifications)
                .WithRequired(e => e.QualificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RatioType>()
                .Property(e => e.RatioTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<RatioType>()
                .HasMany(e => e.Ratios)
                .WithRequired(e => e.RatioType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.TempEDISAccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.Individual)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.Postcode)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.WorkPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.ExistingArrangement)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.PaymentAmount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Referral>()
                .Property(e => e.ReferralPurpose)
                .IsUnicode(false);

            modelBuilder.Entity<Referral>()
                .Property(e => e.SpecialInstructions)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.ShortTermGoal1)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.ShortTermGoal2)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.ShortTermGoal3)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.MedTermGoal1)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.MedTermGoal2)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.MedTermGoal3)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.LongTermGoal1)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.LongTermGoal2)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.LongTermGoal3)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.IncomeSource)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.InvestmentObjective1)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.InvestmentObjective2)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.InvestmentObjective3)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.InvestmentKnowledge)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.RiskAttitude)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.CapitalLossAttitude)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.ShortTermTrading)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .Property(e => e.InvestmentProfile)
                .IsUnicode(false);

            modelBuilder.Entity<RiskProfile>()
                .HasMany(e => e.ProfileProductLinks)
                .WithRequired(e => e.RiskProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sector>()
                .Property(e => e.Sector1)
                .IsUnicode(false);

            modelBuilder.Entity<Sector>()
                .HasMany(e => e.Industries)
                .WithRequired(e => e.Sector)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<Security>()
                .Property(e => e.Ticker)
                .IsUnicode(false);

            modelBuilder.Entity<Security>()
                .Property(e => e.PricePerUnit)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Security>()
                .Property(e => e.Brokerage)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Security>()
                .Property(e => e.MarketValue)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevelAction>()
                .Property(e => e.PersonalProfileUpdated)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceLevelAction>()
                .Property(e => e.FinancialInfoProvided)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceLevelAction>()
                .Property(e => e.LastContactCallMade)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceLevelAction>()
                .Property(e => e.FeeDisclosureStatement)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceLevelAction>()
                .Property(e => e.OngoingStatementSigned)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.ServiceLevelName)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.InternalAdminCostMin)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.InternalAdminCostMax)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.ExternalAdminCostMin)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.ExternalAdminCostMax)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.OngoingCostMin)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.OngoingCostMax)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.InitialAdviceCost)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.MinBrokerageFee)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.MaxBrokerageFee)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.RecordOfAdviceFee)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.ConsultingFeePerHour)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.AccountingFeePerHour)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.LegalFeesPerHour)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ServiceLevel>()
                .Property(e => e.AuditingFees)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Statement>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Statement>()
                .Property(e => e.Balance)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Statement>()
                .Property(e => e.Credit)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Statement>()
                .Property(e => e.Debit)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Statement>()
                .Property(e => e.Amount)
                .HasPrecision(9, 2);

            modelBuilder.Entity<StockDistribution>()
                .Property(e => e.DistributionType)
                .IsUnicode(false);

            modelBuilder.Entity<StockHolding>()
                .Property(e => e.HIN)
                .IsUnicode(false);

            modelBuilder.Entity<StockHolding>()
                .HasMany(e => e.StockDistributions)
                .WithRequired(e => e.StockHolding)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.Ticker)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.APIRCode)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.SecID)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.ISIN)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.FirmName)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.BrandingName)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.ManagerName)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.BusinessCountry)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.Domicile)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.SecurityType)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.DividedYTD)
                .HasPrecision(9, 4);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.DailyPrices)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.DividendHistories)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.MorningStarRatings)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.Ratios)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.StockDistributions)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.StockHoldings)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.StockTransactions)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StockTransaction>()
                .Property(e => e.ContractNo)
                .IsUnicode(false);

            modelBuilder.Entity<StockTransaction>()
                .HasMany(e => e.StockHoldings)
                .WithRequired(e => e.StockTransaction)
                .HasForeignKey(e => e.LastTransactionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Strategy>()
                .Property(e => e.Strategy1)
                .IsUnicode(false);

            modelBuilder.Entity<Strategy>()
                .HasMany(e => e.FundStocks)
                .WithRequired(e => e.Strategy)
                .HasForeignKey(e => e.InvestmentStrategyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.CostPerUnit)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.ContractNo)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.BrokerageFee)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.UnitPrice)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.StampDuty)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.LegalFee)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.EstablishmentFee)
                .HasPrecision(9, 2);

            modelBuilder.Entity<TransactionType>()
                .Property(e => e.TransactionType1)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionType>()
                .HasMany(e => e.StockTransactions)
                .WithRequired(e => e.TransactionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionType>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.TransactionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.PreferredName)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.MaritalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.MobilePhone)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.HomePhone)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.WorkPhone)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.WorkEmail)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.HomeEmail)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.ABN)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.ACN)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.AddressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.AddressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.Postcode)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.PostalAddressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.PostalAddressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.PostalCity)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.PostalState)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.PostalPostcode)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .Property(e => e.PostalCountry)
                .IsUnicode(false);
        }
    }
}

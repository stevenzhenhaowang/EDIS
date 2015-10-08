namespace EDIS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.String(nullable: false, maxLength: 128),
                        ClientGroupID = c.String(),
                        AccountNo = c.String(),
                        AccountType = c.Int(nullable: false),
                        Institution = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.AccountID);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressID = c.String(nullable: false, maxLength: 128),
                        AddressLine1 = c.String(nullable: false, maxLength: 200, unicode: false),
                        AddressLine2 = c.String(maxLength: 200, unicode: false),
                        City = c.String(maxLength: 100, unicode: false),
                        State = c.String(nullable: false, maxLength: 100, unicode: false),
                        Postcode = c.String(nullable: false, maxLength: 20, unicode: false),
                        Country = c.String(nullable: false, maxLength: 100, unicode: false),
                        DateCreated = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "dbo.Adviser_CommissionsAndFees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CommissionsAndFeesId = c.Int(),
                        YesNoNA = c.Int(),
                        CommissionDescription = c.String(maxLength: 500),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Adviser_CV",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CV = c.Binary(storeType: "image"),
                        OriginalFileName = c.String(maxLength: 256),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Adviser_DealerGroupDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        DealerGroupName = c.String(maxLength: 256),
                        AFSL = c.String(maxLength: 16),
                        HasDerivativesLicense = c.Int(),
                        IsAuthorisedRep = c.Int(),
                        AuthorisedRepNumber = c.String(maxLength: 32),
                        AddressLn1 = c.String(maxLength: 128),
                        AddressLn2 = c.String(maxLength: 128),
                        AddressLn3 = c.String(maxLength: 128),
                        Suburb = c.String(maxLength: 64),
                        State = c.String(maxLength: 64),
                        PostCode = c.String(maxLength: 64),
                        Country = c.String(maxLength: 64),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Adviser_Description",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        AdviserDescription = c.String(maxLength: 1000),
                        LastUpdated = c.DateTime(),
                        IsApproved = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Adviser_Details",
                c => new
                    {
                        AccountNumber = c.Int(nullable: false, identity: true),
                        AdvisorUserId = c.String(nullable: false),
                        CurrentTitle = c.String(maxLength: 64),
                        CompanyName = c.String(maxLength: 64),
                        ABNACN = c.String(maxLength: 16),
                        ExperienceStartDate = c.DateTime(storeType: "smalldatetime"),
                        Title = c.String(maxLength: 16),
                        FirstName = c.String(maxLength: 128),
                        MiddleName = c.String(maxLength: 128),
                        LastName = c.String(maxLength: 128),
                        Gender = c.String(maxLength: 32),
                        Phone = c.String(maxLength: 32),
                        Fax = c.String(maxLength: 32),
                        Mobile = c.String(maxLength: 32),
                        AddressLn1 = c.String(maxLength: 128),
                        AddressLn2 = c.String(maxLength: 128),
                        AddressLn3 = c.String(maxLength: 128),
                        Suburb = c.String(maxLength: 64),
                        State = c.String(maxLength: 64),
                        PostCode = c.String(maxLength: 64),
                        Country = c.String(maxLength: 64),
                        Lng = c.Double(),
                        Lat = c.Double(),
                        LastUpdated = c.DateTime(),
                        VerifiedId = c.Int(),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Image = c.Binary(),
                        ImageMimeType = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.AccountNumber);
            
            CreateTable(
                "dbo.Adviser_Education",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        EducationLevels = c.String(maxLength: 64),
                        Institution = c.String(maxLength: 128),
                        CourseName = c.String(maxLength: 128),
                        CurrentlyStudying = c.Int(),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Adviser_ManagementDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TotalAssets = c.Double(),
                        TotalManagedInvestments = c.Double(),
                        TotalDirectAustralianEquities = c.Double(),
                        TotalDirectInternational = c.Double(),
                        TotalFixedInterest = c.Double(),
                        TotalLendingBook = c.Double(),
                        ApproxClientNumbersId = c.Int(),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Adviser_ProfessionalAssociations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ProfessionalAssociationsId = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Adviser_ProfessionalType",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ProfessionTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Adviser_ProfilePictures",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ImageData = c.Binary(storeType: "image"),
                        UserTypeId = c.String(maxLength: 128),
                        PictureApprovalStatusId = c.Int(),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Adviser_Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UserId_Client = c.String(maxLength: 128),
                        Feedback_Q1 = c.Double(),
                        Feedback_Q1_Comments = c.String(maxLength: 500),
                        Feedback_Q2 = c.Double(),
                        Feedback_Q2_Comments = c.String(maxLength: 500),
                        Feedback_Q3 = c.Double(),
                        Feedback_Q3_Comments = c.String(maxLength: 500),
                        Feedback_Q4 = c.Double(),
                        Feedback_Q4_Comments = c.String(maxLength: 500),
                        Feedback_Q5 = c.Double(),
                        Feedback_Q5_Comments = c.String(maxLength: 500),
                        AdditionalComments = c.String(maxLength: 500),
                        IsSuccessReferral = c.String(maxLength: 5),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Adviser_ServicesProviding",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        SubServiceId = c.Int(),
                        SubServiceProvideYesNo = c.String(maxLength: 5, unicode: false),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Adviser_Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        PlanTypeId = c.Int(),
                        SubscriptionStatusId = c.Int(),
                        AmountPaid = c.Double(),
                        StartDate = c.DateTime(storeType: "smalldatetime"),
                        EndDate = c.DateTime(storeType: "smalldatetime"),
                        TransactionId = c.String(maxLength: 512),
                        TransactionDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Adviser_TargetClients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        AnnualIncomeLevelsId = c.Int(),
                        InvestibleAssetsLevelId = c.Int(),
                        TotalAssetsLevelId = c.Int(),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Adviser_Verified_Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        VerifiedStatusId = c.Int(),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        AttachmentID = c.String(nullable: false, maxLength: 128),
                        NoteID = c.String(maxLength: 128),
                        Path = c.String(unicode: false),
                        Title = c.String(nullable: false, maxLength: 200, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        Comments = c.String(maxLength: 200, unicode: false),
                        Data = c.Binary(),
                        AttachmentType = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.AttachmentID)
                .ForeignKey("dbo.Notes", t => t.NoteID)
                .Index(t => t.NoteID);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteID = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        AdviserID = c.String(nullable: false, maxLength: 128),
                        ClientID = c.String(nullable: false, maxLength: 128),
                        AssetClass = c.String(maxLength: 100, unicode: false),
                        ProductClass = c.String(maxLength: 100, unicode: false),
                        Product = c.String(maxLength: 100, unicode: false),
                        Purpose = c.String(maxLength: 100, unicode: false),
                        TimeSpent = c.Double(),
                        NoteSerial = c.String(maxLength: 100, unicode: false),
                        Subject = c.String(nullable: false, maxLength: 200, unicode: false),
                        Body = c.String(unicode: false),
                        FollowupActions = c.String(maxLength: 100, unicode: false),
                        DateDue = c.DateTime(storeType: "date"),
                        Status = c.String(maxLength: 50, unicode: false),
                        FollowupDate = c.DateTime(storeType: "date"),
                        DateCompleted = c.DateTime(),
                        Reminder = c.Boolean(),
                        ReminderDate = c.DateTime(),
                        NoteType = c.Int(nullable: false),
                        IsAccepted = c.Boolean(),
                        IsDeclined = c.Boolean(),
                        AssetTypeID = c.Int(),
                        AccountID = c.String(maxLength: 128),
                        SenderRole = c.Int(nullable: false),
                        Client_ClientUserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.NoteID)
                .ForeignKey("dbo.AssetTypes", t => t.AssetTypeID)
                .ForeignKey("dbo.Clients", t => t.Client_ClientUserID)
                .Index(t => t.AssetTypeID)
                .Index(t => t.Client_ClientUserID);
            
            CreateTable(
                "dbo.AssetTypes",
                c => new
                    {
                        AssetTypeID = c.Int(nullable: false, identity: true),
                        AssetType = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.AssetTypeID);
            
            CreateTable(
                "dbo.Statements",
                c => new
                    {
                        StatementID = c.String(nullable: false, maxLength: 128),
                        AssetTypeID = c.Int(nullable: false),
                        AccountID = c.String(nullable: false, maxLength: 128),
                        StatementDate = c.DateTime(nullable: false, storeType: "date"),
                        Description = c.String(nullable: false, maxLength: 50, unicode: false),
                        Balance = c.Decimal(nullable: false, precision: 9, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 9, scale: 2),
                        Debit = c.Decimal(nullable: false, precision: 9, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 9, scale: 2),
                    })
                .PrimaryKey(t => t.StatementID)
                .ForeignKey("dbo.AssetTypes", t => t.AssetTypeID, cascadeDelete: true)
                .Index(t => t.AssetTypeID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.String(nullable: false, maxLength: 128),
                        TransactionTypeID = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 9, scale: 2),
                        TransactionDate = c.DateTime(nullable: false, storeType: "date"),
                        ContractNo = c.String(nullable: false, maxLength: 100, unicode: false),
                        TransactionFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountID = c.String(nullable: false, maxLength: 128),
                        NoOfUnits = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        AssetType_AssetTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.AssetTypes", t => t.AssetType_AssetTypeID)
                .Index(t => t.AssetType_AssetTypeID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientUserID = c.String(nullable: false, maxLength: 128),
                        ClientGroupID = c.String(maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        LastContact = c.DateTime(storeType: "date"),
                        LastReview = c.DateTime(storeType: "date"),
                        RiskProfileOutcome = c.Int(),
                        Image = c.Binary(),
                        ImageMimeType = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ClientUserID);
            
            CreateTable(
                "dbo.Policies",
                c => new
                    {
                        PolicyID = c.String(nullable: false, maxLength: 128),
                        ClientID = c.String(nullable: false, maxLength: 128),
                        InsuranceType = c.String(nullable: false, maxLength: 100, unicode: false),
                        PolicyType = c.String(nullable: false, maxLength: 100, unicode: false),
                        PolicyName = c.String(nullable: false, maxLength: 100, unicode: false),
                        PolicyNumber = c.String(maxLength: 50, unicode: false),
                        AccountName = c.String(nullable: false, maxLength: 100, unicode: false),
                        AccountAddress1 = c.String(maxLength: 200, unicode: false),
                        AccountAddress2 = c.String(maxLength: 200, unicode: false),
                        AccountAddress3 = c.String(maxLength: 200, unicode: false),
                        AccountCity = c.String(maxLength: 100, unicode: false),
                        AccountState = c.String(maxLength: 100, unicode: false),
                        AccountPostCode = c.String(maxLength: 100, unicode: false),
                        InceptionDate = c.DateTime(storeType: "date"),
                        LastRenewalDate = c.DateTime(storeType: "date"),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        DateCreated = c.DateTime(storeType: "date"),
                        DateModified = c.DateTime(storeType: "date"),
                        Commentary = c.String(unicode: false),
                        Institution = c.String(maxLength: 100, unicode: false),
                        RenewalDueDate = c.DateTime(storeType: "date"),
                        Client_ClientUserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PolicyID)
                .ForeignKey("dbo.Clients", t => t.Client_ClientUserID)
                .Index(t => t.Client_ClientUserID);
            
            CreateTable(
                "dbo.Beneficiaries",
                c => new
                    {
                        BeneficiaryID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 200, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 100, unicode: false),
                        Relationship = c.String(nullable: false, maxLength: 100, unicode: false),
                        Address1 = c.String(maxLength: 200, unicode: false),
                        Address2 = c.String(maxLength: 200, unicode: false),
                        Address3 = c.String(maxLength: 200, unicode: false),
                        City = c.String(maxLength: 50, unicode: false),
                        State = c.String(maxLength: 50, unicode: false),
                        PostCode = c.String(maxLength: 10, unicode: false),
                        Phone = c.String(maxLength: 50, unicode: false),
                        Mobile = c.String(maxLength: 50, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        BeneficiaryPercentage = c.Double(nullable: false),
                        PolicyID = c.String(maxLength: 128),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        DateModified = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.BeneficiaryID)
                .ForeignKey("dbo.Policies", t => t.PolicyID)
                .Index(t => t.PolicyID);
            
            CreateTable(
                "dbo.PolicyDetails",
                c => new
                    {
                        PolicyDetailID = c.String(nullable: false, maxLength: 128),
                        PolicyID = c.String(nullable: false, maxLength: 128),
                        DetailType = c.String(nullable: false, maxLength: 100, unicode: false),
                        DetailName = c.String(maxLength: 100, unicode: false),
                        Description = c.String(maxLength: 100, unicode: false),
                        Amount = c.Decimal(precision: 9, scale: 2),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        DateModified = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.PolicyDetailID)
                .ForeignKey("dbo.Policies", t => t.PolicyDetailID)
                .Index(t => t.PolicyDetailID);
            
            CreateTable(
                "dbo.RiskProfiles",
                c => new
                    {
                        RiskProfileID = c.String(nullable: false, maxLength: 128),
                        ClientID = c.String(nullable: false, maxLength: 128),
                        ShortTermGoal1 = c.String(maxLength: 200, unicode: false),
                        ShortTermGoal2 = c.String(maxLength: 200, unicode: false),
                        ShortTermGoal3 = c.String(maxLength: 200, unicode: false),
                        MedTermGoal1 = c.String(maxLength: 200, unicode: false),
                        MedTermGoal2 = c.String(maxLength: 200, unicode: false),
                        MedTermGoal3 = c.String(maxLength: 200, unicode: false),
                        LongTermGoal1 = c.String(maxLength: 200, unicode: false),
                        LongTermGoal2 = c.String(maxLength: 200, unicode: false),
                        LongTermGoal3 = c.String(maxLength: 200, unicode: false),
                        Comments = c.String(unicode: false),
                        RetirementAge = c.Int(),
                        RetirementIncome = c.Double(),
                        IncomeSource = c.String(maxLength: 100, unicode: false),
                        InvestmentObjective1 = c.String(maxLength: 200, unicode: false),
                        InvestmentObjective2 = c.String(maxLength: 200, unicode: false),
                        InvestmentObjective3 = c.String(maxLength: 200, unicode: false),
                        InvestmentTimeHorizon = c.Int(),
                        InvestmentKnowledge = c.String(maxLength: 200, unicode: false),
                        RiskAttitude = c.String(maxLength: 200, unicode: false),
                        CapitalLossAttitude = c.String(maxLength: 200, unicode: false),
                        ShortTermTrading = c.String(maxLength: 50, unicode: false),
                        ShortTermAssetPercent = c.Double(),
                        ShortTermEquityPercent = c.Double(),
                        InvestmentProfile = c.String(maxLength: 100, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        Client_ClientUserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RiskProfileID)
                .ForeignKey("dbo.Clients", t => t.Client_ClientUserID)
                .Index(t => t.Client_ClientUserID);
            
            CreateTable(
                "dbo.Client_AdvisoryNeeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ReasonForSeekingAdvice = c.String(maxLength: 1000),
                        PersonalFinancialGoals = c.String(maxLength: 1000),
                        ExpectationsFromAdviser = c.String(maxLength: 1000),
                        AdditionalComments = c.String(maxLength: 1000),
                        CommunicationMethodId = c.Int(),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Client_Details",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        DateOfBirth = c.DateTime(storeType: "smalldatetime"),
                        Title = c.String(maxLength: 16),
                        FirstName = c.String(maxLength: 128),
                        MiddleName = c.String(maxLength: 128),
                        LastName = c.String(maxLength: 128),
                        Gender = c.String(maxLength: 32),
                        Phone = c.String(maxLength: 32),
                        Fax = c.String(maxLength: 32),
                        Mobile = c.String(maxLength: 32),
                        AddressLn1 = c.String(maxLength: 128),
                        AddressLn2 = c.String(maxLength: 128),
                        AddressLn3 = c.String(maxLength: 128),
                        Suburb = c.String(maxLength: 64),
                        State = c.String(maxLength: 64),
                        PostCode = c.String(maxLength: 64),
                        Country = c.String(maxLength: 64),
                        Lng = c.Double(),
                        Lat = c.Double(),
                        LastUpdated = c.DateTime(),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ClientGroupId = c.String(maxLength: 128, fixedLength: true),
                        EntityName = c.String(maxLength: 512),
                        EntityType = c.String(maxLength: 256),
                        abn = c.String(maxLength: 256),
                        acn = c.String(maxLength: 256),
                        ClientType = c.String(maxLength: 50),
                        EmploymentStatus = c.String(maxLength: 50),
                        AnnualIncome = c.Double(),
                        SuperAnnuationAmount = c.Double(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Client_FinancialPosition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TimeFrameForAdviceId = c.Int(),
                        EmploymentStatusId = c.Int(),
                        TotalAnnualIncome = c.Double(),
                        TotalAssets = c.Double(),
                        TotalLiabilities = c.Double(),
                        TotalEquity = c.Double(),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Client_InvestmentProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        InvestableAssets = c.Double(),
                        TimeHorizonId = c.Int(),
                        ReturnsExpectation = c.Double(),
                        InvestmentKnowledgeId = c.Int(),
                        AttitudeToRiskId = c.Int(),
                        AttitudeToCapitalLossId = c.Int(),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Client_Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UserId_Adviser = c.String(maxLength: 128),
                        Feedback_Q1 = c.Double(),
                        Feedback_Q1_Comments = c.String(maxLength: 500),
                        Feedback_Q2 = c.Double(),
                        Feedback_Q2_Comments = c.String(maxLength: 500),
                        Feedback_Q3 = c.Double(),
                        Feedback_Q3_Comments = c.String(maxLength: 500),
                        Feedback_Q4 = c.Double(),
                        Feedback_Q4_Comments = c.String(maxLength: 500),
                        Feedback_Q5 = c.Double(),
                        Feedback_Q5_Comments = c.String(maxLength: 500),
                        AdditionalComments = c.String(maxLength: 500),
                        IsSuccessReferral = c.String(maxLength: 5),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientFileActions",
                c => new
                    {
                        ClientFileActionID = c.Int(nullable: false),
                        ClientFileAction = c.String(nullable: false, maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.ClientFileActionID);
            
            CreateTable(
                "dbo.ClientFileInformations",
                c => new
                    {
                        ClientFileID = c.String(nullable: false, maxLength: 128),
                        ClientID = c.String(nullable: false, maxLength: 128),
                        ClientActionID = c.Int(nullable: false),
                        Response = c.String(nullable: false, maxLength: 100, unicode: false),
                        ResponseDate = c.DateTime(storeType: "date"),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        ClientFileInformation1_ClientFileID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ClientFileID)
                .ForeignKey("dbo.ClientFileInformations", t => t.ClientFileInformation1_ClientFileID)
                .Index(t => t.ClientFileInformation1_ClientFileID);
            
            CreateTable(
                "dbo.ClientGroups",
                c => new
                    {
                        ClientGroupID = c.String(nullable: false, maxLength: 128),
                        AccountName = c.String(),
                        AdviserNumber = c.Int(nullable: false, identity: true),
                        Alias = c.String(nullable: false, maxLength: 100, unicode: false),
                        RiskProfileOutcome = c.Int(),
                        ServiceLevelID = c.Int(),
                        MainClientID = c.String(maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClientGroupID)
                .ForeignKey("dbo.ServiceLevels", t => t.ServiceLevelID)
                .Index(t => t.ServiceLevelID);
            
            CreateTable(
                "dbo.ServiceLevels",
                c => new
                    {
                        ServiceLevelID = c.Int(nullable: false),
                        ServiceLevelName = c.String(nullable: false, maxLength: 50, unicode: false),
                        ReviewsPerAnnum = c.Int(nullable: false),
                        ReportsPerAnnum = c.Int(nullable: false),
                        PhoneContactsPerAnnum = c.Int(nullable: false),
                        StatementsPerAnnum = c.Int(nullable: false),
                        RecordOfAdvicePerAnnum = c.Int(nullable: false),
                        AdviceCallPerAnnum = c.Int(nullable: false),
                        BulletinsPerAnnum = c.Int(nullable: false),
                        AustralianEquityResearch = c.Boolean(nullable: false),
                        InternationalEquityResearch = c.Boolean(nullable: false),
                        ManagedInvestmentResearch = c.Boolean(nullable: false),
                        PropertyResearch = c.Boolean(nullable: false),
                        IPOs = c.Boolean(nullable: false),
                        OnlineAccess = c.Boolean(nullable: false),
                        SeminarsPerAnnum = c.Int(nullable: false),
                        InternalAdminCostMin = c.Decimal(nullable: false, precision: 9, scale: 2),
                        InternalAdminCostMax = c.Decimal(nullable: false, precision: 9, scale: 2),
                        ExternalAdminCostMin = c.Decimal(nullable: false, precision: 9, scale: 2),
                        ExternalAdminCostMax = c.Decimal(nullable: false, precision: 9, scale: 2),
                        OngoingCostMin = c.Decimal(nullable: false, precision: 9, scale: 2),
                        OngoingCostMax = c.Decimal(nullable: false, precision: 9, scale: 2),
                        InitialAdviceCost = c.Decimal(nullable: false, precision: 9, scale: 2),
                        MinBrokerageFee = c.Decimal(nullable: false, precision: 9, scale: 2),
                        MaxBrokerageFee = c.Decimal(nullable: false, precision: 9, scale: 2),
                        RecordOfAdviceFee = c.Decimal(nullable: false, precision: 9, scale: 2),
                        ConsultingFeePerHour = c.Decimal(nullable: false, precision: 9, scale: 2),
                        AccountingFeePerHour = c.Decimal(nullable: false, precision: 9, scale: 2),
                        LegalFeesPerHour = c.Decimal(nullable: false, precision: 9, scale: 2),
                        AuditingFees = c.Decimal(nullable: false, precision: 9, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceLevelID);
            
            CreateTable(
                "dbo.CommunicationRequest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adviser_UserId = c.String(maxLength: 128),
                        Client_UserId = c.String(maxLength: 128),
                        AcceptOrDecline = c.Boolean(),
                        RequestDate = c.DateTime(),
                        DeclineCommunicationReasonsId = c.Int(),
                        ProcessDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DailyPrices",
                c => new
                    {
                        DailyPriceID = c.String(nullable: false, maxLength: 128),
                        StockID = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 9, scale: 4),
                        High = c.Decimal(nullable: false, precision: 9, scale: 4),
                        Low = c.Decimal(nullable: false, precision: 9, scale: 4),
                        High52WeekDate = c.DateTime(nullable: false, storeType: "date"),
                        High52WeekPrice = c.Decimal(nullable: false, precision: 9, scale: 4),
                        Low52WeekDate = c.DateTime(nullable: false, storeType: "date"),
                        Low52WeekPrice = c.Decimal(nullable: false, precision: 9, scale: 4),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DailyPriceID);
            
            CreateTable(
                "dbo.DealerGroups",
                c => new
                    {
                        DealerGroupID = c.String(nullable: false, maxLength: 128),
                        DealerGroupName = c.String(nullable: false, maxLength: 100, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DealerGroupID);
            
            CreateTable(
                "dbo.Dependants",
                c => new
                    {
                        DependantID = c.String(nullable: false, maxLength: 128),
                        DependantClientID = c.String(nullable: false, maxLength: 128),
                        ParentClientID = c.String(nullable: false, maxLength: 128),
                        Relationship = c.String(maxLength: 100, unicode: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.DependantID);
            
            CreateTable(
                "dbo.DistributionLists",
                c => new
                    {
                        DistributionListId = c.Int(nullable: false, identity: true),
                        DistributionList = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.DistributionListId);
            
            CreateTable(
                "dbo.DividendHistories",
                c => new
                    {
                        DividendHistoryID = c.String(nullable: false, maxLength: 128),
                        StockID = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        CurrencyID = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 9, scale: 4),
                        Status = c.String(nullable: false, maxLength: 50, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DividendHistoryID);
            
            CreateTable(
                "dbo.EducationRecords",
                c => new
                    {
                        EducationRecordID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(nullable: false, maxLength: 128),
                        EducationLevel = c.String(maxLength: 50, unicode: false),
                        YearStarted = c.Int(),
                        YearCompleted = c.Int(),
                        YearsSinceCompletion = c.Int(),
                        EducationInstitution = c.String(maxLength: 100, unicode: false),
                        CourseAttended = c.String(maxLength: 100, unicode: false),
                        Description = c.String(unicode: false),
                        Comments = c.String(unicode: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.EducationRecordID);
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        IndustryID = c.Int(nullable: false),
                        Industry = c.String(nullable: false, maxLength: 200, unicode: false),
                        SectorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IndustryID)
                .ForeignKey("dbo.Sectors", t => t.SectorID)
                .Index(t => t.SectorID);
            
            CreateTable(
                "dbo.Sectors",
                c => new
                    {
                        SectorID = c.Int(nullable: false),
                        Sector = c.String(nullable: false, maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.SectorID);
            
            CreateTable(
                "dbo.MorningStarCategories",
                c => new
                    {
                        MorningStarCategoryID = c.Int(nullable: false),
                        MorningStarCategory = c.String(nullable: false, maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.MorningStarCategoryID);
            
            CreateTable(
                "dbo.MorningStarRatings",
                c => new
                    {
                        MorningStarRatingID = c.String(nullable: false, maxLength: 128),
                        StockID = c.String(nullable: false, maxLength: 128),
                        MorningStarCategoryID = c.Int(nullable: false),
                        RatingOverall = c.String(nullable: false, maxLength: 50, unicode: false),
                        AnalystRating = c.String(nullable: false, maxLength: 50, unicode: false),
                        Rating3Yr = c.String(nullable: false, maxLength: 50, unicode: false),
                        Rating5Yr = c.String(nullable: false, maxLength: 50, unicode: false),
                        Rating10Yr = c.String(nullable: false, maxLength: 50, unicode: false),
                        RatingDate = c.DateTime(nullable: false, storeType: "date"),
                        RiskOverall = c.String(nullable: false, maxLength: 50, unicode: false),
                        Risk3Yr = c.String(nullable: false, maxLength: 50, unicode: false),
                        Risk5Yr = c.String(nullable: false, maxLength: 50, unicode: false),
                        Risk10Yr = c.String(nullable: false, maxLength: 50, unicode: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MorningStarRatingID)
                .ForeignKey("dbo.MorningStarCategories", t => t.MorningStarCategoryID)
                .Index(t => t.MorningStarCategoryID);
            
            CreateTable(
                "dbo.NoteLinks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        NoteID1 = c.String(),
                        NoteID2 = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProfessionalAssociations",
                c => new
                    {
                        ProfessionalAssociationsId = c.Int(nullable: false),
                        ProfessionalAssociations = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProfessionalAssociationsId);
            
            CreateTable(
                "dbo.Referrals",
                c => new
                    {
                        ReferralID = c.String(nullable: false, maxLength: 128),
                        TempEDISAccountNo = c.String(maxLength: 50, unicode: false),
                        Individual = c.String(nullable: false, maxLength: 100, unicode: false),
                        Company = c.String(maxLength: 100, unicode: false),
                        Address1 = c.String(maxLength: 100, unicode: false),
                        Address2 = c.String(maxLength: 100, unicode: false),
                        City = c.String(maxLength: 100, unicode: false),
                        Postcode = c.String(maxLength: 10, unicode: false),
                        WorkPhone = c.String(maxLength: 20, unicode: false),
                        MobileNo = c.String(maxLength: 20, unicode: false),
                        EmailAddress = c.String(maxLength: 50, unicode: false),
                        ExistingArrangement = c.String(nullable: false, maxLength: 20, unicode: false),
                        PaymentMade = c.Boolean(nullable: false),
                        PaymentAmount = c.Decimal(precision: 9, scale: 2),
                        ReferralPurpose = c.String(maxLength: 50, unicode: false),
                        SpecialInstructions = c.String(nullable: false, maxLength: 50, unicode: false),
                        AdviserID = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReferralID);
            
            CreateTable(
                "dbo.ResourcesReferences",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        tokenValue = c.String(),
                        resourceUrl = c.String(),
                        UserId = c.String(),
                        fileExtension = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ServiceLevelActions",
                c => new
                    {
                        ServiceLevelActionID = c.String(nullable: false, maxLength: 128),
                        ClientGroupID = c.String(nullable: false, maxLength: 128),
                        PersonalProfileUpdated = c.String(nullable: false, maxLength: 10, unicode: false),
                        PersonalProfileUpdateDate = c.DateTime(nullable: false, storeType: "date"),
                        FinancialInfoProvided = c.String(nullable: false, maxLength: 10, unicode: false),
                        FinancialInfoProvidedDate = c.DateTime(nullable: false, storeType: "date"),
                        LastContactCallMade = c.String(nullable: false, maxLength: 10, unicode: false),
                        LastContactCallMadeDate = c.DateTime(nullable: false, storeType: "date"),
                        FeeDisclosureStatement = c.String(nullable: false, maxLength: 10, unicode: false),
                        FeeDisclosureStatementDate = c.DateTime(nullable: false, storeType: "date"),
                        OngoingStatementSigned = c.String(nullable: false, maxLength: 10, unicode: false),
                        OngoingStatementSignedDate = c.DateTime(nullable: false, storeType: "date"),
                        LastStatementOfAdviceDate = c.DateTime(nullable: false, storeType: "date"),
                        NumStatementOfAdvice = c.Int(nullable: false),
                        LastRecordOfAdviceDate = c.DateTime(nullable: false, storeType: "date"),
                        NumRecordOfAdvice = c.Int(nullable: false),
                        NumAnnualReviewMeeting = c.Int(nullable: false),
                        NumPhoneContacts = c.Int(nullable: false),
                        NumInvestmentReports = c.Int(nullable: false),
                        NumAdviceCalls = c.Int(nullable: false),
                        NumSeminars = c.Int(nullable: false),
                        NumMonthlyBulletin = c.Int(nullable: false),
                        NumWeeklyBulletin = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceLevelActionID);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false),
                        ServiceName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.SubServices",
                c => new
                    {
                        SubServiceId = c.Int(nullable: false, identity: true),
                        SubServiceName = c.String(maxLength: 128),
                        ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.SubServiceId);
            
            CreateTable(
                "dbo.User_DistributionList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        DistributionListId = c.Int(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_DistributionLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistributionListId = c.Int(),
                        UserId = c.String(maxLength: 256),
                        RetDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_NewsletterServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        NewsletterServicesId = c.Int(),
                        IsSubscribed = c.String(maxLength: 5),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MorningStarRatings", "MorningStarCategoryID", "dbo.MorningStarCategories");
            DropForeignKey("dbo.Industries", "SectorID", "dbo.Sectors");
            DropForeignKey("dbo.ClientGroups", "ServiceLevelID", "dbo.ServiceLevels");
            DropForeignKey("dbo.ClientFileInformations", "ClientFileInformation1_ClientFileID", "dbo.ClientFileInformations");
            DropForeignKey("dbo.RiskProfiles", "Client_ClientUserID", "dbo.Clients");
            DropForeignKey("dbo.Policies", "Client_ClientUserID", "dbo.Clients");
            DropForeignKey("dbo.PolicyDetails", "PolicyDetailID", "dbo.Policies");
            DropForeignKey("dbo.Beneficiaries", "PolicyID", "dbo.Policies");
            DropForeignKey("dbo.Notes", "Client_ClientUserID", "dbo.Clients");
            DropForeignKey("dbo.Attachments", "NoteID", "dbo.Notes");
            DropForeignKey("dbo.Transactions", "AssetType_AssetTypeID", "dbo.AssetTypes");
            DropForeignKey("dbo.Statements", "AssetTypeID", "dbo.AssetTypes");
            DropForeignKey("dbo.Notes", "AssetTypeID", "dbo.AssetTypes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.MorningStarRatings", new[] { "MorningStarCategoryID" });
            DropIndex("dbo.Industries", new[] { "SectorID" });
            DropIndex("dbo.ClientGroups", new[] { "ServiceLevelID" });
            DropIndex("dbo.ClientFileInformations", new[] { "ClientFileInformation1_ClientFileID" });
            DropIndex("dbo.RiskProfiles", new[] { "Client_ClientUserID" });
            DropIndex("dbo.PolicyDetails", new[] { "PolicyDetailID" });
            DropIndex("dbo.Beneficiaries", new[] { "PolicyID" });
            DropIndex("dbo.Policies", new[] { "Client_ClientUserID" });
            DropIndex("dbo.Transactions", new[] { "AssetType_AssetTypeID" });
            DropIndex("dbo.Statements", new[] { "AssetTypeID" });
            DropIndex("dbo.Notes", new[] { "Client_ClientUserID" });
            DropIndex("dbo.Notes", new[] { "AssetTypeID" });
            DropIndex("dbo.Attachments", new[] { "NoteID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropTable("dbo.User_NewsletterServices");
            DropTable("dbo.User_DistributionLists");
            DropTable("dbo.User_DistributionList");
            DropTable("dbo.SubServices");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceLevelActions");
            DropTable("dbo.ResourcesReferences");
            DropTable("dbo.Referrals");
            DropTable("dbo.ProfessionalAssociations");
            DropTable("dbo.NoteLinks");
            DropTable("dbo.MorningStarRatings");
            DropTable("dbo.MorningStarCategories");
            DropTable("dbo.Sectors");
            DropTable("dbo.Industries");
            DropTable("dbo.EducationRecords");
            DropTable("dbo.DividendHistories");
            DropTable("dbo.DistributionLists");
            DropTable("dbo.Dependants");
            DropTable("dbo.DealerGroups");
            DropTable("dbo.DailyPrices");
            DropTable("dbo.CommunicationRequest");
            DropTable("dbo.ServiceLevels");
            DropTable("dbo.ClientGroups");
            DropTable("dbo.ClientFileInformations");
            DropTable("dbo.ClientFileActions");
            DropTable("dbo.Client_Ratings");
            DropTable("dbo.Client_InvestmentProfile");
            DropTable("dbo.Client_FinancialPosition");
            DropTable("dbo.Client_Details");
            DropTable("dbo.Client_AdvisoryNeeds");
            DropTable("dbo.RiskProfiles");
            DropTable("dbo.PolicyDetails");
            DropTable("dbo.Beneficiaries");
            DropTable("dbo.Policies");
            DropTable("dbo.Clients");
            DropTable("dbo.Transactions");
            DropTable("dbo.Statements");
            DropTable("dbo.AssetTypes");
            DropTable("dbo.Notes");
            DropTable("dbo.Attachments");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Adviser_Verified_Status");
            DropTable("dbo.Adviser_TargetClients");
            DropTable("dbo.Adviser_Subscriptions");
            DropTable("dbo.Adviser_ServicesProviding");
            DropTable("dbo.Adviser_Ratings");
            DropTable("dbo.Adviser_ProfilePictures");
            DropTable("dbo.Adviser_ProfessionalType");
            DropTable("dbo.Adviser_ProfessionalAssociations");
            DropTable("dbo.Adviser_ManagementDetails");
            DropTable("dbo.Adviser_Education");
            DropTable("dbo.Adviser_Details");
            DropTable("dbo.Adviser_Description");
            DropTable("dbo.Adviser_DealerGroupDetails");
            DropTable("dbo.Adviser_CV");
            DropTable("dbo.Adviser_CommissionsAndFees");
            DropTable("dbo.Addresses");
            DropTable("dbo.Accounts");
        }
    }
}

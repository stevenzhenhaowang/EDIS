namespace EDIS_DOMAIN
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
        public virtual DbSet<NoteLink> NoteLinks { get; set; }
        public virtual DbSet<ResourcesReference> ResourcesReferences { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
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
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Beneficiary> Beneficiaries { get; set; }
        public virtual DbSet<Client_AdvisoryNeeds> Client_AdvisoryNeeds { get; set; }
        public virtual DbSet<Client_Details> Client_Details { get; set; }
        public virtual DbSet<Client_FinancialPosition> Client_FinancialPosition { get; set; }
        public virtual DbSet<Client_InvestmentProfile> Client_InvestmentProfile { get; set; }
        public virtual DbSet<Client_Ratings> Client_Ratings { get; set; }
        public virtual DbSet<ClientFileAction> ClientFileActions { get; set; }
        public virtual DbSet<ClientFileInformation> ClientFileInformations { get; set; }
        public virtual DbSet<ClientGroup> ClientGroups { get; set; }
        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<CommunicationRequest> CommunicationRequests { get; set; }
        public virtual DbSet<DailyPrice> DailyPrices { get; set; }
        public virtual DbSet<DealerGroup> DealerGroups { get; set; }
        public virtual DbSet<Dependant> Dependants { get; set; }
        public virtual DbSet<DistributionList> DistributionLists { get; set; }
        public virtual DbSet<DividendHistory> DividendHistories { get; set; }
        public virtual DbSet<EducationRecord> EducationRecords { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<MorningStarCategory> MorningStarCategories { get; set; }
        public virtual DbSet<MorningStarRating> MorningStarRatings { get; set; }

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<PolicyDetail> PolicyDetails { get; set; }
        public virtual DbSet<ProfessionalAssociation> ProfessionalAssociations { get; set; }
        public virtual DbSet<Referral> Referrals { get; set; }
        public virtual DbSet<RiskProfile> RiskProfiles { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<ServiceLevelAction> ServiceLevelActions { get; set; }
        public virtual DbSet<ServiceLevel> ServiceLevels { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Statement> Statements { get; set; }
        public virtual DbSet<SubService> SubServices { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User_DistributionList> User_DistributionList { get; set; }
        public virtual DbSet<User_DistributionLists> User_DistributionLists { get; set; }
        public virtual DbSet<User_NewsletterServices> User_NewsletterServices { get; set; }
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

         

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

        

            modelBuilder.Entity<AssetType>()
                .Property(e => e.AssetType1)
                .IsUnicode(false);

           

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.Statements)
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
          
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Notes)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Policies)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Client>()
                .HasMany(e => e.RiskProfiles)
                .WithRequired(e => e.Client)
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
                .Property(e => e.Relationship)
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

           

            modelBuilder.Entity<Industry>()
                .Property(e => e.Industry1)
                .IsUnicode(false);

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

          
            modelBuilder.Entity<Sector>()
                .Property(e => e.Sector1)
                .IsUnicode(false);

            modelBuilder.Entity<Sector>()
                .HasMany(e => e.Industries)
                .WithRequired(e => e.Sector)
                .WillCascadeOnDelete(false);

        

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
         

            modelBuilder.Entity<Transaction>()
                .Property(e => e.ContractNo)
                .IsUnicode(false);
                     
            modelBuilder.Entity<Transaction>()
                .Property(e => e.UnitPrice)
                .HasPrecision(9, 2);
          

           
        }
    }
}

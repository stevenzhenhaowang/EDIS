USE [master]
GO
/****** Object:  Database [edisdatabase]    Script Date: 03/03/15 1:01:36 PM ******/
CREATE DATABASE [edisdatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'edisdatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014\MSSQL\DATA\edisdatabase.mdf' , SIZE = 11264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'edisdatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014\MSSQL\DATA\edisdatabase_log.ldf' , SIZE = 3456KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [edisdatabase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [edisdatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [edisdatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [edisdatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [edisdatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [edisdatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [edisdatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [edisdatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [edisdatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [edisdatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [edisdatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [edisdatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [edisdatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [edisdatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [edisdatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [edisdatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [edisdatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [edisdatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [edisdatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [edisdatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [edisdatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [edisdatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [edisdatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [edisdatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [edisdatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [edisdatabase] SET  MULTI_USER 
GO
ALTER DATABASE [edisdatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [edisdatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [edisdatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [edisdatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [edisdatabase] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'edisdatabase', N'ON'
GO
USE [edisdatabase]
GO
/****** Object:  User [Trial]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE USER [Trial] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [edisdbadmin]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE USER [edisdbadmin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[db_owner]
GO
/****** Object:  DatabaseRole [aspnet_WebEvent_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_WebEvent_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_ReportingAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Roles_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Roles_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_BasicAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Roles_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Profile_ReportingAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Profile_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Profile_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Profile_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Profile_BasicAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Profile_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Personalization_ReportingAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Personalization_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Personalization_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Personalization_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Personalization_BasicAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Personalization_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Membership_ReportingAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Membership_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Membership_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Membership_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Membership_BasicAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE ROLE [aspnet_Membership_BasicAccess]
GO
ALTER ROLE [aspnet_WebEvent_FullAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Roles_ReportingAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Roles_FullAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Roles_BasicAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Profile_ReportingAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Profile_FullAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Profile_BasicAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Personalization_ReportingAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Personalization_FullAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Personalization_BasicAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Membership_ReportingAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Membership_FullAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [aspnet_Membership_BasicAccess] ADD MEMBER [Trial]
GO
ALTER ROLE [db_owner] ADD MEMBER [Trial]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [Trial]
GO
ALTER ROLE [db_datareader] ADD MEMBER [Trial]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [Trial]
GO
ALTER ROLE [db_owner] ADD MEMBER [edisdbadmin]
GO
ALTER ROLE [aspnet_Roles_ReportingAccess] ADD MEMBER [aspnet_Roles_FullAccess]
GO
ALTER ROLE [aspnet_Roles_BasicAccess] ADD MEMBER [aspnet_Roles_FullAccess]
GO
ALTER ROLE [aspnet_Profile_ReportingAccess] ADD MEMBER [aspnet_Profile_FullAccess]
GO
ALTER ROLE [aspnet_Profile_BasicAccess] ADD MEMBER [aspnet_Profile_FullAccess]
GO
ALTER ROLE [aspnet_Personalization_ReportingAccess] ADD MEMBER [aspnet_Personalization_FullAccess]
GO
ALTER ROLE [aspnet_Personalization_BasicAccess] ADD MEMBER [aspnet_Personalization_FullAccess]
GO
ALTER ROLE [aspnet_Membership_ReportingAccess] ADD MEMBER [aspnet_Membership_FullAccess]
GO
ALTER ROLE [aspnet_Membership_BasicAccess] ADD MEMBER [aspnet_Membership_FullAccess]
GO
/****** Object:  Schema [aspnet_Membership_BasicAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Membership_BasicAccess]
GO
/****** Object:  Schema [aspnet_Membership_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Membership_FullAccess]
GO
/****** Object:  Schema [aspnet_Membership_ReportingAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Membership_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Personalization_BasicAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Personalization_BasicAccess]
GO
/****** Object:  Schema [aspnet_Personalization_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Personalization_FullAccess]
GO
/****** Object:  Schema [aspnet_Personalization_ReportingAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Personalization_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Profile_BasicAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Profile_BasicAccess]
GO
/****** Object:  Schema [aspnet_Profile_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Profile_FullAccess]
GO
/****** Object:  Schema [aspnet_Profile_ReportingAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Profile_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Roles_BasicAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Roles_BasicAccess]
GO
/****** Object:  Schema [aspnet_Roles_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Roles_FullAccess]
GO
/****** Object:  Schema [aspnet_Roles_ReportingAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_Roles_ReportingAccess]
GO
/****** Object:  Schema [aspnet_WebEvent_FullAccess]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE SCHEMA [aspnet_WebEvent_FullAccess]
GO
/****** Object:  UserDefinedFunction [dbo].[CSVIntoTable]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		WACH
-- Create date: 01 Jul 2010
-- Description:	Parses a 2D array string into a table of values
-- =============================================
CREATE FUNCTION [dbo].[CSVIntoTable]
(
	@ColDelimiter CHAR,
    @RowDelimiter CHAR,
    @CSVString VARCHAR(MAX)
)
RETURNS @Elements TABLE
(
    ElementNumber INT IDENTITY(1,1),
    ElementValue VARCHAR(1000),
    RowNum INT,
    ColNum INT
)
AS
BEGIN

    
	-- Fill the table variable with the rows for your result set
	--2D Split with "Zero" Based Indices


    --===== Declare a variable to store the number of elements detected in each group
    -- of the input parameter
    DECLARE @GroupCount INT

    

    --===== Determine the number of elements in a group (assumes they're all the same)
    SELECT @GroupCount = 
        -- Find the length of the first group...
        LEN(SUBSTRING(@CSVString,1,CHARINDEX(@RowDelimiter,@CSVString)))
        -- ... subtract the length of the first group without any commas... 
      - LEN(REPLACE(SUBSTRING(@CSVString,1,CHARINDEX(@RowDelimiter,@CSVString)),@ColDelimiter,''))
        -- ... and add 1 because there is always 1 element more than commas.
      + 1

    --===== Add start and end commas to the Parameter and change all "group"
    -- delimiters to a comma so we can handle all the elements the same way.
    SET @CSVString = @ColDelimiter+REPLACE(@CSVString,@RowDelimiter,@ColDelimiter) +@ColDelimiter

    --===== Join the Tally table to the string at the character level and
    -- when we find a comma, insert what's between that comma and 
    -- the next comma into the Elements table
    INSERT INTO @Elements (ElementValue)
    SELECT SUBSTRING(@CSVString,N+1,CHARINDEX(@ColDelimiter,@CSVString,N+1)-N-1)
    FROM dbo.Tally
    WHERE N < LEN(@CSVString)
    AND SUBSTRING(@CSVString,N,1) = @ColDelimiter --Notice how we find the comma

    --===== Calculate and update the "row" and "column" indices
    UPDATE @Elements
    SET RowNum = (ElementNumber-1)/@GroupCount+1, --"Zero" based row
        ColNum = (ElementNumber-1)%@GroupCount+1  --"Zero" based col

	RETURN 
END


GO
/****** Object:  UserDefinedFunction [dbo].[GetDateDiff]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 25/02/2014
-- Description:	Returns the difference between 2 dates in terms of Years, Months and Days Parts
-- =============================================
CREATE FUNCTION [dbo].[GetDateDiff] 
(
	-- Add the parameters for the function here
	@StartDate date, 
	@EndDate date
)
RETURNS 
@ReturnTable TABLE 
(
	-- Add the column definitions for the TABLE variable here
	Years int, 
	Months int,
	Days int,
	TotalDays int
)
AS
BEGIN
	DECLARE @years_diff int
	DECLARE @months_diff int
	DECLARE @days_diff int

	select @years_diff = 
	case 
		when datepart(day, @StartDate) > datepart(day, @EndDate) then datediff(month, @StartDate, @EndDate) - 1 
		else datediff(month, @StartDate, @EndDate)
	end/ 12
	select @months_diff = 
	case 
		when datepart(day, @StartDate) > datepart(day, @EndDate) then datediff(month, @StartDate, @EndDate) - 1
		else datediff(month, @StartDate, @EndDate)
	end 
	select @days_diff = 
	case
		when datepart(day, @StartDate) = datepart(day, @EndDate) then 0
		when datepart(day, @StartDate) > datepart(day, @EndDate) then datediff(day, dateadd(month, @months_diff , @StartDate), @EndDate)
		else datediff(day, dateadd(month, @months_diff %12, @StartDate), @EndDate)
	end

	INSERT INTO @ReturnTable 
		(Years, Months, Days, TotalDays) 
		VALUES(@years_diff, @months_diff %12, @days_diff, DateDiff(day, @StartDate, @EndDate))
	RETURN 
END


GO
/****** Object:  UserDefinedFunction [dbo].[GetMaxAccountNo]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 05 May 2014
-- Description:	Returns the maximum accountNo currently used
-- =============================================
CREATE FUNCTION [dbo].[GetMaxAccountNo]
(
	
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @MaxAccountNo INT

	SELECT @MaxAccountNo = Max(AccountNo) FROM dbo.UserData

	-- Return the result of the function
	RETURN @MaxAccountNo 

END


GO
/****** Object:  UserDefinedFunction [dbo].[IsAccountIDExist]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 17/03/2014
-- Description:	Returns a true / false depending on whether there is an existing account id in the appropriate asset
-- Note:        This function will require an appropriate update whenever a new asset type is entered
-- =============================================
CREATE FUNCTION [dbo].[IsAccountIDExist] 
(
	@AccountID nvarchar(128),
	@AssetTypeID int	
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result bit
	DECLARE @ResultCount int 

	DECLARE @identifiers AS Table 
	(
		ID nvarchar(128)
	)

	INSERT INTO @identifiers (ID)
	SELECT CH.CashHoldingsID FROM  dbo.CashHoldings CH WHERE CH.AssetTypeID = @AssetTypeID AND CH.CashHoldingsID = @AccountID

	INSERT INTO @identifiers (ID)
	SELECT DP.DirectPropertyID FROM dbo.DirectProperties DP WHERE DP.AssetTypeID = @AssetTypeID AND DP.DirectPropertyID = @AccountID

	INSERT INTO @identifiers (ID)
	SELECT FH.FixedIncomeID FROM dbo.FixedIncomeHoldings FH WHERE FH.AssetTypeID = @AssetTypeID AND FH.FixedIncomeID = @AccountID

	INSERT INTO @identifiers (ID)
	SELECT L.LoanID FROM dbo.Loans L WHERE L.AssetTypeID = @AssetTypeID AND L.LoanID = @AccountID

	INSERT INTO @identifiers (ID)
	SELECT ML.MarginLoanID FROM dbo.MarginLoans ML WHERE ML.AssetTypeID = @AssetTypeID AND ML.MarginLoanID = @AccountID

	INSERT INTO @identifiers (ID)
	SELECT SH.StockHoldingID FROM dbo.StockHoldings SH WHERE SH.AssetTypeID = @AssetTypeID AND SH.StockHoldingID = @AccountID
		
	SELECT @ResultCount = COUNT(ID) FROM @identifiers WHERE ID IS NOT NULL 

	SELECT @Result = 
		CASE 
			WHEN @ResultCount = 1 THEN 1 
			WHEN @AssetTypeID IS NULL AND @AccountID IS NULL THEN 1 
			ELSE 0 
		END

	RETURN @Result

END


GO
/****** Object:  UserDefinedFunction [dbo].[WordsToTable]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[WordsToTable] ( @StringInput VARCHAR(max), @Delimiter NVARCHAR(1) )
RETURNS @OutputTable TABLE (Word VARCHAR(50))
AS
BEGIN

    DECLARE @value NVARCHAR(50)

	IF @StringInput IS NOT NULL
	BEGIN
		WHILE LEN(@StringInput) > 0
		BEGIN
			SET @value      = LEFT(@StringInput, 
									ISNULL(NULLIF(CHARINDEX(@Delimiter, @StringInput) - 1, -1),
									LEN(@StringInput)))
			SET @StringInput = SUBSTRING(@StringInput,
										 ISNULL(NULLIF(CHARINDEX(@Delimiter, @StringInput), 0),
										 LEN(@StringInput)) + 1, LEN(@StringInput))

			INSERT INTO @OutputTable(Word) VALUES ( @value )
		END
	END
    
    RETURN
END

GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressID] [nvarchar](128) NOT NULL,
	[AddressLine1] [varchar](200) NOT NULL,
	[AddressLine2] [varchar](200) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NOT NULL,
	[Postcode] [varchar](20) NOT NULL,
	[Country] [varchar](100) NOT NULL,
	[DateCreated] [date] NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Admin_Posts]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin_Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostHeading] [nvarchar](512) NULL,
	[PostAuthor] [nvarchar](256) NULL,
	[PostImageData] [image] NULL,
	[PostDate] [datetime] NULL,
	[PostContents] [nvarchar](max) NULL,
	[PostTypeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_CommissionsAndFees]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_CommissionsAndFees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[CommissionsAndFeesId] [int] NULL,
	[YesNoNA] [int] NULL,
	[CommissionDescription] [nvarchar](500) NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_CV]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_CV](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[CV] [image] NULL,
	[OriginalFileName] [nvarchar](256) NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_DealerGroupDetails]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_DealerGroupDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[DealerGroupName] [nvarchar](256) NULL,
	[AFSL] [nvarchar](16) NULL,
	[HasDerivativesLicense] [int] NULL,
	[IsAuthorisedRep] [int] NULL,
	[AuthorisedRepNumber] [nvarchar](32) NULL,
	[AddressLn1] [nvarchar](128) NULL,
	[AddressLn2] [nvarchar](128) NULL,
	[AddressLn3] [nvarchar](128) NULL,
	[Suburb] [nvarchar](64) NULL,
	[State] [nvarchar](64) NULL,
	[PostCode] [nvarchar](64) NULL,
	[Country] [nvarchar](64) NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_Description]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_Description](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[AdviserDescription] [nvarchar](1000) NULL,
	[LastUpdated] [datetime] NULL,
	[IsApproved] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_Details]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_Details](
	[UserId] [nvarchar](128) NULL,
	[AccountNumber] [int] IDENTITY(10000,1) NOT NULL,
	[CurrentTitle] [nvarchar](64) NULL,
	[CompanyName] [nvarchar](64) NULL,
	[ABNACN] [nvarchar](16) NULL,
	[ExperienceStartDate] [smalldatetime] NULL,
	[Title] [nvarchar](16) NULL,
	[FirstName] [nvarchar](128) NULL,
	[MiddleName] [nvarchar](128) NULL,
	[LastName] [nvarchar](128) NULL,
	[Gender] [nvarchar](32) NULL,
	[Phone] [nvarchar](32) NULL,
	[Fax] [nvarchar](32) NULL,
	[Mobile] [nvarchar](32) NULL,
	[AddressLn1] [nvarchar](128) NULL,
	[AddressLn2] [nvarchar](128) NULL,
	[AddressLn3] [nvarchar](128) NULL,
	[Suburb] [nvarchar](64) NULL,
	[State] [nvarchar](64) NULL,
	[PostCode] [nvarchar](64) NULL,
	[Country] [nvarchar](64) NULL,
	[Lng] [float] NULL,
	[Lat] [float] NULL,
	[LastUpdated] [datetime] NULL,
	[VerifiedId] [int] NULL,
	[CreatedOn] [datetime2](7) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_Education]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_Education](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[EducationLevels] [nvarchar](64) NULL,
	[Institution] [nvarchar](128) NULL,
	[CourseName] [nvarchar](128) NULL,
	[CurrentlyStudying] [int] NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_ManagementDetails]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_ManagementDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[TotalAssets] [float] NULL,
	[TotalManagedInvestments] [float] NULL,
	[TotalDirectAustralianEquities] [float] NULL,
	[TotalDirectInternational] [float] NULL,
	[TotalFixedInterest] [float] NULL,
	[TotalLendingBook] [float] NULL,
	[ApproxClientNumbersId] [int] NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_ProfessionalAssociations]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_ProfessionalAssociations](
	[UserId] [nvarchar](128) NULL,
	[ProfessionalAssociationsId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_ProfessionalType]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_ProfessionalType](
	[UserId] [nvarchar](128) NULL,
	[ProfessionTypeId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_ProfilePictures]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_ProfilePictures](
	[UserId] [nvarchar](128) NULL,
	[ImageData] [image] NULL,
	[UserTypeId] [nvarchar](128) NULL,
	[PictureApprovalStatusId] [int] NULL,
	[LastUpdated] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_Ratings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_Ratings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[UserId_Client] [nvarchar](128) NULL,
	[Feedback_Q1] [float] NULL,
	[Feedback_Q1_Comments] [nvarchar](500) NULL,
	[Feedback_Q2] [float] NULL,
	[Feedback_Q2_Comments] [nvarchar](500) NULL,
	[Feedback_Q3] [float] NULL,
	[Feedback_Q3_Comments] [nvarchar](500) NULL,
	[Feedback_Q4] [float] NULL,
	[Feedback_Q4_Comments] [nvarchar](500) NULL,
	[Feedback_Q5] [float] NULL,
	[Feedback_Q5_Comments] [nvarchar](500) NULL,
	[AdditionalComments] [nvarchar](500) NULL,
	[IsSuccessReferral] [nvarchar](5) NULL,
	[LastUpdate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_ServicesProviding]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Adviser_ServicesProviding](
	[UserId] [nvarchar](128) NULL,
	[SubServiceId] [int] NULL,
	[SubServiceProvideYesNo] [varchar](5) NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Adviser_Subscriptions]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_Subscriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[PlanTypeId] [int] NULL,
	[SubscriptionStatusId] [int] NULL,
	[AmountPaid] [float] NULL,
	[StartDate] [smalldatetime] NULL,
	[EndDate] [smalldatetime] NULL,
	[TransactionId] [nvarchar](512) NULL,
	[TransactionDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_TargetClients]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_TargetClients](
	[UserId] [nvarchar](128) NULL,
	[AnnualIncomeLevelsId] [int] NULL,
	[InvestibleAssetsLevelId] [int] NULL,
	[TotalAssetsLevelId] [int] NULL,
	[LastUpdated] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adviser_Verified_Status]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adviser_Verified_Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[VerifiedStatusId] [int] NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AdviserCapabilityLinks]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdviserCapabilityLinks](
	[AdviserCapabilityID] [int] IDENTITY(1,1) NOT NULL,
	[AdviserID] [nvarchar](128) NOT NULL,
	[CapabilityTypeID] [int] NOT NULL,
	[Capable] [bit] NOT NULL,
 CONSTRAINT [PK_AdviserCapabililtyLinks] PRIMARY KEY CLUSTERED 
(
	[AdviserCapabilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AdviserCapabilityUnique] UNIQUE NONCLUSTERED 
(
	[AdviserID] ASC,
	[CapabilityTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Advisers]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advisers](
	[AdviserID] [nvarchar](128) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[Overview] [varchar](max) NULL,
	[Photo] [varchar](5) NULL,
	[ExperienceStart] [date] NULL,
	[RepresentativeNo] [varchar](50) NULL,
	[AFSL] [varchar](50) NULL,
	[DealerGroupID] [nvarchar](128) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_Advisers] PRIMARY KEY CLUSTERED 
(
	[AdviserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AnnualIncomeLevels]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnnualIncomeLevels](
	[AnnualIncomeLevelsId] [int] NOT NULL,
	[AnnualIncomeLevels] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[AnnualIncomeLevelsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppointmentID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NULL,
	[AdviserID] [nvarchar](128) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Details] [varchar](max) NOT NULL,
	[AppointmentTime] [datetime] NOT NULL,
	[AppointmentType] [varchar](50) NULL,
	[DurationHours] [decimal](9, 2) NOT NULL,
	[Comments] [varchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[AppointmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApproxClientNumbers]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApproxClientNumbers](
	[ApproxClientNumbersId] [int] NOT NULL,
	[ApproxClientNumbers] [nvarchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[ApproxClientNumbersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[ApplicationId] [nvarchar](128) NOT NULL DEFAULT (newid()),
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [aspnet_Applications_Index]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE CLUSTERED INDEX [aspnet_Applications_Index] ON [dbo].[aspnet_Applications]
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL DEFAULT (newid()),
	[UserName] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[MobileAlias] [nvarchar](16) NULL DEFAULT (NULL),
	[IsAnonymous] [bit] NOT NULL DEFAULT ((0)),
	[LastActivityDate] [datetime] NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [aspnet_Users_Index]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE UNIQUE CLUSTERED INDEX [aspnet_Users_Index] ON [dbo].[aspnet_Users]
(
	[ApplicationId] ASC,
	[LoweredUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AssetTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AssetTypes](
	[AssetTypeID] [int] IDENTITY(1,1) NOT NULL,
	[AssetType] [varchar](100) NOT NULL,
 CONSTRAINT [PK_AssetTypes] PRIMARY KEY CLUSTERED 
(
	[AssetTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Attachments]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attachments](
	[AttachmentID] [nvarchar](128) NOT NULL,
	[NoteID] [nvarchar](128) NULL,
	[Path] [varchar](max) NULL,
	[Title] [varchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[Comments] [varchar](200) NULL,
	[Data] [varbinary](max) NULL,
	[AttachmentType] [varchar](100) NULL,
 CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED 
(
	[AttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AttitudeToCapitalLoss]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttitudeToCapitalLoss](
	[AttitudeToCapitalLossId] [int] NOT NULL,
	[AttitudeToCapitalLoss] [nvarchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[AttitudeToCapitalLossId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AttitudeToRisk]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttitudeToRisk](
	[AttitudeToRiskId] [int] NOT NULL,
	[AttitudeToRisk] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[AttitudeToRiskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AustEquityCurrentScores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AustEquityCurrentScores](
	[AustEquityCurrentScoreID] [nvarchar](128) NOT NULL,
	[SettingName] [varchar](50) NULL,
	[StockID] [nvarchar](128) NULL,
	[IsSetting] [bit] NOT NULL,
	[EPSGrowth] [decimal](9, 2) NOT NULL,
	[MorningstarRecommendation] [varchar](50) NOT NULL,
	[DIVYield] [decimal](9, 2) NOT NULL,
	[Frank] [decimal](9, 2) NOT NULL,
	[ROA] [decimal](9, 2) NOT NULL,
	[ROE] [decimal](9, 2) NOT NULL,
	[IntCover] [decimal](9, 2) NOT NULL,
	[DebtEquity] [decimal](9, 2) NOT NULL,
	[PE] [decimal](9, 2) NOT NULL,
	[IntrinsicValueVariation] [decimal](9, 2) NOT NULL,
	[ScoreRanking] [int] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_AustEquityCurrentScores] PRIMARY KEY CLUSTERED 
(
	[AustEquityCurrentScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AustEquityHistoricalScores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AustEquityHistoricalScores](
	[AustHistoricalScoreID] [nvarchar](128) NOT NULL,
	[SettingName] [varchar](50) NULL,
	[StockID] [nvarchar](128) NULL,
	[IsSetting] [bit] NOT NULL,
	[EPSGrowth] [decimal](9, 2) NOT NULL,
	[MarketCapitalisation] [bigint] NOT NULL,
	[DIVYield] [decimal](9, 2) NOT NULL,
	[Frank] [decimal](9, 2) NOT NULL,
	[ROA] [decimal](9, 2) NOT NULL,
	[ROE] [decimal](9, 2) NOT NULL,
	[IntCover] [decimal](9, 2) NOT NULL,
	[DebtEquity] [decimal](9, 2) NOT NULL,
	[PE] [decimal](9, 2) NOT NULL,
	[Beta5Year] [decimal](9, 2) NOT NULL,
	[ScoreRanking] [int] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_AustEquityHistoricalScores] PRIMARY KEY CLUSTERED 
(
	[AustHistoricalScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Beneficiaries]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Beneficiaries](
	[BeneficiaryID] [nvarchar](128) NOT NULL,
	[FirstName] [varchar](200) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Relationship] [varchar](100) NOT NULL,
	[Address1] [varchar](200) NULL,
	[Address2] [varchar](200) NULL,
	[Address3] [varchar](200) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[PostCode] [varchar](10) NULL,
	[Phone] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[BeneficiaryPercentage] [float] NOT NULL,
	[PolicyID] [nvarchar](128) NULL,
	[DateCreated] [date] NOT NULL,
	[DateModified] [date] NOT NULL,
 CONSTRAINT [PK_Beneficiaries] PRIMARY KEY CLUSTERED 
(
	[BeneficiaryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Bookmarks]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bookmarks](
	[BookmarkID] [nvarchar](128) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[BookmarkName] [varchar](100) NOT NULL,
	[BookmarkLink] [varchar](100) NOT NULL,
	[Comments] [varchar](500) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_Bookmarks] PRIMARY KEY CLUSTERED 
(
	[BookmarkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CapabilityGroups]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CapabilityGroups](
	[CapabilityGroupID] [int] IDENTITY(1,1) NOT NULL,
	[CapabilityGroup] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CapabilityGroups] PRIMARY KEY CLUSTERED 
(
	[CapabilityGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CapabilityGroups] UNIQUE NONCLUSTERED 
(
	[CapabilityGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CapabilityTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CapabilityTypes](
	[CapabilityTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CapabilityGroupID] [int] NOT NULL,
	[CapabilityType] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CapabilityTypes] PRIMARY KEY CLUSTERED 
(
	[CapabilityTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CapabilityTypes] UNIQUE NONCLUSTERED 
(
	[CapabilityGroupID] ASC,
	[CapabilityType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CashHoldings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CashHoldings](
	[CashHoldingsID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[AssetTypeID] [int] NOT NULL,
	[InterestRate] [decimal](9, 2) NULL,
	[Frequency] [decimal](9, 2) NULL,
	[NextPayment] [date] NULL,
	[DebitInterest] [decimal](9, 2) NULL,
	[DateOpened] [date] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[StatementMethod] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CashHoldings] PRIMARY KEY CLUSTERED 
(
	[CashHoldingsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CashHoldings] UNIQUE NONCLUSTERED 
(
	[CashHoldingsID] ASC,
	[AssetTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Client_AdvisoryNeeds]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client_AdvisoryNeeds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[ReasonForSeekingAdvice] [nvarchar](1000) NULL,
	[PersonalFinancialGoals] [nvarchar](1000) NULL,
	[ExpectationsFromAdviser] [nvarchar](1000) NULL,
	[AdditionalComments] [nvarchar](1000) NULL,
	[CommunicationMethodId] [int] NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client_Details]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client_Details](
	[UserId] [nvarchar](128) NOT NULL,
	[DateOfBirth] [smalldatetime] NULL,
	[Title] [nvarchar](16) NULL,
	[FirstName] [nvarchar](128) NULL,
	[MiddleName] [nvarchar](128) NULL,
	[LastName] [nvarchar](128) NULL,
	[Gender] [nvarchar](32) NULL,
	[Phone] [nvarchar](32) NULL,
	[Fax] [nvarchar](32) NULL,
	[Mobile] [nvarchar](32) NULL,
	[AddressLn1] [nvarchar](128) NULL,
	[AddressLn2] [nvarchar](128) NULL,
	[AddressLn3] [nvarchar](128) NULL,
	[Suburb] [nvarchar](64) NULL,
	[State] [nvarchar](64) NULL,
	[PostCode] [nvarchar](64) NULL,
	[Country] [nvarchar](64) NULL,
	[Lng] [float] NULL,
	[Lat] [float] NULL,
	[LastUpdated] [datetime] NULL,
	[CreatedOn] [datetime2](7) NULL,
	[ClientGroupId] [nchar](128) NULL,
 CONSTRAINT [PK_Client_Details] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client_FinancialPosition]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client_FinancialPosition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[TimeFrameForAdviceId] [int] NULL,
	[EmploymentStatusId] [int] NULL,
	[TotalAnnualIncome] [float] NULL,
	[TotalAssets] [float] NULL,
	[TotalLiabilities] [float] NULL,
	[TotalEquity] [float] NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client_InvestedProducts]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Client_InvestedProducts](
	[UserId] [nvarchar](128) NULL,
	[InvestedProductsId] [int] NULL,
	[InvestedYesNo] [varchar](5) NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Client_InvestmentProfile]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client_InvestmentProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[InvestableAssets] [float] NULL,
	[TimeHorizonId] [int] NULL,
	[ReturnsExpectation] [float] NULL,
	[InvestmentKnowledgeId] [int] NULL,
	[AttitudeToRiskId] [int] NULL,
	[AttitudeToCapitalLossId] [int] NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client_Ratings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client_Ratings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[UserId_Adviser] [nvarchar](128) NULL,
	[Feedback_Q1] [float] NULL,
	[Feedback_Q1_Comments] [nvarchar](500) NULL,
	[Feedback_Q2] [float] NULL,
	[Feedback_Q2_Comments] [nvarchar](500) NULL,
	[Feedback_Q3] [float] NULL,
	[Feedback_Q3_Comments] [nvarchar](500) NULL,
	[Feedback_Q4] [float] NULL,
	[Feedback_Q4_Comments] [nvarchar](500) NULL,
	[Feedback_Q5] [float] NULL,
	[Feedback_Q5_Comments] [nvarchar](500) NULL,
	[AdditionalComments] [nvarchar](500) NULL,
	[IsSuccessReferral] [nvarchar](5) NULL,
	[LastUpdate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client_ServicesSeeking]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Client_ServicesSeeking](
	[UserId] [nvarchar](128) NULL,
	[SubServiceId] [int] NULL,
	[SubServiceSeekYesNo] [varchar](5) NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClientFileActions]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClientFileActions](
	[ClientFileActionID] [int] NOT NULL,
	[ClientFileAction] [varchar](500) NOT NULL,
 CONSTRAINT [PK_ClientFileActions] PRIMARY KEY CLUSTERED 
(
	[ClientFileActionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClientFileInformations]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClientFileInformations](
	[ClientFileID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[ClientActionID] [int] NOT NULL,
	[Response] [varchar](100) NOT NULL,
	[ResponseDate] [date] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_ClientFileInformations] PRIMARY KEY CLUSTERED 
(
	[ClientFileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClientGroups]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClientGroups](
	[ClientGroupID] [nvarchar](128) NOT NULL,
	[AdviserID] [nvarchar](128) NULL,
	[Alias] [varchar](100) NOT NULL,
	[AccountName] [varchar](100) NULL,
	[AccountNo] [int] NOT NULL,
	[RiskProfileOutcome] [int] NULL,
	[ServiceLevelID] [int] NULL,
	[MainClientID] [nvarchar](128) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_ClientGroups] PRIMARY KEY CLUSTERED 
(
	[ClientGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientID] [nvarchar](128) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[ClientGroupID] [nvarchar](128) NULL,
	[DesignationAccount] [varchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[LastContact] [date] NULL,
	[LastReview] [date] NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CommissionsAndFees]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommissionsAndFees](
	[CommissionsAndFeesId] [int] NOT NULL,
	[CommissionsAndFees] [nvarchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[CommissionsAndFeesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommunicationMethod]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommunicationMethod](
	[CommunicationMethodId] [int] NOT NULL,
	[CommunicationMethod] [nvarchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[CommunicationMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommunicationRequest]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommunicationRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Adviser_UserId] [nvarchar](128) NULL,
	[Client_UserId] [nvarchar](128) NULL,
	[AcceptOrDecline] [bit] NULL,
	[RequestDate] [datetime] NULL,
	[DeclineCommunicationReasonsId] [int] NULL,
	[ProcessDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ComplianceIncomeExpenses]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComplianceIncomeExpenses](
	[ComplianceIncomeExpensesID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[Centrelink] [decimal](9, 2) NULL,
	[ReceivedMaintenance] [decimal](9, 2) NULL,
	[SuperannunationPension] [decimal](9, 2) NULL,
	[OtherTaxableIncome] [decimal](9, 2) NULL,
	[OtherForeignIncome] [decimal](9, 2) NULL,
	[NonTaxableIncome] [decimal](9, 2) NULL,
	[FoodExpenses] [decimal](9, 2) NULL,
	[ClothingExpenses] [decimal](9, 2) NULL,
	[MedicalExpenses] [decimal](9, 2) NULL,
	[UtilitiesBills] [decimal](9, 2) NULL,
	[CommunicationsBills] [decimal](9, 2) NULL,
	[Furniture] [decimal](9, 2) NULL,
	[MortgageRental] [decimal](9, 2) NULL,
	[HomeInsurance] [decimal](9, 2) NULL,
	[VehicleInsurance] [decimal](9, 2) NULL,
	[Repairs] [decimal](9, 2) NULL,
	[CouncilRates] [decimal](9, 2) NULL,
	[VehicleRegistration] [decimal](9, 2) NULL,
	[Petrol] [decimal](9, 2) NULL,
	[VehicleLoans] [decimal](9, 2) NULL,
	[Entertainment] [decimal](9, 2) NULL,
	[HolidayTravel] [decimal](9, 2) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_ComplianceIncomeExpenses] PRIMARY KEY CLUSTERED 
(
	[ComplianceIncomeExpensesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Currencies]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Currencies](
	[CurrencyID] [int] NOT NULL,
	[Currency] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED 
(
	[CurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DailyPrices]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyPrices](
	[DailyPriceID] [nvarchar](128) NOT NULL,
	[StockID] [nvarchar](128) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Price] [decimal](9, 4) NOT NULL,
	[High] [decimal](9, 4) NOT NULL,
	[Low] [decimal](9, 4) NOT NULL,
	[High52WeekDate] [date] NOT NULL,
	[High52WeekPrice] [decimal](9, 4) NOT NULL,
	[Low52WeekDate] [date] NOT NULL,
	[Low52WeekPrice] [decimal](9, 4) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_DailyPrices] PRIMARY KEY CLUSTERED 
(
	[DailyPriceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DailyPrices] UNIQUE NONCLUSTERED 
(
	[Date] ASC,
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DealerGroups]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DealerGroups](
	[DealerGroupID] [nvarchar](128) NOT NULL,
	[DealerGroupName] [varchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_DealerGroups] PRIMARY KEY CLUSTERED 
(
	[DealerGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeclineCommunicationReasons]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeclineCommunicationReasons](
	[DeclineCommunicationReasonsId] [int] NOT NULL,
	[DeclineCommunicationReasons] [nvarchar](512) NULL,
PRIMARY KEY CLUSTERED 
(
	[DeclineCommunicationReasonsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dependants]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dependants](
	[DependantID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[Relationship] [varchar](100) NULL,
	[DateOfBirth] [date] NULL,
	[FinancialDependant] [bit] NOT NULL,
	[DependantIncome] [float] NULL,
	[DependantAsset] [float] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_Dependants] PRIMARY KEY CLUSTERED 
(
	[DependantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DirectProperties]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DirectProperties](
	[DirectPropertyID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[PurchasedPrice] [decimal](18, 2) NOT NULL,
	[StampDuty] [decimal](18, 2) NOT NULL,
	[PurchaseMiscFee] [decimal](18, 2) NOT NULL,
	[PurchaseLegalFee] [decimal](18, 2) NOT NULL,
	[PurchaseBankFee] [decimal](18, 2) NOT NULL,
	[PurchasedDate] [date] NOT NULL,
	[SalePrice] [decimal](18, 2) NULL,
	[AgentCommission] [decimal](18, 2) NULL,
	[SaleMiscFee] [decimal](18, 2) NULL,
	[SaleLegalFee] [decimal](18, 2) NULL,
	[SaleBankFee] [decimal](18, 2) NULL,
	[SaleLoanRepayment] [decimal](18, 2) NULL,
	[SaleDate] [date] NULL,
	[State] [varchar](50) NOT NULL,
	[EstimatedValue] [decimal](18, 2) NOT NULL,
	[RentalIncome] [decimal](18, 2) NOT NULL,
	[OwnershipLength] [int] NOT NULL,
	[Country] [varchar](50) NULL,
	[Region] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[OutstandingLoan] [decimal](18, 2) NULL,
	[YearsOnLoan] [decimal](9, 2) NULL,
	[LoanRateType] [varchar](50) NULL,
	[FixedTermEndDate] [date] NULL,
	[FixedRate] [decimal](9, 3) NULL,
	[FixedLoanPercent] [decimal](9, 3) NULL,
	[InterestRepayment] [decimal](9, 3) NULL,
	[VariableRate] [decimal](9, 3) NULL,
	[AgentName] [varchar](100) NULL,
	[AgentCompany] [varchar](100) NULL,
	[AgentAddress] [varchar](200) NULL,
	[AgentPhone] [varchar](50) NULL,
	[AgentFax] [varchar](50) NULL,
	[AgentEmail] [varchar](100) NULL,
	[AgentYearFrom] [date] NULL,
	[AgentYearTo] [date] NULL,
	[AssetTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[DirectPropertyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DistributionLists]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistributionLists](
	[DistributionListId] [int] IDENTITY(1,1) NOT NULL,
	[DistributionList] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[DistributionListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DividendHistories]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DividendHistories](
	[DividendHistoryID] [nvarchar](128) NOT NULL,
	[StockID] [nvarchar](128) NOT NULL,
	[Date] [date] NOT NULL,
	[CurrencyID] [int] NOT NULL,
	[Amount] [decimal](9, 4) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_DividendHistories] PRIMARY KEY CLUSTERED 
(
	[DividendHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EducationLevels]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationLevels](
	[EducationLevelsId] [int] NOT NULL,
	[EducationLevels] [nvarchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[EducationLevelsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EducationRecords]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EducationRecords](
	[EducationRecordID] [nvarchar](128) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[EducationLevel] [varchar](50) NULL,
	[YearStarted] [int] NULL,
	[YearCompleted] [int] NULL,
	[YearsSinceCompletion] [int] NULL,
	[EducationInstitution] [varchar](100) NULL,
	[CourseAttended] [varchar](100) NULL,
	[Description] [varchar](max) NULL,
	[Comments] [varchar](max) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_ClientEducations] PRIMARY KEY CLUSTERED 
(
	[EducationRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmploymentRecords]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmploymentRecords](
	[EmploymentRecordID] [nvarchar](128) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[Status] [varchar](50) NULL,
	[EmploymentType] [varchar](50) NULL,
	[EmployerName] [varchar](100) NOT NULL,
	[Position] [varchar](50) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[HoursPerWeek] [decimal](9, 2) NULL,
	[GrossSalary] [float] NULL,
	[Commissions] [float] NULL,
	[AfterTaxSalary] [float] NULL,
	[SuperContribution] [float] NULL,
	[AdditionalSuperContribution] [float] NULL,
	[MiscContribution] [float] NULL,
	[FBT] [float] NULL,
	[EmployeeSharePlan] [float] NULL,
	[SickLeave] [decimal](9, 2) NULL,
	[AnnualLeave] [decimal](9, 2) NULL,
	[LongServiceLeave] [decimal](9, 2) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_EmploymentRecords] PRIMARY KEY CLUSTERED 
(
	[EmploymentRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmploymentStatus]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentStatus](
	[EmploymentStatusId] [int] NOT NULL,
	[EmploymentStatus] [nvarchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmploymentStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Exchanges]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Exchanges](
	[ExchangeID] [int] NOT NULL,
	[ExchangeName] [varchar](200) NOT NULL,
	[ExchangeCountry] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Exchanges] PRIMARY KEY CLUSTERED 
(
	[ExchangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Expenses]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Expenses](
	[ExpenseID] [nvarchar](128) NOT NULL,
	[ExpenseTypeID] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Frequency] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentDate] [date] NULL,
	[Comments] [varchar](500) NULL,
	[AccountID] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[AssetTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED 
(
	[ExpenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExpenseTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExpenseTypes](
	[ExpenseTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ExpenseTypes] PRIMARY KEY CLUSTERED 
(
	[ExpenseTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FinancialHealthGrades]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FinancialHealthGrades](
	[FinancialHealthGradeID] [int] NOT NULL,
	[FinancialHealthGrade] [varchar](200) NOT NULL,
 CONSTRAINT [PK_FinancialHealthGrades] PRIMARY KEY CLUSTERED 
(
	[FinancialHealthGradeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FixedIncomeHoldings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FixedIncomeHoldings](
	[FixedIncomeID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[PurchaseDate] [date] NOT NULL,
	[IssuerName] [varchar](100) NOT NULL,
	[Ticker] [varchar](50) NOT NULL,
	[InterestPaid] [decimal](18, 2) NULL,
	[MaturityValue] [decimal](18, 2) NULL,
	[FixedTerm] [int] NOT NULL,
	[BondPrice] [decimal](18, 2) NULL,
	[TransactionFee] [decimal](18, 2) NULL,
	[MinimumFee] [decimal](18, 2) NULL,
	[MaximumFee] [decimal](18, 2) NULL,
	[AccountNo] [varchar](50) NULL,
	[AccountUserID] [varchar](50) NULL,
	[ClearingSponsor] [varchar](50) NULL,
	[HIN] [varchar](50) NULL,
	[AssetTypeID] [int] NOT NULL,
	[NextPaymentDate] [date] NULL,
	[NextPaymentAmount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_FixedIncomeHoldings] PRIMARY KEY CLUSTERED 
(
	[FixedIncomeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FundStocks]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FundStocks](
	[FundStockID] [nvarchar](128) NOT NULL,
	[InvestmentStrategyID] [int] NOT NULL,
	[PortfolioCurrencyID] [int] NOT NULL,
	[PricingFrequency] [int] NOT NULL,
	[NAVDailyBaseFrequency] [int] NOT NULL,
	[NAVMoEnd] [int] NOT NULL,
	[FundStructureID] [int] NOT NULL,
	[FinancialHealthGradeID] [int] NOT NULL,
	[Wholesale] [bit] NOT NULL,
	[Institutional] [bit] NOT NULL,
	[GlobalCategoryGroupID] [int] NOT NULL,
	[PrimaryProspectusBenchmark] [varchar](100) NULL,
	[PrimaryProspectusBenchmarkID] [int] NULL,
	[FundSizeBaseCurrencyID] [int] NOT NULL,
	[FundSizeDate] [date] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_FundStocks] PRIMARY KEY CLUSTERED 
(
	[FundStockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FundStructures]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FundStructures](
	[FundStructureID] [int] NOT NULL,
	[FundStructure] [varchar](200) NOT NULL,
 CONSTRAINT [PK_FundStructures] PRIMARY KEY CLUSTERED 
(
	[FundStructureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GlobalCategories]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GlobalCategories](
	[GlobalCategoryID] [int] NOT NULL,
	[GlobalCategoryGroupID] [int] NOT NULL,
	[GlobalCategory] [varchar](200) NOT NULL,
 CONSTRAINT [PK_GlobalCategories] PRIMARY KEY CLUSTERED 
(
	[GlobalCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GlobalCategoryGroups]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GlobalCategoryGroups](
	[GlobalCategoryGroupID] [int] NOT NULL,
	[GlobalCategoryGroup] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GlobalCategoryGroups] PRIMARY KEY CLUSTERED 
(
	[GlobalCategoryGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Industries]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Industries](
	[IndustryID] [int] NOT NULL,
	[Industry] [varchar](200) NOT NULL,
	[SectorID] [int] NOT NULL,
 CONSTRAINT [PK_Industries] PRIMARY KEY CLUSTERED 
(
	[IndustryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InterEquityCurrentScores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InterEquityCurrentScores](
	[InterEquityCurrentScoreID] [nvarchar](128) NOT NULL,
	[SettingName] [varchar](50) NULL,
	[StockID] [nvarchar](128) NULL,
	[IsSetting] [bit] NOT NULL,
	[FiveYearTotalReturn] [decimal](9, 2) NOT NULL,
	[MorningstarRecommendation] [varchar](50) NOT NULL,
	[DIVYield] [decimal](9, 2) NOT NULL,
	[ROA] [decimal](9, 2) NOT NULL,
	[ROE] [decimal](9, 2) NOT NULL,
	[FinancialLeverage] [decimal](9, 2) NOT NULL,
	[YearRevenueGrowth] [decimal](9, 2) NOT NULL,
	[DebtEquityRatio] [decimal](9, 2) NOT NULL,
	[CreditRating] [varchar](50) NOT NULL,
	[ValueVariation] [decimal](9, 2) NOT NULL,
	[ScoreRanking] [int] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_InterEquityCurrentScores] PRIMARY KEY CLUSTERED 
(
	[InterEquityCurrentScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InterEquityHistoricalScores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InterEquityHistoricalScores](
	[InterEquityHistoricalScoreID] [nvarchar](128) NOT NULL,
	[SettingName] [varchar](50) NULL,
	[StockID] [nvarchar](128) NULL,
	[IsSetting] [bit] NOT NULL,
	[OneYearReturn] [decimal](9, 2) NOT NULL,
	[MarketCapitalisation] [bigint] NULL,
	[DIVYield] [decimal](9, 2) NOT NULL,
	[ROA] [decimal](9, 2) NOT NULL,
	[ROE] [decimal](9, 2) NOT NULL,
	[QuickRatio] [decimal](9, 2) NOT NULL,
	[CurrentRatio] [decimal](9, 2) NOT NULL,
	[DebtEquityRatio] [decimal](9, 2) NOT NULL,
	[PERatio] [decimal](9, 2) NOT NULL,
	[Beta5Year] [decimal](9, 2) NOT NULL,
	[ScoreRanking] [int] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_InterEquityHistoricalScores] PRIMARY KEY CLUSTERED 
(
	[InterEquityHistoricalScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InvestedProducts]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvestedProducts](
	[InvestedProductsId] [int] NOT NULL,
	[InvestedProductsName] [nvarchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[InvestedProductsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvestibleAssetsLevel]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvestibleAssetsLevel](
	[InvestibleAssetsLevelId] [int] NOT NULL,
	[InvestibleAssetsLevel] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[InvestibleAssetsLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvestmentKnowledge]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvestmentKnowledge](
	[InvestmentKnowledgeId] [int] NOT NULL,
	[InvestmentKnowledge] [nvarchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[InvestmentKnowledgeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvestmentTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InvestmentTypes](
	[InvestmentTypeID] [int] NOT NULL,
	[InvestmentType] [varchar](200) NOT NULL,
 CONSTRAINT [PK_InvestmentTypes] PRIMARY KEY CLUSTERED 
(
	[InvestmentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IPOs]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IPOs](
	[IPOID] [nvarchar](128) NOT NULL,
	[IssuerName] [varchar](100) NULL,
	[InstrumentCode] [varchar](100) NULL,
	[OfferSize] [int] NULL,
	[PricePerUnit] [decimal](18, 2) NULL,
	[Increments] [int] NULL,
	[InvestmentMin] [int] NULL,
	[InvestmentMax] [int] NULL,
	[ForecastRatio] [decimal](18, 2) NULL,
	[DistributionFreq] [int] NULL,
	[ForecastDistribution] [decimal](18, 2) NULL,
	[ForecastDividend] [decimal](18, 2) NULL,
	[ForecastFranking] [decimal](18, 2) NULL,
	[OfferType] [varchar](50) NULL,
	[LodgementDate] [date] NULL,
	[BookbuildDate] [date] NULL,
	[OpeningDate] [date] NULL,
	[GeneralClosingDate] [date] NULL,
	[BrokerClosingDate] [date] NULL,
	[IssueDate] [date] NULL,
	[SettlementTradeDate] [date] NULL,
	[HoldingDespatchDate] [date] NULL,
	[NormalTradeDate] [date] NULL,
	[RecordFirstPayDate] [date] NULL,
	[FirstInterestDate] [date] NULL,
	[FirstRedemptionDate] [date] NULL,
	[MaturityDate] [date] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_IPOs] PRIMARY KEY CLUSTERED 
(
	[IPOID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Loans]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Loans](
	[LoanID] [nvarchar](128) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[LoanType] [varchar](100) NOT NULL,
	[DataFeed] [varchar](50) NOT NULL,
	[InterestRate] [decimal](9, 2) NOT NULL,
	[PaymentFrequency] [int] NOT NULL,
	[PaymentAmount] [decimal](9, 2) NOT NULL,
	[LoanTerm] [int] NOT NULL,
	[CreditLimit] [int] NOT NULL,
	[AccountBalance] [decimal](9, 2) NOT NULL,
	[LoanStart] [date] NOT NULL,
	[AccountNo] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[AssetTypeID] [int] NOT NULL,
	[LoanProvider] [varchar](100) NOT NULL,
	[LastPaymentDate] [date] NOT NULL,
	[FinancialInstitution] [varchar](50) NOT NULL,
	[RelationshipManager] [varchar](100) NOT NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Postcode] [varchar](10) NOT NULL,
	[RealEstateAddress] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Liabilities] PRIMARY KEY CLUSTERED 
(
	[LoanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ManagedFundsCurrentScores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ManagedFundsCurrentScores](
	[ManagedFundsCurrentScoreID] [nvarchar](128) NOT NULL,
	[SettingName] [varchar](50) NULL,
	[StockID] [nvarchar](128) NULL,
	[IsSetting] [bit] NOT NULL,
	[OneYearReturn] [decimal](9, 2) NOT NULL,
	[MorningstarRecommendation] [varchar](50) NOT NULL,
	[OneYearAlpha] [decimal](9, 2) NOT NULL,
	[OneYearBeta] [decimal](9, 2) NOT NULL,
	[OneYearInfoRatio] [decimal](9, 2) NOT NULL,
	[OneYearTrackError] [decimal](9, 2) NOT NULL,
	[OneYearSharpRatio] [decimal](9, 2) NOT NULL,
	[MaxManagementExpenseRatio] [decimal](9, 2) NOT NULL,
	[PerformanceFee] [decimal](9, 2) NOT NULL,
	[YearsSinceInception] [decimal](9, 2) NOT NULL,
	[ScoreRanking] [int] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_ManagedFundsCurrentScores] PRIMARY KEY CLUSTERED 
(
	[ManagedFundsCurrentScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ManagedFundsHistoricalScores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ManagedFundsHistoricalScores](
	[ManagedFundsHistoricalScoreID] [nvarchar](128) NOT NULL,
	[SettingName] [varchar](50) NULL,
	[StockID] [nvarchar](128) NULL,
	[IsSetting] [bit] NOT NULL,
	[FiveYearReturn] [decimal](9, 2) NOT NULL,
	[FiveYearAlpha] [decimal](9, 2) NOT NULL,
	[FiveYearBeta] [decimal](9, 2) NOT NULL,
	[FiveYearInfoRatio] [decimal](9, 2) NOT NULL,
	[FiveYearStdDev] [decimal](9, 2) NOT NULL,
	[FiveYearSkewRatio] [decimal](9, 2) NOT NULL,
	[FiveYearTrackError] [decimal](9, 2) NOT NULL,
	[FiveYearSharpRatio] [decimal](9, 2) NOT NULL,
	[GlobalCategory] [decimal](9, 2) NOT NULL,
	[FundSize] [int] NOT NULL,
	[ScoreRanking] [int] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_ManagedFundsHistoricalScores] PRIMARY KEY CLUSTERED 
(
	[ManagedFundsHistoricalScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MarginLoans]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MarginLoans](
	[MarginLoanID] [nvarchar](128) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[HIN] [varchar](50) NOT NULL,
	[LoanProvider] [varchar](50) NOT NULL,
	[VariableRateLoan] [varchar](50) NOT NULL,
	[VariableRateAmount] [decimal](9, 2) NOT NULL,
	[FixedRateLoan] [varchar](50) NOT NULL,
	[FixedRateAmount] [decimal](9, 2) NOT NULL,
	[UnsettledTransactions] [decimal](9, 2) NOT NULL,
	[TotalBalance] [decimal](9, 2) NOT NULL,
	[NetInterestYTD] [decimal](9, 2) NOT NULL,
	[MonthlyInterest] [decimal](9, 2) NOT NULL,
	[MinBrokerage] [decimal](9, 2) NOT NULL,
	[MaxBrokerage] [decimal](9, 2) NOT NULL,
	[TotalBrokerage] [decimal](9, 2) NOT NULL,
	[TotalGST] [decimal](9, 2) NOT NULL,
	[ManagementFee] [decimal](9, 2) NOT NULL,
	[AdvisersComission] [decimal](9, 2) NOT NULL,
	[AssetTypeID] [int] NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[LoanLimit] [decimal](9, 2) NOT NULL,
	[SpendingLimit] [decimal](9, 2) NOT NULL,
	[AvailableCash] [decimal](9, 2) NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_MarginLoans] PRIMARY KEY CLUSTERED 
(
	[MarginLoanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MorningStarCategories]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MorningStarCategories](
	[MorningStarCategoryID] [int] NOT NULL,
	[MorningStarCategory] [varchar](200) NOT NULL,
 CONSTRAINT [PK_MorningStarCategories] PRIMARY KEY CLUSTERED 
(
	[MorningStarCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MorningStarRatings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MorningStarRatings](
	[MorningStarRatingID] [nvarchar](128) NOT NULL,
	[StockID] [nvarchar](128) NOT NULL,
	[MorningStarCategoryID] [int] NOT NULL,
	[RatingOverall] [varchar](50) NOT NULL,
	[AnalystRating] [varchar](50) NOT NULL,
	[Rating3Yr] [varchar](50) NOT NULL,
	[Rating5Yr] [varchar](50) NOT NULL,
	[Rating10Yr] [varchar](50) NOT NULL,
	[RatingDate] [date] NOT NULL,
	[RiskOverall] [varchar](50) NOT NULL,
	[Risk3Yr] [varchar](50) NOT NULL,
	[Risk5Yr] [varchar](50) NOT NULL,
	[Risk10Yr] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_MorningStarRatings] PRIMARY KEY CLUSTERED 
(
	[MorningStarRatingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsletterServices]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterServices](
	[NewsletterServicesId] [int] NOT NULL,
	[NewsletterServices] [nvarchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[NewsletterServicesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NoteLinks]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteLinks](
	[NoteID1] [nvarchar](128) NOT NULL,
	[NoteID2] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_CorrespondenceReferences] PRIMARY KEY CLUSTERED 
(
	[NoteID1] ASC,
	[NoteID2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Notes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notes](
	[NoteID] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[AdviserID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[AssetClass] [varchar](100) NULL,
	[ProductClass] [varchar](100) NULL,
	[Product] [varchar](100) NULL,
	[Purpose] [varchar](100) NULL,
	[TimeSpent] [float] NULL,
	[NoteSerial] [varchar](100) NULL,
	[Subject] [varchar](200) NOT NULL,
	[Body] [varchar](max) NULL,
	[FollowupActions] [varchar](100) NULL,
	[DateDue] [date] NULL,
	[Status] [varchar](50) NULL,
	[FollowupDate] [date] NULL,
	[DateCompleted] [datetime] NULL,
	[Reminder] [bit] NULL,
	[ReminderDate] [datetime] NULL,
	[NoteTypesID] [int] NOT NULL,
	[IsAccepted] [bit] NULL,
	[IsDeclined] [bit] NULL,
	[AssetTypeID] [int] NULL,
	[AccountID] [nvarchar](128) NULL,
 CONSTRAINT [PK_Correspondence] PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NoteTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NoteTypes](
	[NoteTypeID] [int] IDENTITY(1,1) NOT NULL,
	[NoteType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CorrespondenceTypes] PRIMARY KEY CLUSTERED 
(
	[NoteTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PictureApprovalStatus]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PictureApprovalStatus](
	[PictureApprovalStatusId] [int] NULL,
	[PictureApprovalStatus] [nvarchar](32) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Policies]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Policies](
	[PolicyID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[InsuranceType] [varchar](100) NOT NULL,
	[PolicyType] [varchar](100) NOT NULL,
	[PolicyName] [varchar](100) NOT NULL,
	[PolicyNumber] [varchar](50) NULL,
	[AccountName] [varchar](100) NOT NULL,
	[AccountAddress1] [varchar](200) NULL,
	[AccountAddress2] [varchar](200) NULL,
	[AccountAddress3] [varchar](200) NULL,
	[AccountCity] [varchar](100) NULL,
	[AccountState] [varchar](100) NULL,
	[AccountPostCode] [varchar](100) NULL,
	[InceptionDate] [date] NULL,
	[LastRenewalDate] [date] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[DateCreated] [date] NULL,
	[DateModified] [date] NULL,
	[Commentary] [varchar](max) NULL,
	[Institution] [varchar](100) NULL,
	[RenewalDueDate] [date] NULL,
 CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED 
(
	[PolicyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PolicyDetails]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PolicyDetails](
	[PolicyDetailID] [nvarchar](128) NOT NULL,
	[PolicyID] [nvarchar](128) NOT NULL,
	[DetailType] [varchar](100) NOT NULL,
	[DetailName] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[Amount] [decimal](9, 2) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[DateCreated] [date] NOT NULL,
	[DateModified] [date] NOT NULL,
 CONSTRAINT [PK_PolicyDetails] PRIMARY KEY CLUSTERED 
(
	[PolicyDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductTypes](
	[ProductTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ProductType] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED 
(
	[ProductTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProfessionalAssociations]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfessionalAssociations](
	[ProfessionalAssociationsId] [int] NOT NULL,
	[ProfessionalAssociations] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProfessionalAssociationsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProfessionType]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfessionType](
	[ProfessionTypeId] [int] NOT NULL,
	[ProfessionType] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProfessionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profile_RejectReasons]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile_RejectReasons](
	[Id] [int] NOT NULL,
	[RejectReason] [nvarchar](512) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProfilePhoto_RejectReasons]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfilePhoto_RejectReasons](
	[Id] [int] NOT NULL,
	[RejectReason] [nvarchar](512) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProfileProductLinks]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileProductLinks](
	[RiskProfileID] [nvarchar](128) NOT NULL,
	[ProductTypeID] [int] NOT NULL,
	[ProfileProductLinkID] [int] IDENTITY(1,1) NOT NULL,
	[Selected] [bit] NULL,
 CONSTRAINT [ProfileProductLinkID] PRIMARY KEY CLUSTERED 
(
	[ProfileProductLinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [ProfileProductUnique] UNIQUE NONCLUSTERED 
(
	[RiskProfileID] ASC,
	[ProductTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Qualifications]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Qualifications](
	[QualificationID] [nvarchar](128) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[Qualification] [varchar](200) NOT NULL,
	[QualificationIndex] [int] NOT NULL,
	[QualificationTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Qualifications] PRIMARY KEY CLUSTERED 
(
	[QualificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QualificationTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QualificationTypes](
	[QualificationTypeID] [int] NOT NULL,
	[QualificationType] [varchar](100) NOT NULL,
 CONSTRAINT [PK_QualificationTypes] PRIMARY KEY CLUSTERED 
(
	[QualificationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ratios]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratios](
	[RatioID] [nvarchar](128) NOT NULL,
	[StockID] [nvarchar](128) NOT NULL,
	[EntryDate] [date] NOT NULL,
	[RatioTypeID] [int] NOT NULL,
	[Value] [float] NOT NULL,
 CONSTRAINT [PK_Ratios] PRIMARY KEY CLUSTERED 
(
	[RatioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RatioTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RatioTypes](
	[RatioTypeID] [int] NOT NULL,
	[RatioTypeName] [varchar](200) NOT NULL,
 CONSTRAINT [PK_RatioTypes] PRIMARY KEY CLUSTERED 
(
	[RatioTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Referrals]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Referrals](
	[ReferralID] [nvarchar](128) NOT NULL,
	[TempEDISAccountNo] [varchar](50) NULL,
	[Individual] [varchar](100) NOT NULL,
	[Company] [varchar](100) NULL,
	[Address1] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[City] [varchar](100) NULL,
	[Postcode] [varchar](10) NULL,
	[WorkPhone] [varchar](20) NULL,
	[MobileNo] [varchar](20) NULL,
	[EmailAddress] [varchar](50) NULL,
	[ExistingArrangement] [varchar](20) NOT NULL,
	[PaymentMade] [bit] NOT NULL,
	[PaymentAmount] [decimal](9, 2) NULL,
	[ReferralPurpose] [varchar](50) NULL,
	[SpecialInstructions] [varchar](50) NOT NULL,
	[AdviserID] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RiskProfiles]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RiskProfiles](
	[RiskProfileID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[ShortTermGoal1] [varchar](200) NULL,
	[ShortTermGoal2] [varchar](200) NULL,
	[ShortTermGoal3] [varchar](200) NULL,
	[MedTermGoal1] [varchar](200) NULL,
	[MedTermGoal2] [varchar](200) NULL,
	[MedTermGoal3] [varchar](200) NULL,
	[LongTermGoal1] [varchar](200) NULL,
	[LongTermGoal2] [varchar](200) NULL,
	[LongTermGoal3] [varchar](200) NULL,
	[Comments] [varchar](max) NULL,
	[RetirementAge] [int] NULL,
	[RetirementIncome] [float] NULL,
	[IncomeSource] [varchar](100) NULL,
	[InvestmentObjective1] [varchar](200) NULL,
	[InvestmentObjective2] [varchar](200) NULL,
	[InvestmentObjective3] [varchar](200) NULL,
	[InvestmentTimeHorizon] [int] NULL,
	[InvestmentKnowledge] [varchar](200) NULL,
	[RiskAttitude] [varchar](200) NULL,
	[CapitalLossAttitude] [varchar](200) NULL,
	[ShortTermTrading] [varchar](50) NULL,
	[ShortTermAssetPercent] [float] NULL,
	[ShortTermEquityPercent] [float] NULL,
	[InvestmentProfile] [varchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_RiskProfiles] PRIMARY KEY CLUSTERED 
(
	[RiskProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sectors]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sectors](
	[SectorID] [int] NOT NULL,
	[Sector] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Sectors] PRIMARY KEY CLUSTERED 
(
	[SectorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Securities]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Securities](
	[SecuritiesID] [nvarchar](128) NOT NULL,
	[MarginLoanID] [nvarchar](128) NOT NULL,
	[PurchaseDate] [date] NOT NULL,
	[Company] [varchar](50) NULL,
	[Ticker] [varchar](50) NULL,
	[Units] [int] NOT NULL,
	[PricePerUnit] [decimal](9, 2) NOT NULL,
	[Brokerage] [decimal](9, 2) NOT NULL,
	[MarketValue] [decimal](9, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_Securities] PRIMARY KEY CLUSTERED 
(
	[SecuritiesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ServiceLevelActions]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ServiceLevelActions](
	[ServiceLevelActionID] [nvarchar](128) NOT NULL,
	[ClientGroupID] [nvarchar](128) NOT NULL,
	[PersonalProfileUpdated] [varchar](10) NOT NULL,
	[PersonalProfileUpdateDate] [date] NOT NULL,
	[FinancialInfoProvided] [varchar](10) NOT NULL,
	[FinancialInfoProvidedDate] [date] NOT NULL,
	[LastContactCallMade] [varchar](10) NOT NULL,
	[LastContactCallMadeDate] [date] NOT NULL,
	[FeeDisclosureStatement] [varchar](10) NOT NULL,
	[FeeDisclosureStatementDate] [date] NOT NULL,
	[OngoingStatementSigned] [varchar](10) NOT NULL,
	[OngoingStatementSignedDate] [date] NOT NULL,
	[LastStatementOfAdviceDate] [date] NOT NULL,
	[NumStatementOfAdvice] [int] NOT NULL,
	[LastRecordOfAdviceDate] [date] NOT NULL,
	[NumRecordOfAdvice] [int] NOT NULL,
	[NumAnnualReviewMeeting] [int] NOT NULL,
	[NumPhoneContacts] [int] NOT NULL,
	[NumInvestmentReports] [int] NOT NULL,
	[NumAdviceCalls] [int] NOT NULL,
	[NumSeminars] [int] NOT NULL,
	[NumMonthlyBulletin] [int] NOT NULL,
	[NumWeeklyBulletin] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_ServiceLevelActions] PRIMARY KEY CLUSTERED 
(
	[ServiceLevelActionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ServiceLevels]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ServiceLevels](
	[ServiceLevelID] [int] NOT NULL,
	[ServiceLevelName] [varchar](50) NOT NULL,
	[ReviewsPerAnnum] [int] NOT NULL,
	[ReportsPerAnnum] [int] NOT NULL,
	[PhoneContactsPerAnnum] [int] NOT NULL,
	[StatementsPerAnnum] [int] NOT NULL,
	[RecordOfAdvicePerAnnum] [int] NOT NULL,
	[AdviceCallPerAnnum] [int] NOT NULL,
	[BulletinsPerAnnum] [int] NOT NULL,
	[AustralianEquityResearch] [bit] NOT NULL,
	[InternationalEquityResearch] [bit] NOT NULL,
	[ManagedInvestmentResearch] [bit] NOT NULL,
	[PropertyResearch] [bit] NOT NULL,
	[IPOs] [bit] NOT NULL,
	[OnlineAccess] [bit] NOT NULL,
	[SeminarsPerAnnum] [int] NOT NULL,
	[InternalAdminCostMin] [decimal](9, 2) NOT NULL,
	[InternalAdminCostMax] [decimal](9, 2) NOT NULL,
	[ExternalAdminCostMin] [decimal](9, 2) NOT NULL,
	[ExternalAdminCostMax] [decimal](9, 2) NOT NULL,
	[OngoingCostMin] [decimal](9, 2) NOT NULL,
	[OngoingCostMax] [decimal](9, 2) NOT NULL,
	[InitialAdviceCost] [decimal](9, 2) NOT NULL,
	[MinBrokerageFee] [decimal](9, 2) NOT NULL,
	[MaxBrokerageFee] [decimal](9, 2) NOT NULL,
	[RecordOfAdviceFee] [decimal](9, 2) NOT NULL,
	[ConsultingFeePerHour] [decimal](9, 2) NOT NULL,
	[AccountingFeePerHour] [decimal](9, 2) NOT NULL,
	[LegalFeesPerHour] [decimal](9, 2) NOT NULL,
	[AuditingFees] [decimal](9, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_ServiceLevels] PRIMARY KEY CLUSTERED 
(
	[ServiceLevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Services]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[ServiceId] [int] NOT NULL,
	[ServiceName] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Statements]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Statements](
	[StatementID] [nvarchar](128) NOT NULL,
	[AssetTypeID] [int] NOT NULL,
	[AccountID] [nvarchar](128) NOT NULL,
	[StatementDate] [date] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Balance] [decimal](9, 2) NOT NULL,
	[Credit] [decimal](9, 2) NOT NULL,
	[Debit] [decimal](9, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Amount] [decimal](9, 2) NOT NULL,
 CONSTRAINT [PK_Statements] PRIMARY KEY CLUSTERED 
(
	[StatementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StockDistributions]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockDistributions](
	[StockDistributionID] [nvarchar](128) NOT NULL,
	[StockHoldingID] [nvarchar](128) NOT NULL,
	[StockID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[DistributionDate] [date] NOT NULL,
	[DistributionType] [varchar](50) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Franking] [decimal](18, 2) NOT NULL,
	[Units] [int] NOT NULL,
	[ExDates] [date] NOT NULL,
	[BookDate] [date] NOT NULL,
	[PaymentDate] [date] NOT NULL,
 CONSTRAINT [PK_StockDistributions] PRIMARY KEY CLUSTERED 
(
	[StockDistributionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StockHoldings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockHoldings](
	[StockHoldingID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[StockID] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[Units] [int] NOT NULL,
	[LastTransactionID] [nvarchar](128) NOT NULL,
	[ServiceFee] [decimal](18, 2) NULL,
	[AssetTypeID] [int] NOT NULL,
	[HIN] [varchar](50) NULL,
 CONSTRAINT [PK_Holdings] PRIMARY KEY CLUSTERED 
(
	[StockHoldingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Holdings] UNIQUE NONCLUSTERED 
(
	[ClientID] ASC,
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stocks]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stocks](
	[StockID] [nvarchar](128) NOT NULL,
	[FundStockID] [nvarchar](128) NOT NULL,
	[Ticker] [varchar](max) NULL,
	[APIRCode] [varchar](10) NOT NULL,
	[SecID] [varchar](50) NOT NULL,
	[ISIN] [varchar](20) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[FirmName] [varchar](100) NULL,
	[BrandingName] [varchar](100) NULL,
	[ManagerName] [varchar](50) NULL,
	[ManagerTenureAVG] [float] NULL,
	[ManagerTenureLONG] [float] NULL,
	[ExchangeID] [int] NOT NULL,
	[BusinessCountry] [varchar](50) NOT NULL,
	[Domicile] [varchar](50) NOT NULL,
	[BaseCurrencyID] [int] NOT NULL,
	[SecurityType] [varchar](10) NOT NULL,
	[InvestmentTypeID] [int] NOT NULL,
	[InceptionDate] [date] NOT NULL,
	[IPODate] [date] NOT NULL,
	[DateMarketCap] [float] NOT NULL,
	[DividedDistributionFrequency] [float] NOT NULL,
	[DividedYTD] [decimal](9, 4) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Stocks] PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StockTransactions]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockTransactions](
	[StockTransactionID] [nvarchar](128) NOT NULL,
	[StockID] [nvarchar](128) NOT NULL,
	[ClientID] [nvarchar](128) NOT NULL,
	[AdviserID] [nvarchar](128) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[TransactionDate] [date] NOT NULL,
	[ContractNo] [varchar](50) NOT NULL,
	[Units] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[BrokerageFee] [decimal](18, 2) NOT NULL,
	[TransactionTypeID] [int] NOT NULL,
 CONSTRAINT [PK_StockTransactions] PRIMARY KEY CLUSTERED 
(
	[StockTransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Strategies]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Strategies](
	[StrategyID] [int] NOT NULL,
	[Strategy] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Strategies] PRIMARY KEY CLUSTERED 
(
	[StrategyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubscriptionPlanTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionPlanTypes](
	[PlanTypeId] [int] NOT NULL,
	[PlanType] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[PlanTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubscriptionStatus]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionStatus](
	[SubscriptionStatusId] [int] NOT NULL,
	[SubscriptionStatus] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[SubscriptionStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubServices]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubServices](
	[SubServiceId] [int] NOT NULL,
	[SubServiceName] [nvarchar](128) NULL,
	[ServiceId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SubServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tally]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tally](
	[N] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Index [PKC_Tally]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE UNIQUE CLUSTERED INDEX [PKC_Tally] ON [dbo].[Tally]
(
	[N] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeFrameForAdvice]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeFrameForAdvice](
	[TimeFrameForAdviceId] [int] NOT NULL,
	[TimeFrameForAdvice] [nvarchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[TimeFrameForAdviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TimeHorizon]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeHorizon](
	[TimeHorizonId] [int] NOT NULL,
	[TimeHorizon] [nvarchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[TimeHorizonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TotalAssetsLevel]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TotalAssetsLevel](
	[TotalAssetsLevelId] [int] NOT NULL,
	[TotalAssetsLevel] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[TotalAssetsLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrackAdviserViews]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackAdviserViews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ViewByTypeId] [int] NULL,
	[RetDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionID] [nvarchar](128) NOT NULL,
	[TransactionTypeID] [int] NOT NULL,
	[CostPerUnit] [decimal](9, 2) NOT NULL,
	[TransactionDate] [date] NOT NULL,
	[ContractNo] [varchar](100) NOT NULL,
	[BrokerageFee] [decimal](9, 2) NOT NULL,
	[UnitPrice] [decimal](9, 2) NOT NULL,
	[StampDuty] [decimal](9, 2) NOT NULL,
	[LegalFee] [decimal](9, 2) NOT NULL,
	[EstablishmentFee] [decimal](9, 2) NOT NULL,
	[AssetTypeID] [int] NOT NULL,
	[AccountID] [nvarchar](128) NOT NULL,
	[Units] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TransactionTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TransactionTypes](
	[TransactionTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionType] [varchar](200) NOT NULL,
 CONSTRAINT [PK_TransactionTypes] PRIMARY KEY CLUSTERED 
(
	[TransactionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_DistributionList]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_DistributionList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[DistributionListId] [int] NULL,
	[LastUpdate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_DistributionLists]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_DistributionLists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DistributionListId] [int] NULL,
	[UserId] [nvarchar](256) NULL,
	[RetDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_NewsletterServices]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_NewsletterServices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[NewsletterServicesId] [int] NULL,
	[IsSubscribed] [nvarchar](5) NULL,
	[LastUpdated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserData]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserData](
	[UserID] [nvarchar](128) NOT NULL,
	[Title] [varchar](100) NULL,
	[FirstName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NOT NULL,
	[Gender] [nvarchar](20) NULL,
	[PreferredName] [varchar](200) NULL,
	[DateOfBirth] [date] NULL,
	[MaritalStatus] [varchar](50) NULL,
	[MobilePhone] [varchar](20) NULL,
	[HomePhone] [varchar](20) NULL,
	[WorkPhone] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[WorkEmail] [varchar](100) NULL,
	[HomeEmail] [varchar](100) NULL,
	[Active] [bit] NULL,
	[CompanyName] [varchar](100) NULL,
	[Position] [varchar](100) NULL,
	[ABN] [varchar](50) NULL,
	[ACN] [varchar](50) NULL,
	[AddressLine1] [varchar](100) NULL,
	[AddressLine2] [varchar](100) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[Postcode] [varchar](10) NULL,
	[Country] [varchar](100) NULL,
	[PostalAddressLine1] [varchar](100) NULL,
	[PostalAddressLine2] [varchar](100) NULL,
	[PostalCity] [varchar](100) NULL,
	[PostalState] [varchar](100) NULL,
	[PostalPostcode] [varchar](10) NULL,
	[PostalCountry] [varchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_UserData] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VerifiedStatus]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VerifiedStatus](
	[VerifiedStatusId] [int] NOT NULL,
	[VerifiedStatus] [nvarchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[VerifiedStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebStats_NumberOfLogins]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebStats_NumberOfLogins](
	[Email] [nvarchar](256) NULL,
	[LoginDate] [datetime] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_WebStats_NumberOfLogins] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[show_client_file_compliance_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[show_client_file_compliance_scores]
as
select 
	u.userid,
	cf.clientID,
	case when cf.clientactionid = 1 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID01],
	case when cf.clientactionid = 2 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID02],
	case when cf.clientactionid = 3 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID03],
	case when cf.clientactionid = 4 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID04],
	case when cf.clientactionid = 5 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID05],
	case when cf.clientactionid = 6 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID06],
	case when cf.clientactionid = 7 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID07],
	case when cf.clientactionid = 8 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID08],
	case when cf.clientactionid = 9 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID09],
	case when cf.clientactionid = 10 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID10],
	case when cf.clientactionid = 11 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID11],
	case when cf.clientactionid = 12 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID12],
	case when cf.clientactionid = 13 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID13],
	case when cf.clientactionid = 14 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID14],
	case when cf.clientactionid = 15 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID15],
	case when cf.clientactionid = 16 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID16],
	case when cf.clientactionid = 17 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID17],
	case when cf.clientactionid = 18 and ((cf.response = 'NA') OR (cf.response = 'YES' AND cf.responsedate IS NOT NULL)) THEN 1.0 ELSE 0.0 END [ClientActionID18]

from dbo.aspnet_Users U 
inner join dbo.clients c on u.userid = c.userid
left join dbo.clientfileinformations cf on c.clientid = cf.clientid




GO
/****** Object:  View [dbo].[show_compliance_income_expenses_score]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[show_compliance_income_expenses_score] 
AS
SELECT
	U.Id,
	CI.ClientID,
	CI.ComplianceIncomeExpensesID,
	ISNULL(CASE 
		WHEN (CI.Centrelink IS NOT NULL) AND (CI.ReceivedMaintenance IS NOT NULL) AND (CI.SuperannunationPension IS NOT NULL) AND (CI.OtherTaxableIncome IS NOT NULL) 
		AND  (CI.OtherForeignIncome IS NOT NULL) AND (CI.NonTaxableIncome IS NOT NULL) THEN 2.0 ELSE 0.0 END, 0.0) [ComplianceIncomeScore],
	ISNULL(CASE WHEN (CI.FoodExpenses IS NOT NULL) AND (CI.ClothingExpenses IS NOT NULL) 
		AND (CI.MedicalExpenses IS NOT NULL) AND (CI.UtilitiesBills IS NOT NULL) AND (CI.CommunicationsBills IS NOT NULL) AND (CI.Furniture IS NOT NULL)
		AND (CI.MortgageRental IS NOT NULL) AND (CI.HomeInsurance IS NOT NULL) AND (CI.VehicleInsurance IS NOT NULL) AND (CI.Repairs IS NOT NULL) 
		AND (CI.CouncilRates IS NOT NULL) AND (CI.Petrol IS NOT NULL) AND (CI.VehicleLoans IS NOT NULL) AND (CI.Entertainment IS NOT NULL) 
		AND (CI.HolidayTravel IS NOT NULL) THEN 3.0 ELSE 0.0 END, 0.0) [ComplianceExpenseScore]
	
FROM dbo.ComplianceIncomeExpenses CI
LEFT JOIN dbo.Clients C ON C.ClientID = CI.ClientID
INNER JOIN dbo.AspNetUsers U ON U.Id = C.UserID 




GO
/****** Object:  View [dbo].[show_employment_details_compliance_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[show_employment_details_compliance_scores]
AS
SELECT 
	U.UserID,
	ISNULL(CASE WHEN (ER.Status IS NOT NULL AND ER.Status <> '') AND (ER.EmploymentType IS NOT NULL AND ER.EmploymentType <> '') THEN 2.0 ELSE 0.0 END, 0.0) [EmploymentStatus],
	ISNULL(CASE WHEN (ER.EmployerName IS NOT NULL AND ER.EmployerName <> '') AND (ER.Position IS NOT NULL AND ER.Position <> '') AND (ER.StartDate IS NOT NULL) AND (ER.HoursPerWeek IS NOT NULL) THEN 2.0 ELSE 0.0 END , 0.0) [EmploymentDetails],
	ISNULL(CASE WHEN (ER.GrossSalary IS NOT NULL) AND (ER.Commissions IS NOT NULL) AND (ER.AfterTaxSalary IS NOT NULL) AND (ER.SuperContribution IS NOT NULL) 
		AND (ER.AdditionalSuperContribution IS NOT NULL) AND (ER.MiscContribution IS NOT NULL) AND (ER.FBT IS NOT NULL) AND (ER.EmployeeSharePlan IS NOT NULL) THEN 3.0 ELSE 0.0 END , 0.0) [SalaryDetails]

FROM dbo.aspnet_Users U 
INNER JOIN dbo.Clients C ON U.UserID = C.UserID
LEFT JOIN dbo.EmploymentRecords ER ON C.UserID = ER.UserID 





GO
/****** Object:  View [dbo].[show_investment_profile_compliance_score]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[show_investment_profile_compliance_score]
AS
SELECT 
	U.Id,
	C.ClientID,
	RP.RiskProfileID,
	ISNULL(CASE WHEN IncomeSource <> '' THEN 1.0 ELSE 0.0 END , 0.0) [IncomeSource],
	ISNULL(CASE WHEN  InvestmentProfile <> '' THEN 1.0 ELSE 0.0 END,  0.0)[InvestmentProfile],
	ISNULL(CASE WHEN (InvestmentObjective1 <> '') AND (InvestmentObjective2 <> '') AND (InvestmentObjective3 <> '') THEN 2.0 ELSE 0.0 END , 0.0)[InvestmentObjective],
	ISNULL(CASE WHEN InvestmentTImeHorizon > 0 THEN 2.0 ELSE 0.0 END , 0.0) [InvestmentTimeHorizon],
	ISNULL(CASE WHEN InvestmentKnowledge <> '' THEN 1.0 ELSE 0.0 END , 0.0) [InvestmentKnowledge], 
	ISNULL(CASE WHEN RiskAttitude <> '' THEN 2.0 ELSE 0.0 END , 0.0) [RiskAttitude],
	ISNULL(CASE WHEN CapitalLossAttitude <> '' THEN 2.0 ELSE 0.0 END , 0.0)[CapitalLossAttitude],
	ISNULL(CASE WHEN (ShortTermTrading <> '' ) AND (ShortTermAssetPercent >= 0) AND (ShortTermEquityPercent >= 0) THEN 2.0 ELSE 0.0 END , 0.0 ) [ShortTermTrading],
	ISNULL(CASE WHEN PP.ProductTypeCount = PT.TotalProductTypes THEN 2.0 ELSE 0.0 END, 0.0) [ProductSelected]
	
FROM dbo.RiskProfiles RP 
LEFT JOIN dbo.Clients C ON C.ClientID = RP.ClientID 
INNER JOIN dbo.AspNetUsers U ON U.Id = C.UserID 
LEFT JOIN 
(SELECT
	RiskProfileID, 
	COUNT(DISTINCT ProductTypeID)[ProductTypeCount]

FROM dbo.ProfileProductLinks PP 
WHERE PP.Selected IS NOT NULL 
GROUP BY PP.RiskProfileID ) PP ON RP.RiskProfileID = PP.RiskProfileID,

(SELECT	
	COUNT(DISTINCT ProductTypeID) [TotalProductTypes]
FROM dbo.ProductTypes) PT 




GO
/****** Object:  View [dbo].[show_personal_details_compliance_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW  [dbo].[show_personal_details_compliance_scores]
AS 

SELECT
	UD.UserID,
	ISNULL(CASE WHEN (UD.Title IS NOT NULL AND UD.Title <> '') AND (UD.FirstName IS NOT NULL AND UD.FirstName <> '') THEN 0.5 ELSE 0.0 END, 0.0) [SaluationFirstName],
	ISNULL(CASE WHEN (UD.Gender IS NOT NULL AND UD.Gender <> '') AND (UD.LastName IS NOT NULL AND UD.LastName <> '') THEN 1.0 ELSE 0.0 END, 0.0) [LastNameGender],
	ISNULL(CASE WHEN (UD.DateOfBirth IS NOT NULL) THEN 1.0 ELSE 0.0 END, 0.0) [DateOfBirth],
	ISNULL(CASE WHEN (UD.MaritalStatus IS NOT NULL AND UD.MaritalStatus <> '') THEN 1.0 ELSE 0.0 END, 0.0) [MaritalStatus],
	ISNULL(CASE WHEN (UD.AddressLine1 IS NOT NULL AND UD.AddressLine1 <> '') AND (UD.City IS NOT NULL AND UD.City <> '') AND (UD.State IS NOT NULL AND UD.State <> '') AND (UD.Postcode IS NOT NULL AND UD.Postcode <> '') THEN 0.5 ELSE 0.0 END, 0.0) [AddressDetails],
	ISNULL(CASE WHEN (UD.HomePhone IS NOT NULL AND UD.HomePhone <> '') AND (UD.WorkPhone IS NOT NULL AND UD.WorkPhone <> '') AND (UD.MobilePhone IS NOT NULL AND UD.MobilePhone <> '') AND (UD.HomeEmail <> '' OR UD.WorkEmail <> '') THEN 1.0 ELSE 0.0 END , 0.0) [ContactDetails]
FROM dbo.UserData UD 
INNER JOIN dbo.Clients C ON UD.UserID = C.UserID 





GO
/****** Object:  View [dbo].[show_service_level_actions_compliance]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[show_service_level_actions_compliance]
AS
	SELECT 
		U.UserID,
		ISNULL(CASE WHEN (SLA.PersonalProfileUpdated = 'YES' AND (DATEDIFF(d, SLA.PersonalProfileUpdateDate , GetDate()) < 365) OR SLA.PersonalProfileUpdated = 'NA') THEN 3.0 ELSE 0.0 END , 0.0) [PersonProfileUpdated],
		ISNULL(CASE WHEN (SLA.FinancialInfoProvided = 'YES' AND (DATEDIFF(d, SLA.FinancialInfoProvidedDate, GetDate()) < 365) OR SLA.FinancialInfoProvided = 'NA') THEN 3.0 ELSE 0.0 END, 0.0) [FinancialInfoProvided],
		ISNULL(CASE WHEN (SLA.LastContactCallMade = 'YES' AND (DATEDIFF(m, SLA.LastContactCallMadeDate, GetDate()) < 6) OR SLA.LastContactCallMade = 'NA') THEN 1.0 ELSE 0.0 END, 0.0) [LastContactCall],
		ISNULL(CASE WHEN (SLA.FeeDisclosureStatement = 'YES' AND (DATEDIFF(d, SLA.FeeDisclosureStatementDate, GetDate()) < 365) OR SLA.FeeDisclosureStatement = 'NA') THEN 2.0 ELSE 0.0 END, 0.0) [FeeDisclosureStatement],
		ISNULL(CASE WHEN (SLA.OngoingStatementSigned = 'YES' AND (DATEDIFF(d, SLA.OngoingStatementSignedDate, GetDate()) < 730) OR SLA.OngoingStatementSigned = 'NA') THEN 2.0 ELSE 0.0 END, 0.0) [OngoingStatementSigned],
		ISNULL(CASE WHEN (SLA.LastStatementOfAdviceDate IS NOT NULL AND SLA.NumStatementOfAdvice >= SL.StatementsPerAnnum) THEN 1.0 ELSE 0.0 END, 0.0)[StatementsOfAdvice],
		ISNULL(CASE WHEN (SLA.LastRecordOfAdviceDate IS NOT NULL AND SLA.NumRecordOfAdvice >= SL.RecordOfAdvicePerAnnum) THEN 1.0 ELSE 0.0 END, 0.0) [RecordOfAdvice],
		ISNULL(CASE WHEN (SLA.NumAnnualReviewMeeting >= SL.ReviewsPerAnnum) THEN 1.0 ELSE 0.0 END, 0.0) [AnnualReviews],
		ISNULL(CASE WHEN (SLA.NumPhoneContacts >= SL.PhoneContactsPerAnnum) THEN 1.0 ELSE 0.0 END, 0.0) [PhoneContacts],
		ISNULL(CASE WHEN (SLA.NumInvestmentReports >= SL.ReportsPerAnnum) THEN 1.0 ELSE 0.0 END, 0.0) [InvestmentReports],
		ISNULL(CASE WHEN (SLA.NumAdviceCalls >= SL.AdviceCallPerAnnum) THEN 1.0 ELSE 0.0 END, 0.0) [AdviceCalls],
		ISNULL(CASE WHEN (SLA.NumSeminars >= SL.SeminarsPerAnnum) THEN 1.0 ELSE 0.0 END, 0.0) [Seminars],
		ISNULL(CASE WHEN (SLA.NumMonthlyBulletin >= 12 ) THEN 1.0 ELSE 0.0 END, 0.0)[MonthlyBulletins],
		ISNULL(CASE WHEN ((SLA.NumWeeklyBulletin >= SL.BulletinsPerAnnum AND SL.BulletinsPerAnnum > 12) OR (SL.BulletinsPerAnnum  <= 12)) THEN 1.0 ELSE 0.0 END, 0.0) [WeeklyBulletins]

	FROM dbo.aspnet_Users U 
	INNER JOIN dbo.Clients C ON U.UserID = C.UserID 
	INNER JOIN dbo.ClientGroups CG ON CG.ClientGroupID = C.ClientGroupID
	LEFT JOIN dbo.ServiceLevelActions SLA ON CG.ClientGroupID = SLA.ClientGroupID
	LEFT JOIN dbo.ServiceLevels SL ON CG.ServiceLevelID = SL.ServiceLevelID
	






GO
/****** Object:  View [dbo].[show_client_compliance_score_summary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[show_client_compliance_score_summary]
AS

select
	c.clientid,
	(cfcs.ClientActionID01 + cfcs.ClientActionID02 + cfcs.ClientActionID03 + cfcs.ClientActionID04 + cfcs.ClientActionID05 +
	cfcs.ClientActionID06 + cfcs.ClientActionID07 + cfcs.ClientActionID08 + cfcs.ClientActionID09 + cfcs.ClientActionID10 +
	cfcs.ClientActionID11 + cfcs.ClientActionID12 + cfcs.ClientActionID13 + cfcs.ClientActionID14 + cfcs.ClientActionID15 + 
	cfcs.ClientActionID16 + cfcs.ClientActionID17 + cfcs.ClientActionID18) [ClientFileComplianceScore],
	cies.ComplianceExpenseScore,
	cies.ComplianceIncomeScore,
	(edcs.EmploymentDetails + edcs.EmploymentStatus + edcs.SalaryDetails) [EmploymentDetailsScore],
	(ipcs.IncomeSource + ipcs.InvestmentProfile + ipcs.InvestmentObjective + ipcs.InvestmentTimeHorizon + 
	ipcs.InvestmentKnowledge + ipcs.RiskAttitude + ipcs.CapitalLossAttitude + ipcs.ShortTermTrading + 
	ipcs.ProductSelected) [InvestmentProfileComplianceScore],
	(pdcs.SaluationFirstName + pdcs.LastNameGender + pdcs.DateOfBirth +
	pdcs.MaritalStatus + pdcs.AddressDetails + pdcs.ContactDetails) [PersonalDetailsComplianceScore],
	(slac.PersonProfileUpdated + slac.FinancialInfoProvided + slac.LastContactCall + slac.FeeDisclosureStatement +
	slac.OngoingStatementSigned + slac.StatementsOfAdvice + slac.RecordOfAdvice + slac.AnnualReviews +
	slac.PhoneContacts + slac.InvestmentReports + slac.AdviceCalls + slac.Seminars + slac.MonthlyBulletins +
	slac.WeeklyBulletins) [ServiceLevelActionsComplianceScore]

from dbo.clients c 
inner join dbo.show_client_file_compliance_scores cfcs on c.clientid = cfcs.clientID
inner join dbo.show_compliance_income_expenses_score cies on c.ClientID = cies.ClientID
inner join dbo.show_employment_details_compliance_scores edcs on c.UserID = edcs.UserID
inner join dbo.show_investment_profile_compliance_score ipcs on c.clientid = ipcs.ClientID
inner join dbo.show_personal_details_compliance_scores pdcs on c.UserID = pdcs.UserID
inner join dbo.show_service_level_actions_compliance slac on c.UserID = slac.UserID




GO
/****** Object:  View [dbo].[client_birthdays]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[client_birthdays]
AS
SELECT 
	CG.AdviserID,
	C.ClientID,
	UD.DateOfBirth,
	CASE WHEN DatePart(year, UD.DateOfBirth) % 4 = 0 THEN FLOOR((DateDIFF(dd, UD.DateOfBirth, GetDate()) + 0.25) / 365.25) ELSE FLOOR(DateDIFF(dd, UD.DateOfBirth, GetDate()) / 365.25) END AS AGE,
	DateDiff(d, GetDate(), DateAdd(m, 1, GetDate())) DaysTillMonth,
	DateDiff(d, GetDate(), DateFromParts(DatePart(yyyy, GetDate()), DatePart(month, UD.DateOfBirth), DatePart(d, UD.DateOfBirth))) DaysTillBirthday
	
FROM  dbo.ClientGroups CG 
INNER JOIN dbo.Clients C ON CG.ClientGroupID = C.ClientGroupID 
INNER JOIN dbo.UserData UD ON  C.UserID = UD.UserID





GO
/****** Object:  UserDefinedFunction [dbo].[ClientsPendingBirthdays]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 11/04/2014
-- Description:	Returns a client count whose about to an "age" birthday in the next month
-- =============================================
CREATE FUNCTION [dbo].[ClientsPendingBirthdays] 
(	
	-- Add the parameters for the function here
	@Age int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT
		cb.AdviserID,
		Count(ClientID) NumberOfClients

	FROM dbo.client_birthdays cb

	WHERE cb.Age = @Age - 1 --age birthday -1 
	AND cb.DaysTillBirthday > 0 --Birthday yet to occurr
	AND cb.DaysTillBirthday <= DaysTillMonth -- Birthday within a month

	GROUP BY cb.AdviserID 
)


GO
/****** Object:  View [dbo].[clients_entering_retirement]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[clients_entering_retirement]
AS
select 
	cb.AdviserID,
	cb.ClientID,
	cb.Age,
	R.RetirementAge
FROM dbo.client_birthdays cb  
INNER JOIN dbo.RiskProfiles R ON cb.ClientID = R.ClientID 

WHERE 
R.RetirementAge - cb.Age < 1 



GO
/****** Object:  View [dbo].[show_aust_equity_current_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[show_aust_equity_current_scores]
AS
	SELECT 
		A.StockID,
		A.EPSGrowthScore,
		A.MorningstarRecommendationScore,
		A.DIVYieldScore,
		A.FrankScore,
		A.ROAScore,
		A.ROEScore,
		A.IntCoverScore,
		A.DebtEquityScore,
		A.PEScore,
		A.IntrinsicValueVariationScore,
		(A.EPSGrowthScore + A.MorningstarRecommendationScore + A.DIVYieldScore + 
		A.FrankScore + A.ROAScore +	A.ROEScore + A.IntCoverScore + 
		A.DebtEquityScore + A.PEScore +	A.IntrinsicValueVariationScore) [CurrentTotalScore]
	FROM 
	(SELECT 
		a0.StockID, 
		case when a0.EPSGrowth <= a5.EPSGrowth then a1.ScoreRanking
			when a0.EPSGrowth <= a4.EPSGrowth then a2.ScoreRanking
			when a0.EPSGrowth <= a3.EPSGrowth then a3.ScoreRanking
			when a0.EPSGrowth <= a2.EPSGrowth then a4.ScoreRanking
			when a0.EPSGrowth <= a1.EPSGrowth then a5.ScoreRanking
			else a1.ScoreRanking end [EPSGrowthScore] ,
		case when a0.MorningstarRecommendation = 'BUY' OR a0.MorningstarRecommendation = 'HIGHLY RECOMMENDED' then a1.ScoreRanking
			when a0.MorningstarRecommendation = 'ACCUMULATE' OR a0.MorningstarRecommendation = 'RECOMMENDED' then a2.ScoreRanking
			when a0.MorningstarRecommendation = 'HOLD' OR a0.MorningstarRecommendation = 'INVESTMENTGRADE' then a3.ScoreRanking
			when a0.MorningstarRecommendation = 'REDUCE'  then a4.ScoreRanking
			when a0.MorningstarRecommendation = 'SELL' OR a0.MorningstarRecommendation = 'AVOID' then a5.ScoreRanking
			else a5.ScoreRanking * 20 end [MorningstarRecommendationScore],
		case when a0.DIVYield >= a1.DIVYield then a1.ScoreRanking
			when a0.DIVYield >= a2.DIVYield then a2.ScoreRanking
			when a0.DIVYield >= a3.DIVYield then a3.ScoreRanking
			when a0.DIVYield >= a4.DIVYield then a4.ScoreRanking
			when a0.DIVYield >= a5.DIVYield then a5.ScoreRanking
			else a5.ScoreRanking end [DIVYieldScore],
		case when a0.Frank >= a1.Frank then a1.ScoreRanking
			when a0.Frank >= a2.Frank then a2.ScoreRanking 
			when a0.Frank >= a3.Frank then a3.ScoreRanking
			when a0.Frank >= a4.Frank then a4.ScoreRanking
			when a0.Frank >= a5.Frank then a5.ScoreRanking
			else a5.ScoreRanking end [FrankScore],
		case when a0.ROA >= a1.ROA then a1.ScoreRanking
			when a0.ROA >= a2.ROA then a2.ScoreRanking
			when a0.ROA >= a3.ROA then a3.ScoreRanking
			when a0.ROA >= a4.ROA then a4.ScoreRanking
			when a0.ROA >= a5.ROA then a5.ScoreRanking
			else a5.ScoreRanking end [ROAScore],
		case when a0.ROE >= a1.ROE then a1.ScoreRanking
			when a0.ROE >= a2.ROE then a2.ScoreRanking
			when a0.ROE >= a3.ROE then a3.ScoreRanking
			when a0.ROE >= a4.ROE then a4.ScoreRanking
			when a0.ROE >= a5.ROE then a5.ScoreRanking
			else a5.ScoreRanking end [ROEScore],
		case when a0.IntCover >= a1.IntCover then a1.ScoreRanking
			when a0.IntCover >= a2.IntCover then a2.ScoreRanking
			when a0.IntCover >= a3.IntCover then a3.ScoreRanking
			when a0.IntCover >= a4.IntCover then a4.ScoreRanking
			when a0.IntCover >= a5.IntCover then a5.ScoreRanking
			else a5.ScoreRanking end [IntCoverScore],
		case when a0.DebtEquity <= a1.DebtEquity then a1.ScoreRanking
			when a0.DebtEquity <= a2.DebtEquity then a2.ScoreRanking
			when a0.DebtEquity <= a3.DebtEquity then a3.ScoreRanking
			when a0.DebtEquity <= a4.DebtEquity then a4.ScoreRanking
			when a0.DebtEquity <= a5.DebtEquity then a5.ScoreRanking
			else a5.ScoreRanking end [DebtEquityScore],
		case when a0.PE <= a1.PE then a1.ScoreRanking
			when a0.PE <= a2.PE then a2.ScoreRanking
			when a0.PE <= a3.PE then a3.ScoreRanking
			when a0.PE <= a4.PE then a4.ScoreRanking
			when a0.PE <= a5.PE then a5.ScoreRanking
			else a5.ScoreRanking end [PEScore],
		case when a0.IntrinsicValueVariation >= a1.IntrinsicValueVariation then a1.ScoreRanking
			when a0.IntrinsicValueVariation >= a2.IntrinsicValueVariation then a2.ScoreRanking
			when a0.IntrinsicValueVariation >= a3.IntrinsicValueVariation then a3.ScoreRanking
			when a0.IntrinsicValueVariation >= a4.IntrinsicValueVariation then a4.ScoreRanking
			when a0.IntrinsicValueVariation >= a5.IntrinsicValueVariation then a5.ScoreRanking 
			else a5.ScoreRanking end [IntrinsicValueVariationScore]
		 
	FROM dbo.AustEquityCurrentScores a0,
		dbo.AustEquityCurrentScores a1, 
		dbo.AustEquityCurrentScores a2,
		dbo.AustEquityCurrentScores a3, 
		dbo.AustEquityCurrentScores a4,
		dbo.AustEquityCurrentScores a5

	WHERE a1.SettingName = 'Defensive'	AND a1.IsSetting = 1 
	AND a2.SettingName = 'Conservative' AND a2.IsSetting = 1
	AND a3.SettingName = 'Balance'		AND a3.IsSetting = 1 
	AND a4.SettingName = 'Assertive'	AND a4.IsSetting = 1
	AND a5.SettingName = 'Aggressive'	AND a5.IsSetting = 1) A




GO
/****** Object:  View [dbo].[show_aust_equity_historical_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[show_aust_equity_historical_scores]
AS
	SELECT 
		A.StockID,
		A.EPSGrowthScore,
		A.MarketCapitalisationScore,
		A.DIVYieldScore,
		A.FrankScore,
		A.ROAScore,
		A.ROEScore,
		A.IntCoverScore,
		A.DebtEquityScore,
		A.PEScore,
		A.Beta5YearScore,
		(A.EPSGrowthScore +	A.MarketCapitalisationScore + A.DIVYieldScore +
		A.FrankScore + A.ROAScore +	A.ROEScore + A.IntCoverScore +
		A.DebtEquityScore +	A.PEScore +	A.Beta5YearScore) [TotalHistoricalScore]
	FROM 	
	(SELECT 
		a0.StockID, 
		case when a0.EPSGrowth <= a5.EPSGrowth then a1.ScoreRanking
			when a0.EPSGrowth <= a4.EPSGrowth then a2.ScoreRanking
			when a0.EPSGrowth <= a3.EPSGrowth then a3.ScoreRanking
			when a0.EPSGrowth <= a2.EPSGrowth then a4.ScoreRanking
			when a0.EPSGrowth <= a1.EPSGrowth then a5.ScoreRanking
			else a1.ScoreRanking end [EPSGrowthScore] ,
		case when a0.MarketCapitalisation >= a1.MarketCapitalisation then a1.ScoreRanking
			when a0.MarketCapitalisation >= a2.MarketCapitalisation then a2.ScoreRanking
			when a0.MarketCapitalisation >= a3.MarketCapitalisation then a3.ScoreRanking
			when a0.MarketCapitalisation >= a4.MarketCapitalisation then a4.ScoreRanking
			when a0.MarketCapitalisation >= a5.MarketCapitalisation then a5.ScoreRanking
			else a5.ScoreRanking end [MarketCapitalisationScore],
		case when a0.DIVYield >= a1.DIVYield then a1.ScoreRanking
			when a0.DIVYield >= a2.DIVYield then a2.ScoreRanking
			when a0.DIVYield >= a3.DIVYield then a3.ScoreRanking
			when a0.DIVYield >= a4.DIVYield then a4.ScoreRanking
			when a0.DIVYield >= a5.DIVYield then a5.ScoreRanking
			else a5.ScoreRanking end [DIVYieldScore],
		case when a0.Frank >= a1.Frank then a1.ScoreRanking
			when a0.Frank >= a2.Frank then a2.ScoreRanking 
			when a0.Frank >= a3.Frank then a3.ScoreRanking
			when a0.Frank >= a4.Frank then a4.ScoreRanking
			when a0.Frank >= a5.Frank then a5.ScoreRanking
			else a5.ScoreRanking end [FrankScore],
		case when a0.ROA >= a1.ROA then a1.ScoreRanking
			when a0.ROA >= a2.ROA then a2.ScoreRanking
			when a0.ROA >= a3.ROA then a3.ScoreRanking
			when a0.ROA >= a4.ROA then a4.ScoreRanking
			when a0.ROA >= a5.ROA then a5.ScoreRanking
			else a5.ScoreRanking end [ROAScore],
		case when a0.ROE >= a1.ROE then a1.ScoreRanking
			when a0.ROE >= a2.ROE then a2.ScoreRanking
			when a0.ROE >= a3.ROE then a3.ScoreRanking
			when a0.ROE >= a4.ROE then a4.ScoreRanking
			when a0.ROE >= a5.ROE then a5.ScoreRanking
			else a5.ScoreRanking end [ROEScore],
		case when a0.IntCover >= a1.IntCover then a1.ScoreRanking
			when a0.IntCover >= a2.IntCover then a2.ScoreRanking
			when a0.IntCover >= a3.IntCover then a3.ScoreRanking
			when a0.IntCover >= a4.IntCover then a4.ScoreRanking
			when a0.IntCover >= a5.IntCover then a5.ScoreRanking
			else a5.ScoreRanking end [IntCoverScore],
		case when a0.DebtEquity <= a1.DebtEquity then a1.ScoreRanking
			when a0.DebtEquity <= a2.DebtEquity then a2.ScoreRanking
			when a0.DebtEquity <= a3.DebtEquity then a3.ScoreRanking
			when a0.DebtEquity <= a4.DebtEquity then a4.ScoreRanking
			when a0.DebtEquity <= a5.DebtEquity then a5.ScoreRanking
			else a5.ScoreRanking end [DebtEquityScore],
		case when a0.PE <= a1.PE then a1.ScoreRanking
			when a0.PE <= a2.PE then a2.ScoreRanking
			when a0.PE <= a3.PE then a3.ScoreRanking
			when a0.PE <= a4.PE then a4.ScoreRanking
			when a0.PE <= a5.PE then a5.ScoreRanking
			else a5.ScoreRanking end [PEScore],
		case when a0.Beta5Year <= a1.Beta5Year then a1.ScoreRanking
			when a0.Beta5Year <= a2.Beta5Year then a2.ScoreRanking
			when a0.Beta5Year <= a3.Beta5Year then a3.ScoreRanking
			when a0.Beta5Year <= a4.Beta5Year then a4.ScoreRanking
			when a0.Beta5Year <= a5.Beta5Year then a5.ScoreRanking 
			else a5.ScoreRanking end [Beta5YearScore]
	FROM dbo.AustEquityHistoricalScores a0,
		dbo.AustEquityHistoricalScores a1, 
		dbo.AustEquityHistoricalScores a2,
		dbo.AustEquityHistoricalScores a3, 
		dbo.AustEquityHistoricalScores a4,
		dbo.AustEquityHistoricalScores a5

	WHERE a1.SettingName = 'Defensive'	AND a1.IsSetting = 1 
	AND a2.SettingName = 'Conservative' AND a2.IsSetting = 1
	AND a3.SettingName = 'Balance'		AND a3.IsSetting = 1 
	AND a4.SettingName = 'Assertive'	AND a4.IsSetting = 1
	AND a5.SettingName = 'Aggressive'	AND a5.IsSetting = 1 ) A




GO
/****** Object:  View [dbo].[show_aust_equity_suitability_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[show_aust_equity_suitability_scores]
AS
select 
	a.*,
	case 
		when a.TotalSuitabilityScore <= 70 then 'Defensive' 
		when a.TotalSuitabilityScore <= 90 then 'Conservative'
		when a.TotalSuitabilityScore <= 110 then 'Balance'
		when a.TotalSuitabilityScore <= 130 then 'Assertive'
		when a.TotalSuitabilityScore <= 200 then 'Aggressive'
	else 'Danger - Not on APL' end [Recommendation]

from 
(select 
	h.StockID,
	h.EPSGrowthScore [HistoricalEPSGrowthScore], 
	h.MarketCapitalisationScore [HistoricalMarketCapitalisationScore],
	h.DIVYieldScore [HistoricalDIVYieldScore],
	h.FrankScore [HistoricalFrankScore],
	h.ROAScore [HistoricalROAScore],
	h.ROEScore [HistoricalROEScore],
	h.IntCoverScore [HistoricalIntCoverScore],
	h.DebtEquityScore [HistoricalDebtEquityScore],
	h.PEScore [HistoricalPEScore],
	h.Beta5YearScore [HistoricalBeta5YearScore],
	c.EPSGrowthScore [CurrentEPSGrowthScore],
	c.MorningstarRecommendationScore [CurrentMorningstarRecommendationScore],
	c.DIVYieldScore [CurrentDIVYieldScore],
	c.FrankScore [CurrentFrankScore],
	c.ROAScore [CurrentROAScore],
	c.ROEScore [CurrentROEScore],
	c.IntCoverScore [CurrentIntCoverScore],
	c.DebtEquityScore [CurrentDebtEquityScore],
	c.PEScore [CurrentPEScore],
	c.IntrinsicValueVariationScore [CurrentIntrinsicValueVariationScore],
	(h.TotalHistoricalScore + c.CurrentTotalScore) [TotalSuitabilityScore]
	

from dbo.show_aust_equity_historical_scores h 
join dbo.show_aust_equity_current_scores c on h.stockid = c.stockid ) a



GO
/****** Object:  View [dbo].[show_inter_equity_current_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[show_inter_equity_current_scores]
AS
	SELECT 
		i.StockID,
		i.FiveYearTotalReturnScore,
		i.MorningStarRecommendationScore,
		i.DIVYieldScore,
		i.ROAScore,
		i.ROEScore,
		i.FinancialLeverageScore,
		i.YearRevenueGrowthScore,
		i.DebtEquityRatioScore,
		i.CreditRatingScore,
		i.ValueVariationScore,
		(i.FiveYearTotalReturnScore + i.MorningStarRecommendationScore + i.DIVYieldScore +
		i.ROAScore + i.ROEScore + i.FinancialLeverageScore + i.YearRevenueGrowthScore +
		i.DebtEquityRatioScore + i.CreditRatingScore + i.ValueVariationScore) [TotalCurrentScore]
	FROM 
	(SELECT 
		i0.StockID, 
		case when i0.FiveYearTotalReturn >= i1.FiveYearTotalReturn then i1.ScoreRanking
			when i0.FiveYearTotalReturn >= i2.FiveYearTotalReturn then i2.ScoreRanking
			when i0.FiveYearTotalReturn >= i3.FiveYearTotalReturn then i3.ScoreRanking
			when i0.FiveYearTotalReturn >= i4.FiveYearTotalReturn then i4.ScoreRanking
			when i0.FiveYearTotalReturn >= i5.FiveYearTotalReturn then i5.ScoreRanking
			else i5.ScoreRanking end [FiveYearTotalReturnScore] ,
		case when i0.MorningstarRecommendation = 'BUY' OR i0.MorningstarRecommendation = 'HIGHLY RECOMMENDED' then i1.ScoreRanking
			when i0.MorningstarRecommendation = 'ACCUMULATE' OR i0.MorningstarRecommendation = 'RECOMMENDED' then i2.ScoreRanking
			when i0.MorningstarRecommendation = 'HOLD' OR i0.MorningstarRecommendation = 'INVESTMENTGRADE' then i3.ScoreRanking
			when i0.MorningstarRecommendation = 'REDUCE'  then i4.ScoreRanking
			when i0.MorningstarRecommendation = 'SELL' OR i0.MorningstarRecommendation = 'AVOID' then i5.ScoreRanking
			else i5.ScoreRanking * 20 end [MorningStarRecommendationScore],
		case when i0.DIVYield >= i1.DIVYield then i1.ScoreRanking
			when i0.DIVYield >= i2.DIVYield then i2.ScoreRanking
			when i0.DIVYield >= i3.DIVYield then i3.ScoreRanking
			when i0.DIVYield >= i4.DIVYield then i4.ScoreRanking
			when i0.DIVYield >= i5.DIVYield then i5.ScoreRanking
			else i5.ScoreRanking end [DIVYieldScore],
		case when i0.ROA >= i1.ROA then i1.ScoreRanking
			when i0.ROA >= i2.ROA then i2.ScoreRanking
			when i0.ROA >= i3.ROA then i3.ScoreRanking
			when i0.ROA >= i4.ROA then i4.ScoreRanking
			when i0.ROA >= i5.ROA then i5.ScoreRanking
			else i5.ScoreRanking end [ROAScore],
		case when i0.ROE >= i1.ROE then i1.ScoreRanking
			when i0.ROE >= i2.ROE then i2.ScoreRanking
			when i0.ROE >= i3.ROE then i3.ScoreRanking
			when i0.ROE >= i4.ROE then i4.ScoreRanking
			when i0.ROE >= i5.ROE then i5.ScoreRanking
			else i5.ScoreRanking end [ROEScore],
		case when i0.FinancialLeverage <= i1.FinancialLeverage then i1.ScoreRanking
			when i0.FinancialLeverage <= i2.FinancialLeverage then i2.ScoreRanking
			when i0.FinancialLeverage <= i3.FinancialLeverage then i3.ScoreRanking
			when i0.FinancialLeverage <= i4.FinancialLeverage then i4.ScoreRanking
			when i0.FinancialLeverage <= i5.FinancialLeverage then i5.ScoreRanking
			else i5.ScoreRanking end [FinancialLeverageScore],
		case when i0.YearRevenueGrowth >= i1.YearRevenueGrowth then i1.ScoreRanking
			when i0.YearRevenueGrowth >= i2.YearRevenueGrowth then i2.ScoreRanking
			when i0.YearRevenueGrowth >= i3.YearRevenueGrowth then i3.ScoreRanking
			when i0.YearRevenueGrowth >= i4.YearRevenueGrowth then i4.ScoreRanking
			when i0.YearRevenueGrowth >= i5.YearRevenueGrowth then i5.ScoreRanking
			else i5.ScoreRanking end [YearRevenueGrowthScore],
		case when i0.DebtEquityRatio <= i1.DebtEquityRatio then i1.ScoreRanking
			when i0.DebtEquityRatio <= i2.DebtEquityRatio then i2.ScoreRanking
			when i0.DebtEquityRatio <= i3.DebtEquityRatio then i3.ScoreRanking
			when i0.DebtEquityRatio <= i4.DebtEquityRatio then i4.ScoreRanking
			when i0.DebtEquityRatio <= i5.DebtEquityRatio then i5.ScoreRanking
			else i5.ScoreRanking end [DebtEquityRatioScore],
		case when i0.CreditRating = 'AAA' OR i0.CreditRating = 'AAA+' OR i0.CreditRating = 'AAA-' then  i1.ScoreRanking
			when i0.CreditRating = 'AA' OR i0.CreditRating = 'AA+' OR i0.CreditRating = 'AA-' then i2.ScoreRanking
			when i0.CreditRating = 'A' OR i0.CreditRating = 'A+' OR i0.CreditRating = 'A-' then i3.ScoreRanking
			when i0.CreditRating = 'BBB' OR i0.CreditRating = 'BBB+' then i4.ScoreRanking
			when i0.CreditRating = 'BBB-' OR i0.CreditRating = 'BB+' OR i0.CreditRating = 'BB' or i0.CreditRating = 'BB-' OR i0.CreditRating = '0' OR i0.CreditRating='' then i5.ScoreRanking 
			else i5.ScoreRanking end [CreditRatingScore],
		case when i0.ValueVariation >= i1.ValueVariation then i1.ScoreRanking
			when i0.ValueVariation >= i2.ValueVariation then i2.ScoreRanking
			when i0.ValueVariation >= i3.ValueVariation then i3.ScoreRanking
			when i0.ValueVariation >= i4.ValueVariation then i4.ScoreRanking
			when i0.ValueVariation >= i5.ValueVariation then i5.ScoreRanking
			else i5.ScoreRanking end [ValueVariationScore]
	FROM dbo.InterEquityCurrentScores i0,
		dbo.InterEquityCurrentScores i1, 
		dbo.InterEquityCurrentScores i2,
		dbo.InterEquityCurrentScores i3, 
		dbo.InterEquityCurrentScores i4,
		dbo.InterEquityCurrentScores i5

	WHERE i1.SettingName = 'Defensive'	AND i1.IsSetting = 1 
	AND i2.SettingName = 'Conservative' AND i2.IsSetting = 1
	AND i3.SettingName = 'Balance'		AND i3.IsSetting = 1 
	AND i4.SettingName = 'Assertive'	AND i4.IsSetting = 1
	AND i5.SettingName = 'Aggressive'	AND i5.IsSetting = 1)i




GO
/****** Object:  View [dbo].[show_inter_equity_historical_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[show_inter_equity_historical_scores]
AS
	SELECT 
		i.StockID,
		i.OneYearReturnScore,
		i.MarketCapitalisationScore,
		i.DIVYieldScore,
		i.ROAScore,
		i.ROEScore,
		i.QuickRatioScore,
		i.CurrentRatioScore,
		i.DebtEquityRatioScore,
		i.PERatioScore,
		i.Beta5YearScore,
		(i.OneYearReturnScore +	i.MarketCapitalisationScore + i.DIVYieldScore +
		i.ROAScore + i.ROEScore + i.QuickRatioScore + i.CurrentRatioScore +
		i.DebtEquityRatioScore + i.PERatioScore + i.Beta5YearScore) [TotalHistocialScore]
	FROM 
	(SELECT 
		i0.StockID, 
		case when i0.OneYearReturn >= i1.OneYearReturn then i1.ScoreRanking
			when i0.OneYearReturn >= i2.OneYearReturn then i2.ScoreRanking
			when i0.OneYearReturn >= i3.OneYearReturn then i3.ScoreRanking
			when i0.OneYearReturn >= i4.OneYearReturn then i4.ScoreRanking
			when i0.OneYearReturn >= i5.OneYearReturn then i5.ScoreRanking
			else i5.ScoreRanking end [OneYearReturnScore] ,
		case when i0.MarketCapitalisation >= i1.MarketCapitalisation then i1.ScoreRanking
			when i0.MarketCapitalisation >= i2.MarketCapitalisation then i2.ScoreRanking
			when i0.MarketCapitalisation >= i3.MarketCapitalisation then i3.ScoreRanking
			when i0.MarketCapitalisation >= i4.MarketCapitalisation then i4.ScoreRanking
			when i0.MarketCapitalisation >= i5.MarketCapitalisation then i5.ScoreRanking
			else i5.ScoreRanking end [MarketCapitalisationScore],
		case when i0.DIVYield >= i1.DIVYield then i1.ScoreRanking
			when i0.DIVYield >= i2.DIVYield then i2.ScoreRanking
			when i0.DIVYield >= i3.DIVYield then i3.ScoreRanking
			when i0.DIVYield >= i4.DIVYield then i4.ScoreRanking
			when i0.DIVYield >= i5.DIVYield then i5.ScoreRanking
			else i5.ScoreRanking end [DIVYieldScore],
		case when i0.ROA >= i1.ROA then i1.ScoreRanking
			when i0.ROA >= i2.ROA then i2.ScoreRanking
			when i0.ROA >= i3.ROA then i3.ScoreRanking
			when i0.ROA >= i4.ROA then i4.ScoreRanking
			when i0.ROA >= i5.ROA then i5.ScoreRanking
			else i5.ScoreRanking end [ROAScore],
		case when i0.ROE >= i1.ROE then i1.ScoreRanking
			when i0.ROE >= i2.ROE then i2.ScoreRanking
			when i0.ROE >= i3.ROE then i3.ScoreRanking
			when i0.ROE >= i4.ROE then i4.ScoreRanking
			when i0.ROE >= i5.ROE then i5.ScoreRanking
			else i5.ScoreRanking end [ROEScore],
		case when i0.QuickRatio >= i1.QuickRatio then i1.ScoreRanking
			when i0.QuickRatio >= i2.QuickRatio then i2.ScoreRanking
			when i0.QuickRatio >= i3.QuickRatio then i3.ScoreRanking
			when i0.QuickRatio >= i4.QuickRatio then i4.ScoreRanking
			when i0.QuickRatio >= i5.QuickRatio then i5.ScoreRanking
			else i5.ScoreRanking end [QuickRatioScore],
		case when i0.CurrentRatio >= i1.CurrentRatio then i1.ScoreRanking
			when i0.CurrentRatio >= i2.CurrentRatio then i2.ScoreRanking
			when i0.CurrentRatio >= i3.CurrentRatio then i3.ScoreRanking
			when i0.CurrentRatio >= i4.CurrentRatio then i4.ScoreRanking
			when i0.CurrentRatio >= i5.CurrentRatio then i5.ScoreRanking
			else i5.ScoreRanking end [CurrentRatioScore],
		case when i0.DebtEquityRatio <= i1.DebtEquityRatio then i1.ScoreRanking
			when i0.DebtEquityRatio <= i2.DebtEquityRatio then i2.ScoreRanking
			when i0.DebtEquityRatio <= i3.DebtEquityRatio then i3.ScoreRanking
			when i0.DebtEquityRatio <= i4.DebtEquityRatio then i4.ScoreRanking
			when i0.DebtEquityRatio <= i5.DebtEquityRatio then i5.ScoreRanking
			else i5.ScoreRanking end [DebtEquityRatioScore],
		case when i0.PERatio <= i1.PERatio then i1.ScoreRanking
			when i0.PERatio <= i2.PERatio then i2.ScoreRanking
			when i0.PERatio <= i3.PERatio then i3.ScoreRanking
			when i0.PERatio <= i4.PERatio then i4.ScoreRanking
			when i0.PERatio <= i5.PERatio then i5.ScoreRanking
			else i5.ScoreRanking end [PERatioScore],
		case when i0.Beta5Year <= i1.Beta5Year then i1.ScoreRanking
			when i0.Beta5Year <= i2.Beta5Year then i2.ScoreRanking
			when i0.Beta5Year <= i3.Beta5Year then i3.ScoreRanking
			when i0.Beta5Year <= i4.Beta5Year then i4.ScoreRanking
			when i0.Beta5Year <= i5.Beta5Year then i5.ScoreRanking 
			else i5.ScoreRanking end [Beta5YearScore]
	FROM dbo.InterEquityHistoricalScores i0,
		dbo.InterEquityHistoricalScores i1, 
		dbo.InterEquityHistoricalScores i2,
		dbo.InterEquityHistoricalScores i3, 
		dbo.InterEquityHistoricalScores i4,
		dbo.InterEquityHistoricalScores i5

	WHERE i1.SettingName = 'Defensive'	AND i1.IsSetting = 1 
	AND i2.SettingName = 'Conservative' AND i2.IsSetting = 1
	AND i3.SettingName = 'Balance'		AND i3.IsSetting = 1 
	AND i4.SettingName = 'Assertive'	AND i4.IsSetting = 1
	AND i5.SettingName = 'Aggressive'	AND i5.IsSetting = 1) i




GO
/****** Object:  View [dbo].[show_inter_equity_suitability_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[show_inter_equity_suitability_scores]
AS
select 
	i.*,
	case 
		when i.TotalSuitabilityScore <= 70 then 'Defensive' 
		when i.TotalSuitabilityScore <= 90 then 'Conservative'
		when i .TotalSuitabilityScore <= 110 then 'Balance'
		when i.TotalSuitabilityScore <= 130 then 'Assertive'
		when i.TotalSuitabilityScore <= 200 then 'Aggressive'
	else 'Danger - Not on APL' end [Recommendation]
from
(select
	h.StockID,
	h.OneYearReturnScore [HistoricalOneYearReturnScore],
	h.MarketCapitalisationScore [HistoricalMarketCapitalisationScore],
	h.DIVYieldScore [HistoricalDIVYieldScore],
	h.ROAScore [HistoricalROAScore],
	h.ROEScore [HistoricaROEScore],
	h.QuickRatioScore [HistoricalQuickRatioScore],
	h.CurrentRatioScore [HistoricalCurrentRatioScore],
	h.DebtEquityRatioScore [HistoricalDebtEquityRatioScore],
	h.PERatioScore [HistoricalPERatioScore],
	h.Beta5YearScore [HistoricalBeta5YearScore],
	c.FiveYearTotalReturnScore [CurrentFiveYearTotalReturnScore],
	c.MorningStarRecommendationScore,
	c.DIVYieldScore [CurrentDIVYieldScore],
	c.ROAScore [CurrentROAScore],
	c.ROEScore [CurrentROEScore],
	c.FinancialLeverageScore [CurrentFinancialLeverageScore],
	c.YearRevenueGrowthScore [CurrentYearRevenueGrowthScore],
	c.DebtEquityRatioScore [CurrentDebtEquityRatioScore],
	c.CreditRatingScore [CurrentCreditRatingScore],
	c.ValueVariationScore [CurrentValueVariationScore],
	(c.TotalCurrentScore + h.TotalHistocialScore) [TotalSuitabilityScore]
from dbo.show_inter_equity_historical_scores h
join dbo.show_inter_equity_current_scores c on h.StockID = c.StockID) i 



GO
/****** Object:  View [dbo].[show_managed_funds_current_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[show_managed_funds_current_scores]
AS
	SELECT
		m.StockID,
		m.OneYearReturnScore,
		m.OneYearAlphaScore,
		m.MorningStarRecommendationScore,
		m.OneYearBetaScore,
		m.OneYearInfoRatioScore,
		m.OneYearTrackErrorScore,
		m.OneYearSharpRatioScore,
		m.MaxManagementExpenseRatioScore,
		m.PerformanceFeeScore,
		m.YearsSinceInceptionScore,
		(m.OneYearReturnScore +	m.OneYearAlphaScore + m.MorningStarRecommendationScore +
		m.OneYearBetaScore + m.OneYearInfoRatioScore +	m.OneYearTrackErrorScore +
		m.OneYearSharpRatioScore +	m.MaxManagementExpenseRatioScore +	m.PerformanceFeeScore +
		m.YearsSinceInceptionScore) [TotalCurrentScore]

	FROM
	(SELECT 
		m0.StockID, 
		case when m0.OneYearReturn >= m1.OneYearReturn then m1.ScoreRanking
			when m0.OneYearReturn >= m2.OneYearReturn then m2.ScoreRanking
			when m0.OneYearReturn >= m3.OneYearReturn then m3.ScoreRanking
			when m0.OneYearReturn >= m4.OneYearReturn then m4.ScoreRanking
			when m0.OneYearReturn >= m5.OneYearReturn then m5.ScoreRanking
			else m5.ScoreRanking end [OneYearReturnScore] ,
		case when m0.MorningstarRecommendation = 'BUY' OR m0.MorningstarRecommendation = 'HIGHLY RECOMMENDED' then m1.ScoreRanking
			when m0.MorningstarRecommendation = 'ACCUMULATE' OR m0.MorningstarRecommendation = 'RECOMMENDED' then m2.ScoreRanking
			when m0.MorningstarRecommendation = 'HOLD' OR m0.MorningstarRecommendation = 'INVESTMENTGRADE' then m3.ScoreRanking
			when m0.MorningstarRecommendation = 'REDUCE'  then m4.ScoreRanking
			when m0.MorningstarRecommendation = 'SELL' OR m0.MorningstarRecommendation = 'AVOID' then m5.ScoreRanking
			else m5.ScoreRanking * 20 end [MorningStarRecommendationScore],
		case when m0.OneYearAlpha >= m1.OneYearAlpha then m1.ScoreRanking
			when m0.OneYearAlpha >= m2.OneYearAlpha then m2.ScoreRanking
			when m0.OneYearAlpha >= m3.OneYearAlpha then m3.ScoreRanking
			when m0.OneYearAlpha >= m4.OneYearAlpha then m4.ScoreRanking
			when m0.OneYearAlpha >= m5.OneYearAlpha then m5.ScoreRanking
			else m5.ScoreRanking end [OneYearAlphaScore],
		case when m0.OneYearBeta <= m1.OneYearBeta then m1.ScoreRanking
			when m0.OneYearBeta <= m2.OneYearBeta then m2.ScoreRanking
			when m0.OneYearBeta <= m3.OneYearBeta then m3.ScoreRanking
			when m0.OneYearBeta <= m4.OneYearBeta then m4.ScoreRanking
			when m0.OneYearBeta <= m5.OneYearBeta then m5.ScoreRanking
			else m5.ScoreRanking end [OneYearBetaScore],
		case when m0.OneYearInfoRatio >= m1.OneYearInfoRatio then m1.ScoreRanking
			when m0.OneYearInfoRatio >= m2.OneYearInfoRatio then m2.ScoreRanking
			when m0.OneYearInfoRatio >= m3.OneYearInfoRatio then m3.ScoreRanking
			when m0.OneYearInfoRatio >= m4.OneYearInfoRatio then m4.ScoreRanking
			when m0.OneYearInfoRatio >= m5.OneYearInfoRatio then m5.ScoreRanking
			else m5.ScoreRanking end [OneYearInfoRatioScore],
		case when m0.OneYearTrackError >= m1.OneYearTrackError then m1.ScoreRanking
			when m0.OneYearTrackError >= m2.OneYearTrackError then m2.ScoreRanking
			when m0.OneYearTrackError >= m3.OneYearTrackError then m3.ScoreRanking
			when m0.OneYearTrackError >= m4.OneYearTrackError then m4.ScoreRanking
			when m0.OneYearTrackError >= m5.OneYearTrackError then m5.ScoreRanking
			else m5.ScoreRanking end [OneYearTrackErrorScore],
		case when m0.OneYearSharpRatio >= m1.OneYearSharpRatio then m1.ScoreRanking
			when m0.OneYearSharpRatio >= m2.OneYearSharpRatio then m2.ScoreRanking
			when m0.OneYearSharpRatio >= m3.OneYearSharpRatio then m3.ScoreRanking
			when m0.OneYearSharpRatio >= m4.OneYearSharpRatio then m4.ScoreRanking
			when m0.OneYearSharpRatio >= m5.OneYearSharpRatio then m5.ScoreRanking
			else m5.ScoreRanking end [OneYearSharpRatioScore],
		case when m0.MaxManagementExpenseRatio <= m1.MaxManagementExpenseRatio then m1.ScoreRanking
			when m0.MaxManagementExpenseRatio <= m2.MaxManagementExpenseRatio then m2.ScoreRanking
			when m0.MaxManagementExpenseRatio <= m3.MaxManagementExpenseRatio then m3.ScoreRanking
			when m0.MaxManagementExpenseRatio <= m4.MaxManagementExpenseRatio then m4.ScoreRanking
			when m0.MaxManagementExpenseRatio <= m5.MaxManagementExpenseRatio then m5.ScoreRanking
			else m5.ScoreRanking end [MaxManagementExpenseRatioScore],
		case when m0.PerformanceFee <= m1.PerformanceFee then m1.ScoreRanking
			when m0.PerformanceFee <= m2.PerformanceFee then m2.ScoreRanking
			when m0.PerformanceFee <= m3.PerformanceFee then m3.ScoreRanking
			when m0.PerformanceFee <= m4.PerformanceFee then m4.ScoreRanking
			when m0.PerformanceFee <= m5.PerformanceFee then m5.ScoreRanking
			else m5.ScoreRanking end [PerformanceFeeScore],
		case when m0.YearsSinceInception >= m1.YearsSinceInception then m1.ScoreRanking
			when m0.YearsSinceInception >= m2.YearsSinceInception then m2.ScoreRanking
			when m0.YearsSinceInception >= m3.YearsSinceInception then m3.ScoreRanking
			when m0.YearsSinceInception >= m4.YearsSinceInception then m4.ScoreRanking
			when m0.YearsSinceInception >= m5.YearsSinceInception then m5.ScoreRanking
			else m5.ScoreRanking end [YearsSinceInceptionScore]
	FROM dbo.ManagedFundsCurrentScores m0,
		dbo.ManagedFundsCurrentScores m1, 
		dbo.ManagedFundsCurrentScores m2,
		dbo.ManagedFundsCurrentScores m3, 
		dbo.ManagedFundsCurrentScores m4,
		dbo.ManagedFundsCurrentScores m5

	WHERE m1.SettingName = 'Defensive'	AND m1.IsSetting = 1 
	AND m2.SettingName = 'Conservative' AND m2.IsSetting = 1
	AND m3.SettingName = 'Balance'		AND m3.IsSetting = 1 
	AND m4.SettingName = 'Assertive'	AND m4.IsSetting = 1
	AND m5.SettingName = 'Aggressive'	AND m5.IsSetting = 1) m




GO
/****** Object:  View [dbo].[show_managed_funds_historical_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[show_managed_funds_historical_scores]
AS
	SELECT 
		m.StockID,
		m.FiveYearReturnScore,
		m.FiveYearAlphaScore,
		m.FiveYearBetaScore,
		m.FiveYearInfoRatioScore,
		m.FiveYearStdDevScore,
		m.FiveYearSkewRatioScore,
		m.FiveYearTrackErrorScore,
		m.FiveYearSharpRatioScore,
		m.GlobalCategoryScore,
		m.FundSizeScore,
		(m.FiveYearReturnScore + m.FiveYearAlphaScore +	m.FiveYearBetaScore +
		m.FiveYearInfoRatioScore + m.FiveYearStdDevScore + m.FiveYearSkewRatioScore +
		m.FiveYearTrackErrorScore +	m.FiveYearSharpRatioScore +	m.GlobalCategoryScore +
		m.FundSizeScore) [TotalHistoricalScore]
	FROM
	(SELECT
		m0.StockID, 
		case when m0.FiveYearReturn >= m1.FiveYearReturn then m1.ScoreRanking
			when m0.FiveYearReturn >= m2.FiveYearReturn then m2.ScoreRanking
			when m0.FiveYearReturn >= m3.FiveYearReturn then m3.ScoreRanking
			when m0.FiveYearReturn >= m4.FiveYearReturn then m4.ScoreRanking
			when m0.FiveYearReturn >= m5.FiveYearReturn then m5.ScoreRanking
			else m5.ScoreRanking end [FiveYearReturnScore] ,
		case when m0.FiveYearAlpha >= m1.FiveYearAlpha then m1.ScoreRanking
			when m0.FiveYearAlpha >= m2.FiveYearAlpha then m2.ScoreRanking
			when m0.FiveYearAlpha >= m3.FiveYearAlpha then m3.ScoreRanking
			when m0.FiveYearAlpha >= m4.FiveYearAlpha then m4.ScoreRanking
			when m0.FiveYearAlpha >= m5.FiveYearAlpha then m5.ScoreRanking
			else m5.ScoreRanking end [FiveYearAlphaScore],
		case when m0.FiveYearBeta <= m1.FiveYearBeta then m1.ScoreRanking
			when m0.FiveYearBeta <= m2.FiveYearBeta then m2.ScoreRanking
			when m0.FiveYearBeta <= m3.FiveYearBeta then m3.ScoreRanking
			when m0.FiveYearBeta <= m4.FiveYearBeta then m4.ScoreRanking
			when m0.FiveYearBeta <= m5.FiveYearBeta then m5.ScoreRanking
			else m5.ScoreRanking end [FiveYearBetaScore],
		case when m0.FiveYearInfoRatio >= m1.FiveYearInfoRatio then m1.ScoreRanking
			when m0.FiveYearInfoRatio >= m2.FiveYearInfoRatio then m2.ScoreRanking
			when m0.FiveYearInfoRatio >= m3.FiveYearInfoRatio then m3.ScoreRanking
			when m0.FiveYearInfoRatio >= m4.FiveYearInfoRatio then m4.ScoreRanking
			when m0.FiveYearInfoRatio >= m5.FiveYearInfoRatio then m5.ScoreRanking
			else m5.ScoreRanking end [FiveYearInfoRatioScore],
		case when m0.FiveYearStdDev >= m1.FiveYearStdDev then m1.ScoreRanking
			when m0.FiveYearStdDev >= m2.FiveYearStdDev then m2.ScoreRanking
			when m0.FiveYearStdDev >= m3.FiveYearStdDev then m3.ScoreRanking
			when m0.FiveYearStdDev >= m4.FiveYearStdDev then m4.ScoreRanking
			when m0.FiveYearStdDev >= m5.FiveYearStdDev then m5.ScoreRanking
			else m5.ScoreRanking end [FiveYearStdDevScore],
		case when m0.FiveYearSkewRatio >= m1.FiveYearSkewRatio then m1.ScoreRanking
			when m0.FiveYearSkewRatio >= m2.FiveYearSkewRatio then m2.ScoreRanking
			when m0.FiveYearSkewRatio >= m3.FiveYearSkewRatio then m3.ScoreRanking
			when m0.FiveYearSkewRatio >= m4.FiveYearSkewRatio then m4.ScoreRanking
			when m0.FiveYearSkewRatio >= m5.FiveYearSkewRatio then m5.ScoreRanking
			else m5.ScoreRanking end [FiveYearSkewRatioScore],
		case when m0.FiveYearTrackError >= m1.FiveYearTrackError then m1.ScoreRanking
			when m0.FiveYearTrackError >= m2.FiveYearTrackError then m2.ScoreRanking
			when m0.FiveYearTrackError >= m3.FiveYearTrackError then m3.ScoreRanking
			when m0.FiveYearTrackError >= m4.FiveYearTrackError then m4.ScoreRanking
			when m0.FiveYearTrackError >= m5.FiveYearTrackError then m5.ScoreRanking
			else m5.ScoreRanking end [FiveYearTrackErrorScore],
		case when m0.FiveYearSharpRatio >= m1.FiveYearSharpRatio then m1.ScoreRanking
			when m0.FiveYearSharpRatio >= m2.FiveYearSharpRatio then m2.ScoreRanking
			when m0.FiveYearSharpRatio >= m3.FiveYearSharpRatio then m3.ScoreRanking
			when m0.FiveYearSharpRatio >= m4.FiveYearSharpRatio then m4.ScoreRanking
			when m0.FiveYearSharpRatio >= m5.FiveYearSharpRatio then m5.ScoreRanking
			else m5.ScoreRanking end [FiveYearSharpRatioScore],
		case when m0.GlobalCategory >= m1.GlobalCategory then m1.ScoreRanking
			when m0.GlobalCategory >= m2.GlobalCategory then m2.ScoreRanking
			when m0.GlobalCategory >= m3.GlobalCategory then m3.ScoreRanking
			when m0.GlobalCategory >= m4.GlobalCategory then m4.ScoreRanking
			when m0.GlobalCategory >= m5.GlobalCategory then m5.ScoreRanking
			else m5.ScoreRanking end [GlobalCategoryScore],
		case when m0.FundSize >= m1.FundSize then m1.ScoreRanking
			when m0.FundSize >= m2.FundSize then m2.ScoreRanking
			when m0.FundSize >= m3.FundSize then m3.ScoreRanking
			when m0.FundSize >= m4.FundSize then m4.ScoreRanking
			when m0.FundSize >= m5.FundSize then m5.ScoreRanking
			else m5.ScoreRanking end [FundSizeScore]
	FROM dbo.ManagedFundsHistoricalScores m0,
		dbo.ManagedFundsHistoricalScores m1, 
		dbo.ManagedFundsHistoricalScores m2,
		dbo.ManagedFundsHistoricalScores m3, 
		dbo.ManagedFundsHistoricalScores m4,
		dbo.ManagedFundsHistoricalScores m5

	WHERE m1.SettingName = 'Defensive'	AND m1.IsSetting = 1 
	AND m2.SettingName = 'Conservative' AND m2.IsSetting = 1
	AND m3.SettingName = 'Balance'		AND m3.IsSetting = 1 
	AND m4.SettingName = 'Assertive'	AND m4.IsSetting = 1
	AND m5.SettingName = 'Aggressive'	AND m5.IsSetting = 1 )m






GO
/****** Object:  View [dbo].[show_managed_funds_suitability_scores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[show_managed_funds_suitability_scores]
as
select 
	m.*,
	case 
		when m.TotalSuitabilityScore <= 70 then 'Defensive' 
		when m.TotalSuitabilityScore <= 90 then 'Conservative'
		when m.TotalSuitabilityScore <= 110 then 'Balance'
		when m.TotalSuitabilityScore <= 130 then 'Assertive'
		when m.TotalSuitabilityScore <= 200 then 'Aggressive'
	else 'Danger - Not on APL' end [Recommendation]
from
(select 
	h.StockID,
	h.FiveYearReturnScore,
	h.FiveYearAlphaScore,
	h.FiveYearBetaScore,
	h.FiveYearInfoRatioScore,
	h.FiveYearStdDevScore,
	h.FiveYearSkewRatioScore,
	h.FiveYearTrackErrorScore,
	h.FiveYearSharpRatioScore,
	h.GlobalCategoryScore,
	h.FundSizeScore,
	c.OneYearReturnScore,
	c.OneYearAlphaScore,
	c.MorningStarRecommendationScore,
	c.OneYearBetaScore,
	c.OneYearInfoRatioScore,
	c.OneYearTrackErrorScore,
	c.OneYearSharpRatioScore,
	c.MaxManagementExpenseRatioScore,
	c.PerformanceFeeScore,
	c.YearsSinceInceptionScore,
	(h.TotalHistoricalScore + c.TotalCurrentScore)[TotalSuitabilityScore] 


from dbo.show_managed_funds_historical_scores h 
join dbo.show_managed_funds_current_scores c on h.StockID = c.StockID) m



GO
/****** Object:  View [dbo].[current_stock_prices]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[current_stock_prices]
AS

SELECT TOP 100 PERCENT
	s.name, 
	s.Ticker,
	S.APIRCode,
	S.SecID,
	S.ISIN,
	dp1.stockid,
	dp1.date,
	dp1.price
	
FROM dbo.DailyPrices dp1
INNER JOIN dbo.Stocks S ON dp1.StockID = S.StockID 
INNER JOIN
(SELECT 
	stockid,
	max(date) date
FROM dbo.DailyPrices
GROUP BY stockid) dp2
ON dp1.StockID = dp2.StockID AND dp1.date = dp2.date 

ORDER BY S.Ticker 



GO
/****** Object:  View [dbo].[show_capability_adviser_counts]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[show_capability_adviser_counts]

AS
	SELECT 
		CG.CapabilityGroupID,
		CG.CapabilityGroup,
		CT.CapabilityTypeID,
		CT.CapabilityType,
		COUNT(CL.AdviserCapabilityID)[AdviserCount]

	FROM dbo.CapabilityTypes CT 
	LEFT JOIN dbo.AdviserCapabilityLinks CL ON CT.CapabilityTypeID = CL.CapabilityTypeID
	INNER JOIN dbo.CapabilityGroups CG ON CG.CapabilityGroupID = CT.CapabilityGroupID

	WHERE CL.Capable = 1 
	GROUP BY 
		CG.CapabilityGroupID,
		CG.CapabilityGroup,
		CT.CapabilityTypeID,
		CT.CapabilityType




GO
/****** Object:  View [dbo].[show_stored_procedures]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[show_stored_procedures]
AS

SELECT TOP 100 PERCENT
	p.name[procedure_name],
	s.name [parameter_name],
	t.name [type],
	CASE WHEN s.is_output = 1 THEN 'OUTPUT' ELSE 'INPUT' END [direction]

FROM sys.procedures p 
inner join sys.parameters s on p.object_id = s.object_id
inner join sys.types t on s.system_type_id = t.system_type_id

WHERE p.name NOT LIKE 'sp_%' 

ORDER BY p.name


GO
/****** Object:  View [dbo].[vw_aspnet_Applications]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_Applications]
  AS SELECT [dbo].[aspnet_Applications].[ApplicationName], [dbo].[aspnet_Applications].[LoweredApplicationName], [dbo].[aspnet_Applications].[ApplicationId], [dbo].[aspnet_Applications].[Description]
  FROM [dbo].[aspnet_Applications]
  

GO
/****** Object:  View [dbo].[vw_aspnet_Users]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_Users]
  AS SELECT [dbo].[aspnet_Users].[ApplicationId], [dbo].[aspnet_Users].[UserId], [dbo].[aspnet_Users].[UserName], [dbo].[aspnet_Users].[LoweredUserName], [dbo].[aspnet_Users].[MobileAlias], [dbo].[aspnet_Users].[IsAnonymous], [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Users]
  

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [aspnet_Users_Index2]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE NONCLUSTERED INDEX [aspnet_Users_Index2] ON [dbo].[aspnet_Users]
(
	[ApplicationId] ASC,
	[LastActivityDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AssetTypes]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AssetTypes] ON [dbo].[AssetTypes]
(
	[AssetType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Clients]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Clients] ON [dbo].[Clients]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ProductTypes]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductTypes] ON [dbo].[ProductTypes]
(
	[ProductType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ServiceLevels]    Script Date: 03/03/15 1:01:40 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ServiceLevels] ON [dbo].[ServiceLevels]
(
	[ServiceLevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdviserCapabilityLinks] ADD  CONSTRAINT [DF_AdviserCapabililtyLinks_Capable]  DEFAULT ((0)) FOR [Capable]
GO
ALTER TABLE [dbo].[Expenses] ADD  CONSTRAINT [DF_Expenses_Frequency]  DEFAULT ((0)) FOR [Frequency]
GO
ALTER TABLE [dbo].[Expenses] ADD  CONSTRAINT [DF_Expenses_Amount]  DEFAULT ((0)) FOR [Amount]
GO
ALTER TABLE [dbo].[ProfileProductLinks] ADD  CONSTRAINT [DF_ProfileProductLinks_Selected]  DEFAULT ((0)) FOR [Selected]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_CostPerUnit]  DEFAULT ((0)) FOR [CostPerUnit]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Table_1_TransactionFee]  DEFAULT ((0)) FOR [BrokerageFee]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_StampDuty]  DEFAULT ((0)) FOR [StampDuty]
GO
ALTER TABLE [dbo].[AdviserCapabilityLinks]  WITH CHECK ADD  CONSTRAINT [FK_AdviserCapabililtyLinks_Advisers] FOREIGN KEY([AdviserID])
REFERENCES [dbo].[Advisers] ([AdviserID])
GO
ALTER TABLE [dbo].[AdviserCapabilityLinks] CHECK CONSTRAINT [FK_AdviserCapabililtyLinks_Advisers]
GO
ALTER TABLE [dbo].[AdviserCapabilityLinks]  WITH CHECK ADD  CONSTRAINT [FK_AdviserCapabililtyLinks_CapabilityTypes] FOREIGN KEY([CapabilityTypeID])
REFERENCES [dbo].[CapabilityTypes] ([CapabilityTypeID])
GO
ALTER TABLE [dbo].[AdviserCapabilityLinks] CHECK CONSTRAINT [FK_AdviserCapabililtyLinks_CapabilityTypes]
GO
ALTER TABLE [dbo].[Advisers]  WITH CHECK ADD  CONSTRAINT [FK_Advisers_DealerGroups] FOREIGN KEY([DealerGroupID])
REFERENCES [dbo].[DealerGroups] ([DealerGroupID])
GO
ALTER TABLE [dbo].[Advisers] CHECK CONSTRAINT [FK_Advisers_DealerGroups]
GO
ALTER TABLE [dbo].[Advisers]  WITH CHECK ADD  CONSTRAINT [FK_Advisers_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Advisers] CHECK CONSTRAINT [FK_Advisers_Users]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Advisers] FOREIGN KEY([AdviserID])
REFERENCES [dbo].[Advisers] ([AdviserID])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Advisers]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Clients]
GO
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[Attachments]  WITH CHECK ADD  CONSTRAINT [FK_Attachments_Notes] FOREIGN KEY([NoteID])
REFERENCES [dbo].[Notes] ([NoteID])
GO
ALTER TABLE [dbo].[Attachments] CHECK CONSTRAINT [FK_Attachments_Notes]
GO
ALTER TABLE [dbo].[AustEquityCurrentScores]  WITH CHECK ADD  CONSTRAINT [FK_AustEquityCurrentScores_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[AustEquityCurrentScores] CHECK CONSTRAINT [FK_AustEquityCurrentScores_Stocks]
GO
ALTER TABLE [dbo].[AustEquityHistoricalScores]  WITH CHECK ADD  CONSTRAINT [FK_AustEquityHistoricalScores_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[AustEquityHistoricalScores] CHECK CONSTRAINT [FK_AustEquityHistoricalScores_Stocks]
GO
ALTER TABLE [dbo].[Beneficiaries]  WITH CHECK ADD  CONSTRAINT [FK_Beneficiaries_Policies] FOREIGN KEY([PolicyID])
REFERENCES [dbo].[Policies] ([PolicyID])
GO
ALTER TABLE [dbo].[Beneficiaries] CHECK CONSTRAINT [FK_Beneficiaries_Policies]
GO
ALTER TABLE [dbo].[Bookmarks]  WITH CHECK ADD  CONSTRAINT [FK_Bookmarks_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Bookmarks] CHECK CONSTRAINT [FK_Bookmarks_Users]
GO
ALTER TABLE [dbo].[CapabilityTypes]  WITH CHECK ADD  CONSTRAINT [FK_CapabilityTypes_CapabilityGroups] FOREIGN KEY([CapabilityGroupID])
REFERENCES [dbo].[CapabilityGroups] ([CapabilityGroupID])
GO
ALTER TABLE [dbo].[CapabilityTypes] CHECK CONSTRAINT [FK_CapabilityTypes_CapabilityGroups]
GO
ALTER TABLE [dbo].[CashHoldings]  WITH CHECK ADD  CONSTRAINT [FK_CashHoldings_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[CashHoldings] CHECK CONSTRAINT [FK_CashHoldings_AssetTypes]
GO
ALTER TABLE [dbo].[CashHoldings]  WITH CHECK ADD  CONSTRAINT [FK_CashHoldings_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[CashHoldings] CHECK CONSTRAINT [FK_CashHoldings_Clients]
GO
ALTER TABLE [dbo].[ClientFileInformations]  WITH CHECK ADD  CONSTRAINT [FK_ClientFileInformations_ClientFileInformations] FOREIGN KEY([ClientFileID])
REFERENCES [dbo].[ClientFileInformations] ([ClientFileID])
GO
ALTER TABLE [dbo].[ClientFileInformations] CHECK CONSTRAINT [FK_ClientFileInformations_ClientFileInformations]
GO
ALTER TABLE [dbo].[ClientGroups]  WITH CHECK ADD  CONSTRAINT [FK_ClientGroups_ServiceLevels] FOREIGN KEY([ServiceLevelID])
REFERENCES [dbo].[ServiceLevels] ([ServiceLevelID])
GO
ALTER TABLE [dbo].[ClientGroups] CHECK CONSTRAINT [FK_ClientGroups_ServiceLevels]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_Users]
GO
ALTER TABLE [dbo].[ComplianceIncomeExpenses]  WITH CHECK ADD  CONSTRAINT [FK_ComplianceIncomeExpenses_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[ComplianceIncomeExpenses] CHECK CONSTRAINT [FK_ComplianceIncomeExpenses_Clients]
GO
ALTER TABLE [dbo].[DailyPrices]  WITH CHECK ADD  CONSTRAINT [FK_DailyPrices_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[DailyPrices] CHECK CONSTRAINT [FK_DailyPrices_Stocks]
GO
ALTER TABLE [dbo].[Dependants]  WITH CHECK ADD  CONSTRAINT [FK_Dependants_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[Dependants] CHECK CONSTRAINT [FK_Dependants_Clients]
GO
ALTER TABLE [dbo].[DirectProperties]  WITH CHECK ADD  CONSTRAINT [FK_RealEstates_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[DirectProperties] CHECK CONSTRAINT [FK_RealEstates_AssetTypes]
GO
ALTER TABLE [dbo].[DirectProperties]  WITH CHECK ADD  CONSTRAINT [FK_RealEstates_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[DirectProperties] CHECK CONSTRAINT [FK_RealEstates_Clients]
GO
ALTER TABLE [dbo].[DividendHistories]  WITH CHECK ADD  CONSTRAINT [FK_DividendHistories_Currencies] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[Currencies] ([CurrencyID])
GO
ALTER TABLE [dbo].[DividendHistories] CHECK CONSTRAINT [FK_DividendHistories_Currencies]
GO
ALTER TABLE [dbo].[DividendHistories]  WITH CHECK ADD  CONSTRAINT [FK_DividendHistories_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[DividendHistories] CHECK CONSTRAINT [FK_DividendHistories_Stocks]
GO
ALTER TABLE [dbo].[EducationRecords]  WITH CHECK ADD  CONSTRAINT [FK_EducationRecords_aspnet_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[EducationRecords] CHECK CONSTRAINT [FK_EducationRecords_aspnet_Users]
GO
ALTER TABLE [dbo].[EmploymentRecords]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentRecords_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[EmploymentRecords] CHECK CONSTRAINT [FK_EmploymentRecords_Users]
GO
ALTER TABLE [dbo].[Expenses]  WITH CHECK ADD  CONSTRAINT [FK_Expenses_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [FK_Expenses_AssetTypes]
GO
ALTER TABLE [dbo].[Expenses]  WITH CHECK ADD  CONSTRAINT [FK_Expenses_ExpenseTypes] FOREIGN KEY([ExpenseTypeID])
REFERENCES [dbo].[ExpenseTypes] ([ExpenseTypeID])
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [FK_Expenses_ExpenseTypes]
GO
ALTER TABLE [dbo].[FixedIncomeHoldings]  WITH CHECK ADD  CONSTRAINT [FK_FixedIncomeHoldings_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[FixedIncomeHoldings] CHECK CONSTRAINT [FK_FixedIncomeHoldings_AssetTypes]
GO
ALTER TABLE [dbo].[FixedIncomeHoldings]  WITH CHECK ADD  CONSTRAINT [FK_FixedIncomeHoldings_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[FixedIncomeHoldings] CHECK CONSTRAINT [FK_FixedIncomeHoldings_Clients]
GO
ALTER TABLE [dbo].[FundStocks]  WITH CHECK ADD  CONSTRAINT [FK_FundStocks_Currencies] FOREIGN KEY([PortfolioCurrencyID])
REFERENCES [dbo].[Currencies] ([CurrencyID])
GO
ALTER TABLE [dbo].[FundStocks] CHECK CONSTRAINT [FK_FundStocks_Currencies]
GO
ALTER TABLE [dbo].[FundStocks]  WITH CHECK ADD  CONSTRAINT [FK_FundStocks_Currencies1] FOREIGN KEY([FundSizeBaseCurrencyID])
REFERENCES [dbo].[Currencies] ([CurrencyID])
GO
ALTER TABLE [dbo].[FundStocks] CHECK CONSTRAINT [FK_FundStocks_Currencies1]
GO
ALTER TABLE [dbo].[FundStocks]  WITH CHECK ADD  CONSTRAINT [FK_FundStocks_FinancialHealthGrades] FOREIGN KEY([FinancialHealthGradeID])
REFERENCES [dbo].[FinancialHealthGrades] ([FinancialHealthGradeID])
GO
ALTER TABLE [dbo].[FundStocks] CHECK CONSTRAINT [FK_FundStocks_FinancialHealthGrades]
GO
ALTER TABLE [dbo].[FundStocks]  WITH CHECK ADD  CONSTRAINT [FK_FundStocks_FundStructures] FOREIGN KEY([FundStructureID])
REFERENCES [dbo].[FundStructures] ([FundStructureID])
GO
ALTER TABLE [dbo].[FundStocks] CHECK CONSTRAINT [FK_FundStocks_FundStructures]
GO
ALTER TABLE [dbo].[FundStocks]  WITH CHECK ADD  CONSTRAINT [FK_FundStocks_GlobalCategoryGroups] FOREIGN KEY([GlobalCategoryGroupID])
REFERENCES [dbo].[GlobalCategoryGroups] ([GlobalCategoryGroupID])
GO
ALTER TABLE [dbo].[FundStocks] CHECK CONSTRAINT [FK_FundStocks_GlobalCategoryGroups]
GO
ALTER TABLE [dbo].[FundStocks]  WITH CHECK ADD  CONSTRAINT [FK_FundStocks_Strategies] FOREIGN KEY([InvestmentStrategyID])
REFERENCES [dbo].[Strategies] ([StrategyID])
GO
ALTER TABLE [dbo].[FundStocks] CHECK CONSTRAINT [FK_FundStocks_Strategies]
GO
ALTER TABLE [dbo].[Industries]  WITH CHECK ADD  CONSTRAINT [FK_Industries_Sectors] FOREIGN KEY([SectorID])
REFERENCES [dbo].[Sectors] ([SectorID])
GO
ALTER TABLE [dbo].[Industries] CHECK CONSTRAINT [FK_Industries_Sectors]
GO
ALTER TABLE [dbo].[InterEquityCurrentScores]  WITH CHECK ADD  CONSTRAINT [FK_InterEquityCurrentScores_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[InterEquityCurrentScores] CHECK CONSTRAINT [FK_InterEquityCurrentScores_Stocks]
GO
ALTER TABLE [dbo].[InterEquityHistoricalScores]  WITH CHECK ADD  CONSTRAINT [FK_InterEquityHistoricalScores_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[InterEquityHistoricalScores] CHECK CONSTRAINT [FK_InterEquityHistoricalScores_Stocks]
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD  CONSTRAINT [FK_Loans_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[Loans] CHECK CONSTRAINT [FK_Loans_AssetTypes]
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD  CONSTRAINT [FK_Loans_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[Loans] CHECK CONSTRAINT [FK_Loans_Clients]
GO
ALTER TABLE [dbo].[ManagedFundsCurrentScores]  WITH CHECK ADD  CONSTRAINT [FK_ManagedFundsCurrentScores_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[ManagedFundsCurrentScores] CHECK CONSTRAINT [FK_ManagedFundsCurrentScores_Stocks]
GO
ALTER TABLE [dbo].[ManagedFundsHistoricalScores]  WITH CHECK ADD  CONSTRAINT [FK_ManagedFundsHistoricalScores_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[ManagedFundsHistoricalScores] CHECK CONSTRAINT [FK_ManagedFundsHistoricalScores_Stocks]
GO
ALTER TABLE [dbo].[MarginLoans]  WITH CHECK ADD  CONSTRAINT [FK_MarginLoans_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[MarginLoans] CHECK CONSTRAINT [FK_MarginLoans_AssetTypes]
GO
ALTER TABLE [dbo].[MarginLoans]  WITH CHECK ADD  CONSTRAINT [FK_MarginLoans_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[MarginLoans] CHECK CONSTRAINT [FK_MarginLoans_Clients]
GO
ALTER TABLE [dbo].[MorningStarRatings]  WITH CHECK ADD  CONSTRAINT [FK_MorningStarRatings_MorningStarCategories] FOREIGN KEY([MorningStarCategoryID])
REFERENCES [dbo].[MorningStarCategories] ([MorningStarCategoryID])
GO
ALTER TABLE [dbo].[MorningStarRatings] CHECK CONSTRAINT [FK_MorningStarRatings_MorningStarCategories]
GO
ALTER TABLE [dbo].[MorningStarRatings]  WITH CHECK ADD  CONSTRAINT [FK_MorningStarRatings_MorningStarRatings] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[MorningStarRatings] CHECK CONSTRAINT [FK_MorningStarRatings_MorningStarRatings]
GO
ALTER TABLE [dbo].[NoteLinks]  WITH CHECK ADD  CONSTRAINT [FK_NoteReferences_Notes1] FOREIGN KEY([NoteID2])
REFERENCES [dbo].[Notes] ([NoteID])
GO
ALTER TABLE [dbo].[NoteLinks] CHECK CONSTRAINT [FK_NoteReferences_Notes1]
GO
ALTER TABLE [dbo].[NoteLinks]  WITH CHECK ADD  CONSTRAINT [FK_NoteReferences_Notes2] FOREIGN KEY([NoteID1])
REFERENCES [dbo].[Notes] ([NoteID])
GO
ALTER TABLE [dbo].[NoteLinks] CHECK CONSTRAINT [FK_NoteReferences_Notes2]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Advisers] FOREIGN KEY([AdviserID])
REFERENCES [dbo].[Advisers] ([AdviserID])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Advisers]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_AssetTypes]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Clients]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_NoteTypes] FOREIGN KEY([NoteTypesID])
REFERENCES [dbo].[NoteTypes] ([NoteTypeID])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_NoteTypes]
GO
ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Clients]
GO
ALTER TABLE [dbo].[PolicyDetails]  WITH CHECK ADD  CONSTRAINT [FK_PolicyInformation_Policies] FOREIGN KEY([PolicyDetailID])
REFERENCES [dbo].[Policies] ([PolicyID])
GO
ALTER TABLE [dbo].[PolicyDetails] CHECK CONSTRAINT [FK_PolicyInformation_Policies]
GO
ALTER TABLE [dbo].[ProfileProductLinks]  WITH CHECK ADD  CONSTRAINT [FK_ProfileProductLinks_ProductTypes] FOREIGN KEY([ProductTypeID])
REFERENCES [dbo].[ProductTypes] ([ProductTypeID])
GO
ALTER TABLE [dbo].[ProfileProductLinks] CHECK CONSTRAINT [FK_ProfileProductLinks_ProductTypes]
GO
ALTER TABLE [dbo].[ProfileProductLinks]  WITH CHECK ADD  CONSTRAINT [FK_ProfileProductLinks_RiskProfiles] FOREIGN KEY([RiskProfileID])
REFERENCES [dbo].[RiskProfiles] ([RiskProfileID])
GO
ALTER TABLE [dbo].[ProfileProductLinks] CHECK CONSTRAINT [FK_ProfileProductLinks_RiskProfiles]
GO
ALTER TABLE [dbo].[Qualifications]  WITH CHECK ADD  CONSTRAINT [FK_Qualifications_QualificationTypes] FOREIGN KEY([QualificationTypeID])
REFERENCES [dbo].[QualificationTypes] ([QualificationTypeID])
GO
ALTER TABLE [dbo].[Qualifications] CHECK CONSTRAINT [FK_Qualifications_QualificationTypes]
GO
ALTER TABLE [dbo].[Qualifications]  WITH CHECK ADD  CONSTRAINT [FK_Qualifications_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Qualifications] CHECK CONSTRAINT [FK_Qualifications_Users]
GO
ALTER TABLE [dbo].[Ratios]  WITH CHECK ADD  CONSTRAINT [FK_Ratios_Ratios] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[Ratios] CHECK CONSTRAINT [FK_Ratios_Ratios]
GO
ALTER TABLE [dbo].[Ratios]  WITH CHECK ADD  CONSTRAINT [FK_Ratios_RatioTypes] FOREIGN KEY([RatioTypeID])
REFERENCES [dbo].[RatioTypes] ([RatioTypeID])
GO
ALTER TABLE [dbo].[Ratios] CHECK CONSTRAINT [FK_Ratios_RatioTypes]
GO
ALTER TABLE [dbo].[RiskProfiles]  WITH CHECK ADD  CONSTRAINT [FK_RiskProfiles_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[RiskProfiles] CHECK CONSTRAINT [FK_RiskProfiles_Clients]
GO
ALTER TABLE [dbo].[Securities]  WITH CHECK ADD  CONSTRAINT [FK_Securities_Securities] FOREIGN KEY([MarginLoanID])
REFERENCES [dbo].[MarginLoans] ([MarginLoanID])
GO
ALTER TABLE [dbo].[Securities] CHECK CONSTRAINT [FK_Securities_Securities]
GO
ALTER TABLE [dbo].[Statements]  WITH CHECK ADD  CONSTRAINT [FK_Statements_Statements] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[Statements] CHECK CONSTRAINT [FK_Statements_Statements]
GO
ALTER TABLE [dbo].[StockDistributions]  WITH CHECK ADD  CONSTRAINT [FK_StockDistributions_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[StockDistributions] CHECK CONSTRAINT [FK_StockDistributions_Clients]
GO
ALTER TABLE [dbo].[StockDistributions]  WITH CHECK ADD  CONSTRAINT [FK_StockDistributions_StockHoldings] FOREIGN KEY([StockHoldingID])
REFERENCES [dbo].[StockHoldings] ([StockHoldingID])
GO
ALTER TABLE [dbo].[StockDistributions] CHECK CONSTRAINT [FK_StockDistributions_StockHoldings]
GO
ALTER TABLE [dbo].[StockDistributions]  WITH CHECK ADD  CONSTRAINT [FK_StockDistributions_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[StockDistributions] CHECK CONSTRAINT [FK_StockDistributions_Stocks]
GO
ALTER TABLE [dbo].[StockHoldings]  WITH CHECK ADD  CONSTRAINT [FK_StockHoldings_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[StockHoldings] CHECK CONSTRAINT [FK_StockHoldings_AssetTypes]
GO
ALTER TABLE [dbo].[StockHoldings]  WITH CHECK ADD  CONSTRAINT [FK_StockHoldings_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[StockHoldings] CHECK CONSTRAINT [FK_StockHoldings_Clients]
GO
ALTER TABLE [dbo].[StockHoldings]  WITH CHECK ADD  CONSTRAINT [FK_StockHoldings_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[StockHoldings] CHECK CONSTRAINT [FK_StockHoldings_Stocks]
GO
ALTER TABLE [dbo].[StockHoldings]  WITH CHECK ADD  CONSTRAINT [FK_StockHoldings_StockTransactions] FOREIGN KEY([LastTransactionID])
REFERENCES [dbo].[StockTransactions] ([StockTransactionID])
GO
ALTER TABLE [dbo].[StockHoldings] CHECK CONSTRAINT [FK_StockHoldings_StockTransactions]
GO
ALTER TABLE [dbo].[Stocks]  WITH CHECK ADD  CONSTRAINT [FK_Stocks_Currencies] FOREIGN KEY([BaseCurrencyID])
REFERENCES [dbo].[Currencies] ([CurrencyID])
GO
ALTER TABLE [dbo].[Stocks] CHECK CONSTRAINT [FK_Stocks_Currencies]
GO
ALTER TABLE [dbo].[Stocks]  WITH CHECK ADD  CONSTRAINT [FK_Stocks_Exchanges] FOREIGN KEY([ExchangeID])
REFERENCES [dbo].[Exchanges] ([ExchangeID])
GO
ALTER TABLE [dbo].[Stocks] CHECK CONSTRAINT [FK_Stocks_Exchanges]
GO
ALTER TABLE [dbo].[Stocks]  WITH CHECK ADD  CONSTRAINT [FK_Stocks_FundStocks] FOREIGN KEY([FundStockID])
REFERENCES [dbo].[FundStocks] ([FundStockID])
GO
ALTER TABLE [dbo].[Stocks] CHECK CONSTRAINT [FK_Stocks_FundStocks]
GO
ALTER TABLE [dbo].[Stocks]  WITH CHECK ADD  CONSTRAINT [FK_Stocks_InvestmentTypes] FOREIGN KEY([InvestmentTypeID])
REFERENCES [dbo].[InvestmentTypes] ([InvestmentTypeID])
GO
ALTER TABLE [dbo].[Stocks] CHECK CONSTRAINT [FK_Stocks_InvestmentTypes]
GO
ALTER TABLE [dbo].[StockTransactions]  WITH CHECK ADD  CONSTRAINT [FK_StockTransactions_Advisers] FOREIGN KEY([AdviserID])
REFERENCES [dbo].[Advisers] ([AdviserID])
GO
ALTER TABLE [dbo].[StockTransactions] CHECK CONSTRAINT [FK_StockTransactions_Advisers]
GO
ALTER TABLE [dbo].[StockTransactions]  WITH CHECK ADD  CONSTRAINT [FK_StockTransactions_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[StockTransactions] CHECK CONSTRAINT [FK_StockTransactions_Clients]
GO
ALTER TABLE [dbo].[StockTransactions]  WITH CHECK ADD  CONSTRAINT [FK_StockTransactions_Stocks] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[StockTransactions] CHECK CONSTRAINT [FK_StockTransactions_Stocks]
GO
ALTER TABLE [dbo].[StockTransactions]  WITH CHECK ADD  CONSTRAINT [FK_StockTransactions_TransactionTypes] FOREIGN KEY([TransactionTypeID])
REFERENCES [dbo].[TransactionTypes] ([TransactionTypeID])
GO
ALTER TABLE [dbo].[StockTransactions] CHECK CONSTRAINT [FK_StockTransactions_TransactionTypes]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_AssetTypes] FOREIGN KEY([AssetTypeID])
REFERENCES [dbo].[AssetTypes] ([AssetTypeID])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_AssetTypes]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_TransactionTypes] FOREIGN KEY([TransactionTypeID])
REFERENCES [dbo].[TransactionTypes] ([TransactionTypeID])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_TransactionTypes]
GO
ALTER TABLE [dbo].[UserData]  WITH CHECK ADD  CONSTRAINT [FK_UserData_AspNetUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserData] CHECK CONSTRAINT [FK_UserData_AspNetUsers]
GO
/****** Object:  StoredProcedure [dbo].[CreateAccountNote]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 17/03/2014
-- Description:	Creates a new note related to a particular account
-- =============================================
CREATE PROCEDURE [dbo].[CreateAccountNote] 
	@ClientID nvarchar(128), 
	@AdviserID nvarchar(128),
	@AssetTypeID int = NULL, 
	@AccountID nvarchar(128) = NULL, 
	@Subject varchar(200) , 
	@Body varchar(max) , 
	@NoteSerial varchar(100), 
	@TimeSpent float = NULL, 
	@NoteTypeID int, 
	@NoteID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	SELECT @Error = 0 

	DECLARE @AccountIDExist BIT 
	SELECT @AccountIDExist = dbo.IsAccountIDExist(@AccountID, @AssetTypeID)
	
	-- Check to see that the assettypeid and accountid matches with the appropriate asset/liability type
	IF @AccountIDExist <> 1 
	BEGIN
		SELECT @Error = -1 
	END

	-- Start the actual transaction
	BEGIN TRAN
	SELECT @NoteID = NEWID()

	IF @Error = 0 
	BEGIN
	INSERT INTO dbo.Notes
		(NoteID
		,NoteTypesID
		,ClientID
		,AdviserID
		,AssetTypeID
		,AccountID
		,Subject
		,Body
		,NoteSerial
		,TimeSpent
		,DateCreated
		,DateModified)
	VALUES
		(@NoteID
		,@NoteTypeID
		,@ClientID
		,@AdviserID
		,@AssetTypeID
		,@AccountID
		,@Subject
		,@Body
		,@NoteSerial
		,@TimeSpent
		,GetDate()
		,GetDate())
	END

	-- Check for errors
	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @NoteID = NULL 
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[CreateAdviser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 21/02/2014
-- Description:	Creates an Adviser Profile
-- =============================================
CREATE PROCEDURE [dbo].[CreateAdviser] 
	@UserID				nvarchar(128), 
    @FirstName			varchar(100),
	@LastName			varchar(100),
	@DateOfBirth		datetime,
	@CurrentTimeUtc     datetime,
	@AdviserID			nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- call membership_createuser to get the useerid
	DECLARE @ReturnValue INT 
	DECLARE @ErrorCode   INT 
	DECLARE @TranStarted bit 
	DECLARE @RecordCount INT
	IF (@@TRANCOUNT = 0)
	BEGIN
		BEGIN TRANSACTION 
		SET @TranStarted = 1
	END
	ELSE
	BEGIN
		SET @TranStarted = 0
	END

	-- check to see no other user data exists
	SET @RecordCount = (SELECT COUNT(UserID) FROM dbo.UserData WHERE UserID = @UserID)
	IF @RecordCount > 0 
	BEGIN
		GOTO Cleanup
	END
		 
	-- call createuserdata to add to userdata table 
	EXEC @ReturnValue = dbo.CreateUserData
		@UserID
		,@FirstName
		,@LastName
		,@DateOfBirth
		,@CurrentTimeUtc

	IF @ReturnValue <> 0 
	BEGIN 
		SET @ErrorCode = @ReturnValue 
		GOTO Cleanup
	END

	-- check that the user id doesn't already exist in the adviser table
	SET @RecordCount = (SELECT COUNT(AdviserID) FROM dbo.Advisers WHERE UserID = @UserID)
	IF @RecordCount > 0 
	BEGIN
		SELECT @ErrorCode = -2 
		GOTO Cleanup
	END

	-- check that the user id doesn't already exist in the client table either
	SET @RecordCount = (SELECT COUNT(ClientID) FROM Clients WHERE UserID = @UserID)
	IF @RecordCount > 0 
	BEGIN
		SET @ErrorCode = -3
		GOTO Cleanup
	END

	IF @AdviserID IS NULL
	BEGIN
		SET @AdviserID = NEWID()
	END
	
	-- begin the insert work 

	INSERT INTO dbo.Advisers 
		(AdviserID
		,UserID
		,DateCreated
		,DateModified)

	VALUES
		(@AdviserID
		,@UserID
		,GetDate()
		,GetDate())

	-- Check for error
	SELECT @ErrorCode = @@ERROR 
	IF @ErrorCode <> 0 
	BEGIN 
		GOTO Cleanup
	END

	-- Insert corresponding entries into the adviser capability table 
	INSERT INTO dbo.AdviserCapabilityLinks (AdviserID, CapabilityTypeID) SELECT @AdviserID, CT.CapabilityTypeID FROM dbo.CapabilityTypes CT 
	SELECT @ErrorCode = @@ERROR 
	IF @ErrorCode <> 0 
	BEGIN 
		GOTO Cleanup 
	END

	-- Finalize transaction 
	IF @TranStarted = 1
	BEGIN
		SET @TranStarted = 0 
		COMMIT TRANSACTION 
	END

	RETURN 0 

Cleanup: 
	IF (@TranStarted = 1)
	BEGIN
		SET @TranStarted = 0 
		ROLLBACK TRANSACTION
		SET @AdviserID = NULL 
	END

	RETURN @ErrorCode

END


GO
/****** Object:  StoredProcedure [dbo].[CreateAppointment]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 09/04/2014
-- Description:	Creates a new appointment
-- =============================================
CREATE PROCEDURE [dbo].[CreateAppointment]	
	@AdviserID			nvarchar(128),
	@ClientID			nvarchar(128) = NULL, 
	@AppointmentType	varchar(50) = NULL, 
	@Title				varchar(100),
	@Details			varchar(max), 
	@AppointmentTime	datetime, 
	@DurationHours		decimal(9,2),
	@Comments			varchar(100),
	@DateCreated		datetime,
	@DateModified		datetime,
	@AppointmentID		nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @TranStarted bit 
	DECLARE @ErrorCode  int

	IF (@@TRANCOUNT = 0)
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END
	ELSE
	BEGIN
		SET @TranStarted = 0 
	END

	IF @AppointmentID IS NULL
	BEGIN
		SET @AppointmentID = NEWID()
	END 

	INSERT INTO dbo.Appointments 
		(AppointmentID
		,AdviserID
		,ClientID
		,Title
		,Details
		,AppointmentType 
		,AppointmentTime
		,DurationHours
		,Comments
		,DateCreated
		,DateModified)
	VALUES
		(@AppointmentID
		,@AdviserID
		,@ClientID
		,@Title
		,@Details
		,@AppointmentType 
		,@AppointmentTime
		,@DurationHours
		,@Comments
		,GetDate()
		,GetDate())
	
	SET @ErrorCode = @@ERROR 

	IF @ErrorCode <> 0 
	BEGIN
		GOTO Cleanup
	END

	IF (@TranStarted = 1)
	BEGIN
		SET @TranStarted = 0 
		COMMIT TRANSACTION 
	END

	RETURN @ErrorCode
Cleanup: 
	IF (@TranStarted = 1)
	BEGIN
		SET @AppointmentID = NULL
		SET @TranStarted = 0

		ROLLBACK TRANSACTION 
	END

	RETURN @ErrorCode
END


GO
/****** Object:  StoredProcedure [dbo].[CreateBeneficiary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin CHung
-- Create date: 26/03/2014
-- Description:	Creates a beneficiary entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateBeneficiary] 
	@PolicyID nvarchar(128),
	@FirstName varchar(200),
	@LastName varchar(200) , 
	@Relationship varchar(100),
	@Address1 varchar(200) = NULL,
	@Address2 varchar(200) = NULL,
	@Address3 varchar(200) = NULL,
	@City varchar(50) = NULL,
	@State varchar(50) = NULL,
	@PostCode varchar(10) = NULL,
	@Phone varchar(50) = NULL, 
	@Mobile varchar(50) = NULL,
	@Email varchar(100) = NULL, 
	@BeneficiaryPercentage float,
	@BeneficiaryID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	DECLARE @PolicyCount INT 

	SELECT @PolicyCount = COUNT(PolicyID) FROM dbo.Policies WHERE PolicyID = @PolicyID 
	
	IF @PolicyCount <> 1
	BEGIN
		SELECT @Error = -1 
	END

	SELECT @BeneficiaryID = NEWID()

	BEGIN TRAN
	IF @Error = 0 
	BEGIN
		INSERT INTO dbo.Beneficiaries
			(BeneficiaryID
			,PolicyID
			,FirstName
			,LastName
			,Relationship
			,Address1
			,Address2
			,Address3
			,City
			,State
			,PostCode
			,Phone
			,Mobile
			,Email
			,BeneficiaryPercentage
			,DateCreated
			,DateModified)
		VALUES
			(@BeneficiaryID
			,@PolicyID
			,@FirstName
			,@LastName
			,@Relationship
			,@Address1
			,@Address2
			,@Address3
			,@City
			,@State
			,@PostCode
			,@Phone
			,@Mobile
			,@Email
			,@BeneficiaryPercentage
			,GetDate()
			,GetDate())

		SELECT @Error = @@ERROR 

	END
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		SELECT @BeneficiaryID = NULL
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateBookmark]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 03/04/2014
-- Description:	Creates a new bookmark entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateBookmark] 
	@UserID nvarchar(128),
	@BookmarkName varchar(100), 
	@BookmarkLink varchar(100),
	@Comments varchar(500),
	@BookmarkID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	SELECT @BookmarkID = NEWID()

	BEGIN TRAN
	INSERT INTO dbo.Bookmarks
		(BookmarkID
		,UserID
		,BookmarkName
		,BookmarkLink
		,Comments
		,DateCreated
		,DateModified)
	VALUES
		(@BookmarkID
		,@UserID
		,@BookmarkName
		,@BookmarkLink
		,@Comments
		,GetDate()
		,GetDate())
		
	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @BookmarkID = NULL 
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateCapabilityGroup]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/02/2014
-- Description:	Creates a new capability group
-- =============================================
CREATE PROCEDURE [dbo].[CreateCapabilityGroup] 
	-- Add the parameters for the stored procedure here
	@CapabilityGroup varchar(100),
	@CapabilityID INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT

	BEGIN TRAN

	INSERT INTO dbo.CapabilityGroups
		(CapabilityGroup) 
	VALUES (@CapabilityGroup)

	SELECT @Error = @@ERROR  

	IF @Error = 0 
	BEGIN 
		SELECT @CapabilityID = SCOPE_IDENTITY()
		SELECT @Error = @@ERROR 
	END

	IF @Error = 0 
	BEGIN 
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateCapabilityType]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/02/2014
-- Description:	Creates a capability type entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateCapabilityType] 
	-- Add the parameters for the stored procedure here
	@CapabilityGroupID int,
	@CapabilityType varchar(100),
	@CapabilityTypeID int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN

	INSERT INTO dbo.CapabilityTypes
		(CapabilityGroupID
		,CapabilityType)
	VALUES
		(@CapabilityGroupID 
		,@CapabilityType)

	SELECT @Error = @@ERROR

	IF @Error = 0 
	BEGIN
		SELECT @CapabilityTypeID = SCOPE_IDENTITY()
		SELECT @Error = @@ERROR 
	END
	
	IF @Error = 0 
	BEGIN
		-- Insert corresponding entry into the adviser-capability link table 
		INSERT INTO dbo.AdviserCapabilityLinks (AdviserID, CapabilityTypeID) SELECT A.AdviserID , @CapabilityTypeID FROM dbo.Advisers A 
		SELECT @Error = @@ERROR 
	END

	IF @Error = 0
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN 
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateCashHolding]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Creates a new cash holdings
-- =============================================
CREATE PROCEDURE [dbo].[CreateCashHolding] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128),
	@InterestRate decimal(9,2) = NULL,
	@Frequency decimal(9,2) = NULL,
	@NextPayment date = NULL,
	@DebitInterest decimal(9,2) = NULL,
	@DateOpened date ,
	@StatementMethod varchar(50),
	@CashHoldingsID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT

	-- Check for AssetTypeID, if it doesn't exist, enter it in
    DECLARE @AssetTypeID INT 
	SELECT @AssetTypeID = AssetTypeID FROM dbo.AssetTypes WHERE AssetType = 'Cash Holding'
	SELECT @Error = 0 

	IF @AssetTypeID IS NULL OR @AssetTypeID = 0 
	BEGIN
		INSERT INTO dbo.AssetTypes (AssetType) SELECT 'Cash Holding' 
		SELECT @AssetTypeID = SCOPE_IDENTITY()
		SELECT @Error = @@ERROR 

		IF NOT @AssetTypeID > 0
		BEGIN 
			SELECT @Error = -1 	
		END
	END

	BEGIN TRAN
	IF @Error = 0 
	BEGIN
		SELECT @CashHoldingsID = NEWID()
		
		INSERT INTO dbo.CashHoldings
			(CashHoldingsID
			,ClientID
			,AssetTypeID
			,InterestRate
			,Frequency
			,NextPayment
			,DebitInterest
			,DateOpened
			,StatementMethod
			,DateCreated
			,DateModified)
		VALUES
			(@CashHoldingsID
			,@ClientID
			,@AssetTypeID
			,@InterestRate
			,@Frequency
			,@NextPayment
			,@DebitInterest
			,@DateOpened
			,@StatementMethod
			,GetDate()
			,GetDate())

		SELECT @Error = @@ERROR 
	END

	-- Check for errors
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @CashHoldingsID = NULL
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateClient]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 21/02/2014
-- Description:	Creates a Client Profile for a particular userID
-- =============================================
CREATE PROCEDURE [dbo].[CreateClient] 
	@UserID				nvarchar(128),
	@ClientGroupID		nvarchar(128),
    @FirstName			varchar(100),
	@LastName			varchar(100),
	@DateOfBirth		datetime,
	@DesignationAccount	varchar(100) = NULL,
	@CurrentTimeUtc     datetime,
	@ClientID			nvarchar(128) = NULL OUTPUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- call membership_createuser to get the useerid
	DECLARE @ReturnValue INT 
	DECLARE @ErrorCode   INT 
	DECLARE @TranStarted bit 
	DECLARE @RecordCount INT

	IF (@@TRANCOUNT = 0)
	BEGIN
		BEGIN TRANSACTION 
		SET @TranStarted = 1
	END
	ELSE
	BEGIN
		SET @TranStarted = 0
	END
	
	-- check that there's no current userdata in the database
	SET @RecordCount = (SELECT COUNT(UserID) FROM dbo.UserData WHERE UserID = @UserID)
	IF @RecordCount > 0 
	BEGIN
		SET @ErrorCode = -1
		GOTO Cleanup
	END

	-- call createuserdata to add to userdata table 
	EXEC @ReturnValue = dbo.CreateUserData
		@UserID
		,@FirstName
		,@LastName
		,@DateOfBirth
		,@CurrentTimeUtc

	IF @ReturnValue <> 0 
	BEGIN 
		SET @ErrorCode = @ReturnValue 
		GOTO Cleanup
	END

	-- Check to make sure that the user isn't already part of another client entry
	SET @RecordCount = (SELECT COUNT(ClientID) FROM Clients WHERE UserID = @UserID)

	-- User ID already identified
	IF @RecordCount > 0 
	BEGIN
		SELECT @ErrorCode = -2
		GOTO Cleanup
	END

	-- Check the advisers table to see that this user address isn't an adviser
	SET @RecordCount = (SELECT COUNT(AdviserID) FROM dbo.Advisers WHERE UserID = @UserID)
	-- User ID already identified
	IF @RecordCount > 0 
	BEGIN
		SELECT @ErrorCode = -3
		GOTO Cleanup
	END

	-- start the actual transaction 	
	IF @ClientID IS NULL 
	BEGIN
		SELECT @ClientID = NEWID()
	END
	
	INSERT INTO dbo.Clients
		(ClientID
		,UserID
		,ClientGroupID
		,DesignationAccount
		,DateCreated
		,DateModified)
	VALUES
		(@ClientID
		,@UserID
		,@ClientGroupID
		,@DesignationAccount
		,GetDate()
		,GetDate())
		
	-- Check for errors 
	SELECT @ErrorCode = @@ERROR
	IF @ErrorCode <> 0
	BEGIN
		GOTO Cleanup
	END

	-- set this clientid as the mainclientid of the clientgroup

	UPDATE dbo.ClientGroups 
	SET 
		MainClientID = @ClientID
		,DateModified = GetDate()

	WHERE ClientGroupID = @ClientGroupID

	-- Check for errors
	SELECT @ErrorCode = @@ERROR 
	IF @ErrorCode <> 0 
	BEGIN
		GOTO Cleanup
	END

	-- Finalize transaction 
	IF (@TranStarted = 1)
	BEGIN
		SET @TranStarted = 0 
		COMMIT TRANSACTION 
	END
	RETURN 0 
	
Cleanup: 
	IF (@TranStarted = 1)
	BEGIN
		SET @TranStarted = 0 
		ROLLBACK TRANSACTION
		SET @ClientID = NULL 
	END

	RETURN @ErrorCode
END


GO
/****** Object:  StoredProcedure [dbo].[CreateClientFileInformation]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 01 May 2014
-- Description:	Creates a new client file information record
-- =============================================
CREATE PROCEDURE [dbo].[CreateClientFileInformation]
	@ClientID nvarchar(128),
	@ClientActionID int, 
	@Response varchar(100),
	@ResponseDate DateTime = NULL, 
	@ClientFileID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ErrorCode INT 
	DECLARE @TransStarted BIT 
	DECLARE @RecordCount INT 

	IF (@@TRANCOUNT = 0)
	BEGIN
		SET @TransStarted = 1 
		BEGIN TRANSACTION 
	END

	-- check that the corresponding ClientID and ClientActionID doesn't already have a match, if so, do an update instead
	SET @RecordCount = (SELECT COUNT(ClientFileID) FROM dbo.ClientFileInformations WHERE ClientID = @ClientID AND ClientActionID = @ClientActionID )

	IF @RecordCount > 1
	BEGIN
		SET @ErrorCode = -1
		GOTO Cleanup
	END 

	-- single record exists, just update it 
	IF @RecordCount = 1 
	BEGIN
		SET @ClientFileID = (SELECT ClientFileID FROM dbo.ClientFileInformations WHERE ClientID = @ClientID AND ClientActionID = @ClientActionID)
		EXEC @ErrorCode = dbo.UpdateClientFileInformation @ClientFileID, @Response, @ResponseDate 

	END

	-- no record exists, insert it 
	IF @RecordCount = 0 
	BEGIN 
		SET @ClientFileID = NEWID()
		INSERT INTO dbo.ClientFileInformations 
			(ClientFileID
			,ClientID
			,ClientActionID
			,Response
			,ResponseDate
			,DateCreated
			,DateModified)
		VALUES
			(@ClientFileID
			,@ClientID
			,@ClientActionID
			,@Response
			,@ResponseDate
			,GetDate()
			,GetDate())

		SET @ErrorCode = @@ERROR 
	END

	-- Check for errors and finalize the transaction 
	IF @ErrorCode <> 0 
	BEGIN 
		GOTO Cleanup 
	END

	IF @TransStarted = 1
	BEGIN
		COMMIT TRANSACTION 
		SET @TransStarted = 0 
	END

	RETURN 0 
Cleanup: 
	
	IF @TransStarted = 1
	BEGIN
		ROLLBACK TRANSACTION 
		SET @ClientFileID = NULL
	END 

	RETURN @ErrorCode
END


GO
/****** Object:  StoredProcedure [dbo].[CreateClientGroup]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06 May 2014
-- Description:	Creates a new ClientGroup entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateClientGroup]
	@AdviserID		nvarchar(128) = NULL,
	@Alias			varchar(100),
	@AccountNo		int				 = NULL OUTPUT, 
	@ClientGroupID	nvarchar(128) = NULL OUTPUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error			int
	DECLARE @TransStarted	bit 
	DECLARE @RecordCount	int
	
	-- check that the accountNo or alias are not currently in use
	SELECT @RecordCount = COUNT(@ClientGroupID) FROM dbo.ClientGroups WHERE AccountNo = @AccountNo OR Alias LIKE '%' + @Alias + '%'

	IF @RecordCount > 0 
	BEGIN
		SET @Error = -1 
		GOTO Cleanup
	END

	SELECT @AccountNo = MAX(AccountNo) FROM dbo.ClientGroups 

	-- if no valid accountNo has been created or if it's less than the minimum value, set it to the minimum
	IF @AccountNo IS NULL OR @AccountNo < 10001
	BEGIN
		SELECT @AccountNo = 10001
	END
	-- if valid accountNo exists then increment the accountno by 1
	ELSE
	BEGIN
		SELECT @AccountNo = @AccountNo + 1 
	END

	-- check again to make sure it is valid
	IF @AccountNo IS NULL OR @AccountNo < 10001 
	BEGIN
		SET @Error = -2 
		GOTO Cleanup
	END

	IF @@TRANCOUNT = 0 
	BEGIN
		SET @TransStarted = 1
		BEGIN TRANSACTION
	END

	-- do the actual import
	IF @ClientGroupID IS NULL
	BEGIN
		SELECT @ClientGroupID = NEWID()
	END

	INSERT INTO dbo.ClientGroups	
		(ClientGroupID
		,AdviserID
		,Alias
		,AccountNo
		,DateCreated
		,DateModified)
	VALUES
		(@ClientGroupID
		,@AdviserID
		,@Alias
		,@AccountNo
		,GetDate()
		,GetDate())

	SELECT @Error = @@ERROR 

	-- check for errors
	IF @Error <> 0 
	BEGIN
		GOTO Cleanup
	END

	-- Finalize transaction
	IF @TransStarted = 1
	BEGIN
		COMMIT TRANSACTION 
		SET @TransStarted = 0 
	END

	RETURN 0 

Cleanup:
	IF @TransStarted = 1
	BEGIN
		ROLLBACK TRANSACTION
		SET @TransStarted = 0
		SET @ClientGroupID = NULL
		SET @AccountNo = 0
	END

	RETURN @Error

END


GO
/****** Object:  StoredProcedure [dbo].[CreateComplianceIncomeExpense]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 30 Apr 2014
-- Description:	Creates a new compliance expenses and income records
-- =============================================
CREATE PROCEDURE [dbo].[CreateComplianceIncomeExpense]
	@ClientID				nvarchar(128), 
	@Centrelink				decimal(9,2) = NULL,
	@ReceivedMaintenance	decimal(9,2) = NULL,
	@SuperannuationPension	decimal(9,2) = NULL,
	@OtherTaxableIncome		decimal(9,2) = NULL,
	@OtherForeignIncome		decimal(9,2) = NULL,
	@NonTaxableIncome		decimal(9,2) = NULL,
	@FoodExpenses			decimal(9,2) = NULL,
	@ClothingExpenses		decimal(9,2) = NULL,
	@MedicalExpenses		decimal(9,2) = NULL,
	@UtilitiesBills			decimal(9,2) = NULL,
	@CommunicationBills		decimal(9,2) = NULL,
	@Furniture				decimal(9,2) = NULL,
	@MortgageRental			decimal(9,2) = NULL,
	@HomeInsurance			decimal(9,2) = NULL,
	@VehicleInsurance		decimal(9,2) = NULL,
	@Repairs				decimal(9,2) = NULL,
	@CouncilRates			decimal(9,2) = NULL,
	@VehicleRegistration	decimal(9,2) = NULL,
	@Petrol					decimal(9,2) = NULL,
	@VehicleLoans			decimal(9,2) = NULL,
	@Entertainment			decimal(9,2) = NULL,
	@HolidayTravel			decimal(9,2) = NULL,
	@ComplianceIncomeExpensesID nvarchar(128) = NULL OUTPUT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ErrorCode		INT
	DECLARE @TransStarted	BIT 

	IF (@@TRANCOUNT = 0)
	BEGIN
		BEGIN TRANSACTION
		SET @TransStarted = 1 
	END
	ELSE
	BEGIN
		SET @TransStarted = 0 
	END

	-- set the new id 
	IF @ComplianceIncomeExpensesID IS NULL 
	BEGIN 
		SET @ComplianceIncomeExpensesID = NEWID()
	END

	INSERT INTO dbo.ComplianceIncomeExpenses
		(ComplianceIncomeExpensesID
		,ClientID
		,Centrelink
		,ReceivedMaintenance
		,SuperannunationPension
		,OtherTaxableIncome
		,OtherForeignIncome
		,NonTaxableIncome
		,FoodExpenses
		,ClothingExpenses
		,MedicalExpenses
		,UtilitiesBills
		,CommunicationsBills
		,Furniture
		,MortgageRental
		,HomeInsurance
		,VehicleInsurance
		,Repairs
		,CouncilRates
		,VehicleRegistration
		,Petrol
		,VehicleLoans
		,Entertainment
		,HolidayTravel
		,DateCreated
		,DateModified)
	VALUES
		(@ComplianceIncomeExpensesID
		,@ClientID
		,@Centrelink
		,@ReceivedMaintenance
		,@SuperannuationPension
		,@OtherTaxableIncome
		,@OtherForeignIncome
		,@NonTaxableIncome
		,@FoodExpenses
		,@ClothingExpenses
		,@MedicalExpenses
		,@UtilitiesBills
		,@CommunicationBills
		,@Furniture
		,@MortgageRental
		,@HomeInsurance
		,@VehicleInsurance
		,@Repairs
		,@CouncilRates
		,@VehicleRegistration
		,@Petrol
		,@VehicleLoans
		,@Entertainment
		,@HolidayTravel	
		,GetDate()
		,GetDate())

	-- Check for errors
	SET @ErrorCode = @@ERROR 
	IF @ErrorCode <> 0 
	BEGIN
		GOTO Cleanup
	END
	-- Finalize transaction 
	IF @TransStarted = 1 
	BEGIN
		SET @TransStarted = 0 
		COMMIT TRANSACTION 
	END

	RETURN 0
Cleanup:
	IF (@TransStarted = 1)
	BEGIN
		SET @TransStarted = 0 
		ROLLBACK TRANSACTION 
		SET @ComplianceIncomeExpensesID = NULL 
	END

	RETURN @ErrorCode

END


GO
/****** Object:  StoredProcedure [dbo].[CreateCorrespondenceNote]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 17/03/2014
-- Description:	Creates a correspondence note
-- =============================================
CREATE PROCEDURE [dbo].[CreateCorrespondenceNote] 
	@ClientID nvarchar(128), 
	@AdviserID nvarchar(128),
	@NoteTypeID int, 
	@AssetClass varchar(100) = NULL, 
	@ProductClass varchar(100) = NULL, 
	@Product varchar(100) = NULL, 
	@Purpose varchar(100) = NULL, 
	@TimeSpent float = NULL, 
	@NoteSerial varchar(100) = NULL, 
	@Subject varchar(200), 
	@Body varchar(max) = NULL,
	@FollowupDate date = NULL, 
	@DateDue date = NULL, 
	@Status varchar(50) = NULL,
	@DateCompleted datetime = NULL, 
	@Reminder bit = NULL, 
	@ReminderDate datetime = NULL, 
	@IsAccepted bit = NULL, 
	@IsDeclined bit = NULL, 
	@NoteID nvarchar(128) = NULL OUTPUT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 
	SELECT @Error = 0 

	SELECT @NoteID = NEWID()

	BEGIN TRAN 

	INSERT INTO dbo.Notes
		(NoteID
		,NoteTypesID
		,ClientID
		,AdviserID
		,AssetClass
		,ProductClass
		,Product
		,Purpose
		,TimeSpent
		,NoteSerial
		,Subject
		,Body
		,FollowupDate
		,DateDue
		,Status
		,DateCompleted
		,Reminder
		,ReminderDate
		,IsAccepted
		,IsDeclined
		,DateCreated
		,DateModified)
	VALUES
		(@NoteID
		,@NoteTypeID
		,@ClientID
		,@AdviserID
		,@AssetClass
		,@ProductClass
		,@Product
		,@Purpose
		,@TimeSpent
		,@NoteSerial
		,@Subject
		,@Body
		,@FollowupDate
		,@DateDue
		,@Status
		,@DateCompleted
		,@Reminder
		,@ReminderDate
		,@IsAccepted
		,@IsDeclined
		,GetDate()
		,GetDate())

	SELECT @Error = @@ERROR 
		
	IF @Error = 0 
	BEGIN
		COMMIT TRAN 
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @NoteID = NULL 
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateDailyPrice]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 01/04/2014
-- Description:	Creates a new Daily Price entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateDailyPrice] 
	@StockID nvarchar(128),
	@Date datetime ,
	@Price decimal(9,4),
	@High decimal(9,4),
	@Low decimal(9,4),
	@High52WeekDate date, 
	@High52WeekPrice decimal(9,4),
	@Low52WeekDate date ,
	@Low52WeekPrice decimal(9,4),
	@DailyPriceID nvarchar(128) = NULL OUTPUT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT
	DECLARE @RecordCount INT 

	SELECT @RecordCount = COUNT(DailyPriceID) FROM dbo.DailyPrices WHERE Date = @Date AND StockID = @StockID 

	IF @RecordCount <> 0 
	BEGIN 
		SELECT @Error = -1
	END 
	
	BEGIN TRAN
	IF @Error = 0
	BEGIN
		INSERT INTO dbo.DailyPrices 
			(StockID
			,Date
			,Price
			,High
			,Low
			,High52WeekDate
			,High52WeekPrice
			,Low52WeekDate
			,Low52WeekPrice
			,DateCreated)
		VALUES
			(@StockID
			,@Date
			,@Price
			,@High
			,@Low
			,@High52WeekDate
			,@High52WeekPrice
			,@Low52WeekDate
			,@Low52WeekPrice
			,GetDate())

		SELECT @Error = @@ERROR 
	END
	
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		SELECT @DailyPriceID = NULL
		ROLLBACK TRAN
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[CreateDealerGroup]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06 May 2014
-- Description:	Creates a new DealerGroup entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateDealerGroup]
	@DealerGroupName	varchar(100),
	@DealerGroupID		nvarchar(128) = NULL	OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error			INT 
	DECLARE @TransStarted	BIT

	IF @@TRANCOUNT = 0 
	BEGIN
		SET @TransStarted = 1
		BEGIN TRANSACTION
	END

	SELECT @DealerGroupID = NEWID()

	-- Perform the actual insert
	INSERT INTO dbo.DealerGroups
		(DealerGroupID
		,DealerGroupName
		,DateCreated
		,DateModified)
	VALUES
		(@DealerGroupID
		,@DealerGroupName
		,GetDate()
		,GetDate())

	SELECT @Error = @@ERROR

	-- Check for Errors
	IF @Error <> 0
	BEGIN
		GOTO Cleanup
	END

	-- Finalize transaction 
	IF @TransStarted = 1
	BEGIN
		COMMIT TRANSACTION
		SET @TransStarted = 0 
	END

	RETURN 0 

Cleanup:
	IF @TransStarted = 1 
	BEGIN
		ROLLBACK TRANSACTION
		SET @TransStarted = 0 
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[CreateDependant]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Creates a Dependant for a particular client
-- =============================================
CREATE PROCEDURE [dbo].[CreateDependant] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128),
	@FullName varchar(100),
	@Relationship varchar(100) = NULL,
	@DateOfBirth date = NULL,
	@FinancialDependant BIT ,
	@DependantIncome float = NULL,
	@DependantAsset float = NULL,
	@DependantID nvarchar(128)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	SELECT @DependantID = NEWID()

	BEGIN TRAN
	-- Enter the data into the database
	INSERT INTO dbo.Dependants
		(DependantID
		,ClientID
		,Fullname
		,Relationship
		,DateOfBirth
		,FinancialDependant
		,DependantIncome
		,DependantAsset
		,DateCreated
		,DateModified)

	VALUES
		(@DependantID
		,@ClientID
		,@FullName
		,@Relationship
		,@DateOfBirth
		,@FinancialDependant
		,@DependantIncome
		,@DependantAsset
		,GetDate()
		,GetDate())

	SELECT @Error = @@ERROR

	-- Check for erro
	IF @Error = 0
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @DependantID = NULL
	END

	RETURN @Error

END


GO
/****** Object:  StoredProcedure [dbo].[CreateDirectProperty]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06/03/2014
-- Description:	Creates a new RealEstate record
-- =============================================
CREATE PROCEDURE [dbo].[CreateDirectProperty] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128), 
	@PurchasedPrice decimal(18,2),
	@StampDuty decimal(18,2),
	@PurchaseMiscFee decimal(18,2),
	@PurchaseLegalFee decimal(18,2),
	@PurchaseBankFee decimal(18,2),
	@PurchasedDate date, 
	@SalePrice decimal(18,2) = NULL,
	@AgentCommission decimal(18,2) = NULL,
	@SaleMiscFee decimal(18,2) = NULL,
	@SaleLegalFee decimal(18,2) = NULL,
	@SaleBankFee decimal(18,2) = NULL, 
	@SaleLoanRepayment decimal(18,2) = NULL, 
	@SaleDate date = NULL, 
	@State varchar(50), 
	@EstimatedValue decimal(18,2), 
	@RentalIncome decimal(18,2), 
	@OwnershipLength int ,
	@Country varchar(50) = NULL, 
	@Region varchar(50) = NULL, 
	@Status varchar(50) = NULL, 
	@OutstandingLoan decimal(18,2) = NULL, 
	@YearsOnLoan decimal(9,2) = NULL, 
	@LoanRateType varchar(50) = NULL, 
	@FixedTermEndDate date = NULL, 
	@FixedRate decimal(9,3) = NULL, 
	@FixedLoanPercent decimal(9,3) = NULL, 
	@InterestRepayment decimal(9,3) = NULL, 
	@VariableRate decimal(9,3) = NULL, 
	@AgentName varchar(100) = NULL, 
	@AgentCompany varchar(100) = NULL, 
	@AgentAddress varchar(200) = NULL, 
	@AgentPhone varchar(50) = NULL, 
	@AgentFax varchar(50) = NULL, 
	@AgentEmail varchar(100) = NULL, 
	@AgentYearFrom date = NULL, 
	@AgentYearTo date = NULL, 
	@DirectPropertyID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT
	DECLARE @AssetTypeID INT 

	SELECT @AssetTypeID = AssetTypeID FROM dbo.AssetTypes WHERE AssetType = 'Direct Property'
	SELECT @Error = 0 

	IF @AssetTypeID IS NULL OR @AssetTypeID = 0
	BEGIN
		INSERT INTO dbo.AssetTypes (AssetType) SELECT 'Real Estate' 

		SELECT @AssetTypeID = SCOPE_IDENTITY()
		SELECT @Error = @@ERROR 

		IF NOT @AssetTypeID > 0 
		BEGIN
			SELECT @Error = -1 
		END
	END
	
	IF @Error = 0 
	BEGIN 
		SELECT @DirectPropertyID = NEWID()

		BEGIN TRAN
		INSERT INTO dbo.DirectProperties
			(DirectPropertyID
			,ClientID
			,PurchasedPrice
			,StampDuty
			,PurchaseMiscFee
			,PurchaseLegalFee
			,PurchaseBankFee
			,PurchasedDate
			,SalePrice
			,AgentCommission
			,SaleMiscFee
			,SaleLegalFee
			,SaleBankFee
			,SaleLoanRepayment
			,SaleDate
			,State
			,EstimatedValue
			,RentalIncome
			,OwnershipLength
			,Country
			,Region
			,Status
			,OutstandingLoan
			,YearsOnLoan
			,LoanRateType
			,FixedTermEndDate
			,FixedRate
			,FixedLoanPercent
			,InterestRepayment
			,VariableRate
			,AgentName
			,AgentCompany
			,AgentAddress
			,AgentPhone
			,AgentFax
			,AgentEmail
			,AgentYearFrom
			,AgentYearTo
			,DateCreated
			,DateModified)

		VALUES
			(@DirectPropertyID
			,@ClientID
			,@PurchasedPrice
			,@StampDuty
			,@PurchaseMiscFee
			,@PurchaseLegalFee 
			,@PurchaseBankFee 
			,@PurchasedDate 
			,@SalePrice 
			,@AgentCommission 
			,@SaleMiscFee 
			,@SaleLegalFee 
			,@SaleBankFee 
			,@SaleLoanRepayment 
			,@SaleDate 
			,@State 
			,@EstimatedValue 
			,@RentalIncome 
			,@OwnershipLength 
			,@Country
			,@Region 
			,@Status 
			,@OutstandingLoan 
			,@YearsOnLoan 
			,@LoanRateType 
			,@FixedTermEndDate 
			,@FixedRate 
			,@FixedLoanPercent
			,@InterestRepayment 
			,@VariableRate 
			,@AgentName 
			,@AgentCompany 
			,@AgentAddress 
			,@AgentPhone 
			,@AgentFax 
			,@AgentEmail 
			,@AgentYearFrom 
			,@AgentYearTo 
			,GetDate()
			,GetDate())

		-- Check for errors
		SELECT @Error = @@ERROR
	END
	
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @DirectPropertyID = NULL
	END

	RETURN @Error
END


GO
/****** Object:  StoredProcedure [dbo].[CreateEducationRecord]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Creates a new education record for a user
-- =============================================
CREATE PROCEDURE [dbo].[CreateEducationRecord] 
	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128),
	@EducationLevel varchar(50) = NULL,
	@YearStarted int = NULL,
	@YearCompleted int = NULL,
	@YearSinceCompletion int = NULL, 
	@EducationInstitution varchar(100) = NULL,
	@CourseAttended varchar(100) = NULL, 
	@Description varchar(max) = NULL,
	@Comments varchar(max) = NULL,
	@EducationRecordID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT

	SELECT @EducationRecordID = NEWID()

	BEGIN TRAN

	INSERT INTO dbo.EducationRecords
		(EducationRecordID
		,UserID
		,EducationLevel 
		,YearStarted
		,YearCompleted
		,YearsSinceCompletion
		,EducationInstitution
		,CourseAttended
		,Description
		,Comments
		,DateCreated
		,DateModified)
	VALUES
		(@EducationRecordID
		,@UserID
		,@EducationLevel
		,@YearStarted
		,@YearCompleted
		,@YearSinceCompletion
		,@EducationInstitution
		,@CourseAttended
		,@Description
		,@Comments
		,GetDate()
		,GetDate())

	SELECT @Error = @@ERROR

	IF @Error = 0
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @EducationRecordID = NULL
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateEmploymentRecord]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin CHung
-- Create date: 24/02/2014
-- Description:	Creates a new employment record
-- =============================================
CREATE PROCEDURE [dbo].[CreateEmploymentRecord] 
	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128),
	@Status varchar(50) = NULL,
	@EmploymentType varchar(50) = NULL,
	@EmployerName varchar(100),
	@Position varchar(50),
	@StartDate date,
	@EndDate date = NULL,
	@HoursPerWeek decimal(9,2) = NULL,
	@GrossSalary float = NULL,
	@Commissions float = NULL,
	@AfterTaxSalary float = NULL, 
	@SuperContribution float = NULL,
	@AdditionalSuperContribution float = NULL,
	@MiscContribution float = NULL,
	@FBT float = NULL,
	@EmployeeSharePlan float = NULL,
	@SickLeave decimal(9,2) = NULL,
	@AnnualLeave decimal(9,2) = NULL,
	@LongServiceLeave decimal(9,2) = NULL,
	@EmploymentRecordID nvarchar(128) = NULL OUTPUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN

	SELECT @EmploymentRecordID = NEWID()

	INSERT INTO dbo.EmploymentRecords
		(EmploymentRecordID 
		,UserID
		,Status
		,EmploymentType
		,EmployerName
		,Position
		,StartDate
		,EndDate
		,HoursPerWeek
		,GrossSalary
		,Commissions
		,AfterTaxSalary
		,SuperContribution
		,AdditionalSuperContribution
		,MiscContribution
		,FBT
		,EmployeeSharePlan
		,SickLeave
		,AnnualLeave
		,LongServiceLeave
		,DateCreated
		,DateModified)
	VALUES
		(@EmploymentRecordID
		,@UserID
		,@Status
		,@EmploymentType
		,@EmployerName
		,@Position
		,@StartDate
		,@EndDate
		,@HoursPerWeek
		,@GrossSalary
		,@Commissions
		,@AfterTaxSalary
		,@SuperContribution
		,@AdditionalSuperContribution
		,@MiscContribution
		,@FBT
		,@EmployeeSharePlan
		,@SickLeave
		,@AnnualLeave
		,@LongServiceLeave
		,GetDate()
		,GetDate())

	SELECT @Error = @@ERROR

	IF @Error = 0
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @EmploymentRecordID = NULL
	END

	RETURN @Error
END


GO
/****** Object:  StoredProcedure [dbo].[CreateExpense]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin CHung
-- Create date: 19/03/2014
-- Description:	Creates an expense record
-- =============================================
CREATE PROCEDURE [dbo].[CreateExpense] 
	@ExpenseTypeID int,
	@AssetTypeID int,
	@AccountID nvarchar(128),
	@Description varchar(50),
	@Frequency int,
	@Amount decimal(18,2), 
	@PaymentDate date, 
	@Comments varchar(500),
	@ExpenseID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	DECLARE @AccountExist BIT 

	-- Check that the account exists 
	SELECT @AccountExist = dbo.IsAccountIDExist(@AccountID, @AssetTypeID)
	IF @AccountExist <> 1
	BEGIN
		SELECT @Error = -1
	END

	BEGIN TRAN

	SELECT @ExpenseID = NEWID()

	INSERT INTO dbo.Expenses
		(ExpenseID
		,ExpenseTypeID
		,AssetTypeID
		,AccountID
		,Description
		,Frequency
		,Amount
		,PaymentDate
		,Comments
		,DateCreated
		,DateModified)
	VALUES
		(@ExpenseID
		,@ExpenseTypeID
		,@AssetTypeID
		,@AccountID
		,@Description
		,@Frequency
		,@Amount
		,@PaymentDate
		,@Comments
		,GetDate()
		,GetDate())

	-- Check for errors 
	SELECT @Error = @@ERROR 
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @ExpenseID = NULL
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateFixedIncome]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Creates a fixed income asset record
-- =============================================
CREATE PROCEDURE [dbo].[CreateFixedIncome] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128),
	@PurchaseDate date,
	@IssuerName varchar(100),
	@Tikcer varchar(50),
	@InterestPaid decimal(18,2) = NULL,
	@MaturityValue decimal(18,2) = NULL,
	@FixedTerm int,
	@BondPrice decimal(18,2) = NULL,
	@TransactionFee decimal(18,2) = NULL,
	@MinimumFee decimal(18,2) = NULL,
	@MaximumFee decimal(18,2) = NULL,
	@AccountNo varchar(50) = NULL,
	@AccountUserID varchar(50) = NULL,
	@ClearingSponsor varchar(50) = NULL,
	@HIN varchar(50) = NULL,
	@NextPaymentDate date = NULL, 
	@NextPaymentAmount decimal(18,2) = NULL, 
	@FixedIncomeID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT
	DECLARE @AssetTypeID INT

	SELECT @AssetTypeID = AssetTypeID FROM dbo.AssetTypes WHERE AssetType = 'Fixed Income Holdings'
	SELECT @Error = 0 

	-- check for existence of AssetType, if it doesn't exist, create it 
	IF @AssetTypeID IS NULL OR @AssetTypeID = 0 
	BEGIN
		INSERT INTO dbo.AssetTypes (AssetType) SELECT 'Fixed Income Holdings'

		SELECT @AssetTypeID = SCOPE_IDENTITY()
		SELECT @Error = @@ERROR 

		IF NOT @AssetTypeID > 0
		BEGIN 
			SELECT @Error = -1
		END
	END

	BEGIN TRAN
	IF @Error = 0 
	BEGIN
		SELECT @FixedIncomeID = NEWID()
		INSERT INTO dbo.FixedIncomeHoldings 
			(FixedIncomeID
			,ClientID
			,AssetTypeID
			,PurchaseDate
			,IssuerName
			,Ticker
			,InterestPaid
			,MaturityValue
			,FixedTerm
			,BondPrice
			,TransactionFee
			,MinimumFee
			,MaximumFee
			,AccountNo
			,AccountUserID
			,ClearingSponsor
			,HIN
			,NextPaymentDate
			,NextPaymentAmount 
			,DateCreated
			,DateModified)
		VALUES
			(@FixedIncomeID
			,@ClientID
			,@AssetTypeID
			,@PurchaseDate
			,@IssuerName
			,@Tikcer
			,@InterestPaid
			,@MaturityValue
			,@FixedTerm
			,@BondPrice
			,@TransactionFee
			,@MinimumFee
			,@MaximumFee
			,@AccountNo
			,@AccountUserID
			,@ClearingSponsor
			,@HIN
			,@NextPaymentDate
			,@NextPaymentAmount 
			,GetDate()
			,GetDate())

		SELECT @Error = @@ERROR 
	END
	
	-- Check for error 
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @FixedIncomeID = NULL
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateIPO]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 27/03/2014
-- Description:	Creates a new IPO entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateIPO] 
	@IssuerName varchar(100) = NULL, 
	@InstrumentCode varchar(100) = NULL,
	@OfferSize int = NULL, 
	@PricePerUnit decimal (18,2) = NULL, 
	@Increments int = NULL, 
	@InvestmentMin int = NULL, 
	@InvestmentMax int = NULL, 
	@ForecastRatio decimal(18,2) = NULL, 
	@DistributionFreq int = NULL, 
	@ForecastDistribution decimal(18,2) = NULL, 
	@ForecastDividend decimal(18,2) = NULL, 
	@ForecastFranking decimal(18,2) = NULL, 
	@OfferType varchar(50) = NULL, 
	@LodgementDate date = NULL, 
	@BookbuildDate date = NULL, 
	@OpeningDate date = NULL, 
	@GeneralClosingDate date = NULL, 
	@BrokerClosingDate date = NULL, 
	@IssueDate date = NULL, 
	@SettlementTradeDate date = NULL, 
	@HoldingDespatchDate date = NULL, 
	@NormalTradeDate  date = NULL, 
	@RecordFirstPayDate date = NULL, 
	@FirstInterestDate date = NULL, 
	@FirstRedemptionDate date = NULL, 
	@MaturityDate date = NULL, 
	@IPOID nvarchar(128) = NULL 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	SELECT @IPOID = NEWID()
	BEGIN TRAN

	INSERT INTO dbo.IPOs
		(IPOID
		,IssuerName
		,InstrumentCode
		,OfferSize
		,PricePerUnit
		,Increments
		,InvestmentMin
		,InvestmentMax
		,ForecastRatio
		,DistributionFreq
		,ForecastDistribution
		,ForecastDividend
		,ForecastFranking
		,OfferType
		,LodgementDate
		,BookbuildDate
		,OpeningDate
		,GeneralClosingDate
		,BrokerClosingDate
		,IssueDate
		,SettlementTradeDate
		,HoldingDespatchDate
		,NormalTradeDate
		,RecordFirstPayDate
		,FirstInterestDate
		,FirstRedemptionDate
		,MaturityDate
		,DateCreated
		,DateModified)
	VALUES 
		(@IPOID
		,@IssuerName
		,@InstrumentCode
		,@OfferSize
		,@PricePerUnit
		,@Increments
		,@InvestmentMin
		,@InvestmentMax
		,@ForecastRatio
		,@DistributionFreq
		,@ForecastDistribution
		,@ForecastDividend
		,@ForecastFranking
		,@OfferType
		,@LodgementDate
		,@BookbuildDate
		,@OpeningDate
		,@GeneralClosingDate
		,@BrokerClosingDate
		,@IssueDate
		,@SettlementTradeDate
		,@HoldingDespatchDate
		,@NormalTradeDate
		,@RecordFirstPayDate
		,@FirstInterestDate
		,@FirstRedemptionDate
		,@MaturityDate
		,GetDate()
		,GetDate())

	SELECT @Error = @@ERROR
	IF @Error = 0 
	BEGIN 
		COMMIT TRAN
	END
	ELSE
	BEGIN 
		ROLLBACK TRAN
		SELECT @IPOID = NULL 
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateLoan]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Creates a new loan record
-- =============================================
CREATE PROCEDURE [dbo].[CreateLoan] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128),
	@Description varchar(100),
	@LoanType varchar(100),
	@DataFeed varchar(50),
	@InterestRate decimal(9,2), 
	@PaymentFrequency int, 
	@PaymentAmount decimal(9,2),
	@LoanTerm int,
	@CreditLimt int, 
	@AccountBalance decimal(9,2),
	@LoanStart date, 
	@AccountNo varchar(50),
	@LoanProvider varchar(100),
	@LastPaymentDate date, 
	@FinancialInstitution varchar(50),
	@RelationshipManager varchar(100),
	@AddressLine1 varchar(100),
	@AddressLine2 varchar(100),
	@City varchar(50), 
	@State varchar(50),
	@Postcode varchar(10),
	@RealEstateAddress varchar(200),
	@LoanID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @AssetTypeID INT
	DECLARE @Error INT 

	-- Find the appropriate assettypeid for Loans 
	SELECT @AssetTypeID = AssetTypeID FROM dbo.AssetTypes WHERE AssetType = 'Loans'
	SELECT @Error = 0

	IF @AssetTypeID IS NULL OR @AssetTypeID = 0 
	BEGIN
		INSERT INTO AssetTypes (AssetType) SELECT 'Loans'

		SELECT @AssetTypeID = SCOPE_IDENTITY()
		SELECT @Error = @@ERROR 

		IF NOT @AssetTypeID > 0 
		BEGIN
			SELECT @Error = -1 
		END
	END

	-- begin the insert procedure
	BEGIN TRAN

	IF @Error = 0
	BEGIN
		SELECT @LoanID = NEWID()

		INSERT INTO dbo.Loans
			(LoanID
			,ClientID
			,AssetTypeID
			,Description
			,LoanType
			,DataFeed
			,InterestRate
			,PaymentFrequency
			,PaymentAmount
			,LoanTerm
			,CreditLimit
			,AccountBalance
			,LoanStart
			,AccountNo
			,LoanProvider
			,LastPaymentDate
			,FinancialInstitution
			,RelationshipManager
			,AddressLine1
			,AddressLine2
			,City
			,State
			,Postcode
			,RealEstateAddress
			,DateCreated
			,DateModified)
		VALUES
			(@LoanID
			,@ClientID
			,@AssetTypeID
			,@Description 
			,@LoanType 
			,@DataFeed 
			,@InterestRate 
			,@PaymentFrequency 
			,@PaymentAmount 
			,@LoanTerm 
			,@CreditLimt 
			,@AccountBalance 
			,@LoanStart 
			,@AccountNo 
			,@LoanProvider 
			,@LastPaymentDate 
			,@FinancialInstitution
			,@RelationshipManager 
			,@AddressLine1 
			,@AddressLine2 
			,@City 
			,@State 
			,@Postcode 
			,@RealEstateAddress 
			,GetDate()
			,GetDate())

		SELECT @Error = @@ERROR 
	END

	-- Check for errors
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @LoanID = NULL 
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateMarginLoan]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Creates a new Margin Loans record
-- =============================================
CREATE PROCEDURE [dbo].[CreateMarginLoan] 
	@ClientID nvarchar(128)
    ,@Description varchar(100)
    ,@HIN varchar(50)
    ,@LoanProvider varchar(50)
    ,@VariableRateLoan varchar(50)
    ,@VariableRateAmount decimal(9,2)
    ,@FixedRateLoan varchar(50)
    ,@FixedRateAmount decimal(9,2)
    ,@UnsettledTransactions decimal(9,2)
    ,@TotalBalance decimal(9,2)
    ,@NetInterestYTD decimal(9,2)
    ,@MonthlyInterest decimal(9,2)
    ,@MinBrokerage decimal(9,2)
    ,@MaxBrokerage decimal(9,2)
    ,@TotalBrokerage decimal(9,2)
    ,@TotalGST decimal(9,2)
    ,@ManagementFee decimal(9,2)
    ,@AdvisersComission decimal(9,2)
    ,@LoanLimit decimal(9,2)
    ,@SpendingLimit decimal(9,2)
    ,@AvailableCash decimal(9,2)
	,@MarginLoanID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @AssetTypeID INT 
	DECLARE @Error INT 

	-- check for the asset type 
	SELECT @AssetTypeID = AssetTypeID FROM dbo.AssetTypes WHERE AssetType = 'Margin Loans' 
	SELECT @Error = 0 
	
	IF @AssetTypeID IS NULL OR @AssetTypeID = 0 
	BEGIN 
		INSERT INTO dbo.AssetTypes (AssetType) SELECT 'Margin Loans'
	
		SELECT @AssetTypeID = SCOPE_IDENTITY()
		SELECT @Error = @@ERROR 

		IF NOT @AssetTypeID > 0 
		BEGIN
			SELECT @Error = -1
		END
	END

	-- INSERT INTO the database 
	BEGIN TRAN
	IF @Error = 0 
	BEGIN 
		SELECT @MarginLoanID = NEWID()
		
		INSERT INTO dbo.MarginLoans
           (MarginLoanID
           ,ClientID
		   ,AssetTypeID
           ,Description
           ,HIN
           ,LoanProvider
           ,VariableRateLoan
           ,VariableRateAmount
           ,FixedRateLoan
           ,FixedRateAmount
           ,UnsettledTransactions
           ,TotalBalance
           ,NetInterestYTD
           ,MonthlyInterest
           ,MinBrokerage
           ,MaxBrokerage
           ,TotalBrokerage
           ,TotalGST
           ,ManagementFee
           ,AdvisersComission
           ,LoanLimit
           ,SpendingLimit
           ,AvailableCash
		   ,DateCreated
           ,DateModified)
		VALUES
			(@MarginLoanID
           ,@ClientID
		   ,@AssetTypeID
           ,@Description
           ,@HIN
           ,@LoanProvider
           ,@VariableRateLoan
           ,@VariableRateAmount
           ,@FixedRateLoan
           ,@FixedRateAmount
           ,@UnsettledTransactions
           ,@TotalBalance
           ,@NetInterestYTD
           ,@MonthlyInterest
           ,@MinBrokerage
           ,@MaxBrokerage
           ,@TotalBrokerage
           ,@TotalGST
           ,@ManagementFee
           ,@AdvisersComission
           ,@LoanLimit
           ,@SpendingLimit
           ,@AvailableCash
		   ,GetDate()
           ,GetDate())

		SELECT @Error = @@ERROR 
	END 

	-- Check for errors 
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @MarginLoanID = NULL
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateNewUser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 20/02/2014
-- Description:	Creates a new user on the database
-- =============================================
CREATE PROCEDURE [dbo].[CreateNewUser] 
	-- Add the parameters for the stored procedure here
	@FirstName varchar(100),
	@MiddleName varchar(100) = NULL,
	@LastName varchar(100),
	@Email varchar(100) = NULL,
	@Password varchar(200),
	@UserID nvarchar(128) = NULL OUTPUT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT
	SET @Error = 0 

	DECLARE @RecordCount INT 
	SELECT @RecordCount = COUNT (UserID) FROM Users WHERE Email LIKE '%' + @Email + '%' 
	
	IF @RecordCount > 0 
	BEGIN
		PRINT 'User name already exists'
		RETURN -1
	END

	SELECT @UserID = NEWID() 
	BEGIN TRAN

	INSERT INTO dbo.Users 
		(UserID
		,FirstName
		,LastName
		,Email
		,UserType
		,Password
		,DateCreated
		,DateModified)
	VALUES
		(@UserID
		,@FirstName
		,@LastName
		,@Email
		,'Non-compliant'
		,@Password
		,GetDate()
		,GetDate())

	-- Check for errors
	SELECT @Error = @@ERROR

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @UserID = NULL
	END

	RETURN @Error

	END 



GO
/****** Object:  StoredProcedure [dbo].[CreateNoteLink]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 18/03/2014
-- Description:	Creates a note link
-- =============================================
CREATE PROCEDURE [dbo].[CreateNoteLink]
	@NoteID nvarchar(128),
	@LinkedNoteIDs varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 
	DECLARE @Count INT 
	DECLARE @LinkedCounts INT 

	DECLARE @tempTable TABLE 
	(
		ElementValue nvarchar(128)
	)

	INSERT INTO @tempTable (ElementValue) SELECT CONVERT(nvarchar(128), Word) FROM dbo.WordsToTable(@LinkedNoteIDs, ',') 


	-- check that the original noteID actually exists in the notes table 
	SELECT @Count = COUNT(NoteID) FROM dbo.Notes WHERE NoteID = @NoteID 

	IF @Count <> 1 
	BEGIN
		SELECT @Error = -1
	END

	-- check that the linked list exists in the database
	SELECT @Count = COUNT(NoteID) FROM dbo.Notes WHERE NoteID IN (SELECT ElementValue FROM @tempTable)
	SELECT @LinkedCounts = COUNT(ElementValue) FROM @tempTable 

	IF @Count <> @LinkedCounts AND @Error = 0
	BEGIN
		SELECT @Error = -2
	END 

	BEGIN TRAN
	-- During the insert, ensure that noteID1 is less than NoteID2 
	INSERT INTO dbo.NoteLinks (NoteID1, NoteID2, DateCreated)
	SELECT 
		CASE WHEN @NoteID > ElementValue THEN ElementValue ELSE @NoteID END, 
		CASE WHEN @NoteID > ElementValue THEN @NoteID ELSE ElementValue END,
		GetDate()
	FROM @tempTable 

	SELECT @Error = @@ERROR 

	-- Check for errors 
	IF @Error = 0
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN 
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreatePolicy]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/03/2014
-- Description:	Creates a new policy
-- =============================================
CREATE PROCEDURE [dbo].[CreatePolicy] 
	@ClientID nvarchar(128), 
	@InsuranceType varchar(100),
	@PolicyType varchar(100),
	@PolicyName varchar(100),
	@PolicyNumber varchar(50) = NULL, 
	@AccountName varchar(100),
	@AccountAddress1 varchar(200) = NULL, 
	@AccountAddress2 varchar(200) = NULL,
	@AccountAddress3 varchar(200) = NULL,
	@AccountCity varchar(100) = NULL,
	@AccountState varchar(100) = NULL,
	@AccountPostCode varchar(100) = NULL,
	@InceptionDate date = NULL,
	@LastRenewalDate date = NULL, 
	@StartDate date = NULL,
	@EndDate date = NULL, 
	@Commentary varchar(max) = NULL,
	@Institution varchar(100) = NULL,
	@RenewalDueDate date = NULL, 
	@PolicyID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	SELECT @PolicyID = NEWID()

	BEGIN TRAN
	INSERT INTO dbo.Policies 
		(PolicyID
		,ClientID
		,InsuranceType
		,PolicyType
		,PolicyName
		,PolicyNumber
		,AccountName
		,AccountAddress1
		,AccountAddress2
		,AccountAddress3
		,AccountCity
		,AccountState
		,AccountPostCode
		,InceptionDate
		,LastRenewalDate
		,StartDate
		,EndDate
		,Commentary
		,Institution
		,RenewalDueDate
		,DateCreated
		,DateModified)
	VALUES
		(@PolicyID
		,@ClientID
		,@InsuranceType
		,@PolicyType
		,@PolicyName
		,@PolicyNumber
		,@AccountName
		,@AccountAddress1
		,@AccountAddress2
		,@AccountAddress3
		,@AccountCity
		,@AccountState
		,@AccountPostCode
		,@InceptionDate
		,@LastRenewalDate
		,@StartDate
		,@EndDate
		,@Commentary
		,@Institution
		,@RenewalDueDate
		,GetDate()
		,GetDate())

	SELECT @Error = @@ERROR 

	-- Check for errors 
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @PolicyID = NULL
	END
	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreatePolicyDetail]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/03/2014
-- Description:	Creates a new Policy Detail Record
-- =============================================
CREATE PROCEDURE [dbo].[CreatePolicyDetail] 
	@PolicyID nvarchar(128),
	@DetailType varchar(100) ,
	@DetailName varchar(100) = NULL,
	@Description varchar(100) = NULL, 
	@Amount decimal(9,2) = NULL, 
	@StartDate date = NULL, 
	@EndDate date = NULL ,
	@PolicyDetailID nvarchar(128) = NULL OUTPUT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	SELECT @PolicyDetailID = NEWID()

	BEGIN TRAN

	INSERT INTO dbo.PolicyDetails 
		(PolicyDetailID
		,PolicyID
		,DetailType
		,DetailName
		,Description
		,Amount
		,StartDate
		,EndDate
		,DateCreated
		,DateModified)
	VALUES
		(@PolicyDetailID
		,@PolicyID
		,@DetailType
		,@DetailName
		,@Description
		,@Amount
		,@StartDate
		,@EndDate
		,GetDate()
		,GetDate())

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @PolicyDetailID = NULL 
	END

	RETURN @Error 
	
END


GO
/****** Object:  StoredProcedure [dbo].[CreateProductType]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 21/03/2014
-- Description:	Creates a new Product Type
-- =============================================
CREATE PROCEDURE [dbo].[CreateProductType] 
	-- Add the parameters for the stored procedure here
	@ProductType varchar(100) ,
	@ProductTypeID int = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN

	INSERT INTO dbo.ProductTypes 
		(ProductType)
	VALUES(@ProductType)

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		SELECT @ProductTypeID = SCOPE_IDENTITY()
		SELECT @Error = @@ERROR 
	END


	-- insert a default negative entry into the Risk Profile Product Type matrix
	IF @Error = 0 
	BEGIN
		INSERT INTO dbo.ProfileProductLinks (RiskProfileID, ProductTypeID, Selected) SELECT R.RiskProfileID, @ProductTypeID, NULL FROM dbo.RiskProfiles R 
		SELECT @Error = @@ERROR 
	END

	-- Check for error and commit if no errors 
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END
	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateQualification]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 25/03/2014
-- Description:	Creates a new qualification entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateQualification] 
	-- Add the parameters for the stored procedure here
	@QualificationTypeID int,
	@UserID nvarchar(128),
	@Qualification varchar(200),
	@QualificationIndex int ,
	@QualificationID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Error INT

	SELECT @QualificationID = NEWID()
	
	BEGIN TRAN

	INSERT INTO dbo.Qualifications
		(QualificationID
		,UserID
		,QualificationTypeID
		,Qualification
		,QualificationIndex)
	VALUES
		(@QualificationID
		,@UserID
		,@QualificationTypeID
		,@Qualification
		,@QualificationIndex)

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN 
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @QualificationID = NULL
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[CreateRiskProfile]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 05/03/2014
-- Description:	Inserts a new risk profile record for a client
-- =============================================
CREATE PROCEDURE [dbo].[CreateRiskProfile]
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128),
	@ShortTermGoal1 varchar(200) = NULL, 
	@ShortTermGoal2 varchar(200) = NULL,
	@ShortTermGoal3 varchar(200) = NULL, 
	@MedTermGoal1 varchar(200) = NULL, 
	@MedTermGoal2 varchar(200) = NULL,
	@MedTermGoal3 varchar(200) = NULL, 
	@LongTermGoal1 varchar(200) = NULL, 
	@LongTermGoal2 varchar(200) = NULL, 
	@LongTermGoal3 varchar(200) = NULL, 
	@Comments varchar(max) = NULL, 
	@RetirementAge int = NULL, 
	@RetirementIncome float = NULL, 
	@IncomeSource varchar(100) = NULL, 
	@InvestmentObjective1 varchar(200) = NULL, 
	@InvestmentObjective2 varchar(200) = NULL, 
	@InvestmentObjective3 varchar(200) = NULL, 
	@InvestmentTimeHorizon int = NULL, 
	@InvestmentKnowledge varchar(200) = NULL, 
	@RiskAttitude varchar(200) = NULL, 
	@CapitalLossAttitude varchar(200) = NULL, 
	@ShortTermTrading varchar(50) = NULL, 
	@ShortTermAssetPercent float = NULL, 
	@ShortTermEquityPercent float = NULL, 
	@InvestmentProfile varchar(100) = NULL, 
	@ProductsSelection varchar(max) = NULL,
	@RiskProfileID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 
	
	-- Check to ensure client has no current risk profile
	DECLARE @RecordCount INT 
	SELECT COUNT(RiskProfileID) FROM dbo.RiskProfiles WHERE ClientID = @ClientID 

	-- Drop out if there's already a profile created
	IF @RecordCount > 0 
	BEGIN
		SELECT @RiskProfileID = NULL 
		RETURN - 1
	END

	-- Need to parse the products selected, as it is a variable list, we need to create a temp table so that they can be matched to the profiles
	DECLARE @tempTable TABLE
	(
		ElementValue VARCHAR(1000),
		RowNum INT,
		ColNum INT
	)

	INSERT INTO @tempTable (ElementValue, RowNum, ColNum) SELECT ElementValue, RowNum, ColNum FROM dbo.CSVIntoTable(',','|', @ProductsSelection)
	
	DECLARE @profileData TABLE 
	(
		ProductTypeID int, 
		Selected bit
	)

	INSERT INTO @profileData (ProductTypeID, Selected) 
	SELECT 
		CONVERT(int, D1.ElementValue),
		CONVERT(bit, D2.ElementValue)
	FROM @tempTable D1 INNER JOIN @tempTable D2 ON D1.RowNum = D2.RowNum WHERE D1.ColNum = 1 AND D2.ColNum = 2

	BEGIN TRAN
	-- Create the RiskProfile record
	SELECT @RiskProfileID = NEWID()

	INSERT INTO dbo.RiskProfiles
		(RiskProfileID
		,ClientID
		,ShortTermGoal1
		,ShortTermGoal2
		,ShortTermGoal3
		,MedTermGoal1
		,MedTermGoal2
		,MedTermGoal3
		,LongTermGoal1
		,LongTermGoal2
		,LongTermGoal3
		,Comments
		,RetirementAge
		,RetirementIncome
		,IncomeSource
		,InvestmentObjective1
		,InvestmentObjective2
		,InvestmentObjective3
		,InvestmentTimeHorizon
		,InvestmentKnowledge
		,RiskAttitude
		,CapitalLossAttitude
		,ShortTermTrading
		,ShortTermAssetPercent
		,ShortTermEquityPercent
		,InvestmentProfile
		,DateCreated
		,DateModified)
	VALUES
		(@RiskProfileID
		,@ClientID 
		,@ShortTermGoal1 
		,@ShortTermGoal2 
		,@ShortTermGoal3 
		,@MedTermGoal1 
		,@MedTermGoal2 
		,@MedTermGoal3 
		,@LongTermGoal1 
		,@LongTermGoal2 
		,@LongTermGoal3 
		,@Comments 
		,@RetirementAge
		,@RetirementIncome 
		,@IncomeSource 
		,@InvestmentObjective1 
		,@InvestmentObjective2 
		,@InvestmentObjective3 
		,@InvestmentTimeHorizon 
		,@InvestmentKnowledge 
		,@RiskAttitude 
		,@CapitalLossAttitude 
		,@ShortTermTrading 
		,@ShortTermAssetPercent 
		,@ShortTermEquityPercent 
		,@InvestmentProfile
		,GetDate()
		,GetDate())

	-- Check for Errors
	SELECT @Error = @@ERROR 

	-- Create the linking entries to link the newly created profile to the products
	IF @Error = 0
	BEGIN
		
		INSERT INTO dbo.ProfileProductLinks (RiskProfileID, ProductTypeID, Selected) SELECT @RiskProfileID, P.ProductTypeID, 0 FROM ProductTypes P 
		SELECT @Error = @@ERROR 
	END

	-- Update the linking entries with the riskprofile information'
	IF @Error = 0 
	BEGIN
		UPDATE dbo.ProfileProductLinks 
		SET
			Selected = D.Selected
		FROM @profileData D , dbo.ProfileProductLinks P 
		WHERE D.ProductTypeID = P.ProductTypeID AND P.RiskProfileID = @RiskProfileID 

		SELECT @Error = @@ERROR 
	END

	-- Check for errors and commit if no errors 
	IF @Error = 0 
	BEGIN 
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @RiskProfileID = NULL 
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[CreateSecurity]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 26/03/2014
-- Description:	Creates a new securities entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateSecurity] 
	@MarginLoanID nvarchar(128),
	@PurchaseDate date, 
	@Company varchar(50) = NULL,
	@Ticker varchar(50) = NULL,
	@Units int,
	@PricePerUnit decimal(9,2) ,
	@Brokerage decimal(9,2),
	@MarketValue decimal(9,2), 
	@SecuritiesID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	DECLARE @MarginLoanCount INT 

	-- Check to ensure taht the margin loan given actually exists
	SELECT @MarginLoanCount = COUNT(MarginLoanID) FROM dbo.MarginLoans 
	WHERE MarginLoanID = @MarginLoanID

	IF @MarginLoanCount <> 1
	BEGIN
		SELECT @Error = -1
	END

	SELECT @SecuritiesID = NEWID()
	BEGIN TRAN
	IF @Error = 0 
	BEGIN
		INSERT INTO dbo.Securities
			(SecuritiesID
			,MarginLoanID
			,PurchaseDate
			,Company
			,Ticker
			,Units
			,PricePerUnit
			,Brokerage
			,MarketValue
			,DateCreated
			,DateModified)
		VALUES
			(@SecuritiesID
			,@MarginLoanID
			,@PurchaseDate
			,@Company
			,@Ticker
			,@Units
			,@PricePerUnit
			,@Brokerage
			,@MarketValue
			,GetDate()
			,GetDate())

		SELECT @Error = @@ERROR 
	END

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		SELECT @SecuritiesID = NULL 
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateStatement]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/03/2014
-- Description:	Creates a statement entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateStatement] 
	@AssetTypeID INT,
	@AccountID nvarchar(128),
	@StatementDate date, 
	@Description varchar(50), 
	@Balance decimal(9,2), 
	@Credit decimal (9,2),
	@Debit decimal (9,2),
	@Amount decimal (9,2),
	@StatementID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	DECLARE @AccountExist BIT 

	SELECT @AccountExist = dbo.IsAccountIDExist(@AccountID, @AssetTypeID)

	IF @AccountExist <> 1
	BEGIN
		SELECT @Error = -1
	END

	BEGIN TRAN

	SELECT @StatementID = NEWID()
	IF @Error = 0
	BEGIN
		INSERT INTO dbo.Statements
			(StatementID
			,AssetTypeID
			,AccountID
			,StatementDate
			,Description
			,Balance
			,Credit
			,Debit
			,Amount
			,DateCreated)
		VALUES
			(@StatementID
			,@AssetTypeID
			,@AccountID
			,@StatementDate
			,@Description
			,@Balance
			,@credit
			,@Debit
			,@Amount
			,GetDate())

		SELECT @Error = @@ERROR

	END

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @StatementID = NULL
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateStockHolding]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06/03/2014
-- Description:	Creates a new stocks holding entry
-- =============================================
CREATE PROCEDURE [dbo].[CreateStockHolding] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128),
	@StockID nvarchar(128),
	@Units int,
	@ServiceFee decimal(18,2),
	@HIN varchar(50),
	@StockHoldingID nvarchar(128) = NULL OUTPUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT
	DECLARE @AssetTypeID INT 

	SELECT @AssetTypeID = AssetTypeID FROM dbo.AssetTypes WHERE AssetType = 'Equity Holdings'
	SELECT @Error = 0 

	IF @AssetTypeID IS NULL OR @AssetTypeID = 0
	BEGIN
		INSERT INTO dbo.AssetTypes (AssetType) SELECT 'Equity Holdings' 

		SELECT @AssetTypeID = SCOPE_IDENTITY()
		SELECT @Error = @@ERROR 

		IF NOT @AssetTypeID > 0 
		BEGIN
			SELECT @Error = -1
		END

	END

	-- Check to see the current client doesn't already have a holding of this stock
	DECLARE @RecordCount INT 
	SELECT @RecordCount = COUNT(StockHoldingID) FROM dbo.StockHoldings WHERE ClientID = @ClientID AND StockID = @StockID

	IF @RecordCount > 0 
	BEGIN
		SELECT @StockHoldingID = NULL 
		RETURN -1 
	END
	
	BEGIN TRAN

	IF @Error = 0
	BEGIN
		SELECT @StockHoldingID = NEWID()
		INSERT INTO dbo.StockHoldings
			(ClientID
			,StockID
			,Units
			,ServiceFee
			,AssetTypeID
			,HIN
			,StockHoldingID
			,DateCreated
			,DateModified)
		VALUES
			(@ClientID
			,@StockID
			,@Units
			,@ServiceFee
			,@AssetTypeID
			,@HIN
			,@StockHoldingID
			,GetDate()
			,GetDate())

		SELECT @Error = @@ERROR
	END
	
	IF @Error = 0
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @StockHoldingID = NULL
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[CreateTallyTable]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 05/03/2014
-- Description:	Creates a tally table required for the conversion function dbo.CSVIntoTable to convert CSV into a table
-- =============================================
CREATE PROCEDURE [dbo].[CreateTallyTable]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF OBJECT_ID('dbo.Tally') IS NOT NULL DROP TABLE dbo.Tally
	
	-- Define how many rows wanted in Tally Table

	SET ROWCOUNT 100000
	SELECT IDENTITY(INT, 1, 1) N
	INTO dbo.Tally
	FROM master.sys.all_columns c
	CROSS JOIN master.sys.all_columns c1

	SET ROWCOUNT 0 

	CREATE UNIQUE CLUSTERED INDEX PKC_Tally ON dbo.Tally (N)
	
END


GO
/****** Object:  StoredProcedure [dbo].[CreateTransaction]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 19/03/2014
-- Description:	Creates a new transaction record
-- =============================================
CREATE PROCEDURE [dbo].[CreateTransaction] 
	-- Add the parameters for the stored procedure here
	@AssetTypeID int, 
	@AccountID nvarchar(128), 
	@TransactionTypeID int, 
	@CostPerUnit decimal(9,2),
	@TransactionDate date,
	@ContractNo varchar(100),
	@BrokerageFee decimal(9,2),
	@UnitPrice decimal(9,2),
	@StampDuty decimal(9,2),
	@LegalFee decimal(9,2),
	@EstablishmentFee decimal(9,2),
	@Units int,
	@TransactionID nvarchar(128) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 
	DECLARE @AccountExist BIT 

	-- Check that the account actually exists
	SELECT @AccountExist = dbo.IsAccountIDExist(@AccountID, @AssetTypeID)

	IF @AccountExist <> 1
	BEGIN 
		SELECT @Error = -1 
	END

	-- Check for ERROR 
	BEGIN TRAN

	IF @Error = 0 
	BEGIN
	SELECT @TransactionID = NEWID()

	INSERT INTO dbo.Transactions
		(TransactionID
		,TransactionTypeID
		,CostPerUnit
		,TransactionDate
		,ContractNo
		,BrokerageFee
		,UnitPrice
		,StampDuty
		,LegalFee
		,EstablishmentFee
		,AssetTypeID
		,AccountID
		,Units
		,DateCreated
		,DateModified)
	VALUES
		(@TransactionID
		,@TransactionTypeID
		,@CostPerUnit
		,@TransactionDate
		,@ContractNo
		,@BrokerageFee
		,@UnitPrice
		,@StampDuty
		,@LegalFee
		,@EstablishmentFee
		,@AssetTypeID
		,@AccountID
		,@Units
		,GetDate()
		,GetDate())
		
	SELECT @Error = @@ERROR 	
	END

	IF @Error = 0 
	BEGIN
	
	-- When selling, we subtract the value from the holdings, else we add, if it's an adjustment then the number provided should be approrpiately signed 
	UPDATE dbo.StockHoldings SET
		Units = CASE WHEN @TransactionTypeID = 2 THEN SH.Units - @Units ELSE SH.Units + @Units END ,
		DateModified = GetDate()
	
	FROM dbo.StockHoldings SH 
	WHERE SH.StockHoldingID = @AccountID AND SH.AssetTypeID = @AssetTypeID 

	SELECT @Error = @@ERROR 
	
	END
	
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SELECT @TransactionID = NULL
	END

END


GO
/****** Object:  StoredProcedure [dbo].[CreateUserData]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/04/2014
-- Description:	Creates a new user data profile
-- =============================================
CREATE PROCEDURE [dbo].[CreateUserData] 
	@UserID             nvarchar(128),
	@FirstName			varchar(100)       ,
	@LastName			varchar(100)       ,
	@DateOfBirth		datetime           ,
	@CurrentTimeUtc		datetime           ,
	@MaxAccountNo		INT = NULL   OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @TranStarted  bit
	DECLARE @ErrorCode    INT 
	DECLARE @RecordCount  INT
	
	-- Check to see that the userID exists in the database 
	SELECT @RecordCount = COUNT(UserID) FROM dbo.aspnet_Membership WHERE UserId = @UserID 

	IF @RecordCount <> 1 
	BEGIN
		SET @ErrorCode = -1 
		GOTO Cleanup
	END

	-- Now check to see that there's not already a userdata profile for our user 
	SELECT @RecordCount = COUNT(UserID) FROM dbo.UserData WHERE UserID = @UserID 

	IF @RecordCount >0 
	BEGIN 
		SET @ErrorCode = -2 
		GOTO Cleanup
	END 

	-- User found, start our transaction (if required) and insert
	-- IF @@TRANSCOUNT indicates current transaction in place, do not start a new one, instead let the calling stored proc do the commiting
	IF (@@TRANCOUNT = 0)
	BEGIN
		BEGIN TRANSACTION 
		SET @TranStarted = 1
	END 
	ELSE
	BEGIN
		SET @TranStarted = 0
	END

	INSERT INTO dbo.UserData
		(UserID
		,FirstName
		,LastName
		,DateOfBirth
		,DateCreated
		,DateModified)
	VALUES
		(@UserID
		,@FirstName
		,@LastName
		,@DateOfBirth
		,@CurrentTimeUtc
		,@CurrentTimeUtc)

	-- Check for errors 
	SELECT @ErrorCode = @@ERROR 

	IF (@ErrorCode <> 0)
	BEGIN 
		GOTO Cleanup 
	END

	-- Finalize transaction 
	IF (@TranStarted = 1)
	BEGIN 
		SET @TranStarted = 0 
		COMMIT TRANSACTION 
	END

	RETURN 0 

Cleanup: 
	IF (@TranStarted = 1 )
	BEGIN
		SET @TranStarted = 0
		ROLLBACK TRANSACTION 
	END
	RETURN @ErrorCode

END


GO
/****** Object:  StoredProcedure [dbo].[DeleteAppointment]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 09/04/2014
-- Description:	Deletes an appointment entry
-- =============================================
CREATE PROCEDURE [dbo].[DeleteAppointment] 
	@AppointmentID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @TranStarted bit
	DECLARE @ErrorCode   int 

	IF (@@TRANCOUNT = 0)
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1 
	END 
	ELSE
	BEGIN
		SET @TranStarted = 0 
	END

	DELETE FROM dbo.Appointments 
	WHERE AppointmentID = @AppointmentID 

	SET @ErrorCode = @@ERROR 

	IF @ErrorCode <> 0 
	BEGIN
		GOTO Cleanup 
	END

	IF (@TranStarted = 1)
	BEGIN
		SET @TranStarted = 0 
		COMMIT TRANSACTION 
	END

	RETURN @ErrorCode

Cleanup:
	
	IF (@TranStarted = 1)
	BEGIN
		SET @TranStarted = 0 
		ROLLBACK TRANSACTION 
	END

	RETURN @ErrorCode
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteBeneficiary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin CHung
-- Create date: 26/03/2014
-- Description:	Deletes a beneficiary entry
-- =============================================
CREATE PROCEDURE [dbo].[DeleteBeneficiary] 
	-- Add the parameters for the stored procedure here
	@BeneficiaryID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN
	DELETE FROM dbo.Beneficiaries 
	WHERE BeneficiaryID = @BeneficiaryID

	SELECT @Error = @@ERROR
	
	IF @Error = 0
	BEGIN
		COMMIT TRAN 
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteBookmark]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 03/04/2014
-- Description:	Deletes a bookmark entry
-- =============================================
CREATE PROCEDURE [dbo].[DeleteBookmark] 
	-- Add the parameters for the stored procedure here
	@BookmarkID nvarchar(128) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN

	DELETE FROM dbo.Bookmarks WHERE BookmarkID = @BookmarkID 

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteDailyPrice]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 01/04/2014
-- Description:	Deletes an existing Daily Prices record
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDailyPrice] 
	@DailyPriceID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN

	DELETE FROM dbo.DailyPrices WHERE DailyPriceID = @DailyPriceID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteDependant]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Deletes a dependant entry from the database
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDependant] 
	-- Add the parameters for the stored procedure here
	@DependantID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT
    BEGIN TRAN

	DELETE FROM dbo.Dependants WHERE DependantID = @DependantID

	SELECT @Error = @@ERROR

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN 
		ROLLBACK TRAN
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[DeleteEducationRecord]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Deletes an education record
-- =============================================
CREATE PROCEDURE [dbo].[DeleteEducationRecord] 
	-- Add the parameters for the stored procedure here
	@EducationRecordID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	BEGIN TRAN

	DELETE FROM dbo.EducationRecords WHERE EducationRecordID = @EducationRecordID

	SELECT @Error = @@ERROR

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN 
		ROLLBACK TRAN
	END

	RETURN @Error
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteEmploymentRecord]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/02/2014
-- Description:	Deletes an employment record from the database
-- =============================================
CREATE PROCEDURE [dbo].[DeleteEmploymentRecord] 
	-- Add the parameters for the stored procedure here
	@EmploymentRecordID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	BEGIN TRAN

	DELETE FROM dbo.EmploymentRecords WHERE EmploymentRecordID = @EmploymentRecordID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN 
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[DeleteIPO]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 27/03/2014
-- Description:	Deletes an IPO record
-- =============================================
CREATE PROCEDURE [dbo].[DeleteIPO] 
	@IPOID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN

	DELETE FROM dbo.IPOs 
	WHERE IPOID = @IPOID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteNote]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 18/02/2014
-- Description:	Deletes a note
-- =============================================
CREATE PROCEDURE [dbo].[DeleteNote] 
	@NoteID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT
    
	-- Before we delete the note, we need to delete the links related to the note
	BEGIN TRAN

	DELETE FROM dbo.NoteLinks WHERE NoteID1 = @NoteID OR NoteID2 = @NoteID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		-- Remove the attachments related to notes 
		DELETE FROM dbo.Attachments WHERE NoteID = @NoteID 
		SELECT @Error = @@ERROR 
	END

	IF @Error = 0 
	BEGIN 
		-- Remove the note itself
		DELETE FROM dbo.Notes WHERE NoteID = @NoteID
		SELECT @Error = @@ERROR 
	END

	-- check for errors 
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteNoteAttachment]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 18/03/2014
-- Description:	Deletes an attachment record from the database
-- =============================================
CREATE PROCEDURE [dbo].[DeleteNoteAttachment] 
	-- Add the parameters for the stored procedure here
	@AttachmentID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN
    DELETE FROM dbo.Attachments WHERE AttachmentID = @AttachmentID 

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[DeletePolicy]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/03/2014
-- Description:	Deletes a policy entry
-- =============================================
CREATE PROCEDURE [dbo].[DeletePolicy] 
	@PolicyID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN
	-- First delete all linked policydetails 
	DELETE FROM dbo.PolicyDetails
	WHERE PolicyID = @PolicyID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		-- Finally delete the policy record
		DELETE FROM dbo.Policies
		WHERE PolicyID = @PolicyID

		SELECT @Error = @@ERROR 
	END

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[DeletePolicyDetail]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/03/2014
-- Description:	Deletes a policy detail
-- =============================================
CREATE PROCEDURE [dbo].[DeletePolicyDetail] 
	@PolicyDetailID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN

	DELETE FROM dbo.PolicyDetails 

	WHERE PolicyDetailID = @PolicyDetailID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN 
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteQualification]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 25/03/2014
-- Description:	Deletes a qualification
-- =============================================
CREATE PROCEDURE [dbo].[DeleteQualification] 
	@QualificationID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN

	DELETE FROM dbo.Qualifications 
	WHERE QualificationID = @QualificationID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteSecurity]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 26/03/2014
-- Description:	Deletes a securities entry
-- =============================================
CREATE PROCEDURE [dbo].[DeleteSecurity] 
	-- Add the parameters for the stored procedure here
	@SecuritiesID nvarchar(128)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN

	DELETE FROM dbo.Securities 
	WHERE SecuritiesID = @SecuritiesID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[DeleteStatement]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 25/03/2014
-- Description:	Deletes a statement entry
-- =============================================
CREATE PROCEDURE [dbo].[DeleteStatement] 
	@StatementID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN
	DELETE FROM dbo.Statements
	WHERE StatementID = @StatementID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[GetAccountExpenses]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 19/03/2014
-- Description:	Returns a list of expenses related to an account
-- =============================================
CREATE PROCEDURE [dbo].[GetAccountExpenses] 
	@AssetTypeID int,
	@AccountID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		E.ExpenseID,
		E.DateCreated, 
		T.ExpenseType,
		E.Description, 
		E.Frequency,
		E.Amount,
		E.PaymentDate,
		E.Comments,
		E.DateModified

	FROM dbo.Expenses E
	INNER JOIN dbo.ExpenseTypes T ON E.ExpenseTypeID = T.ExpenseTypeID

	WHERE E.AssetTypeID = @AssetTypeID AND E.AccountID = @AccountID 
END


GO
/****** Object:  StoredProcedure [dbo].[GetAccountNote]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 19/03/2014
-- Description:	Returns a list of notes related to a particular account (asset/liaibility)
-- =============================================
CREATE PROCEDURE [dbo].[GetAccountNote] 
	@AssetTypeID int, 
	@AccountID nvarchar(128) ,
	@ClientID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		N.NoteID,
		N.DateCreated,
		N.TimeSpent,
		N.Subject,
		N.NoteSerial
	FROM dbo.Notes N

	WHERE 
		N.AssetTypeID = @AssetTypeID 
	AND N.AccountID = @AccountID 
	AND N.ClientID = @ClientID

END


GO
/****** Object:  StoredProcedure [dbo].[GetAccountStatements]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 25/03/2014
-- Description:	Returns a list of statements for a particular account
-- =============================================
CREATE PROCEDURE [dbo].[GetAccountStatements] 
	@AssetTypeID int ,
	@AccountID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		S.StatementID,
		S.AssetTypeID,
		S.AccountID,
		S.StatementDate, 
		S.Description,
		S.Balance,
		S.Credit,
		S.Debit,
		S.Amount,
		S.DateCreated
		
	FROM dbo.Statements S 
	WHERE AssetTypeID = @AssetTypeID AND AccountID = @AccountID

	ORDER BY S.StatementDate DESC 
END


GO
/****** Object:  StoredProcedure [dbo].[GetAdviser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 12 May 2014
-- Description:	Returns an advisers record
-- =============================================
CREATE PROCEDURE [dbo].[GetAdviser]
	@AdviserID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		A.UserId,
		U.Id,
		U.Email,
		A.FirstName,
		A.LastName,
		U.UserName,
		AD.AdviserDescription,
		AP.ImageData,
		A.ExperienceStartDate,
		ADGD.AuthorisedRepNumber,
		ADGD.AFSL,
		ADGD.DealerGroupName

	FROM dbo.Adviser_Details A
	INNER JOIN dbo.AspNetUsers U ON U.Id = A.UserID	
	INNER JOIN dbo.Adviser_Description AD ON AD.UserId = A.UserID	
	INNER JOIN dbo.Adviser_ProfilePictures AP ON AP.UserId = A.UserId
	INNER JOIN dbo.Adviser_DealerGroupDetails ADGD ON ADGD.UserId = A.UserId
	WHERE A.UserId = @AdviserID 
END


GO
/****** Object:  StoredProcedure [dbo].[GetAdviserCapabilities]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 27/02/2014
-- Description:	Returns a list of capability settings related to a particular Adviser
-- =============================================
CREATE PROCEDURE [dbo].[GetAdviserCapabilities] 
	-- Add the parameters for the stored procedure here
	@AdviserID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		CL.AdviserID,
		CL.CapabilityTypeID,
		CT.CapabilityType [Capability Type],
		CT.CapabilityGroupID,
		CG.CapabilityGroup [Capability Group],
		CL.Capable [Capable]

	FROM dbo.AdviserCapabilityLinks CL 
	INNER JOIN dbo.CapabilityTypes CT ON CL.CapabilityTypeID = CT.CapabilityTypeID
	INNER JOIN dbo.CapabilityGroups CG ON CT.CapabilityGroupID = CG.CapabilityGroupID

	WHERE (CL.AdviserID = @AdviserID OR @AdviserID IS NULL)
END


GO
/****** Object:  StoredProcedure [dbo].[GetAdviserCapabilitySummary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: Wai Yin Chung
-- Description:	Returns a list of adviser IDs and their capabilities
-- =============================================
CREATE PROCEDURE [dbo].[GetAdviserCapabilitySummary] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @cols varchar(max)
	SELECT @cols = STUFF((SELECT DISTINCT TOP 100 PERCENT '],[' +  CT.CapabilityType FROM dbo.CapabilityTypes CT ORDER BY '],[' +  CT.CapabilityType FOR XML PATH('')) , 1, 2, '') + ']'

	DECLARE @sql varchar(max)
	SELECT @sql = 
		N'SELECT
			Email,
			AdviserID, ' + @cols +	'
		FROM
			(SELECT 
				U.Email,
				A.AdviserID,
				CT.CapabilityType,
				CONVERT(INT,CL.Capable)[Capable]

			FROM dbo.Users U
			INNER JOIN dbo.Advisers A ON U.UserID = A.UserID
			LEFT JOIN dbo.AdviserCapabilityLinks CL ON A.AdviserID = CL.AdviserID
			FULL OUTER JOIN dbo.CapabilityTypes CT ON CL.CapabilityTypeID = CT.CapabilityTypeID) AS Source
		PIVOT( 
			MIN(Capable)
			FOR [CapabilityType] IN (' + @cols + ')
		) AS PivotTable

		WHERE Email IS NOT NULL
		'

	EXEC (@sql)
END


GO
/****** Object:  StoredProcedure [dbo].[GetAdviserClientNote]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 19/03/2014
-- Description:	Returns message headers for adviser/client
-- =============================================
CREATE PROCEDURE [dbo].[GetAdviserClientNote] 
	-- Add the parameters for the stored procedure here
	@AdviserID nvarchar(128) = NULL, 
	@ClientID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		N.NoteID,
		N.DateCreated, 
		U.FirstName + ' ' + U.LastName [Individual], 
		N.Subject,
		N.TimeSpent 

	FROM dbo.Notes N 
	INNER JOIN dbo.Clients C ON N.ClientID = C.ClientID 
	INNER JOIN dbo.UserData U ON C.UserID = U.UserID 
	
	WHERE (N.ClientID = @ClientID) OR 
	(N.AdviserID = @AdviserID)
END


GO
/****** Object:  StoredProcedure [dbo].[GetAdviserPortfolioSummary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 05 May 2014
-- Description:	Gets the portfolio summary for a particular adviser
-- =============================================
CREATE PROCEDURE [dbo].[GetAdviserPortfolioSummary]
	@AdviserID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		A.UserId,
		CG.ClientGroupID,
		SUM(CASE WHEN IT.InvestmentTypeID = 1 THEN SH.Units * DP.Price END) [AustralianEquity],
		SUM(CASE WHEN IT.InvestmentTypeID = 2 THEN SH.Units * DP.Price END) [InternationalEquity],
		SUM(CASE WHEN IT.InvestmentTypeID = 3 THEN SH.Units * DP.Price END) [ManagedInvestment],
		SUM(CASE WHEN IT.InvestmentTypeID = 4 THEN SH.Units * DP.Price END) [MiscInvestment],
		SUM(D.PurchasedPrice) [PropertyValue],
		SUM(FH.MaturityValue) [FixedIncome],
		SUM(CH.DebitInterest) [CashHoldings],
		SUM(L.AccountBalance) [Loans],
		SUM(ML.TotalBalance) [MarginLoans]

	FROM dbo.Adviser_Details A 
	INNER JOIN dbo.ClientGroups CG ON A.UserId = CG.AdviserID
	INNER JOIN dbo.Clients C ON CG.ClientGroupID = C.ClientGroupID 
	LEFT JOIN dbo.DirectProperties D ON C.ClientID = D.ClientID
	LEFT JOIN dbo.CashHoldings CH ON C.ClientID = CH.ClientID
	LEFT JOIN dbo.FixedIncomeHoldings FH ON C.ClientID = FH.ClientID
	LEFT JOIN dbo.StockHoldings SH ON C.ClientID = SH.ClientID
	INNER JOIN dbo.Stocks S ON SH.StockID = S.StockID
	INNER JOIN dbo.InvestmentTypes IT ON S.InvestmentTypeID = IT.InvestmentTypeID 
	INNER JOIN dbo.DailyPrices DP ON S.StockID = DP.StockID 
	INNER JOIN 
	(
		SELECT 
			MAX(Date) MaxDate,
			StockID
		FROM dbo.DailyPrices DP
		GROUP BY StockID ) DP1 ON DP1.MaxDate = DP.Date AND DP.StockID = DP1.StockID
	LEFT JOIN dbo.Loans L ON C.ClientID = L.ClientID
	LEFT JOIN dbo.MarginLoans ML ON C.ClientID = ML.ClientID
	
	WHERE A.UserId = @AdviserID

	GROUP BY A.UserId, CG.ClientGroupID
END


GO
/****** Object:  StoredProcedure [dbo].[GetAdviserSideTabReminder]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 11/04/2014
-- Description:	Returns a list of reminders for Adviser reminders
-- =============================================
CREATE PROCEDURE [dbo].[GetAdviserSideTabReminder] 
	@AdviserID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		A.UserId, 
		COUNT(cb1.ClientID) [Clients_Having_Birthday],
		cb2.NumberOfClients [Clients_Turning_55_NextMonth],
		COUNT(cr1.ClientID) [Clients_Entering_Retirement],
		COUNT(AP.AppointmentID) [Portfolio_Reviews],
		COUNT(P.PolicyID) [Policies_Due_Today], 
		COUNT(F.FixedIncomeID) [Term_Deposits_Maturity_Due],
		COUNT(I.IPOID ) [IPOs_Due_Next_14_Days]

	FROM dbo.Adviser_Details A 
	LEFT JOIN dbo.ClientGroups CG ON A.UserId = CG.AdviserID 
	LEFT JOIN dbo.Clients C ON CG.ClientGroupID = C.ClientGroupID
	LEFT JOIN dbo.client_birthdays cb1 ON A.UserId = cb1.AdviserID
	LEFT JOIN dbo.ClientsPendingBirthdays(55) cb2 ON A.UserId = cb2.AdviserID 
	LEFT JOIN dbo.clients_entering_retirement cr1 ON A.UserId = cr1.AdviserID 
	LEFT JOIN dbo.Appointments AP ON A.UserId = AP.AdviserID 
	LEFT JOIN dbo.Policies P ON C.ClientID = P.ClientID 
	LEFT JOIN dbo.FixedIncomeHoldings F ON C.ClientID = F.ClientID ,
	dbo.IPOs I
	
	WHERE A.UserId = @AdviserID 
	AND ((DatePart(month, cb1.DateOfBirth) = DatePart(month, GetDate())) AND (DatePart(day, cb1.DateOfBirth) = DatePart(day, GetDate())))
	AND ((CAST(Ap.AppointmentTime AS DATE) = CAST(GetDate()  AS DATE)) AND (Ap.AppointmentType = 'Portfolio Review'))
	AND (CAST(F.NextPaymentDate AS DATE) = CAST(GetDate() AS DATE))
	AND (DATEDIFF(d, GetDate(), I.BrokerClosingDate)) < 14 
	GROUP BY A.UserId, cb2.NumberOfClients
END


GO
/****** Object:  StoredProcedure [dbo].[GetAppointmentDetail]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 09/04/2014
-- Description:	Returns details of an appointment
-- =============================================
CREATE PROCEDURE [dbo].[GetAppointmentDetail] 
	@AppointmentID nvarchar(128) = NULL ,
	@AdviserID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		AP.AppointmentID, 
		AP.AdviserID,
		AP.ClientID,
		UD.FirstName,
		UD.LastName, 
		UD.MobilePhone,
		M.Email, 
		AP.AppointmentType,
		AP.AppointmentTime, 
		AP.Title,
		AP.Details, 
		AP.DurationHours, 
		AP.Comments, 
		AP.DateCreated,
		AP.DateModified
	FROM dbo.Appointments AP
	LEFT JOIN dbo.Clients C ON AP.ClientID = C.ClientID
	INNER JOIN dbo.AspNetUsers U ON U.Id = C.UserID 
	INNER JOIN dbo.UserData UD ON U.Id = UD.UserID
	INNER JOIN dbo.AspNetUsers M ON U.Id = M.Id 
	WHERE (AP.AppointmentID = @AppointmentID OR @AppointmentID IS NULL)
	AND (AP.AdviserID = @AdviserID)
END


GO
/****** Object:  StoredProcedure [dbo].[GetAttachmentContents]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 18/03/2014
-- Description:	Downloads an attachment file
-- =============================================
CREATE PROCEDURE [dbo].[GetAttachmentContents] 
	-- Add the parameters for the stored procedure here
	@AttachmentID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		Data
	FROM dbo.Attachments
	WHERE AttachmentID = @AttachmentID 

END


GO
/****** Object:  StoredProcedure [dbo].[GetBeneficiaries]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 26/03/2014
-- Description:	Selects all beneficiaries related to a particular policyid
-- =============================================
CREATE PROCEDURE [dbo].[GetBeneficiaries] 
	@PolicyID nvarchar(128),
	@BeneficiaryID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		B.BeneficiaryID,
		B.PolicyID,
		B.FirstName,
		B.LastName,
		B.Relationship,
		B.Address1,
		B.Address2,
		B.Address3,
		B.City,
		B.State,
		B.PostCode,
		B.Phone,
		B.Mobile,
		B.Email,
		B.BeneficiaryPercentage,
		B.DateCreated,
		B.DateModified

	FROM dbo.Beneficiaries B

	WHERE B.PolicyID = @PolicyID AND (B.BeneficiaryID = @BeneficiaryID OR @BeneficiaryID IS NULL)
END


GO
/****** Object:  StoredProcedure [dbo].[GetBookmarks]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 03/04/2014
-- Description:	Selects a single or a group of bookmarks depending on the parameters provided
-- =============================================
CREATE PROCEDURE [dbo].[GetBookmarks] 
	@UserID nvarchar(128) ,
	@BookmarkID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		B.UserID,
		B.BookmarkID,
		B.BookmarkName,
		B.BookmarkLink,
		B.Comments,
		B.DateCreated,
		B.DateModified
	FROM dbo.Bookmarks B

	WHERE UserID = @UserID AND 
	(BookmarkID = @BookmarkID OR @BookmarkID IS NULL) 

	ORDER BY B.BookmarkName

END


GO
/****** Object:  StoredProcedure [dbo].[GetCapabilityGroups]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/02/2014
-- Description:	Gets a list of Capability Groups available
-- =============================================
CREATE PROCEDURE [dbo].[GetCapabilityGroups] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		CapabilityGroupID,
		CapabilityGroup
	FROM dbo.CapabilityGroups
END


GO
/****** Object:  StoredProcedure [dbo].[GetCapabilityTypes]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/02/2014
-- Description:	Gets a list of capability types
-- =============================================
CREATE PROCEDURE [dbo].[GetCapabilityTypes] 
	-- Add the parameters for the stored procedure here
	@CapabilityGroupID INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		CT.CapabilityGroupID,
		CG.CapabilityGroup,
		CT.CapabilityTypeID,
		CT.CapabilityType
	FROM dbo.CapabilityTypes CT
	INNER JOIN dbo.CapabilityGroups CG
	ON CT.CapabilityGroupID = CG.CapabilityGroupID

	-- only return the group requested, but if null is passed in, then all capabilty groups are included
	WHERE @CapabilityGroupID IS NULL
	OR CT.CapabilityGroupID = @CapabilityGroupID 

END


GO
/****** Object:  StoredProcedure [dbo].[GetCashHoldings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Returns a list or a single record of cash holdings record
-- =============================================
CREATE PROCEDURE [dbo].[GetCashHoldings] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128), 
	@CashHoldingsID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		CH.ClientID,
		CH.CashHoldingsID,
		CH.AssetTypeID,
		CH.InterestRate,
		CH.Frequency,
		CH.NextPayment,
		CH.DebitInterest,
		CH.DateOpened,
		CH.DateCreated,
		CH.DateModified,
		CH.StatementMethod

	FROM dbo.CashHoldings CH

	WHERE CH.ClientID = @ClientID AND (CH.CashHoldingsID = @CashHoldingsID OR @CashHoldingsID IS NULL)

END


GO
/****** Object:  StoredProcedure [dbo].[GetClientComplianceScoreSummary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 20 May 2014
-- Description:	Returns the compliance score summary for a particular client
-- =============================================
CREATE PROCEDURE [dbo].[GetClientComplianceScoreSummary]
	@ClientID nvarchar(128) = NULL, 
	@ClientGroupID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		cg.ClientGroupID ,
		c.clientid,
		ccss.ClientFileComplianceScore,
		case when ccss.ClientFileComplianceScore = 5.0 THEN 'Compliant' ELSE 'Non-Compliant' END  [ClientFileCompliance],
		ccss.EmploymentDetailsScore,
		ccss.ComplianceExpenseScore,
		ccss.ComplianceIncomeScore,
		case when ccss.EmploymentDetailsScore + ccss.ComplianceExpenseScore + ccss.ComplianceIncomeScore= 15.0 THEN 'Compliant' ELSE 'Non-Compliant' END [EmploymentDetailsCompliance],
		ccss.InvestmentProfileComplianceScore, 
		case when ccss.InvestmentProfileComplianceScore = 15.0 THEN 'Compliant' ELSE 'Non-Compliant' END [InvestmentProfileCompliance],
		ccss.PersonalDetailsComplianceScore,
		case when ccss.PersonalDetailsComplianceScore = 5.0 THEN 'Compliant' ELSE 'Non-Compliant' END [PersonalDetailsCompliance],
		ccss.ServiceLevelActionsComplianceScore,
		case when ccss.ServiceLevelActionsComplianceScore = 20.0 THEN  'Compliant' ELSE 'Non-Compliant' END [ServiceLevelActionsCompliance]
	FROM dbo.ClientGroups cg 
	INNER JOIN dbo.Clients c on c.ClientGroupID = cg.ClientGroupID
	INNER JOIN dbo.show_client_compliance_score_summary ccss on c.clientid = ccss.clientid 

	WHERE cg.clientgroupid = @ClientGroupID 
	AND (c.Clientid = @clientid OR @clientID IS NULL )
END


GO
/****** Object:  StoredProcedure [dbo].[GetClientFileInformations]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 01 May 2014
-- Description:	Returns a set of clientfile information actions for a particular client 
-- =============================================
CREATE PROCEDURE [dbo].[GetClientFileInformations]
	@ClientID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		CI.ClientID,
		CI.ClientFileID,
		CA.ClientFileActionID,
		CA.ClientFileAction,
		CI.Response,
		CI.ResponseDate,
		CI.DateCreated,
		CI.DateModified

	FROM dbo.ClientFileInformations CI 
	INNER JOIN dbo.ClientFileActions CA ON Ci.ClientActionID = CA.ClientFileActionID

	WHERE CI.ClientID = @ClientID 
END


GO
/****** Object:  StoredProcedure [dbo].[GetClientGroup]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 12 May 2014
-- Description:	Returns the data record of a ClientGroup Record
-- =============================================
CREATE PROCEDURE [dbo].[GetClientGroup]
	@ClientGroupID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		CG.ClientGroupID,
		CG.AdviserID,
		CG.AccountName,
		CG.AccountNo,
		CG.Alias,
		CG.MainClientID,
		CG.RiskProfileOutcome,
		CG.ServiceLevelID,
		CG.DateCreated,
		CG.DateModified

	FROM dbo.ClientGroups CG
	WHERE CG.ClientGroupID = @ClientGroupID

END


GO
/****** Object:  StoredProcedure [dbo].[GetClientGroupComplianceScoreSummary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 20 May 2014
-- Description:	Returns the compliance score summary for a particular client group
-- =============================================
CREATE PROCEDURE [dbo].[GetClientGroupComplianceScoreSummary]
	@ClientGroupID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		cg.ClientGroupID ,
		SUM(ccss.ClientFileComplianceScore) [ClientFileComplianceScore],
		SUM(ccss.ComplianceExpenseScore) [CompliacneExpenseScore],
		SUM(ccss.ComplianceIncomeScore) [ComplianceIncomeScore],
		SUM(ccss.EmploymentDetailsScore) [EmploymentDetailsScore],
		SUM(ccss.InvestmentProfileComplianceScore) [InvestmentProfileComplianceScore],
		SUM(ccss.PersonalDetailsComplianceScore) [PersonalDetailsComplianceScore],
		SUM(ccss.ServiceLevelActionsComplianceScore) [ServiceLevelActionsComplianceScore]
			
	FROM dbo.ClientGroups cg 
	INNER JOIN dbo.Clients c on c.ClientGroupID = cg.ClientGroupID
	INNER JOIN dbo.show_client_compliance_score_summary ccss on c.clientid = ccss.clientid 

	WHERE cg.clientgroupid = @ClientGroupID 

	GROUP BY cg.ClientGroupID 
END




GO
/****** Object:  StoredProcedure [dbo].[GetClientGroupIDFromAccountNo]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 05 May 2014
-- Description:	Returns the UserID based on a AccountNo
-- =============================================
CREATE PROCEDURE [dbo].[GetClientGroupIDFromAccountNo]
	@AccountNo INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ClientGroupID FROM dbo.ClientGroups WHERE AccountNo = @AccountNo 
END


GO
/****** Object:  StoredProcedure [dbo].[GetClientGroupPortfolioSummary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07 May 2014
-- Description:	Gets the portfolio summary for a particular client group
-- =============================================
CREATE PROCEDURE [dbo].[GetClientGroupPortfolioSummary] 
	@ClientGroupID nvarchar(128) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- TODO, sort out the formulas 
	SELECT 
		CG.ClientGroupID,
		CG.Alias,
		CG.AccountName,
		CG.AccountNo,
		COUNT(C.ClientID)[NumberOfClients],
		SUM(CASE WHEN IT.InvestmentTypeID = 1 THEN SH.Units * DP.Price END) [AustralianEquity],
		SUM(CASE WHEN IT.InvestmentTypeID = 2 THEN SH.Units * DP.Price END) [InternationalEquity],
		SUM(CASE WHEN IT.InvestmentTypeID = 3 THEN SH.Units * DP.Price END) [ManagedInvestment],
		SUM(CASE WHEN IT.InvestmentTypeID = 4 THEN SH.Units * DP.Price END) [MiscInvestment],
		SUM(D.PurchasedPrice) [PropertyValue],
		SUM(FH.MaturityValue) [FixedIncome],
		SUM(CH.DebitInterest) [CashHoldings],
		SUM(L.AccountBalance) [Loans],
		SUM(ML.TotalBalance) [MarginLoans]

	FROM dbo.ClientGroups CG 
	LEFT JOIN dbo.Clients C ON CG.ClientGroupID = C.ClientGroupID
	INNER JOIN dbo.Advisers A ON CG.AdviserID = A.AdviserID
	LEFT JOIN dbo.DirectProperties D ON C.ClientID = D.ClientID
	LEFT JOIN dbo.CashHoldings CH ON C.ClientID = CH.ClientID
	LEFT JOIN dbo.FixedIncomeHoldings FH ON C.ClientID = FH.ClientID
	LEFT JOIN dbo.StockHoldings SH ON C.ClientID = SH.ClientID
	INNER JOIN dbo.Stocks S ON SH.StockID = S.StockID
	INNER JOIN dbo.InvestmentTypes IT ON S.InvestmentTypeID = IT.InvestmentTypeID 
	INNER JOIN dbo.DailyPrices DP ON S.StockID = DP.StockID 
	INNER JOIN 
	(
		SELECT 
			MAX(Date) MaxDate,
			StockID
		FROM dbo.DailyPrices DP
		GROUP BY StockID ) DP1 ON DP1.MaxDate = DP.Date AND DP.StockID = DP1.StockID
	LEFT JOIN dbo.Loans L ON C.ClientID = L.ClientID
	LEFT JOIN dbo.MarginLoans ML ON C.ClientID = ML.ClientID
	
	WHERE CG.ClientGroupID = @ClientGroupID 

	GROUP BY 
		CG.ClientGroupID,
		CG.Alias,
		CG.AccountName,
		CG.AccountNo
END


GO
/****** Object:  StoredProcedure [dbo].[GetClientInfo]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Gets the basic information from a client
-- =============================================
CREATE PROCEDURE [dbo].[GetClientInfo] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		U.UserID
		,C.ClientID
		,CG.ClientGroupID
		,CG.AccountNo
		,CG.AccountName
		,CG.Alias
		,U.Title
		,U.FirstName
		,U.MiddleName
		,U.LastName
		,U.Gender
		,U.PreferredName
		,U.DateOfBirth
		,CONVERT(DECIMAL(18,2), DATEDIFF(Month, U.DateOfBirth, GetDate()) / 12.0) [Age]
		,U.MaritalStatus
		,C.DesignationAccount
		,U.CompanyName
		,U.Position
		,U.ABN
		,U.ACN
		,U.AddressLine1
		,U.AddressLine2
		,U.City
		,U.State
		,U.Postcode
		,U.Country
		,U.PostalAddressLine1
		,U.PostalAddressLine2
		,U.PostalCity
		,U.PostalState	
		,U.PostalPostcode
		,U.PostalCountry
		,U.MobilePhone
		,U.HomePhone
		,U.WorkPhone
		,U.WorkEmail
		,U.HomeEmail
		,SL.ServiceLevelName
	FROM dbo.UserData U
	INNER JOIN dbo.Clients C ON U.UserID = C.UserID
	LEFT JOIN dbo.ClientGroups CG ON CG.ClientGroupID = C.ClientGroupID 
    LEFT JOIN dbo.ServiceLevels SL ON CG.ServiceLevelID = SL.ServiceLevelID 

	WHERE ClientID = @ClientID


END


GO
/****** Object:  StoredProcedure [dbo].[GetClientPortfolioSummary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 19/03/2014
-- Description:	Gets the portfolio summary for a particular client
-- =============================================
CREATE PROCEDURE [dbo].[GetClientPortfolioSummary] 
	@ClientID nvarchar(128) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- TODO, sort out the formulas 
	SELECT 
		C.ClientID,
		SUM(CASE WHEN IT.InvestmentTypeID = 1 THEN SH.Units * DP.Price END) [AustralianEquity],
		SUM(CASE WHEN IT.InvestmentTypeID = 2 THEN SH.Units * DP.Price END) [InternationalEquity],
		SUM(CASE WHEN IT.InvestmentTypeID = 3 THEN SH.Units * DP.Price END) [ManagedInvestment],
		SUM(CASE WHEN IT.InvestmentTypeID = 4 THEN SH.Units * DP.Price END) [MiscInvestment],
		SUM(D.PurchasedPrice) [PropertyValue],
		SUM(FH.MaturityValue) [FixedIncome],
		SUM(CH.DebitInterest) [CashHoldings],
		SUM(L.AccountBalance) [Loans],
		SUM(ML.TotalBalance) [MarginLoans]

	FROM dbo.Clients C
	INNER JOIN dbo.ClientGroups CG ON C.ClientGroupID = CG.ClientGroupID
	INNER JOIN dbo.Advisers A ON CG.AdviserID = A.AdviserID
	LEFT JOIN dbo.DirectProperties D ON C.ClientID = D.ClientID
	LEFT JOIN dbo.CashHoldings CH ON C.ClientID = CH.ClientID
	LEFT JOIN dbo.FixedIncomeHoldings FH ON C.ClientID = FH.ClientID
	LEFT JOIN dbo.StockHoldings SH ON C.ClientID = SH.ClientID
	INNER JOIN dbo.Stocks S ON SH.StockID = S.StockID
	INNER JOIN dbo.InvestmentTypes IT ON S.InvestmentTypeID = IT.InvestmentTypeID 
	INNER JOIN dbo.DailyPrices DP ON S.StockID = DP.StockID 
	INNER JOIN 
	(
		SELECT 
			MAX(Date) MaxDate,
			StockID
		FROM dbo.DailyPrices DP
		GROUP BY StockID ) DP1 ON DP1.MaxDate = DP.Date AND DP.StockID = DP1.StockID
	LEFT JOIN dbo.Loans L ON C.ClientID = L.ClientID
	LEFT JOIN dbo.MarginLoans ML ON C.ClientID = ML.ClientID
	WHERE C.ClientID = @ClientID 

	GROUP BY C.ClientID
END


GO
/****** Object:  StoredProcedure [dbo].[GetClientStockSuitabilityScores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 28 May 2014
-- Description:	Gets the suitability scores in a portfolio for a particular client or clientgroup
-- =============================================
CREATE PROCEDURE [dbo].[GetClientStockSuitabilityScores]
	@ClientGroupID nvarchar(128),
	@ClientID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		C.ClientGroupID,
		C.ClientID,
		S.StockID,
		S.Name,
		SH.Units,
		CASE 
			WHEN a.StockID IS NOT NULL THEN a.TotalSuitabilityScore 
			WHEN i.StockID IS NOT NULL THEN i.TotalSuitabilityScore
			WHEN m.StockID IS NOT NULL THEN m.TotalSuitabilityScore
			ELSE 200 
		END [TotalSuitabilityScore],
		CASE 
			WHEN a.StockID IS NOT NULL THEN a.Recommendation
			WHEN i.StockID IS NOT NULL THEN i.Recommendation
			WHEN m.StockID IS NOT NULL THEN m.Recommendation
			ELSE 'Danger - Not on APL' 
		END [Recommendation]

	FROM dbo.Clients C
	INNER JOIN dbo.StockHoldings SH ON C.ClientID = SH.ClientID 
	INNER JOIN dbo.Stocks S ON SH.StockID = S.StockID
	LEFT JOIN dbo.show_aust_equity_suitability_scores a ON S.StockID = a.StockID
	LEFT JOIN dbo.show_inter_equity_suitability_scores i ON S.StockID = i .StockID
	LEFT JOIN dbo.show_managed_funds_suitability_scores m ON S.StockID = m.StockID

	WHERE C.ClientGroupID = @ClientGroupID 
	AND (C.ClientID = @ClientID OR @ClientID IS NULL)
END


GO
/****** Object:  StoredProcedure [dbo].[GetComplianceIncomeExpenseRecord]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 30 Apr 2014
-- Description:	Gets a list of compliance income expense records
-- =============================================
CREATE PROCEDURE [dbo].[GetComplianceIncomeExpenseRecord]
	@ClientID nvarchar(128),
	@ComplianceIncomeExpenseID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		ComplianceIncomeExpensesID
		,ClientID
		,Centrelink
		,ReceivedMaintenance
		,SuperannunationPension
		,OtherTaxableIncome
		,OtherForeignIncome
		,NonTaxableIncome
		,FoodExpenses
		,ClothingExpenses
		,MedicalExpenses
		,UtilitiesBills
		,CommunicationsBills
		,Furniture
		,MortgageRental
		,HomeInsurance
		,VehicleInsurance
		,Repairs
		,CouncilRates
		,VehicleRegistration
		,Petrol
		,VehicleLoans
		,Entertainment
		,HolidayTravel
		,DateCreated
		,DateModified
	FROM dbo.ComplianceIncomeExpense 
	WHERE ClientID = @ClientID 
	AND (ComplianceIncomeExpenseID = @ComplianceIncomeExpenseID OR   @ComplianceIncomeExpenseID IS NULL)


END



GO
/****** Object:  StoredProcedure [dbo].[GetDealerGroup]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 12 May 2014
-- Description:	Returns a dealer group record
-- =============================================
CREATE PROCEDURE [dbo].[GetDealerGroup]
	@DealerGroupID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT 
		DG.DealerGroupID,
		DG.DealerGroupName,
		DG.DateCreated,
		DG.DateModified

	FROM dbo.DealerGroups DG
	WHERE DG.DealerGroupID = @DealerGroupID
END


GO
/****** Object:  StoredProcedure [dbo].[GetDealerGroupAdvisers]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 12 May 2014
-- Description:	Returns the advisers information for a particular DealerGroup
-- =============================================
CREATE PROCEDURE [dbo].[GetDealerGroupAdvisers]
	@DealerGroupID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		A.AdviserID,
		U.UserID,
		UD.FirstName,
		UD.LastName,
		UD.WorkEmail,
		UD.HomeEmail,
		U.UserName,
		A.OverView,
		A.Photo,
		A.ExperienceStart,
		A.RepresentativeNo,
		A.AFSL,
		A.DealerGroupID,
		A.DateCreated,
		A.DateModified

	FROM dbo.Advisers A
	INNER JOIN dbo.aspnet_Users U ON A.UserID = U.UserID
	INNER JOIN dbo.UserData UD ON U.UserID = UD.UserID
	LEFT JOIN dbo.DealerGroups DG ON DG.DealerGroupID = A.DealerGroupID

	WHERE A.DealerGroupID = @DealerGroupID
END


GO
/****** Object:  StoredProcedure [dbo].[GetDependants]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Returns a list of dependants for a client
-- =============================================
CREATE PROCEDURE [dbo].[GetDependants] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		D.DependantID
		,D.DateOfBirth
		,D.DependantAsset
		,D.DependantIncome
		,D.FinancialDependant
		,D.FullName
		,D.Relationship
		,D.ClientID

	FROM dbo.Dependants D

	WHERE D.ClientID = @ClientID

END


GO
/****** Object:  StoredProcedure [dbo].[GetDirectProperties]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06/03/2014
-- Description:	Returns a single real estate record or a dataset of records related to a particular client
-- =============================================
CREATE PROCEDURE [dbo].[GetDirectProperties]
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128), 
	@DirectPropertyID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [DirectPropertyID]
		  ,[ClientID]
		  ,[DateCreated]
		  ,[DateModified]
		  ,[PurchasedPrice]
		  ,[StampDuty]
		  ,[PurchaseMiscFee]
		  ,[PurchaseLegalFee]
		  ,[PurchaseBankFee]
		  ,[PurchasedDate]
		  ,[SalePrice]
		  ,[AgentCommission]
		  ,[SaleMiscFee]
		  ,[SaleLegalFee]
		  ,[SaleBankFee]
		  ,[SaleLoanRepayment]
		  ,[SaleDate]
		  ,[State]
		  ,[EstimatedValue]
		  ,[RentalIncome]
		  ,[OwnershipLength]
		  ,[Country]
		  ,[Region]
		  ,[Status]
		  ,[OutstandingLoan]
		  ,[YearsOnLoan]
		  ,[LoanRateType]
		  ,[FixedTermEndDate]
		  ,[FixedRate]
		  ,[FixedLoanPercent]
		  ,[InterestRepayment]
		  ,[VariableRate]
		  ,[AgentName]
		  ,[AgentCompany]
		  ,[AgentAddress]
		  ,[AgentPhone]
		  ,[AgentFax]
		  ,[AgentEmail]
		  ,[AgentYearFrom]
		  ,[AgentYearTo]
		  ,[AssetTypeID]
	  FROM [dbo].[DirectProperties]

	  WHERE ClientID = @ClientID AND (DirectPropertyID = @DirectPropertyID OR @DirectPropertyID IS NULL)
END


GO
/****** Object:  StoredProcedure [dbo].[GetEducationRecords]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Returns a list of Education Records for a particular User
-- =============================================
CREATE PROCEDURE [dbo].[GetEducationRecords] 
	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		E.EducationRecordID
		,E.UserID
		,E.EducationLevel
		,E.YearStarted
		,E.YearCompleted
		,E.YearsSinceCompletion
		,E.EducationInstitution
		,E.CourseAttended
		,E.Description
		,E.Comments
	FROM EducationRecords E
	WHERE E.UserID = @UserID


END


GO
/****** Object:  StoredProcedure [dbo].[GetEmploymentRecords]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/02/2014
-- Description:	Returns a list of employment records for the client specified
-- =============================================
CREATE PROCEDURE [dbo].[GetEmploymentRecords] 
	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		ER.UserID
		,ER.EmploymentRecordID
		,ER.Status
		,ER.EmploymentType
		,ER.EmployerName
		,ER.Position
		,ER.StartDate
		,ER.EndDate
		,CASE WHEN ER.EndDate IS NULL 
			THEN CONVERT(NUMERIC(9,2), DATEDIFF(month, ER.StartDate, GetDate()) / 12.0) 
			ELSE CONVERT(NUMERIC(9,2), DATEDIFF(month, ER.StartDate, ER.EndDate) / 12.0) 
		END [Duration]
		,ER.HoursPerWeek
		,ER.GrossSalary
		,ER.Commissions
		,ER.AfterTaxSalary
		,ER.SuperContribution
		,ER.AdditionalSuperContribution
		,ER.MiscContribution

	FROM dbo.EmploymentRecords ER

	WHERE ER.UserID = @UserID

END


GO
/****** Object:  StoredProcedure [dbo].[GetFixedincome]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Returns a single or a set of FixedIncome Holdings records
-- =============================================
CREATE PROCEDURE [dbo].[GetFixedincome] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128) , 
	@FixedIncomeID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		ClientID
		,FixedIncomeID
		,DateCreated
		,DateModified
		,PurchaseDate
		,IssuerName
		,Ticker
		,InterestPaid
		,MaturityValue
		,FixedTerm
		,BondPrice
		,TransactionFee
		,MinimumFee
		,MaximumFee
		,AccountNo
		,AccountUserID
		,ClearingSponsor
		,HIN
		,NextPaymentDate
		,NextPaymentAmount 

	FROM dbo.FixedIncomeHoldings

	WHERE ClientID = @ClientID AND (FixedIncomeID = @FixedIncomeID OR @FixedIncomeID IS NULL)
END


GO
/****** Object:  StoredProcedure [dbo].[GetMarginLoans]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Returns a single or a dataset of margin loan records related to a particular client
-- =============================================
CREATE PROCEDURE [dbo].[GetMarginLoans] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128),
	@MarginLoanID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		MarginLoanID
		,ClientID
		,AssetTypeID
		,Description
		,HIN
		,LoanProvider
		,VariableRateLoan
		,VariableRateAmount
		,FixedRateLoan
		,FixedRateAmount
		,UnsettledTransactions
		,TotalBalance
		,NetInterestYTD
		,MonthlyInterest
		,MinBrokerage
		,MaxBrokerage
		,TotalBrokerage
		,TotalGST
		,ManagementFee
		,AdvisersComission
		,LoanLimit
		,SpendingLimit
		,AvailableCash
		,DateCreated
		,DateModified
	FROM dbo.MarginLoans

	WHERE ClientID = @ClientID AND (MarginLoanID = @MarginLoanID OR @MarginLoanID IS NULL)
END


GO
/****** Object:  StoredProcedure [dbo].[GetNoteAttachmentDetails]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 18/03/2013
-- Description:	Retrieves details of attachments (without the actual data)
-- =============================================
CREATE PROCEDURE [dbo].[GetNoteAttachmentDetails] 
	-- Add the parameters for the stored procedure here
	@NoteID nvarchar(128),
	@AttachmentID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		AttachmentID
		,NoteID
		,Path
		,Title
		,Comments
		,AttachmentType
		,DateCreated
		,DateModified
	FROM dbo.Attachments
	WHERE (AttachmentID = @AttachmentID) 
	OR ((NoteID = @NoteID) AND (@AttachmentID IS NULL))


END


GO
/****** Object:  StoredProcedure [dbo].[GetNoteDetail]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 19/03/2014
-- Description:	Gets details of a note 
-- =============================================
CREATE PROCEDURE [dbo].[GetNoteDetail] 
	-- Add the parameters for the stored procedure here
	@NoteID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		ClientID, 
		AdviserID,
		AssetClass,
		ProductClass,
		Product,
		Purpose,
		TimeSpent,
		NoteSerial,
		Subject,
		Body,
		FollowupActions,
		DateDue,
		Status,
		FollowupDate,
		DateCompleted,
		Reminder,
		ReminderDate,
		NoteTypesID,
		IsAccepted, 
		IsDeclined
		

	FROM dbo.Notes
	WHERE NoteID = @NoteID
END


GO
/****** Object:  StoredProcedure [dbo].[GetPolicies]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/03/2014
-- Description:	Gets an insurance profile or one set of profiles for a client
-- =============================================
CREATE PROCEDURE [dbo].[GetPolicies] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128) = NULL, 
	@PolicyID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		PolicyID,
		ClientID,
		InsuranceType,
		PolicyType,
		PolicyName,
		PolicyNumber,
		AccountName,
		AccountAddress1,
		AccountAddress2,
		AccountAddress3,
		AccountCity,
		AccountState,
		AccountPostCode,
		InceptionDate,
		LastRenewalDate,
		StartDate,
		EndDate,
		Commentary,
		Institution,
		RenewalDueDate, 
		DateDiff(d, LastRenewalDate, RenewalDueDate)[DaysToRenewal],
		DateCreated,
		DateModified

	FROM dbo.Policies

	WHERE (PolicyID = @PolicyID OR @PolicyID IS NULL) AND (ClientID = @ClientID ) 
END


GO
/****** Object:  StoredProcedure [dbo].[GetPolicyDetails]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/03/2014
-- Description:	Returns policy details for a particular policy
-- =============================================
CREATE PROCEDURE [dbo].[GetPolicyDetails] 
	@PolicyID nvarchar(128) = NULL, 
	@PolicyDetailID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		PD.PolicyDetailID,
		PD.PolicyID,
		PD.DetailType,
		PD.DetailName,
		PD.Description,
		PD.Amount,
		PD.StartDate,
		PD.EndDate,
		PD.DateCreated,
		PD.DateModified

	FROM dbo.PolicyDetails PD

	WHERE (PolicyDetailID = @PolicyDetailID) AND (PolicyID = @PolicyID OR @PolicyID IS NULL)
END


GO
/****** Object:  StoredProcedure [dbo].[GetQualifications]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 25/03/2014
-- Description:	Gets a list of qualifications for a particular adviser
-- =============================================
CREATE PROCEDURE [dbo].[GetQualifications] 
	@UserID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		Q.QualificationID,
		Q.UserID,
		Q.QualificationTypeID,
		Q.Qualification,
		Q.QualificationIndex

	FROM dbo.Qualifications Q

	WHERE UserID = @UserID


END


GO
/****** Object:  StoredProcedure [dbo].[GetRiskProfile]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 05/03/2014
-- Description:	Gets a risk profile for a particular client
-- =============================================
CREATE PROCEDURE [dbo].[GetRiskProfile] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [RiskProfileID]
      ,[ClientID]
      ,[ShortTermGoal1]
      ,[ShortTermGoal2]
      ,[ShortTermGoal3]
      ,[MedTermGoal1]
      ,[MedTermGoal2]
      ,[MedTermGoal3]
      ,[LongTermGoal1]
      ,[LongTermGoal2]
      ,[LongTermGoal3]
      ,[Comments]
      ,[RetirementAge]
      ,[RetirementIncome]
      ,[IncomeSource]
      ,[InvestmentObjective1]
      ,[InvestmentObjective2]
      ,[InvestmentObjective3]
      ,[InvestmentTimeHorizon]
      ,[InvestmentKnowledge]
      ,[RiskAttitude]
      ,[CapitalLossAttitude]
      ,[ShortTermTrading]
      ,[ShortTermAssetPercent]
      ,[ShortTermEquityPercent]
	  ,[InvestmentProfile]
      ,[DateCreated]
      ,[DateModified]
  FROM [dbo].[RiskProfiles]
  WHERE ClientID = @ClientID 


END


GO
/****** Object:  StoredProcedure [dbo].[GetRiskProfileProducts]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 05/03/2014
-- Description:	Gets a list of products associated with a risk profile (or client)
-- =============================================
CREATE PROCEDURE [dbo].[GetRiskProfileProducts] 
	@ClientID nvarchar(128) = NULL,
	@RiskProfileID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		C.ClientID,
		R.RiskProfileID,
		PR.ProductTypeID,
		PT.ProductType,
		PR.Selected

	FROM dbo.Clients C
	INNER JOIN dbo.RiskProfiles R ON C.ClientID = R.ClientID
	INNER JOIN dbo.ProfileProductLinks PR ON R.RiskProfileID = PR.RiskProfileID
	INNER JOIN dbo.ProductTypes PT ON PR.ProductTypeID = PT.ProductTypeID

	WHERE C.ClientID = @ClientID OR R.RiskProfileID = @RiskProfileID 
END


GO
/****** Object:  StoredProcedure [dbo].[GetSecurities]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 26/03/2014
-- Description:	Gets a list of securities for a particular margin loan
-- =============================================
CREATE PROCEDURE [dbo].[GetSecurities] 
	@SecuritiesID nvarchar(128) = NULL, 
	@MarginLoanID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		S.SecuritiesID,
		S.MarginLoanID,
		S.PurchaseDate,
		S.Company,
		S.Ticker,
		S.Units,
		S.PricePerUnit,
		S.Brokerage,
		S.MarketValue,
		S.DateCreated,
		S.DateModified
	
	FROM dbo.Securities S

	WHERE S.MarginLoanID = @MarginLoanID AND (S.SecuritiesID = @SecuritiesID OR @SecuritiesID IS NULL)
END


GO
/****** Object:  StoredProcedure [dbo].[GetStockHoldings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06/03/2014
-- Description:	Returns a single stockholding record or a dataset of records related to a particular client
-- =============================================
CREATE PROCEDURE [dbo].[GetStockHoldings] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128), 
	@StockHoldingID nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		SH.StockHoldingID
		,S.StockID
		,S.Name
		,S.Ticker
		,SH.Units
		,SH.ServiceFee
	FROM dbo.StockHoldings SH
	INNER JOIN dbo.Stocks S ON S.StockID = SH.StockID

	WHERE SH.ClientID = @ClientID AND (SH.StockHoldingID  = @StockHoldingID OR @StockHoldingID IS NULL)
END


GO
/****** Object:  StoredProcedure [dbo].[GetStockPrices]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 01/04/2014
-- Description:	Gets the latest prices for each stock or stock price on a particular date
-- =============================================
CREATE PROCEDURE [dbo].[GetStockPrices] 
	@StockID nvarchar(128) ,
	@Date date = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		DP1.StockID,
		DP1.Date,
		DP1.Price

	FROM dbo.DailyPrices DP1,
	(SELECT 
		StockID,
		MAX(Date) Date
	FROM dbo.DailyPrices
	GROUP BY StockID) DP2
	
	-- if @Date is null then the latest entry is selected as the price 
	WHERE DP1.StockID = @StockID AND 
	(DP1.Date = @Date OR (@Date IS NULL AND DP1.Date = DP2.Date))

END


GO
/****** Object:  StoredProcedure [dbo].[GetStocks]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 27/03/2014
-- Description:	Gets the stocks matching particular search parameters
-- =============================================
CREATE PROCEDURE [dbo].[GetStocks] 
	@FundStockID nvarchar(128) = NULL, 
	@InvestmentTypeID int = NULL, 
	@ExchangeID int = NULL, 
	@BaseCurrencyID int = NULL
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		S.FundStockID,
		S.StockID,
		S.Name,
		S.Ticker,
		S.APIRCode,
		S.SecID,
		S.ISIN,
		S.FirmName,
		S.BrandingName,
		S.ManagerName,
		S.ManagerName,
		S.ManagerTenureAVG,
		S.ManagerTenureLONG,
		S.ExchangeID,
		E.ExchangeName,
		E.ExchangeCountry,
		S.BusinessCountry,
		S.Domicile,
		S.BaseCurrencyID,
		C.Currency,
		S.SecurityType,
		S.InvestmentTypeID,
		IT.InvestmentType,
		S.InceptionDate,
		S.IPODate,
		S.DateMarketCap,
		S.DividedDistributionFrequency,
		S.DividedYTD,
		S.DateCreated

	FROM dbo.Stocks S
	INNER JOIN dbo.Exchanges E ON S.ExchangeID = E.ExchangeID
	INNER JOIN dbo.Currencies C ON S.BaseCurrencyID = C.CurrencyID
	INNER JOIN dbo.InvestmentTypes IT ON S.InvestmentTypeID = IT.InvestmentTypeID
	
	WHERE (S.FundStockID = @FundStockID OR @FundStockID IS NULL)
	AND (S.InvestmentTypeID = @InvestmentTypeID OR @InvestmentTypeID IS NULL)
	AND (S.ExchangeID = @ExchangeID OR @ExchangeID IS NULL)
	AND (S.BaseCurrencyID = @BaseCurrencyID OR @BaseCurrencyID IS NULL) 
END


GO
/****** Object:  StoredProcedure [dbo].[GetStockSuitabilityScores]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 28 May 2014
-- Description:	Gets the suitability scores that fits a particular criteria 
-- =============================================
CREATE PROCEDURE [dbo].[GetStockSuitabilityScores]
	@FundStockID nvarchar(128) = NULL,
	@StockID nvarchar(128) = NULL, 
	@Recommendation varchar(20) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		F.FundStockID,
		S.StockID,
		S.Name,
		CASE 
			WHEN a.StockID IS NOT NULL THEN a.TotalSuitabilityScore 
			WHEN i.StockID IS NOT NULL THEN i.TotalSuitabilityScore
			WHEN m.StockID IS NOT NULL THEN m.TotalSuitabilityScore
			ELSE 200 
		END [TotalSuitabilityScore],
		CASE 
			WHEN a.StockID IS NOT NULL THEN a.Recommendation
			WHEN i.StockID IS NOT NULL THEN i.Recommendation
			WHEN m.StockID IS NOT NULL THEN m.Recommendation
			ELSE 200 
		END [Recommendation]

	FROM dbo.FundStocks F
	INNER JOIN dbo.Stocks S ON F.FundStockID = S.FundStockID
	LEFT JOIN dbo.show_aust_equity_suitability_scores a ON S.StockID = a.StockID
	LEFT JOIN dbo.show_inter_equity_suitability_scores i ON S.StockID = i .StockID
	LEFT JOIN dbo.show_managed_funds_suitability_scores m ON S.StockID = m.StockID

	WHERE (F.FundStockID = @FundStockID OR @FundStockID IS NULL)
	AND (S.StockID = @StockID OR @StockID IS NULL)
	AND (a.Recommendation = @Recommendation OR i.Recommendation = @Recommendation OR m.Recommendation = @Recommendation OR @Recommendation IS NULL)
END



GO
/****** Object:  StoredProcedure [dbo].[GetTransactions]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 19/03/2014
-- Description:	Returns a list of Transactions related to a particular account
-- =============================================
CREATE PROCEDURE [dbo].[GetTransactions] 
	-- Add the parameters for the stored procedure here
	@AssetTypeID int,
	@AccountID nvarchar(128),
	@TransactionTypeID int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		T.TransactionDate,
		T.ContractNo,
		T.Units,
		T.CostPerUnit,
		T.BrokerageFee,
		T.StampDuty, 
		T.LegalFee,
		T.EstablishmentFee,
		(T.BrokerageFee + T.StampDuty + T.LegalFee +	T.EstablishmentFee )* 0.10 [GST],
		(T.Units * T.CostPerUnit) + (T.BrokerageFee + T.StampDuty + T.LegalFee +	T.EstablishmentFee ) * 1.1 [NetValue(InclGST)],
		(T.Units * T.CostPerUnit) + (T.BrokerageFee + T.StampDuty + T.LegalFee +	T.EstablishmentFee )  [NetValue(ExclGST)]


	FROM dbo.Transactions T
	WHERE (AssetTypeID = @AssetTypeID) 
	AND (AccountID = @AccountID)
	AND (TransactionTypeID = @TransactionTypeID OR @TransactionTypeID IS NULL)

END


GO
/****** Object:  StoredProcedure [dbo].[GetUserData]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 02 May 2014
-- Description:	Returns a UserData profile
-- =============================================
CREATE PROCEDURE [dbo].[GetUserData]
	@UserID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		UserID
		,[Title]
		,[FirstName]
		,[MiddleName]
		,[LastName]
		,[Gender]
		,[PreferredName]
		,[DateOfBirth]
		,[MaritalStatus]
		,[MobilePhone]
		,[HomePhone]
		,[WorkPhone]
		,[Fax]
		,[WorkEmail]
		,[HomeEmail]
		,[Active]
		,[CompanyName]
		,[Position]
		,[ABN]
		,[ACN]
		,[AddressLine1]
		,[AddressLine2]
		,[City]
		,[State]
		,[Postcode]
		,[Country]
		,[PostalAddressLine1]
		,[PostalAddressLine2]
		,[PostalCity]
		,[PostalState]
		,[PostalPostcode]
		,[PostalCountry]

	FROM dbo.UserData
	WHERE UserID = @UserID
END


GO
/****** Object:  StoredProcedure [dbo].[GetUserID]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 20/02/2013
-- Description:	Attempts to get the UserID based on email and password
-- =============================================
CREATE PROCEDURE [dbo].[GetUserID] 
	-- Add the parameters for the stored procedure here
	@Email varchar(100),
	@Password varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @UserCorrect int

	
	SELECT @UserCorrect = COUNT(UserID)
	FROM dbo.Users
	WHERE Email=@Email AND Password=@Password

	IF @UserCorrect = 0 
	BEGIN
		PRINT 'Incorrect User Name Or Password'
		SELECT
			NULL [UserID],
			'Incorrect User Name Or Password'[UserType]
	END
	ELSE
	BEGIN
		SELECT 
			UserID,
			UserType
		FROM dbo.Users
		WHERE Email=@Email AND Password = @Password
	END

END


GO
/****** Object:  StoredProcedure [dbo].[UpdateAdviser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Updates an Adviser table entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateAdviser] 
	-- Add the parameters for the stored procedure here
	@AdviserID nvarchar(128),
	@Overview varchar(max) = NULL, 
	@Photo varchar(5) = NULL, 
	@ExperienceStart date = NULL, 
	@RepresentativeNo varchar(50) = NULL,
	@AFSL varchar(50)= NULL,
	@DealerGroupID nvarchar(128) = NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	BEGIN TRAN

	UPDATE dbo.Advisers SET
		Overview = @Overview
		,Photo = @Photo
		,ExperienceStart = @ExperienceStart
		,RepresentativeNo = @RepresentativeNo
		,AFSL = @AFSL
		,DealerGroupID = @DealerGroupID
		,DateModified = GetDate()

	WHERE AdviserID = @AdviserID

	SELECT @Error = @@ERROR
	
	IF @Error = 0
	BEGIN 
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[UpdateAdviserCapabilities]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 20/03/2014
-- Description:	Updates the adviser capability link profile
-- =============================================
CREATE PROCEDURE [dbo].[UpdateAdviserCapabilities] 
	-- Add the parameters for the stored procedure here
	@AdviserID nvarchar(128) , 
	@CapabilitySelection varchar(max) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	-- Need to parse the products selected, as it is a variable list, we need to create a temp table so that they can be matched to the profiles
	DECLARE @tempTable TABLE
	(
		ElementValue VARCHAR(1000),
		RowNum INT,
		ColNum INT
	)

	INSERT INTO @tempTable (ElementValue, RowNum, ColNum) SELECT ElementValue, RowNum, ColNum FROM dbo.CSVIntoTable(',','|', @CapabilitySelection)
	
	-- put the converted data into a profile table to update with
	DECLARE @profileTable TABLE
	(
		CapabilityID INT ,
		Capable BIT 
	)

	INSERT INTO @profileTable (CapabilityID, Capable) 
	SELECT 
		CONVERT(INT, D1.ElementValue) CapabilityID, 
		CONVERT(BIT, D2.ElementValue) Capabile

	FROM @tempTable D1 INNER JOIN @tempTable D2 ON D1.RowNum = D2.RowNum WHERE D1.ColNum = 1 AND D2.ColNum = 2

	BEGIN TRAN
	UPDATE dbo.AdviserCapabilityLinks 
	SET
		Capable = P.Capable 
	FROM dbo.AdviserCapabilityLinks AC 
	INNER JOIN @profileTable P ON AC.CapabilityTypeID = P.CapabilityID 
	WHERE AC.AdviserID = @AdviserID AND AC.CapabilityTypeID = P.CapabilityID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END
	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateAppointment]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 09/04/2014
-- Description:	Updates an appointment entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateAppointment] 
	@AppointmentID	nvarchar(128) ,
	@Title				varchar(100),
	@Details			varchar(max),
	@AppointmentTime	datetime,
	@AppointmentType	varchar(50) = NULL, 
	@DurationHours		decimal(9,2),
	@Comments			varchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @TranStarted bit 
	DECLARE @ErrorCode   int
		
	IF (@@TRANCOUNT = 0)
	BEGIN
		BEGIN TRANSACTION 
		SET @TranStarted = 1
	END
	ELSE
	BEGIN
		SET @TranStarted = 0 
	END

	UPDATE dbo.Appointments SET
		Title = @Title
		,Details = @Details
		,AppointmentType = @AppointmentType
		,AppointmentTime = @AppointmentTime
		,DurationHours = @DurationHours
		,Comments = @Comments
		,DateModified = GetDate()

	WHERE AppointmentID = @AppointmentID 

	SET @ErrorCode = @@ERROR 

	IF @ErrorCode <> 0 
	BEGIN
		GOTO Cleanup 
	END
	IF (@TranStarted = 1)
	BEGIN
		SET @TranStarted = 0
		COMMIT TRANSACTION
	END
	RETURN @ErrorCode

Cleanup:
	IF (@TranStarted = 1)
	BEGIN
		SET @TranStarted = 0 
		ROLLBACK TRANSACTION 
	END

	RETURN @ErrorCode
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateBeneficiary]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 26/03/2014
-- Description:	Updates a beneficiary entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateBeneficiary] 
	@BeneficiaryID nvarchar(128),
	@FirstName varchar(200), 
	@LastName varchar(100), 
	@Relationship varchar(100),
	@Address1 varchar(200) = NULL,
	@Address2 varchar(200) = NULL,
	@Address3 varchar(200) = NULL,
	@City varchar(50) = NULL,
	@State varchar(50) = NULL,
	@PostCode varchar(10) = NULL,
	@Phone varchar(50) = NULL, 
	@Mobile varchar(50) = NULL,
	@Email varchar(100) = NULL, 
	@BeneficiaryPercentage float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

    BEGIN TRAN

	UPDATE dbo.Beneficiaries SET
		FirstName = @FirstName
		,LastName = @LastName
		,Relationship = @Relationship
		,Address1 = @Address1
		,Address2 = @Address2
		,Address3 = @Address3
		,City = @City
		,State = @State
		,PostCode = @PostCode
		,Phone = @Phone
		,Mobile = @Mobile
		,Email = @Email
		,BeneficiaryPercentage = @BeneficiaryPercentage
		,DateModified = GetDate()

	SELECT @Error = @@ERROR 

	IF @Error = 0
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateBookmark]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 03/04/2014
-- Description:	Updates a bookmark entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateBookmark] 
	@BookmarkID nvarchar(128),
	@BookmarkName varchar(100),
	@BookmarkLink varchar(100),
	@Comments varchar(500)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	BEGIN TRAN

	UPDATE dbo.Bookmarks SET
		BookmarkName = @BookmarkName
		,BookmarkLink = @BookmarkLink
		,Comments = @Comments 
		,DateModified = GetDate()

	WHERE BookmarkID = @BookmarkID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateCashHolding]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Updates an existing record of cash holding
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCashHolding] 
	-- Add the parameters for the stored procedure here
	@CashHoldingsID nvarchar(128),
	@InterestRate decimal(9,2) = NULL,
	@Frequency decimal(9,2) = NULL,
	@NextPayment date = NULL,
	@DebitInterest decimal(9,2) = NULL,
	@DateOpened date,
	@StatementMethod varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	BEGIN TRAN
	UPDATE dbo.CashHoldings
	SET
		InterestRate = @InterestRate
		,Frequency = @Frequency
		,NextPayment = @NextPayment
		,DebitInterest = @DebitInterest
		,DateOpened = @DateOpened
		,StatementMethod = @StatementMethod
		,DateModified = GetDate()

	WHERE CashHoldingsID = @CashHoldingsID

	SELECT @Error = @@ERROR 
	
	-- Check for errors 
	IF @Error = 0 
	BEGIN	
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateClient]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Updates a client profile
-- =============================================
CREATE PROCEDURE [dbo].[UpdateClient] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128),
	@ClientGroupID nvarchar(128),
	@LastContact date = NULL,
	@LastReview date = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	BEGIN TRAN

	UPDATE dbo.Clients SET
		ClientGroupID = @ClientGroupID
		,LastContact = @LastContact
		,LastReview = @LastReview
		,DateModified = GetDate()

	WHERE ClientID = @ClientID

	SELECT @Error = @@ERROR

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
	 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateClientFileInformation]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 01 May 2014
-- Description:	Updates an existing client file information record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateClientFileInformation] 
	@ClientFileID nvarchar(128) = NULL ,
	@Response varchar(100),
	@ResponseDate DateTime = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @TransStarted BIT 
	DECLARE @ErrorCode INT 

	IF @@TRANCOUNT = 0 
	BEGIN 
		SET @TransStarted = 1
		BEGIN TRANSACTION
	END

	-- run the insert statement 
	UPDATE dbo.ClientFileInformations
	SET
		Response = @Response
		,ResponseDate = @ResponseDate
		,DateModified = GetDate()
	WHERE ClientFileID = @ClientFileID 

	SET @ErrorCode = @@ERROR 

	-- Error checking and finalize transaction 
	IF @ErrorCode <> 0
	BEGIN
		GOTO Cleanup
	END

	IF @TransStarted = 1
	BEGIN
		COMMIT TRANSACTION 
	END

	RETURN @ErrorCode 
Cleanup:
	IF @TransStarted = 1
	BEGIN 
		ROLLBACK TRANSACTION 
		SET @TransStarted = 0 
	END

	RETURN @ErrorCode 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateClientGroup]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06 May 2014
-- Description:	Updates a ClientGroup entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateClientGroup]
	@ClientGroupID		nvarchar(128),
	@AccountName		varchar(100) = NULL,
	@AdviserID			nvarchar(128), 
	@Alias				varchar(100) = NULL,
	@RiskProfileOutcome int			 = NULL,
	@ServiceLevelID		int			 = NULL,
	@MainClientID		nvarchar(128) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error			int
	DECLARE @TransStarted	bit 

	IF @@TRANCOUNT = 0 
	BEGIN
		SET @TransStarted = 1
		BEGIN TRANSACTION
	END

	-- Check if @Alias is NULL, if so, then use existing value
	IF @Alias = NULL
	BEGIN
		SELECT @Alias = Alias FROM dbo.ClientGroups WHERE ClientGroupID = @ClientGroupID 
	END


	UPDATE dbo.ClientGroups SET
		AccountName = @AccountName
		,AdviserID = @AdviserID
		,Alias = @Alias
		,RiskProfileOutcome = @RiskProfileOutcome
		,ServiceLevelID = @ServiceLevelID
		,MainClientID = @MainClientID 
		,DateModified = GetDate()

	WHERE ClientGroupID = @ClientGroupID 

	SELECT @Error = @@ERROR 

	-- Check for Errors 
	IF @Error <> 0 
	BEGIN
		GOTO Cleanup
	END
	
	-- Finalize the transaction 
	IF @TransStarted = 1
	BEGIN
		COMMIT TRANSACTION
		SET @TransStarted = 0 
	END

	RETURN 0 

Cleanup:
	IF @TransStarted = 1 
	BEGIN
		ROLLBACK TRANSACTION
		SET @TransStarted = 0 
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateComplianceIncomeExpense]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 30 Apr 2014
-- Description:	Updates a complianceincomeexpense record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateComplianceIncomeExpense]
	@ComplianceIncomeExpenseID nvarchar(128),
	@Centrelink				decimal(9,2) = NULL,
	@ReceivedMaintenance	decimal(9,2) = NULL,
	@SuperannuationPension	decimal(9,2) = NULL,
	@OtherTaxableIncome		decimal(9,2) = NULL,
	@OtherForeignIncome		decimal(9,2) = NULL,
	@NonTaxableIncome		decimal(9,2) = NULL,
	@FoodExpenses			decimal(9,2) = NULL,
	@ClothingExpenses		decimal(9,2) = NULL,
	@MedicalExpenses		decimal(9,2) = NULL,
	@UtilitiesBills			decimal(9,2) = NULL,
	@CommunicationBills		decimal(9,2) = NULL,
	@Furniture				decimal(9,2) = NULL,
	@MortgageRental			decimal(9,2) = NULL,
	@HomeInsurance			decimal(9,2) = NULL,
	@VehicleInsurance		decimal(9,2) = NULL,
	@Repairs				decimal(9,2) = NULL,
	@CouncilRates			decimal(9,2) = NULL,
	@VehicleRegistration	decimal(9,2) = NULL,
	@Petrol					decimal(9,2) = NULL,
	@VehicleLoans			decimal(9,2) = NULL,
	@Entertainment			decimal(9,2) = NULL,
	@HolidayTravel			decimal(9,2) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ErrorCode INT 
	DECLARE @TransStarted BIT 

	IF (@@TRANCOUNT = 0)
	BEGIN
		BEGIN TRANSACTION
		SET @TransStarted = 1 
	END
	ELSE
	BEGIN
		SET @TransStarted = 0 
	END

	UPDATE dbo.ComplianceIncomeExpenses
	SET
		Centrelink = @Centrelink
		,ReceivedMaintenance = @ReceivedMaintenance
		,SuperannunationPension = @SuperannuationPension
		,OtherTaxableIncome = @OtherTaxableIncome
		,OtherForeignIncome = @OtherForeignIncome
		,NonTaxableIncome = @NonTaxableIncome
		,FoodExpenses = @FoodExpenses
		,ClothingExpenses = @ClothingExpenses
		,MedicalExpenses = @MedicalExpenses
		,UtilitiesBills = @UtilitiesBills
		,CommunicationsBills = @CommunicationBills
		,Furniture = @Furniture
		,MortgageRental = @MortgageRental
		,HomeInsurance = @HomeInsurance
		,VehicleInsurance = @VehicleInsurance
		,Repairs = @Repairs
		,CouncilRates = @CouncilRates
		,VehicleRegistration = @VehicleRegistration
		,Petrol = @Petrol
		,VehicleLoans = @VehicleLoans
		,Entertainment = @Entertainment
		,HolidayTravel = @HolidayTravel
		,DateModified = GetDate()

	WHERE ComplianceIncomeExpensesID = @ComplianceIncomeExpenseID 

	-- Check for Errors 
	SET @ErrorCode = @@ERROR 
	IF @ErrorCode <> 0 
	BEGIN
		GOTO Cleanup 
	END

	-- Finalize transaction 
	IF @TransStarted = 1 
	BEGIN
		SET @TransStarted = 0 
		COMMIT TRANSACTION 
	END

	RETURN 0 
Cleanup:
	
	IF @TransStarted = 1
	BEGIN
		ROLLBACK TRANSACTION
	END

	RETURN @ErrorCode
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateDealerGroup]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06 May 2014
-- Description:	Updates an existing Dealer Group entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateDealerGroup]
	@DealerGroupID nvarchar(128),
	@DealerGroupName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error			INT 
	DECLARE @TransStarted	BIT

	IF @@TRANCOUNT = 0 
	BEGIN
		SET @TransStarted = 1
		BEGIN TRANSACTION
	END

	UPDATE dbo.DealerGroups SET
		DealerGroupName = @DealerGroupName 
		,DateModified = GetDate()

	WHERE DealerGroupID = @DealerGroupID 

	SELECT @Error = @@ERROR

	-- Check for Errors
	IF @Error <> 0
	BEGIN
		GOTO Cleanup
	END

	-- Finalize transaction 
	IF @TransStarted = 1
	BEGIN
		COMMIT TRANSACTION
		SET @TransStarted = 0 
	END

	RETURN 0 

Cleanup:
	IF @TransStarted = 1 
	BEGIN
		ROLLBACK TRANSACTION
		SET @TransStarted = 0 
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateDependant]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Updates a dependant entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateDependant] 
	-- Add the parameters for the stored procedure here
	@DependantID nvarchar(128),
	@FullName varchar(100),
	@Relationship varchar(100) = NULL,
	@DateOfBirth date = NULL,
	@FinancialDependant bit,
	@DependantIncome float = NULL,
	@DependantAsset float = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	BEGIN TRAN

	UPDATE dbo.Dependants SET
		FullName = @FullName
		,Relationship = @Relationship
		,DateOfBirth = @DateOfBirth
		,FinancialDependant = @FinancialDependant
		,DependantIncome = @DependantIncome
		,DependantAsset = @DependantAsset 
		,DateModified = GetDate()

	WHERE DependantID = @DependantID

	-- Check for errors
	SELECT @Error = @@ERROR

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END

	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error

END


GO
/****** Object:  StoredProcedure [dbo].[UpdateDirectProperty]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06/03/2014
-- Description:	Updates an existing RealEstate record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateDirectProperty] 
	-- Add the parameters for the stored procedure here
	@DirectPropertyID nvarchar(128), 
	@PurchasedPrice decimal(18,2),
	@StampDuty decimal(18,2),
	@PurchaseMiscFee decimal(18,2),
	@PurchaseLegalFee decimal(18,2),
	@PurchaseBankFee decimal(18,2),
	@PurchasedDate date, 
	@SalePrice decimal(18,2) = NULL,
	@AgentCommission decimal(18,2) = NULL,
	@SaleMiscFee decimal(18,2) = NULL,
	@SaleLegalFee decimal(18,2) = NULL,
	@SaleBankFee decimal(18,2) = NULL, 
	@SaleLoanRepayment decimal(18,2) = NULL, 
	@SaleDate date = NULL, 
	@State varchar(50), 
	@EstimatedValue decimal(18,2), 
	@RentalIncome decimal(18,2), 
	@OwnershipLength int ,
	@Country varchar(50) = NULL, 
	@Region varchar(50) = NULL, 
	@Status varchar(50) = NULL, 
	@OutstandingLoan decimal(18,2) = NULL, 
	@YearsOnLoan decimal(9,2) = NULL, 
	@LoanRateType varchar(50) = NULL, 
	@FixedTermEndDate date = NULL, 
	@FixedRate decimal(9,3) = NULL, 
	@FixedLoanPercent decimal(9,3) = NULL, 
	@InterestRepayment decimal(9,3) = NULL, 
	@VariableRate decimal(9,3) = NULL, 
	@AgentName varchar(100) = NULL, 
	@AgentCompany varchar(100) = NULL, 
	@AgentAddress varchar(200) = NULL, 
	@AgentPhone varchar(50) = NULL, 
	@AgentFax varchar(50) = NULL, 
	@AgentEmail varchar(100) = NULL, 
	@AgentYearFrom date = NULL, 
	@AgentYearTo date = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN

	UPDATE dbo.DirectProperties
	SET 
		PurchasedPrice = @PurchasedPrice
		,StampDuty = @StampDuty
		,PurchaseMiscFee = @PurchaseMiscFee 
		,PurchaseLegalFee = @PurchaseLegalFee 
		,PurchaseBankFee = @PurchaseBankFee 
		,PurchasedDate = @PurchasedDate 
		,SalePrice = @SalePrice 
		,AgentCommission = @AgentCommission 
		,SaleMiscFee = @SaleMiscFee 
		,SaleLegalFee = @SaleLegalFee 
		,SaleBankFee = @SaleBankFee 
		,SaleLoanRepayment = @SaleLoanRepayment 
		,SaleDate = @SaleDate 
		,State = @State 
		,EstimatedValue = @EstimatedValue 
		,RentalIncome = @RentalIncome 
		,OwnershipLength  = @OwnershipLength 
		,Country = @Country 
		,Region = @Region 
		,Status = @Status 
		,OutstandingLoan = @OutstandingLoan 
		,YearsOnLoan = @YearsOnLoan 
		,LoanRateType = @LoanRateType 
		,FixedTermEndDate = @FixedTermEndDate 
		,FixedRate = @FixedRate 
		,FixedLoanPercent = @FixedLoanPercent 
		,InterestRepayment = @InterestRepayment 
		,VariableRate = @VariableRate 
		,AgentName = @AgentName 
		,AgentCompany = @AgentCompany 
		,AgentAddress = @AgentAddress 
		,AgentPhone = @AgentPhone 
		,AgentFax = @AgentFax 
		,AgentEmail = @AgentEmail 
		,AgentYearFrom = @AgentYearFrom 
		,AgentYearTo = @AgentYearTo 
		,DateModified = GetDate()

	WHERE DirectPropertyID = @DirectPropertyID

	SELECT @Error = @@ERROR 

	-- Check for errors 
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAn
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[UpdateEducationRecord]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 22/02/2014
-- Description:	Updates an education record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEducationRecord] 
	-- Add the parameters for the stored procedure here
	@EducationRecordID nvarchar(128),
	@EducationLevel varchar(50) = NULL,
	@YearStarted int = NULL,
	@YearCompleted int = NULL,
	@YearsSinceCompletion int = NULL,
	@EducationInstitution varchar(100) = NULL,
	@CourseAttended varchar(100) = NULL,
	@Description varchar(max) = NULL,
	@Comments varchar(max) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN

	UPDATE dbo.EducationRecords SET
		EducationLevel = @EducationLevel
		,YearStarted = @YearStarted
		,YearCompleted = @YearCompleted
		,YearsSinceCompletion = @YearsSinceCompletion
		,EducationInstitution = @EducationInstitution
		,CourseAttended = @CourseAttended
		,Description = @Description
		,Comments = @Comments
		,DateModified = GetDate()

	WHERE EducationRecordID = @EducationRecordID 

	SELECT @Error = @@ERROR

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[UpdateEmploymentRecord]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/02/2014
-- Description:	Updates an employment record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEmploymentRecord] 
	-- Add the parameters for the stored procedure here
	@EmploymentRecordID nvarchar(128),
	@Status varchar(50) = NULL,
	@EmploymentType varchar(50) = NULL,
	@EmployerName varchar(100), 
	@Position varchar(50), 
	@StartDate date,
	@EndDate date = NULL,
	@HoursPerWeek decimal(9,2) = NULL, 
	@GrossSalary float = NULL,
	@Commissions float = NULL,
	@AfterTaxSalary float = NULL, 
	@SuperContribution float = NULL,
	@AdditionalSuperContribution float = NULL,
	@MiscContribution float = NULL, 
	@FBT float = NULL,
	@EmployeeSharePlan float = NULL, 
	@SickLeave decimal(9,2) = NULL, 
	@AnnualLeave decimal(9,2) = NULL, 
	@LongServiceLeave decimal(9,2) = NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN
	
	UPDATE dbo.EmploymentRecords SET
		Status = @Status
		,EmploymentType = @EmploymentType
		,EmployerName = @EmployerName
		,Position = @Position
		,StartDate = @StartDate
		,EndDate = @EndDate
		,HoursPerWeek = @HoursPerWeek
		,GrossSalary = @GrossSalary
		,Commissions = @Commissions
		,AfterTaxSalary = @AfterTaxSalary
		,SuperContribution = @SuperContribution
		,AdditionalSuperContribution = @AdditionalSuperContribution
		,MiscContribution = @MiscContribution
		,FBT = @FBT
		,EmployeeSharePlan = @EmployeeSharePlan
		,SickLeave = @SickLeave
		,AnnualLeave = @AnnualLeave
		,LongServiceLeave = @LongServiceLeave
		,DateModified = GetDate()

	WHERE EmploymentRecordID =@EmploymentRecordID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN	
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateFixedIncome]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Updates an existing Fixed Income holdings record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateFixedIncome] 
	-- Add the parameters for the stored procedure here
	@FixedIncomeID nvarchar(128),
	@PurchaseDate date,
	@IssuerName varchar(100),
	@Ticker varchar(50),
	@InterestPaid decimal(18,2) = NULL,
	@MaturityValue decimal(18,2) = NULL,
	@FixedTerm int,
	@BondPrice decimal(18,2) = NULL,
	@TransactionFee decimal(18,2) = NULL,
	@MinimumFee decimal(18,2) = NULL,
	@MaximumFee decimal(18,2) = NULL,
	@AccountNo varchar(50) = NULL,
	@AccountUserID varchar(50) = NULL,
	@ClearingSponsor varchar(50) = NULL,
	@HIN varchar(50) = NULL,
	@NextPaymentDate date = NULL,
	@NextPaymentAmount decimal(18, 2) = NULL 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN
	
	UPDATE dbo.FixedIncomeHoldings
	SET
		PurchaseDate = @PurchaseDate
		,IssuerName = @IssuerName
		,Ticker = @Ticker
		,InterestPaid = @InterestPaid
		,MaturityValue = @MaturityValue
		,FixedTerm = @FixedTerm
		,BondPrice = @BondPrice
		,TransactionFee = @TransactionFee
		,MinimumFee = @MinimumFee
		,MaximumFee = @MaximumFee
		,AccountNo = @AccountNo
		,AccountUserID = @AccountUserID
		,ClearingSponsor = @ClearingSponsor
		,HIN = @HIN
		,NextPaymentDate = @NextPaymentDate
		,NextPaymentAmount = @NextPaymentAmount 
		,DateModified = GetDate()

	WHERE FixedIncomeID = @FixedIncomeID

	-- Check for Errors 
	SELECT @Error = @@ERROR 

	IF @Error = 0
	BEGIN
		COMMIT TRAN	
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateIPO]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 27/03/2014
-- Description:	Updastes an IPO entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateIPO] 
	@IPOID nvarchar(128) ,
	@IssuerName varchar(100) = NULL, 
	@InstrumentCode varchar(100) = NULL,
	@OfferSize int = NULL, 
	@PricePerUnit decimal (18,2) = NULL, 
	@Increments int = NULL, 
	@InvestmentMin int = NULL, 
	@InvestmentMax int = NULL, 
	@ForecastRatio decimal(18,2) = NULL, 
	@DistributionFreq int = NULL, 
	@ForecastDistribution decimal(18,2) = NULL, 
	@ForecastDividend decimal(18,2) = NULL, 
	@ForecastFranking decimal(18,2) = NULL, 
	@OfferType varchar(50) = NULL, 
	@LodgementDate date = NULL, 
	@BookbuildDate date = NULL, 
	@OpeningDate date = NULL, 
	@GeneralClosingDate date = NULL, 
	@BrokerClosingDate date = NULL, 
	@IssueDate date = NULL, 
	@SettlementTradeDate date = NULL, 
	@HoldingDespatchDate date = NULL, 
	@NormalTradeDate  date = NULL, 
	@RecordFirstPayDate date = NULL, 
	@FirstInterestDate date = NULL, 
	@FirstRedemptionDate date = NULL, 
	@MaturityDate date = NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 
	BEGIN TRAN
	UPDATE dbo.IPOs SET
		IssuerName = @IssuerName
		,InstrumentCode = @InstrumentCode
		,OfferSize = @OfferSize
		,PricePerUnit = @PricePerUnit
		,Increments = @Increments
		,InvestmentMin = @InvestmentMin
		,InvestmentMax = @InvestmentMax
		,ForecastRatio = @ForecastRatio
		,DistributionFreq = @DistributionFreq
		,ForecastDistribution = @ForecastDistribution
		,ForecastDividend = @ForecastDividend
		,ForecastFranking = @ForecastFranking
		,OfferType = @OfferType
		,LodgementDate = @LodgementDate
		,BookbuildDate = @BookbuildDate
		,OpeningDate = @OpeningDate
		,GeneralClosingDate = @GeneralClosingDate
		,BrokerClosingDate = @BrokerClosingDate
		,IssueDate = @IssueDate
		,SettlementTradeDate = @SettlementTradeDate
		,HoldingDespatchDate = @HoldingDespatchDate
		,NormalTradeDate = @NormalTradeDate
		,RecordFirstPayDate = @RecordFirstPayDate
		,FirstInterestDate = @FirstInterestDate
		,FirstRedemptionDate = @FirstRedemptionDate
		,MaturityDate = @MaturityDate
		,DateModified = GetDate()

	WHERE IPOID = @IPOID

	SELECT @Error = @@ERROR 

	IF @Error = 0
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateLoan]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Updates an existing Loan record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateLoan] 
	@LoanID nvarchar(128),
	@Description varchar(100),
	@LoanType varchar(100),
	@DataFeed varchar(50),
	@InterestRate decimal(9,2), 
	@PaymentFrequency int, 
	@PaymentAmount decimal(9,2),
	@LoanTerm int,
	@CreditLimt int, 
	@AccountBalance decimal(9,2),
	@LoanStart date, 
	@AccountNo varchar(50),
	@LoanProvider varchar(100),
	@LastPaymentDate date, 
	@FinancialInstitution varchar(50),
	@RelationshipManager varchar(100),
	@AddressLine1 varchar(100),
	@AddressLine2 varchar(100),
	@City varchar(50), 
	@State varchar(50),
	@Postcode varchar(10),
	@RealEstateAddress varchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN
	
	UPDATE dbo.Loans
	SET
		Description = @Description 
		,LoanType = @LoanType 
		,DataFeed = @DataFeed 
		,InterestRate = @InterestRate 
		,PaymentFrequency = @PaymentFrequency 
		,PaymentAmount = @PaymentAmount 
		,LoanTerm = @LoanTerm 
		,CreditLimit = @CreditLimt 
		,AccountBalance = @AccountBalance 
		,LoanStart = @LoanStart 
		,AccountNo = @AccountNo 
		,LoanProvider = @LoanProvider 
		,LastPaymentDate = @LastPaymentDate 
		,FinancialInstitution = @FinancialInstitution 
		,RelationshipManager = @RelationshipManager 
		,AddressLine1 = @AddressLine1 
		,AddressLine2 = @AddressLine2 
		,City = @City 
		,State = @State 
		,Postcode = @Postcode 
		,RealEstateAddress = @RealEstateAddress 
		,DateModified = GetDate() 

	WHERE LoanID = @LoanID

	-- check for errors 
	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 

END


GO
/****** Object:  StoredProcedure [dbo].[UpdateMarginLoan]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 07/03/2014
-- Description:	Updates an existing marginLoan record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateMarginLoan] 
	-- Add the parameters for the stored procedure here
	@MarginLoanID nvarchar(128)
    ,@Description varchar(100)
    ,@HIN varchar(50)
    ,@LoanProvider varchar(50)
    ,@VariableRateLoan varchar(50)
    ,@VariableRateAmount decimal(9,2)
    ,@FixedRateLoan varchar(50)
    ,@FixedRateAmount decimal(9,2)
    ,@UnsettledTransactions decimal(9,2)
    ,@TotalBalance decimal(9,2)
    ,@NetInterestYTD decimal(9,2)
    ,@MonthlyInterest decimal(9,2)
    ,@MinBrokerage decimal(9,2)
    ,@MaxBrokerage decimal(9,2)
    ,@TotalBrokerage decimal(9,2)
    ,@TotalGST decimal(9,2)
    ,@ManagementFee decimal(9,2)
    ,@AdvisersComission decimal(9,2)
    ,@LoanLimit decimal(9,2)
    ,@SpendingLimit decimal(9,2)
    ,@AvailableCash decimal(9,2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN

	UPDATE dbo.MarginLoans
	SET 
		Description = @Description
		,HIN = @HIN
		,LoanProvider = @LoanProvider
		,VariableRateLoan = @VariableRateLoan
		,VariableRateAmount = @VariableRateAmount
		,FixedRateLoan = @FixedRateLoan
		,FixedRateAmount = @FixedRateAmount
		,UnsettledTransactions = @UnsettledTransactions
		,TotalBalance = @TotalBalance
		,NetInterestYTD = @NetInterestYTD
		,MonthlyInterest = @MonthlyInterest
		,MinBrokerage = @MinBrokerage
		,MaxBrokerage = @MaxBrokerage
		,TotalBrokerage = @TotalBrokerage
		,TotalGST = @TotalGST
		,AdvisersComission = @AdvisersComission
		,LoanLimit = @LoanLimit
		,SpendingLimit = @SpendingLimit
		,AvailableCash = @AvailableCash
		,DateModified = GetDate()
	
	WHERE MarginLoanID = @MarginLoanID

	SELECT @Error = @@ERROR 

	-- Check for error 
	IF @Error = 0 
	BEGIN 
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateNote]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 18/03/2014
-- Description:	Updates details on a particular note
-- =============================================
CREATE PROCEDURE [dbo].[UpdateNote] 
	-- Add the parameters for the stored procedure here
	@NoteID nvarchar(128),
	@AssetClass varchar(100) = NULL,
	@ProductClass varchar(100) = NULL,
	@Product varchar(100) = NULL,
	@Purpose varchar(100) = NULL,
	@TimeSpent float = NULL, 
	@Subject varchar(200) , 
	@Body varchar(max) = NULL, 
	@FollowupActions varchar(100) = NULL,  
	@DateCompleted datetime = NULL, 
	@Reminder bit = NULL, 
	@ReminderDate datetime = NULL, 
	@IsAccepted bit = NULL, 
	@IsDeclined bit = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 
	
	BEGIN TRAN
	UPDATE dbo.Notes SET
		AssetClass = @AssetClass
		,ProductClass = @ProductClass
		,Product = @Product
		,Purpose = @Purpose
		,TimeSpent = @TimeSpent
		,Subject = @Subject
		,Body = @Body
		,FollowupActions = @FollowupActions
		,DateCompleted = @DateCompleted
		,Reminder = @Reminder
		,ReminderDate = @ReminderDate
		,IsAccepted = @IsAccepted
		,IsDeclined = @IsDeclined
		,DateModified = GetDate()
	WHERE NoteID = @NoteID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateNoteAttachment]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 18/03/2014
-- Description:	Updates details on a note attachment
-- =============================================
CREATE PROCEDURE [dbo].[UpdateNoteAttachment] 
	-- Add the parameters for the stored procedure here
	@AttachmentID nvarchar(128),
	@Comments varchar(200), 
	@Title varchar(200),
	@Path varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN
	UPDATE dbo.Attachments SET 
		Title = @Title
		,Path = @Path
		,Comments = @Comments
		,DateModified = GetDate()

	WHERE AttachmentID = @AttachmentID

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
		
END


GO
/****** Object:  StoredProcedure [dbo].[UpdatePolicy]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/03/2014
-- Description:	Updates an insurance policy
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePolicy] 
	@PolicyID nvarchar(128),
	@InsuranceType varchar(100),
	@PolicyType varchar(100),
	@PolicyName varchar(100),
	@PolicyNumber varchar(50) = NULL, 
	@AccountName varchar(100),
	@AccountAddress1 varchar(200) = NULL, 
	@AccountAddress2 varchar(200) = NULL,
	@AccountAddress3 varchar(200) = NULL,
	@AccountCity varchar(100) = NULL,
	@AccountState varchar(100) = NULL,
	@AccountPostCode varchar(100) = NULL,
	@InceptionDate date = NULL,
	@LastRenewalDate date = NULL, 
	@StartDate date = NULL,
	@EndDate date = NULL, 
	@Commentary varchar(max) = NULL,
	@RenewalDueDate date = NULL, 
	@Institution varchar(100) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	BEGIN TRAN

	UPDATE dbo.Policies  SET
		InsuranceType = @InsuranceType
		,PolicyType = @PolicyType
		,PolicyName = @PolicyName
		,PolicyNumber = @PolicyNumber
		,AccountName = @AccountName
		,AccountAddress1 = @AccountAddress1
		,AccountAddress2 = @AccountAddress2
		,AccountAddress3 = @AccountAddress3
		,AccountCity = @AccountCity
		,AccountState = @AccountState 
		,AccountPostCode = @AccountPostCode
		,InceptionDate = @InceptionDate
		,LastRenewalDate = @LastRenewalDate
		,StartDate = @StartDate
		,EndDate = @EndDate
		,Commentary = @Commentary
		,Institution = @Institution
		,RenewalDueDate = @RenewalDueDate
		,DateModified = GetDate() 

	WHERE PolicyID = @PolicyID
	
	-- check the error code 
	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END
	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdatePolicyDetail]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 24/03/2014
-- Description:	Updates a policy detail
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePolicyDetail] 
	-- Add the parameters for the stored procedure here
	@PolicyDetailID nvarchar(128),
	@DetailType varchar(100),
	@DetailName varchar(100) = NULL,
	@Description varchar(100) = NULL, 
	@Amount decimal(9,2) = NULL, 
	@Startdate date = NULL,
	@EndDate date = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN

	UPDATE dbo.PolicyDetails SET
		DetailType = @DetailType
		,DetailName = @DetailName
		,Description = @Description
		,Amount = @Amount
		,StartDate = @StartDate
		,EndDate = @EndDate 
		,DateModified = GetDate()

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateQualification]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 25/03/2014
-- Description:	Updates a qualification entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateQualification] 
	@QualificationID nvarchar(128),
	@Qualification varchar(200),
	@QualificationIndex int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN

	UPDATE dbo.Qualifications SET
		Qualification = @Qualification
		,QualificationIndex = @QualificationIndex

	WHERE QualificationID = @QualificationID 

	SELECT @Error = @@ERROR 

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateRiskProfile]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 05/03/2014
-- Description:	Updates an existing risk profile
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRiskProfile] 
	-- Add the parameters for the stored procedure here
	@ClientID nvarchar(128),
	@ShortTermGoal1 varchar(200) = NULL, 
	@ShortTermGoal2 varchar(200) = NULL,
	@ShortTermGoal3 varchar(200) = NULL, 
	@MedTermGoal1 varchar(200) = NULL, 
	@MedTermGoal2 varchar(200) = NULL,
	@MedTermGoal3 varchar(200) = NULL, 
	@LongTermGoal1 varchar(200) = NULL, 
	@LongTermGoal2 varchar(200) = NULL, 
	@LongTermGoal3 varchar(200) = NULL, 
	@Comments varchar(max) = NULL, 
	@RetirementAge int = NULL, 
	@RetirementIncome float = NULL, 
	@IncomeSource varchar(100) = NULL, 
	@InvestmentObjective1 varchar(200) = NULL, 
	@InvestmentObjective2 varchar(200) = NULL, 
	@InvestmentObjective3 varchar(200) = NULL, 
	@InvestmentTimeHorizon int = NULL, 
	@InvestmentKnowledge varchar(200) = NULL, 
	@RiskAttitude varchar(200) = NULL, 
	@CapitalLossAttitude varchar(200) = NULL, 
	@ShortTermTrading varchar(50) = NULL, 
	@ShortTermAssetPercent float = NULL, 
	@ShortTermEquityPercent float = NULL, 
	@InvestmentProfile varchar(100) = NULL,
	@ProductsSelection varchar(max) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Parse the RiskProfile Products
	-- Need to parse the products selected, as it is a variable list, we need to create a temp table so that they can be matched to the profiles
	DECLARE @tempTable TABLE
	(
		ElementValue VARCHAR(1000),
		RowNum INT,
		ColNum INT
	)

	INSERT INTO @tempTable (ElementValue, RowNum, ColNum) SELECT ElementValue, RowNum, ColNum FROM dbo.CSVIntoTable(',','|', @ProductsSelection)
	
	DECLARE @profileData TABLE 
	(
		ProductTypeID int, 
		Selected bit
	)

	INSERT INTO @profileData (ProductTypeID, Selected) 
	SELECT 
		CONVERT(int, D1.ElementValue),
		CONVERT(bit, D2.ElementValue)
	FROM @tempTable D1 INNER JOIN @tempTable D2 ON D1.RowNum = D2.RowNum WHERE D1.ColNum = 1 AND D2.ColNum = 2

	DECLARE @RiskProfileID nvarchar(128)

	DECLARE @Error INT 
	DECLARE @TransCount bit 

	IF @@TRANCOUNT = 0 
	BEGIN
		BEGIN TRANSACTION
		SET @TransCount = 1
	END

	-- Update the risk profile first
	UPDATE dbo.RiskProfiles 
	SET 
		ShortTermGoal1 = @ShortTermGoal1
		,ShortTermGoal2 = @ShortTermGoal2
		,ShortTermGoal3 = @ShortTermGoal3
		,MedTermGoal1 = @MedTermGoal1
		,MedTermGoal2 = @MedTermGoal2
		,MedTermGoal3 = @MedTermGoal3
		,LongTermGoal1 = @LongTermGoal1
		,LongTermGoal2 = @LongTermGoal2
		,Comments = @Comments
		,RetirementAge = @RetirementAge
		,RetirementIncome = @RetirementIncome
		,IncomeSource = @IncomeSource
		,InvestmentObjective1 = @InvestmentObjective1
		,InvestmentObjective2 = @InvestmentObjective2
		,InvestmentObjective3 = @InvestmentObjective3
		,InvestmentTimeHorizon = @InvestmentTimeHorizon
		,InvestmentKnowledge = @InvestmentKnowledge
		,RiskAttitude = @RiskAttitude
		,CapitalLossAttitude = @CapitalLossAttitude
		,ShortTermTrading = @ShortTermTrading
		,ShortTermAssetPercent = @ShortTermAssetPercent
		,ShortTermEquityPercent = @ShortTermEquityPercent
		,InvestmentProfile = @InvestmentProfile
		,DateModified = GetDate()

	WHERE ClientID = @ClientID

	SELECT @Error = @@ERROR 
	
	IF @Error <> 0 
	BEGIN
		GOTO Cleanup
	END
	
	SELECT @RiskProfileID = RiskProfileID FROM dbo.RisekProfiles WHERE ClientID = @ClientID
	
	IF @RiskProfileID = NULL 
	BEGIN 
		SELECT @Error = -1 
		GOTO Cleanup
	END 
		 
	-- update the risk profile products 
	UPDATE dbo.ProfileProductLinks 
	SET Selected = P.Selected

	FROM @profileData P , dbo.ProfileProductLinks PL 
	WHERE PL.ProductTypeID = P.ProductTypeID AND PL.RiskProfileID = @RiskProfileID

	SELECT @Error = @@ERROR 

	-- Check for Error s
	IF @Error <> 0
	BEGIN
		GOTO Cleanup
	END

	-- Finalize the transaction 
	IF @TransCount = 1
	BEGIN
		COMMIT TRANSACTION 
		SET @TransCount = 0 
	END

	RETURN 0 

Cleanup:
	IF @TransCount = 1 
	BEGIN
		ROLLBACK TRANSACTION 
		SET @TransCount = 0
	END
	RETURN @Error 
	    
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateSecurity]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 26/03/2014
-- Description:	Updates a security entry
-- =============================================
CREATE PROCEDURE [dbo].[UpdateSecurity] 
	@SecuritiesID nvarchar(128),
	@PurchaseDate date,
	@Company varchar(50) = NULL,
	@Ticker varchar(50) = NULL, 
	@Units int ,
	@PricePerUnit decimal(9,2) ,
	@Brokerage decimal(9,2),
	@MarketValue decimal(9,2)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Error INT 

	BEGIN TRAN

	UPDATE dbo.Securities SET
		PurchaseDate = @PurchaseDate
		,Company = @Company
		,Ticker = @Ticker 
		,Units = @Units
		,PricePerUnit = @PricePerUnit
		,Brokerage = @Brokerage
		,MarketValue = @MarketValue 
		,DateModified = GetDate()

	WHERE SecuritiesID = @SecuritiesID

	SELECT @Error = @@ERROR 

	IF @Error = 0
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN 
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateStockHoldings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 06/03/2014
-- Description:	Updates an existing StockHoldings record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateStockHoldings] 
	-- Add the parameters for the stored procedure here
	@StockHoldingID nvarchar(128),
	@Units int,
	@LastTransactionID nvarchar(128),
	@HIN varchar(50),
	@ServiceFee decimal(18,2) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT 

	BEGIN TRAN

	UPDATE dbo.StockHoldings SET
		Units = @Units
		,LastTransactionID = @LastTransactionID
		,ServiceFee = @ServiceFee
		,HIN = @HIN
		,DateModified = GetDate()

	WHERE StockHoldingID = @StockHoldingID
	SELECT @Error = @@ERROR

	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateUserData]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 21/02/2014
-- Description:	Updates a User profile
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUserData] 
	@UserID nvarchar(128),
	@Title			varchar(100) = NULL,
	@FirstName		varchar(100),
	@MiddleName		varchar(100) = NULL,
	@LastName		varchar(100),
	@Gender			nvarchar(20) = NULL,
	@PreferredName	varchar(200) = NULL,
	@DateOfBirth	datetime	 = NULL,
	@MaritalStatus	varchar(50)  = NULL,
	@MobilePhone	varchar(20)  = NULL,
	@HomePhone		varchar(20)  = NULL,
	@WorkPhone		varchar(20)  = NULL,
	@Fax			varchar(20)  = NULL,
	@WorkEmail		varchar(100) = NULL,
	@HomeEmail		varchar(100) = NULL,
	@Active			bit			 = NULL,
	@CompanyName	varchar(100) = NULL,
	@Position		varchar(100) = NULL,
	@ABN			varchar(50)  = NULL,
	@ACN			varchar(50)  = NULL,
	@AddressLine1	varchar(100) = NULL,
	@AddressLine2	varchar(100) = NULL,
	@City			varchar(100) = NULL,
	@State			varchar(100) = NULL,
	@Postcode		varchar(100) = NULL,
	@Country		varchar(100) = NULL,
	@PostalAddressLine1 varchar(100) = NULL,
	@PostalAddressLine2 varchar(100) = NULL,
	@PostalCity		varchar(100) = NULL,
	@PostalState	varchar(100) = NULL,
	@PostalPostcode varchar(10)  = NULL,
	@PostalCountry	varchar(100) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Error INT

	-- Check to ensure that we do not duplicate the accountNo in the process
	
	BEGIN TRAN

	UPDATE dbo.UserData 
	SET
		Title=@Title
		,FirstName = @FirstName
		,MiddleName = @MiddleName
		,LastName = @LastName
		,Gender = @Gender
		,PreferredName = @PreferredName
		,DateOfBirth = @DateOfBirth
		,MaritalStatus = @MaritalStatus
		,MobilePhone = @MobilePhone
		,HomePhone = @HomePhone
		,WorkPhone = @WorkPhone
		,Fax = @Fax
		,WorkEmail = @WorkEmail
		,HomeEmail = @HomeEmail
		,Active = @Active
		,CompanyName = @CompanyName
		,Position = @Position
		,ABN = @ABN
		,ACN = @ACN	
		,AddressLine1 = @AddressLine1
		,AddressLine2 = @AddressLine2
		,City = @City
		,State = @State
		,Postcode = @Postcode
		,Country = @Country
		,PostalAddressLine1 = @PostalAddressLine1
		,PostalAddressLine2 = @PostalAddressLine2
		,PostalCity = @PostalCity
		,PostalState = @PostalState
		,PostalPostcode = @PostalPostcode
		,PostalCountry = @PostalCountry
		,DateModified = GetDate()


	WHERE UserID = @UserID

	SELECT @Error = @@ERROR
	
	IF @Error = 0 
	BEGIN
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error

END


GO
/****** Object:  StoredProcedure [dbo].[UpdateUserPassword]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wai Yin Chung
-- Create date: 21/02/2014
-- Description:	Updates a user's password based on their email and current password
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUserPassword]
	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128) = NULL,
	@Email varchar(200) = NULL,
	@CurrentPassword varchar(200),
	@NewPassword varchar(200)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @RecordCount INT 
	
	-- Check that the current record exists either by using the user ID (Preferred) Or the email address
	SELECT @RecordCount = COUNT(UserID) FROM Users 
	WHERE ((UserID = @UserID) OR (@UserID IS NULL AND Email = @Email))

	IF @RecordCount <> 1 
	BEGIN
		PRINT N'Incorrect userID or email'
		RETURN -1
	END
	
	-- Check that the Current password matches the one in the database

	DECLARE @ID nvarchar(128) 
	SELECT @ID = UserID FROM Users
	WHERE (((UserID = @UserID) OR (@UserID IS NULL AND Email = @Email)) AND (Password = @CurrentPassword))

	IF @ID IS NULL
	BEGIN
		PRINT N'Incorrect password'
		RETURN -2 
	END

	DECLARE @Error INT
	BEGIN TRAN

	UPDATE Users SET Password = @NewPassword , DateModified = GETDATE() WHERE UserID = @ID

	-- Check for errors on update

	SELECT @Error = @@ERROR

	IF @Error = 0 
	BEGIN 
		COMMIT TRAN
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
	END

	RETURN @Error 


END


GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_Adviser_Verified_Status_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Admin_Adviser_Verified_Status_Add]
@UserId nvarchar(128),
@VerifiedStatusId int

AS
SET NOCOUNT ON

	insert into dbo.Adviser_Verified_Status 
		select @UserId, @VerifiedStatusId, GETDATE()

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_AdviserRatings_Delete]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_AdviserRatings_Delete]
@Id int
as
SET NOCOUNT ON

	delete from Adviser_Ratings where Id = @Id

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_ClientRatings_Delete]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_ClientRatings_Delete]
@Id int
as
SET NOCOUNT ON

	delete from Client_Ratings where Id = @Id

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetAdviserDetailsForApproval]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Admin_GetAdviserDetailsForApproval]
@IsApproved int
as
SET NOCOUNT ON

	select a.Id, c.*, b.AdviserDescription, b.IsApproved
	from 
	(
		select t1.UserId, max(t1.Id) as Id
			from dbo.Adviser_Description t1
			group by t1.UserId
	) a 
	inner join dbo.Adviser_Description b
	on a.UserId = b.UserId and a.Id = b.Id
	inner join dbo.Adviser_Details c
	on a.UserId = c.UserId
	where b.IsApproved = @IsApproved
	order by b.LastUpdated desc

SET NOCOUNT OFF




GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetAdviserForVerification]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Verify/Not Verified

CREATE procedure [dbo].[usp_Admin_GetAdviserForVerification]
@VerifiedStatusId int
as
SET NOCOUNT ON


	select a.*, b.VerifiedStatus 
	from 
	(

		select t1.*, 
			ISNULL((
				select top 1 VerifiedStatusId from dbo.Adviser_Verified_Status where UserId = t1.UserId order by LastUpdated desc
			),0) as VerifiedStatusId
			from dbo.Adviser_Details t1
	) a
	left join dbo.VerifiedStatus b
	on a.VerifiedStatusId = b.VerifiedStatusId
	where a.VerifiedStatusId = @VerifiedStatusId
	order by a.LastUpdated asc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetAllSubscriptionTransactions]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_GetAllSubscriptionTransactions]
as
SET NOCOUNT ON

	select t2.*, 
		t3.PlanTypeId, 
		(
			select top 1 PlanType from dbo.SubscriptionPlanTypes where PlanTypeId = t3.PlanTypeId
		) as PlanType,
		t3.SubscriptionStatusId,
		(
			select top 1 SubscriptionStatus from dbo.SubscriptionStatus where SubscriptionStatusId = t3.SubscriptionStatusId
		) as  SubscriptionStatus,
		t3.AmountPaid, t3.StartDate, t3.EndDate, t3.TransactionId, t3.TransactionDate
		from dbo.AspNetUserRoles t1
		left join dbo.Adviser_Details t2
		on t1.UserId = t2.UserId
		left join dbo.Adviser_Subscriptions t3
		on t1.UserId = t3.UserId
		where t1.RoleId in ( select Id from AspNetRoles where [Name] in ( 'adviser' ) )
		order by t2.AccountNumber asc, t3.Id desc

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetCurrentSubscriptions]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_GetCurrentSubscriptions]
as
SET NOCOUNT ON


	select t2.*, 
		t3.PlanTypeId, 
		(
			select top 1 PlanType from dbo.SubscriptionPlanTypes where PlanTypeId = t3.PlanTypeId
		) as PlanType,
		t3.SubscriptionStatusId,
		(
			select top 1 SubscriptionStatus from dbo.SubscriptionStatus where SubscriptionStatusId = t3.SubscriptionStatusId
		) as  SubscriptionStatus,
		t3.AmountPaid, t3.StartDate, t3.EndDate, t3.TransactionId, t3.TransactionDate
		from dbo.AspNetUserRoles t1
		left join dbo.Adviser_Details t2
		on t1.UserId = t2.UserId
		left join (

			select b.*
				from (
						select UserId, max(Id) as Id from dbo.Adviser_Subscriptions  group by UserId
					) a 
					inner join dbo.Adviser_Subscriptions b
					on a.Id = b.Id and a.UserId = b.UserId

		) t3
		on t1.UserId = t3.UserId
		where t1.RoleId in ( select Id from AspNetRoles where [Name] in ( 'adviser' ) )

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetLatestAdviserOneOnly]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Admin_GetLatestAdviserOneOnly]
as
SET NOCOUNT ON
-- Get the last signed up Adviser

select top 1 t1.UserId, t1.Title, t1.FirstName, t1.LastName, t1.CurrentTitle, t1.LastUpdated 
	from Adviser_Details t1
	order by t1.LastUpdated desc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetListForDeleteReviews_AdviserRatings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_GetListForDeleteReviews_AdviserRatings]
as
SET NOCOUNT ON

select 
( select top 1 Email from AspNetUsers where Id = t1.UserId ) as User_EmailAddress,
( select top 1 Email from AspNetUsers where Id = t1.UserId_Client ) as Client_EmailAddress,
t1.* from Adviser_Ratings t1

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetListForDeleteReviews_ClientRatings]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_GetListForDeleteReviews_ClientRatings]
as
SET NOCOUNT ON
select 
( select top 1 Email from AspNetUsers where Id = t1.UserId ) as User_EmailAddress,
( select top 1 Email from AspNetUsers where Id = t1.UserId_Adviser ) as Adviser_EmailAddress,
t1.* from Client_Ratings t1
SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetListOfDatabaseTables]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_GetListOfDatabaseTables]
as
SET NOCOUNT ON

	SELECT TABLE_SCHEMA + '.' + TABLE_NAME as TABLE_NAME
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_TYPE = 'BASE TABLE'
	ORDER BY TABLE_SCHEMA + '.' + TABLE_NAME


SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetListOfPhotosByApprovalStatusId]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Admin_GetListOfPhotosByApprovalStatusId]
@PictureApprovalStatusId int
as
SET NOCOUNT ON

select c.*
from 
(
	select t1.UserId, max(t1.LastUpdated) as LastUpdated 
		from dbo.Adviser_ProfilePictures t1
		group by t1.UserId
) a
left join dbo.Adviser_ProfilePictures b
on a.UserId = b.UserId and a.LastUpdated = b.LastUpdated
left join dbo.Adviser_Details c
on b.UserId = c.UserId 
where b.PictureApprovalStatusId = @PictureApprovalStatusId and b.UserTypeId = 'Adviser'
order by b.LastUpdated asc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetListOfPhotosByApprovalStatusId_Clients]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_GetListOfPhotosByApprovalStatusId_Clients]
@PictureApprovalStatusId int
as
SET NOCOUNT ON

select c.*
from 
(
	select t1.UserId, max(t1.LastUpdated) as LastUpdated 
		from dbo.Adviser_ProfilePictures t1
		group by t1.UserId
) a
left join dbo.Adviser_ProfilePictures b
on a.UserId = b.UserId and a.LastUpdated = b.LastUpdated
left join dbo.Adviser_Details c
on b.UserId = c.UserId 
where b.PictureApprovalStatusId = @PictureApprovalStatusId and b.UserTypeId = 'Clients'
order by b.LastUpdated asc

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_GetSubscriptionTransactionsForUpdating]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_GetSubscriptionTransactionsForUpdating]
AS
SET NOCOUNT ON

select t1.Id, t1.UserId, t2.AccountNumber, t2.FirstName, t2.LastName, t1.PlanTypeId, 
		(
			select top 1 PlanType from dbo.SubscriptionPlanTypes where PlanTypeId = t1.PlanTypeId
		) as PlanType,
		t1.SubscriptionStatusId,
		(
			select top 1 SubscriptionStatus from dbo.SubscriptionStatus where SubscriptionStatusId = t1.SubscriptionStatusId
		) as  SubscriptionStatus,
		t1.AmountPaid, t1.StartDate, t1.EndDate, t1.TransactionId, t1.TransactionDate 
	from Adviser_Subscriptions t1
	left join Adviser_Details t2
	on t1.UserId = t2.UserId
	order by t1.Id asc

SET NOCOUNT OFF




GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_Post_GetImage]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_Post_GetImage]
@Id int
as
SET NOCOUNT ON

	select PostImageData from dbo.Admin_Posts where Id = @Id

SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_Posts_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_Posts_Add]
	@PostHeading nvarchar(512),
	@PostAuthor nvarchar(256),
	@PostImageData image,
	@PostContents nvarchar(MAX),
	@PostTypeId int
as
SET NOCOUNT ON

	insert into dbo.Admin_Posts
		select @PostHeading, @PostAuthor, @PostImageData, GETDATE(), @PostContents, @PostTypeId

SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_Posts_Delete]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_Posts_Delete]
@Id int
as
SET NOCOUNT ON

	delete from dbo.Admin_Posts where Id = @Id
	
SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_Posts_GetById]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Admin_Posts_GetById]
@Id int
as
SET NOCOUNT ON

	select Id, PostHeading, case when PostTypeId = 1 then PostAuthor
			 else ISNULL((
				select top 1 FirstName + ' ' + LastName from dbo.Adviser_Details where UserId = PostAuthor
			 ),'N/A') end as PostAuthor, PostAuthor as UserId, PostDate, 
			 PostTypeId,
			 PostContents 
		from dbo.Admin_Posts where Id = @Id

SET NOCOUNT OFF




GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_Posts_GetList]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[usp_Admin_Posts_GetList]
@PostTypeId int
as
SET NOCOUNT ON

	select Id, PostHeading, 
		case when PostTypeId = 1 then PostAuthor
			 else ISNULL((
				select top 1 FirstName + ' ' + LastName from dbo.Adviser_Details where UserId = PostAuthor
			 ),'N/A') end as PostAuthor, PostDate, PostContents from dbo.Admin_Posts where PostTypeId = @PostTypeId order by PostDate desc	
	
SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_Admin_Posts_GetList_ByAuthor]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Admin_Posts_GetList_ByAuthor]
@PostTypeId int, @UserId nvarchar(256)
as
SET NOCOUNT ON

	select Id, PostHeading, 
		case when PostTypeId = 1 then PostAuthor
			 else ISNULL((
				select top 1 FirstName + ' ' + LastName from dbo.Adviser_Details where UserId = PostAuthor
			 ),'N/A') end as PostAuthor, PostDate, PostContents from dbo.Admin_Posts 
			 where PostTypeId = @PostTypeId and PostAuthor = @UserId order by PostDate desc	
	
SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_CommissionsAndFees_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Adviser_CommissionsAndFees_Add]
	@UserId nvarchar(128),
	@CommissionsAndFeesId int,
	@YesNoNA int,
	@CommissionDescription nvarchar(500)
as
SET NOCOUNT ON

	insert into dbo.Adviser_CommissionsAndFees
		select @UserId, @CommissionsAndFeesId, @YesNoNA, @CommissionDescription, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_CommissionsAndFees_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Adviser_CommissionsAndFees_Get]
@UserId nvarchar(128), @CommissionsAndFeesId int
as
SET NOCOUNT ON

	select top 1 a.*, b.CommissionsAndFees
		from [dbo].[Adviser_CommissionsAndFees] a
		left join CommissionsAndFees b
		on a.CommissionsAndFeesId = b.CommissionsAndFeesId
		where a.CommissionsAndFeesId = @CommissionsAndFeesId and a.UserId = @UserId 
		order by LastUpdated desc
		
SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_CommunicationRequest_Accept]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_CommunicationRequest_Accept]
@Id int
as
SET NOCOUNT ON

	update dbo.CommunicationRequest 
		set AcceptOrDecline = 1, DeclineCommunicationReasonsId = 0, ProcessDate = getdate()
		where Id = @Id

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_CommunicationRequest_Decline]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_CommunicationRequest_Decline]
@Id int,
@DeclineCommunicationReasonsId int
as
SET NOCOUNT ON

	update dbo.CommunicationRequest 
		set AcceptOrDecline = 0, DeclineCommunicationReasonsId = @DeclineCommunicationReasonsId, ProcessDate = getdate()
		where Id = @Id

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_CV_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Adviser_CV_Add]
	@UserId nvarchar(128),
	@OriginalFileName nvarchar(256),
	@CV image
	
as
SET NOCOUNT ON

	insert into dbo.Adviser_CV
		select @UserId, @CV, @OriginalFileName, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_DealerGroupDetails_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_DealerGroupDetails_Add]
	@UserId nvarchar(128),
	@DealerGroupName nvarchar(256),
	@AFSL nvarchar(16),

	@HasDerivativesLicense int,
	@IsAuthorisedRep int,
	@AuthorisedRepNumber nvarchar(32),

	@AddressLn1 nvarchar(128),
	@AddressLn2 nvarchar(128),
	@AddressLn3 nvarchar(128),
	@Suburb nvarchar(64),
	@State nvarchar(64),
	@PostCode nvarchar(64),
	@Country nvarchar(64)
as
SET NOCOUNT ON

	insert into dbo.Adviser_DealerGroupDetails	
		select @UserId, @DealerGroupName, @AFSL, @HasDerivativesLicense, @IsAuthorisedRep, @AuthorisedRepNumber,
			   @AddressLn1, @AddressLn2, @AddressLn3, @Suburb, @State, @PostCode, @Country, GETDATE()


SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_DealerGroupDetails_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_DealerGroupDetails_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * 
		from dbo.Adviser_DealerGroupDetails 
		where Id = ( select max(Id) from dbo.Adviser_DealerGroupDetails where UserId = @UserId )
	
SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Description_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Adviser_Description_Add]
@UserId nvarchar(128),
@AdviserDescription nvarchar(1000)
as
SET NOCOUNT ON

	insert into dbo.Adviser_Description
		select @UserId, @AdviserDescription, GETDATE(), 0

SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Description_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[usp_Adviser_Description_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select top 1 * from dbo.Adviser_Description where UserId = @UserId and IsApproved = 1 order by LastUpdated desc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Description_Get_ForAdviser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_Description_Get_ForAdviser]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select top 1 * from dbo.Adviser_Description where UserId = @UserId order by LastUpdated desc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Details_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Adviser_Details_Add]
	@UserId nvarchar(128),
	
	@CurrentTitle nvarchar(64), 
	@CompanyName nvarchar(64), 
	@ABNACN nvarchar(16), 
	@ExperienceStartDate smalldatetime,
	
	@Title nvarchar(16),
	@FirstName nvarchar(128),
	@MiddleName nvarchar(128),
	@LastName nvarchar(128),
	@Gender nvarchar(32),

	@Phone nvarchar(32),
	@Fax nvarchar(32),
	@Mobile nvarchar(32),

	@AddressLn1 nvarchar(128),
	@AddressLn2 nvarchar(128),
	@AddressLn3 nvarchar(128),
	@Suburb nvarchar(64),
	@State nvarchar(64),
	@PostCode nvarchar(64),
	@Country nvarchar(64),

	@Lng float,
	@Lat float
as
SET NOCOUNT ON

	if exists( select * from dbo.Adviser_Details where UserId = @UserId )
	begin
		update dbo.Adviser_Details
			set CurrentTitle = @CurrentTitle, CompanyName = @CompanyName, ABNACN = @ABNACN, ExperienceStartDate = @ExperienceStartDate, Title = @Title,
				FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Gender = @Gender, Phone = @Phone, Fax = @Fax, Mobile = @Mobile,
				AddressLn1 = @AddressLn1, AddressLn2 = @AddressLn2, AddressLn3 = @AddressLn3, Suburb = @Suburb, [State] = @State, PostCode = @PostCode,
				Country = @Country, Lng = @Lng, Lat = @Lat, LastUpdated = GETDATE(), VerifiedId = 0

			where UserId = @UserId


	end
	else 
	begin

		insert into dbo.Adviser_Details
			select @UserId, @CurrentTitle, @CompanyName, @ABNACN, @ExperienceStartDate, @Title, @FirstName, @MiddleName, @LastName, @Gender, @Phone, @Fax, @Mobile,
				   @AddressLn1, @AddressLn2, @AddressLn3, @Suburb, @State, @PostCode, @Country, @Lng, @Lat, GETDATE(), 0, GETDATE()
	end

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Details_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Adviser_Details_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select top 1 * from dbo.Adviser_Details where UserId = @UserId order by LastUpdated desc


SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Education_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Adviser_Education_Add]
	@UserId nvarchar(128),
	@EducationLevels nvarchar(128),
	@Institution nvarchar(128),
	@CourseName nvarchar(128),
	@CurrentlyStudying int
as
SET NOCOUNT ON

	insert into dbo.Adviser_Education
		select @UserId, @EducationLevels, @Institution, @CourseName, @CurrentlyStudying, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Education_Delete]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_Education_Delete]
@Id int
as
SET NOCOUNT ON

	delete from dbo.Adviser_Education where Id = @Id

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Education_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Adviser_Education_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select t1.Id, t1.UserId, 
		t1.EducationLevels, 
		t1.Institution, 
		t1.CourseName, 
		t1.CurrentlyStudying, 
		case when t1.CurrentlyStudying = 1 then 'Completed' else 'Studying' end as CurrentlyStudyingStatus,
		t1.LastUpdated
		from dbo.Adviser_Education t1
		where t1.UserId = @UserId
	
SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_GetLatestFive]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Adviser_GetLatestFive]
as
SET NOCOUNT ON

	select top 4 * from dbo.Adviser_Details order by LastUpdated desc

SET NOCOUNT OFF




GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_GetVerificationStatus]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Adviser_GetVerificationStatus]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select a.UserId, b.VerifiedStatusId, b.VerifiedStatus 
	from 
	(

		select t1.*, 
			ISNULL((
				select top 1 VerifiedStatusId from dbo.Adviser_Verified_Status where UserId = t1.UserId order by LastUpdated desc
			),0) as VerifiedStatusId
			from dbo.Adviser_Details t1
	) a
	left join dbo.VerifiedStatus b
	on a.VerifiedStatusId = b.VerifiedStatusId
	where a.UserId = @UserId

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_HasCV_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_HasCV_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * from dbo.Adviser_CV where UserId = @UserId

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ManagementDetails]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_ManagementDetails]
	@UserId nvarchar(128),
	@TotalAssets float,
	@TotalManagedInvestments float,
	@TotalDirectAustralianEquities float,
	@TotalDirectInternational float,
	@TotalFixedInterest float,
	@TotalLendingBook float,
	@ApproxClientNumbersId int
as
SET NOCOUNT ON

	insert into dbo.Adviser_ManagementDetails
		select @UserId, @TotalAssets, @TotalManagedInvestments, @TotalDirectAustralianEquities, 
			   @TotalDirectInternational, @TotalFixedInterest, @TotalLendingBook,
			   @ApproxClientNumbersId, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ManagementDetails_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_ManagementDetails_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * 
		from [dbo].[Adviser_ManagementDetails]
		where Id = ( select max(Id) from [dbo].[Adviser_ManagementDetails] where UserId = @UserId )

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ProfessionalAssociations_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Adviser_ProfessionalAssociations_Add]
	@UserId nvarchar(128),
	@ProfessionalAssociationsId int,
	@OnOff bit
as
SET NOCOUNT ON

	delete from dbo.Adviser_ProfessionalAssociations where UserId = @UserId and ProfessionalAssociationsId = @ProfessionalAssociationsId

	if @OnOff = 1
	begin
		insert into dbo.Adviser_ProfessionalAssociations 
			select @UserId, @ProfessionalAssociationsId
	end	


SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ProfessionalAssociations_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_ProfessionalAssociations_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * 
		from dbo.Adviser_ProfessionalAssociations
		where UserId = @UserId

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ProfessionalType]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Adviser_ProfessionalType]
	@UserId nvarchar(128),
	@ProfessionTypeId int
as
SET NOCOUNT ON

	delete from dbo.Adviser_ProfessionalType where UserId = @UserId

	insert into dbo.Adviser_ProfessionalType 
		select @UserId, @ProfessionTypeId

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ProfessionalType_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_ProfessionalType_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * 
		from dbo.Adviser_ProfessionalType
		where UserId = @UserId

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Profile_SetApproval]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Adviser_Profile_SetApproval]
@Id int, @ProfileApprovalStatusId int

as
SET NOCOUNT ON

	update dbo.Adviser_Description 
		set IsApproved = @ProfileApprovalStatusId
		where Id = @Id

SET NOCOUNT OFF




GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ProfilePictures_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Adviser_ProfilePictures_Add]
	@UserId nvarchar(128),
	@UserTypeId nvarchar(128),
	@ImageData image
as
SET NOCOUNT ON

	if @UserTypeId = 'Client' 
	begin
		insert into dbo.Adviser_ProfilePictures
			select @UserId, 
				   @ImageData, 
				   @UserTypeId, 
				   2,
				   GETDATE()
	end
	else
	begin

		insert into dbo.Adviser_ProfilePictures
			select @UserId, 
				   @ImageData, 
				   @UserTypeId,
				   1, 
				   GETDATE()

	end

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ProfilePictures_SetApproval]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Adviser_ProfilePictures_SetApproval]
@UserId nvarchar(128),
@PictureApprovalStatusId int

AS
SET NOCOUNT ON

	update dbo.Adviser_ProfilePictures 
		set PictureApprovalStatusId = @PictureApprovalStatusId 
		where UserId = @UserId and LastUpdated = ( select max(LastUpdated) from dbo.Adviser_ProfilePictures where UserId = @UserId )

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Ratings_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Adviser_Ratings_Add]
	@UserId nvarchar(128),
	@UserId_Client nvarchar(128),

	@Feedback_Q1 float,
	@Feedback_Q1_Comments nvarchar(500),

	@Feedback_Q2 float,
	@Feedback_Q2_Comments nvarchar(500),

	@Feedback_Q3 float,
	@Feedback_Q3_Comments nvarchar(500),

	@Feedback_Q4 float,
	@Feedback_Q4_Comments nvarchar(500),

	@Feedback_Q5 float,
	@Feedback_Q5_Comments nvarchar(500),

	@AdditionalComments nvarchar(500),

	@IsSuccessReferral nvarchar(5)
AS
SET NOCOUNT ON

	insert into dbo.Adviser_Ratings
		select @UserId, @UserId_Client, 
			   @Feedback_Q1, @Feedback_Q1_Comments, @Feedback_Q2, @Feedback_Q2_Comments, @Feedback_Q3, @Feedback_Q3_Comments,
			   @Feedback_Q4, @Feedback_Q4_Comments, @Feedback_Q5, @Feedback_Q5_Comments, @AdditionalComments, @IsSuccessReferral, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Ratings_CheckExists]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Adviser_Ratings_CheckExists]
@UserId nvarchar(128),
@UserId_Client nvarchar(128)

as
SET NOCOUNT ON

	declare @Exists int
	set @Exists = 0

	if exists ( select * from dbo.Adviser_Ratings where UserId = @UserId and UserId_Client = @UserId_Client )
	begin
		set @Exists = 1
	end

	select @Exists as FeedbackExists

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Ratings_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Adviser_Ratings_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	declare @Rating float

	set @Rating = isnull((
		select AVG( ( t1.Feedback_Q1 + t1.Feedback_Q2 + t1.Feedback_Q3 + t1.Feedback_Q4 + t1.Feedback_Q5 ) / 5 ) as Rating
		from dbo.Adviser_Ratings t1
		where t1.UserId = @UserId
		group by t1.UserId
	),0)

	select @UserId as UserId, @Rating as Rating


SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Ratings_Get_DetailedList]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Adviser_Ratings_Get_DetailedList]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select top 3 * 
		from dbo.Client_Ratings t1
		where UserId_Adviser = @UserId
		order by t1.LastUpdate desc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Ratings_GetByClientId]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_Adviser_Ratings_GetByClientId]
@UserId nvarchar(128), @AdviserId nvarchar(128)
as
SET NOCOUNT ON

	declare @Rating float

	set @Rating = isnull((
		select AVG( ( t1.Feedback_Q1 + t1.Feedback_Q2 + t1.Feedback_Q3 + t1.Feedback_Q4 + t1.Feedback_Q5 ) / 5 ) as Rating
		from dbo.Adviser_Ratings t1
		where t1.UserId = @AdviserId and UserId_Client = @UserId
		group by t1.UserId
	), -1)

	select @UserId as UserId, @Rating as Rating


SET NOCOUNT OFF

-- exec [usp_Adviser_Ratings_GetByClientId] 'aaa', 'aaa'



GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ServicesProviding_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Adviser_ServicesProviding_Get]
@UserId nvarchar(128), @ServiceId int
as
SET NOCOUNT ON

select * 
	from dbo.Adviser_ServicesProviding t1
	where t1.UserId = @UserId 
	and t1.SubServiceId in ( select SubServiceId from SubServices where ServiceId = @ServiceId )
	and t1.SubServiceProvideYesNo = 'True'
	order by t1.SubServiceId asc
	
SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_ServicesProviding_GetByAdviser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_Adviser_ServicesProviding_GetByAdviser]
@UserId nvarchar(128)
as
SET NOCOUNT ON

select a.UserId, a.SubServiceId, c.SubServiceName, a.DateAdded
from
(
	select t1.UserId, t1.SubServiceId, max(t1.DateAdded) as DateAdded
		from dbo.Adviser_ServicesProviding t1
		where UserId = @UserId
		group by t1.UserId, t1.SubServiceId
) a 
inner join dbo.Adviser_ServicesProviding b
on a.UserId = b.UserId and a.SubServiceId = b.SubServiceId and a.DateAdded = b.DateAdded
left join dbo.SubServices c
on a.SubServiceId = c.SubServiceId
where b.SubServiceProvideYesNo = 'True'
order by c.SubServiceId asc
	
SET NOCOUNT OFF








GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Subscriptions_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_Adviser_Subscriptions_Add]
	@UserId nvarchar(128),
	@PlanTypeId int,
	@SubscriptionStatusId int,
	@AmountPaid float,
	@StartDate smalldatetime,
	@EndDate smalldatetime,
	@TransactionId nvarchar(512)
as
SET NOCOUNT ON

	insert into dbo.Adviser_Subscriptions
		select @UserId, @PlanTypeId, @SubscriptionStatusId, @AmountPaid, @StartDate, @EndDate, @TransactionId, GETDATE()	

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_Subscruptions_Update]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_Adviser_Subscruptions_Update]
	@Id int,
	@PlanTypeId int,
	@SubscriptionStatusId int,
	@AmountPaid float,
	@StartDate smalldatetime,
	@EndDate smalldatetime,
	@TransactionId nvarchar(512),
	@TransactionDate datetime
as 
SET NOCOUNT ON

	update dbo.Adviser_Subscriptions
		set PlanTypeId = @PlanTypeId, SubscriptionStatusId = @SubscriptionStatusId, AmountPaid = @AmountPaid, StartDate = @StartDate, EndDate = @EndDate, TransactionId = @TransactionId
		where Id = @Id


SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_SubService_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Adviser_SubService_Add]
	@UserId nvarchar(128),
	@SubServiceId int,
	@SubServiceSeekYesNo varchar(5)
as
SET NOCOUNT ON

	insert into dbo.Adviser_ServicesProviding
		select @UserId, @SubServiceId, @SubServiceSeekYesNo, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_TargetClients_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Adviser_TargetClients_Add]
	@UserId nvarchar(128),
	@AnnualIncomeLevelsId int,
	@InvestibleAssetsLevelId int,
	@TotalAssetsLevelId int
as
SET NOCOUNT ON

	insert into dbo.Adviser_TargetClients
		select @UserId, @AnnualIncomeLevelsId, @InvestibleAssetsLevelId, @TotalAssetsLevelId, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Adviser_TargetClients_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Adviser_TargetClients_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select top 1 * from dbo.Adviser_TargetClients where UserId = @UserId order by LastUpdated desc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_CheckSubscriptionStatusForAdviser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_CheckSubscriptionStatusForAdviser]
@UserId varchar(128)
as
SET NOCOUNT ON

	declare @Status int
	set @Status = 0

	declare @NumReferrals int

	declare @currDate datetime
	set @currDate = getdate()

	declare @plan_StartDate smalldatetime
	declare @plan_EndDate smalldatetime

	-- Get the current subscription plan for the adviser

	declare @PlanId int
	set @PlanId = ISNULL(( select max(Id) from dbo.Adviser_Subscriptions t1 where t1.UserId = @UserId and SubscriptionStatusId = 1 and @currDate between StartDate and EndDate ), -1)

	declare @PlanTypeId int
	set @PlanTypeId = ISNULL(( select PlanTypeId from dbo.Adviser_Subscriptions t1 where t1.Id = @PlanId ), 0)
	
	-- Default no plans, so max 3 referrals in lifetime 
	if @PlanTypeId = 0  
	begin
		set @NumReferrals = ISNULL((select count(*) from dbo.CommunicationRequest where Adviser_UserId = @UserId and AcceptOrDecline is not null), 0)
		if @NumReferrals >= 3
		begin
			-- Adviser need to pay
			set @Status = 1
		end	
	end
	
	-- Max 3 referals over 12 months period

	if @PlanTypeId = 1
	begin
		set @plan_StartDate = (select StartDate from Adviser_Subscriptions where Id = @PlanId )
		set @plan_EndDate = (select EndDate from Adviser_Subscriptions where Id = @PlanId )

		set @NumReferrals = ISNULL((select count(*) from dbo.CommunicationRequest where Adviser_UserId = @UserId and AcceptOrDecline is not null and ProcessDate between @plan_StartDate and @plan_EndDate ), 0)
		if @NumReferrals >= 3 
		begin
			-- Adviser need to pay
			set @Status = 1
		end
	end

	-- Max 5 referrals over 12 months period
	if @PlanTypeId = 2
	begin
		set @plan_StartDate = (select StartDate from Adviser_Subscriptions where Id = @PlanId )
		set @plan_EndDate = (select EndDate from Adviser_Subscriptions where Id = @PlanId )

		set @NumReferrals = ISNULL((select count(*) from dbo.CommunicationRequest where Adviser_UserId = @UserId and AcceptOrDecline is not null and ProcessDate between @plan_StartDate and @plan_EndDate ), 0)
		if @NumReferrals > 5 
		begin
			-- Adviser need to pay
			set @Status = 1
		end
	end

	-- Unlimited referrals, only need to find if there's a current subscription
	if @PlanTypeId >= 3
	begin
		set @plan_StartDate = (select StartDate from Adviser_Subscriptions where Id = @PlanId )
		set @plan_EndDate = (select EndDate from Adviser_Subscriptions where Id = @PlanId )

		if @currDate not between @plan_StartDate and @plan_EndDate
		begin
			-- Adviser need to pay
			set @Status = 1
		end		
	end

	select 
		@Status as PaymentRequired, 
		@PlanTypeId as PlanTypeId, 
		@PlanId as PlanId,
		( select PlanType from dbo.SubscriptionPlanTypes where PlanTypeId = @PlanTypeId ) as PlanTypeName,
		@NumReferrals as NumReferrals

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_Client_AdvisoryNeeds_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_Client_AdvisoryNeeds_Add]
	@UserId nvarchar(128),
	@ReasonForSeekingAdvice nvarchar(1000),
	@PersonalFinancialGoals nvarchar(1000),
	@ExpectationsFromAdviser nvarchar(1000),
	@AdditionalComments nvarchar(1000),
	@CommunicationMethodId int
as
SET NOCOUNT ON

	insert into dbo.Client_AdvisoryNeeds ( UserId, ReasonForSeekingAdvice, PersonalFinancialGoals, ExpectationsFromAdviser, AdditionalComments, CommunicationMethodId, LastUpdated )
		values ( @UserId, @ReasonForSeekingAdvice, @PersonalFinancialGoals, @ExpectationsFromAdviser, @AdditionalComments, @CommunicationMethodId, GETDATE() )

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_AdvisoryNeeds_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_Client_AdvisoryNeeds_Get]
	@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * from dbo.Client_AdvisoryNeeds t1
		where t1.Id = ( select max(Id) from dbo.Client_AdvisoryNeeds where UserId = @UserId )

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_Details_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Client_Details_Add]
	@UserId nvarchar(128),
	@DateOfBirth smalldatetime,

	@Title nvarchar(16),
	@FirstName nvarchar(128),
	@MiddleName nvarchar(128),
	@LastName nvarchar(128),

	@Gender nvarchar(32),

	@Phone nvarchar(32),
	@Fax nvarchar(32),
	@Mobile nvarchar(32),

	@AddressLn1 nvarchar(128),
	@AddressLn2 nvarchar(128),
	@AddressLn3 nvarchar(128),
	@Suburb nvarchar(64),
	@State nvarchar(64),
	@PostCode nvarchar(64),
	@Country nvarchar(64),

	@Lng float,
	@Lat float,

	@ClientGroupId nvarchar(128)
as
SET NOCOUNT ON

	if exists ( select * from dbo.Client_Details where UserId = @UserId )
	begin
		update dbo.Client_Details	
			set DateOfBirth = @DateOfBirth,
				Title = @Title, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Gender = @Gender, Phone = @Phone, Fax = @Fax,
				Mobile = @Mobile, AddressLn1 = @AddressLn1, AddressLn2 = @AddressLn2, AddressLn3 = @AddressLn3,
				Suburb = @Suburb, [State] = @State, PostCode = @PostCode, Lng = @Lng, Lat = @Lat, LastUpdated = GETDATE(), ClientGroupId = @ClientGroupId
			where UserId = @UserId
	end
	else
	begin
		insert into dbo.Client_Details	
			select @UserId, @DateOfBirth, @Title, @FirstName, @MiddleName, @LastName, @Gender, @Phone, @Fax, @Mobile, @AddressLn1, @AddressLn2, @AddressLn3, @Suburb, @State, @PostCode, @Country, @Lng, @Lat, getdate(), getdate(), @ClientGroupId
	end

SET NOCOUNT OFF







GO
/****** Object:  StoredProcedure [dbo].[usp_Client_Details_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_Client_Details_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * from dbo.Client_Details where UserId = @UserId

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_Details_GetForEmailTemplate_Result]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Client_Details_GetForEmailTemplate_Result]
@UserId nvarchar(128) 
as
SET NOCOUNT ON

declare @Rating float

set @Rating = ISNULL((
	select AVG( ( t1.Feedback_Q1 + t1.Feedback_Q2 + t1.Feedback_Q3 + t1.Feedback_Q4 + t1.Feedback_Q5 ) / 5 ) as Rating
			from dbo.Client_Ratings t1
			where t1.UserId = @UserId
			group by t1.UserId
),0) 

select	t1.FirstName, t1.LastName, 
		t1.AddressLn1, t1.AddressLn2, t1.AddressLn3, t1.Suburb, t1.[State], t1.PostCode, 
		t1.Phone, t1.Mobile, 
		(
			select Email from AspNetUsers where Id = t1.UserId
		) as EmailAddress,

		@Rating as Rating,

		t2.ReasonForSeekingAdvice, t2.PersonalFinancialGoals, t2.ExpectationsFromAdviser, t2.AdditionalComments, t2.CommunicationMethodId,
		(
			select CommunicationMethod from CommunicationMethod where CommunicationMethodId = t2.CommunicationMethodId
		) as CommunicationMethod,

		ISNULL((
			select b.TimeHorizon 
				from dbo.Client_InvestmentProfile a left join TimeHorizon b
				on a.TimeHorizonId = b.TimeHorizonId where a.Id = (select max(Id) from dbo.Client_InvestmentProfile where UserId = @UserId )
		),'N/A') as TimeHorizon		

	from dbo.Client_Details t1
	left join dbo.Client_AdvisoryNeeds t2
	on t1.UserId = t2.UserId and t2.Id = ( select max(Id) from dbo.Client_AdvisoryNeeds where UserId = @UserId )
	where t1.UserId = @UserId



SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_FinancialPosition_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_Client_FinancialPosition_Add]
	@UserId nvarchar(128),
	@TimeFrameForAdviceId int,
	@EmploymentStatusId int,
	@TotalAnnualIncome float,
	@TotalAssets float,
	@TotalLiabilities float,
	@TotalEquity float
as
SET NOCOUNT ON

	insert into dbo.Client_FinancialPosition
		select @UserId, @TimeFrameForAdviceId, @EmploymentStatusId, @TotalAnnualIncome, @TotalAssets, @TotalLiabilities, @TotalEquity, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_FinancialPosition_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_Client_FinancialPosition_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * from Client_FinancialPosition
		where Id = ( select max(Id) from Client_FinancialPosition where UserId = @UserId )
		and UserId = @UserId

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_InvestedProducts_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_Client_InvestedProducts_Add]
	@UserId nvarchar(128),
	@InvestedYesNo varchar(5),
	@InvestedProductsId int
as
SET NOCOUNT ON

	insert into dbo.Client_InvestedProducts
		select @UserId, @InvestedProductsId, @InvestedYesNo, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_InvestedProducts_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_Client_InvestedProducts_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select t1.* from Client_InvestedProducts t1
	inner join (
		select UserId, InvestedProductsId, max(DateAdded) as DateAdded
			from Client_InvestedProducts
			where UserId = @UserId
			group by UserId, InvestedProductsId
	) t2
	on t1.UserId = t2.UserId and t1.InvestedProductsId = t2.InvestedProductsId and t1.DateAdded = t2.DateAdded
	order by t1.InvestedProductsId asc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_InvestmentProfile_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_Client_InvestmentProfile_Add]
	@UserId nvarchar(128),
	@InvestableAssets float,
	@TimeHorizonId int,
	@ReturnsExpectation float,
	@InvestmentKnowledgeId int,
	@AttitudeToRiskId int,
	@AttitudeToCapitalLossId int

as
SET NOCOUNT ON

	insert into dbo.Client_InvestmentProfile
		select @UserId, @InvestableAssets, @TimeHorizonId, @ReturnsExpectation, @InvestmentKnowledgeId, @AttitudeToRiskId, @AttitudeToCapitalLossId, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_InvestmentProfile_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Client_InvestmentProfile_Get]
@UserId nvarchar(128)

as
SET NOCOUNT ON

select * 
	from dbo.Client_InvestmentProfile 
	 where Id = (select max(Id) from dbo.Client_InvestmentProfile where UserId = @UserId)

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_Ratings_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Client_Ratings_Add]
	@UserId nvarchar(128),
	@UserId_Adviser nvarchar(128),

	@Feedback_Q1 float,
	@Feedback_Q1_Comments nvarchar(500),

	@Feedback_Q2 float,
	@Feedback_Q2_Comments nvarchar(500),

	@Feedback_Q3 float,
	@Feedback_Q3_Comments nvarchar(500),

	@Feedback_Q4 float,
	@Feedback_Q4_Comments nvarchar(500),

	@Feedback_Q5 float,
	@Feedback_Q5_Comments nvarchar(500),

	@AdditionalComments nvarchar(500),

	@IsSuccessReferral nvarchar(5)
AS
SET NOCOUNT ON

	insert into dbo.Client_Ratings
		select @UserId, @UserId_Adviser, 
			   @Feedback_Q1, @Feedback_Q1_Comments, @Feedback_Q2, @Feedback_Q2_Comments, @Feedback_Q3, @Feedback_Q3_Comments,
			   @Feedback_Q4, @Feedback_Q4_Comments, @Feedback_Q5, @Feedback_Q5_Comments, @AdditionalComments, @IsSuccessReferral, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_Ratings_CheckExists]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_Client_Ratings_CheckExists]

@UserId nvarchar(128),
@UserId_Adviser nvarchar(128)

as
SET NOCOUNT ON

	declare @Exists int
	set @Exists = 0

	if exists ( select * from dbo.Client_Ratings where UserId = @UserId and UserId_Adviser = @UserId_Adviser )
	begin
		set @Exists = 1
	end

	select @Exists as FeedbackExists

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_Ratings_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Client_Ratings_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select t1.UserId, AVG( ( t1.Feedback_Q1 + t1.Feedback_Q2 + t1.Feedback_Q3 + t1.Feedback_Q4 + t1.Feedback_Q5 ) / 5 ) as Rating
		from dbo.Client_Ratings t1
		where t1.UserId = @UserId
		group by t1.UserId

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_Ratings_Get_DetailedList]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_Client_Ratings_Get_DetailedList]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select top 3 * 
		from dbo.Adviser_Ratings t1
		where UserId_Client = @UserId
		order by t1.LastUpdate desc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_Ratings_GetByAdviserId]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Client_Ratings_GetByAdviserId]
@UserId nvarchar(128), @ClientId nvarchar(128)
as
SET NOCOUNT ON

	declare @Rating float

	set @Rating = isnull((
		select AVG( ( t1.Feedback_Q1 + t1.Feedback_Q2 + t1.Feedback_Q3 + t1.Feedback_Q4 + t1.Feedback_Q5 ) / 5 ) as Rating
		from dbo.Client_Ratings t1
		where t1.UserId = @ClientId and UserId_Adviser = @UserId
		group by t1.UserId
	), -1)

	select @UserId as UserId, @Rating as Rating
	

SET NOCOUNT OFF

-- exec [usp_Adviser_Ratings_GetByClientId] 'aaa', 'aaa'



GO
/****** Object:  StoredProcedure [dbo].[usp_Client_SubService_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Client_SubService_Add]
	@UserId nvarchar(128),
	@SubServiceId int,
	@SubServiceSeekYesNo varchar(5)
as
SET NOCOUNT ON

	insert into dbo.Client_ServicesSeeking
		select @UserId, @SubServiceId, @SubServiceSeekYesNo, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_SubService_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_Client_SubService_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select t1.* from dbo.Client_ServicesSeeking t1
	inner join (
		select UserId, SubServiceId, max(DateAdded) as DateAdded
			from dbo.Client_ServicesSeeking
			where UserId = @UserId
			group by UserId, SubServiceId
	) t2
	on t1.UserId = t2.UserId and t1.SubServiceId = t2.SubServiceId and t1.DateAdded = t2.DateAdded
	order by t1.SubServiceId asc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Client_SubService_GetTrueOnly]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_Client_SubService_GetTrueOnly]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select t1.UserId, t1.SubServiceId, t1.SubServiceSeekYesNo, t3.SubServiceName, t1.DateAdded
	from dbo.Client_ServicesSeeking t1
	inner join (
		select UserId, SubServiceId, max(DateAdded) as DateAdded
			from dbo.Client_ServicesSeeking
			where UserId = @UserId
			group by UserId, SubServiceId
	) t2
	on t1.UserId = t2.UserId and t1.SubServiceId = t2.SubServiceId and t1.DateAdded = t2.DateAdded
	left join dbo.SubServices t3
	on t1.SubServiceId = t3.SubServiceId
	where t1.SubServiceSeekYesNo = 'True'
	order by t1.SubServiceId asc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_CommunicationRequest_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_CommunicationRequest_Add]
	@Adviser_UserId nvarchar(128),
	@Client_UserId nvarchar(128),
	@AcceptOrDecline bit
as
SET NOCOUNT ON

	insert into dbo.CommunicationRequest
		select @Adviser_UserId, @Client_UserId, @AcceptOrDecline, GETDATE(), 0, null

SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_CommunicationRequest_Get_ForAdviser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_CommunicationRequest_Get_ForAdviser]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * 
		from dbo.CommunicationRequest
		where Adviser_UserId = @UserId
		order by RequestDate desc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_CommunicationRequest_Get_ForClient]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_CommunicationRequest_Get_ForClient]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select * 
		from dbo.CommunicationRequest
		where Client_UserId = @UserId
		order by RequestDate desc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_CommunicationRequest_GetListForAdviser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_CommunicationRequest_GetListForAdviser]
@UserId nvarchar(128), 
@StatusId int
as
SET NOCOUNT ON

	if @StatusId = 0 
	begin

		select t1.*, 
			ISNULL((
				select top 1 FirstName from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as FirstName,
			ISNULL((
				select top 1 LastName from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as LastName,
			ISNULL((
				select top 1 Suburb from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as Suburb,
			ISNULL((
				select top 1 PostCode from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as PostCode,

			ISNULL((
				select top 1 b.EmploymentStatus 
					from Client_FinancialPosition a 
					left join EmploymentStatus b
					on a.EmploymentStatusId = b.EmploymentStatusId where a.UserId = t1.Client_UserId order by a.LastUpdated desc
			),'N/A') as EmploymentStatus,
			
			ISNULL((
				select top 1 DATEDIFF( YEAR, DateOfBirth, getdate()) as Age from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
				),0) as Age

			from CommunicationRequest t1
			where t1.AcceptOrDecline is null and Adviser_UserId = @UserId
			and t1.Client_UserId not in ( select UserId from AspNetUserRoles where RoleId in (select Id from AspNetRoles where [Name] in ( 'preadviser','adviser' ) ) ) 
			order by t1.RequestDate desc

	end

	if @StatusId = 1 
	begin

		select t1.*, 
			ISNULL((
				select top 1 FirstName from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as FirstName,
			ISNULL((
				select top 1 LastName from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as LastName,
			ISNULL((
				select top 1 Suburb from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as Suburb,
			ISNULL((
				select top 1 PostCode from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as PostCode,

			ISNULL((
				select top 1 b.EmploymentStatus 
					from Client_FinancialPosition a 
					left join EmploymentStatus b
					on a.EmploymentStatusId = b.EmploymentStatusId where a.UserId = t1.Client_UserId order by a.LastUpdated desc
			),'N/A') as EmploymentStatus,
			
			ISNULL((
				select top 1 DATEDIFF( YEAR, DateOfBirth, getdate()) as Age from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
				),0) as Age

			from CommunicationRequest t1
			where t1.AcceptOrDecline = 1 and Adviser_UserId = @UserId
			and t1.Client_UserId not in ( select UserId from AspNetUserRoles where RoleId in (select Id from AspNetRoles where [Name] in ( 'preadviser','adviser' ) ) ) 
			order by t1.RequestDate desc

	end
	
	if @StatusId = 2 
	begin

		select t1.*, 
			ISNULL((
				select top 1 FirstName from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as FirstName,
			ISNULL((
				select top 1 LastName from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as LastName,
			ISNULL((
				select top 1 Suburb from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as Suburb,
			ISNULL((
				select top 1 PostCode from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as PostCode,

			ISNULL((
				select top 1 b.EmploymentStatus 
					from Client_FinancialPosition a 
					left join EmploymentStatus b
					on a.EmploymentStatusId = b.EmploymentStatusId where a.UserId = t1.Client_UserId order by a.LastUpdated desc
			),'N/A') as EmploymentStatus,
			
			ISNULL((
				select top 1 DATEDIFF( YEAR, DateOfBirth, getdate()) as Age from Client_Details where UserId = t1.Client_UserId order by LastUpdated desc
				),0) as Age

			from CommunicationRequest t1
			where t1.AcceptOrDecline = 0 and Adviser_UserId = @UserId
			and t1.Client_UserId not in ( select UserId from AspNetUserRoles where RoleId in (select Id from AspNetRoles where [Name] in ( 'preadviser','adviser' ) ) ) 
			order by t1.RequestDate desc

	end


SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_CommunicationRequest_GetListForAdviserFromProfessionals]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_CommunicationRequest_GetListForAdviserFromProfessionals]
@UserId nvarchar(128), 
@StatusId int
as
SET NOCOUNT ON

	if @StatusId = 0 
	begin

		select t1.*, 
			ISNULL((
				select top 1 FirstName from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as FirstName,
			ISNULL((
				select top 1 LastName from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as LastName,
			ISNULL((
				select top 1 Suburb from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as Suburb,
			ISNULL((
				select top 1 PostCode from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as PostCode,

			ISNULL((
				select top 1 CurrentTitle from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
	 		),'N/A') as CurrentTitle,
			
			ISNULL((
				select top 1 CompanyName from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
				),'N/A') as CompanyName

			from CommunicationRequest t1
			where t1.AcceptOrDecline is null and Adviser_UserId = @UserId
			and t1.Client_UserId in ( select UserId from AspNetUserRoles where RoleId in (select Id from AspNetRoles where [Name] in ( 'preadviser','adviser', 'admin' ) ) ) 
			order by t1.RequestDate desc

	end

	if @StatusId = 1 
	begin

		select t1.*, 
			ISNULL((
				select top 1 FirstName from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as FirstName,
			ISNULL((
				select top 1 LastName from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as LastName,
			ISNULL((
				select top 1 Suburb from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as Suburb,
			ISNULL((
				select top 1 PostCode from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as PostCode,

			ISNULL((
				select top 1 CurrentTitle from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
	 		),'N/A') as CurrentTitle,
			
			ISNULL((
				select top 1 CompanyName from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
				),'N/A') as CompanyName

			from CommunicationRequest t1
			where t1.AcceptOrDecline = 1 and Adviser_UserId = @UserId
			and t1.Client_UserId in ( select UserId from AspNetUserRoles where RoleId in (select Id from AspNetRoles where [Name] in ( 'preadviser','adviser', 'admin' ) ) ) 
			order by t1.RequestDate desc

	end
	
	if @StatusId = 2 
	begin

		select t1.*, 
			ISNULL((
				select top 1 FirstName from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as FirstName,
			ISNULL((
				select top 1 LastName from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as LastName,
			ISNULL((
				select top 1 Suburb from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as Suburb,
			ISNULL((
				select top 1 PostCode from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
			),'N/A') as PostCode,

			ISNULL((
				select top 1 CurrentTitle from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
	 		),'N/A') as CurrentTitle,
			
			ISNULL((
				select top 1 CompanyName from Adviser_Details where UserId = t1.Client_UserId order by LastUpdated desc
				),'N/A') as CompanyName

			from CommunicationRequest t1
			where t1.AcceptOrDecline = 0 and Adviser_UserId = @UserId
			and t1.Client_UserId in ( select UserId from AspNetUserRoles where RoleId in (select Id from AspNetRoles where [Name] in ( 'preadviser','adviser', 'admin' ) ) ) 
			order by t1.RequestDate desc

	end


SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_CommunicationRequest_GetListForClient]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_CommunicationRequest_GetListForClient]
@UserId nvarchar(128),
@StatusId int
as
SET NOCOUNT ON

-- Pending requests
if @StatusId = 0	
begin

	select * 
		from [dbo].[CommunicationRequest] t1
		where t1.Client_UserId = @UserId and t1.AcceptOrDecline is null
		order by RequestDate desc

end

-- Accepted requests
if @StatusId = 1	
begin

		select * 
			from [dbo].[CommunicationRequest] t1
			where t1.Client_UserId = @UserId and t1.AcceptOrDecline = 1
	order by RequestDate desc

end

-- Declined requests
if @StatusId = 2	
begin

			select * 
				from [dbo].[CommunicationRequest] t1
				where t1.Client_UserId = @UserId and t1.AcceptOrDecline = 0
		order by RequestDate desc

end


SET NOCOUNT OFF




GO
/****** Object:  StoredProcedure [dbo].[usp_DistributionLists_GetAllUsersByListId]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_DistributionLists_GetAllUsersByListId]
@DistributionListId int
as
SET NOCOUNT ON
select t1.UserId, 'adviser' as RoleName, t2.Email, t3.CompanyName, t3.FirstName, t3.LastName, 
	   case when EXISTS( select * from User_DistributionLists where UserId = t1.UserId and DistributionListId = @DistributionListId ) then 'true' else 'false' end as Subscribed
	from AspNetUserRoles t1 
	left join AspNetUsers t2
	on t1.UserId = t2.Id
	inner join Adviser_Details t3
	on t1.UserId = t3.UserId
	where t1.RoleId in ( select Id from AspNetRoles where [Name] = 'adviser' )

union 

select t1.UserId, 'client' as RoleName, t2.Email, t3.CompanyName, t3.FirstName, t3.LastName,
	   case when EXISTS( select * from User_DistributionLists where UserId = t1.UserId and DistributionListId = @DistributionListId ) then 'true' else 'false' end as Subscribed
	from AspNetUserRoles t1 
	left join AspNetUsers t2
	on t1.UserId = t2.Id
	inner join Adviser_Details t3
	on t1.UserId = t3.UserId
	where t1.RoleId in ( select Id from AspNetRoles where [Name] = 'client' )

union 

select t1.UserId, 'admin' as RoleName, t2.Email, 'CompareAdvisers.com' as CompanyName, 'Staff' as FirstName, 'Member' as LastName, 
	   case when EXISTS( select * from User_DistributionLists where UserId = t1.UserId and DistributionListId = @DistributionListId ) then 'true' else 'false' end as Subscribed
	from AspNetUserRoles t1 
	left join AspNetUsers t2
	on t1.UserId = t2.Id
	where t1.RoleId in ( select Id from AspNetRoles where [Name] = 'admin' )

	
SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_DistributionLists_GetEmailsByListId]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_DistributionLists_GetEmailsByListId]
@DistributionListId int
as
SET NOCOUNT ON

	select t1.*, t2.Email 
		from dbo.User_DistributionLists t1
			left join dbo.AspNetUsers t2
			on t1.UserId = t2.Id
			where DistributionListId = @DistributionListId

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_GetAdviserEmailByCommunicationId]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_GetAdviserEmailByCommunicationId]
@Id int
as
SET NOCOUNT ON

	select t1.Id, t1.Adviser_UserId, t1.Client_UserId, t2.Email as ClientEmailAddress, t3.FirstName as ClientFirstName, t3.LastName as ClientLastName
		from dbo.CommunicationRequest t1
		left join dbo.AspNetUsers t2
		on t1.Client_UserId = t2.Id
		left join Client_Details t3
		on t1.Client_UserId = t3.UserId
		where t1.Id = @Id

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllFeedbackCommentsOnAdviser]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_GetAllFeedbackCommentsOnAdviser]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select *, ( Feedback_Q1 + Feedback_Q2 + Feedback_Q3 + Feedback_Q4 + Feedback_Q5 ) / 5 as FinalRating from dbo.Adviser_Ratings
		where UserId = @UserId
		order by Id desc

SET NOCOUNT OFF





GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllFeedbackCommentsOnClient]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_GetAllFeedbackCommentsOnClient]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select *, ( Feedback_Q1 + Feedback_Q2 + Feedback_Q3 + Feedback_Q4 + Feedback_Q5 ) / 5 as FinalRating from dbo.Client_Ratings
		where UserId = @UserId
		order by Id desc

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_GetClientCommunicationHistory]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- exec usp_GetClientCommunicationHistory '763acb92-a043-4519-904a-fef31a9402fc'

CREATE procedure [dbo].[usp_GetClientCommunicationHistory]
@UserId nvarchar(128)
as
SET NOCOUNT ON

-- Get commmunication history (last 2) for client

select top 2 t1.Id, t1.Adviser_UserId, t1.RequestDate, t2.FirstName, t2.LastName, t2.CurrentTitle, t2.Suburb, t2.[State], t2.PostCode
	from dbo.CommunicationRequest t1
	left join dbo.Adviser_Details t2
	on t1.Adviser_UserId = t2.UserId 
	where t1.Client_UserId = @UserId
	order by t1.RequestDate desc

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_GetClientEmailByCommunicationId]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_GetClientEmailByCommunicationId]
@Id int
as
SET NOCOUNT ON

	select t1.Id, t1.Adviser_UserId, t1.Client_UserId, t2.Email as ClientEmailAddress, t3.FirstName as AdviserFirstName, t3.LastName as AdviserLastName, t3.CurrentTitle, t4.DeclineCommunicationReasons
		from dbo.CommunicationRequest t1
		left join dbo.AspNetUsers t2
		on t1.Client_UserId = t2.Id
		left join Adviser_Details t3
		on t1.Adviser_UserId = t3.UserId
		left join DeclineCommunicationReasons t4
		on t1.DeclineCommunicationReasonsId = t4.DeclineCommunicationReasonsId
		where t1.Id = @Id

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_RecommendedAdvisers_Top3]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_RecommendedAdvisers_Top3]
@Adviser_UserId nvarchar(128),
@Client_UserId nvarchar(128)
as
SET NOCOUNT ON

declare @CurrProfessionTypeId int 
set @CurrProfessionTypeId = isnull(( select ProfessionTypeId from dbo.Adviser_ProfessionalType where UserId = @Adviser_UserId ), 0)

-- Calculation of scores is based on:
-- 1. adviser professional type
-- 2. adviser services providing
-- 3. adviser ratings

-- Get Advisers who are blocked

if object_id('tempdb..#advisersBlocked' ) is not null drop table #advisersBlocked

-- Filter for advisers who are blocked
select b.UserId into #advisersBlocked
from 
(
	select UserId, max(Id) as Id 
		from [dbo].[Adviser_Verified_Status] 
		group by UserId
) a
inner join [dbo].[Adviser_Verified_Status] b
on a.Id = b.Id and a.UserId = b.UserId
where b.VerifiedStatusId in ( 2 )


if object_id('tempdb..#clientServicesSeek' ) is not null drop table #clientServicesSeek

select b.* into #clientServicesSeek
from 
(
	select t1.UserId, t1.SubServiceId, max(t1.DateAdded) as DateAdded 
		from Client_ServicesSeeking t1 
		group by t1.UserId, t1.SubServiceId
) a
inner join Client_ServicesSeeking b
on a.UserId = b.UserId and a.SubServiceId = b.SubServiceId and a.DateAdded = b.DateAdded
where a.UserId = @Client_UserId and b.SubServiceSeekYesNo = 'True'

if object_id('tempdb..#adviserServicesSeek' ) is not null drop table #adviserServicesSeek

select b.* into #adviserServicesSeek
from
(
	select t1.UserId, t1.SubServiceId, max(t1.DateAdded) as DateAdded
		from [dbo].[Adviser_ServicesProviding] t1
		group by t1.UserId, t1.SubServiceId
) a
inner join [Adviser_ServicesProviding] b
on a.UserId = b.UserId and a.SubServiceId = b.SubServiceId and a.DateAdded = b.DateAdded


-- Get all Adviser ratings
if object_id('tempdb..#adviserRatings' ) is not null drop table #adviserRatings

select t1.UserId, AVG( ( t1.Feedback_Q1 + t1.Feedback_Q2 + t1.Feedback_Q3 + t1.Feedback_Q4 + t1.Feedback_Q5 ) / 5 ) as Rating
	into #adviserRatings
	from dbo.Adviser_Ratings t1
	group by t1.UserId

select top 3 * 
from
(
	select *,

		ISNULL((
			select count(*) from #adviserServicesSeek where UserId = t1.UserId and SubServiceId in ( select SubServiceId from #clientServicesSeek )
		),0) +

		ISNULL((
			select count(*) from dbo.Adviser_ProfessionalType where UserId = t1.UserId and ProfessionTypeId = @CurrProfessionTypeId
		),0) + 

		ISNULL((
			select Rating from #adviserRatings where UserId = t1.UserId
		),0) as ServicesMatchScore

		from Adviser_Details t1
		where t1.UserId not in ( select UserId from #advisersBlocked ) and t1.UserId not in ( @Adviser_UserId )
) t1
order by t1.ServicesMatchScore desc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_Search_Adviser_GetList]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- exec [usp_Search_Adviser_GetList] 0, 0

CREATE procedure [dbo].[usp_Search_Adviser_GetList]
@lng float, @lat float
AS
SET NOCOUNT ON


if OBJECT_ID('tempdb..#ServiceTempTable') is not null drop table #ServiceTempTable
if OBJECT_ID('tempdb..#BlockedAdvisers') is not null drop table #BlockedAdvisers


	create table #BlockedAdvisers
	(
		UserId nvarchar(128)
	)

	insert into #BlockedAdvisers
		select b.UserId
			from
			(
				select a.UserId, max(a.LastUpdated) as LastUpdated 
					from dbo.Adviser_Verified_Status a
					group by a.UserId
			) b inner join dbo.Adviser_Verified_Status c
			on b.UserId = c.UserId and b.LastUpdated = c.LastUpdated

			where c.VerifiedStatusId = 2

	create table #ServiceTempTable 
	( 
		UserId nvarchar(128), 
		SubServiceId int, 
		SubServiceName nvarchar(128) 
	)

	insert into #ServiceTempTable
		select b.UserId, b.SubServiceId, c.SubServiceName
			from 
			(
				select UserId, SubServiceId, max(DateAdded) as DateAdded from Adviser_ServicesProviding
				group by UserId, SubServiceId
			) a
			inner join Adviser_ServicesProviding b
			on a.UserId = b.UserId and a.SubServiceId = b.SubServiceId and a.DateAdded = b.DateAdded
			left join SubServices c
			on b.SubServiceId = c.SubServiceId
			where b.SubServiceProvideYesNo = 'True'

	if @lng = 0 and @lat = 0 
	begin

		select t1.Id,

			ISNULL((
				select top 1 AccountNumber from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'') as [AccountNumber],

			ISNULL((
				select top 1 FirstName from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [FirstName],

			ISNULL((
				select top 1 LastName from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [LastName],

			ISNULL((
				select bb.ProfessionType 
					from Adviser_ProfessionalType aa
					left join ProfessionType bb
					on aa.ProfessionTypeId = bb.ProfessionTypeId where aa.UserId = t1.Id 
			),'N/A') as ProfessionType,

			ISNULL((
				select top 1 Suburb from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [Suburb],

			ISNULL((
				select top 1 PostCode from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [PostCode],

			case when ISNULL(( select top 1 AFSL from Adviser_DealerGroupDetails where UserId = t1.Id order by Id desc ),'' ) <> '' then 'Yes' else 'No' end as HasAFSL,

			case when ISNULL(( select top 1 IsAuthorisedRep from Adviser_DealerGroupDetails where UserId = t1.Id order by Id desc ), 0 ) = 1 then 'Yes' else 'No' end as IsAuthorisedRep,

			ISNULL((
				select top 1 CurrentTitle from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [CurrentTitle],

			ISNULL((
				select top 1 DATEDIFF( YEAR, ExperienceStartDate, getdate()) as YearsExperience from Adviser_Details where UserId = t1.Id order by LastUpdated desc
				),0) as YearsExperience,

			ISNULL((
				select top 1 TotalAssets from Adviser_ManagementDetails where UserId = t1.Id order by LastUpdated desc
			),0) as TotalAssetsUnderManagement,

			case when ISNULL(( select top 1 VerifiedStatusId from dbo.Adviser_Verified_Status where UserId = t1.Id order by LastUpdated desc ), 0 ) = 1 then 'Verified' else 'Not Verified' end as VerifiedStatus,

			ISNULL(( select max(LastUpdated) from Adviser_Details where UserId = t1.Id ), GETDATE() ) as [LastUpdated],


			( select [p].SubServiceName as 'li'
				from #ServiceTempTable as [p]
				WHERE [p].[UserId] collate database_default = t1.Id collate database_default
				for xml path(''), type ) as Specialization,

			ISNULL((
				select AVG( ( Feedback_Q1 + Feedback_Q2 + Feedback_Q3 + Feedback_Q4 + Feedback_Q5 ) / 5 ) as Rating
					from dbo.Adviser_Ratings
					where UserId = t1.Id
					group by UserId
			),0) as [Rating],

			0 as distance

			from AspNetUsers t1

			where t1.Id in ( select UserId from AspNetUserRoles where RoleId = (select Id from AspNetRoles where [Name] = 'adviser')) 
			and t1.Id collate database_default not in ( select UserId from #BlockedAdvisers )
			order by [LastUpdated] desc

	end
	else
	begin

		select t1.Id,

			ISNULL((
				select top 1 AccountNumber from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'') as [AccountNumber],

			ISNULL((
				select top 1 FirstName from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [FirstName],

			ISNULL((
				select top 1 LastName from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [LastName],

			ISNULL((
				select bb.ProfessionType 
					from Adviser_ProfessionalType aa
					left join ProfessionType bb
					on aa.ProfessionTypeId = bb.ProfessionTypeId where aa.UserId = t1.Id 
			),'N/A') as ProfessionType,

			ISNULL((
				select top 1 Suburb from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [Suburb],

			ISNULL((
				select top 1 PostCode from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [PostCode],

			case when ISNULL(( select top 1 AFSL from Adviser_DealerGroupDetails where UserId = t1.Id order by Id desc ),'' ) <> '' then 'Yes' else 'No' end as HasAFSL,

			case when ISNULL(( select top 1 IsAuthorisedRep from Adviser_DealerGroupDetails where UserId = t1.Id order by Id desc ), 0 ) = 1 then 'Yes' else 'No' end as IsAuthorisedRep,

			ISNULL((
				select top 1 CurrentTitle from Adviser_Details where UserId = t1.Id order by LastUpdated desc
			),'N/A') as [CurrentTitle],

			ISNULL((
				select top 1 DATEDIFF( YEAR, ExperienceStartDate, getdate()) as YearsExperience from Adviser_Details where UserId = t1.Id order by LastUpdated desc
				),0) as YearsExperience,

			ISNULL((
				select top 1 TotalAssets from Adviser_ManagementDetails where UserId = t1.Id order by LastUpdated desc
			),0) as TotalAssetsUnderManagement,

			case when ISNULL(( select top 1 VerifiedStatusId from dbo.Adviser_Verified_Status where UserId = t1.Id order by LastUpdated desc ), 0 ) = 1 then 'Verified' else 'Not Verified' end as VerifiedStatus,

			ISNULL(( select max(LastUpdated) from Adviser_Details where UserId = t1.Id ), GETDATE() ) as [LastUpdated],

			( select [p].SubServiceName as 'li'
				from #ServiceTempTable as [p]
				WHERE [p].[UserId] collate database_default = t1.Id collate database_default
				for xml path(''), type ) as Specialization,

			ISNULL((
				select AVG( ( Feedback_Q1 + Feedback_Q2 + Feedback_Q3 + Feedback_Q4 + Feedback_Q5 ) / 5 ) as Rating
					from dbo.Adviser_Ratings
					where UserId = t1.Id
					group by UserId
			),0) as [Rating],

			ACOS(SIN(RADIANS(@lat)) * SIN(RADIANS(
				ISNULL(( select top 1 Lat from dbo.Adviser_Details where UserId = t1.Id order by LastUpdated desc ), 0)
			)) + COS(RADIANS(@lat)) * COS(RADIANS(
				ISNULL(( select top 1 Lat from dbo.Adviser_Details where UserId = t1.Id order by LastUpdated desc ), 0)
			)) * COS(RADIANS(@lng - 
				ISNULL(( select top 1 Lng from dbo.Adviser_Details where UserId = t1.Id order by LastUpdated desc ), 0)
			))) * 6378.10 as distance

			from AspNetUsers t1

			where t1.Id in ( select UserId from AspNetUserRoles where RoleId = (select Id from AspNetRoles where [Name] = 'adviser')) 
			and t1.Id collate database_default not in ( select UserId from #BlockedAdvisers )
			order by distance asc

	end
	
SET NOCOUNT OFF










GO
/****** Object:  StoredProcedure [dbo].[usp_TrackAdviserViews_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_TrackAdviserViews_Add]
@UserId nvarchar(128), @ViewByTypeId int
as
SET NOCOUNT ON

	insert into  dbo.TrackAdviserViews values ( @UserId, @ViewByTypeId, getdate())

SET NOCOUNT OFF



GO
/****** Object:  StoredProcedure [dbo].[usp_User_NewsletterServices_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_User_NewsletterServices_Add]
@UserId nvarchar(128), @tNewsletterServicesId int, @tIsSubscribed nvarchar(5)
as
SET NOCOUNT ON

	insert into dbo.User_NewsletterServices
		select @UserId, @tNewsletterServicesId, @tIsSubscribed, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_User_NewsletterServices_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_User_NewsletterServices_Get]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select t1.* from dbo.User_NewsletterServices t1
	inner join (
		select UserId, NewsletterServicesId, max(LastUpdated) as LastUpdated
			from dbo.User_NewsletterServices
			where UserId = @UserId
			group by UserId, NewsletterServicesId
	) t2
	on t1.UserId = t2.UserId and t1.NewsletterServicesId = t2.NewsletterServicesId and t1.LastUpdated = t2.LastUpdated
	order by t1.NewsletterServicesId asc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_User_NewsletterServices_GetWithName]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_User_NewsletterServices_GetWithName]
@UserId nvarchar(128)
as
SET NOCOUNT ON

	select t1.*, t3.NewsletterServices from dbo.User_NewsletterServices t1
	inner join (
		select UserId, NewsletterServicesId, max(LastUpdated) as LastUpdated
			from dbo.User_NewsletterServices
			where UserId = @UserId
			group by UserId, NewsletterServicesId
	) t2
	left join dbo.NewsletterServices t3
	on t2.NewsletterServicesId = t3.NewsletterServicesId
	on t1.UserId = t2.UserId and t1.NewsletterServicesId = t2.NewsletterServicesId and t1.LastUpdated = t2.LastUpdated
	order by t1.NewsletterServicesId asc

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_WebStats_NumberOfLogins_Add]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_WebStats_NumberOfLogins_Add]
@Email nvarchar(256)
as
SET NOCOUNT ON

	insert into dbo.WebStats_NumberOfLogins
		select @Email, GETDATE()

SET NOCOUNT OFF






GO
/****** Object:  StoredProcedure [dbo].[usp_WebStats_NumberOfLogins_Get]    Script Date: 03/03/15 1:01:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_WebStats_NumberOfLogins_Get]
as
SET NOCOUNT ON

	select count(*) as NumberOfLogins from dbo.WebStats_NumberOfLogins

SET NOCOUNT OFF






GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable of addresses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Addresses'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Link table of adviser to capability types' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserCapabilityLinks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ensures that each adviser is linked to the capability only once' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserCapabilityLinks', @level2type=N'CONSTRAINT',@level2name=N'AdviserCapabilityUnique'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable containing information related to advisers' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advisers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Stores a number of appointments' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Appointments'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table description tyeps of assets available' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AssetTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable containing attachments related to correspondence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Attachments'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable of beneficiaries linked to insurance policies' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Beneficiaries'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of the different groups of capability types for advisers' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CapabilityGroups'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table for capability types that advisers can have' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CapabilityTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable contain information for clients' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Clients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of global currencies' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Currencies'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable containing information particular to daily prices of a stock' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DailyPrices'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable of dependants for clients' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dependants'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable of investment properties' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DirectProperties'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable containing data particular to dividend histories' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DividendHistories'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable of client/adviser education records' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EducationRecords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable of client employment records' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentRecords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of exchanges' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Exchanges'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Table to store management expenses associated with assets/liabilities' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Expenses'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of all expense types to do with managing an asset/liability' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ExpenseTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of financial health grades' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FinancialHealthGrades'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable containing fund stocks information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FundStocks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of fund legal structures' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FundStructures'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of global categories' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GlobalCategories'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of global category groups' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GlobalCategoryGroups'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of industries.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Industries'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of investment types' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InvestmentTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data table of initial public offering by companies' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IPOs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of Morning Star Categories' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MorningStarCategories'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable containing information particular to MorningStar Ratings' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MorningStarRatings'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Linking table allowing correspondences to be linked to others' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoteLinks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data table related to correspondence between client and advisers' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table related to correspondencetypes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoteTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable containing information on insurance policies' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policies'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data table listing policy details (benefits/conditions) of an insurance policy
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PolicyDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup tables of product types to include in risk profiles' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable to store professional qualification/memberships' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Qualifications'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table to store qualification types' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'QualificationTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable of ratios entered' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Ratios'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of ratio types' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RatioTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable containing information of risk profiles for clients' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiskProfiles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of investment sectors' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sectors'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data table of stock distributions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StockDistributions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data table to store information on stock holdings for a particular client' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StockHoldings'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datatable containing information particular to stocks' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stocks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data table showing stock transactions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StockTransactions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table containing investment strategy types' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Strategies'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lookup table of possible transaction types' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TransactionTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Table of users in the database' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserData'
GO
USE [master]
GO
ALTER DATABASE [edisdatabase] SET  READ_WRITE 
GO

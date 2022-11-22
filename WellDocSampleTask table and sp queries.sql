

/****** Object:  Table [dbo].[Users]    Script Date: 11/22/2022 11:14:23 AM ******/
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Address] [nvarchar](max) NULL,
	[LastName] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
 PRIMARY KEY ([Id])
) 
GO


/****** Object:  StoredProcedure [dbo].[SPGetUsers]    Script Date: 11/22/2022 11:23:10 AM ******/
DROP PROCEDURE [dbo].[SPGetUsers]
GO
-- =============================================
-- Author:		Yellaiah
-- Create date: 18-Nov-2022
-- Description:	Get Users table information
-- =============================================
CREATE PROCEDURE [dbo].[SPGetUsers]  
(
@Id INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;
	IF @Id<>0
    BEGIN
		-- Select User details by Id
        SELECT Id,FirstName,Email,Mobile,Address,LastName,Password,IsActive from [dbo].[Users] where Id=@Id;
    END
    ELSE
    BEGIN
        -- Select All Users table data
		SELECT Id,FirstName,Email,Mobile,Address,LastName,Password,IsActive from [dbo].[Users];
    END
END
GO

/****** Object:  StoredProcedure [dbo].[SPValidateUser]    Script Date: 11/22/2022 11:24:57 AM ******/
DROP PROCEDURE [dbo].[SPValidateUser]
GO

/****** Object:  StoredProcedure [dbo].[SPValidateUser]    Script Date: 11/22/2022 11:24:57 AM ******/
-- =============================================
-- Author:		Yellaiah
-- Create date: 21-Nov-2022
-- Description:	Validate the User name and password
-- =============================================
CREATE PROCEDURE [dbo].[SPValidateUser] 
(
@Email NVARCHAR(100),
@Password NVARCHAR(100),
@ReturnCode NVARCHAR(20) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS (SELECT 1 FROM Users WHERE Email=@Email and Password=@Password)
    BEGIN
        SET @ReturnCode ='C200'
        RETURN
    END
    ELSE
    BEGIN
        SET @ReturnCode ='C201' -- User doesn't exists
        RETURN
    END
END
GO

/****** Object:  StoredProcedure [dbo].[SPSaveOrUpdateUser]    Script Date: 11/22/2022 11:25:31 AM ******/
DROP PROCEDURE [dbo].[SPSaveOrUpdateUser]
GO

/****** Object:  StoredProcedure [dbo].[SPSaveOrUpdateUser]    Script Date: 11/22/2022 11:25:31 AM ******/
-- =============================================
-- Author:		Yellaiah
-- Create date: 18-Nov-2022
-- Description:	Save or Update the Users table information
-- =============================================
CREATE PROCEDURE [dbo].[SPSaveOrUpdateUser] 
(
@Id INT,
@FirstName NVARCHAR(100),
@Email NVARCHAR(100),
@Mobile NVARCHAR(20)=NULL,
@Address NVARCHAR(MAX)=NULL,
@LastName NVARCHAR(100)=NULL,
@Password NVARCHAR(100),
@IsActive BIT,
@ReturnCode NVARCHAR(20) OUTPUT
)
AS
BEGIN
	SET @ReturnCode = 'C200'
	IF(@Id <> 0)
    BEGIN

		IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email AND Id <> @Id)
        BEGIN
            SET @ReturnCode = 'C201' -- user email already exists
            RETURN
        END
		IF EXISTS (SELECT 1 FROM Users WHERE Id = @Id)
        BEGIN
			UPDATE Users SET
			FirstName = @FirstName,
			Email = @Email,
			Mobile = @Mobile,
			Address = @Address,
			LastName=@LastName,
			Password=@Password,
			IsActive = 1
			WHERE Id = @Id;
			SET @ReturnCode = 'C200' -- done            
        END
		ELSE
		BEGIN
			SET @ReturnCode = 'C202' -- user id not exists
            RETURN
		END
        
    END
    ELSE
    BEGIN
		
		IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
        BEGIN
            SET @ReturnCode = 'C201' -- user already exists
            RETURN
        END

        INSERT INTO Users (FirstName,Email,Mobile,Address,LastName,Password,IsActive)
        VALUES (@FirstName,@Email,@Mobile,@Address,@LastName,@Password,1)

        SET @ReturnCode = 'C200' -- done
    END
END
GO

/****** Object:  StoredProcedure [dbo].[SPDeleteUser]    Script Date: 11/22/2022 11:26:24 AM ******/
DROP PROCEDURE [dbo].[SPDeleteUser]
GO

/****** Object:  StoredProcedure [dbo].[SPDeleteUser]    Script Date: 11/22/2022 11:26:24 AM ******/
-- =============================================
-- Author:		Yellaiah
-- Create date: 18-Nov-2022
-- Description:	Delete the User table information
-- =============================================
CREATE PROCEDURE [dbo].[SPDeleteUser] 
(
@Id INT,
@ReturnCode NVARCHAR(20) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
    SET @ReturnCode = 'C200'
    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @Id)
    BEGIN
        SET @ReturnCode ='C203' -- Id doesn't exists
        RETURN
    END
    ELSE
    BEGIN
        DELETE FROM Users WHERE Id = @Id
        SET @ReturnCode = 'C200'
        RETURN
    END
END
GO



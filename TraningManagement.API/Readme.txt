This application belongs to TraningFeedback API and responsible to generate all the required tables. 

USE master;
ALTER DATABASE [trainingmanagement] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [trainingmanagement] ;

/************************************************************Below commond only for knwoledge, please don't run************************************************************/
PM> Add-Migration InitialDbMigration -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration TopHeaderInformation_New_Table_Add -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration New_Table_And_Relation_Add -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Added_StartDate_EndDate_Instructions -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Added_New_Coloums_In_Subject -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Added_New_Coloums_In_Images -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Added_New_Coloums_In_Trainer -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Added_New_Coloums_Email_In_Trainer -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Added_New_Coloums_Subject_Trainer -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Added_PreCourseEnrollment_Answer_Table -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Added_New_Column_MaxAnswerTime -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Change_Column_Name_IsPreEnrollmentLinkVisited -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
PM> Add-Migration Added_ApplicationVersion -OutputDir Migrations/TrainingManagementDb -Context TrainingManagementDbContext -StartupProject TrainingManagement.API
/************************************************************Above commond only for knwoledge, please don't run************************************************************/

Please run the below commands in Package Manager Console to create the required tables.

/************************************************************Below commond use for migration run************************************************************/
Update-Database -Migration InitialDbMigration -Context TrainingManagementDbContext
Update-Database -Migration TopHeaderInformation_New_Table_Add -Context TrainingManagementDbContext
Update-Database -Migration ChangeDataType -Context TrainingManagementDbContext
Update-Database -Migration TraineeFeedback_Table_Modify -Context TrainingManagementDbContext
Update-Database -Migration Added_StartDate_EndDate_Instructions -Context TrainingManagementDbContext
Update-Database -Migration Added_New_Coloums_In_Subject -Context TrainingManagementDbContext
Update-Database -Migration Added_New_Coloums_In_Trainer -Context TrainingManagementDbContext
Update-Database -Migration Added_New_Coloums_Email_In_Trainer -Context TrainingManagementDbContext
Update-Database -Migration Added_PreCourseEnrollment_Answer_Table -Context TrainingManagementDbContext
Update-Database -Migration Added_New_Column_MaxAnswerTime -Context TrainingManagementDbContext
Update-Database -Migration Change_Column_Name_IsPreEnrollmentLinkVisited -Context TrainingManagementDbContext
/************************************************************Above commond use for migration run************************************************************/
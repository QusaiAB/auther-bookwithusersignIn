using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagment.Data.Migrations
{
    public partial class AddAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'01a7413e-a4dc-464f-89fe-510ec537eeaf', N'admin.test@hotmail.com', N'ADMIN.TEST@HOTMAIL.COM', N'admin.test@hotmail.com', N'ADMIN.TEST@HOTMAIL.COM', 0, N'AQAAAAEAACcQAAAAEM2E+ZCvMYuqBFn09zXf83oTYNArc41ogVGpx1A6m6xCH1D4mmh2Req+Y2YP8ctr7w==', N'PKI7P476RBSNVMVGOBF3ULHQ6BCKYJHA', N'7ebb71fc-fec9-4c07-9ad0-471913e77f24', NULL, 0, 0, NULL, 1, 0) " );


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELET FROM [dbo].[AspNetUsers] WHERE Id='01a7413e-a4dc-464f-89fe-510ec537eeaf' ");
        }
    }
}


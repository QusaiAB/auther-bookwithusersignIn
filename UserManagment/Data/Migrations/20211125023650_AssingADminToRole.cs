using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagment.Data.Migrations
{
    public partial class AssingADminToRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT  INTO AspNetUserRoles (UserId ,RoleId) SELECT '01a7413e-a4dc-464f-89fe-510ec537eeaf',Id FROM AspNetRoles ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELET FROM AspNetUserRoles WHERE UserId='01a7413e-a4dc-464f-89fe-510ec537eeaf' ");
        }
    }
}

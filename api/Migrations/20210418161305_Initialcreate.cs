using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace api.Migrations
{
    public partial class Initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UsersDetail",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Designation = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Contactno = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Dob = table.Column<string>(type: "text", nullable: true),
                    Birthplace = table.Column<string>(type: "text", nullable: true),
                    Publications = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Biodata = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdby = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDetail", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UsersDetailHistory",
                columns: table => new
                {
                    logdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Designation = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Contactno = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Dob = table.Column<string>(type: "text", nullable: true),
                    Birthplace = table.Column<string>(type: "text", nullable: true),
                    Publications = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Biodata = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdby = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDetailHistory", x => x.logdate);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UsersDetail");

            migrationBuilder.DropTable(
                name: "UsersDetailHistory");
        }
    }
}

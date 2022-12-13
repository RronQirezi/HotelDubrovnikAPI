using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDubrovnikAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventTitle = table.Column<string>(type: "varchar(max)", nullable: true),
                    EventDateFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventDateTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventImage = table.Column<string>(type: "varchar(max)", nullable: true),
                    EventDescription = table.Column<string>(type: "varchar(max)", nullable: true),
                    EventURL = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(name: "Room_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<int>(name: "Phone_Number", type: "int", nullable: false),
                    RoomNumber = table.Column<int>(name: "Room_Number", type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", nullable: false),
                    Booked = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(name: "Room_Type", type: "varchar(max)", nullable: false),
                    RoomPhoto = table.Column<string>(name: "Room_Photo", type: "varchar(max)", nullable: false),
                    RoomPrice = table.Column<int>(name: "Room_Price", type: "int", nullable: false),
                    RoomCapacity = table.Column<int>(name: "Room_Capacity", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}

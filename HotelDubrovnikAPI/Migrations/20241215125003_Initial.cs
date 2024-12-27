using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDubrovnikAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Eventid = table.Column<int>(name: "Event_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eventtitle = table.Column<string>(name: "Event_title", type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eventimg = table.Column<string>(name: "Event_img", type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eventurl = table.Column<string>(name: "Event_url", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Eventid);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Reservationid = table.Column<int>(name: "Reservation_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Roomid = table.Column<int>(name: "Room_id", type: "int", nullable: false),
                    Fromdate = table.Column<DateTime>(name: "From_date", type: "datetime2", nullable: false),
                    Todate = table.Column<DateTime>(name: "To_date", type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentificationNr = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Reservationid);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(name: "Room_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<int>(name: "Phone_Number", type: "int", nullable: false),
                    RoomNumber = table.Column<int>(name: "Room_Number", type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Booked = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(name: "Room_Type", type: "nvarchar(max)", nullable: false),
                    RoomPhoto = table.Column<string>(name: "Room_Photo", type: "nvarchar(max)", nullable: false),
                    RoomPrice = table.Column<int>(name: "Room_Price", type: "int", nullable: false),
                    RoomCapacity = table.Column<int>(name: "Room_Capacity", type: "int", nullable: false),
                    RoomStatus = table.Column<string>(name: "Room_Status", type: "nvarchar(max)", nullable: true)
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
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}

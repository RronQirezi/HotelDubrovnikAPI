using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelDubrovnikAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    eventid = table.Column<int>(name: "event_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eventtitle = table.Column<string>(name: "event_title", type: "nvarchar(max)", nullable: true),
                    fromdate = table.Column<DateTime>(name: "from_date", type: "datetime2", nullable: false),
                    todate = table.Column<DateTime>(name: "to_date", type: "datetime2", nullable: false),
                    eventimg = table.Column<string>(name: "event_img", type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eventurl = table.Column<string>(name: "event_url", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.eventid);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    reservationid = table.Column<int>(name: "reservation_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roomid = table.Column<int>(name: "room_id", type: "int", nullable: false),
                    fromdate = table.Column<DateTime>(name: "from_date", type: "datetime2", nullable: false),
                    todate = table.Column<DateTime>(name: "to_date", type: "datetime2", nullable: false),
                    payment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userid = table.Column<int>(name: "user_id", type: "int", nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "nvarchar(max)", nullable: false),
                    identification = table.Column<int>(type: "int", nullable: false),
                    phonenumber = table.Column<int>(name: "phone_number", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.reservationid);
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
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}

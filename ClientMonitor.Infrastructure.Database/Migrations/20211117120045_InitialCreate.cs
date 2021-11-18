using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientMonitor.Infrastructure.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECpus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BusyCpu = table.Column<double>(type: "REAL", nullable: false),
                    FreeCpu = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECpus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EDataForEdit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDataForEdit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EHttps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Length = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EHttps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EProcs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Process = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EProcs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ERams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BusyRam = table.Column<double>(type: "REAL", nullable: false),
                    FreeRam = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeLog = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 1, "Путь выгрузки файлов ~Выдача", "PathClaim", "C:\\Users\\Big Lolipop\\Desktop\\Записи с камер\\video\\ZLOSE" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 2, "Путь выгрузки файлов ~Склад", "PathStorage", "C:\\Users\\Big Lolipop\\Desktop\\Записи с камер\\video\\KMXLM" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 3, "Формат выгрузки файлов", "FormatFile", "*mp4" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 4, "Путь хранения файлов в облаке ~Выдача", "PathDownloadClaim", "Записи/Выдача" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 5, "Путь хранения файлов в облаке ~Склад", "PathDownloadStorage", "Записи/Склад" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 6, "Почта для входа в облако", "Mail", "afc.studio@yandex.ru" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 7, "Пароль для входа в облако", "Pas", "lollipop321123" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 8, "Время начала загрузки в облако~~Обновляется со следующей проверки!!!", "TimeCloud", "17.11.2021 20:00:00" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 9, "Время первой проверки мониторинга характеристик ПК~~Обновляется со следующей проверки!!!", "TimeFirst", "17.11.2021 6:00:00" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 10, "Время второй проверки мониторинга характеристик ПК~~Обновляется со следующей проверки!!!", "TimeSecond", "17.11.2021 15:30:00" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 11, "Периодичность мониторинга сайтов/серверов", "PeriodMonitoring", "3600000" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 12, "Id чата в телеграме для отправки сообщений по мониторингу сайтов и серверов ", "IdChatServer", "-742266994" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 13, "Id чата в телеграме для отправки сообщений по мониторингу характеристик ПК", "IdChatMonitoring", "-693501604" });

            migrationBuilder.InsertData(
                table: "EDataForEdit",
                columns: new[] { "Id", "Date", "Name", "Value" },
                values: new object[] { 14, "Проверка для остановки/запуска приложения", "onOff", "0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ECpus");

            migrationBuilder.DropTable(
                name: "EDataForEdit");

            migrationBuilder.DropTable(
                name: "EHttps");

            migrationBuilder.DropTable(
                name: "EProcs");

            migrationBuilder.DropTable(
                name: "ERams");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}

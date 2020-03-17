using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations {
    public partial class population : Migration {
        protected override void Up (MigrationBuilder mb) {
            mb.Sql ("INSERT INTO Authors(Name, Age) VALUES ('Tim Maia', 46)");
            mb.Sql ("INSERT INTO Musics(Name, Year, AuthorId) VALUES ('Eu Amo Você', 1970, 1)");
        }
        protected override void Down (MigrationBuilder mb) {
            mb.Sql ("DELETE FROM AUTHORS");
            mb.Sql ("DELETE FROM MUSICS");
        }
    }
}
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bionuclear.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColaCorreos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    correo_electronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    enviado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColaCorreos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LinksResultados",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_expediente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre_documento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinksResultados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Resultados",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre_paciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    correo_electroncio_paciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha_registro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre_doctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sexo_paciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numero_expediente = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    correo_electronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre_completo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipo_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColaCorreos");

            migrationBuilder.DropTable(
                name: "LinksResultados");

            migrationBuilder.DropTable(
                name: "Resultados");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}

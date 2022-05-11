using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymSysM.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    idActividad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad", x => x.idActividad);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaEmpleado",
                columns: table => new
                {
                    idCategoriaEmpleado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(maxLength: 50, nullable: false),
                    salarioHora = table.Column<decimal>(type: "money", nullable: false),
                    salarioBase = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaEmpleado_1", x => x.idCategoriaEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    idSala = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(maxLength: 50, nullable: false),
                    capacidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.idSala);
                });

            migrationBuilder.CreateTable(
                name: "Subscripcion",
                columns: table => new
                {
                    idSubscripcion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(maxLength: 50, nullable: false),
                    meses = table.Column<int>(nullable: false),
                    tarifa = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    descripcion = table.Column<string>(maxLength: 50, nullable: false),
                    cantidadSesionesUVA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscripcion", x => x.idSubscripcion);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    idEmpleado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(maxLength: 50, nullable: false),
                    nombre = table.Column<string>(maxLength: 50, nullable: false),
                    apellidos = table.Column<string>(maxLength: 50, nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    correo = table.Column<string>(type: "xml", nullable: true),
                    telefono = table.Column<int>(nullable: false),
                    direccion = table.Column<string>(maxLength: 50, nullable: false),
                    idCategoriaEmpleado = table.Column<int>(nullable: false),
                    nSeguroSocial = table.Column<string>(maxLength: 50, nullable: false),
                    cuentaIBAN = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.idEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleado_CategoriaEmpleado",
                        column: x => x.idCategoriaEmpleado,
                        principalTable: "CategoriaEmpleado",
                        principalColumn: "idCategoriaEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    idCliente = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(maxLength: 50, nullable: false),
                    nombre = table.Column<string>(maxLength: 50, nullable: false),
                    apellidos = table.Column<string>(maxLength: 50, nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    telefono = table.Column<string>(maxLength: 50, nullable: false),
                    direccion = table.Column<string>(maxLength: 50, nullable: false),
                    correo = table.Column<string>(type: "xml", nullable: false),
                    estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    idSubscripcion = table.Column<int>(nullable: false),
                    fechaSubscripcion = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(sysdatetime())"),
                    fechaRenovacion = table.Column<DateTime>(type: "date", nullable: true),
                    cantSesionesUVA = table.Column<int>(nullable: false),
                    sesionesUVAdisp = table.Column<int>(type: "int", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.idCliente);
                    table.ForeignKey(
                        name: "FK_Cliente_Subscripcion",
                        column: x => x.idSubscripcion,
                        principalTable: "Subscripcion",
                        principalColumn: "idSubscripcion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clase",
                columns: table => new
                {
                    idClase = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idActividad = table.Column<int>(nullable: false),
                    idSala = table.Column<int>(nullable: false),
                    idEmpleado = table.Column<int>(nullable: false),
                    dia = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    horaInicio = table.Column<TimeSpan>(nullable: false),
                    horaFin = table.Column<TimeSpan>(nullable: false),
                    capacidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clase", x => x.idClase);
                    table.ForeignKey(
                        name: "FK_Clase_Actividad",
                        column: x => x.idActividad,
                        principalTable: "Actividad",
                        principalColumn: "idActividad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clase_Empleado",
                        column: x => x.idEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "idEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clase_Sala",
                        column: x => x.idSala,
                        principalTable: "Sala",
                        principalColumn: "idSala",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planilla",
                columns: table => new
                {
                    idPlanilla = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpleado = table.Column<int>(nullable: false),
                    horasTrabajadas = table.Column<int>(nullable: false, defaultValueSql: "((8))"),
                    salarioHora = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    salarioBruto = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    salarioNeto = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    CCSS = table.Column<decimal>(type: "decimal(18, 0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planilla", x => x.idPlanilla);
                    table.ForeignKey(
                        name: "FK_Planilla_Empleado",
                        column: x => x.idEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "idEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SesionUVA",
                columns: table => new
                {
                    idSesionUVA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "date", nullable: false),
                    horaInicio = table.Column<TimeSpan>(nullable: false),
                    duracion = table.Column<TimeSpan>(nullable: false),
                    tarifa = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    idCliente = table.Column<int>(nullable: false),
                    idEmpleado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionUVA", x => x.idSesionUVA);
                    table.ForeignKey(
                        name: "FK_SesionUVA_Cliente",
                        column: x => x.idCliente,
                        principalTable: "Cliente",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    idMatricula = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCliente = table.Column<int>(nullable: false),
                    idClase = table.Column<int>(nullable: false),
                    fechaHora = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => x.idMatricula);
                    table.ForeignKey(
                        name: "FK_Matricula_Clase",
                        column: x => x.idClase,
                        principalTable: "Clase",
                        principalColumn: "idClase",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matricula_Cliente",
                        column: x => x.idCliente,
                        principalTable: "Cliente",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clase_idActividad",
                table: "Clase",
                column: "idActividad");

            migrationBuilder.CreateIndex(
                name: "IX_Clase_idEmpleado",
                table: "Clase",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Clase_idSala",
                table: "Clase",
                column: "idSala");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_idSubscripcion",
                table: "Cliente",
                column: "idSubscripcion");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_idCategoriaEmpleado",
                table: "Empleado",
                column: "idCategoriaEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_idClase",
                table: "Matricula",
                column: "idClase");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_idCliente",
                table: "Matricula",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Planilla_idEmpleado",
                table: "Planilla",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SesionUVA_idCliente",
                table: "SesionUVA",
                column: "idCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "Planilla");

            migrationBuilder.DropTable(
                name: "SesionUVA");

            migrationBuilder.DropTable(
                name: "Clase");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Subscripcion");

            migrationBuilder.DropTable(
                name: "CategoriaEmpleado");
        }
    }
}

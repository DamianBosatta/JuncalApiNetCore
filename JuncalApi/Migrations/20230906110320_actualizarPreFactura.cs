using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuncalApi.Migrations
{
    /// <inheritdoc />
    public partial class actualizarPreFactura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "latin1");

            migrationBuilder.CreateTable(
                name: "juncal.aceria",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cuit = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'"),
                    Codproveedor = table.Column<string>(name: "Cod_proveedor", type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "juncal.chofer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dni = table.Column<int>(type: "int(11)", nullable: false),
                    telefono = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "juncal.cuentas_corrientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idprovedoor = table.Column<int>(name: "id_provedoor", type: "int(5)", nullable: false),
                    idtipomoviento = table.Column<int>(name: "id_tipo_moviento", type: "int(2)", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    hora = table.Column<TimeOnly>(type: "time", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    importe = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    idusuario = table.Column<int>(name: "id_usuario", type: "int(5)", nullable: false),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.cuentas_corrientes_tipos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    tipo = table.Column<int>(type: "int(1)", nullable: false, comment: "1- Suma 2 - Resta"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.estados",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.estados_internos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.estados_reclamo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    isdelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.excel_config",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdAceria = table.Column<int>(name: "Id_Aceria", type: "int(10)", nullable: false),
                    Remito = table.Column<int>(type: "int(2)", nullable: false),
                    Fecha = table.Column<int>(type: "int(2)", nullable: false),
                    MaterialNombre = table.Column<int>(name: "Material_Nombre", type: "int(2)", nullable: false),
                    MaterialCodigo = table.Column<int>(name: "Material_Codigo", type: "int(2)", nullable: false),
                    Bruto = table.Column<int>(type: "int(2)", nullable: false),
                    Tara = table.Column<int>(type: "int(2)", nullable: false),
                    Descuento = table.Column<int>(type: "int(2)", nullable: false),
                    DescuentoDetalle = table.Column<int>(name: "Descuento_Detalle", type: "int(2)", nullable: false),
                    Neto = table.Column<int>(type: "int(2)", nullable: false),
                    configRemitoDesde = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    configRemitoCantidad = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    configMaterialCantidad = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    configMaterialHasta = table.Column<sbyte>(type: "tinyint(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.factura",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Destinatario = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Direccion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    cuit = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    ContratoNumero = table.Column<string>(name: "Contrato_Numero", type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    ContratoNombre = table.Column<string>(name: "Contrato_Nombre", type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    NumeroFactura = table.Column<string>(name: "Numero_Factura", type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Fecha = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    TotalFactura = table.Column<decimal>(name: "Total_Factura", type: "decimal(10)", precision: 10, nullable: false),
                    NombreUsuario = table.Column<string>(name: "Nombre_Usuario", type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.material",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "juncal.proveedor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    origen = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.sucursal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    numero = table.Column<int>(type: "int(11)", nullable: false),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.tipo_acoplado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.tipo_camion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.transportista",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cuit = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "juncal.contrato",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechavigencia = table.Column<DateTime>(name: "fecha_vigencia", type: "datetime(6)", nullable: true),
                    fechavencimiento = table.Column<DateTime>(name: "fecha_vencimiento", type: "datetime(6)", nullable: true),
                    idaceria = table.Column<int>(name: "id_aceria", type: "int(11)", nullable: true),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    valorFlete = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    tipo = table.Column<int>(type: "int(2)", nullable: false, defaultValueSql: "'1'", comment: "1-CIF 2-FOB")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_contrato_aceria",
                        column: x => x.idaceria,
                        principalTable: "juncal.aceria",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "juncal.Factura_Materiales",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idFactura = table.Column<int>(name: "id_Factura", type: "int(11)", nullable: false),
                    nombreMaterial = table.Column<string>(name: "nombre_Material", type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Peso = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    subTota = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_FacturaMateriales_Factura",
                        column: x => x.idFactura,
                        principalTable: "juncal.factura",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.aceria_material",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idaceria = table.Column<int>(name: "id_aceria", type: "int(11)", nullable: false),
                    idmaterial = table.Column<int>(name: "id_material", type: "int(11)", nullable: false),
                    cod = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_aceria_materiales_aceria",
                        column: x => x.idaceria,
                        principalTable: "juncal.aceria",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_aceria_materiales_materiales",
                        column: x => x.idmaterial,
                        principalTable: "juncal.material",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "juncal.direccion_proveedor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    direccion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    isdelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    idProveedor = table.Column<int>(type: "int(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_idproveedor",
                        column: x => x.idProveedor,
                        principalTable: "juncal.proveedor",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.material_proveedor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idmaterial = table.Column<int>(name: "id_material", type: "int(11)", nullable: false),
                    idproveedor = table.Column<int>(name: "id_proveedor", type: "int(11)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_material_proveedor_material",
                        column: x => x.idmaterial,
                        principalTable: "juncal.material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_material_proveedor_proveedor",
                        column: x => x.idproveedor,
                        principalTable: "juncal.proveedor",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usuario = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    apellido = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    dni = table.Column<int>(type: "int(11)", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    passwordHASH = table.Column<byte[]>(type: "blob", nullable: false),
                    idrol = table.Column<int>(name: "id_rol", type: "int(11)", nullable: true),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    passwordSALT = table.Column<byte[]>(type: "blob", nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    TokenCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    TokenExpires = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuario_roles",
                        column: x => x.idrol,
                        principalTable: "juncal.roles",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.acoplado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    patente = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    marca = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    año = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    idTipo = table.Column<int>(name: "id_Tipo", type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "juncal.acoplado_ibfk_1",
                        column: x => x.idTipo,
                        principalTable: "juncal.tipo_acoplado",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.camion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    patente = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    marca = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tara = table.Column<int>(type: "int(11)", nullable: true),
                    idchofer = table.Column<int>(name: "id_chofer", type: "int(11)", nullable: true),
                    idtransportista = table.Column<int>(name: "id_transportista", type: "int(11)", nullable: true),
                    idinterno = table.Column<int>(name: "id_interno", type: "int(11)", nullable: true),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'"),
                    idtipoCamion = table.Column<int>(name: "id_tipoCamion", type: "int(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_camion_chofer",
                        column: x => x.idchofer,
                        principalTable: "juncal.chofer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_camion_transportista",
                        column: x => x.idtransportista,
                        principalTable: "juncal.transportista",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_tipoCamion",
                        column: x => x.idtipoCamion,
                        principalTable: "juncal.tipo_camion",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "juncal.contrato_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idcontrato = table.Column<int>(name: "id_contrato", type: "int(11)", nullable: false),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    idmaterial = table.Column<int>(name: "id_material", type: "int(200)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_contrato_items_contrato",
                        column: x => x.idcontrato,
                        principalTable: "juncal.contrato",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_material",
                        column: x => x.idmaterial,
                        principalTable: "juncal.aceria_material",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.proveedor_presupuesto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    fechaactualizacion = table.Column<DateTime>(name: "fecha_actualizacion", type: "datetime(6)", nullable: false),
                    isDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    idProveedor = table.Column<int>(type: "int(11)", nullable: true),
                    idAceria = table.Column<int>(type: "int(11)", nullable: true),
                    idUsuario = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "idAceria_Aceria",
                        column: x => x.idAceria,
                        principalTable: "juncal.aceria",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "idProveedor_proveedor",
                        column: x => x.idProveedor,
                        principalTable: "juncal.proveedor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "idUsuario_Usuario",
                        column: x => x.idUsuario,
                        principalTable: "juncal.usuario",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.orden",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idaceria = table.Column<int>(name: "id_aceria", type: "int(11)", nullable: false),
                    idcontrato = table.Column<int>(name: "id_contrato", type: "int(11)", nullable: true),
                    remito = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    idcamion = table.Column<int>(name: "id_camion", type: "int(11)", nullable: true),
                    idestado = table.Column<int>(name: "id_estado", type: "int(11)", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'"),
                    idproveedor = table.Column<int>(name: "id_proveedor", type: "int(200)", nullable: true),
                    idacoplado = table.Column<int>(name: "id_acoplado", type: "int(200)", nullable: true),
                    Observaciones = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    iddireccionproveedor = table.Column<int>(name: "id_direccion_proveedor", type: "int(200)", nullable: true),
                    Facturado = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'"),
                    fechafacturacion = table.Column<DateTime>(name: "fecha_facturacion", type: "datetime(6)", nullable: true),
                    idUsuariocreacion = table.Column<int>(name: "id_Usuario_creacion", type: "int(11)", nullable: true),
                    idUsuariofacturacion = table.Column<int>(name: "id_Usuario_facturacion", type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_direccion_proveedor",
                        column: x => x.iddireccionproveedor,
                        principalTable: "juncal.direccion_proveedor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_acoplado",
                        column: x => x.idacoplado,
                        principalTable: "juncal.acoplado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_proveedor",
                        column: x => x.idproveedor,
                        principalTable: "juncal.proveedor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orden_aceria",
                        column: x => x.idaceria,
                        principalTable: "juncal.aceria",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orden_camion",
                        column: x => x.idcamion,
                        principalTable: "juncal.camion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orden_contrato",
                        column: x => x.idcontrato,
                        principalTable: "juncal.contrato",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orden_estados",
                        column: x => x.idestado,
                        principalTable: "juncal.estados",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_usuariocreacion",
                        column: x => x.idUsuariocreacion,
                        principalTable: "juncal.usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_usuariofacturacion",
                        column: x => x.idUsuariofacturacion,
                        principalTable: "juncal.usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.orden_interno",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idaceria = table.Column<int>(name: "id_aceria", type: "int(11)", nullable: false),
                    idcontrato = table.Column<int>(name: "id_contrato", type: "int(11)", nullable: true),
                    remito = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    idcamion = table.Column<int>(name: "id_camion", type: "int(11)", nullable: true),
                    idestadointerno = table.Column<int>(name: "id_estado_interno", type: "int(11)", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Observaciones = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    iddireccionproveedor = table.Column<int>(name: "id_direccion_proveedor", type: "int(11)", nullable: true),
                    idproveedor = table.Column<int>(name: "id_proveedor", type: "int(11)", nullable: true),
                    idacoplado = table.Column<int>(name: "id_acoplado", type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_id_aceria",
                        column: x => x.idaceria,
                        principalTable: "juncal.aceria",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_acoplado_interno",
                        column: x => x.idacoplado,
                        principalTable: "juncal.acoplado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_camion",
                        column: x => x.idcamion,
                        principalTable: "juncal.camion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_contrato",
                        column: x => x.idcontrato,
                        principalTable: "juncal.contrato",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_direccion_proveedor",
                        column: x => x.iddireccionproveedor,
                        principalTable: "juncal.direccion_proveedor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_estado_interno",
                        column: x => x.idestadointerno,
                        principalTable: "juncal.estados_internos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_proveedor_interno",
                        column: x => x.idproveedor,
                        principalTable: "juncal.proveedor",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.proveedor_presupuesto_materiales",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    idPresupuesto = table.Column<int>(type: "int(11)", nullable: false),
                    idMaterial = table.Column<int>(type: "int(11)", nullable: false),
                    precioCIF = table.Column<double>(name: "precio_CIF", type: "double", nullable: false),
                    precioFOB = table.Column<double>(name: "precio_FOB", type: "double", nullable: false),
                    isdeleted = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "idMaterial_Material",
                        column: x => x.idMaterial,
                        principalTable: "juncal.material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "idPresupuesto_presupuesto",
                        column: x => x.idPresupuesto,
                        principalTable: "juncal.proveedor_presupuesto",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.orden_marterial",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idorden = table.Column<int>(name: "id_orden", type: "int(11)", nullable: false),
                    idmaterial = table.Column<int>(name: "id_material", type: "int(11)", nullable: false),
                    peso = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: true),
                    numFactura = table.Column<string>(name: "num_Factura", type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    facturadoParcial = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaFacturado = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_orden_marterial_material",
                        column: x => x.idmaterial,
                        principalTable: "juncal.material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orden_marterial_orden",
                        column: x => x.idorden,
                        principalTable: "juncal.orden",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.remitos_reclamados",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idEstadoReclamo = table.Column<int>(type: "int(11)", nullable: false),
                    idRemito = table.Column<int>(type: "int(11)", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    observacion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    fechareclamo = table.Column<DateTime>(name: "fecha_reclamo", type: "datetime(6)", nullable: true),
                    observacionreclamo = table.Column<string>(name: "observacion_reclamo", type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    fechafinalizado = table.Column<DateTime>(name: "fecha_finalizado", type: "datetime(6)", nullable: true),
                    observacionfinalizado = table.Column<string>(name: "observacion_finalizado", type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    idUsuarioreclamo = table.Column<int>(name: "id_Usuario_reclamo", type: "int(11)", nullable: true),
                    isDeleted = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    idusuarioingreso = table.Column<int>(name: "id_usuario_ingreso", type: "int(11)", nullable: true),
                    idusuariofinalizado = table.Column<int>(name: "id_usuario_finalizado", type: "int(11)", nullable: true),
                    idAceria = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_Aceria",
                        column: x => x.idAceria,
                        principalTable: "juncal.aceria",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_UsuarioReclamo",
                        column: x => x.idUsuarioreclamo,
                        principalTable: "juncal.usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_remito",
                        column: x => x.idRemito,
                        principalTable: "juncal.orden",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_usuarioFinalizado",
                        column: x => x.idusuariofinalizado,
                        principalTable: "juncal.usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_usuarioIngres",
                        column: x => x.idusuarioingreso,
                        principalTable: "juncal.usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "remitos_reclamados_Estados",
                        column: x => x.idEstadoReclamo,
                        principalTable: "juncal.estados_reclamo",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.orden_material_interno_recibido",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idordeninterno = table.Column<int>(name: "id_orden_interno", type: "int(11)", nullable: false),
                    idmaterial = table.Column<int>(name: "id_material", type: "int(11)", nullable: false),
                    peso = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_id_material_interno_recibido",
                        column: x => x.idmaterial,
                        principalTable: "juncal.material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_orden_interno_recibido",
                        column: x => x.idordeninterno,
                        principalTable: "juncal.orden_interno",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.orden_material_interno_recogido",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idordeninterno = table.Column<int>(name: "id_orden_interno", type: "int(11)", nullable: false),
                    idmaterial = table.Column<int>(name: "id_material", type: "int(11)", nullable: false),
                    peso = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_id_material_interno",
                        column: x => x.idmaterial,
                        principalTable: "juncal.material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_id_orden_interno",
                        column: x => x.idordeninterno,
                        principalTable: "juncal.orden_interno",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "juncal.preFacturar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idorden = table.Column<int>(name: "id_orden", type: "int(11)", nullable: false),
                    idmaterialenviado = table.Column<int>(name: "id_material_enviado", type: "int(11)", nullable: false),
                    idmaterialrecibido = table.Column<int>(name: "id_material_recibido", type: "int(11)", nullable: false),
                    peso = table.Column<decimal>(type: "decimal(11)", precision: 11, nullable: false),
                    pesotara = table.Column<decimal>(name: "peso_tara", type: "decimal(11)", precision: 11, nullable: false),
                    pesobruto = table.Column<decimal>(name: "peso_bruto", type: "decimal(11)", precision: 11, nullable: false),
                    pesoneto = table.Column<decimal>(name: "peso_neto", type: "decimal(11)", precision: 11, nullable: false),
                    facturado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    remito = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    idUsuarioFacturacion = table.Column<int>(type: "int(11)", nullable: true),
                    FechaFacturado = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_MaterialEnviado_OrdenMaterial",
                        column: x => x.idmaterialenviado,
                        principalTable: "juncal.orden_marterial",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_MaterialRecibido_MaterialAceria",
                        column: x => x.idmaterialrecibido,
                        principalTable: "juncal.aceria_material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_idOrden_orden",
                        column: x => x.idorden,
                        principalTable: "juncal.orden",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_idUsuarioFacturacion_Usuario",
                        column: x => x.idUsuarioFacturacion,
                        principalTable: "juncal.usuario",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateIndex(
                name: "fk_aceria_materiales_aceria",
                table: "juncal.aceria_material",
                column: "id_aceria");

            migrationBuilder.CreateIndex(
                name: "fk_aceria_materiales_materiales",
                table: "juncal.aceria_material",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "id_Tipo",
                table: "juncal.acoplado",
                column: "id_Tipo");

            migrationBuilder.CreateIndex(
                name: "fk_camion_chofer",
                table: "juncal.camion",
                column: "id_chofer");

            migrationBuilder.CreateIndex(
                name: "fk_camion_transportista",
                table: "juncal.camion",
                column: "id_transportista");

            migrationBuilder.CreateIndex(
                name: "fk_id_tipoCamion",
                table: "juncal.camion",
                column: "id_tipoCamion");

            migrationBuilder.CreateIndex(
                name: "fk_contrato_aceria",
                table: "juncal.contrato",
                column: "id_aceria");

            migrationBuilder.CreateIndex(
                name: "fk_contrato_items_contrato",
                table: "juncal.contrato_items",
                column: "id_contrato");

            migrationBuilder.CreateIndex(
                name: "fk_id_material",
                table: "juncal.contrato_items",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "fk_idproveedor",
                table: "juncal.direccion_proveedor",
                column: "idProveedor");

            migrationBuilder.CreateIndex(
                name: "fk_FacturaMateriales_Factura",
                table: "juncal.Factura_Materiales",
                column: "id_Factura");

            migrationBuilder.CreateIndex(
                name: "fk_material_proveedor_material",
                table: "juncal.material_proveedor",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "fk_material_proveedor_proveedor",
                table: "juncal.material_proveedor",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "fk_direccion_proveedor",
                table: "juncal.orden",
                column: "id_direccion_proveedor");

            migrationBuilder.CreateIndex(
                name: "fk_id_acoplado",
                table: "juncal.orden",
                column: "id_acoplado");

            migrationBuilder.CreateIndex(
                name: "fk_id_proveedor",
                table: "juncal.orden",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "fk_orden_aceria",
                table: "juncal.orden",
                column: "id_aceria");

            migrationBuilder.CreateIndex(
                name: "fk_orden_camion",
                table: "juncal.orden",
                column: "id_camion");

            migrationBuilder.CreateIndex(
                name: "fk_orden_contrato",
                table: "juncal.orden",
                column: "id_contrato");

            migrationBuilder.CreateIndex(
                name: "fk_orden_estados",
                table: "juncal.orden",
                column: "id_estado");

            migrationBuilder.CreateIndex(
                name: "fk_usuariocreacion",
                table: "juncal.orden",
                column: "id_Usuario_creacion");

            migrationBuilder.CreateIndex(
                name: "fk_usuariofacturacion",
                table: "juncal.orden",
                column: "id_Usuario_facturacion");

            migrationBuilder.CreateIndex(
                name: "fk_id_aceria",
                table: "juncal.orden_interno",
                column: "id_aceria");

            migrationBuilder.CreateIndex(
                name: "fk_id_acoplado_interno",
                table: "juncal.orden_interno",
                column: "id_acoplado");

            migrationBuilder.CreateIndex(
                name: "fk_id_camion",
                table: "juncal.orden_interno",
                column: "id_camion");

            migrationBuilder.CreateIndex(
                name: "fk_id_contrato",
                table: "juncal.orden_interno",
                column: "id_contrato");

            migrationBuilder.CreateIndex(
                name: "fk_id_direccion_proveedor",
                table: "juncal.orden_interno",
                column: "id_direccion_proveedor");

            migrationBuilder.CreateIndex(
                name: "fk_id_estado_interno",
                table: "juncal.orden_interno",
                column: "id_estado_interno");

            migrationBuilder.CreateIndex(
                name: "fk_id_proveedor_interno",
                table: "juncal.orden_interno",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "fk_orden_marterial_material",
                table: "juncal.orden_marterial",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "fk_orden_marterial_orden",
                table: "juncal.orden_marterial",
                column: "id_orden");

            migrationBuilder.CreateIndex(
                name: "fk_id_material_interno_recibido",
                table: "juncal.orden_material_interno_recibido",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "fk_id_orden_interno_recibido",
                table: "juncal.orden_material_interno_recibido",
                column: "id_orden_interno");

            migrationBuilder.CreateIndex(
                name: "fk_id_material_interno",
                table: "juncal.orden_material_interno_recogido",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "fk_id_orden_interno",
                table: "juncal.orden_material_interno_recogido",
                column: "id_orden_interno");

            migrationBuilder.CreateIndex(
                name: "fk_idOrden_orden",
                table: "juncal.preFacturar",
                column: "id_orden");

            migrationBuilder.CreateIndex(
                name: "fk_idUsuarioFacturacion_Usuario",
                table: "juncal.preFacturar",
                column: "idUsuarioFacturacion");

            migrationBuilder.CreateIndex(
                name: "fk_MaterialEnviado_OrdenMaterial",
                table: "juncal.preFacturar",
                column: "id_material_enviado");

            migrationBuilder.CreateIndex(
                name: "fk_MaterialRecibido_MaterialAceria",
                table: "juncal.preFacturar",
                column: "id_material_recibido");

            migrationBuilder.CreateIndex(
                name: "idAceria_Aceria",
                table: "juncal.proveedor_presupuesto",
                column: "idAceria");

            migrationBuilder.CreateIndex(
                name: "idProveedor_proveedor",
                table: "juncal.proveedor_presupuesto",
                column: "idProveedor");

            migrationBuilder.CreateIndex(
                name: "idUsuario_Usuario",
                table: "juncal.proveedor_presupuesto",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "idMaterial_Material",
                table: "juncal.proveedor_presupuesto_materiales",
                column: "idMaterial");

            migrationBuilder.CreateIndex(
                name: "idPresupuesto_presupuesto",
                table: "juncal.proveedor_presupuesto_materiales",
                column: "idPresupuesto");

            migrationBuilder.CreateIndex(
                name: "fk_Aceria",
                table: "juncal.remitos_reclamados",
                column: "idAceria");

            migrationBuilder.CreateIndex(
                name: "fk_remito",
                table: "juncal.remitos_reclamados",
                column: "idRemito");

            migrationBuilder.CreateIndex(
                name: "fk_usuarioFinalizado",
                table: "juncal.remitos_reclamados",
                column: "id_usuario_finalizado");

            migrationBuilder.CreateIndex(
                name: "fk_usuarioIngres",
                table: "juncal.remitos_reclamados",
                column: "id_usuario_ingreso");

            migrationBuilder.CreateIndex(
                name: "fk_UsuarioReclamo",
                table: "juncal.remitos_reclamados",
                column: "id_Usuario_reclamo");

            migrationBuilder.CreateIndex(
                name: "remitos_reclamados_Estados",
                table: "juncal.remitos_reclamados",
                column: "idEstadoReclamo");

            migrationBuilder.CreateIndex(
                name: "unq_roles_id",
                table: "juncal.roles",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_usuario_roles",
                table: "juncal.usuario",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "juncal.contrato_items");

            migrationBuilder.DropTable(
                name: "juncal.cuentas_corrientes");

            migrationBuilder.DropTable(
                name: "juncal.cuentas_corrientes_tipos");

            migrationBuilder.DropTable(
                name: "juncal.excel_config");

            migrationBuilder.DropTable(
                name: "juncal.Factura_Materiales");

            migrationBuilder.DropTable(
                name: "juncal.material_proveedor");

            migrationBuilder.DropTable(
                name: "juncal.orden_material_interno_recibido");

            migrationBuilder.DropTable(
                name: "juncal.orden_material_interno_recogido");

            migrationBuilder.DropTable(
                name: "juncal.preFacturar");

            migrationBuilder.DropTable(
                name: "juncal.proveedor_presupuesto_materiales");

            migrationBuilder.DropTable(
                name: "juncal.remitos_reclamados");

            migrationBuilder.DropTable(
                name: "juncal.sucursal");

            migrationBuilder.DropTable(
                name: "juncal.factura");

            migrationBuilder.DropTable(
                name: "juncal.orden_interno");

            migrationBuilder.DropTable(
                name: "juncal.orden_marterial");

            migrationBuilder.DropTable(
                name: "juncal.aceria_material");

            migrationBuilder.DropTable(
                name: "juncal.proveedor_presupuesto");

            migrationBuilder.DropTable(
                name: "juncal.estados_reclamo");

            migrationBuilder.DropTable(
                name: "juncal.estados_internos");

            migrationBuilder.DropTable(
                name: "juncal.orden");

            migrationBuilder.DropTable(
                name: "juncal.material");

            migrationBuilder.DropTable(
                name: "juncal.direccion_proveedor");

            migrationBuilder.DropTable(
                name: "juncal.acoplado");

            migrationBuilder.DropTable(
                name: "juncal.camion");

            migrationBuilder.DropTable(
                name: "juncal.contrato");

            migrationBuilder.DropTable(
                name: "juncal.estados");

            migrationBuilder.DropTable(
                name: "juncal.usuario");

            migrationBuilder.DropTable(
                name: "juncal.proveedor");

            migrationBuilder.DropTable(
                name: "juncal.tipo_acoplado");

            migrationBuilder.DropTable(
                name: "juncal.chofer");

            migrationBuilder.DropTable(
                name: "juncal.transportista");

            migrationBuilder.DropTable(
                name: "juncal.tipo_camion");

            migrationBuilder.DropTable(
                name: "juncal.aceria");

            migrationBuilder.DropTable(
                name: "juncal.roles");
        }
    }
}

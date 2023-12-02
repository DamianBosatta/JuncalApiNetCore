using System;
using System.Collections.Generic;
using JuncalApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace JuncalApi.DataBase;

public partial class JuncalContext : DbContext
{
    public JuncalContext()
    {
    }

    public JuncalContext(DbContextOptions<JuncalContext> options)
        : base(options)
    {
    }


    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<JuncalAceriaMaterial> JuncalAceriaMaterials { get; set; }

    public virtual DbSet<JuncalAcerium> JuncalAceria { get; set; }

    public virtual DbSet<JuncalAcoplado> JuncalAcoplados { get; set; }

    public virtual DbSet<JuncalCamion> JuncalCamions { get; set; }

    public virtual DbSet<JuncalCcMovimientoRemito> JuncalCcMovimientoRemitos { get; set; }

    public virtual DbSet<JuncalCcTiposMovimiento> JuncalCcTiposMovimientos { get; set; }

    public virtual DbSet<JuncalChofer> JuncalChofers { get; set; }

    public virtual DbSet<JuncalContrato> JuncalContratos { get; set; }

    public virtual DbSet<JuncalContratoItem> JuncalContratoItems { get; set; }

    public virtual DbSet<JuncalDireccionProveedor> JuncalDireccionProveedors { get; set; }

    public virtual DbSet<JuncalEstado> JuncalEstados { get; set; }

    public virtual DbSet<JuncalEstadosInterno> JuncalEstadosInternos { get; set; }

    public virtual DbSet<JuncalEstadosReclamo> JuncalEstadosReclamos { get; set; }

    public virtual DbSet<JuncalExcelConfig> JuncalExcelConfigs { get; set; }

    public virtual DbSet<JuncalFactura> JuncalFacturas { get; set; }

    public virtual DbSet<JuncalFacturaMateriale> JuncalFacturaMateriales { get; set; }

    public virtual DbSet<JuncalMaterial> JuncalMaterials { get; set; }

    public virtual DbSet<JuncalNotificacione> JuncalNotificaciones { get; set; }

    public virtual DbSet<JuncalOrden> JuncalOrdens { get; set; }

    public virtual DbSet<JuncalOrdenInterno> JuncalOrdenInternos { get; set; }

    public virtual DbSet<JuncalOrdenMarterial> JuncalOrdenMarterials { get; set; }

    public virtual DbSet<JuncalOrdenMaterialInternoRecibido> JuncalOrdenMaterialInternoRecibidos { get; set; }

    public virtual DbSet<JuncalOrdenMaterialInternoRecogido> JuncalOrdenMaterialInternoRecogidos { get; set; }

    public virtual DbSet<JuncalPreFacturar> JuncalPreFacturars { get; set; }

    public virtual DbSet<JuncalProveedor> JuncalProveedors { get; set; }

    public virtual DbSet<JuncalProveedorCuentaCorriente> JuncalProveedorCuentaCorrientes { get; set; }

    public virtual DbSet<JuncalProveedorListaprecio> JuncalProveedorListaprecios { get; set; }

    public virtual DbSet<JuncalProveedorListapreciosMateriale> JuncalProveedorListapreciosMateriales { get; set; }

    public virtual DbSet<JuncalRemitosReclamado> JuncalRemitosReclamados { get; set; }

    public virtual DbSet<JuncalRole> JuncalRoles { get; set; }

    public virtual DbSet<JuncalSucursal> JuncalSucursals { get; set; }

    public virtual DbSet<JuncalTipoAcoplado> JuncalTipoAcoplados { get; set; }

    public virtual DbSet<JuncalTipoCamion> JuncalTipoCamions { get; set; }

    public virtual DbSet<JuncalTransportistum> JuncalTransportista { get; set; }

    public virtual DbSet<JuncalUsuario> JuncalUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string? connectionString = configuration.GetConnectionString("JuncalApiDB");

            optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("5.7.30-mysql"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity
                .ToTable("__EFMigrationsHistory")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<JuncalAceriaMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.aceria_material")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdAceria, "fk_aceria_materiales_aceria");

            entity.HasIndex(e => e.IdMaterial, "fk_aceria_materiales_materiales");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cod)
                .HasMaxLength(255)
                .HasColumnName("cod");
            entity.Property(e => e.IdAceria)
                .HasColumnType("int(11)")
                .HasColumnName("id_aceria");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdAceriaNavigation).WithMany(p => p.JuncalAceriaMaterials)
                .HasForeignKey(d => d.IdAceria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_aceria_materiales_aceria");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.JuncalAceriaMaterials)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_aceria_materiales_materiales");
        });

        modelBuilder.Entity<JuncalAcerium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.aceria")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CodProveedor)
                .HasMaxLength(255)
                .HasColumnName("Cod_proveedor");
            entity.Property(e => e.Cuit)
                .HasMaxLength(255)
                .HasColumnName("cuit");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalAcoplado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.acoplado");

            entity.HasIndex(e => e.IdTipo, "id_Tipo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Año)
                .HasMaxLength(4)
                .HasColumnName("año");
            entity.Property(e => e.IdTipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_Tipo");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Marca)
                .HasMaxLength(255)
                .HasColumnName("marca");
            entity.Property(e => e.Patente)
                .HasMaxLength(255)
                .HasColumnName("patente");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.JuncalAcoplados)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("juncal.acoplado_ibfk_1");
        });

        modelBuilder.Entity<JuncalCamion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.camion")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdChofer, "fk_camion_chofer");

            entity.HasIndex(e => e.IdTransportista, "fk_camion_transportista");

            entity.HasIndex(e => e.IdTipoCamion, "fk_id_tipoCamion");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdChofer)
                .HasColumnType("int(11)")
                .HasColumnName("id_chofer");
            entity.Property(e => e.IdInterno)
                .HasColumnType("int(11)")
                .HasColumnName("id_interno");
            entity.Property(e => e.IdTipoCamion)
                .HasColumnType("int(200)")
                .HasColumnName("id_tipoCamion");
            entity.Property(e => e.IdTransportista)
                .HasColumnType("int(11)")
                .HasColumnName("id_transportista");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Marca)
                .HasMaxLength(255)
                .HasColumnName("marca");
            entity.Property(e => e.Patente)
                .HasMaxLength(255)
                .HasColumnName("patente");
            entity.Property(e => e.Tara)
                .HasColumnType("int(11)")
                .HasColumnName("tara");

            entity.HasOne(d => d.IdChoferNavigation).WithMany(p => p.JuncalCamions)
                .HasForeignKey(d => d.IdChofer)
                .HasConstraintName("fk_camion_chofer");

            entity.HasOne(d => d.IdTipoCamionNavigation).WithMany(p => p.JuncalCamions)
                .HasForeignKey(d => d.IdTipoCamion)
                .HasConstraintName("fk_id_tipoCamion");

            entity.HasOne(d => d.IdTransportistaNavigation).WithMany(p => p.JuncalCamions)
                .HasForeignKey(d => d.IdTransportista)
                .HasConstraintName("fk_camion_transportista");
        });

        modelBuilder.Entity<JuncalCcMovimientoRemito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.cc_movimiento_remito");

            entity.HasIndex(e => e.IdMovimiento, "fk_juncal.proveedor_cc_remitos_proveedor_cuentacorriente");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Finalizado)
                .HasPrecision(10)
                .HasColumnName("finalizado");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.IdMovimiento)
                .HasColumnType("int(11)")
                .HasColumnName("id_movimiento");
            entity.Property(e => e.IdRemito)
                .HasColumnType("int(11)")
                .HasColumnName("id_remito");
            entity.Property(e => e.Pesaje1)
                .HasPrecision(10)
                .HasColumnName("pesaje1");
            entity.Property(e => e.Pesaje2)
                .HasPrecision(10)
                .HasColumnName("pesaje2");

            entity.HasOne(d => d.IdMovimientoNavigation).WithMany(p => p.JuncalCcMovimientoRemitos)
                .HasForeignKey(d => d.IdMovimiento)
                .HasConstraintName("fk_juncal.proveedor_cc_remitos_proveedor_cuentacorriente");
        });

        modelBuilder.Entity<JuncalCcTiposMovimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.cc_tipos_movimientos");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<JuncalChofer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.chofer")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasColumnType("int(11)")
                .HasColumnName("dni");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<JuncalContrato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.contrato")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdAceria, "fk_contrato_aceria");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.FechaVigencia).HasColumnName("fecha_vigencia");
            entity.Property(e => e.IdAceria)
                .HasColumnType("int(11)")
                .HasColumnName("id_aceria");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .HasColumnName("numero");
            entity.Property(e => e.Tipo)
                .HasDefaultValueSql("'1'")
                .HasComment("1-CIF 2-FOB")
                .HasColumnType("int(2)")
                .HasColumnName("tipo");
            entity.Property(e => e.ValorFlete)
                .HasPrecision(10)
                .HasColumnName("valorFlete");

            entity.HasOne(d => d.IdAceriaNavigation).WithMany(p => p.JuncalContratos)
                .HasForeignKey(d => d.IdAceria)
                .HasConstraintName("fk_contrato_aceria");
        });

        modelBuilder.Entity<JuncalContratoItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.contrato_items");

            entity.HasIndex(e => e.IdContrato, "fk_contrato_items_contrato");

            entity.HasIndex(e => e.IdMaterial, "fk_id_material");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdContrato)
                .HasColumnType("int(11)")
                .HasColumnName("id_contrato");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(200)")
                .HasColumnName("id_material");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Precio)
                .HasPrecision(10)
                .HasColumnName("precio");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.JuncalContratoItems)
                .HasForeignKey(d => d.IdContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contrato_items_contrato");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.JuncalContratoItems)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_material");
        });

        modelBuilder.Entity<JuncalDireccionProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.direccion_proveedor");

            entity.HasIndex(e => e.IdProveedor, "fk_idproveedor");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.IdProveedor)
                .HasColumnType("int(200)")
                .HasColumnName("idProveedor");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.JuncalDireccionProveedors)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idproveedor");
        });

        modelBuilder.Entity<JuncalEstado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.estados");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalEstadosInterno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.estados_internos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalEstadosReclamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.estados_reclamo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalExcelConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.excel_config");

            entity.Property(e => e.Id).HasColumnType("int(10)");
            entity.Property(e => e.Bruto).HasColumnType("int(2)");
            entity.Property(e => e.ConfigMaterialCantidad)
                .HasColumnType("tinyint(4)")
                .HasColumnName("configMaterialCantidad");
            entity.Property(e => e.ConfigMaterialHasta)
                .HasColumnType("tinyint(4)")
                .HasColumnName("configMaterialHasta");
            entity.Property(e => e.ConfigRemitoCantidad)
                .HasColumnType("tinyint(4)")
                .HasColumnName("configRemitoCantidad");
            entity.Property(e => e.ConfigRemitoDesde)
                .HasColumnType("tinyint(4)")
                .HasColumnName("configRemitoDesde");
            entity.Property(e => e.Descuento).HasColumnType("int(2)");
            entity.Property(e => e.DescuentoDetalle)
                .HasColumnType("int(2)")
                .HasColumnName("Descuento_Detalle");
            entity.Property(e => e.Fecha).HasColumnType("int(2)");
            entity.Property(e => e.IdAceria)
                .HasColumnType("int(10)")
                .HasColumnName("Id_Aceria");
            entity.Property(e => e.MaterialCodigo)
                .HasColumnType("int(2)")
                .HasColumnName("Material_Codigo");
            entity.Property(e => e.MaterialNombre)
                .HasColumnType("int(2)")
                .HasColumnName("Material_Nombre");
            entity.Property(e => e.Neto).HasColumnType("int(2)");
            entity.Property(e => e.Remito).HasColumnType("int(2)");
            entity.Property(e => e.Tara).HasColumnType("int(2)");
        });

        modelBuilder.Entity<JuncalFactura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.factura");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ContratoNombre)
                .HasMaxLength(255)
                .HasColumnName("Contrato_Nombre");
            entity.Property(e => e.ContratoNumero)
                .HasMaxLength(255)
                .HasColumnName("Contrato_Numero");
            entity.Property(e => e.Cuit)
                .HasMaxLength(255)
                .HasColumnName("cuit");
            entity.Property(e => e.Destinatario).HasMaxLength(255);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Fecha).HasMaxLength(255);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(255)
                .HasColumnName("Nombre_Usuario");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(255)
                .HasColumnName("Numero_Factura");
            entity.Property(e => e.TotalFactura)
                .HasPrecision(10)
                .HasColumnName("Total_Factura");
        });

        modelBuilder.Entity<JuncalFacturaMateriale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.Factura_Materiales");

            entity.HasIndex(e => e.IdFactura, "fk_FacturaMateriales_Factura");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdFactura)
                .HasColumnType("int(11)")
                .HasColumnName("id_Factura");
            entity.Property(e => e.NombreMaterial)
                .HasMaxLength(255)
                .HasColumnName("nombre_Material");
            entity.Property(e => e.Peso).HasPrecision(10);
            entity.Property(e => e.SubTota)
                .HasPrecision(10)
                .HasColumnName("subTota");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.JuncalFacturaMateriales)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FacturaMateriales_Factura");
        });

        modelBuilder.Entity<JuncalMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.material")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalNotificacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.notificaciones");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CantidadContratos)
                .HasColumnType("int(11)")
                .HasColumnName("cantidadContratos");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
        });

        modelBuilder.Entity<JuncalOrden>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.orden");

            entity.HasIndex(e => e.IdDireccionProveedor, "fk_direccion_proveedor");

            entity.HasIndex(e => e.IdAcoplado, "fk_id_acoplado");

            entity.HasIndex(e => e.IdProveedor, "fk_id_proveedor");

            entity.HasIndex(e => e.IdAceria, "fk_orden_aceria");

            entity.HasIndex(e => e.IdCamion, "fk_orden_camion");

            entity.HasIndex(e => e.IdContrato, "fk_orden_contrato");

            entity.HasIndex(e => e.IdEstado, "fk_orden_estados");

            entity.HasIndex(e => e.IdUsuarioCreacion, "fk_usuariocreacion");

            entity.HasIndex(e => e.IdUsuarioFacturacion, "fk_usuariofacturacion");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Facturado).HasDefaultValueSql("'0'");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.FechaFacturacion).HasColumnName("fecha_facturacion");
            entity.Property(e => e.IdAceria)
                .HasColumnType("int(11)")
                .HasColumnName("id_aceria");
            entity.Property(e => e.IdAcoplado)
                .HasColumnType("int(200)")
                .HasColumnName("id_acoplado");
            entity.Property(e => e.IdCamion)
                .HasColumnType("int(11)")
                .HasColumnName("id_camion");
            entity.Property(e => e.IdContrato)
                .HasColumnType("int(11)")
                .HasColumnName("id_contrato");
            entity.Property(e => e.IdDireccionProveedor)
                .HasColumnType("int(200)")
                .HasColumnName("id_direccion_proveedor");
            entity.Property(e => e.IdEstado)
                .HasColumnType("int(11)")
                .HasColumnName("id_estado");
            entity.Property(e => e.IdProveedor)
                .HasColumnType("int(200)")
                .HasColumnName("id_proveedor");
            entity.Property(e => e.IdUsuarioCreacion)
                .HasColumnType("int(11)")
                .HasColumnName("id_Usuario_creacion");
            entity.Property(e => e.IdUsuarioFacturacion)
                .HasColumnType("int(11)")
                .HasColumnName("id_Usuario_facturacion");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Observaciones).HasMaxLength(255);
            entity.Property(e => e.Remito)
                .HasMaxLength(255)
                .HasColumnName("remito");

            entity.HasOne(d => d.IdAceriaNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdAceria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_aceria");

            entity.HasOne(d => d.IdAcopladoNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdAcoplado)
                .HasConstraintName("fk_id_acoplado");

            entity.HasOne(d => d.IdCamionNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdCamion)
                .HasConstraintName("fk_orden_camion");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdContrato)
                .HasConstraintName("fk_orden_contrato");

            entity.HasOne(d => d.IdDireccionProveedorNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdDireccionProveedor)
                .HasConstraintName("fk_direccion_proveedor");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_estados");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("fk_id_proveedor");

            entity.HasOne(d => d.IdUsuarioCreacionNavigation).WithMany(p => p.JuncalOrdenIdUsuarioCreacionNavigations)
                .HasForeignKey(d => d.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_usuariocreacion");

            entity.HasOne(d => d.IdUsuarioFacturacionNavigation).WithMany(p => p.JuncalOrdenIdUsuarioFacturacionNavigations)
                .HasForeignKey(d => d.IdUsuarioFacturacion)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_usuariofacturacion");
        });

        modelBuilder.Entity<JuncalOrdenInterno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.orden_interno");

            entity.HasIndex(e => e.IdAceria, "fk_id_aceria");

            entity.HasIndex(e => e.IdAcoplado, "fk_id_acoplado_interno");

            entity.HasIndex(e => e.IdCamion, "fk_id_camion");

            entity.HasIndex(e => e.IdContrato, "fk_id_contrato");

            entity.HasIndex(e => e.IdDireccionProveedor, "fk_id_direccion_proveedor");

            entity.HasIndex(e => e.IdEstadoInterno, "fk_id_estado_interno");

            entity.HasIndex(e => e.IdProveedor, "fk_id_proveedor_interno");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdAceria)
                .HasColumnType("int(11)")
                .HasColumnName("id_aceria");
            entity.Property(e => e.IdAcoplado)
                .HasColumnType("int(11)")
                .HasColumnName("id_acoplado");
            entity.Property(e => e.IdCamion)
                .HasColumnType("int(11)")
                .HasColumnName("id_camion");
            entity.Property(e => e.IdContrato)
                .HasColumnType("int(11)")
                .HasColumnName("id_contrato");
            entity.Property(e => e.IdDireccionProveedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_direccion_proveedor");
            entity.Property(e => e.IdEstadoInterno)
                .HasColumnType("int(11)")
                .HasColumnName("id_estado_interno");
            entity.Property(e => e.IdProveedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_proveedor");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Observaciones).HasMaxLength(255);
            entity.Property(e => e.Remito)
                .HasMaxLength(255)
                .HasColumnName("remito");

            entity.HasOne(d => d.IdAceriaNavigation).WithMany(p => p.JuncalOrdenInternos)
                .HasForeignKey(d => d.IdAceria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_aceria");

            entity.HasOne(d => d.IdAcopladoNavigation).WithMany(p => p.JuncalOrdenInternos)
                .HasForeignKey(d => d.IdAcoplado)
                .HasConstraintName("fk_id_acoplado_interno");

            entity.HasOne(d => d.IdCamionNavigation).WithMany(p => p.JuncalOrdenInternos)
                .HasForeignKey(d => d.IdCamion)
                .HasConstraintName("fk_id_camion");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.JuncalOrdenInternos)
                .HasForeignKey(d => d.IdContrato)
                .HasConstraintName("fk_id_contrato");

            entity.HasOne(d => d.IdDireccionProveedorNavigation).WithMany(p => p.JuncalOrdenInternos)
                .HasForeignKey(d => d.IdDireccionProveedor)
                .HasConstraintName("fk_id_direccion_proveedor");

            entity.HasOne(d => d.IdEstadoInternoNavigation).WithMany(p => p.JuncalOrdenInternos)
                .HasForeignKey(d => d.IdEstadoInterno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_estado_interno");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.JuncalOrdenInternos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("fk_id_proveedor_interno");
        });

        modelBuilder.Entity<JuncalOrdenMarterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.orden_marterial");

            entity.HasIndex(e => e.IdMaterial, "fk_orden_marterial_material");

            entity.HasIndex(e => e.IdOrden, "fk_orden_marterial_orden");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FacturadoParcial).HasColumnName("facturadoParcial");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.IdOrden)
                .HasColumnType("int(11)")
                .HasColumnName("id_orden");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.NumFactura)
                .HasMaxLength(255)
                .HasColumnName("num_Factura");
            entity.Property(e => e.Peso)
                .HasPrecision(10)
                .HasColumnName("peso");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.JuncalOrdenMarterials)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_marterial_material");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.JuncalOrdenMarterials)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_marterial_orden");
        });

        modelBuilder.Entity<JuncalOrdenMaterialInternoRecibido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.orden_material_interno_recibido");

            entity.HasIndex(e => e.IdMaterial, "fk_id_material_interno_recibido");

            entity.HasIndex(e => e.IdOrdenInterno, "fk_id_orden_interno_recibido");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.IdOrdenInterno)
                .HasColumnType("int(11)")
                .HasColumnName("id_orden_interno");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Peso)
                .HasPrecision(10)
                .HasColumnName("peso");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.JuncalOrdenMaterialInternoRecibidos)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_material_interno_recibido");

            entity.HasOne(d => d.IdOrdenInternoNavigation).WithMany(p => p.JuncalOrdenMaterialInternoRecibidos)
                .HasForeignKey(d => d.IdOrdenInterno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_orden_interno_recibido");
        });

        modelBuilder.Entity<JuncalOrdenMaterialInternoRecogido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.orden_material_interno_recogido");

            entity.HasIndex(e => e.IdMaterial, "fk_id_material_interno");

            entity.HasIndex(e => e.IdOrdenInterno, "fk_id_orden_interno");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.IdOrdenInterno)
                .HasColumnType("int(11)")
                .HasColumnName("id_orden_interno");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Peso)
                .HasPrecision(10)
                .HasColumnName("peso");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.JuncalOrdenMaterialInternoRecogidos)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_material_interno");

            entity.HasOne(d => d.IdOrdenInternoNavigation).WithMany(p => p.JuncalOrdenMaterialInternoRecogidos)
                .HasForeignKey(d => d.IdOrdenInterno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_orden_interno");
        });

        modelBuilder.Entity<JuncalPreFacturar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.preFacturar");

            entity.HasIndex(e => e.IdMaterialEnviado, "fk_MaterialEnviado_OrdenMaterial");

            entity.HasIndex(e => e.IdMaterialRecibido, "fk_MaterialRecibido_MaterialAceria");

            entity.HasIndex(e => e.IdOrden, "fk_idOrden_orden");

            entity.HasIndex(e => e.IdUsuarioFacturacion, "fk_idUsuarioFacturacion_Usuario");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Facturado).HasColumnName("facturado");
            entity.Property(e => e.IdMaterialEnviado)
                .HasColumnType("int(11)")
                .HasColumnName("id_material_enviado");
            entity.Property(e => e.IdMaterialRecibido)
                .HasColumnType("int(11)")
                .HasColumnName("id_material_recibido");
            entity.Property(e => e.IdOrden)
                .HasColumnType("int(11)")
                .HasColumnName("id_orden");
            entity.Property(e => e.IdUsuarioFacturacion)
                .HasColumnType("int(11)")
                .HasColumnName("idUsuarioFacturacion");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Peso)
                .HasPrecision(11)
                .HasColumnName("peso");
            entity.Property(e => e.PesoBruto)
                .HasPrecision(11)
                .HasColumnName("peso_bruto");
            entity.Property(e => e.PesoNeto)
                .HasPrecision(11)
                .HasColumnName("peso_neto");
            entity.Property(e => e.PesoTara)
                .HasPrecision(11)
                .HasColumnName("peso_tara");
            entity.Property(e => e.Remito)
                .HasMaxLength(255)
                .HasColumnName("remito");

            entity.HasOne(d => d.IdMaterialEnviadoNavigation).WithMany(p => p.JuncalPreFacturars)
                .HasForeignKey(d => d.IdMaterialEnviado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MaterialEnviado_OrdenMaterial");

            entity.HasOne(d => d.IdMaterialRecibidoNavigation).WithMany(p => p.JuncalPreFacturars)
                .HasForeignKey(d => d.IdMaterialRecibido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MaterialRecibido_MaterialAceria");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.JuncalPreFacturars)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idOrden_orden");

            entity.HasOne(d => d.IdUsuarioFacturacionNavigation).WithMany(p => p.JuncalPreFacturars)
                .HasForeignKey(d => d.IdUsuarioFacturacion)
                .HasConstraintName("fk_idUsuarioFacturacion_Usuario");
        });

        modelBuilder.Entity<JuncalProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.proveedor");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Origen)
                .HasMaxLength(255)
                .HasColumnName("origen");
        });

        modelBuilder.Entity<JuncalProveedorCuentaCorriente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.proveedor_CuentaCorriente");

            entity.HasIndex(e => e.IdProveedor, "fk_juncal.cuenta_corriente_proveedor");

            entity.HasIndex(e => e.IdTipoMovimiento, "fk_juncal.cuenta_corriente_tipo");

            entity.HasIndex(e => e.IdUsuario, "fk_juncal.cuenta_corriente_usuario");

            entity.HasIndex(e => e.IdMaterial, "fk_juncal.proveedor_cuentacorriente_listaprecios_materiales");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.IdProveedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_proveedor");
            entity.Property(e => e.IdRemitoExterno)
                .HasColumnType("int(11)")
                .HasColumnName("idRemitoExterno");
            entity.Property(e => e.IdRemitoInterno)
                .HasColumnType("int(11)")
                .HasColumnName("idRemitoInterno");
            entity.Property(e => e.IdTipoMovimiento)
                .HasColumnType("int(11)")
                .HasColumnName("id_tipo_movimiento");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.Importe)
                .HasPrecision(10)
                .HasColumnName("importe");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Observacion)
                .HasMaxLength(255)
                .HasColumnName("observacion");
            entity.Property(e => e.Peso).HasColumnName("peso");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.JuncalProveedorCuentaCorrientes)
                .HasForeignKey(d => d.IdMaterial)
                .HasConstraintName("fk_juncal.proveedor_cuentacorriente_listaprecios_materiales");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.JuncalProveedorCuentaCorrientes)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("fk_juncal.cuenta_corriente_proveedor");

            entity.HasOne(d => d.IdTipoMovimientoNavigation).WithMany(p => p.JuncalProveedorCuentaCorrientes)
                .HasForeignKey(d => d.IdTipoMovimiento)
                .HasConstraintName("fk_juncal.cuenta_corriente_tipo");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.JuncalProveedorCuentaCorrientes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_juncal.cuenta_corriente_usuario");
        });

        modelBuilder.Entity<JuncalProveedorListaprecio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.proveedor_listaprecios");

            entity.HasIndex(e => e.IdProveedor, "fk_juncal.proveedor_listaprecios_proveedor");

            entity.HasIndex(e => e.IdUsuario, "fk_juncal.proveedor_listaprecios_usuario");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("fecha_vencimiento");
            entity.Property(e => e.FechaVigencia)
                .HasColumnType("datetime")
                .HasColumnName("fecha_vigencia");
            entity.Property(e => e.IdProveedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_proveedor");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.JuncalProveedorListaprecios)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("fk_juncal.proveedor_listaprecios_proveedor");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.JuncalProveedorListaprecios)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_juncal.proveedor_listaprecios_usuario");
        });

        modelBuilder.Entity<JuncalProveedorListapreciosMateriale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.proveedor_listaprecios_materiales");

            entity.HasIndex(e => e.IdProveedorListaprecios, "fk_juncal.proveedor_listaprecios_juncal.proveedor_listaprecios");

            entity.HasIndex(e => e.IdMaterialJuncal, "fk_juncal.proveedor_listaprecios_materiales_juncal_material");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdMaterialJuncal)
                .HasColumnType("int(11)")
                .HasColumnName("id_material_juncal");
            entity.Property(e => e.IdProveedorListaprecios)
                .HasColumnType("int(11)")
                .HasColumnName("id_proveedor_listaprecios");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10)
                .HasColumnName("precio");

            entity.HasOne(d => d.IdMaterialJuncalNavigation).WithMany(p => p.JuncalProveedorListapreciosMateriales)
                .HasForeignKey(d => d.IdMaterialJuncal)
                .HasConstraintName("fk_juncal.proveedor_listaprecios_materiales_juncal_material");

            entity.HasOne(d => d.IdProveedorListapreciosNavigation).WithMany(p => p.JuncalProveedorListapreciosMateriales)
                .HasForeignKey(d => d.IdProveedorListaprecios)
                .HasConstraintName("fk_juncal.proveedor_listaprecios_juncal.proveedor_listaprecios");
        });

        modelBuilder.Entity<JuncalRemitosReclamado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.remitos_reclamados");

            entity.HasIndex(e => e.IdAceria, "fk_Aceria");

            entity.HasIndex(e => e.IdUsuarioReclamo, "fk_UsuarioReclamo");

            entity.HasIndex(e => e.IdRemito, "fk_remito");

            entity.HasIndex(e => e.IdUsuarioFinalizado, "fk_usuarioFinalizado");

            entity.HasIndex(e => e.IdUsuarioIngreso, "fk_usuarioIngres");

            entity.HasIndex(e => e.IdEstadoReclamo, "remitos_reclamados_Estados");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.FechaFinalizado).HasColumnName("fecha_finalizado");
            entity.Property(e => e.FechaReclamo).HasColumnName("fecha_reclamo");
            entity.Property(e => e.IdAceria)
                .HasColumnType("int(11)")
                .HasColumnName("idAceria");
            entity.Property(e => e.IdEstadoReclamo)
                .HasColumnType("int(11)")
                .HasColumnName("idEstadoReclamo");
            entity.Property(e => e.IdRemito)
                .HasColumnType("int(11)")
                .HasColumnName("idRemito");
            entity.Property(e => e.IdUsuarioFinalizado)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario_finalizado");
            entity.Property(e => e.IdUsuarioIngreso)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario_ingreso");
            entity.Property(e => e.IdUsuarioReclamo)
                .HasColumnType("int(11)")
                .HasColumnName("id_Usuario_reclamo");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("tinyint(4)")
                .HasColumnName("isDeleted");
            entity.Property(e => e.Observacion)
                .HasMaxLength(255)
                .HasColumnName("observacion");
            entity.Property(e => e.ObservacionFinalizado)
                .HasMaxLength(255)
                .HasColumnName("observacion_finalizado");
            entity.Property(e => e.ObservacionReclamo)
                .HasMaxLength(255)
                .HasColumnName("observacion_reclamo");

            entity.HasOne(d => d.IdAceriaNavigation).WithMany(p => p.JuncalRemitosReclamados)
                .HasForeignKey(d => d.IdAceria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Aceria");

            entity.HasOne(d => d.IdEstadoReclamoNavigation).WithMany(p => p.JuncalRemitosReclamados)
                .HasForeignKey(d => d.IdEstadoReclamo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("remitos_reclamados_Estados");

            entity.HasOne(d => d.IdRemitoNavigation).WithMany(p => p.JuncalRemitosReclamados)
                .HasForeignKey(d => d.IdRemito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_remito");

            entity.HasOne(d => d.IdUsuarioFinalizadoNavigation).WithMany(p => p.JuncalRemitosReclamadoIdUsuarioFinalizadoNavigations)
                .HasForeignKey(d => d.IdUsuarioFinalizado)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_usuarioFinalizado");

            entity.HasOne(d => d.IdUsuarioIngresoNavigation).WithMany(p => p.JuncalRemitosReclamadoIdUsuarioIngresoNavigations)
                .HasForeignKey(d => d.IdUsuarioIngreso)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_usuarioIngres");

            entity.HasOne(d => d.IdUsuarioReclamoNavigation).WithMany(p => p.JuncalRemitosReclamadoIdUsuarioReclamoNavigations)
                .HasForeignKey(d => d.IdUsuarioReclamo)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_UsuarioReclamo");
        });

        modelBuilder.Entity<JuncalRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.roles");

            entity.HasIndex(e => e.Id, "unq_roles_id").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalSucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.sucursal");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasColumnType("int(11)")
                .HasColumnName("numero");
        });

        modelBuilder.Entity<JuncalTipoAcoplado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.tipo_acoplado");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalTipoCamion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.tipo_camion");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalTransportistum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.transportista")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cuit)
                .HasMaxLength(255)
                .HasColumnName("cuit");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.usuario");

            entity.HasIndex(e => e.IdRol, "fk_usuario_roles");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasColumnType("int(11)")
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IdRol)
                .HasColumnType("int(11)")
                .HasColumnName("id_rol");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.PasswordHash)
                .HasColumnType("blob")
                .HasColumnName("passwordHASH");
            entity.Property(e => e.PasswordSalt)
                .HasColumnType("blob")
                .HasColumnName("passwordSALT");
            entity.Property(e => e.RefreshToken).HasMaxLength(255);
            entity.Property(e => e.TokenCreated).HasColumnType("datetime");
            entity.Property(e => e.TokenExpires).HasColumnType("datetime");
            entity.Property(e => e.Usuario)
                .HasMaxLength(255)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.JuncalUsuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_usuario_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

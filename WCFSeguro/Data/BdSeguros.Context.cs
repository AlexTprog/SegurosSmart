﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFSeguro.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDSegurosEntities : DbContext
    {
        public BDSegurosEntities()
            : base("name=BDSegurosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TMCliente> TMClientes { get; set; }
        public virtual DbSet<TMCompaniaAseguradora> TMCompaniaAseguradoras { get; set; }
        public virtual DbSet<TMSeguro> TMSeguroes { get; set; }
        public virtual DbSet<TRAfiliacion> TRAfiliacions { get; set; }
        public virtual DbSet<TRPago> TRPagoes { get; set; }
    }
}

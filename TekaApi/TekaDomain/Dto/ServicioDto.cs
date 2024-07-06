﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekaDomain.Dto
{

    public class TecnicoDto
    {
        public int IdTecnico { get; set; }
        public string NombreTecnico { get; set; }
        public string Cedula { get; set; }
        public string TelefonoTecnico { get; set; }
        public EstadoTecnicoDto EstadoTecnico { get; set; }
    }

    public class EstadoTecnicoDto
    {
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
    }

    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public CiudadDto Ciudad { get; set; }
    }

    public class CiudadDto
    {
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
    }

    public class EstadoUsuarioDto
    {
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
    }

    public class TipoServicioDto
    {
        public int IdTipoServicio { get; set; }
        public string NombreTipoServicio { get; set; }
    }

    public class HorarioDto
    {
        public int IdHorario { get; set; }
        public int IdTecnico { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string NombreTecnico { get; set; }
    }

    public class HorarioServicioDto
    {
        public int IdHorarioServicio { get; set; }
        public int IdHorario { get; set; }
        public int IdServicio { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string NombreTecnico { get; set; }
        public string NombreCliente { get; set; }
        public string TipoServicio { get; set; }
    }
}

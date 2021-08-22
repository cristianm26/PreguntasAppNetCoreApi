using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.Model
{
    public class Pregunta
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="varchar(100)")]
        public string Descripcion { get; set; }
        public int cuestionarioId { get; set; }
        public Cuestionario cuestionario { get; set; }
        public List<Respuesta> Respuesta { get; set; }

    }
}

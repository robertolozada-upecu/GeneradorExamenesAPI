using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class RegistroUsuarioResponseDTO
    {
        public bool RegistroSatisfactorio { get; set; }
        public IEnumerable<string> Errores { get; set; }
    }
}

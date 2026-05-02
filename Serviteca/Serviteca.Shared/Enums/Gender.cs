using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviteca.Shared.Enums
{
    public enum Gender
    {
        [Description("Masculino")]
        Male,

        [Description("Femenino")]
        Female,

        [Description("Otros")]
        Others,
    }
}

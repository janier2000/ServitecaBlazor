using System.ComponentModel;

namespace Serviteca.Shared.Enums;

public enum Gender
{
    [Description("Masculino")]
    Male,

    [Description("Femenino")]
    Female,

    [Description("Otros")]
    Others,
}
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project0.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class IncidentRecord
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public int Id_Employee { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}

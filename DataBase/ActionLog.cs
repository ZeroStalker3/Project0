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
    
    public partial class ActionLog
    {
        public int Id { get; set; }
        public string ActionDescription { get; set; }
        public System.DateTime ActionDate { get; set; }
        public int Id_User { get; set; }
        public int Id_Incident { get; set; }
        public int Id_employee { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual IncidentRecord IncidentRecord { get; set; }
        public virtual User User { get; set; }
    }
}
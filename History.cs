//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseWorkAPP
{
    using System;
    using System.Collections.Generic;
    
    public partial class History
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public System.DateTime changes_date { get; set; }
        public int control_points_id { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual ControlPoints ControlPoints { get; set; }
    }
}

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
    
    public partial class Point
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Point()
        {
            this.ControlPoints = new HashSet<ControlPoints>();
        }
    
        public int id { get; set; }
        public int clean_value { get; set; }
        public int noise_value { get; set; }
        public int w_value { get; set; }
    
        public virtual CleanValue CleanValue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlPoints> ControlPoints { get; set; }
        public virtual NoiseValue NoiseValue { get; set; }
    }
}
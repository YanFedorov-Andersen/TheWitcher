//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheWitcher.Domain.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClothesType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClothesType()
        {
            this.Clothes = new HashSet<Clothes>();
        }
    
        public int Id { get; set; }
        public string ClothesType1 { get; set; }
        public string TypeCharacteristics { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clothes> Clothes { get; set; }
    }
}

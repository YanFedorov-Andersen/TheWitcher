//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheWitcher.DataAccess.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class HeroInQuest
    {
        public int Id { get; set; }
        public Nullable<int> HeroId { get; set; }
        public Nullable<int> QuestId { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
    
        public virtual Heroes Heroes { get; set; }
        public virtual Quest Quest { get; set; }
    }
}

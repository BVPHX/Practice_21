//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practice_21
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    public partial class ReleasePlan
    {
        public int ProductCode { get; set; }
        public Nullable<int> ProductAmount { get; set; }
        public virtual Products Products { get; set; }
    }
}